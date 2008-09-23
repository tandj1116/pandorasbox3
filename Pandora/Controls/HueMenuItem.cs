using System;
using System.Drawing;
using System.Windows.Forms;

namespace TheBox.Controls
{
	public class HueMenuItem : System.Windows.Forms.MenuItem
	{
		private System.Drawing.Bitmap Image;
		private bool NoHue = false;

		public HueMenuItem( string text, short[] colors )
		{
			// Override Owner Draw
			this.OwnerDraw = true;

			this.Text = text;

			MakeImage( colors );
		}

		private void MakeImage( short[] colors )
		{
			Image = new System.Drawing.Bitmap( 34, 14 );

			if ( colors == null )
			{
				this.NoHue = true;
				return;
			}

			// Draw border
			for ( int x = 0; x < 34; x++ )
			{
				Image.SetPixel( x, 0, Color.Black );
				Image.SetPixel( x, 13, Color.Black );
			}
			for ( int y = 1; y < 14; y++ )
			{
				Image.SetPixel( 0, y, Color.Black );
				Image.SetPixel( 33, y, Color.Black );
			}

			for ( int i = 0; i < 32; i++ )
			{
				for ( int y = 1; y < 13; y++ )
					Image.SetPixel( i + 1, y, TheBox.Mul.Hue.ToColor( colors[i] ) );
			}
		}

		protected override void OnMeasureItem(System.Windows.Forms.MeasureItemEventArgs e)
		{
			Font MenuFont = System.Windows.Forms.SystemInformation.MenuFont;

			StringFormat stringformat = new StringFormat();

			SizeF sizef = e.Graphics.MeasureString( this.Text, MenuFont, 1000, stringformat );

			e.ItemWidth = (int) Math.Ceiling( sizef.Width ) + Image.Width;
			e.ItemHeight = Math.Max( (int) Math.Ceiling( sizef.Height ), Image.Height  );
		}

		protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
		{
			Font MenuFont = System.Windows.Forms.SystemInformation.MenuFont;

			SolidBrush menuBrush = null;

			if ( ( e.State & DrawItemState.Selected ) != 0 )
			{
				menuBrush = new SolidBrush( SystemColors.HighlightText );
			}
			else
			{
				menuBrush = new SolidBrush( SystemColors.MenuText );
			}

			StringFormat stringformat = new StringFormat();
			stringformat.LineAlignment = StringAlignment.Center;

			Rectangle rectImage = e.Bounds;

			rectImage.Width = Image.Width;
			rectImage.Height = Image.Height;

			Rectangle rectText = e.Bounds;

			rectText.X += rectImage.Width;

			// Draw rect
			if ( ( e.State & DrawItemState.Selected ) != 0 )
				e.Graphics.FillRectangle( SystemBrushes.Highlight, e.Bounds );
			else
				e.Graphics.FillRectangle( SystemBrushes.Menu, e.Bounds );


			if ( this.NoHue )
			{
				// Draw rect
				Pen blackPen = new Pen( new SolidBrush( Color.Black ) );
				Pen redPen = new Pen( new SolidBrush( Color.Red ) );

				e.Graphics.DrawRectangle( blackPen, rectImage.X, rectImage.Y, rectImage.Width - 1, rectImage.Height - 1);
				e.Graphics.DrawLine( redPen, rectImage.Left, rectImage.Top, rectImage.Right - 1, rectImage.Bottom - 1);
				e.Graphics.DrawLine( redPen, rectImage.Left, rectImage.Bottom - 1, rectImage.Right - 1, rectImage.Top );

			}
			else
			{
				// Draw image
				e.Graphics.DrawImage( Image, rectImage );
			}

			// Draw text
			e.Graphics.DrawString( this.Text, 
				MenuFont, 
				menuBrush, 
				e.Bounds.Left + Image.Width,
				e.Bounds.Top );
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose (disposing);

			if ( disposing )
			{
				if ( this.Image != null )
					Image.Dispose();
			}
		}

	}
}
