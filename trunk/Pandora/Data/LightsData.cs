using System;
using System.IO;
using System.Collections;
using TheBox.Common;
using System.Xml;
using System.Drawing;

namespace TheBox.Data
{
	/// <summary>
	/// Provides access to the light sources structure
	/// </summary>
	public class LightsData
	{
		private ArrayList m_Structure;
		private string m_SelectedCategory;

		public LightsData()
		{
			m_Structure = new ArrayList();
			CreateStructure();

			GenericNode gNode = m_Structure[ 0 ] as GenericNode;
			m_SelectedCategory = gNode.Name;
		}

		/// <summary>
		/// Reads the lights structure
		/// </summary>
		private void CreateStructure()
		{
			Stream stream = Pandora.DataAssembly.GetManifestResourceStream( "Data.Lights.xml" );
			XmlDocument dom = new XmlDocument();
			dom.Load( stream );
			stream.Close();

			XmlNode root = dom.ChildNodes[ 1 ];
         
			foreach ( XmlNode catNode in root.ChildNodes )
			{
				GenericNode gNode = new GenericNode( catNode.Attributes[ "name" ].Value );
				m_Structure.Add( gNode );

				foreach ( XmlNode lightNode in catNode.ChildNodes )
				{
					gNode.Elements.Add( lightNode.Attributes[ "name" ].Value );
				}
			}
		}

		/// <summary>
		/// Gets the list of categories available
		/// </summary>
		public string[] Categories
		{
			get
			{
				string[] categories = new string[ m_Structure.Count ];

				for ( int i = 0; i < categories.Length; i++ )
				{
					GenericNode gNode = m_Structure[ i ] as GenericNode;

					categories[ i ] = gNode.Name;
				}

				return categories;
			}
		}

		/// <summary>
		/// Gets or sets the currently selected categories
		/// </summary>
		public string SelectedCategory
		{
			get { return m_SelectedCategory; }
			set
			{
				if ( value != m_SelectedCategory )
				{
					foreach ( GenericNode gNode in m_Structure )
					{
						if ( value == gNode.Name )
						{
							m_SelectedCategory = value;
						}
					}
				}
			}
		}

		/// <summary>
		/// Gets the generic node for the currently selected category
		/// </summary>
		private GenericNode SelectedNode
		{
			get
			{
				foreach ( GenericNode gNode in m_Structure )
				{
					if ( gNode.Name == m_SelectedCategory )
					{
						return gNode;
					}
				}

				return null;
			}
		}

		/// <summary>
		/// Gets the list of images for the current selection
		/// </summary>
		public Image[] Images
		{
			get
			{
				GenericNode gNode = SelectedNode;

				Image[] images = new Image[ gNode.Elements.Count ];

				for ( int i = 0; i < images.Length; i++ )
				{
					string name = (string) gNode.Elements[ i ];
					string location = string.Format( "Data.Lights.{0}.{1}.jpg", m_SelectedCategory, name );

					images[ i ] = Bitmap.FromStream( Pandora.DataAssembly.GetManifestResourceStream( location ) );
				}

				return images;
			}
		}

		/// <summary>
		/// Gets the list of names for the sources in the selected category
		/// </summary>
		public string[] Names
		{
			get
			{
				GenericNode gNode = SelectedNode;

				string[] names = new string[ gNode.Elements.Count ];

				for ( int i = 0; i < names.Length; i++ )
				{
					names[ i ] = (string) gNode.Elements[ i ];
				}

				return names;
			}
		}
	}
}