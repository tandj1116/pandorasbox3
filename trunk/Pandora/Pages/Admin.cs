using System;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TheBox.Pages
{
	/// <summary>
	/// Summary description for Admin.
	/// </summary>
	public class Admin : System.Windows.Forms.UserControl
	{
		[ DllImport("user32.dll") ]
		private static extern int GetWindowText(int hWnd, StringBuilder text, int count); 

		private TheBox.Buttons.BoxButton boxButton1;
		private TheBox.Buttons.BoxButton boxButton2;
		private TheBox.Buttons.BoxButton boxButton3;
		private TheBox.Buttons.BoxButton boxButton4;
		private TheBox.Buttons.BoxButton boxButton5;
		private TheBox.Buttons.BoxButton boxButton6;
		private TheBox.Buttons.BoxButton boxButton7;
		private TheBox.Buttons.BoxButton boxButton8;
		private TheBox.Buttons.BoxButton boxButton9;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private TheBox.Buttons.BoxButton boxButton10;
		private TheBox.Buttons.BoxButton boxButton11;
		private System.Windows.Forms.ComboBox cmbFind;
		private System.Windows.Forms.Button bFind;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label labServer;
		private System.Windows.Forms.Button bFindServer;
		private System.Windows.Forms.OpenFileDialog OpenFile;
		private System.Windows.Forms.Button bHideConsole;
		private System.Windows.Forms.Button bShowConsole;
		private System.Windows.Forms.Button bStop;
		private System.Windows.Forms.Button bFindServerProc;
		private System.Windows.Forms.Button bRun;
		private System.Windows.Forms.Button bGetServ;
		private System.Windows.Forms.Button bStartServer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbArgs;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		[ DllImport( "User32" ) ]
		private static extern bool ShowWindow( int hWnd, int nCmdShow );

		[DllImportAttribute("user32.dll")]
		private static extern IntPtr FindWindow(string ClassName, string WindowName);

		private const int SW_HIDE = 0;
		private const int SW_SHOW = 5;

		private Process m_Process;

		public Admin()
		{
			InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.boxButton1 = new TheBox.Buttons.BoxButton();
			this.boxButton2 = new TheBox.Buttons.BoxButton();
			this.boxButton3 = new TheBox.Buttons.BoxButton();
			this.boxButton4 = new TheBox.Buttons.BoxButton();
			this.boxButton5 = new TheBox.Buttons.BoxButton();
			this.boxButton6 = new TheBox.Buttons.BoxButton();
			this.boxButton7 = new TheBox.Buttons.BoxButton();
			this.boxButton8 = new TheBox.Buttons.BoxButton();
			this.boxButton9 = new TheBox.Buttons.BoxButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.boxButton10 = new TheBox.Buttons.BoxButton();
			this.cmbFind = new System.Windows.Forms.ComboBox();
			this.bFind = new System.Windows.Forms.Button();
			this.boxButton11 = new TheBox.Buttons.BoxButton();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.bRun = new System.Windows.Forms.Button();
			this.bFindServerProc = new System.Windows.Forms.Button();
			this.bStop = new System.Windows.Forms.Button();
			this.bShowConsole = new System.Windows.Forms.Button();
			this.bHideConsole = new System.Windows.Forms.Button();
			this.bFindServer = new System.Windows.Forms.Button();
			this.labServer = new System.Windows.Forms.Label();
			this.OpenFile = new System.Windows.Forms.OpenFileDialog();
			this.bGetServ = new System.Windows.Forms.Button();
			this.bStartServer = new System.Windows.Forms.Button();
			this.cmbArgs = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// boxButton1
			// 
			this.boxButton1.AllowEdit = true;
			this.boxButton1.ButtonID = 55;
			this.boxButton1.Def = null;
			this.boxButton1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton1.IsActive = true;
			this.boxButton1.Location = new System.Drawing.Point(8, 16);
			this.boxButton1.Name = "boxButton1";
			this.boxButton1.TabIndex = 0;
			this.boxButton1.Text = "Profile";
			// 
			// boxButton2
			// 
			this.boxButton2.AllowEdit = true;
			this.boxButton2.ButtonID = 56;
			this.boxButton2.Def = null;
			this.boxButton2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton2.IsActive = true;
			this.boxButton2.Location = new System.Drawing.Point(8, 44);
			this.boxButton2.Name = "boxButton2";
			this.boxButton2.TabIndex = 1;
			this.boxButton2.Text = "Generate";
			// 
			// boxButton3
			// 
			this.boxButton3.AllowEdit = true;
			this.boxButton3.ButtonID = 57;
			this.boxButton3.Def = null;
			this.boxButton3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton3.IsActive = true;
			this.boxButton3.Location = new System.Drawing.Point(8, 40);
			this.boxButton3.Name = "boxButton3";
			this.boxButton3.TabIndex = 2;
			this.boxButton3.Text = "Save";
			// 
			// boxButton4
			// 
			this.boxButton4.AllowEdit = true;
			this.boxButton4.ButtonID = 58;
			this.boxButton4.Def = null;
			this.boxButton4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton4.IsActive = true;
			this.boxButton4.Location = new System.Drawing.Point(8, 64);
			this.boxButton4.Name = "boxButton4";
			this.boxButton4.TabIndex = 3;
			this.boxButton4.Text = "Ban";
			// 
			// boxButton5
			// 
			this.boxButton5.AllowEdit = true;
			this.boxButton5.ButtonID = 59;
			this.boxButton5.Def = null;
			this.boxButton5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton5.IsActive = true;
			this.boxButton5.Location = new System.Drawing.Point(8, 16);
			this.boxButton5.Name = "boxButton5";
			this.boxButton5.TabIndex = 4;
			this.boxButton5.Text = "Admin";
			// 
			// boxButton6
			// 
			this.boxButton6.AllowEdit = true;
			this.boxButton6.ButtonID = 64;
			this.boxButton6.Def = null;
			this.boxButton6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton6.IsActive = true;
			this.boxButton6.Location = new System.Drawing.Point(8, 112);
			this.boxButton6.Name = "boxButton6";
			this.boxButton6.TabIndex = 4;
			this.boxButton6.Text = "Guards";
			// 
			// boxButton7
			// 
			this.boxButton7.AllowEdit = true;
			this.boxButton7.ButtonID = 65;
			this.boxButton7.Def = null;
			this.boxButton7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton7.IsActive = true;
			this.boxButton7.Location = new System.Drawing.Point(8, 44);
			this.boxButton7.Name = "boxButton7";
			this.boxButton7.TabIndex = 3;
			this.boxButton7.Text = "Unfreeze";
			// 
			// boxButton8
			// 
			this.boxButton8.AllowEdit = true;
			this.boxButton8.ButtonID = 66;
			this.boxButton8.Def = null;
			this.boxButton8.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton8.IsActive = true;
			this.boxButton8.Location = new System.Drawing.Point(8, 16);
			this.boxButton8.Name = "boxButton8";
			this.boxButton8.TabIndex = 2;
			this.boxButton8.Text = "Freeze";
			// 
			// boxButton9
			// 
			this.boxButton9.AllowEdit = true;
			this.boxButton9.ButtonID = 67;
			this.boxButton9.Def = null;
			this.boxButton9.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton9.IsActive = true;
			this.boxButton9.Location = new System.Drawing.Point(8, 88);
			this.boxButton9.Name = "boxButton9";
			this.boxButton9.TabIndex = 1;
			this.boxButton9.Text = "Firewall";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.boxButton5);
			this.groupBox1.Controls.Add(this.boxButton3);
			this.groupBox1.Controls.Add(this.boxButton4);
			this.groupBox1.Controls.Add(this.boxButton9);
			this.groupBox1.Controls.Add(this.boxButton6);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(4, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(92, 140);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Admin.Control";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.boxButton1);
			this.groupBox2.Controls.Add(this.boxButton2);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(196, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(92, 72);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Admin.World";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.boxButton8);
			this.groupBox3.Controls.Add(this.boxButton7);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(100, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(92, 72);
			this.groupBox3.TabIndex = 5;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Admin.Statics";
			// 
			// boxButton10
			// 
			this.boxButton10.AllowEdit = true;
			this.boxButton10.ButtonID = 68;
			this.boxButton10.Def = null;
			this.boxButton10.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton10.IsActive = true;
			this.boxButton10.Location = new System.Drawing.Point(108, 80);
			this.boxButton10.Name = "boxButton10";
			this.boxButton10.TabIndex = 6;
			this.boxButton10.Text = "Misc";
			// 
			// cmbFind
			// 
			this.cmbFind.Location = new System.Drawing.Point(108, 112);
			this.cmbFind.Name = "cmbFind";
			this.cmbFind.Size = new System.Drawing.Size(116, 21);
			this.cmbFind.TabIndex = 7;
			// 
			// bFind
			// 
			this.bFind.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bFind.Location = new System.Drawing.Point(228, 108);
			this.bFind.Name = "bFind";
			this.bFind.Size = new System.Drawing.Size(52, 28);
			this.bFind.TabIndex = 8;
			this.bFind.Text = "Common.Find";
			this.bFind.Click += new System.EventHandler(this.bFind_Click);
			// 
			// boxButton11
			// 
			this.boxButton11.AllowEdit = true;
			this.boxButton11.ButtonID = 69;
			this.boxButton11.Def = null;
			this.boxButton11.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton11.IsActive = true;
			this.boxButton11.Location = new System.Drawing.Point(204, 80);
			this.boxButton11.Name = "boxButton11";
			this.boxButton11.TabIndex = 9;
			this.boxButton11.Text = "Custom";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Controls.Add(this.cmbArgs);
			this.groupBox4.Controls.Add(this.bStartServer);
			this.groupBox4.Controls.Add(this.bGetServ);
			this.groupBox4.Controls.Add(this.bStop);
			this.groupBox4.Controls.Add(this.bShowConsole);
			this.groupBox4.Controls.Add(this.bHideConsole);
			this.groupBox4.Controls.Add(this.bFindServer);
			this.groupBox4.Controls.Add(this.labServer);
			this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox4.Location = new System.Drawing.Point(292, 0);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(200, 140);
			this.groupBox4.TabIndex = 10;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Admin.Process";
			// 
			// bRun
			// 
			this.bRun.Location = new System.Drawing.Point(0, 0);
			this.bRun.Name = "bRun";
			this.bRun.TabIndex = 0;
			// 
			// bFindServerProc
			// 
			this.bFindServerProc.Location = new System.Drawing.Point(0, 0);
			this.bFindServerProc.Name = "bFindServerProc";
			this.bFindServerProc.TabIndex = 0;
			// 
			// bStop
			// 
			this.bStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bStop.Location = new System.Drawing.Point(4, 84);
			this.bStop.Name = "bStop";
			this.bStop.Size = new System.Drawing.Size(92, 23);
			this.bStop.TabIndex = 4;
			this.bStop.Text = "Common.Stop";
			this.bStop.Click += new System.EventHandler(this.bStop_Click);
			// 
			// bShowConsole
			// 
			this.bShowConsole.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bShowConsole.Location = new System.Drawing.Point(104, 112);
			this.bShowConsole.Name = "bShowConsole";
			this.bShowConsole.Size = new System.Drawing.Size(92, 23);
			this.bShowConsole.TabIndex = 3;
			this.bShowConsole.Text = "Admin.Show";
			this.bShowConsole.Click += new System.EventHandler(this.bShowConsole_Click);
			// 
			// bHideConsole
			// 
			this.bHideConsole.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bHideConsole.Location = new System.Drawing.Point(4, 112);
			this.bHideConsole.Name = "bHideConsole";
			this.bHideConsole.Size = new System.Drawing.Size(92, 23);
			this.bHideConsole.TabIndex = 2;
			this.bHideConsole.Text = "Admin.Hide";
			this.bHideConsole.Click += new System.EventHandler(this.bHideConsole_Click);
			// 
			// bFindServer
			// 
			this.bFindServer.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bFindServer.Location = new System.Drawing.Point(148, 16);
			this.bFindServer.Name = "bFindServer";
			this.bFindServer.Size = new System.Drawing.Size(48, 23);
			this.bFindServer.TabIndex = 1;
			this.bFindServer.Text = "Common.File";
			this.bFindServer.Click += new System.EventHandler(this.bFindServer_Click);
			// 
			// labServer
			// 
			this.labServer.Location = new System.Drawing.Point(4, 16);
			this.labServer.Name = "labServer";
			this.labServer.Size = new System.Drawing.Size(136, 23);
			this.labServer.TabIndex = 0;
			this.labServer.Text = "Admin.ProcessNotFound";
			this.labServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labServer.Paint += new System.Windows.Forms.PaintEventHandler(this.labServer_Paint);
			// 
			// bGetServ
			// 
			this.bGetServ.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bGetServ.Location = new System.Drawing.Point(104, 84);
			this.bGetServ.Name = "bGetServ";
			this.bGetServ.Size = new System.Drawing.Size(92, 23);
			this.bGetServ.TabIndex = 5;
			this.bGetServ.Text = "Common.Find";
			this.bGetServ.Click += new System.EventHandler(this.bGetServ_Click);
			// 
			// bStartServer
			// 
			this.bStartServer.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bStartServer.Location = new System.Drawing.Point(148, 44);
			this.bStartServer.Name = "bStartServer";
			this.bStartServer.Size = new System.Drawing.Size(48, 32);
			this.bStartServer.TabIndex = 6;
			this.bStartServer.Text = "Common.Run";
			this.bStartServer.Click += new System.EventHandler(this.bStartServer_Click);
			// 
			// cmbArgs
			// 
			this.cmbArgs.Location = new System.Drawing.Point(4, 56);
			this.cmbArgs.Name = "cmbArgs";
			this.cmbArgs.Size = new System.Drawing.Size(136, 21);
			this.cmbArgs.TabIndex = 7;
			this.cmbArgs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbArgs_KeyDown);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(4, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 16);
			this.label1.TabIndex = 8;
			this.label1.Text = "Admin.CmdArgs";
			// 
			// Admin
			// 
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.boxButton11);
			this.Controls.Add(this.bFind);
			this.Controls.Add(this.cmbFind);
			this.Controls.Add(this.boxButton10);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "Admin";
			this.Size = new System.Drawing.Size(496, 142);
			this.Load += new System.EventHandler(this.Admin_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// OnLoad, apply options
		/// </summary>
		private void Admin_Load(object sender, System.EventArgs e)
		{
			try
			{
				UpdateFindCombo();
				UpdateRecentArgs();

				if ( Pandora.Profile.Admin.ServerExe != null && System.IO.File.Exists( Pandora.Profile.Admin.ServerExe ) )
				{
					// We have a server file
					FindServerProcess();
					EnableServerButtons();
				}
			}
			catch {} // VS
		}

		/// <summary>
		/// Updates the combo box by loading the recently used search strings
		/// </summary>
		private void UpdateFindCombo()
		{
			cmbFind.Items.Clear();

			cmbFind.Items.AddRange( Pandora.Profile.Admin.FindByName.GetArray() );
		}
		
		/// <summary>
		/// Performs the FindByName command
		/// </summary>
		private void PerformFindByName()
		{
			string item = cmbFind.Text;

			if ( item != null && item.Length > 0 )
			{
				Pandora.Profile.Commands.DoFindByName( item );
				Pandora.Profile.Admin.FindByName.AddString( item );

				UpdateFindCombo();
			}
		}

		/// <summary>
		/// Find by name button
		/// </summary>
		private void bFind_Click(object sender, System.EventArgs e)
		{
			PerformFindByName();
		}

		#region Server Process

		/// <summary>
		/// Find server
		/// </summary>
		private void bFindServer_Click(object sender, System.EventArgs e)
		{
			if ( OpenFile.ShowDialog() == DialogResult.OK )
			{
				Pandora.Profile.Admin.ServerExe = OpenFile.FileName;

				FindServerProcess();
				EnableServerButtons();
			}
		}

		/// <summary>
		/// Finds the server process
		/// </summary>
		/// <returns>A Process object</returns>
		private void FindServerProcess()
		{
			string file = Pandora.Profile.Admin.ServerExe;

			if ( file != null && !System.IO.File.Exists( file ) )
				return;

			Process[] processes = Process.GetProcesses();

			foreach( Process p in processes )
			{
				try
				{
					if ( p.MainModule.FileName.ToLower() == file.ToLower() )
					{
						m_Process = p;
						m_Process.EnableRaisingEvents = true;
						m_Process.Exited += new EventHandler(m_Process_Exited);
					}
				}
				catch {}
			}
		}

		/// <summary>
		/// Gets the console window
		/// </summary>
		/// <param name="p">The server process</param>
		/// <returns>A handle for the console window</returns>
		private int GetConsole( Process p )
		{
			if ( p == null )
			{
				Pandora.Profile.Admin.Console = 0;
				Pandora.Profile.Admin.ConsoleTitle = null;

				return 0;
			}

			if ( p.MainWindowHandle.ToInt32() != 0 )
			{
				Pandora.Profile.Admin.Console = p.MainWindowHandle.ToInt32();
				Pandora.Profile.Admin.ConsoleTitle = p.MainWindowTitle;

				return p.MainWindowHandle.ToInt32();
			}
			else
			{
				int handle = Pandora.Profile.Admin.Console;

				if ( handle == 0 )
					return 0;

				NativeWindow win = NativeWindow.FromHandle( new IntPtr( handle ) );

				const int nChars = 256;
				StringBuilder Buff = new StringBuilder(nChars);

				if ( GetWindowText(handle, Buff, nChars) > 0 )
				{
					// Window has a title
					if ( Buff.ToString() == Pandora.Profile.Admin.ConsoleTitle )
					{
						return handle;
					}
				}

				// The stored  window is no more
				Pandora.Profile.Admin.ConsoleTitle = null;
				Pandora.Profile.Admin.Console = 0;

				return 0;
			}
		}

		/// <summary>
		/// Show console button
		/// </summary>
		private void bShowConsole_Click(object sender, System.EventArgs e)
		{
			IntPtr ptr = new IntPtr( GetConsole( m_Process ) );
			
			if ( ptr != IntPtr.Zero )
			{
				ShowWindow( ptr.ToInt32(), SW_SHOW );
				Pandora.Profile.Admin.ConsoleHidden = false;
			}

			EnableServerButtons();
		}

		/// <summary>
		/// Hide console button
		/// </summary>
		private void bHideConsole_Click(object sender, System.EventArgs e)
		{
			IntPtr ptr = new IntPtr( GetConsole( m_Process ) );

			if ( ptr != IntPtr.Zero )
			{
				ShowWindow( ptr.ToInt32(), SW_HIDE );
				Pandora.Profile.Admin.ConsoleHidden = true;
			}

			EnableServerButtons();
		}

		/// <summary>
		/// Enables the server buttons depending on the server state
		/// </summary>
		private void EnableServerButtons()
		{
			if ( m_Process != null )
			{
				labServer.Text = Pandora.TextProvider[ "Admin.Running" ];
			}
			else
			{
				labServer.Text = Pandora.TextProvider[ "Admin.Stopped" ];
			}

			bStartServer.Enabled = Pandora.Profile.Admin.ServerExe != null
				&& System.IO.File.Exists( Pandora.Profile.Admin.ServerExe )
				&& m_Process == null;
			
			bStop.Enabled = m_Process != null;

			bGetServ.Enabled = m_Process == null
				&& Pandora.Profile.Admin.ServerExe != null
				&& System.IO.File.Exists( Pandora.Profile.Admin.ServerExe );

			bHideConsole.Enabled = m_Process != null
				&& GetConsole( m_Process ) != 0
				&& !Pandora.Profile.Admin.ConsoleHidden;

			bShowConsole.Enabled = m_Process != null
				&& GetConsole( m_Process ) != 0
				&& Pandora.Profile.Admin.ConsoleHidden;
		}

		/// <summary>
		/// The server process has been closed
		/// </summary>
		private void m_Process_Exited(object sender, EventArgs e)
		{
			m_Process = null;
			EnableServerButtons();
		}

		private void labServer_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			TheBox.Common.Utility.DrawBorder( labServer, e.Graphics );
		}

		/// <summary>
		/// Find the server process
		/// </summary>
		private void bGetServ_Click(object sender, System.EventArgs e)
		{
			FindServerProcess();
			EnableServerButtons();
		}

		/// <summary>
		/// Stop the server
		/// </summary>
		private void bStop_Click(object sender, System.EventArgs e)
		{
			if ( m_Process != null )
			{
				m_Process.Kill();
			}

			EnableServerButtons();
		}

		/// <summary>
		/// Run the server
		/// </summary>
		private void bStartServer_Click(object sender, System.EventArgs e)
		{
			string args = null;

			if ( cmbArgs.Text.Length > 0 )
			{
				args = cmbArgs.Text;
				UpdateRecentArgs();
			}

			if ( args == null )
			{
				m_Process = Process.Start( Pandora.Profile.Admin.ServerExe );
			}
			else
			{
				m_Process = Process.Start( Pandora.Profile.Admin.ServerExe, args );
			}

			m_Process.EnableRaisingEvents = true;
			m_Process.Exited += new EventHandler(m_Process_Exited);

			Pandora.Profile.Admin.ConsoleHidden = false;

			EnableServerButtons();
			bHideConsole.Enabled = true;
		}

		/// <summary>
		/// Updates the recent arguments combo box
		/// </summary>
		private void UpdateRecentArgs()
		{
			string arg = cmbArgs.Text;

			if ( arg.Length > 0 )
			{
				Pandora.Profile.Admin.Args.AddString( arg );
			}

			cmbArgs.BeginUpdate();
			cmbArgs.Items.Clear();
			
			foreach( string s in Pandora.Profile.Admin.Args.List )
			{
				cmbArgs.Items.Add( s );
			}

			cmbArgs.EndUpdate();

			if ( arg.Length > 0 )
				cmbArgs.SelectedItem = arg;
		}

		/// <summary>
		/// Handle enter key in the combo box
		/// </summary>
		private void cmbArgs_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.Enter )
			{
				bStartServer.PerformClick();
			}
		}

		#endregion
	}
}