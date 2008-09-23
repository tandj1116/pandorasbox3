using System;
using System.Collections;
using System.Windows.Forms;
using TheBox.Common;
using System.Xml.Serialization;

namespace SoundExplorer
{
	public class UOSound
	{
		private int m_Index;
		private string m_Name;

		public UOSound()
		{
		}

		public UOSound( string name, int index )
		{
			m_Index = index;
			m_Name = name;
		}

		[ XmlAttribute ]
		/// <summary>
		/// Gets or sets the sound index
		/// </summary>
		public int Index
		{
			get { return m_Index; }
			set { m_Index = value; }
		}

		[ XmlAttribute ]
		/// <summary>
		/// Gets or sets the sound name
		/// </summary>
		public string Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}
	}

	[ Serializable, XmlInclude( typeof( GenericNode ) ), XmlInclude( typeof( UOSound ) ) ]
	/// <summary>
	/// Summary description for SoundData.
	/// </summary>
	public class SoundData
	{
		private ArrayList m_Structure;

		/// <summary>
		/// Gets or sets the sounds library structure
		/// </summary>
		public ArrayList Structure
		{
			get { return m_Structure; }
			set { m_Structure = value; }
		}

		public SoundData()
		{
			m_Structure = new ArrayList();
		}

		public SoundData( TreeNodeCollection nodes ) : this()
		{
			foreach ( TreeNode node in nodes )
			{
				m_Structure.Add( DoNode( node ) );
			}
		}

		private object DoNode( TreeNode node )
		{
			if ( node.Tag == null )
			{
				GenericNode cat = new GenericNode( node.Text );

				foreach ( TreeNode child in node.Nodes )
				{
					cat.Elements.Add( DoNode ( child ) );
				}

				return cat;
			}
			else
			{
				UOSound snd = new UOSound( node.Text, (int) node.Tag );

				return snd;
			}
		}

		public TreeNode[] TreeNodes
		{
			get
			{
				TreeNode[] nodes = new TreeNode[ m_Structure.Count ];

				for( int i = 0; i < nodes.Length; i++ )
				{
					nodes[ i ] = GetNode( m_Structure[ i ] as GenericNode );
				}

				return nodes;
			}
		}

		private TreeNode GetNode( GenericNode gNode )
		{
			TreeNode node = new TreeNode( gNode.Name );

			foreach ( object o in gNode.Elements )
			{
				GenericNode child = o as GenericNode;
				UOSound item = o as UOSound;

				if ( child != null )
				{
					node.Nodes.Add( GetNode( child ) );
				}
				else if ( item != null )
				{
					TreeNode itemNode = new TreeNode( item.Name );
					itemNode.Tag = item.Index;
					node.Nodes.Add( itemNode );
				}
			}

			return node;
		}
	}
}
