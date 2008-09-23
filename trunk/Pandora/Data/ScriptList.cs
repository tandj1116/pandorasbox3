using System;
using System.Collections;
using System.Windows.Forms;

using TheBox.Common;

namespace TheBox.Data
{
	/// <summary>
	/// Provides access to generic scripted items
	/// </summary>
	public class ScriptList
	{
		private ArrayList m_Items;

		/// <summary>
		/// Gets or sets the list of generic nodes composing this object
		/// </summary>
		public ArrayList List
		{
			get
			{
				return m_Items;
			}
			set
			{
				m_Items = value;
			}
		}

		/// <summary>
		/// Creates a new ScriptList object
		/// </summary>
		public ScriptList()
		{
			m_Items = new ArrayList();
		}

		/// <summary>
		/// Creates a new ScriptList object and initializes the contents
		/// </summary>
		/// <param name="items">The ArrayList to use as content</param>
		public ScriptList( ArrayList items )
		{
			m_Items = items;
		}

		/// <summary>
		/// Updates the contents of the listing from a TreeNodeCollection object
		/// </summary>
		/// <param name="nodes">The collection of nodes holding the information to store</param>
		/// <param name="exclusions">A list of TreeNode items that should be excluded</param>
		public void Update( TreeNodeCollection nodes, params TreeNode[] exclusions )
		{
			m_Items.Clear();

			foreach ( TreeNode node in nodes )
			{
				bool exclude = false;

				foreach ( TreeNode cmp in exclusions )
				{
					if ( node == cmp )
					{
						exclude = true;
						break;
					}
				}

				if ( exclude )
				{
					continue;
				}

				m_Items.Add( DoNode( node ) );
			}

			OnSaving( new EventArgs() );
		}

		/// <summary>
		/// Process and converts a TreeNode
		/// </summary>
		/// <param name="node">The TreeNode to convert</param>
		/// <returns>A GenericNode object corresponding to the TreeNode</returns>
		private GenericNode DoNode( TreeNode node )
		{
			GenericNode gNode = new GenericNode( node.Text );

			foreach ( object o in ( node.Tag as ArrayList ) )
				gNode.Elements.Add( o );

			foreach ( TreeNode subNode in node.Nodes )
				gNode.Elements.Add( DoNode( subNode ) );

			return gNode;
		}

		/// <summary>
		/// Converts a GenericNode to a TreeNode
		/// </summary>
		/// <param name="from">The GenericNode to examine</param>
		/// <returns>A TreeNode object corresponding to the GenericNode supplied</returns>
		private TreeNode GetNode( GenericNode from )
		{
			TreeNode node = new TreeNode( from.Name );
			node.Tag = new ArrayList();

			foreach ( object o in from.Elements )
			{
				if ( o is GenericNode )
					node.Nodes.Add( GetNode( o as GenericNode ) );
				else
					( node.Tag as ArrayList ).Add( o );
			}

			return node;
		}

		/// <summary>
		/// Gets the TreeNodes corresponding to the list
		/// </summary>
		/// <returns></returns>
		public TreeNode[] GetNodes()
		{
			TreeNode[] nodes = new TreeNode[ m_Items.Count ];

			for( int i = 0; i < nodes.Length; i++ )
			{
				if ( m_Items[ i ] is GenericNode )
					nodes[ i ] = GetNode( m_Items[ i ] as GenericNode );
			}

			return nodes;
		}

		/// <summary>
		/// Gets the TreeNodes corresponding to the valid items in the list (ignores generic nodes)
		/// </summary>
		/// <param name="items">The list to evaluate</param>
		/// <returns>null if there are no valid entries in the array list, the corresponding tree nodes otherwise</returns>
		public TreeNode[] GetDataNodes( ArrayList items )
		{
			int count = 0;

			foreach ( object o in items )
				if ( ! ( o is GenericNode ) )
					count++;

			if ( count == 0 )
				return null;

			TreeNode[] nodes = new TreeNode[ count ];

			count = 0;

			foreach ( object o in items )
			{
				if ( ! ( o is GenericNode ) )
				{
					TreeNode node = new TreeNode();

					if ( o is BoxItem )
					{
						node.Text = ( o as BoxItem ).Name;
					}
					else if ( o is BoxMobile )
					{
						node.Text = ( o as BoxMobile ).Name;
					}

					node.Tag = o;

					nodes[ count++ ] = node;
				}
			}

			ExpandNames( nodes );
			return nodes;
		}

		/// <summary>
		/// Occurs when the object is being updated and requires saving
		/// </summary>
		public event EventHandler Saving;

		protected virtual void OnSaving( EventArgs e )
		{
			if ( Saving != null )
			{
				Saving( this, e );
			}
		}

		/// <summary>
		/// Adds a space before each uppercase letter
		/// </summary>
		/// <param name="nodes"></param>
		private void ExpandNames( TreeNode[] nodes )
		{
			foreach ( TreeNode node in nodes )
			{
				string text = node.Text;
				int index = 1;

				while ( index < text.Length )
				{
					if ( char.IsUpper( text, index ) )
					{
						if ( index < text.Length - 1 )
						{
							if ( char.IsLower( text, index + 1 ) )
							{
								text = text.Insert( index++, " " );
							}
						}
						else
						{
							// Last char, insert space only if after lowercase
							if ( char.IsLower( text, index - 1 ) )
							{
								text = text.Insert( index++, " " );
							}
						}
					}

					index++;
				}

				node.Text = text;
			}
		}
	}
}
