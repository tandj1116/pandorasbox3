using System;
using System.Collections;
using System.Xml.Serialization;

namespace TheBox.Common
{
	/// <summary>
	/// GenericNode is a general purpose data structure shaped like a tree. Each node has
	/// a Name and a list of sub-items.
	/// </summary>
	public class GenericNode : IComparable
	{
		private string m_Name;
		private ArrayList m_Elements;

		/// <summary>
		/// Gets or sets the name of the node
		/// </summary>
		[ XmlAttribute ]
		public string Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}

		/// <summary>
		/// Gets or sets the subelements of this node
		/// </summary>
		public ArrayList Elements
		{
			get { return m_Elements; }
			set { m_Elements = value; }
		}

		/// <summary>
		/// Creates a new generic node object
		/// </summary>
		public GenericNode()
		{
			m_Elements = new ArrayList();
		}

		/// <summary>
		/// Creates a new generic node object
		/// </summary>
		/// <param name="name">The name of the node</param>
		public GenericNode( string name ) : this ()
		{
			m_Name = name;
		}

		#region IComparable Members

		/// <summary>
		/// Compares this GenericNode to a second GenericNode
		/// </summary>
		/// <param name="obj">The GenericNode to compare to</param>
		/// <returns>The comparison result</returns>
		public int CompareTo(object obj)
		{
			GenericNode cmp = obj as GenericNode;

			if ( cmp != null )
			{
				return m_Name.CompareTo( cmp.m_Name );
			}
			else
			{
				return 0;
			}
		}

		#endregion
	}
}
