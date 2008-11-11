using System;
// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
using System.Collections.Generic;
// Issue 10 - end
using System.Xml.Serialization;

namespace TheBox.BoxServer
{
	[ Serializable, XmlInclude( typeof( ClientEntry ) ) ]
	/// <summary>
	/// Defines the list of clients currently connected
	/// </summary>
	public class ClientListMessage : BoxMessage
	{
		// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
		private List<ClientEntry> m_Clients;
		// Issue 10 - End

		/// <summary>
		/// Gets or sets the list of connected clients
		/// </summary>
		// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
		public List<ClientEntry> Clients
		// Issue 10 - End
		{
			get { return m_Clients; }
			set { m_Clients = value; }
		}

		public ClientListMessage()
		{
		}
	}

	public class ClientEntry
	{
		private string m_Name;
		private string m_Account;
		private int m_X;
		private int m_Y;
		private int m_Map;
		private DateTime m_LastLogin;
		private int m_Serial;

		[ XmlAttribute ]
		/// <summary>
		/// Gets or sets the name of the connected client
		/// </summary>
		public string Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}

		[ XmlAttribute ]
			/// <summary>
			/// Gets or sets the name of the connected client
			/// </summary>
		public string Account
		{
			get { return m_Account; }
			set { m_Account = value; }
		}

		[ XmlAttribute ]
			/// <summary>
			/// Gets or sets the X location of the connected client
			/// </summary>
		public int X
		{
			get { return m_X; }
			set { m_X = value; }
		}

		[ XmlAttribute ]
			/// <summary>
			/// Gets or sets the Y location of the connected client
			/// </summary>
		public int Y
		{
			get { return m_Y; }
			set { m_Y = value; }
		}

		[ XmlAttribute ]
			/// <summary>
			/// Gets or sets the map of the connected client
			/// </summary>
		public int Map
		{
			get { return m_Map; }
			set { m_Map = value; }
		}

		[ XmlAttribute ]
			/// <summary>
			/// Gets or sets the last login time
			/// </summary>
		public string LastLogin
		{
			get { return m_LastLogin.ToString(); }
			set
			{
				try { m_LastLogin = DateTime.Parse( value ); }
				catch { m_LastLogin = DateTime.MinValue; }
			}
		}

		[ XmlAttribute ]
			/// <summary>
			/// Gets or sets the mobile's serial
			/// </summary>
		public int Serial
		{
			get { return m_Serial; }
			set { m_Serial = value; }
		}

		[ XmlAttribute ]
		/// <summary>
		/// Gets the last login time
		/// </summary>
		public DateTime LoggedIn
		{
			get { return m_LastLogin; }
		}

		public ClientEntry()
		{
		}
	}
}
