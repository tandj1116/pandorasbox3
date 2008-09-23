using System;

namespace TheBox.Buttons
{
	/// <summary>
	/// Summary description for LastCommand.
	/// </summary>
	public class LastCommand : IButtonFunction, ICloneable
	{
		/// <summary>
		/// Creates a new last command function
		/// </summary>
		public LastCommand()
		{
		}

		#region IButtonFunction Members

		public string Name
		{
			get { return "Buttons.LastCommand"; }
		}

		public bool AllowsSecondButton
		{
			get
			{
				return true;
			}
		}

		public bool RequiresSecondButton
		{
			get
			{
				return true;
			}
		}

		public void DoAction(BoxButton button, System.Drawing.Point clickPoint, System.Windows.Forms.MouseButtons mouseButton)
		{
			OnSendLastCommand( new EventArgs() );
		}

		/// <summary>
		/// Not used in this class
		/// </summary>
		public event TheBox.SendCommandEventHandler SendCommand;

		protected virtual void OnSendCommand( SendCommandEventArgs e )
		{
			if ( SendCommand != null )
			{
				SendCommand( this, e );
			}
		}

		/// <summary>
		/// Occurs when the button should send the last command
		/// </summary>
		public event System.EventHandler SendLastCommand;

		protected virtual void OnSendLastCommand( EventArgs e )
		{
			if ( SendLastCommand != null )
			{
				SendLastCommand( this, e );
			}
		}

		/// <summary>
		/// Not used in this class
		/// </summary>
		public event TheBox.CommandChangedEventHandler CommandChanged;

		protected virtual void OnCommandChanged( CommandChangedEventArgs e )
		{
			if ( CommandChanged != null )
			{
				CommandChanged( this, e );
			}
		}

		#endregion

		#region ICloneable Members

		public object Clone()
		{
			return new LastCommand();
		}

		#endregion
	}
}