using System;
using System.Collections;
using System.Xml.Serialization;
using System.Windows.Forms;

using TheBox.Common;

namespace TheBox.Data
{
	/// <summary>
	/// Describes and categorizes all the locations for a given facet
	/// </summary>
	[ Serializable ]
	[ XmlInclude( typeof ( GenericNode ) ) ]
	[ XmlInclude( typeof ( Location ) ) ]
	public class Facet
	{
		private byte m_Map;
		private ArrayList m_Nodes;

		/// <summary>
		/// Gets or sets the map file corresponding to this facet
		/// </summary>
		[ XmlAttribute ]
		public byte MapValue
		{
			get { return m_Map; }
			set { m_Map = value; }
		}

		/// <summary>
		/// Gets or sets the category nodes
		/// </summary>
		public ArrayList Nodes
		{
			get { return m_Nodes; }
			set { m_Nodes = value; }
		}

		/// <summary>
		/// Creates a new facet
		/// </summary>
		public Facet()
		{
			m_Nodes = new ArrayList();
		}

		/// <summary>
		/// Gets a TreeNode corresponding to this facet
		/// </summary>
		/// <param name="name">The name of this facet</param>
		/// <returns></returns>
		public TreeNode GetTreeNode( string name )
		{
			TreeNode FacetNode = new TreeNode( name );

			foreach ( GenericNode Category in m_Nodes )
			{
				TreeNode CategoryNode = new TreeNode( Category.Name );

				foreach ( GenericNode Subsection in Category.Elements )
				{
					TreeNode SubsectionNode = new TreeNode( Subsection.Name );
					SubsectionNode.Tag = Subsection.Elements;

					CategoryNode.Nodes.Add( SubsectionNode );
				}

				FacetNode.Nodes.Add( CategoryNode );
			}

			return FacetNode;
		}

		/// <summary>
		/// Creates a Facet object from a collection of tree nodes
		/// </summary>
		/// <param name="nodes">The TreeNodeCollection used as source for this Facet object</param>
		/// <param name="name">The map file index corresponding to this facet</param>
		/// <returns>A Facet object representing the nodes collection</returns>
		public static Facet FromTreeNodes( TreeNodeCollection nodes, byte name )
		{
			Facet facet = new Facet();

			facet.MapValue = name;

			foreach ( TreeNode CatNode in nodes )
			{
				GenericNode Category = new GenericNode( CatNode.Text );

				foreach ( TreeNode SubNode in CatNode.Nodes )
				{
					GenericNode Subsection = new GenericNode( SubNode.Text );
					Subsection.Elements = ( ArrayList ) SubNode.Tag;

					Category.Elements.Add( Subsection );
				}

				facet.m_Nodes.Add( Category );
			}

			return facet;
		}

		/// <summary>
		/// Adds a new location to this facet
		/// </summary>
		/// <param name="loc">The location that should be added</param>
		/// <param name="category">The category name for the new location</param>
		/// <param name="subsection">The subsection name for the new location</param>
		public void AddLocation( Location loc, string category, string subsection )
		{
			GenericNode catNode = null;

			foreach ( GenericNode cat in m_Nodes )
			{
				if ( cat.Name.ToLower() == category.ToLower() )
				{
					catNode = cat;
					break;
				}
			}

			if ( catNode == null )
			{
				catNode = new GenericNode( category );
				m_Nodes.Add( catNode );
			}

			GenericNode subNode = null;

			foreach( GenericNode sub in catNode.Elements )
			{
				if ( sub.Name.ToLower() == subsection.ToLower() )
				{
					subNode = sub;
					break;
				}
			}

			if ( subNode == null )
			{
				subNode = new GenericNode( subsection );
				catNode.Elements.Add( subNode );
			}

			subNode.Elements.Add( loc );
		}

		/// <summary>
		/// Removes a location from the list
		/// </summary>
		/// <param name="loc">The location object that should be deleted</param>
		/// <param name="category">The category it belongs to</param>
		/// <param name="subsection">The subsection parent of the location</param>
		public void DeleteLocation( Location loc, string category, string subsection )
		{
			foreach ( GenericNode cat in m_Nodes )
			{
				if ( cat.Name.ToLower() == category.ToLower() )
				{
					foreach ( GenericNode sub in ( cat.Elements ) )
					{
						if ( sub.Name.ToLower() == subsection.ToLower() )
						{
							foreach ( Location l in sub.Elements )
							{
								if ( l == loc )
								{
									sub.Elements.Remove( loc );
									return;
								}
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Searches the current facet for locations according to an input text
		/// </summary>
		/// <param name="nodes">The TreeNodeCollection representing the category nodes of a facet</param>
		/// <param name="text">The text to search for in the location names</param>
		/// <returns>A SearchResults object</returns>
		public static SearchResults Search( TreeNodeCollection nodes, string text )
		{
			text = text.ToLower();
			SearchResults results = new SearchResults();

			foreach ( TreeNode cat in nodes )
			{
				foreach ( TreeNode sub in cat.Nodes )
				{
					foreach ( Location loc in sub.Tag as ArrayList )
					{
						if ( loc.Name.ToLower().IndexOf( text ) != -1 )
						{
							Result res = new Result( sub, ( sub.Tag as ArrayList ).IndexOf( loc ) );
							results.Add( res );
						}
					}
				}
			}

			return results;
		}
	}
}