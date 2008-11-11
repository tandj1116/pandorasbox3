using System;
using System.Xml;
using System.IO;
// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
using System.Collections.Generic;
// Issue 10 - End
using System.Windows.Forms;
using TheBox.Common;

namespace TheBox.Data
{
	/// <summary>
	/// Provides access to information about doors
	/// </summary>
	public class DoorsData
	{
		// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
		private List<GenericNode> m_Structure;
		// Issue 10 - End

		private string m_PortNS;
		private string m_PortEW;
		private int m_PortNSBase;
		private int m_PortEWBase;
		private string m_PortName;

		private ContextMenu m_Menu;

		/// <summary>
		/// Gets the doors context menu
		/// </summary>
		public ContextMenu Menu
		{
			get { return m_Menu; }
		}

		/// <summary>
		/// Occurs when a door is selected
		/// </summary>
		public event DoorEventHandler DoorSelected;

		/// <summary>
		/// Occurs when a portcullis is selected
		/// </summary>
		public event PortcullisEventHandler PortcullisSelected;

		/// <summary>
		/// Creates a new DoorsData object
		/// </summary>
		public DoorsData()
		{
			// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
			m_Structure = new List<GenericNode>();
			// Issue 10 - End
			Load();
			BuildMenu();
		}

		/// <summary>
		/// Loads the DoorsData from the Data assembly
		/// </summary>
		private void Load()
		{
			Stream stream = Pandora.DataAssembly.GetManifestResourceStream( "Data.Doors.xml" );
			XmlDocument dom = new XmlDocument();
			dom.Load( stream );
			stream.Close();

			XmlNode root = dom.ChildNodes[ 1 ];

			foreach( XmlNode xNode in root.ChildNodes )
			{
				if ( xNode.ChildNodes.Count == 0 )
				{
					// Portcullis
					m_PortNS = xNode.Attributes[ "itemNS" ].Value;
					m_PortEW = xNode.Attributes[ "itemEW" ].Value;
					m_PortName = xNode.Attributes[ "name" ].Value;
					m_PortEWBase = int.Parse( xNode.Attributes[ "EW" ].Value );
					m_PortNSBase = int.Parse ( xNode.Attributes[ "NS" ].Value );
				}
				else
				{
					GenericNode gNode = new GenericNode( xNode.Attributes[ "name" ].Value );
					m_Structure.Add( gNode );

					foreach( XmlNode doorNode in xNode.ChildNodes )
					{
						gNode.Elements.Add( DoorInfo.FromXmlNode( doorNode ) );
					}
				}
			}
		}

		/// <summary>
		/// Builds the context menu used to select doors
		/// </summary>
		private void BuildMenu()
		{
			m_Menu = new ContextMenu();

			foreach( GenericNode gNode in m_Structure )
			{
				MenuItem category = new MenuItem( gNode.Name );
				m_Menu.MenuItems.Add( category );

				foreach( DoorInfo door in gNode.Elements )
				{
					InternalMenuItem mi = new InternalMenuItem( door );
					mi.Click += new EventHandler(DoorClicked);
					category.MenuItems.Add( mi );
				}
			}

			MenuItem port = new MenuItem( m_PortName );
			port.Click += new EventHandler(PortcullisClicked);
			m_Menu.MenuItems.Add( port );
		}

		/// <summary>
		/// User clicks a door menu item
		/// </summary>
		private void DoorClicked(object sender, EventArgs e)
		{
			InternalMenuItem mi = sender as InternalMenuItem;

			if ( DoorSelected != null )
			{
				string category = null;

				foreach( MenuItem parent in m_Menu.MenuItems )
				{
					if ( parent.MenuItems.Contains( mi ) )
					{
						category = parent.Text;
						break;
					}
				}

				DoorSelected( new DoorEventArgs( mi.Door, category ) );
			}
		}

		/// <summary>
		/// User clicks the portcullis menu item
		/// </summary>
		private void PortcullisClicked(object sender, EventArgs e)
		{
			if ( PortcullisSelected != null )
			{
				PortcullisEventArgs args = new PortcullisEventArgs( m_PortName, m_PortNS, m_PortEW, m_PortNSBase, m_PortEWBase );
				PortcullisSelected( args );
			}
		}

		#region Internal Menu Item

		private class InternalMenuItem : MenuItem
		{
			private DoorInfo m_Door;

