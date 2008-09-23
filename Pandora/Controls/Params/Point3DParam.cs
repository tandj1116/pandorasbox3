using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace TheBox.Controls.Params
{
	/// <summary>
	/// Summary description for Point3DParam.
	/// </summary>
	public class Point3DParam : System.Windows.Forms.UserControl, IParam
	{
		private System.Windows.Forms.Label labName;
		private System.Windows.Forms.LinkLabel lnk;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Point3DForm m_Form;
		private int m_X;
		private int m_Y;
		private int m_Z;

		public Point3DParam()
		{
			// This call is required by the Windows.Forms Form Designer.
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

				Pandora.ToolTip.SetToolTip( labName, null );
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
			this.labName = new System.Windows.Forms.Label();
			this.lnk = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// labName
			// 
			this.labName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labName.Location = new System.Drawing.Point(0, 0);
			this.labName.Name = "labName";
			this.labName.Size = new System.Drawing.Size(96, 16);
			this.labName.TabIndex = 0;
			// 
			// lnk
			// 
			this.lnk.Location = new System.Drawing.Point(0, 16);
			this.lnk.Name = "lnk";
			this.lnk.Size = new System.Drawing.Size(100, 20);
			this.lnk.TabIndex = 1;
			this.lnk.TabStop = true;
			this.lnk.Text = "(0,0,0)";
			this.lnk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lnk.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk_LinkClicked);
			// 
			// Point3DParam
			// 
			this.Controls.Add(this.lnk);
			this.Controls.Add(this.labName);
			this.Name = "Point3DParam";
			this.Size = new System.Drawing.Size(96, 36);
			this.Load += new System.EventHandler(this.Point3DParam_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void lnk_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			m_Form = new Point3DForm( m_X, m_Y, m_Z );

			Point clientPoint = new Point( 10, Height - m_Form.Height );
			m_Form.Location = this.PointToScreen( clientPoint );

			m_Form.Closed += new EventHandler(m_Form_Closed);
			
			m_Form.Show();
		}

		private void m_Form_Closed(object sender, EventArgs e)
		{
			lnk.Text = m_Form.SelectedPoint;
		}

		private void Point3DParam_Load(object sender, System.EventArgs e)
		{
			try
			{
				GetPointFromMap();
				Pandora.Map.MapLocationChanged += new EventHandler(Map_LocationChanged);
			}
			catch {}
		}

		private void GetPointFromMap()
		{
			m_X = Pandora.Map.Center.X;
			m_Y = Pandora.Map.Center.Y;
			m_Z = Pandora.Map.GetMapHeight( Pandora.Map.Center );

			lnk.Text = string.Format( "({0},{1},{2})", m_X, m_Y, m_Z );
		}

		#region IParam Members

		public string ParamName
		{
			set
			{
				labName.Text = value;
				Pandora.ToolTip.SetToolTip( labName, value );
			}
		}

		public string Value
		{
			get
			{
				return lnk.Text;
			}
		}

		public bool IsDefined
		{
			get
			{
				return true;
			}
		}

		#endregion

		private void Map_LocationChanged(object sender, EventArgs e)
		{
			GetPointFromMap();
		}
	}
}
