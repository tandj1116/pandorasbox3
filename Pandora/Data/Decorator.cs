using System;
using System.IO;
// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
using System.Collections.Generic;
// Issue 10 - End
using System.Windows.Forms;
using System.Xml.Serialization;

using TheBox.Common;

namespace TheBox.Data
{
	/// <summary>
	/// Summary description for Decorator.
	/// </summary>
	public class Decorator
	{
		private static BoxDecoList m_Default;
		private static BoxDecoList m_Custom;

		private static TreeNode[] m_DefaultNodes;

		/// <summary>
		/// Gets the treenodes that should be displayed on the tree
		/// </summary>
		public static TreeNode[] TreeNodes
		{
			get
			{
				TreeNode[] nodes = null;

				if ( Pandora.Profile.Deco.ShowCustomDeco  )
				{
					nodes = new TreeNode[ m_Default.Structure.Count + 1 ];

					if ( m_Custom.Structure.Count == 1 )
					{
						nodes[ 0 ] = GetNodes( m_Custom.Structure )[ 0 ];
					}
					else
					{
						nodes[ 0 ] = new TreeNode( Pandora.TextProvider[ "Deco.CustomNode" ] );
					}

					Array.Copy( m_DefaultNodes, 0, nodes, 1, m_DefaultNodes.Length );
				}
				else
				{
					nodes = m_DefaultNodes;
				}

				return nodes;
			}
		}

		/// <summary>
		/// Sets the custom decoration
		/// </summary>
		public static TreeNode CustomDeco
		{
			set
			{
				m_Custom.Structure.Clear();

				GenericNode n = new GenericNode( Pandora.TextProvider[ "Deco.CustomNode" ] );

				foreach ( TreeNode sub in value.Nodes )
				{
					GenericNode gNode = new GenericNode( sub.Text );

					// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
					gNode.Elements.AddRange( sub.Tag as List<object> );
					// Issue 10 - End
					gNode.Elements.Sort();

					n.Elements.Add( gNode );
				}

				n.Elements.Sort();
				m_Custom.Structure.Add( n );

				m_Custom.Save( Path.Combine( Pandora.Profile.BaseFolder, "CustomDeco.xml" ) );
			}
		}

		static Decorator()
		{
			// Load the default deco from the data assembly
			Stream stream = Pandora.DataAssembly.GetManifestResourceStream( "Data.Deco.xml" );
			XmlSerializer serializer = new XmlSerializer( typeof( BoxDecoList ) );
			m_Default = serializer.Deserialize( stream ) as BoxDecoList;
			stream.Close();

			/*Support s = new Support();
			s.StructureS = new List<GenericNode>();
			for(int i = 0; i < m_Default.Structure.Count; i++)
			{
				GenericNode n = m_Default.Structure[i] as GenericNode;
				s.StructureS.Add(n);
			}
			TextWriter w = new StreamWriter(@"c:\deco.xml");
			try
			{
			XmlSerializer ser = new XmlSerializer(typeof(Support));
			
				ser.Serialize(w, s);
			}
			catch (System.Exception e)
			{
				MessageBox.Show(e.ToString());
			}
			
			w.Close();*/

			string custom = Path.Combine( Pandora.Profile.BaseFolder, "CustomDeco.xml" );

			if ( File.Exists( custom ) )
			{
				try
				{
					FileStream cStream = new FileStream( custom, FileMode.Open, FileAccess.Read, FileShare.ReadWrite );
					m_Custom = serializer.Deserialize( cStream ) as BoxDecoList;
					cStream.Close();
				}
				catch ( Exception err )
				{
					Pandora.Log.WriteError( err, "Couldn't load custom deco for profile {0}", Pandora.Profile.Name );
					m_Custom = new BoxDecoList();
				}
			}
			else
			{
				m_Custom = new BoxDecoList();
			}

			// Create the default nodes
			m_DefaultNodes = GetNodes( m_Default.Structure );
		}

		/// <summary>
		/// Gets the tree nodes corresponding to the given list of generic nodes
		/// </summary>
		/// <param name="list">The list describing the generic nodes structure</param>
		/// <returns>An array of TreeNode items</returns>
		private static TreeNode[] GetNodes(List<GenericNode> list)
		{
			TreeNode[] nodes = new TreeNode[ list.Count ];

			for( int i = 0; i < list.Count; i++ )
			{
				GenericNode n = list[ i ] as GenericNode;

				TreeNode cat = new TreeNode( n.Name );
				nodes[ i ] = cat;

				foreach ( GenericNode n2 in n.Elements )
				{
					TreeNode sub = new TreeNode( n2.Name );
					cat.Nodes.Add( sub );
					// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
					sub.Tag = new List<object>();
					( sub.Tag as List<object> ).AddRange( n2.Elements );
					// Issue 10 - End
				}
			}

			return nodes;
		}
	}
}
