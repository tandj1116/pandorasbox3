using System;
using System.Collections.Specialized;

using TheBox.Common;

namespace TheBox.Options
{
	/// <summary>
	/// Provides options for properties
	/// </summary>
	public class PropsOptions
	{
		public PropsOptions()
		{
			m_RecentProps = new RecentStringList();
			m_RecentValues = new RecentStringList();
			m_RecentTypes = new RecentStringList();
			m_RecentClasses = new RecentStringList();
			m_FBTypes = new RecentStringList();
			m_FBProps = new RecentStringList();
			m_FBValues = new RecentStringList();

			m_Filters = new StringCollection();
		}

		private TheBox.Common.RecentStringList m_RecentProps;
		private TheBox.Common.RecentStringList m_RecentValues;
		private TheBox.Common.RecentStringList m_RecentTypes;
		private RecentStringList m_FBTypes;
		private RecentStringList m_FBProps;
		private RecentStringList m_FBValues;
		private bool m_UseType = false;
		private StringCollection m_Filters;
		
		// Props Data
		private bool m_ShowAllTypes = false;
		private bool m_ShowAllProps = false;
		private TheBox.Data.AccessLevel m_Filter = TheBox.Data.AccessLevel.Administrator;
		private TheBox.Common.RecentStringList m_RecentClasses = null;

		// TimeSpan
		private int m_Days;
		private int m_Hours;
		private int m_Minutes;
		private int m_Seconds;

		// Point3D
		private int m_PointX;
		private int m_PointY;
		private int m_PointZ;

		/// <summary>
		/// Gets or sets the list of recently used properties
		/// </summary>
		public RecentStringList RecentProps
		{
			get { return m_RecentProps; }
			set { m_RecentProps = value; }
		}

		/// <summary>
		/// Gets or sets the list of recently used values
		/// </summary>
		public RecentStringList RecentValues
		{
			get { return m_RecentValues; }
			set { m_RecentValues = value; }
		}

		/// <summary>
		/// Gets or sets the list of recently used types to limit a property setting
		/// </summary>
		public RecentStringList RecentTypes
		{
			get { return m_RecentTypes; }
			set { m_RecentTypes = value; }
		}

		/// <summary>
		/// Gets or sets a value stating whether a gSet would use the type to limit its targets
		/// </summary>
		public bool UseType
		{
			get { return m_UseType; }
			set { m_UseType = value; }
		}

		/// <summary>
		/// Specifies whether the classes tree should display types which have no declared properties
		/// </summary>
		public bool ShowAllTypes
		{
			get { return m_ShowAllTypes; }
			set { m_ShowAllTypes = value; }
		}

		/// <summary>
		/// Specifies whether each class will display all properties, included the ones inherited
		/// </summary>
		public bool ShowAllProps
		{
			get { return m_ShowAllProps; }
			set { m_ShowAllProps = value; }
		}

		/// <summary>
		/// States how to fileter the properties displayed on the properties tree
		/// </summary>
		public TheBox.Data.AccessLevel Filter
		{
			get { return m_Filter; }
			set { m_Filter = value; }
		}

		/// <summary>
		/// Gets or sets the recently searched classes
		/// </summary>
		public RecentStringList RecentClasses
		{
			get { return m_RecentClasses; }
			set { m_RecentClasses = value; }
		}

		/// <summary>
		/// Gets or sets the number of days for the TimeSpan control
		/// </summary>
		public int Days
		{
			get { return m_Days; }
			set { m_Days = value; }
		}

		/// <summary>
		/// Gets or sets the number of hours for the TimeSpan control
		/// </summary>
		public int Hours
		{
			get { return m_Hours; }
			set { m_Hours = value; }
		}

		/// <summary>
		/// Gets or sets the number of minutes for the TimeSpan control
		/// </summary>
		public int Minutes
		{
			get { return m_Minutes; }
			set { m_Minutes = value; }
		}

		/// <summary>
		/// Gets or sets the number of seconds for the TimeSpan control
		/// </summary>
		public int Seconds
		{
			get { return m_Seconds; }
			set { m_Seconds = value; }
		}

		/// <summary>
		/// Gets or sets the value of the X coordinate on the Point3D control
		/// </summary>
		public int PointX
		{
			get { return m_PointX; }
			set { m_PointX = value; }
		}

		/// <summary>
		/// Gets or sets the value of the Y coordinate on the Point3D control
		/// </summary>
		public int PointY
		{
			get { return m_PointY; }
			set { m_PointY = value; }
		}

		/// <summary>
		/// Gets or sets the value of the Z coordinate on the Point3D control
		/// </summary>
		public int PointZ
		{
			get { return m_PointZ; }
			set { m_PointZ = value; }
		}

		/// <summary>
		/// Gets or sets the list of recently used types in the FB
		/// </summary>
		public RecentStringList FBTypes
		{
			get { return m_FBTypes; }
			set { m_FBTypes = value; }
		}

		/// <summary>
		/// Gets or sets the list of recently used props in the FB
		/// </summary>
		public RecentStringList FBProps
		{
			get { return m_FBProps; }
			set { m_FBProps = value; }
		}

		/// <summary>
		/// Gets or sets the list of recently used values in the FB
		/// </summary>
		public RecentStringList FBValues
		{
			get { return m_FBValues; }
			set { m_FBValues = value; }
		}

		/// <summary>
		/// Gets or sets the preset filters
		/// </summary>
		public StringCollection Filters
		{
			get { return m_Filters; }
			set { m_Filters = value; }
		}
	}
}