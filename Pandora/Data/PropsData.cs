using System;
// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
using System.Collections.Generic;
// Issue 10 - End
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using System.Windows.Forms;

using TheBox.BoxServer;
using TheBox.Common;

namespace TheBox.Data
{
	[ Serializable, XmlInclude( typeof( BoxProp ) ), XmlInclude( typeof( BoxEnum ) ), XmlInclude( typeof( GenericNode ) ) ]
	/// <summary>
	/// Summary description for PropsData.
	/// </summary>
	public class PropsData
	{
		private static PropsData m_Props = null;
		private TreeNode[] m_TreeNodes;

		/// <summary>
		/// Occurs when the props data is changed
		/// </summary>
		public static event EventHandler PropsChanged;
		
		/// <summary>
		/// Get or sets the PropsData currently loaded
		/// </summary>
		public static PropsData Props
		{
			get
			{
				if ( m_Props == null || m_Props.m_Structure.Count == 0 )
				{
					m_Props = PropsData.Load();
				}

				return m_Props;
			}
			set
			{
				m_Props = value;
				m_Props.Save();

				if ( PropsChanged != null )
				{
					PropsChanged( null, new EventArgs() );
				}
			}
		}

		public BoxEnum FindEnum( string name )
		{
			foreach ( BoxEnum e in m_Props.m_Enums )
			{
				if ( name == e.Name )
					return e;
			}

			return null;
		}

		/// <summary>
		/// Searches for a specified class name (or part of it)
		/// </summary>
		/// <param name="text">The text to search for</param>
		/// <returns>A List of paths on the structure node. Path elements are separated by a dot.</returns>ù
		// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
		public List<string> FindClass( string text )
		// Issue 10 - End
		{
			text = text.ToLower();

			string path = "";
			// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
			List<string> results = new List<string>();
			// Issue 10 - End
			
			foreach( GenericNode gNode in m_Structure )
			{
				SearchNode( text, results, path, gNode );
			}

			return results;
		}

		/// <summary>
		/// Searches a GenericNode for a class name
		/// </summary>
		/// <param name="text">The string to search for</param>
		/// <param name="results">The List of strings containing the results</param>
		/// <param name="path">The current path on the structure tree</param>
		/// <param name="node">The GenericNode to search</param>
		// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
		private void SearchNode( string text, List<string> results, string path, GenericNode node )
		// Issue 10 - End
		{
			if ( path == "" )
				path += node.Name;
			else
				path += string.Format( ".{0}", node.Name );

			if ( node.Name.ToLower().IndexOf( text ) > -1 )
			{
				// This is a match
				results.Add( path );
			}

			// Recurse
			foreach ( object obj in node.Elements )
			{
				if ( obj is GenericNode )
				{
					SearchNode( text, results, path, obj as GenericNode );
				}
			}
		}

		/// <summary>
		/// Gets the classes tree nodes, including classes that only inherit properties
		/// </summary>
		public TreeNode[] TreeNodes
		{
			get
			{
				// Always refresh
				if ( Pandora.Profile.Props.ShowAllTypes )
				{
					m_TreeNodes = GetClassNodes();
				}
				else
				{
					m_TreeNodes = GetClassNodesSimple();
				}

				return m_TreeNodes;
			}
		}

		/// <summary>
		/// Loads the PropsData from the default file location
		/// </summary>
		/// <returns>The loaded PropsData object, or an empty PropsData if none was found</returns>
		public static PropsData Load()
		{
			PropsData pd = null;

			try
			{
				string file = Path.Combine( Pandora.Profile.BaseFolder, "PropsData.xml" );
				FileStream stream = new FileStream( file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite );
				XmlSerializer serializer = new XmlSerializer( typeof( PropsData ) );
				pd = serializer.Deserialize( stream ) as PropsData;

				return pd;
			}
			catch ( Exception err )
			{
				Pandora.Log.WriteError( err, "Couldn't load PropsData." );
				return new PropsData();
			}
		}

		/// <summary>
		/// Saves the PropsData to its default location
		/// </summary>
		public void Save()
		{
			try
			{
				string file = Path.Combine( Pandora.Profile.BaseFolder, "PropsData.xml" );
				FileStream stream = new FileStream( file, FileMode.Create, FileAccess.Write, FileShare.Read );
				XmlSerializer serializer = new XmlSerializer( typeof( PropsData ) );
				serializer.Serialize( stream, this );
			}
			catch ( Exception err )
			{
				Pandora.Log.WriteError( err, "Couldn't write PropsData." );
			}
		}

		/// <summary>
		/// Gets the class tree that doesn't display classes that only inherit
		/// </summary>
		/// <returns></returns>
		private TreeNode[] GetClassNodesSimple()
		{
			TreeNode[] nodes;

			if ( m_Structure.Count == 2 )
			{
				nodes = new TreeNode[ 2 ];

				for ( int i = 0; i < 2; i++ )
				{
					// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
					nodes[ i ] = DoNodeSimple( m_Structure[ i ] );
					// Issue 10 - End
				}
			}
			else
			{
				nodes = new TreeNode[ 0 ];
			}

			return nodes;
		}

