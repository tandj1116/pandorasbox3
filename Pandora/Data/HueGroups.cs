using System;
using System.Collections;
using System.Xml.Serialization;

namespace TheBox.Data
{
	/// <summary>
	/// Describes the hue groups defined for a profile
	/// </summary>
	[ Serializable, XmlInclude( typeof( HuesCollection ) ) ]
	public class HueGroups
	{
		private ArrayList m_Groups;

		/// <summary>
		/// Gets or sets the list of groups
		/// </summary>
		public ArrayList Groups
		{
			get { return m_Groups; }
			set { m_Groups = value; }
		}

		public HueGroups()
		{
			m_Groups = new ArrayList();
		}

		/// <summary>
		/// Loads the hue groups
		/// </summary>
		/// <returns>A HueGroups object</returns>
		public static HueGroups Load()
		{
			string filename = System.IO.Path.Combine( Pandora.Profile.BaseFolder, "HueGroups.xml" );
			return TheBox.Common.Utility.LoadXml( typeof( HueGroups ), filename ) as HueGroups;
		}

		/// <summary>
		/// Saves the hue groups to file
		/// </summary>
		public void Save()
		{
			string filename = System.IO.Path.Combine( Pandora.Profile.BaseFolder, "HueGroups.xml" );
			TheBox.Common.Utility.SaveXml( this, filename );
		}
	}

	[ Serializable ]
	/// <summary>
	/// Defines a group of hues
	/// </summary>
	public class HuesCollection
	{
		private string m_Name;
		private ArrayList m_Hues;

		/// <summary>
		/// Gets or sets the name of this group
		/// </summary>
		[ XmlAttribute ]
		public string Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}

		/// <summary>
		/// Gets or sets the list of hues
		/// </summary>
		public ArrayList Hues
		{
			get { return m_Hues; }
			set { m_Hues = value; }
		}

		public HuesCollection()
		{
			m_Hues = new ArrayList();
		}

		public override string ToString()
		{
			return m_Name;
		}
	}
}
