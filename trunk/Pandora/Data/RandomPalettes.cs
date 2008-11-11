using System;
// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
using System.Collections.Generic;
// Issue 10 - End
using System.Drawing;
using TheBox.BoxServer;

namespace TheBox.Data
{
	#region Random Rectangle
	/// <summary>
	/// Creates a random rectangle tiling
	/// </summary>
	public class RandomRectangle
	{
		private RandomTilesList m_TileSet;
		private Rectangle m_Rectangle;
		private bool[,] m_Grid;
		private double m_Fill;
		private BuildMessage m_Message;
		private int m_Map;
		private int m_Z;

		private int m_Hue;
		private HuesCollection m_RandomHues;

		/// <summary>
		/// Gets or sets the hue used to hue the items
		/// </summary>
		public int Hue
		{
			get { return m_Hue; }
			set
			{
				m_Hue = value;
				m_RandomHues = null;
			}
		}

		/// <summary>
		/// Gets or sets the random hues collection used to hue the items
		/// </summary>
		public HuesCollection RandomHues
		{
			get { return m_RandomHues; }
			set { m_RandomHues = value; }
		}

		/// <summary>
		/// Gets or sets the Z at which tiling occurs
		/// </summary>
		public int Z
		{
			get { return m_Z; }
			set { m_Z = value; }
		}

		/// <summary>
		/// Creates a BoxMessage by applying the random logic to the structure
		/// </summary>
		/// <returns>The calculated BoxMessage</returns>
		public BuildMessage CreateMessage()
		{
			GenerateGrid();
			GenerateItems();

			return m_Message;
		}

		/// <summary>
		/// Creates a new random rectangle tiler
		/// </summary>
		/// <param name="tileset">The random tileset to use</param>
		/// <param name="rectangle">The rectangle for the tiling</param>
		/// <param name="fillpercentage">The percentage of the rectangle that should be filled</param>
		/// <param name="map">The map on which the tiling will occur</param>
		public RandomRectangle( RandomTilesList tileset, Rectangle rectangle, double fillpercentage, int map )
		{
			m_TileSet = tileset;
			m_Rectangle = rectangle;
			m_Fill = fillpercentage;
			m_Map = map;
		}

		/// <summary>
		/// Generates the grid for the tiling according to the fill percentage
		/// </summary>
		private void GenerateGrid()
		{
			m_Grid = new bool[ m_Rectangle.Width, m_Rectangle.Height ];
			Random rnd = new Random();

			for ( int x = 0; x < m_Rectangle.Width; x++ )
			{
				for ( int y = 0; y < m_Rectangle.Height; y++ )
				{
					m_Grid[ x, y ] = rnd.NextDouble() <= m_Fill;
				}
			}
		}

		/// <summary>
		/// Generates the items
		/// </summary>
		private void GenerateItems()
		{
			Random rnd = new Random();
			m_Message = new BuildMessage();

			MapViewer.Maps oldMap = Pandora.Map.Map;
			Pandora.Map.Map = (MapViewer.Maps) m_Map;

			for ( int x = 0; x < m_Rectangle.Width; x++ )
			{
				for ( int y = 0; y < m_Rectangle.Height; y++ )
				{
					if ( ! m_Grid[ x, y ] )
						continue;

					RandomTile tile = m_TileSet.Tiles[ rnd.Next( m_TileSet.Tiles.Count ) ] as RandomTile;

					foreach( int id in tile.Items )
					{
						BuildItem item = new BuildItem();
						item.ID = id;

						// Hue
						int hue = m_Hue;

						if ( m_RandomHues != null )
						{
							// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
							hue = m_RandomHues.Hues[ rnd.Next( m_RandomHues.Hues.Count ) ];
							// Issue 10 - End
						}
						
						item.Hue = hue;

						// Location
						item.X = m_Rectangle.X + x;
						item.Y = m_Rectangle.Y + y;

						if ( m_Z != -1 )
							item.Z = Pandora.Map.GetMapHeight( new Point( item.X, item.Y ) );
						else
							item.Z = m_Z;
						

						m_Message.Items.Add( item );
					}
				}
			}

			Pandora.Map.Map = oldMap;
		}
	}
	#endregion

	#region Random Brush

	/// <summary>
	/// Defines a brush that can be randomized
	/// </summary>
	public class RandomBrush
	{
		private int m_Width;
		private int m_Height;
		private bool[,] m_Grid;

		/// <summary>
		/// Gets or sets the brush width
		/// </summary>
		public int Width
		{
			get { return m_Width; }
			set { m_Width = value; }
		}

		/// <summary>
		/// Gets or sets the brush height
		/// </summary>
		public int Height
		{
			get { return m_Height; }
			set { m_Height = value; }
		}

		public RandomBrush( int width, int height )
		{
			m_Width = width;
			m_Height = height;
		}

		/// <summary>
		/// Randomizes the brush
		/// </summary>
		/// <param name="fill">The percentage of the brush area to fill</param>
		public void RandomizeBrush( double fill )
		{
			m_Grid = new bool[ m_Width, m_Height ];

			Random rnd = new Random();

			for ( int x = 0; x < m_Width; x++ )
			{
				for ( int y = 0; y < m_Height; y++ )
				{
					m_Grid[ x, y ] = rnd.NextDouble() < fill;
				}
			}
		}

		/// <summary>
		/// Creates the message that performs the brush
		/// </summary>
		/// <param name="tileset">The tileset for the brush</param>
		/// <param name="hues">A list of hues to use for the brush</param>
		/// <param name="fill">The are percentage to fill</param>
		/// <returns>The server message created</returns>
		// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
		private RandomBrushMessage CreateMessage( RandomTilesList tileset, List<int> hues, double fill )
		// Issue 10 - End
		{
			RandomBrushMessage msg = new RandomBrushMessage();
			Random rnd = new Random();

			RandomizeBrush( fill );

			for( int x = 0; x < m_Width; x++ )
			{
				for ( int y = 0; y < m_Height; y++ )
				{
					if ( m_Grid[ x, y ] )
					{
						RandomTile tile = tileset.Tiles[ rnd.Next( tileset.Tiles.Count ) ] as RandomTile;

						foreach( int id in tile.Items )
						{
							BuildItem item = new BuildItem();

							item.ID = id;
							// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
							item.Hue = hues[ rnd.Next( hues.Count ) ];
							// Issue 10 - End

							item.X = x - ( m_Width / 2 );
							item.Y = y - ( m_Height / 2 );

							msg.Items.Add( item );
						}
					}
				}
			}

			return msg;
		}

		/// <summary>
		/// Creates the message that will perform the brush using a random hue
		/// </summary>
		/// <param name="tileset">The tileset to choose items from</param>
		/// <param name="hues">The hues list to choose hues from</param>
		/// <param name="fill">The percentage of the area to fill</param>
		/// <returns>A message that can be sent to the server</returns>
		public RandomBrushMessage CreateMessage( RandomTilesList tileset, HuesCollection hues, double fill )
		{
			return CreateMessage( tileset, hues.Hues, fill );
		}

		/// <summary>
		/// Creates the message that will perform the brush using a single hue
		/// </summary>
		/// <param name="tileset">The tileset to use in the message</param>
		/// <param name="hue">The hue to use for the items</param>
		/// <param name="fill">Percentage of the area to fill</param>
		/// <returns>A message that can be sent to the server</returns>
		public RandomBrushMessage CreateMessage( RandomTilesList tileset, int hue, double fill )
		{
			// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
			List<int> list = new List<int>();
			// Issue 10 - End
			list.Add( hue );

			return CreateMessage( tileset, list, fill );
		}

	}

	#endregion
}