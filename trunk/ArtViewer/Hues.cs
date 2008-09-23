using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace TheBox.UltimaImport
{
	public class Hues
	{
		
	}

	public class Hue
	{
		public static Hue GetHue( int index )
		{
			string path = ArtViewer.ArtViewer.MulManager[ "hues.mul" ];

			if ( path == null || !File.Exists( path ) )
			{
				return null;
			}

			Hue hue = null;

			using ( FileStream fs = new FileStream( path, FileMode.Open, FileAccess.Read, FileShare.Read ) )
			{
				BinaryReader reader = new BinaryReader( fs );

				// Group number: / 8
				int group = index / 8;
				int hueIndex = index % 8;

				int position = group * 708 + hueIndex * 88;

				if ( position + 88 < fs.Length )
				{
					fs.Seek( position, SeekOrigin.Begin );

					hue = new Hue( index, reader );
				}
			}

			return hue;
		}

		public static void ApplyHueTo( int index, Bitmap bmp, bool onlyHueGrayPixels )
		{
			Hue hue = GetHue( index );

			if ( hue != null )
			{
				hue.ApplyTo( bmp, onlyHueGrayPixels );
			}
		}

		public static void ApplyHueTo( Hue hue, Bitmap bmp, bool onlyHueGrayPixels )
		{
			if ( hue != null )
			{
				hue.ApplyTo( bmp, onlyHueGrayPixels );
			}
		}

		private int m_Index;
		private short[] m_Colors;
		private string m_Name;

		public int Index{ get{ return m_Index; } }
		public short[] Colors{ get{ return m_Colors; } }
		public string Name{ get{ return m_Name; } }

		public Hue( int index )
		{
			m_Name = "Null";
			m_Index = index;
			m_Colors = new short[34];
		}

		public Color GetColor( int index )
		{
			int c16 = m_Colors[index];

			return Color.FromArgb( (c16 & 0x7C00) >> 7, (c16 & 0x3E0) >> 2, (c16 & 0x1F) << 3 );
		}

		public Hue( int index, BinaryReader bin )
		{
			m_Index = index;
			m_Colors = new short[34];

			for ( int i = 0; i < 34; ++i )
				m_Colors[i] = (short)(bin.ReadUInt16() | 0x8000);

			bool nulled = false;

			StringBuilder sb = new StringBuilder( 20, 20 );

			for ( int i = 0; i < 20; ++i )
			{
				char c = (char)bin.ReadByte();

				if ( c == 0 )
					nulled = true;
				else if ( !nulled )
					sb.Append( c );
			}

			m_Name = sb.ToString();
		}

		public unsafe void ApplyTo( Bitmap bmp, bool onlyHueGrayPixels )
		{
			BitmapData bd = bmp.LockBits( new Rectangle( 0, 0, bmp.Width, bmp.Height ), ImageLockMode.ReadWrite, PixelFormat.Format16bppArgb1555 );

			int stride = bd.Stride >> 1;
			int width = bd.Width;
			int height = bd.Height;
			int delta = stride - width;

			ushort *pBuffer = (ushort *)bd.Scan0;
			ushort *pLineEnd = pBuffer + width;
			ushort *pImageEnd = pBuffer + (stride * height);

			ushort *pColors = stackalloc ushort[0x40];

			fixed ( short *pOriginal = m_Colors )
			{
				ushort *pSource = (ushort *)pOriginal + 2;
				ushort *pDest = pColors;
				ushort *pEnd = pDest + 32;

				while ( pDest < pEnd )
					*pDest++ = 0;

				pEnd += 32;

				while ( pDest < pEnd )
					*pDest++ = *pSource++;
			}

			if ( onlyHueGrayPixels )
			{
				int c;
				int r;
				int g;
				int b;

				while ( pBuffer < pImageEnd )
				{
					while ( pBuffer < pLineEnd )
					{
						c = *pBuffer;
						r = (c >> 10) & 0x1F;
						g = (c >>  5) & 0x1F;
						b =  c        & 0x1F;

						if ( r == g && r == b )
							*pBuffer++ = pColors[c >> 10];
						else
							++pBuffer;
					}

					pBuffer += delta;
					pLineEnd += stride;
				}
			}
			else
			{
				while ( pBuffer < pImageEnd )
				{
					while ( pBuffer < pLineEnd )
					{
						*pBuffer = pColors[(*pBuffer) >> 10];
						++pBuffer;
					}

					pBuffer += delta;
					pLineEnd += stride;
				}
			}

			bmp.UnlockBits( bd );
		}
	}
}