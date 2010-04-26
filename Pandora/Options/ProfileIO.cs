using System;
using System.IO;
// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
using System.Collections.Generic;
// Issue 10 - End
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Xml.Serialization;
using TheBox.Common;

namespace TheBox.Options
{
	/// <summary>
	/// Defines a class for importing/exporting PB2 profiles
	/// </summary>
	public class ProfileIO
	{
		private static int m_CurrentVersion = 1;
		public string Name;
		public TheBox.Options.CommandsOptions Commands;
		public bool CustomDeco = false;
		public string CommandPrefix;
		public string[] Modifiers;
		public bool[] ModifierWarnings;
		// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
		public List<string> SpeechPresets;
		public List<string> WebPresets;
		// Issue 10 - End
		public bool[] EnabledMaps;
		public string[] MapNames;
		public string[] EmbeddedList;
		public int MapIndex = -1;
		public int ButtonIndex = -1;
		public bool CustomMaps;
		public bool UseServer;
		public string Server;
		public int Port;
		public int ProfileVersion;

		private Profile m_Profile;

		public ProfileIO()
		{
		}

		public ProfileIO( Profile p )
		{
			ProfileVersion = m_CurrentVersion;

			m_Profile = p;

			Name = p.Name;

			Commands = p.Commands;

			CustomDeco = p.Deco.ShowCustomDeco;

			CommandPrefix = p.General.CommandPrefix;
			Modifiers = p.General.Modifiers;
			ModifierWarnings = p.General.ModifiersWarnings;
			SpeechPresets = p.General.SpeechPresets;
			WebPresets = p.General.WebPresets;

			EnabledMaps = p.Travel.EnabledMaps;
			MapNames = p.Travel.MapNames;
			CustomMaps = p.Travel.CustomMaps;

			UseServer = p.Server.Enabled;
			Server = p.Server.Address;
			Port = p.Server.Port;
		}

		public static Profile Load( string filename )
		{
			// Uncompress first of all
			FileStream fs = new FileStream( filename, FileMode.Open, FileAccess.Read, FileShare.Read );
			byte[] data = new byte[ fs.Length ];
			fs.Read( data, 0, data.Length );
			fs.Close();

			byte[] uncompressed = TheBox.Common.BoxZLib.Decompress( data );

			string temp = Path.Combine( Pandora.Folder, "temp.dll" );
			fs = new FileStream( temp, FileMode.Create, FileAccess.Write, FileShare.Read );
			fs.Write( uncompressed, 0, uncompressed.Length );
			fs.Close();

			if ( !File.Exists( temp ) )
				return null;

			Assembly asm = Assembly.LoadFile( temp );

			// Get ProfileIO object
			ProfileIO prof = Utility.GetEmbeddedObject( typeof( ProfileIO ), "ProfileIO.xml", asm ) as ProfileIO;

			if ( prof == null )
				return null;

			if ( prof.ProfileVersion > m_CurrentVersion )
			{
                if (Pandora.Localization.TextProvider != null)
					System.Windows.Forms.MessageBox.Show( Pandora.Localization.TextProvider[ "Misc.ProfileIOErr" ] );
				else
					System.Windows.Forms.MessageBox.Show( "Please upgrade your version of Pandora's Box.\n\nThe profile you are importing has been created with a more recent version of this software." );
			}

			prof.Generate( asm );

			Profile p = new Profile();

			p.Name = prof.Name;
			p.General.CommandPrefix = prof.CommandPrefix;
			p.Deco.ShowCustomDeco = prof.CustomDeco;
			p.Travel.EnabledMaps = prof.EnabledMaps;
			p.Travel.MapNames = prof.MapNames;
			p.Travel.CustomMaps = prof.CustomMaps;
			p.General.Modifiers = prof.Modifiers;
			p.General.ModifiersWarnings = prof.ModifierWarnings;
			p.General.SpeechPresets = prof.SpeechPresets;
			p.General.WebPresets = prof.WebPresets;
			p.Server.Enabled = prof.UseServer;
			p.Server.Address = prof.Server;
			p.Server.Port = prof.Port;
			p.Commands = prof.Commands;

			p.Save();

			return p;
		}

