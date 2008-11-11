using System;
using System.IO;
// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
using System.Collections.Generic;
// Issue 10 - End
using System.Windows.Forms;
using System.Xml.Serialization;

using TheBox;
using TheBox.Common;

namespace TheBox.Data
{
	/// <summary>
	/// Manages the locations for Pandora's box
	/// </summary>
	public class TravelAgent
	{
		private Facet[] m_Facets;
		private string m_BaseFolder;
		private string[] m_FacetNames;

		/// <summary>
		/// Creates a TravelAgent, provider of travel data for Pandora
		/// </summary>
		public TravelAgent()
		{
			m_BaseFolder = Path.Combine( Pandora.Profile.BaseFolder, "Locations" );
			m_FacetNames = Pandora.Profile.Travel.MapNames;

			m_Facets = new Facet[ Pandora.Profile.Travel.MapCount ];

			Load();
		}

		private string GetFile( int Index )
		{
			string filename = Path.Combine( m_BaseFolder, string.Format( "map{0}.xml", Index ) );

			return filename;
		}

		private void SaveFile( int index )
		{
			string file = GetFile( index );

			Pandora.Log.WriteEntry( string.Format( "Saving file: {0}", file ) );

			XmlSerializer serializer = new XmlSerializer( typeof( Facet ) );

			try
			{
				FileStream stream = new FileStream( file, FileMode.Create );

				serializer.Serialize( stream, m_Facets[ index ] );

				stream.Close();
			}
			catch ( Exception err )
			{
				Pandora.Log.WriteError( err, string.Format( "Updates for map file {0} have not been recorded.", index ) );
			}
		}

		private void Load()
		{
			Utility.EnsureDirectory( m_BaseFolder );

			XmlSerializer serializer = new XmlSerializer( typeof( Facet ) );

			for ( int i = 0; i < Pandora.Profile.Travel.MapCount; i++ )
			{
				if ( !Pandora.Profile.Travel.EnabledMaps[ i ] )
					continue;

				Pandora.Log.WriteEntry( string.Format( "Reading locations file {0}: {1}", i, GetFile( i ) ) );

				FileStream stream = null;

				try
				{
					stream = new FileStream( GetFile( i ), FileMode.Open );
				}
				catch ( System.IO.FileNotFoundException )
				{
					Pandora.Log.WriteEntry( "Creating new data files for map {0}", i.ToString() );

					m_Facets[ i ] = new Facet();
					m_Facets[ i ].MapValue = (byte) i;

					continue;
				}
				catch ( Exception err )
				{
					Pandora.Log.WriteError( err, null );
					continue;
				}

				try
				{
					m_Facets[i] = (Facet)serializer.Deserialize(stream);
				}
				
				catch ( Exception err )
				{
					Pandora.Log.WriteError( err, null );
					continue;
				}

				m_Facets[ i ].MapValue = (byte) i;
				stream.Close();
			}
		}

		/// <summary>
		/// Gets a TreeNode array for the full version of the tree
		/// </summary>
		/// <returns>A TreeNode arraylist. Each node is a facet node</returns>
		public TreeNode[] GetFullTree()
		{
			int count = 0;

			for ( int i = 0; i < Pandora.Profile.Travel.MapCount; i++ )
			{
				if ( Pandora.Profile.Travel.EnabledMaps[ i ] )
					count++;
			}

			TreeNode[] nodes = new TreeNode[ count ];

			int index = 0;

			for ( int i = 0; i < Pandora.Profile.Travel.MapCount; i++ )
			{
				if ( Pandora.Profile.Travel.EnabledMaps[ i ] )
				{
					if ( m_Facets[ i ] == null )
					{
						m_Facets[ i ] = new Facet();
						m_Facets[ i ].MapValue = (byte) i;
						m_FacetNames[ i ] = Pandora.Profile.Travel.MapNames[ i ];
					}

					nodes[ index ] = m_Facets[ i ].GetTreeNode( m_FacetNames[ i ] );
					nodes[ index ].Tag = (int) m_Facets[ i ].MapValue;

					index++;
				}
			}

			return nodes;
		}

