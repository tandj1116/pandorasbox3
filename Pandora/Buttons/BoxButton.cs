using System;
using System.Windows.Forms;

namespace TheBox.Buttons
{
	/// <summary>
	/// Summary description for BoxButton.
	/// </summary>
	public class BoxButton : Button
	{
		#region Configuration Menu

		private ContextMenu m_Menu;
		private MenuItem mEdit;
		private MenuItem mClear;
		private MenuItem mImport;
		private MenuItem mExport;
		private System.Windows.Forms.OpenFileDialog OpenFile;
		private System.Windows.Forms.SaveFileDialog SaveFile;
		private MenuItem mRestore;

		private void BuildMenu()
		{
            if (Pandora.TextProvider != null)
            {
                mEdit = new MenuItem(Pandora.TextProvider["Common.Edit"], new EventHandler(EditButton));
                mEdit = new MenuItem(Pandora.TextProvider["Common.Edit"], new EventHandler(EditButton));
                mClear = new MenuItem(Pandora.TextProvider["Common.Clear"], new EventHandler(ClearButton));
                mImport = new MenuItem(Pandora.TextProvider["Common.Import"], new EventHandler(ImportButton));
                mExport = new MenuItem(Pandora.TextProvider["Common.Export"], new EventHandler(ExportButton));
                mRestore = new MenuItem(Pandora.TextProvider["Common.RestoreDefault"], new EventHandler(RestoreDefault));

                m_Menu = new ContextMenu(new MenuItem[]
				{
					mEdit,
					mClear,
					new MenuItem( "-" ),
					mImport,
					mExport,
					new MenuItem( "-" ),
					mRestore
				});

                m_Menu.Popup += new EventHandler(MenuPopup);
            }
		}

		private void MenuPopup( object sender, EventArgs e )
		{
			mClear.Enabled = (m_Def != null);
		}

		/// <summary>
		/// Bring up the customization form
		/// </summary>
		private void EditButton( object sender, EventArgs e )
		{
			ButtonEditor editor = new ButtonEditor();

			if ( m_Def != null )
				editor.Def = m_Def;

			if ( editor.ShowDialog() == DialogResult.OK )
			{
				Pandora.Buttons[ this ] = editor.Def;
				Text = m_Def.Caption;

				if ( HasToolTip )
				{
					Pandora.ToolTip.SetToolTip( this, ToolTipText );
				}
			}

			editor.Dispose();
		}

		/// <summary>
		/// Clear up the button
		/// </summary>
		private void ClearButton( object sender, EventArgs e )
		{
			if ( MessageBox.Show( this, Pandora.TextProvider[ "Buttons.ConfirmClear" ], "", MessageBoxButtons.YesNo ) == DialogResult.Yes )
			{
				Pandora.Buttons.ClearButton( this );
				Pandora.ToolTip.SetToolTip( this, null );
			}
		}

		/// <summary>
		/// Import the button from an Xml file
		/// </summary>
		private void ImportButton( object sender, EventArgs e )
		{
			if ( MessageBox.Show( this, Pandora.TextProvider[ "Buttons.ImportConfirm" ], "", MessageBoxButtons.YesNo ) == DialogResult.Yes )
			{
				if ( OpenFile.ShowDialog() == DialogResult.OK )
				{
					ButtonDef def = ButtonDef.Load( OpenFile.FileName );

					if ( def != null )
					{
						Pandora.Buttons[ this ] = def;
					}
					else
					{
						MessageBox.Show( Pandora.TextProvider[ "Buttons.LoadFail" ] );
					}
				}
			}
		}

		/// <summary>
		/// Export the button to an Xml file
		/// </summary>
		private void ExportButton( object sender, EventArgs e )
		{
			if ( SaveFile.ShowDialog() == DialogResult.OK )
			{
				if ( ! m_Def.Save( SaveFile.FileName ) )
				{
					MessageBox.Show( Pandora.TextProvider[ "Buttons.SaveFail" ] );
				}
			}
		}

		/// <summary>
		/// Restores the default values
		/// </summary>
		private void RestoreDefault( object sender, EventArgs e )
		{
			Pandora.Buttons.ResetDefault( this );
		}

		#endregion

		private void InitializeComponent()
		{
			this.OpenFile = new System.Windows.Forms.OpenFileDialog();
			this.SaveFile = new System.Windows.Forms.SaveFileDialog();
			// 
			// OpenFile
			// 
			this.OpenFile.Filter = "Xml files (*.xml)|*.xml";
			// 
			// SaveFile
			// 
			this.SaveFile.Filter = "Xml files (*.xml)|*.xml";
		}

		private int m_ButtonID = -1;
		private bool m_IDSet = false;
		private bool m_IsActive = true;
		private bool m_AllowEdit = true;

		/// <summary>
		/// Gets or sets a value stating whether the button can be edited
		/// </summary>
		public bool AllowEdit
		{
			get { return m_AllowEdit; }
			set { m_AllowEdit = value; }
		}

