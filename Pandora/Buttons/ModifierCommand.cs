using System;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TheBox.Buttons
{
	/// <summary>
	/// Defines the function of a button that uses modifiers
	/// </summary>
	public class ModifierCommand : IButtonFunction, ICloneable
	{
		private string m_Command;

		[ XmlAttribute ]
		/// <summary>
		/// Gets or sets the base command used by this button
		/// </summary>
		public string Command
		{
			get { return m_Command; }
			set { m_Command = value; }
		}

		/// <summary>
		/// Performs the command
		/// </summary>
		/// <param name="modifier">Specifies the modifier</param>
		private void PerformCommand( string modifier )
		{
			string cmd = m_Command;

			if ( modifier != null )
				cmd = string.Format( "{0} {1}", modifier, cmd );

			if ( SendCommand != null )
			{
				SendCommand( this, new SendCommandEventArgs( cmd, true ) );
			}
		}

		[ XmlIgnore ]
		/// <summary>
		/// Gets the command callback for this button
		/// </summary>
		public CommandCallback CommandCallback
		{
			get { return new CommandCallback( PerformCommand ); }
		}

		public ModifierCommand()
		{
		}

		#region IButtonFunction Members

		public string Name
		{
			get
			{
				return "Buttons.Modifiers";
			}
		}

		public bool AllowsSecondButton
		{
			get
			{
				return false;
			}
		}

		public bool RequiresSecondButton
		{
			get
			{
				return false;
			}
		}

		public void DoAction(BoxButton button, System.Drawing.Point clickPoint, System.Windows.Forms.MouseButtons mouseButton)
		{
			if ( mouseButton == MouseButtons.Left )
			{
				if ( SendCommand != null )
				{
					SendCommand( this, new SendCommandEventArgs( m_Command, true ) );
				}
			}
			else
			{
				Pandora.cmModifiers.Show( button, clickPoint );
			}
		}

        //  Issue 9:  	 Warnings - Interface - Tarion
        protected virtual void OnSendLastCommand(EventArgs e)
        {
            if (SendLastCommand != null)
            {
                SendLastCommand(this, e);
            }
        }

        //  Issue 9:  	 Warnings - Interface - Tarion
        protected virtual void OnCommandChanged(CommandChangedEventArgs e)
        {
            if (CommandChanged != null)
            {
                CommandChanged(this, e);
            }
        }

		public event TheBox.SendCommandEventHandler SendCommand;

		public event System.EventHandler SendLastCommand;

		public event TheBox.CommandChangedEventHandler CommandChanged;

		#endregion

		#region ICloneable Members

		public object Clone()
		{
			ModifierCommand cmd = new ModifierCommand();
			cmd.Command = this.m_Command;

			return cmd;
		}

		#endregion
	}
}