			/// <summary>
			/// Gets the door represented by this menu item
			/// </summary>
			public DoorInfo Door
			{
				get { return m_Door; }
			}

			public InternalMenuItem( DoorInfo info ) : base( info.Name )
			{
				m_Door = info;
			}
		}

		#endregion
	}

	#region Delegates

	public delegate void DoorEventHandler( DoorEventArgs e );
	public delegate void PortcullisEventHandler( PortcullisEventArgs e );

	#endregion

	#region Event Args

	/// <summary>
	/// Defines arguments when a portcullis is being selected
	/// </summary>
	public class PortcullisEventArgs : EventArgs
	{
		private string m_Name;
		private string m_ItemNS;
		private string m_ItemEW;
		private int m_ArtNS;
		private int m_ArtEW;

		/// <summary>
		/// Gets the Portcullis door name
		/// </summary>
		public string Name
		{
			get { return m_Name; }
		}

		/// <summary>
		/// Gets the item name for the NS orientation
		/// </summary>
		public string ItemNS
		{
			get { return m_ItemNS; }
		}

		/// <summary>
		/// Gets the item name for the EW orientation
		/// </summary>
		public string ItemEW
		{
			get { return m_ItemEW; }
		}

		/// <summary>
		/// Gets the art for the NS orientation
		/// </summary>
		public int ArtNS
		{
			get { return m_ArtNS; }
		}

		/// <summary>
		/// Gets the art for the EW orientation
		/// </summary>
		public int ArtEW
		{
			get { return m_ArtEW; }
		}

		public PortcullisEventArgs( string name, string itemNS, string itemEW, int artNS, int artEW )
		{
			m_Name = name;
			m_ItemNS = itemNS;
			m_ItemEW = itemEW;
			m_ArtNS = artNS;
			m_ArtEW = artEW;
		}
	}

	/// <summary>
	/// Defines the arguments of a door selected event
	/// </summary>
	public class DoorEventArgs : EventArgs
	{
		private string m_Name;
		private string m_Item;
		private int m_BaseID;
		private string m_Category;

		/// <summary>
		/// Gets the door name
		/// </summary>
		public string Name
		{
			get
			{
				if ( m_Category != null )
					return string.Format( "{0}:\n{1}", m_Category, m_Name );
				else
					return m_Name;
			}
		}

		/// <summary>
		/// Gets the item name
		/// </summary>
		public string Item
		{
			get { return m_Item; }
		}

		/// <summary>
		/// Gets the item base ID
		/// </summary>
		public int BaseID
		{
			get { return m_BaseID; }
		}

		/// <summary>
		/// Creates a new DoorEventArgs object
		/// </summary>
		/// <param name="info">The DoorInfo object describing the door</param>
		/// <param name="category">The category this door belongs to</param>
		public DoorEventArgs( DoorInfo info, string category )
		{
			m_Name = info.Name;
			m_Item = info.Item;
			m_BaseID = info.BaseID;
			m_Category = category;
		}
	}

	#endregion

	#region DoorInfo

	/// <summary>
	/// Defines a Door
	/// </summary>
	public class DoorInfo
	{
		private string m_Name;
		private string m_Item;
		private int m_BaseID;

		/// <summary>
		/// Gets or sets the door name
		/// </summary>
		public string Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}

		/// <summary>
		/// Gets or sets the door's item name
		/// </summary>
		public string Item
		{
			get { return m_Item; }
			set { m_Item = value; }
		}

		/// <summary>
		/// Gets or sets the door's base ID
		/// </summary>
		public int BaseID
		{
			get { return m_BaseID; }
			set { m_BaseID = value; }
		}

		/// <summary>
		/// Creates a new DoorInfo object
		/// </summary>
		private DoorInfo()
		{
		}

		/// <summary>
		/// Creates a DoorInfo from an Xml node
		/// </summary>
		/// <param name="xNode">The XmlNode to convert to a door info</param>
		/// <returns>A Door Info object</returns>
		public static DoorInfo FromXmlNode( XmlNode xNode )
		{
			DoorInfo door = new DoorInfo();

			door.m_Name = xNode.Attributes[ "name" ].Value;
			door.m_Item = xNode.Attributes[ "item" ].Value;
			door.m_BaseID = int.Parse( xNode.Attributes[ "base" ].Value );

			return door;
		}
	}

	#endregion
}