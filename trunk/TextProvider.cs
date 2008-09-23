using System;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace TheBox.Lang
{
	[ Serializable ]
	/// <summary>
	/// Provides localized text elements for the box
	/// </summary>
	public class TextProvider
	{
		/// <summary>
		/// Gets the text associated with the specified resource
		/// </summary>
		public string this[string description]
		{
			get
			{
				string[] locate = description.Split( new char[] { '.' } );

				if ( locate.Length != 2 )
				{
					throw new Exception( "Bad text request" );
				}

				Hashtable loc = (Hashtable) m_Sections[ locate[ 0 ] ];

				if ( loc == null )
					return null;

				return (string) loc[ locate[1] ];
			}
			set
			{
				string[] locate = description.Split( new char[] { '.' } );

				if ( locate.Length != 2 )
				{
					throw new Exception( "Bad descriptor when adding a new entry to text provider" );
				}

				Add( value, locate[0], locate[1] );
			}
		}

		private Hashtable m_Sections;
		private string m_Language;

		/// <summary>
		/// Gets or sets a string identifying the language represented by the text provider
		/// </summary>
		public string Language
		{
			get { return m_Language; }
			set { m_Language = value; }
		}

		/// <summary>
		/// Gets or sets the data collection for this text provider
		/// </summary>
		public Hashtable Data
		{
			get { return m_Sections; }
			set { m_Sections = value; }
		}

		/// <summary>
		/// Creates a new TextProvider object
		/// </summary>
		public TextProvider()
		{
			m_Sections = new Hashtable();
		}

		private void Add( string text, string category, string definition )
		{
			Hashtable loc = null;

			if ( m_Sections.ContainsKey( category ) )
			{
				loc = (Hashtable) m_Sections[ category ];
			}
			else
			{
				loc = new Hashtable();

				m_Sections.Add( category, loc );
			}

			loc[ definition ] = text;
		}

		/// <summary>
		/// Deletes a section contained in the TextProvider
		/// </summary>
		/// <param name="name">The name of the section that will be deleted</param>
		public void DeleteSection( string name )
		{
			m_Sections.Remove( name );
		}

		/// <summary>
		/// Removes an item from the TextProvider
		/// </summary>
		/// <param name="section"></param>
		/// <param name="item"></param>
		public void RemoveItem( string section, string item )
		{
			Hashtable hash = (Hashtable) m_Sections[ section ];

			if ( hash != null )
			{
				hash.Remove( item );
			}
		}

		public void RemoveItem( string definition )
		{
			string[] loc = definition.Split( new char[] { '.' } );

			if ( loc.Length != 2 )
				return;

			RemoveItem( loc[0], loc[1] );
		}

		public void Serialize( string filename )
		{
			XmlDocument dom = new XmlDocument();

			XmlNode decl = dom.CreateXmlDeclaration( "1.0", null, null );

			dom.AppendChild( decl );

			XmlNode lang = dom.CreateElement( "Data" );

			XmlAttribute langtype = dom.CreateAttribute( "language" );
			langtype.Value = m_Language;
			lang.Attributes.Append( langtype );

			foreach ( string toplevel in m_Sections.Keys )
			{
				XmlNode topnode = dom.CreateElement( "section" );

				XmlAttribute topname = dom.CreateAttribute( "name" );
				topname.Value = toplevel;

				topnode.Attributes.Append( topname );

				Hashtable hash = ( (Hashtable) m_Sections[ toplevel ] );

				foreach( string lowlevel in hash.Keys )
				{
					XmlNode entrynode = dom.CreateElement( "entry" );

					XmlAttribute name = dom.CreateAttribute( "name" );
					name.Value = lowlevel;
					entrynode.Attributes.Append( name );

					XmlAttribute val = dom.CreateAttribute( "text" );
					val.Value = (string) hash[ lowlevel ];
					entrynode.Attributes.Append( val );

					topnode.AppendChild( entrynode );
				}
				
				lang.AppendChild( topnode );
			}

			dom.AppendChild( lang );

			dom.Save( filename );
		}

		public static TextProvider Deserialize( string filename )
		{
			XmlDocument dom = new XmlDocument();

			dom.Load( filename );

			XmlNode data = dom.ChildNodes[ 1 ];

			TextProvider text = new TextProvider();

			text.m_Language = data.Attributes[ "language" ].Value;

			foreach ( XmlNode section in data.ChildNodes )
			{
				string topkey = section.Attributes[ "name" ].Value;

				Hashtable hash = new Hashtable();

				foreach ( XmlNode entry in section.ChildNodes )
				{
					string lowkey = entry.Attributes[ "name" ].Value;
					string t = entry.Attributes[ "text" ].Value;

					hash.Add( lowkey, t );
				}

				text.m_Sections.Add( topkey, hash );
			}

			return text;
		}
	}
}