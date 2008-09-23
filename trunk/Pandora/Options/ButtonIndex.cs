using System;
using System.Collections;
using System.IO;
using System.Xml;

using TheBox.Buttons;

namespace TheBox.Options
{
	/// <summary>
	/// Provides the default indexes on the multi command buttons
	/// </summary>
	public class ButtonIndex
	{
		private Hashtable m_Table;

		/// <summary>
		/// Creates a new Button Index provider for multi command buttons
		/// </summary>
		public ButtonIndex()
		{
			m_Table = new Hashtable();
		}

		/// <summary>
		/// Gets or sets the index for a given button id
		/// </summary>
		public int this[ int ButtonID ]
		{
			get
			{
				if ( m_Table.ContainsKey( ButtonID ) )
					return (int) m_Table[ ButtonID ];
				else
					return -1;
			}
			set
			{
				m_Table[ ButtonID ] = value;
				Save();
			}
		}

		public static ButtonIndex Load()
		{
			string filename = string.Format( Path.Combine( Pandora.Profile.BaseFolder, "bdi.xml" ) );

			if ( File.Exists( filename ) )
			{
				XmlDocument dom = new XmlDocument();

				dom.Load( filename );

				if ( dom.ChildNodes.Count != 2 )
				{
					Pandora.Log.WriteError( null, string.Format( "Bad format for file {0}", filename ) );
					return null;
				}

				XmlElement main = (XmlElement) dom.ChildNodes[ 1 ];

				if ( main.Name != "table" )
				{
					Pandora.Log.WriteError( null, string.Format( "Bad format for file {0}", filename ) );
					return null;
				}

				ButtonIndex index = new ButtonIndex();

				foreach ( XmlElement data in main.ChildNodes )
				{
					try
					{
						int key = Convert.ToInt32( data.Attributes[ "id" ].Value );
						int val = Convert.ToInt32( data.Attributes[ "index" ].Value );

						index.m_Table[ key ] = val;
					}
					catch ( Exception err )
					{
						Pandora.Log.WriteError( err, string.Format( "An error occurred when reading entries from {0}", filename ) );
						return null;
					}
				}

				return index;
			}
			else
			{
				return null;
			}
		}

		private void Save()
		{
			try
			{
				string filename = string.Format( Path.Combine( Pandora.Profile.BaseFolder, "bdi.xml" ) );

				XmlDocument dom = new XmlDocument();

				XmlDeclaration decl = dom.CreateXmlDeclaration( "1.0", null, null );
				dom.AppendChild( decl );

				XmlElement main = dom.CreateElement( "table" );

				foreach ( int key in m_Table.Keys )
				{
					XmlElement data = dom.CreateElement( "data" );

					XmlAttribute id = dom.CreateAttribute( "id" );
					id.Value = key.ToString();
					data.Attributes.Append( id );

					XmlAttribute val = dom.CreateAttribute( "index" );
					val.Value = m_Table[ key ].ToString();
					data.Attributes.Append( val );

					main.AppendChild( data );
				}

				dom.AppendChild( main );

				dom.Save( filename );
			}
			catch ( Exception err )
			{
				Pandora.Log.WriteError( err, string.Format( "Couldn't save the bdi.xml file" ) );
			}
		}

		/// <summary>
		/// Processes a button fixing its default index if appliable
		/// </summary>
		/// <param name="button"></param>
		public void DoButton( BoxButton button )
		{
			if ( button.Def != null && button.Def.MultiDef != null && button.ButtonID >= 0 )
			{
				object o = m_Table[ button.ButtonID ];

				if ( o != null )
				{
					button.Def.MultiDef.DefaultIndex = (int) o;
				}
			}
		}
	}
}