using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using TheBox.BoxServer;

namespace TheBox.Forms
{
	/// <summary>
	/// Summary description for BoxServerForm.
	/// </summary>
	public class BoxServerForm : System.Windows.Forms.Form
	{
		private bool m_Login = false;
		private bool m_Silent;
		private BoxMessage m_Message;
		private BoxMessage m_Response;

		/// <summary>
		/// Gets or sets the message returned by the server
		/// </summary>
		public BoxMessage Response
		{
			get { return m_Response; }
			set { m_Response = value; }
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new BoxServerForm object use to login into a BoxServer
		/// </summary>
		/// <param name="silent">Specifies whether to display error messages or not</param>
		public BoxServerForm( bool silent )
		{
			InitializeComponent();

			Pandora.Localization.LocalizeControl( this );

			m_Login = true;
			m_Silent = silent;
		}

		public BoxServerForm( BoxMessage message )
		{
			InitializeComponent();

			Pandora.Localization.LocalizeControl( this );

			m_Message = message;
			m_Login = false;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BoxServerForm));
			// 
			// BoxServerForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(158, 23);
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "BoxServerForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Misc.Connecting";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.BoxServerForm_Closing);
			this.Load += new System.EventHandler(this.BoxServerForm_Load);

		}
		#endregion

		#region Painting

		private int m_Progress = 0;
		private const int m_Step = 5;

		private Color m_StartColor = Color.MediumVioletRed;
		private Color m_EndColor = SystemColors.Control;

		private Timer m_Timer;

		private int m_MaxProgress 
		{
			get
			{
				return Size.Width - 10;
			}
		}

		private Point m_Start
		{
			get
			{
				return new Point( 5, 5 );
			}
		}

		private Point m_GradientEnd
		{
			get
			{
				return new Point( 5 + m_Progress, 5 );
			}
		}

		private LinearGradientBrush m_Brush
		{
			get
			{
				return new LinearGradientBrush( m_Start, m_GradientEnd, m_StartColor, m_EndColor );
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);

			if ( m_Progress == 150 )
			{
				m_Progress = 0;
			}

			m_Progress += m_Step;
			Brush brush = m_Brush;

			e.Graphics.FillRectangle( brush, 5, 5, m_Progress, 15 );

			Pen pen = new Pen( Color.Black );
			e.Graphics.DrawRectangle( pen, 5, 5, 150, 15 );

			pen.Dispose();
			brush.Dispose();
		}

		private void m_Timer_Tick(object sender, EventArgs e)
		{
			Refresh();
		}

		private void BoxServerForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if ( m_Timer != null )
			{
				m_Timer.Stop();
				m_Timer.Dispose();
			}
		}

		#endregion

		private void BoxServerForm_Load(object sender, System.EventArgs e)
		{
			m_Timer = new Timer();
			m_Timer.Interval = 200;
			m_Timer.Tick += new EventHandler(m_Timer_Tick);
			m_Timer.Start();

			if ( m_Login )
			{
				System.Threading.ThreadPool.QueueUserWorkItem( new System.Threading.WaitCallback( Connect ) );
			}
			else
			{
				System.Threading.ThreadPool.QueueUserWorkItem( new System.Threading.WaitCallback( SendMessage ) );
			}
		}
		private delegate void CloseForm();
		private void Connect( object o )
		{
			bool response = Pandora.BoxConnection.Connect( !m_Silent );
			Invoke(new CloseForm(Close));
		}

		private void SendMessage( object o )
		{
			BoxMessage result = Pandora.BoxConnection.ProcessMessage( m_Message );
			
			if ( result != null )
			{
				if ( Pandora.BoxConnection.CheckErrors( result ) )
				{
					DialogResult = DialogResult.OK;
					m_Response = result;
				}
				else
				{
					DialogResult = DialogResult.Cancel;
				}
			}

            if (!Pandora.BoxConnection.Connected)
				DialogResult = DialogResult.Cancel; // Account for communication error

			Close();
		}
	}
}