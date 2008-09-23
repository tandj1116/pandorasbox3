using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

using TheBox.Buttons;

namespace TheBox.Data
{
	/// <summary>
	/// Provides managment for the Pandora's Box buttons
	/// </summary>
	public class ButtonManager
	{
		/// <summary>
		/// Creates a new button manager object
		/// </summary>
		public ButtonManager()
		{
		}

		private Assembly m_DefaultAssembly = null;

		private Assembly DefaultAssembly
		{
			get
			{
				if ( m_DefaultAssembly == null )
				{
					// Load assembly
					string file = Path.Combine( Pandora.Folder, "Data" );
					file = Path.Combine( file, "DefaultButtons.dll" );

					if ( ! File.Exists( file ) )
					{
						Pandora.Log.WriteError( null, "File {0} doesn't exist. Closing." );
						throw new System.IO.FileNotFoundException( "A required file was not found. Please reinstall the program", file, null );
					}

					m_DefaultAssembly = Assembly.LoadFile( file );
				}

				return m_DefaultAssembly;
			}
		}

		/// <summary>
		/// Gets the ButtonDef object given the button's ID
		/// </summary>
		public ButtonDef this[ BoxButton button ]
		{
			get
			{
				string filename = GetFile( button.ButtonID );

				if ( File.Exists( filename ) )
				{
					// There's a custom def

					try
					{
						ButtonDef def = Load( filename );
						button.Def = def;
						return def;
					}
					catch ( Exception err )
					{
						Pandora.Log.WriteError( err, string.Format( "Custom file for button {0} was corrupted. It has been renamed to {0}.old.xml", filename ) );

						try
						{
							File.Move( filename, string.Format( "{0}.old.xml", filename ) );
						}
						catch
						{
							Pandora.Log.WriteError( null, "Cannot rename file" );
						}
					}
				}
				// Get default

				string resource = string.Format( "DefaultButtons.{0}.xml", button.ButtonID );

				try
				{
					Stream stream = DefaultAssembly.GetManifestResourceStream( resource );

					if ( stream != null )
					{
						XmlSerializer serializer = new XmlSerializer( typeof( ButtonDef ) );
						button.Def = serializer.Deserialize( stream ) as ButtonDef;
						stream.Close();

						return button.Def;
					}
				}
				catch ( Exception err )
				{
					Pandora.Log.WriteError( err, "Error when loading defaults for button {0}", button.ButtonID );
				}

				button.Def = null;
				return null;
			}
			set
			{
				string filename = GetFile( button.ButtonID );

				try
				{
					Save( filename, value );
					button.Def = value;
				}
				catch ( Exception err )
				{
					Pandora.Log.WriteError( err, string.Format( "Error occurred when setting the def object for button #{0}", button.ButtonID ) );
					button.Def = null;
				}
			}
		}

		/// <summary>
		/// Gets the base folder for the storage of button files
		/// </summary>
		private string BaseFolder
		{
			get { return Path.Combine( Pandora.Profile.BaseFolder, "Buttons" ); }
		}

		/// <summary>
		/// Gets the filename for a given button def file
		/// </summary>
		/// <param name="ButtonID">The ID of the button</param>
		/// <returns>The full path to the file defining the button</returns>
		private string GetFile( int ButtonID )
		{
			return Path.Combine( BaseFolder, string.Format( "{0}.xml", ButtonID ) );
		}

		/// <summary>
		/// Restores the default value to a BoxButton
		/// </summary>
		/// <param name="button">The BoxButton that should be restored</param>
		public void ResetDefault( BoxButton button )
		{
			string filename = GetFile( button.ButtonID );

			if ( File.Exists( filename ) )
			{
				try
				{
					File.Delete( filename );
				}
				catch ( Exception err )
				{
					Pandora.Log.WriteError( err, string.Format( "Cannot delete file {0}", filename ) );
					return;
				}
			}

			button.Def = this[ button ];
		}

		/// <summary>
		/// Loads a ButtonDef
		/// </summary>
		/// <param name="FileName">The filename that should be loaded from</param>
		/// <returns>A ButtonDef object read from the file</returns>
		private ButtonDef Load( string FileName )
		{
			XmlSerializer serializer = new XmlSerializer( typeof( ButtonDef ) );

			FileStream stream = new FileStream( FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite );

			ButtonDef def = serializer.Deserialize( stream ) as ButtonDef;

			stream.Close();

			return def;
		}

		/// <summary>
		/// Saves a button def to file
		/// </summary>
		/// <param name="FileName">The filename that should be used to save the def</param>
		/// <param name="def">The ButtonDef object to save</param>
		private void Save( string FileName, ButtonDef def )
		{
			TheBox.Common.Utility.EnsureDirectory( Path.GetDirectoryName( FileName ) );

			XmlSerializer serializer = new XmlSerializer( typeof( ButtonDef ) );

			FileStream stream = new FileStream( FileName, FileMode.Create, FileAccess.Write, FileShare.None );

			serializer.Serialize( stream, def );

			stream.Close();
		}

		/// <summary>
		/// Sets the button in a blank state
		/// </summary>
		/// <param name="button">The BoxButton object to set</param>
		public void ClearButton( BoxButton button )
		{
			this[ button ] = new ButtonDef();
		}
	}
}
