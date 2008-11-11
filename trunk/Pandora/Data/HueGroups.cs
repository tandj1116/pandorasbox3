using System;
// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
using System.Collections.Generic;
// Issue 10 - End
using System.Xml.Serialization;

namespace TheBox.Data
{
	/// <summary>
	/// Describes the hue groups defined for a profile
	/// </summary>
	[ Serializable, XmlInclude( typeof( HuesCollection ) ) ]
	public class HueGroups
	{
		// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
		private List<HuesCollection> m_Groups;
		// Issue 10 - End

		/// <summary>
		/// Gets or sets the list of groups
		/// </summary>
		// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
		public List<HuesCollection> Groups
		// Issue 10 - End
		{
			get { return m_Groups; }
			set { m_Groups = value; }
		}

		public HueGroups()
		{
			// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
			m_Groups = new List<HuesCollection>();
			// Issue 10 - End
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
		// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
		private List<int> m_Hues;
		// Issue 10 - End

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
		// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
		public List<int> Hues
		// Issue 10 - End
		{
			get { return m_Hues; }
			set { m_Hues = value; }
		}

		public HuesCollection()
		{
			// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
			m_Hues = new List<int>();
			// Issue 10 - End
		}

		public override string ToString()
		{
			return m_Name;
		}
	}
}
