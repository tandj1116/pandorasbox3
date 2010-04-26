using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TheBox.Forms
{
	public delegate void PopUpCallback();

	/// <summary>
	/// Summary description for PopUpForm.
	/// </summary>
	public class PopUpForm : System.Windows.Forms.Form
	{
		private enum HotSpot
		{
			None,
			Title,
			Close
		}

		private System.ComponentModel.IContainer components;

		private static Color m_TopColor = Color.LightSkyBlue;
		private static Color m_BottomColor = Color.AliceBlue;
		private static Color m_BorderColor = Color.SteelBlue;
		private static Color m_TextColor = Color.SlateBlue;
		private string m_Title;
		private string m_Message;
		private Rectangle m_TitleBounds;
		private System.Windows.Forms.ImageList imgList;
		private Rectangle m_MessageBounds;
		private Rectangle m_CloseBounds;
		private HotSpot m_HotSpot = HotSpot.None;
		private bool m_ToolTipMode = false;
		private PopUpCallback m_Callback;

		public PopUpForm()
		{
			InitializeComponent();
			Pandora.Localization.LocalizeControl( this );

			// Flickering fix
			SetStyle( ControlStyles.DoubleBuffer, true );
			SetStyle( ControlStyles.UserPaint, true );
			SetStyle( ControlStyles.AllPaintingInWmPaint, true );
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PopUpForm));
			this.imgList = new System.Windows.Forms.ImageList(this.components);
			// 
			// imgList
			// 
			this.imgList.ImageSize = new System.Drawing.Size(13, 13);
			this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
			this.imgList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// PopUpForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.AliceBlue;
			this.ClientSize = new System.Drawing.Size(264, 144);
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "PopUpForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PopUpForm_MouseDown);
			this.Load += new System.EventHandler(this.PopUpForm_Load);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PopUpForm_MouseUp);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PopUpForm_MouseMove);
			this.MouseLeave += new System.EventHandler(this.PopUpForm_MouseLeave);

		}
		#endregion

		#region Events

		private void Calculate()
		{
			Graphics g = CreateGraphics();

			Rectangle rect = new Rectangle( 0, 0, Width - 1, Height - 1 );

			Font font = new Font( "Arial", 8.25f );

			FontStyle titleStyle = FontStyle.Bold;

			if ( m_HotSpot == HotSpot.Title )
			{
				titleStyle |= FontStyle.Underline;
			}

			Font titleFont = new Font( "Arial",8.25f, titleStyle );

			SizeF size1 = g.MeasureString( m_Title, titleFont, 256 );
			SizeF size2 = g.MeasureString( m_Message, font, 256 );

			Size size = new Size( (int) Math.Ceiling( Math.Max( size1.Width, size2.Width ) ) + 12 + 13, (int) Math.Ceiling( size1.Height + size2.Height ) + 12 );
			this.Size = size;

			m_CloseBounds = new Rectangle( size.Width - 17, 4, 13, 13 );

			m_TitleBounds = new Rectangle( 4, 4, (int) Math.Ceiling( size1.Width ), (int) Math.Ceiling( size1.Height ) );
			m_MessageBounds = new Rectangle( 4, 8 + (int) Math.Ceiling( size1.Height ), (int) Math.Ceiling( size2.Width ), (int) Math.Ceiling( size2.Height ) );

			font.Dispose();
			titleFont.Dispose();
			g.Dispose();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);

			Rectangle rect = new Rectangle( 0, 0, Width - 1, Height - 1 );

			LinearGradientBrush brush = new LinearGradientBrush( rect, m_BottomColor, m_TopColor, -90 );
			Pen pen = new Pen( m_BorderColor );
			Brush titleBrush = new SolidBrush( Color.DarkBlue );
			Brush textBrush = new SolidBrush( Color.SlateBlue );
			Font font = new Font( "Arial", 8.25f );
			FontStyle titleStyle = m_HotSpot == HotSpot.Title && m_Callback != null ? FontStyle.Underline | FontStyle.Bold : FontStyle.Bold;
			Font titleFont = new Font( "Arial",8.25f, titleStyle );

			e.Graphics.FillRectangle( brush, rect );
			e.Graphics.DrawRectangle( pen, rect );

			e.Graphics.DrawString( m_Title, titleFont, titleBrush, (RectangleF) m_TitleBounds );
			e.Graphics.DrawString( m_Message, font, textBrush, (RectangleF) m_MessageBounds );

			brush.Dispose();
			pen.Dispose();
			titleBrush.Dispose();
			textBrush.Dispose();
			titleFont.Dispose();
			font.Dispose();

			if ( m_HotSpot == HotSpot.Close )
			{
				if ( Control.MouseButtons == MouseButtons.Left )
				{
					e.Graphics.DrawImage( imgList.Images[ 2 ], m_CloseBounds );
				}
				else
				{
					e.Graphics.DrawImage( imgList.Images[ 1 ], m_CloseBounds );
				}
			}
			else
			{
				e.Graphics.DrawImage( imgList.Images[ 0 ], m_CloseBounds );
			}
		}

		private void PopUpForm_MouseLeave(object sender, System.EventArgs e)
		{
			if ( m_ToolTipMode )
				Close();
		}

		#endregion

		#region Showing

		public static void PopUp( Form owner, string title, string message, bool toolTipMode, PopUpCallback callback )
		{
			PopUpForm form = new PopUpForm();

			form.m_Title = title;
			form.m_Message = message;
			form.m_ToolTipMode = toolTipMode;
			form.m_Callback = callback;

			form.Calculate();

			form.ShowDialog( owner );
		}

		#endregion

		private void PopUpForm_Load(object sender, System.EventArgs e)
		{
			this.Location = Control.MousePosition;

			// Verify Visibility
			Rectangle screen = SystemInformation.WorkingArea;
			Rectangle form = new Rectangle( Location, Size );

			if ( !screen.Contains( form ) )
			{
				int dX = Right - screen.Width;
				int dY = Bottom - screen.Height;

				if ( dX > 0 )
				{
					dX = -dX;
				}
				else
				{
					dX = 0;
				}

				if ( dY > 0 )
				{
					dY = -dY;
				}
				else
				{
					dY = 0;
				}

				Location = new Point( Location.X + dX, Location.Y + dY );
			}
		}

		private void PopUpForm_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			HotSpot spot = HotSpot.None;

			if ( m_TitleBounds.Contains( e.X, e.Y ) )
			{
				spot = HotSpot.Title;
			}
			else if ( m_CloseBounds.Contains( e.X, e.Y ) )
			{
				spot = HotSpot.Close;
			}

			if ( m_HotSpot != spot )
			{
				m_HotSpot = spot;
				Refresh();
			}

			if ( m_HotSpot == HotSpot.Title && m_Callback != null )
			{
				Cursor = Cursors.Hand;
			}
			else
			{
				Cursor = Cursors.Arrow;
			}
		}

		private void PopUpForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( m_HotSpot == HotSpot.Close )
				Refresh();
		}

		private void PopUpForm_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( m_HotSpot == HotSpot.Close )
			{
				Close();
			}
			else if ( m_HotSpot == HotSpot.Title )
			{
				if ( m_Callback != null )
				{
					try
					{
						m_Callback.DynamicInvoke( null );
					}
					catch {}

					Close();
				}
			}
		}
	}
}