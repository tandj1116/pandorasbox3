using System;

namespace TheBox.Options
{
	/// <summary>
	/// Contains options relating to Mobiles
	/// </summary>
	public class MobilesOptions
	{
		/// <summary>
		/// Creates a new MobilesOptions object
		/// </summary>
		public MobilesOptions()
		{
			m_RecentNames = new TheBox.Common.RecentStringList();
		}

		private int m_ArtIndex = 0;
		private int m_Amount = 1;
		private int m_Range = 1;
		private int m_MinDelay = 5;
		private int m_MaxDelay = 10;
		private int m_Team = 0;
		private int m_Extra = 0;
		private TheBox.Common.RecentStringList m_RecentNames;
		private bool m_NameMount = false;

		/// <summary>
		/// Gets or sets the value representing the selected art for Mobiles
		/// </summary>
		public int ArtIndex
		{
			get { return m_ArtIndex; }
			set { m_ArtIndex = value; }
		}

		/// <summary>
		/// Gets or sets the spawn amount
		/// </summary>
		public int Amount
		{
			get { return m_Amount; }
			set { m_Amount = value; }
		}

		/// <summary>
		/// Gets or sets the spawn range
		/// </summary>
		public int Range
		{
			get { return m_Range; }
			set { m_Range = value; }
		}

		/// <summary>
		/// Gets or sets the min delay for the spawn
		/// </summary>
		public int MinDelay
		{
			get { return m_MinDelay; }
			set { m_MinDelay = value; }
		}

		/// <summary>
		/// Gets or sets the max delay for the spawn
		/// </summary>
		public int MaxDelay
		{
			get { return m_MaxDelay; }
			set { m_MaxDelay = value; }
		}

		/// <summary>
		/// Gets or sets the spawn team
		/// </summary>
		public int Team
		{
			get { return m_Team; }
			set { m_Team = value; }
		}

		/// <summary>
		/// Gets or sets the additional extra property for spawns
		/// </summary>
		public int Extra
		{
			get { return m_Extra; }
			set { m_Extra = value; }
		}

		/// <summary>
		/// Gets or sets the list of recently used names
		/// </summary>
		public TheBox.Common.RecentStringList RecentNames
		{
			get { return m_RecentNames; }
			set { m_RecentNames = value; }
		}

		/// <summary>
		/// Gets or sets a value stating whether to name mounts when adding them
		/// </summary>
		public bool NameMount
		{
			get { return m_NameMount; }
			set { m_NameMount = value; }
		}
	}
}