		private void Generate( Assembly asm )
		{
			// Verify name first
			if ( Profile.ExistingProfiles.Contains( Name ) )
			{
				string name = Name;
				int count = 1;

				while ( true )
				{
					Name = string.Format( "{0} {1}", name, count++ );

					if ( ! Profile.ExistingProfiles.Contains( Name ) )
					{
						break;
					}
				}
			}

            string folder = Path.Combine(ProfileManager.Instance.ProfilesFolder, Name);
			string mapsFolder = Path.Combine( folder, "Maps" );
			string locFolder = Path.Combine( folder, "Locations" );
			string butFolder = Path.Combine( folder, "Buttons" );

			Directory.CreateDirectory( folder );
			Directory.CreateDirectory( mapsFolder );
			Directory.CreateDirectory( locFolder );
			Directory.CreateDirectory( butFolder );

			int nextIndex = 0;

			if ( MapIndex > -1 )
				nextIndex = MapIndex;
			else if ( ButtonIndex > -1 )
				nextIndex = MapIndex;
			else
				nextIndex = EmbeddedList.Length;

			int index = 0;

			for ( index = 0; index < nextIndex; index++ )
			{
				string resource = EmbeddedList[ index ];

				Utility.ExtractEmbeddedResource( asm, resource, Path.Combine( folder, resource ) );
			}

			if ( MapIndex > -1 )
			{
				if ( ButtonIndex > -1 )
					nextIndex = ButtonIndex;
				else
					nextIndex = EmbeddedList.Length;

				for ( ; index < nextIndex; index++ )
				{
					string resource = EmbeddedList[ index ];

					string dest = resource.ToLower().EndsWith( ".xml" ) ? locFolder : mapsFolder;

					Utility.ExtractEmbeddedResource( asm, resource, Path.Combine( dest, resource ) );
				}
			}

			if ( ButtonIndex > -1 )
			{
				for ( ; index < EmbeddedList.Length; index ++ )
				{
					string resource = EmbeddedList[ index ];
					Utility.ExtractEmbeddedResource( asm, resource, Path.Combine( butFolder, resource ) );
				}
			}
		}

