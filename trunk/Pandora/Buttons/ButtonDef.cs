using System;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

using TheBox.Common;

namespace TheBox.Buttons
{
	[ Serializable ]
	[ XmlInclude( typeof( LastCommand ) ) ]
	[ XmlInclude( typeof( MenuCommand ) ) ]
	[ XmlInclude( typeof( MenuDef ) ) ]
	[ XmlInclude( typeof( MultiCommandDef ) ) ]
	[ XmlInclude( typeof( GenericNode ) ) ]
	[ XmlInclude( typeof( ModifierCommand ) ) ]
	/// <summary>
	/// Defines the behaviour of a BoxButton
	/// </summary>
	public class ButtonDef : IDisposable, ICloneable
	{
		/// <summary>
		/// Creates a new button definition
		/// </summary>
		public ButtonDef()
		{
		}

		private IButtonFunction m_Left = null;
		private IButtonFunction m_Right = null;
		private string m_Caption = "";
		private SendCommandEventArgs m_LastCommand = null;

		/// <summary>
		/// Occurs when the tool tip text changes
		/// </summary>
		public event ToolTipChangedEventHandler ToolTipChanged;

		protected virtual void OnToolTipChanged( ToolTipEventArgs e )
		{
			if ( ToolTipChanged != null )
			{
				ToolTipChanged( this, e );
			}
		}

		[ XmlIgnore ]
		/// <summary>
		/// Gets or sets the action performed by the left button
		/// </summary>
		public IButtonFunction Left
		{
			get { return m_Left; }
			set
			{
				if ( value == m_Left )
					return;

				if ( m_Left != null )
				{
					IDisposable d = m_Left as IDisposable;

					if ( d != null )
						d.Dispose();
				}

				m_Left = value;

				if ( m_Left != null )
				{
					m_Left.CommandChanged += new CommandChangedEventHandler( OnCommandChanged );
					m_Left.SendCommand += new SendCommandEventHandler( OnChildSendCommand );
					m_Left.SendLastCommand += new EventHandler( SendLastCommand );
				}

				UpdateToolTip();
			}
		}

		[ XmlIgnore ]
		/// <summary>
		/// Gets or sets the action performed by the right button
		/// </summary>
		public IButtonFunction Right
		{
			get { return m_Right; }
			set
			{
				if ( value == m_Right )
					return;

				if ( m_Right != null )
				{
					IDisposable d = m_Right as IDisposable;

					if ( d != null )
						d.Dispose();
				}

				m_Right = value;

				if ( m_Right != null )
				{
					m_Right.CommandChanged += new CommandChangedEventHandler( OnCommandChanged );
					m_Right.SendCommand += new SendCommandEventHandler( OnChildSendCommand );
					m_Right.SendLastCommand += new EventHandler( SendLastCommand );
				}

				UpdateToolTip();
			}
		}

		/// <summary>
		/// Gets or sets the object assigned to the right button
		/// </summary>
		public object RightObject
		{
			get { return m_Right as object; }
			set { Right = value as IButtonFunction; }
		}

		/// <summary>
		/// Gets or sets the object assigned to the left button
		/// </summary>
		public object LeftObject
		{
			get { return m_Left as object; }
			set { Left = value as IButtonFunction; }
		}

		protected void SendLastCommand( object sender, EventArgs e )
		{
			if ( m_LastCommand != null )
			{
				OnSendCommand( m_LastCommand );
			}
		}

		protected void OnChildSendCommand( object sender, SendCommandEventArgs e )
		{
			m_LastCommand = e;
			OnSendCommand( e );

			UpdateToolTip();
		}

		protected void OnCommandChanged( object sender, CommandChangedEventArgs e )
		{
			m_Caption = e.Command.Caption;

			OnCaptionChanged( new EventArgs() );

			UpdateToolTip();
		}

		[ System.Xml.Serialization.XmlAttribute ]
		/// <summary>
		/// Gets or sets the current button caption
		/// </summary>
		public string Caption
		{
			get
			{
				if ( m_Left is MultiCommandDef )
				{
					return ( m_Left as MultiCommandDef ).DefaultCommand.Caption;
				}

				if ( m_Right is MultiCommandDef )
				{
					return ( m_Right as MultiCommandDef ).DefaultCommand.Caption;
				}

				if ( m_Left == null && m_Right == null )
					return string.Empty;

				return m_Caption;
			}
			set
			{
				m_Caption = value;
				OnCaptionChanged( new EventArgs() );
			}
		}

		/// <summary>
		/// Occurs when the caption of the button should change
		/// </summary>
		public event EventHandler CaptionChanged;

