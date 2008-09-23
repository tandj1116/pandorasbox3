using System;
using System.Collections;
using System.IO;

namespace TheBox.Roofing
{
	/// <summary>
	/// Defines a tile set used to create a roof
	/// </summary>
	public class TileSet
	{
		private string m_Name;
		private ArrayList m_Tiles;

		/// <summary>
		/// Gets the name of this tileset
		/// </summary>
		public string Name
		{
			get { return m_Name; }
		}

		/// <summary>
		/// Gets the tiles included in this tileset
		/// </summary>
		public ArrayList Tiles
		{
			get { return m_Tiles; }
		}

		/// <summary>
		/// Creates a new tile set
		/// </summary>
		public TileSet()
		{
			m_Tiles = new ArrayList();
		}

		/// <summary>
		/// Finds the ID of the tile corresponding to a given flag
		/// </summary>
		/// <param name="flags">The flag to search for</param>
		/// <returns>The ID of the corresponding tile</returns>
		public int FindID( uint flags )
		{
			foreach ( TileMask tile in m_Tiles )
			{
				if ( ( flags & ~tile.Flags ) == 0 )
				{
					return tile.ID;
				}
			}

			return 0;
		}

		/// <summary>
		/// Loads the roof tiles defined in rooftiles.cfg
		/// </summary>
		/// <returns>An array list of tilesets</returns>
		public static ArrayList Load()
		{
			ArrayList list = new ArrayList();

			StreamReader reader = new StreamReader( Pandora.DataAssembly.GetManifestResourceStream( "Data.rooftiles.cfg" ) );

			TileSet tileset = null;

			while ( reader.Peek() > -1 )
			{
				string line = reader.ReadLine();
				line.Trim();

				if ( line == null || line.Length == 0 || line.StartsWith( "#" ) )
				{
					continue;
				}

				if ( line.StartsWith( "[" ) )
				{
					line = line.Replace( "[", "" );
					line = line.Replace( "]", "" );

					tileset = new TileSet();
					tileset.m_Name = line;
					list.Add( tileset );

					continue;
				}

				string[] values = line.Split( ' ' );

				if ( values.Length == 2 )
				{
					uint flags = Convert.ToUInt32( values[ 0 ], 16 );
					int tile = Convert.ToInt32( values[ 1 ] );

					TileMask mask = new TileMask( flags, tile );
					tileset.m_Tiles.Add( mask );
				}
			}

			return list;
		}

		public override string ToString()
		{
			return m_Name;
		}
	}
}
