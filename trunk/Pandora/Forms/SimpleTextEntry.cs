using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TheBox.Forms
{
	/// <summary>
	/// Summary description for SimpleTextEntry.
	/// </summary>
	public class SimpleTextEntry : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox tx;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SimpleTextEntry()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
		/// Gets or sets the text edited by this form
		/// </summary>
		public string EntryText
		{
			get { return tx.Text; }
			set { tx.Text = value; }
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tx = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// tx
			// 
			this.tx.Location = new System.Drawing.Point(4, 4);
			this.tx.Name = "tx";
			this.tx.Size = new System.Drawing.Size(160, 20);
			this.tx.TabIndex = 0;
			this.tx.Text = "";
			this.tx.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tx_KeyDown);
			// 
			// SimpleTextEntry
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(168, 28);
			this.ControlBox = false;
			this.Controls.Add(this.tx);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "SimpleTextEntry";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "SimpleTextEntry";
			this.Load += new System.EventHandler(this.SimpleTextEntry_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.SimpleTextEntry_Paint);
			this.Leave += new System.EventHandler(this.SimpleTextEntry_Leave);
			this.Deactivate += new System.EventHandler(this.SimpleTextEntry_Deactivate);
			this.ResumeLayout(false);

		}
		#endregion

		private void SimpleTextEntry_Load(object sender, System.EventArgs e)
		{
			if ( tx.Text.Length > 0 )
			{
				tx.SelectAll();
			}

			tx.Focus();
		}

		private void SimpleTextEntry_Deactivate(object sender, System.EventArgs e)
		{
			Close();
		}

		private void SimpleTextEntry_Leave(object sender, System.EventArgs e)
		{
			Close();
		}

		private void tx_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.Enter )
			{
				Close();
			}
		}

		private void SimpleTextEntry_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			TheBox.Common.Utility.DrawBorder( this, e.Graphics );
		}
	}
}
