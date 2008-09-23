using System;
using System.Collections;

namespace TheBox.Common
{
	/// <summary>
	/// Provides an implementation of a recently used objects list
	/// </summary>
	[ Serializable ]
	public class RecentList
	{
		private int m_Capacity = 10;
		private ArrayList m_List;

		/// <summary>
		/// Creates a new RecentList object
		/// </summary>
		public RecentList()
		{
			m_List = new ArrayList();
		}

		/// <summary>
		/// Creates a new RecentList object
		/// </summary>
		/// <param name="capacity"></param>
		public RecentList( int capacity ) : this()
		{
			m_Capacity = 10;
		}

		/// <summary>
		/// Gets or set the maximum capacity for the list
		/// </summary>
		[ System.Xml.Serialization.XmlAttribute ]
		public int Capacity
		{
			get { return m_Capacity; }
			set { m_Capacity = value; }
		}

		/// <summary>
		/// Gets or sets the items in the list
		/// </summary>
		public ArrayList List
		{
			get { return m_List; }
			set { m_List = value; }
		}

		/// <summary>
		/// Adds an object to the list
		/// </summary>
		/// <param name="o">The object to add</param>
		public void Add( object o )
		{
			if ( m_List.Contains( o ) )
			{
				// Move object to top
				m_List.Remove( o );
				m_List.Insert( 0, o );
			}
			else
			{
				m_List.Insert( 0, o );
				
				if ( m_List.Count > m_Capacity )
					m_List.RemoveRange( m_Capacity, m_List.Count );
			}
		}
	}
}