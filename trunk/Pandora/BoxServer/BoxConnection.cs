using System;
using System.Windows.Forms;

namespace TheBox.BoxServer
{
	/// <summary>
	/// Provides methods for managing interaction with the BoxServer
	/// </summary>
	public class BoxConnection
	{
		private static BoxRemote m_Remote;

		public static void RequestConnection()
		{
			if ( Pandora.Profile.Server.Enabled )
			{
				if ( MessageBox.Show( Pandora.BoxForm,
					Pandora.TextProvider[ "Misc.RequestConnection" ],
					null,
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question ) == DialogResult.Yes )
				{
					Disconnect();

					TheBox.Forms.BoxServerForm form = new TheBox.Forms.BoxServerForm( false );
					form.ShowDialog();
				}
			}
			else
			{
				MessageBox.Show( Pandora.TextProvider[ "Errors.NoServer" ] );
			}
		}

		/// <summary>
		/// Checks a BoxMessage and processes any errors occurred
		/// </summary>
		/// <param name="msg">The BoxMessage returned by the server</param>
		/// <returns>True if the message is OK, false if errors have been found</returns>
		public static bool CheckErrors( BoxMessage msg )
		{
			if ( msg == null )
				return true; // null message means no error

			if ( msg is ErrorMessage )
			{
				// Generic error message
				MessageBox.Show( string.Format( Pandora.TextProvider[ "Errors.GenServErr" ], ( msg as ErrorMessage ).Message ) );
				return false;
			}
			else if ( msg is LoginError )
			{
				LoginError logErr = msg as LoginError;

				string err = null;

				switch ( logErr.Error )
				{
					case AuthenticationResult.AccessLevelError :
						
						err = Pandora.TextProvider[ "Errors.LoginAccess" ];
						break;

					case AuthenticationResult.OnlineMobileRequired:

						err = Pandora.TextProvider[ "Errors.NotOnline" ];
						break;

					case AuthenticationResult.UnregisteredUser:

						err = Pandora.TextProvider[ "Errors.LogUnregistered" ];
						break;

					case AuthenticationResult.WrongCredentials:

						err = Pandora.TextProvider[ "Errors.WrongCredentials" ];
						break;

					case AuthenticationResult.Success:

						return true;
				}

				MessageBox.Show( err );
				return false;
			}
			else if ( msg is FeatureNotSupported )
			{
				MessageBox.Show( Pandora.TextProvider[ "Errors.NotSupported" ] );
				return false;
			}

			return true;
		}

		/// <summary>
		/// Tries to connect to the BoxServer
		/// </summary>
		/// <returns>True if succesful</returns>
		public static bool Connect()
		{
			return Connect( true );
		}

		/// <summary>
		/// Tries to connect to the BoxServer
		/// </summary>
		/// <param name="ProcessErrors">Specifies whether to process errors and display them to the user</param>
		/// <returns>True if succesful</returns>
		public static bool Connect( bool ProcessErrors )
		{
			try
			{
				string ConnectionString = string.Format( "tcp://{0}:{1}/BoxRemote", Pandora.Profile.Server.Address, Pandora.Profile.Server.Port );

				m_Remote = Activator.GetObject( typeof( BoxRemote ), ConnectionString ) as BoxRemote;

				// Perform Login
				BoxMessage msg = new LoginMessage();

				msg.Username = Pandora.Profile.Server.Username;
				msg.Password = Pandora.Profile.Server.Password;
				byte[] data = msg.Compress();
				string outType = null;

				byte[] result = m_Remote.PerformRemoteRequest( msg.GetType().FullName, data, out outType );

				if ( result == null )
				{
					MessageBox.Show( Pandora.TextProvider[ "Errors.ServerError" ] );
					Pandora.Connected = false;
					return false;
				}

				Type t = Type.GetType( outType );

				BoxMessage outcome = BoxMessage.Decompress( result, t );

				if ( ProcessErrors )
				{
					if ( !CheckErrors( outcome ) )
					{
						Pandora.Connected = false;
						return false;
					}
				}

				if ( outcome is LoginSuccess )
				{
					Pandora.Connected = true;
					return true;
				}
				else
				{
					Pandora.Connected = false;
					return false;
				}
			}
			catch( Exception err )
			{
				Pandora.Log.WriteError( err, "Connection failed to box server" );
				Pandora.Connected = false;
			}

			return false;
		}

		/// <summary>
		/// Closes the connection with the server
		/// </summary>
		public static void Disconnect()
		{
			if ( m_Remote != null )
			{
				m_Remote = null;
				Pandora.Connected = false;
			}
		}

		/// <summary>
		/// Sends a message to the server
		/// </summary>
		/// <param name="msg">The message being sent to the server</param>
		/// <param name="window">Specifies whether to use the connection form</param>
		/// <returns>The outcome of the transaction</returns>
		public static BoxMessage ProcessMessage( BoxMessage msg, bool window )
		{
			if ( window )
			{
				TheBox.Forms.BoxServerForm form = new TheBox.Forms.BoxServerForm( msg );
				form.ShowDialog();
				return form.Response;
			}
			else
			{
				return ProcessMessage( msg );
			}
		}
	
		/// <summary>
		/// Sends a message to the server. Processes errors too.
		/// </summary>
		/// <param name="msg">The message to send to the server</param>
		/// <returns>A BoxMessage if there is one</returns>
		public static BoxMessage ProcessMessage( BoxMessage msg )
		{
			BoxMessage outcome = null;

			if ( !Pandora.Connected )
				Connect();

			if ( !Pandora.Connected )
				return null;

			byte[] data = msg.Compress();
			string outType = null;

			try
			{
				byte[] result = m_Remote.PerformRemoteRequest( msg.GetType().FullName, data, out outType );

				if ( result == null )
				{
					return null;
				}

				Type t = Type.GetType( outType );
				outcome = BoxMessage.Decompress( result, t );
				
				if ( !CheckErrors( outcome ) )
				{
					outcome = null;
				}
			}
			catch ( Exception err )
			{
				Pandora.Log.WriteError( err, "Error when processing a BoxMessage of type: {0}", msg.GetType().FullName );
				MessageBox.Show( Pandora.TextProvider[ "Errors.ConnectionLost" ] );
				Pandora.Connected = false;
				outcome = null;
			}

			return outcome;
		}
	}
}