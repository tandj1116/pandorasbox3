using System;
using System.Xml.Serialization;

namespace TheBox.BoxServer
{
	/// <summary>
	/// Summary description for ClientRequestHandler.
	/// </summary>
	public class ClientListCommand : BoxMessage
	{
		private string m_Command;
		private int m_Serial;

		[ XmlAttribute ]
		/// <summary>
		/// Gets or sets the command string
		/// </summary>
		public string Command
		{
			get { return m_Command; }
			set { m_Command = value; }
		}

		[ XmlAttribute ]
		/// <summary>
		/// Gets or sets the player serial
		/// </summary>
		public int Serial
		{
			get { return m_Serial; }
			set { m_Serial = value; }
		}

		public ClientListCommand()
		{
		}

		public ClientListCommand( int serial, string command )
		{
			m_Serial = serial;
			m_Command = command;
		}
	}
}