		public void Save( string filename )
		{
			if ( File.Exists( filename ) )
			{
				File.Delete( filename );
			}
			// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
			List<string> embedded = new List<string>();
			List<string> files = new List<string>();
			// Issue 10 - End

			embedded.Add( "BoxData.xml" );
			files.Add( Path.Combine( m_Profile.BaseFolder, "BoxData.xml" ) );

			embedded.Add( "PropsData.xml" );
			files.Add( Path.Combine( m_Profile.BaseFolder, "PropsData.xml" ) );

			embedded.Add( "HueGroups.xml" );
			files.Add( Path.Combine( m_Profile.BaseFolder, "HueGroups.xml" ) );

			embedded.Add( "RandomTiles.xml" );
			files.Add( Path.Combine( m_Profile.BaseFolder, "RandomTiles.xml" ) );

			embedded.Add( "Skills.ini" );
			files.Add( Path.Combine( m_Profile.BaseFolder, "Skills.ini" ) );

			if ( File.Exists( Path.Combine( m_Profile.BaseFolder, "SpawnData.xml" ) ) )
			{
				embedded.Add( "SpawnData.xml" );
				files.Add( Path.Combine( m_Profile.BaseFolder, "SpawnData.xml" ) );
			}
 
			if ( File.Exists( Path.Combine( m_Profile.BaseFolder, "SpawnGroups.xml" ) ) )
			{
				embedded.Add( "SpawnGroups.xml" );
				files.Add( Path.Combine( m_Profile.BaseFolder, "SpawnGroups.xml" ) );
			}

			MapIndex = embedded.Count;

			for ( int i = 0; i < 4; i++ )
			{
				string xml = Path.Combine( m_Profile.BaseFolder, Path.Combine( "Locations", string.Format( "map{0}.xml", i ) ) );
				string small = Path.Combine( m_Profile.BaseFolder, Path.Combine( "Maps", string.Format( "map{0}small.jpg", i ) ) );
				string big = Path.Combine( m_Profile.BaseFolder, Path.Combine( "Maps", string.Format( "map{0}big.jpg", i ) ) );

				if ( File.Exists( xml ) )
				{
					embedded.Add( Path.GetFileName( xml ) );
					files.Add( xml );
				}

				if ( File.Exists( small ) )
				{
					embedded.Add( Path.GetFileName( small ) );
					files.Add( small );
				}

				if ( File.Exists( big ) )
				{
					embedded.Add( Path.GetFileName( big ) );
					files.Add( big );
				}
			}

			ButtonIndex = embedded.Count;

			if ( ButtonIndex == MapIndex )
				MapIndex = -1;

			if ( Directory.Exists( Path.Combine( m_Profile.BaseFolder, "Buttons" ) ) )
			{
				string[] buttons = Directory.GetFiles( Path.Combine( m_Profile.BaseFolder, "Buttons" ) );

				foreach( string b in buttons )
				{
					embedded.Add( Path.GetFileName( b ) );
					files.Add( b );
				}
			}

			EmbeddedList = new string[ embedded.Count ];

			if ( EmbeddedList.Length == ButtonIndex )
				ButtonIndex = -1;

			string temp = Path.Combine( Pandora.Folder, "temp.xml" );

			// Build the string with all the embedded resources
			string res = string.Format( "/resource:\"{0}\",{1}", temp, "ProfileIO.xml" );

			for ( int i = 0; i < embedded.Count; i++ )
			{
				res += " ";
				// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
				res += string.Format( "/resource:\"{0}\",{1}", files[ i ], embedded[ i ]);
				
				EmbeddedList[ i ] = embedded[ i ];
				// Issue 10 - End
			}

			if ( ! TheBox.Common.Utility.SaveXml( this, temp ) )
			{
				Pandora.Log.WriteError( null, "Couldn't export profile {0}", m_Profile.Name );
				System.Windows.Forms.MessageBox.Show( "An error occurred, export failed." );
				return;
			}

			// Issue 3 - Obsolete interface - Useless code - http://code.google.com/p/pandorasbox3/issues/detail?id=3&can=1 - Kons
      CodeDomProvider compiler = CodeDomProvider.CreateProvider("CSharp");
			// Issue 3 - End

			CompilerParameters options = new CompilerParameters();

			options.CompilerOptions = res;
			options.GenerateExecutable = false;
			options.IncludeDebugInformation = false;
			options.OutputAssembly = filename;

			CompilerResults err = compiler.CompileAssemblyFromSource( options, "//Empty" );

			if ( File.Exists( filename ) )
			{
				FileStream fStream = new FileStream( filename, FileMode.Open, FileAccess.Read, FileShare.Read );
				byte[] data = new byte[ fStream.Length ];
				fStream.Read( data, 0, (int) fStream.Length );
				fStream.Close();

				File.Delete( filename );


                // Issue 4:  	 Profile Exporting does not work - Tarion
                byte[] compressed = null;
                try
                {
                    compressed = TheBox.Common.BoxZLib.Compress(data);
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("Missing zlib.dll, can not export profile.", "Missing library", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                // End Issue 4:

				if ( compressed != null )
				{
					FileStream fs = new FileStream( filename, FileMode.Create, FileAccess.Write, FileShare.Read );
					fs.Write( compressed, 0, compressed.Length );
					fs.Close();
					
					Pandora.Log.WriteEntry( "Profile {0} exported to {1}", m_Profile.Name, filename );
				}
			}

			if ( File.Exists( temp ) )
				File.Delete( temp );
		}
	}
}