		/// <summary>
		/// Creates a node and recurses through all its subnodes (purging prop-empty classes)
		/// </summary>
		/// <param name="gNode">The starting GenericNode</param>
		/// <returns>A TreeNode</returns>
		private TreeNode DoNodeSimple( GenericNode gNode )
		{
			TreeNode node = new TreeNode( gNode.Name );
			// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
			node.Tag = new List<object>();
			// Issue 10 - End

			for ( int i = 0; i < gNode.Elements.Count; i++ )
			{
				object obj = gNode.Elements[ i ];

				if ( obj is BoxProp )
				{
					( node.Tag as List<object> ).Add( obj as BoxProp );
				}
				else if ( obj is GenericNode )
				{
					TreeNode sub = DoNodeSimple( obj as GenericNode );

					// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
					if ( ( sub.Tag as List<object> ).Count > 0 || sub.Nodes.Count > 0 )
					// Issue 10 - End
					{
						node.Nodes.Add( sub );
					}
				}
			}

			return node;
		}

		/// <summary>
		/// Gets the nodes representing the classes
		/// </summary>
		/// <returns>A TreeNode array</returns>
		private TreeNode[] GetClassNodes()
		{
			TreeNode[] nodes;

			if ( m_Structure.Count == 2 )
			{
				nodes = new TreeNode[ 2 ];

				for ( int i = 0; i < 2; i++ )
				{
					// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
					nodes[ i ] = DoNode( m_Structure[ i ] );
					// Issue 10 - End
				}
			}
			else
			{
				nodes = new TreeNode[ 0 ];
			}

			return nodes;
		}

		/// <summary>
		/// Creates a node and recurses through all its subnodes
		/// </summary>
		/// <param name="gNode">The starting GenericNode</param>
		/// <returns>A TreeNode</returns>
		private TreeNode DoNode( GenericNode gNode )
		{
			TreeNode node = new TreeNode( gNode.Name );
			// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
			node.Tag = new List<object>();
			// Issue 10 - End

			for ( int i = 0; i < gNode.Elements.Count; i++ )
			{
				object obj = gNode.Elements[ i ];

				if ( obj is BoxProp )
				{
					// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
					( node.Tag as List<object> ).Add( obj as BoxProp );
					// Issue 10 - End
				}
				else if ( obj is GenericNode )
				{
					node.Nodes.Add( DoNode( obj as GenericNode ) );
				}
			}

			return node;
		}
		// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
		private List<GenericNode> m_Structure;
		private List<BoxEnum> m_Enums;

		public List<GenericNode> Structure
		{
			get { return m_Structure; }
			set { m_Structure = value; }
		}

		public List<BoxEnum> Enums
		// Issue 10 - End
		{
			get { return m_Enums; }
			set { m_Enums = value; }
		}

		public PropsData()
		{
			// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
			m_Structure = new List<GenericNode>();
			m_Enums = new List<BoxEnum>();
			// Issue 10 - End
		}
	}

	public enum BoxPropType
	{
		Boolean,
		Numeric,
		Text,
		TimeSpan,
		DateTime,
		Enumeration,
		Point3D,
		Map,
		Other
	}

	public class BoxProp
	{
		private string m_Name;
		private AccessLevel m_GetAccess;
		private AccessLevel m_SetAccess;
		private bool m_CanGet;
		private bool m_CanSet;
		private BoxPropType m_ValueType;
		private string m_EnumName;

		[ XmlAttribute ]
		public string Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}

		[ XmlAttribute ]
		public AccessLevel GetAccess
		{
			get { return m_GetAccess; }
			set { m_GetAccess = value; }
		}

		[ XmlAttribute ]
		public AccessLevel SetAccess
		{
			get { return m_SetAccess; }
			set { m_SetAccess = value; }
		}

		[ XmlAttribute ]
		public bool CanGet
		{
			get { return m_CanGet; }
			set { m_CanGet = value; }
		}

		[ XmlAttribute ]
		public bool CanSet
		{
			get { return m_CanSet; }
			set { m_CanSet = value; }
		}

		[ XmlAttribute ]
		public BoxPropType ValueType
		{
			get { return m_ValueType; }
			set { m_ValueType = value; }
		}

		[ XmlAttribute ]
		public string EnumName
		{
			get { return m_EnumName; }
			set { m_EnumName = value; }
		}

		public BoxProp()
		{
		}
	}

	/// <summary>
	/// Defines an enumeration used on the server
	/// </summary>
	public class BoxEnum
	{
		private string m_Name;
		// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
		private List<string> m_Values;
		// Issue 10 - End

		[ XmlAttribute ]
		/// <summary>
		/// Gets or sets the name of the enum
		/// </summary>
		public string Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}

		/// <summary>
		/// Gets or sets the possible values for this enum
		/// </summary>
		// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
		public List<string> Values
		// Issue 10 - End
		{
			get { return m_Values; }
			set { m_Values = value; }
		}

		/// <summary>
		/// Creates a new BoxEnum object
		/// </summary>
		public BoxEnum()
		{
			// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
			m_Values = new List<string>();
			// Issue 10 - End
		}
	}

	/*public class SupportProps : PropsData
	{
		private List<GenericNode> m_Structure;
		private List<SupportBoxEnum> m_Enums;

		public List<GenericNode> StructureS
		{
			get { return m_Structure; }
			set { m_Structure = value; }
		}

		public List<SupportBoxEnum> EnumsS
		{
			get { return m_Enums; }
			set { m_Enums = value; }
		}
		public SupportProps()
		{

		}
	}

	public class SupportBoxEnum : BoxEnum
	{
		private List<string> m_Values;

		public List<string> ValuesS
		{
			get { return m_Values; }
			set { m_Values = value; }
		}
		public SupportBoxEnum()
		{

		}
	}*/
}