		protected virtual void OnCaptionChanged( EventArgs e )
		{
			if ( CaptionChanged != null )
			{
				CaptionChanged( this, e );
			}
		}

		/// <summary>
		/// Occurs when the button must send a command
		/// </summary>
		public event SendCommandEventHandler SendCommand;

		protected virtual void OnSendCommand( SendCommandEventArgs e )
		{
			if ( SendCommand != null )
			{
				SendCommand( this, e );
			}
		}

		/// <summary>
		/// Executes the action specified for this button
		/// </summary>
		/// <param name="button">The BoxButton that owns this MenuDef object</param>
		/// <param name="clickPoint">The location of the click</param>
		/// <param name="mouseButton">The mouse button clicked</param>
		public void DoAction( BoxButton button, System.Drawing.Point clickPoint, MouseButtons mouseButton )
		{
			switch ( mouseButton )
			{
				case MouseButtons.Left:

					if ( m_Left != null )
					{
						m_Left.DoAction( button, clickPoint, mouseButton );
					}
					else if ( m_Right != null && !m_Right.AllowsSecondButton )
					{
						m_Right.DoAction( button, clickPoint, mouseButton );
					}
					break;

				case MouseButtons.Right:

					if ( m_Right != null )
					{
						m_Right.DoAction( button, clickPoint, mouseButton );
					}
					else if ( m_Left != null && !m_Left.AllowsSecondButton )
					{
						m_Left.DoAction( button, clickPoint, mouseButton );
					}
					break;
			}
		}

		/// <summary>
		/// Verifies if adding an item as left button will function properly
		/// </summary>
		/// <param name="left">The left button candidate</param>
		/// <returns>True if the combination is valid, false otherwise</returns>
		public bool TryLeft( IButtonFunction left )
		{
			if ( m_Right == null )
			{
				if ( left.RequiresSecondButton )
					return false;
				else
					return true;
			}
			else
			{
				if ( m_Right.AllowsSecondButton && left.AllowsSecondButton )
					return true;
				else
					return false;
			}
		}

		/// <summary>
		/// Verifies if adding an item as right button will function properly
		/// </summary>
		/// <param name="right">The right button candidate</param>
		/// <returns>True if the combination is valid, false otherwise</returns>
		public bool TryRight( IButtonFunction right )
		{
			if ( m_Left == null )
			{
				if ( right.RequiresSecondButton )
					return false;
				else
					return true;
			}
			else
			{
				if ( m_Left.AllowsSecondButton && right.AllowsSecondButton )
					return true;
				else
					return false;
			}
		}

		/// <summary>
		/// Saves the ButtonDef object to an xml file
		/// </summary>
		/// <param name="FileName">The target filename for this</param>
		/// <returns>True if succesful, false otherwise</returns>
		public bool Save( string FileName )
		{
			try
			{
				XmlSerializer serializer = new XmlSerializer( typeof( ButtonDef ) );

				FileStream stream = new FileStream( FileName, FileMode.Create, FileAccess.Write, FileShare.None );

				serializer.Serialize( stream, this );

				stream.Close();	

				Pandora.Log.WriteEntry( string.Format( "Custom button definition serizlized correctly to {0}", FileName ) );
			}
			catch( Exception err )
			{
				Pandora.Log.WriteError( err, string.Format( "Failed to serialize custom button definition to: {0}", FileName ) );
				return false;
			}

			return true;
		}

		/// <summary>
		/// Loads a ButtonDef from an xml file
		/// </summary>
		/// <param name="FileName">The file to read the definition from</param>
		/// <returns>A ButtonDef object read from the XML</returns>
		public static ButtonDef Load( string FileName )
		{
			try
			{
				XmlSerializer serializer = new XmlSerializer( typeof( ButtonDef ) );

				FileStream stream = new FileStream( FileName, FileMode.Open, FileAccess.Read, FileShare.Read );

				ButtonDef def = serializer.Deserialize( stream ) as ButtonDef;

				Pandora.Log.WriteEntry( string.Format( "Succesfully deserialized button from: {0}", FileName ) );

				return def;
			}
			catch ( Exception err )
			{
				Pandora.Log.WriteError( err, string.Format( "Failed to read custom button from: {0}", FileName ) );
				return null;
			}
		}

		/// <summary>
		/// Gets a MultiCommandDef object if there is one assigned. Returns null is none is found
		/// </summary>
		public MultiCommandDef MultiDef
		{
			get
			{
				if ( m_Left != null && m_Left is MultiCommandDef )
					return m_Left as MultiCommandDef;

				if ( m_Right != null && m_Right is MultiCommandDef )
					return m_Right as MultiCommandDef;

				return null;
			}
		}

