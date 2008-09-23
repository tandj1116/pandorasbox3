using System;
using System.Collections;

namespace TheBox.Data
{
	/// <summary>
	/// Provides a matrix for the UO map space
	/// </summary>
	public class UOMatrix
	{
		/// <summary>
		/// ArrayList containing all the rows. Each row is an array list of integer values where 0 means no object
		/// </summary>
		private ArrayList m_Rows;

		/// <summary>
		/// The width in cells of the matrix
		/// </summary>
		private int m_Width;
		
		/// <summary>
		/// The height in cells of the matrix
		/// </summary>
		private int m_Height;

		/// <summary>
		/// Creates a new UOMatrix object
		/// </summary>
		/// <param name="width">The cells width</param>
		/// <param name="height">The cells height</param>
		public UOMatrix( int width, int height )
		{
			m_Rows = new ArrayList();

			for ( int i = 0; i < height; i++ )
			{
				int[] cells = new int[ width ];
				cells.Initialize();

				m_Rows.Add( new ArrayList( cells ) );
			}

			m_Width = width;
			m_Height = height;
		}

		/// <summary>
		/// Gets or sets the width of the matrix in cell units
		/// </summary>
		public int Width
		{
			get { return m_Width; }
			set
			{
				if ( value < m_Width )
				{
					// New value is lower, keep old values in memory
					m_Width = value;
				}
				else if ( value > m_Width )
				{
					int difference = value - m_Width;

					foreach( ArrayList row in m_Rows )
					{
						int[] cells = new int[ difference ];
						cells.Initialize();

						row.AddRange( cells );
					}

					m_Width = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the height of the matrix in cells
		/// </summary>
		public int Height
		{
			get { return m_Height; }
			set
			{
				if ( value < m_Height )
				{
					// New value is lower, keep old values in memory
					m_Height = value;
				}
				else if ( value > m_Height )
				{
					int difference = value - m_Height;

					for ( int i = 0; i < difference; i++ )
					{
						int[] cells = new int[ m_Width ];
						cells.Initialize();

						m_Rows.Add( new ArrayList( cells ) );
					}

					m_Height = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the cell value at the specified location
		/// </summary>
		public int this[int x, int y]
		{
			get
			{
				if ( InRange( x, y ) )
				{
					ArrayList row = m_Rows[ y ] as ArrayList;
					return (int) row[ x ];
				}
				else
				{
					throw new IndexOutOfRangeException( string.Format( "The cell ({0},{1}) doesn't exist in the matrix.", x, y ) );
				}
			}
			set
			{
				if ( InRange( x, y ) )
				{
					ArrayList row = m_Rows[ y ] as ArrayList;
					row[ x ] = value;
				}
				else
				{
					throw new IndexOutOfRangeException( string.Format( "The cell ({0},{1}) doesn't exist in the matrix.", x, y ) );
				}
			}
		}

		/// <summary>
		/// Verifies if the specified coordinate is valid for the matrix
		/// </summary>
		/// <param name="x">The X coordinate</param>
		/// <param name="y">The Y coordinate</param>
		/// <returns>True if the location is valid</returns>
		private bool InRange( int x, int y )
		{
			return ( x >= 0 && x < m_Width && y >= 0 && y < m_Height );
		}
	}
}
