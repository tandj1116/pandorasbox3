using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TheBox.Buttons
{
	/// <summary>
	/// Summary description for SimpleCommand.
	/// </summary>
	public class SimpleCommand : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox txCommand;
		private System.Windows.Forms.CheckBox chkPrefix;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SimpleCommand()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			Pandora.LocalizeControl( this );
		}

		/// <summary>
		/// Creates a new SimpleCommand form
		/// </summary>
		/// <param name="disablePrefixCheck">Specifies whether the option to set the command prefix should be hidden</param>
		public SimpleCommand( bool disablePrefixCheck ) : this()
		{
			chkPrefix.Visible = !disablePrefixCheck;
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

		/// <summary>
		/// Gets the command specified
		/// </summary>
		public string Command
		{
			get { return txCommand.Text; }
			set { txCommand.Text = value; }
		}

		/// <summary>
		/// Gets the prefix value
		/// </summary>
		public bool UsePrefix
		{
			get { return chkPrefix.Checked; }
			set { chkPrefix.Checked = value; }
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SimpleCommand));
			this.txCommand = new System.Windows.Forms.TextBox();
			this.chkPrefix = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txCommand
			// 
			this.txCommand.Location = new System.Drawing.Point(8, 8);
			this.txCommand.Name = "txCommand";
			this.txCommand.Size = new System.Drawing.Size(200, 20);
			this.txCommand.TabIndex = 0;
			this.txCommand.Text = "";
			// 
			// chkPrefix
			// 
			this.chkPrefix.Checked = true;
			this.chkPrefix.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPrefix.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkPrefix.Location = new System.Drawing.Point(8, 32);
			this.chkPrefix.Name = "chkPrefix";
			this.chkPrefix.Size = new System.Drawing.Size(200, 24);
			this.chkPrefix.TabIndex = 1;
			this.chkPrefix.Text = "ButtonMenuEditor.UsePrefix";
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(136, 56);
			this.button1.Name = "button1";
			this.button1.TabIndex = 2;
			this.button1.Text = "Common.Ok";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button2.Location = new System.Drawing.Point(8, 56);
			this.button2.Name = "button2";
			this.button2.TabIndex = 3;
			this.button2.Text = "Common.Cancel";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// SimpleCommand
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(216, 88);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.chkPrefix);
			this.Controls.Add(this.txCommand);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SimpleCommand";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Buttons.EnterCommand";
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			if ( txCommand.Text.Length == 0 )
			{
				MessageBox.Show( Pandora.TextProvider[ "Buttons.ErrCommand" ] );
				return;
			}

			DialogResult = DialogResult.OK;
			Close();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
