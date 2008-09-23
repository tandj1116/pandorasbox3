using System;
using System.Collections;
using System.Drawing;

namespace TheBox.Roofing
{
	/// <summary>
	/// Defines the image of the roof being created by the user
	/// </summary>
	public class RoofImage
	{
		private ArrayList m_Data;
		private int m_Height;
		private int m_Width;
		private Bitmap m_Image;

		/// <summary>
		/// Gets or sets the data containing the structure of the roof
		/// </summary>
		public ArrayList Data
		{
			get { return m_Data; }
			set { m_Data = value; }
		}

		/// <summary>
		/// Gets or sets the image width
		/// </summary>
		public int Width
		{
			get { return m_Width; }
			set { m_Width = value; }
		}

		/// <summary>
		/// Gets or sets the image height
		/// </summary>
		public int Height
		{
			get { return m_Height; }
			set { m_Height = value; }
		}

		/// <summary>
		/// Gets the roof image
		/// </summary>
		public Bitmap Image
		{
			get { return m_Image; }
		}

		/// <summary>
		/// Creates a new RoofImage object
		/// </summary>
		public RoofImage()
		{
			m_Data = new ArrayList();
			m_Height = 0;
			m_Width = 0;
			m_Image = new Bitmap( 240, 240 );
		}

		/// <summary>
		/// Paints a rectangle portion of the image
		/// </summary>
		/// <param name="rect">The rectangle bounds that will be painted</param>
		/// <param name="color">The color used for the paining</param>
		private void PaintRect( Rectangle rect, Color color )
		{
			for ( int i = rect.Left; i <= rect.Right; i++ )
			{
				for ( int j = rect.Top; j <= rect.Bottom; j++ )
				{
					m_Image.SetPixel( i, j, color );
				}
			}
		}

		/// <summary>
		/// Calculates and creates the image
		/// </summary>
		public void CreateImage()
		{
			// Use white background
			for ( int x = 0; x < m_Image.Width; x++ )
			{
				for ( int y = 0; y < m_Image.Height; y++ )
				{
					m_Image.SetPixel( x, y, Color.White );
				}
			}

			if ( m_Data.Count == 0 )
			{
				return; // No data
			}

			// Scale factor
			int dw = m_Image.Width / m_Width;
			int dh = m_Image.Height / m_Height;

			if ( dh > dw )
			{
				dh = dw;
			}
			else
			{
				dw = dh;
			}

			Point basePoint = Point.Empty;

			basePoint.X = ( m_Image.Width - m_Width * dw ) / 2;
			basePoint.Y = ( m_Image.Height - m_Height * dh ) / 2;

			int p = 0; // Counter for the data
			Rectangle rect = Rectangle.Empty;

			for ( int y = 0; y < m_Height; y++ )
			{
				rect.Y = y * dh + basePoint.Y;
				rect.Height = ( y + 1 ) * dh + basePoint.Y - rect.Y;

				for ( int x = 0; x < m_Width; x++ )
				{
					rect.X = x * dw + basePoint.X;
					rect.Width = ( x + 1 ) * dw + basePoint.X - rect.X;

					if ( (int) m_Data[ p ] > 0 )
					{
						// Data positive: valid piece - Use Green
						PaintRect( rect, Color.FromArgb( 0, Math.Min( 255, ( (int) m_Data[ p ] * 10 ) + 100 ), 0 ) );
					}
					else if ( (int) m_Data[ p ] < 0 )
					{
						// Data negative: not valid piece - Use Red
						PaintRect( rect, Color.FromArgb( Math.Max( -255, ( -(int) m_Data[ p ] * 10 ) + 100 ), 0, 0 ) );
					}

					p++;
				}
			}
		}
	}
}
