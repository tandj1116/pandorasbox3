//using System;
//using System.Collections;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Windows.Forms;
//
//namespace TheBox.Controls
//{
//	/// <summary>
//	/// Summary description for TileList.
//	/// </summary>
//	public class TileList : System.Windows.Forms.UserControl
//	{
//		private System.Windows.Forms.Panel panel1;
//		private System.Windows.Forms.Button button1;
//		private System.Windows.Forms.Button button3;
//		private System.Windows.Forms.Button button4;
//		private System.Windows.Forms.Label label1;
//		private System.Windows.Forms.Panel TilePanel;
//		private System.Windows.Forms.VScrollBar sBar;
//		/// <summary> 
//		/// Required designer variable.
//		/// </summary>
//		private System.ComponentModel.Container components = null;
//
//		public TileList()
//		{
//			// This call is required by the Windows.Forms Form Designer.
//			InitializeComponent();
//
//			// TODO: Add any initialization after the InitializeComponent call
//
//		}
//
//		/// <summary> 
//		/// Clean up any resources being used.
//		/// </summary>
//		protected override void Dispose( bool disposing )
//		{
//			if( disposing )
//			{
//				if(components != null)
//				{
//					components.Dispose();
//				}
//			}
//			base.Dispose( disposing );
//		}
//
//		#region Component Designer generated code
//		/// <summary> 
//		/// Required method for Designer support - do not modify 
//		/// the contents of this method with the code editor.
//		/// </summary>
//		private void InitializeComponent()
//		{
//			this.panel1 = new System.Windows.Forms.Panel();
//			this.button1 = new System.Windows.Forms.Button();
//			this.button3 = new System.Windows.Forms.Button();
//			this.button4 = new System.Windows.Forms.Button();
//			this.label1 = new System.Windows.Forms.Label();
//			this.TilePanel = new System.Windows.Forms.Panel();
//			this.sBar = new System.Windows.Forms.VScrollBar();
//			this.panel1.SuspendLayout();
//			this.TilePanel.SuspendLayout();
//			this.SuspendLayout();
//			// 
//			// panel1
//			// 
//			this.panel1.Controls.Add(this.label1);
//			this.panel1.Controls.Add(this.button4);
//			this.panel1.Controls.Add(this.button3);
//			this.panel1.Controls.Add(this.button1);
//			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
//			this.panel1.Location = new System.Drawing.Point(0, 0);
//			this.panel1.Name = "panel1";
//			this.panel1.Size = new System.Drawing.Size(216, 48);
//			this.panel1.TabIndex = 1;
//			// 
//			// button1
//			// 
//			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.button1.Location = new System.Drawing.Point(72, 24);
//			this.button1.Name = "button1";
//			this.button1.Size = new System.Drawing.Size(72, 23);
//			this.button1.TabIndex = 0;
//			this.button1.Text = "Apply";
//			// 
//			// button3
//			// 
//			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.button3.Location = new System.Drawing.Point(144, 0);
//			this.button3.Name = "button3";
//			this.button3.Size = new System.Drawing.Size(72, 23);
//			this.button3.TabIndex = 2;
//			this.button3.Text = "Delete";
//			// 
//			// button4
//			// 
//			this.button4.FlatStyle = System.Windows.Forms.FlatStyle.System;
//			this.button4.Location = new System.Drawing.Point(144, 24);
//			this.button4.Name = "button4";
//			this.button4.Size = new System.Drawing.Size(72, 23);
//			this.button4.TabIndex = 3;
//			this.button4.Text = "Select";
//			// 
//			// label1
//			// 
//			this.label1.BackColor = System.Drawing.Color.White;
//			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
//			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//			this.label1.Location = new System.Drawing.Point(8, 8);
//			this.label1.Name = "label1";
//			this.label1.Size = new System.Drawing.Size(56, 32);
//			this.label1.TabIndex = 4;
//			this.label1.Text = "New Item";
//			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
//			// 
//			// TilePanel
//			// 
//			this.TilePanel.Controls.Add(this.sBar);
//			this.TilePanel.Dock = System.Windows.Forms.DockStyle.Fill;
//			this.TilePanel.Location = new System.Drawing.Point(0, 48);
//			this.TilePanel.Name = "TilePanel";
//			this.TilePanel.Size = new System.Drawing.Size(216, 232);
//			this.TilePanel.TabIndex = 2;
//			// 
//			// sBar
//			// 
//			this.sBar.Dock = System.Windows.Forms.DockStyle.Right;
//			this.sBar.Location = new System.Drawing.Point(199, 0);
//			this.sBar.Name = "sBar";
//			this.sBar.Size = new System.Drawing.Size(17, 232);
//			this.sBar.TabIndex = 0;
//			// 
//			// TileList
//			// 
//			this.Controls.Add(this.TilePanel);
//			this.Controls.Add(this.panel1);
//			this.Name = "TileList";
//			this.Size = new System.Drawing.Size(216, 280);
//			this.panel1.ResumeLayout(false);
//			this.TilePanel.ResumeLayout(false);
//			this.ResumeLayout(false);
//
//		}
//		#endregion
//	}
//}