		/// <summary>
		/// Gets a ModifierCommand def if there is one assigned on this button
		/// </summary>
		public ModifierCommand ModifierDef
		{
			get
			{
				if ( m_Left != null && m_Left is ModifierCommand )
					return m_Left as ModifierCommand;

				if ( m_Right != null && m_Right is ModifierCommand )
					return m_Right as ModifierCommand;

				return null;
			}
		}

		/// <summary>
		/// Gets a description performed by possible actions
		/// </summary>
		/// <param name="function">The IButtonFunction action to evaluate</param>
		/// <returns>A string describing the action, null if not found</returns>
		private string GetButtonAction( IButtonFunction function )
		{
			if ( function is MenuCommand )
			{
				MenuCommand mc = function as MenuCommand;
				
				return mc.FullCommand;
			}
			else if ( function is LastCommand )
			{
				if ( m_LastCommand != null )
					return m_LastCommand.FullCommand;
				else
					return null;
			}
			else if ( function is MenuDef )
			{
				return Pandora.Localization.TextProvider[ "Buttons.MenuDesc" ];
			}
			
			return null;
		}

		/// <summary>
		/// Gets a string representing the tool tip for the button
		/// </summary>
		public string ToolTipText
		{
			get
			{
				if ( ModifierDef != null )
				{
					string left = string.Format( Pandora.Localization.TextProvider[ "Buttons.TipLeft" ], ModifierDef.Command );
					string right = string.Format( Pandora.Localization.TextProvider[ "Buttons.TipRight" ], Pandora.Localization.TextProvider[ "Buttons.ModRight" ] );

					return string.Format( Pandora.Localization.TextProvider[ "Buttons.TipTwoLine" ], left, right );
				}
				else if ( MultiDef == null )
				{
					string left = null;
					string right = null;

					if ( m_Left != null )
					{
						left = GetButtonAction( m_Left );
					}

					if ( m_Right != null )
					{
						right = GetButtonAction( m_Right );
					}

					if ( left != null && right != null )
					{
						string l = string.Format( Pandora.Localization.TextProvider[ "Buttons.TipLeft" ], left );
						string r = string.Format( Pandora.Localization.TextProvider[ "Buttons.TipRight" ], right );

						return string.Format( Pandora.Localization.TextProvider[ "Buttons.TipTwoLine" ], l, r );
					}
					else if ( left != null )
					{
						return string.Format( Pandora.Localization.TextProvider[ "Buttons.TipLeft" ], left );
					}
					else if ( right != null )
					{
						return string.Format( Pandora.Localization.TextProvider[ "Buttons.TipRight" ], right );
					}

					return null;
				}
				else
				{
					// This is a multi def

					string left = string.Format( Pandora.Localization.TextProvider[ "Buttons.TipLeft" ], MultiDef.DefaultCommand.FullCommand );
					string right = string.Format( Pandora.Localization.TextProvider[ "Buttons.TipRight" ], Pandora.Localization.TextProvider[ "Buttons.MultiRightClick" ] );

					return string.Format( Pandora.Localization.TextProvider[ "Buttons.TipTwoLine" ], left, right );
				}
			}
		}

		private void UpdateToolTip()
		{
			OnToolTipChanged( new ToolTipEventArgs( ToolTipText ) );
		}

		[ XmlIgnore ]
		/// <summary>
		/// Gets the command callback for modifiers buttons
		/// </summary>
		public CommandCallback CommandCallback
		{
			get
			{
				if ( m_Left != null && m_Left is ModifierCommand )
				{
					return ( m_Left as ModifierCommand ).CommandCallback;
				}
				else if ( m_Right != null && m_Right is ModifierCommand )
				{
					return ( m_Right as ModifierCommand ).CommandCallback;
				}
				else
				{
					return null;
				}
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			if ( m_Left is IDisposable )
			{
				( m_Left as IDisposable ).Dispose();
			}

			if ( m_Right is IDisposable )
			{
				( m_Right as IDisposable ).Dispose();
			}
		}

		#endregion

		#region ICloneable Members

		public object Clone()
		{
			ButtonDef def = new ButtonDef();

			if ( m_Caption != null )
				def.m_Caption = string.Copy( m_Caption );

			if ( m_LastCommand != null )
				def.m_LastCommand = new SendCommandEventArgs( m_LastCommand.Command, m_LastCommand.UsePrefix );

			if ( m_Left != null )
				def.Left = ( m_Left as ICloneable ).Clone() as IButtonFunction;

			if ( m_Right != null )
				def.Right = ( m_Right as ICloneable ).Clone() as IButtonFunction;
			
			return def;
		}

		#endregion
	}
}