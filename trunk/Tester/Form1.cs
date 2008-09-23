using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Tester
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PropertyGrid pGrid;
		private TheBox.MapViewer.MapViewer mapViewer1;
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pGrid = new System.Windows.Forms.PropertyGrid();
			this.mapViewer1 = new TheBox.MapViewer.MapViewer();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// pGrid
			// 
			this.pGrid.CommandsVisibleIfAvailable = true;
			this.pGrid.LargeButtons = false;
			this.pGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.pGrid.Location = new System.Drawing.Point(8, 8);
			this.pGrid.Name = "pGrid";
			this.pGrid.Size = new System.Drawing.Size(264, 352);
			this.pGrid.TabIndex = 1;
			this.pGrid.Text = "propertyGrid1";
			this.pGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.pGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// mapViewer1
			// 
			this.mapViewer1.Center = new System.Drawing.Point(0, 0);
			this.mapViewer1.DisplayErrors = true;
			this.mapViewer1.DrawStatics = false;
			this.mapViewer1.Location = new System.Drawing.Point(280, 8);
			this.mapViewer1.Map = TheBox.MapViewer.Maps.Felucca;
			this.mapViewer1.Name = "mapViewer1";
			this.mapViewer1.Navigation = TheBox.MapViewer.MapNavigation.LeftMouseButton;
			this.mapViewer1.RotateView = false;
			this.mapViewer1.ShowCross = true;
			this.mapViewer1.Size = new System.Drawing.Size(304, 352);
			this.mapViewer1.TabIndex = 2;
			this.mapViewer1.Text = "mapViewer1";
			this.mapViewer1.WheelZoom = false;
			this.mapViewer1.XRayView = false;
			this.mapViewer1.ZoomLevel = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(416, 376);
			this.button1.Name = "button1";
			this.button1.TabIndex = 3;
			this.button1.Text = "button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(592, 430);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.mapViewer1);
			this.Controls.Add(this.pGrid);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			pGrid.SelectedObject = mapViewer1;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			TheBox.Common.Utility.SendToUO( string.Format( "[go {0} {1} {2}\n",
				mapViewer1.Center.X,
				mapViewer1.Center.Y,
				mapViewer1.GetMapHeight() ) );
		}
	}
}