		/// <summary>
		/// Gets or sets a value stating whether the button will actually send commands
		/// </summary>
		public bool IsActive
		{
			get { return m_IsActive; }
			set { m_IsActive = value; }
		}

		/// <summary>
		/// Gets the unique ID for this customizable button
		/// </summary>
		public int ButtonID
		{
			get
			{
				if ( !Created && !m_IDSet )
					return -1;

				if ( !m_IDSet && m_ButtonID == -1 )
				{
					m_ButtonID = ButtonIDs.NextID;
				}

				return m_ButtonID;
			}
			set
			{
				m_ButtonID = value;
				m_IDSet = true;
			}
		}

		private ButtonDef m_Def;

		/// <summary>
		/// Gets or sets the definition object for this button
		/// </summary>
		public ButtonDef Def
		{
			get { return m_Def; }
			set
			{
				m_Def = value;
				if ( m_Def != null )
				{
					m_Def.CaptionChanged +=new EventHandler(m_Def_CaptionChanged);
					m_Def.SendCommand += new SendCommandEventHandler(m_Def_SendCommand);
					m_Def.ToolTipChanged += new ToolTipChangedEventHandler(m_Def_ToolTipChanged);
					Text = m_Def.Caption;

					Pandora.ToolTip.SetToolTip( this, m_Def.ToolTipText );

					this.Tag = m_Def.CommandCallback;
				}
				else
				{
					Text = "";
					Pandora.ToolTip.SetToolTip( this, null );
				}
			}
		}

		/// <summary>
		/// Creates a new BoxButton
		/// </summary>
		public BoxButton()
		{
			InitializeComponent();

			FlatStyle = FlatStyle.System;

			try
			{
				BuildMenu();
			}
			catch (Exception ex)
			{
                // Issue 6:  	 Improve error management - Tarion
                //Pandora.Log.WriteError(ex, "BuildMenu() failed");
                throw new Exception("BuildMenu() failed", ex);
                // End Issue 6
			}
		}

		#region Control key managment

		[System.Runtime.InteropServices.DllImport( "User32" )] private static extern short GetKeyState( int nVirtKey );
		private static int VK_CONTROL = 0x11;

		/// <summary>
		/// Gets a value stating whether the Control key is pressed or not
		/// </summary>
		private bool CtrlPressed
		{
			get
			{
				short control = GetKeyState( VK_CONTROL );

				if ( control < -100 )
					return true;
				else
					return false;
			}
		}

		#endregion

		/// <summary>
		/// Caption changed
		/// </summary>
		private void m_Def_CaptionChanged(object sender, EventArgs e)
		{
			Text = m_Def.Caption;

			if ( m_Def.MultiDef != null )
				Pandora.Profile.ButtonIndex[ m_ButtonID ] = m_Def.MultiDef.DefaultIndex;
		}

		/// <summary>
		/// Send command
		/// </summary>
		private void m_Def_SendCommand(object sender, SendCommandEventArgs e)
		{
			OnSendCommand( e );

			if ( m_IsActive && !e.Sent )
				Pandora.SendToUO( e.Command, e.UsePrefix );
		}

		/// <summary>
		/// Occurs when the button is sending a command to UO
		/// </summary>
		public event SendCommandEventHandler SendCommand;

		protected virtual void OnSendCommand( SendCommandEventArgs e )
		{
			if ( SendCommand != null )
			{
				SendCommand ( this, e );
			}
		}

		/// <summary>
		/// Mouse Down
		/// </summary>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown (e);

			if ( m_AllowEdit && ( m_Def == null || ( m_Def.Left == null && m_Def.Right == null ) ) )
			{
				EditButton( this, new EventArgs() );
				return;
			}

			if ( CtrlPressed && m_AllowEdit )
			{
				// Configure: show context menu
				m_Menu.Show( this, new System.Drawing.Point ( e.X, e.Y ) );
			}
			else
			{
				// Not pressed, do normal button
				if ( m_Def != null )
				{
					m_Def.DoAction( this, new System.Drawing.Point( e.X, e.Y ), e.Button );
				}
			}
		}

		/// <summary>
		/// Gets the tool tip text for this button
		/// </summary>
		public string ToolTipText
		{
			get
			{
				if ( m_Def != null )
					return m_Def.ToolTipText;
				else
					return null;
			}
		}

		/// <summary>
		/// Gets a value stating whether this button has a tool tip
		/// </summary>
		public bool HasToolTip
		{
			get
			{
				return ( m_Def != null && ( m_Def.Left != null || m_Def.Right != null ) );
			}
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose (disposing);

			if ( disposing )
			{
				if ( m_Def != null )
					m_Def.Dispose();
			}
		}

		private void m_Def_ToolTipChanged(object sender, ToolTipEventArgs e)
		{
			Pandora.ToolTip.SetToolTip( this, e.Text );
		}
	}
}