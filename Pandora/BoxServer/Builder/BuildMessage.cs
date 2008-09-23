using System;
using System.Collections;
using System.Xml.Serialization;

namespace TheBox.BoxServer
{
	[ Serializable, XmlInclude( typeof( BuildItem ) ) ]
	/// <summary>
	/// Requests the server to build a structure
	/// </summary>
	public class BuildMessage : BoxMessage
	{
		private ArrayList m_Items;

		/// <summary>
		/// Gets or sets the items composing this structure
		/// </summary>
		public ArrayList Items
		{
			get { return m_Items; }
			set { m_Items = value; }
		}

		/// <summary>
		/// Creates a new build request
		/// </summary>
		public BuildMessage()
		{
			m_Items = new ArrayList();
		}
	}

	/// <summary>
	/// Defines an item part of a structure created in Pandora's Box
	/// </summary>
	[ Serializable ]
	public class BuildItem
	{
		private int m_ID;
		private int m_Hue;
		private int m_X;
		private int m_Y;
		private int m_Z;

		/// <summary>
		/// Gets or sets the item ID
		/// </summary>
		[ XmlAttribute ]
		public int ID
		{
			get { return m_ID; }
			set { m_ID = value; }
		}

		/// <summary>
		/// Gets or sets the item hue
		/// </summary>
		[ XmlAttribute ]
		public int Hue
		{
			get { return m_Hue; }
			set { m_Hue = value; }
		}

		/// <summary>
		/// Gets or sets the X coordinate
		/// </summary>
		[ XmlAttribute ]
		public int X
		{
			get { return m_X; }
			set { m_X = value; }
		}

		/// <summary>
		/// Gets or sets the Y coordinate
		/// </summary>
		[ XmlAttribute ]
		public int Y
		{
			get { return m_Y; }
			set { m_Y = value; }
		}

		/// <summary>
		/// Gets or sets the Z coordinate
		/// </summary>
		[ XmlAttribute ]
		public int Z
		{
			get { return m_Z; }
			set { m_Z = value; }
		}

		/// <summary>
		/// Creates a new BuildItem object
		/// </summary>
		public BuildItem()
		{
		}
	}
}
