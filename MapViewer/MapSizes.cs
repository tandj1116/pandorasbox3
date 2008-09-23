using System;
using System.Drawing;

namespace TheBox.MapViewer
{
	/// <summary>
	/// Provides the sizes of the UO maps
	/// </summary>
	public class MapSizes
	{
		/// <summary>
		/// Gets the size of the Felucca map
		/// </summary>
		public static Size Felucca
		{
			get { return new Size( 6144, 4096 ); }
		}

		/// <summary>
		/// Gets the size of the Trammel map
		/// </summary>
		public static Size Trammel
		{
			get { return new Size( 6144, 4096 ); }
		}

		/// <summary>
		/// Gets the size of the Ilshenar map
		/// </summary>
		public static Size Ilshenar
		{
			get { return new Size( 2304, 1600 ); }
		}

		/// <summary>
		/// Gets the size of the Malas map
		/// </summary>
		public static Size Malas
		{
			get { return new Size( 2560, 2048 ); }
		}

		/// <summary>
		/// Gets the size of the Tokuno islands map
		/// </summary>
		public static Size Tokuno
		{
			get { return new Size( 1448, 1448 ); }
		}

		/// <summary>
		/// Gets the size of a map
		/// </summary>
		/// <param name="mapfile">The index of the map</param>
		/// <returns>A Size object representing the size of the map</returns>
		public static Size GetSize( int mapfile )
		{
			switch ( mapfile )
			{
				case 0:
				case 1:

					return MapSizes.Felucca;

				case 2:

					return MapSizes.Ilshenar;

				case 3:

					return MapSizes.Malas;

				case 4:

					return MapSizes.Tokuno;
			}

			throw new Exception( string.Format( "Map file {0} not supported", mapfile ) );
		}
	}
}
