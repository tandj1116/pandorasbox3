using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml.Serialization;

namespace Box.Misc
{
	public class PB1Import
	{
		public static TheBox.Data.Facet Convert( string filename )
		{
			LocationsList single = TheBox.Common.Utility.LoadXml( typeof( LocationsList ), filename ) as LocationsList;
			CustomLocations cust = TheBox.Common.Utility.LoadXml( typeof( CustomLocations ), filename ) as CustomLocations;

			if ( single == null )
			{
				cust = TheBox.Common.Utility.LoadXml( typeof( CustomLocations ), filename ) as CustomLocations;

				if ( cust == null )
				{
					return null;
				}
			}

			if ( single != null )
			{
				return Convert( single );
			}
			else
			{
				PB1ImportForm form = new PB1ImportForm();
				form.ShowDialog();
				
				switch ( form.Map )
				{
					case 0 : return Convert( cust.Felucca );
					case 1 : return Convert( cust.Felucca );
					case 2 : return Convert( cust.Ilshenar );
					case 3 : return Convert( cust.Malas );
				}
			}

			return null;
		}

		private static TheBox.Data.Facet Convert( LocationsList list )
		{
			TheBox.Data.Facet f = new TheBox.Data.Facet();

			StringCollection categories = new StringCollection();

			foreach( Location loc in list.Places )
			{
				if ( ! categories.Contains( loc.Category ) )
				{
					categories.Add( loc.Category );
				}
			}

			foreach( string cat in categories )
			{
				TheBox.Common.GenericNode g = new TheBox.Common.GenericNode( cat );
				f.Nodes.Add( g );
			}

			foreach( TheBox.Common.GenericNode g in f.Nodes )
			{
				StringCollection sub = new StringCollection();

				foreach( Location loc in list.Places )
				{
					if ( loc.Category == g.Name && ! sub.Contains( loc.Subsection ) )
					{
						sub.Add( loc.Subsection );
					}
				}

				foreach( string s in sub )
				{
					TheBox.Common.GenericNode gSub = new TheBox.Common.GenericNode( s );
					g.Elements.Add( gSub );
				}
			}

			foreach( TheBox.Common.GenericNode gCat in f.Nodes )
			{
				foreach( TheBox.Common.GenericNode gSub in gCat.Elements )
				{
					foreach( Location loc in list.Places )
					{
						if ( loc.Category == gCat.Name && loc.Subsection == gSub.Name )
						{
							gSub.Elements.Add( Convert( loc ) );
						}
					}
				}
			}

			return f;
		}

		private static TheBox.Data.Location Convert( Location loc )
		{
			TheBox.Data.Location newLoc = new TheBox.Data.Location();

			newLoc.Name = loc.Name;
			newLoc.Map = loc.Map;
			newLoc.X = loc.x;
			newLoc.Y = loc.y;
			newLoc.Z = (sbyte) loc.z;

			return newLoc;
		}
	}

	[XmlInclude (typeof(LocationsList) )]
	[XmlInclude (typeof(Location))]
	public class CustomLocations
	{
		public LocationsList Felucca;
		public LocationsList Ilshenar;
		public LocationsList Malas;

		public CustomLocations()
		{
			Felucca = new LocationsList();
			Ilshenar = new LocationsList();
			Malas = new LocationsList();
		}
	}
	/// <summary>
	/// Summary description for LocationsList.
	/// </summary>
	[XmlInclude (typeof(Location))]
	public class LocationsList
	{
		public ArrayList Places;
		public LocationsList()
		{
			Places = new ArrayList();
		}
	}
	/// <summary>
	/// Summary description for Location.
	/// </summary>
	public class Location : IComparable
	{
		public short ID;
		public string Name;
		public string Category;
		public string Subsection;
		public short Map;
		public short x;
		public short y;
		public short z;
		public bool Custom;

		public Location()
		{
		}

		public int CompareTo( object obj )
		{
			if ( obj is Location )
			{
				Location loc = (Location) obj;

				if ( loc.Map != this.Map )
					return this.Map.CompareTo( loc.Map );
				if ( this.Category != loc.Category )
					return this.Category.CompareTo( loc.Category );
				if ( this.Subsection != loc.Subsection )
					return this.Subsection.CompareTo( loc.Subsection );
				return this.Name.CompareTo( loc.Name );
			}
			throw new Exception("Not a Location: " + obj.ToString() );
		}

		public void MakePoint(string point)
		{
			string X = "";
			string Y = "";
			string Z = "";

			while ( point[0] != ',' )
			{
				X += point[0];
				point = point.Substring( 1, point.Length - 1 );
			}

			point = point.Substring( 1, point.Length - 1 );

			while ( point[0] != ',' )
			{
				Y += point[0];
				point = point.Substring( 1, point.Length - 1 );
			}
			
			point = point.Substring( 1, point.Length - 1 );
			Z = point;

			x = System.Convert.ToInt16(X);
			y = System.Convert.ToInt16(Y);
			z = System.Convert.ToInt16(Z);
		}
	}
}
