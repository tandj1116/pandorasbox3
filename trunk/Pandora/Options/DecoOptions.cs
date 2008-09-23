using System;

namespace TheBox.Options
{
	/// <summary>
	/// Provides options for the decoration tab
	/// </summary>
	public class DecoOptions
	{
		/// <summary>
		/// Creates a new Deco Options
		/// </summary>
		public DecoOptions()
		{
		}

		#region Variables

		private bool m_ShowCustomDeco = false;
		private int m_TileHeight = 0;
		private bool m_Static = true;
		private bool m_Hued = false;
		private int m_MoveAmount = 1;
		private bool m_MassMove = false;
		private int m_ArtIndex = 0;
		private int m_NudgeAmount = 1;
		private string m_Light = "Circle225";
		private bool m_UseRandomizer = false;
		private int m_RandomAmount = 5;

		#endregion

		#region Events

		/// <summary>
		/// Occurs when the ShowCustomDeco value has been changed
		/// </summary>
		public event EventHandler ShowCustomDecoChanged;

		#endregion

		/// <summary>
		/// Gets or sets the light set when adding deco items
		/// </summary>
		public string Light
		{
			get { return m_Light; }
			set { m_Light = value; }
		}

		/// <summary>
		/// Gets or sets the art index currently displayed on the deco tab
		/// </summary>
		public int ArtIndex
		{
			get { return m_ArtIndex; }
			set { m_ArtIndex = value; }
		}

		/// <summary>
		/// States whether the mover should mass move items
		/// </summary>
		public bool MassMove
		{
			get { return m_MassMove; }
			set { m_MassMove = value; }
		}

		/// <summary>
		/// Gets or sets the move amount
		/// </summary>
		public int MoveAmount
		{
			get { return m_MoveAmount; }
			set { m_MoveAmount = value; }
		}

		/// <summary>
		/// States whether tiles should be added hued
		/// </summary>
		public bool Hued
		{
			get { return m_Hued; }
			set { m_Hued = value; }
		}
		
		/// <summary>
		/// States whether deco items should be created not movable
		/// </summary>
		public bool Static
		{
			get { return m_Static; }
			set { m_Static = value; }
		}

		/// <summary>
		/// Gets or sets the current height at which to tile
		/// </summary>
		public int TileHeight
		{
			get { return m_TileHeight; }
			set { m_TileHeight = value; }
		}

		/// <summary>
		/// Gets or sets the amount for the nudge command
		/// </summary>
		public int NudgeAmount
		{
			get { return m_NudgeAmount; }
			set { m_NudgeAmount = value; }
		}

		/// <summary>
		/// States whether to use the randomization constructor
		/// </summary>
		public bool UseRandomizer
		{
			get { return m_UseRandomizer; }
			set { m_UseRandomizer = value; }
		}

		/// <summary>
		/// Gets or set the randomization amount
		/// </summary>
		public int RandomizeAmount
		{
			get {	return m_RandomAmount;}
			set { m_RandomAmount = value; }
		}

		/// <summary>
		/// States whether the Custom Deco node should be displayed or not
		/// </summary>
		public bool ShowCustomDeco
		{
			get { return m_ShowCustomDeco; }
			set
			{
				m_ShowCustomDeco = value;

				if ( ShowCustomDecoChanged != null )
				{
					ShowCustomDecoChanged( this, new EventArgs() );
				}
			}
		}
	}
}
