using System;

using TheBox.Common;

namespace TheBox.Options
{
	/// <summary>
	/// Provides options for the Items tab
	/// </summary>
	public class ItemsOptions
	{
		private int m_Splitter = 0;
		private int m_ArtIndex = 0;
		private int m_ArtHue = 0;
		private bool m_UseOptions = false;
		private bool m_UseCustomParams = false;
		private RecentStringList m_CustomParams;
		private int m_Nudge = 0;
		private int m_Amount = 1;
		private int m_Range = 1;
		private int m_MinDelay = 5;
		private int m_MaxDelay = 10;
		private int m_Team = 0;
		private int m_Extra = 0;
		private int m_Tile = 0;

		// <summary>
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
		/// Gets or sets the ID of the art displayed by the viewer
		/// </summary>
		public int ArtIndex
		{
			get { return m_ArtIndex; }
			set { m_ArtIndex = value; }
		}

		/// <summary>
		/// Gets or sets the hue of the art displayed by the viewer
		/// </summary>
		public int ArtHue
		{
			get { return m_ArtHue; }
			set { m_ArtHue = value; }
		}

		/// <summary>
		/// Gets or sets the position of the splitter
		/// </summary>
		public int Splitter
		{
			get { return m_Splitter; }
			set { m_Splitter = value; }
		}

		/// <summary>
		/// States whether the user wishes to use an additional constructor when available
		/// </summary>
		public bool UseOptions
		{
			get { return m_UseOptions; }
			set { m_UseOptions = value; }
		}

		/// <summary>
		/// States whether the use custom parameters checkbox is checked
		/// </summary>
		public bool UseCustomParams
		{
			get { return m_UseCustomParams; }
			set { m_UseCustomParams = value; }
		}

		/// <summary>
		/// Gets or sets the recently used custom parameters
		/// </summary>
		public RecentStringList CustomParams
		{
			get { return m_CustomParams; } 
			set { m_CustomParams = value; }
		}

		/// <summary>
		/// Gets or sets the nudge amount displayed by the nudge numeric up and down
		/// </summary>
		public int Nudge
		{
			get { return m_Nudge; }
			set
			{
				m_Nudge = Utility.ValidateNumber( value, 0, 127 );
			}
		}

		/// <summary>
		/// Gets or sets the items tile height
		/// </summary>
		public int Tile
		{
			get { return m_Tile; }
			set
			{
				m_Tile = Utility.ValidateNumber( value, -128, 127 );
			}
		}

		/// <summary>
		/// Creates a new ItemsOptions object
		/// </summary>
		public ItemsOptions()
		{
			m_CustomParams = new RecentStringList();
		}
	}
}
