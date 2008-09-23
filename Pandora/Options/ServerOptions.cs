using System;
using System.Xml.Serialization;
using System.Text;
using System.Security.Cryptography;

namespace TheBox.Options
{
	[ Serializable ]
	/// <summary>
	/// Provides options for BoxServer
	/// </summary>
	public class ServerOptions
	{
		public ServerOptions()
		{
		}

		private string m_Address = "";
		private int m_Port = 8035;
		private string m_Username = "";
		private string m_Password = "";
		private bool m_ConnectOnStartup = false;
		private bool m_Enabled = false;
		private bool m_UseSHA1Crypt = false;

		/// <summary>
		/// Gets or sets a value stating whether Pandora should use BoxServer
		/// </summary>
		public bool Enabled
		{
			get { return m_Enabled; }
			set { m_Enabled = value; }
		}

		[ XmlAttribute ]
		/// <summary>
		/// Gets or sets the server address
		/// </summary>
		public string Address
		{
			get { return m_Address; }
			set { m_Address = value; }
		}

		[ XmlAttribute ]
		/// <summary>
		/// Gets or sets the Port number
		/// </summary>
		public int Port
		{
			get { return m_Port; }
			set { m_Port = value; }
		}

		[ XmlAttribute ]
		/// <summary>
		/// Gets or sets the Username
		/// </summary>
		public string Username
		{
			get { return m_Username; }
			set { m_Username = value; }
		}

		[ XmlAttribute ]
		/// <summary>
		/// Gets or sets the Password
		/// </summary>
		public string Password
		{
			get { return m_Password; }
			set { m_Password = value; }
		}

		[ XmlAttribute ]
		/// <summary>
		/// Gets or sets a value stating whether Pandora should connect on startup
		/// </summary>
		public bool ConnectOnStartup
		{
			get { return m_ConnectOnStartup; }
			set { m_ConnectOnStartup = value; }
		}

		/// <summary>
		/// Specifies whether the server uses the SHA1 crypt to perform hashing
		/// </summary>
		public bool UseSHA1Crypt
		{
			get { return m_UseSHA1Crypt; }
			set { m_UseSHA1Crypt = value; }
		}

		/// <summary>
		/// Sets the correct username and password for an outgoing BoxMessage
		/// </summary>
		/// <param name="msg">The BoxMessage that needs authentication values set</param>
		public void FillBoxMessage( TheBox.BoxServer.BoxMessage msg )
		{
			msg.Username = m_Username;
			msg.Password = m_Password;
		}

		/// <summary>
		/// Sets the password by performing the M5D hash
		/// </summary>
		/// <param name="password">The password value</param>
		public void SetPassword( string password )
		{
			if ( ! m_UseSHA1Crypt )
			{								  
				System.Text.Encoding encoding = System.Text.Encoding.ASCII;

				byte[] dataIn = new byte[ 256 ];

				System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();

				int length = System.Text.Encoding.ASCII.GetBytes( password, 0, password.Length > 256 ? 235 : password.Length, dataIn, 0 );

				byte[] hashed = md5.ComputeHash( dataIn, 0, length );

				m_Password = BitConverter.ToString( hashed );
			}
			else
			{
				m_Password = ComputeSHA1PasswordHash( password, m_Username );
			}
		}

		private static string ComputeSHA1PasswordHash( string password, string username )
		{
			HashAlgorithm hash = new SHA1CryptoServiceProvider();
			byte[] buffer = new byte[ 256 ];

			StringBuilder sb = new StringBuilder();

			sb.Append( username );
			sb.Append( username.Length );
			sb.Append( password );

			password = sb.ToString(); // Salted

			int length = Encoding.ASCII.GetBytes( password, 0, password.Length > 256 ? 256 : password.Length, buffer, 0 );
			byte[] hashed = hash.ComputeHash( buffer, 0, length );
			hashed = hash.ComputeHash( hashed, 0, hashed.Length );

			return BitConverter.ToString( hashed );
		}
	}
}