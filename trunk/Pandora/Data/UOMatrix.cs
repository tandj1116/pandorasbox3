using System;
// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
using System.Collections.Generic;
// Issue 10 - End

namespace TheBox.Data
{
	/// <summary>
	/// Provides a matrix for the UO map space
	/// </summary>
	public class UOMatrix
	{
		/// <summary>
		/// List containing all the rows. Each row is an array list of integer values where 0 means no object
		/// </summary>
		// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
		private List<List<int>> m_Rows;
		// Issue 10 - End

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
			// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
			m_Rows = new List<List<int>>();
			// Issue 10 - End

			for ( int i = 0; i < height; i++ )
			{
				int[] cells = new int[ width ];
				cells.Initialize();

				// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
				m_Rows.Add(new List<int>());
				m_Rows[i].AddRange(cells);
				// Issue 10 - End
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
					// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
					foreach( List<int> row in m_Rows )
					// Issue 10 - End
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

						// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
						m_Rows.Add( new List<int>());
						m_Rows[i].AddRange(cells);
						// Issue 10 - End
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
					// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
					List<int> row = m_Rows[y];
					return row[ x ];
					// Issue 10 - End
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
					// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
					List<int> row = m_Rows[ y ];
					// Issue 10 - End
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