		/// <summary>
		/// Gets the tree nodes for a single facet
		/// </summary>
		/// <param name="mapfile">The ID of the facet</param>
		/// <returns>The array of nodes corresponding to the categories of the facet</returns>
		public TreeNode[] GetNode( int mapfile )
		{
			TreeNode facet = m_Facets[ mapfile ].GetTreeNode( "" );

			TreeNode[] nodes = new TreeNode[ facet.Nodes.Count ];

			for ( int i = 0; i < facet.Nodes.Count; i++ )
			{
				nodes[ i ] = facet.Nodes[ i ];
			}

			return nodes;
		}

		/// <summary>
		/// Converts an array list of locations into tree nodes
		/// </summary>
		/// <param name="locations">The list of locations</param>
		/// <returns>An array of TreeNode objects corresponding to the locations supplied</returns>
		// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
		public TreeNode[] GetLocationNodes( List<object> locations )
		// Issue 10 - End
		{
			TreeNode[] nodes = new TreeNode[ locations.Count ];

			for ( int i = 0; i < nodes.Length; i++ )
			{
				// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
				Location loc = locations[ i ] as Location;
				// Issue 10 - End

				nodes[ i ] = new TreeNode( loc.Name );
				nodes[ i ].Tag = loc;
			}

			return nodes;
		}

		/// <summary>
		/// Searches a tree
		/// </summary>
		/// <param name="text">The text to search for</param>
		/// <param name="nodes">The nodes to search</param>
		/// <param name="singlefacet">States whether the nodes correspond to a single facet, or to more than one facet</param>
		/// <returns>The SearchResult object with the results</returns>
		public SearchResults SearchFor( string text, TreeNodeCollection nodes, bool singlefacet )
		{
			if ( singlefacet )
			{
				return Facet.Search( nodes, text );
			}
			else
			{
				SearchResults results = Facet.Search( nodes[ 0 ].Nodes, text );

				for ( int i = 1; i < nodes.Count; i++ )
				{
					results.MergeWith( Facet.Search( nodes[ i ].Nodes, text ) );
				}

				return results;
			}
		}

		/// <summary>
		/// Updates the datafile for a given map
		/// </summary>
		/// <param name="map">The map index</param>
		/// <param name="nodes">The TreeNodeCollection representing the map</param>
		public void UpdateMap( int map, TreeNodeCollection nodes )
		{
			m_Facets[ map ] = Facet.FromTreeNodes( nodes, (byte) map );
			SaveFile( map );
		}

		/// <summary>
		/// Updates an existing facet overwriting the current one
		/// </summary>
		/// <param name="newFacet">The new facet</param>
		/// <param name="map">The map corresponding to the facet</param>
		public void UpdateFacet( Facet newFacet, int map )
		{
			m_Facets[ map ] = newFacet;
			SaveFile( map );
		}

		/// <summary>
		/// Resets the locations file to the default values
		/// </summary>
		public void Reset()
		{
			Pandora.Log.WriteEntry( "Restoring default locations" );

			if ( Directory.Exists( m_BaseFolder ) )
			{
				Directory.Delete( m_BaseFolder, true );
			}

			Directory.CreateDirectory( m_BaseFolder );

			for ( int i = 0; i < Pandora.Profile.Travel.MapCount; i++ )
			{
				if ( Pandora.Profile.Travel.EnabledMaps[ i ] )
				{
					string resource = string.Format( "Data.map{0}.xml", i );
					string file = string.Format( Path.Combine( m_BaseFolder, string.Format( "map{0}.xml", i ) ) );

					try
					{
						TheBox.Common.Utility.ExtractEmbeddedResource( Pandora.DataAssembly, resource, file );
						Pandora.Log.WriteEntry( "Restored default locations for map {0}", i );
					}
					catch ( Exception err )
					{
						Pandora.Log.WriteError( err, "Failed to extract locations for map {0}", i );
					}
				}
			}
		}
	}

	public class SupportFacet : Facet
	{
		private List<GenericNode> m_Nodes;

		public List<GenericNode> NodesS
		{
			get { return m_Nodes; }
			set { m_Nodes = value; }
		}
		public SupportFacet()
		{

		}
	}
}