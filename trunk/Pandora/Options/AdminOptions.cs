using System;

using TheBox.Common;

namespace TheBox.Options
{
	/// <summary>
	/// Provides administration options
	/// </summary>
	public class AdminOptions
	{
		/// <summary>
		/// Creates a new AdminOptions object
		/// </summary>
		public AdminOptions()
		{
			m_FindByName = new RecentStringList();
			m_Args = new RecentStringList();
		}

		private RecentStringList m_FindByName;
		private string m_ServerExe = null;
		private int m_Console = 0;
		private string m_ConsoleTitle = null;
		private bool m_ConsoleHidden = false;
		private RecentStringList m_Args;

		/// <summary>
		/// Gets or sets the recent searches
		/// </summary>
		public RecentStringList FindByName
		{
			get { return m_FindByName; }
			set { m_FindByName = value; }
		}

		/// <summary>
		/// Gets or sets the Server executable file
		/// </summary>
		public string ServerExe
		{
			get { return m_ServerExe; }
			set { m_ServerExe = value; }
		}
      
		/// <summary>
		/// Gets or sets the
		/// </summary>
		public int Console
		{
			get { return m_Console; }
			set { m_Console = value; }
		}

		/// <summary>
		/// Gets or sets the title of the console window
		/// </summary>
		public string ConsoleTitle
		{
			get { return m_ConsoleTitle; }
			set { m_ConsoleTitle = value; }
		}

		/// <summary>
		/// States whether the console has been hidden
		/// </summary>
		public bool ConsoleHidden
		{
			get { return m_ConsoleHidden; }
			set { m_ConsoleHidden = value; }
		}

		/// <summary>
		/// Gets or sets the recent command line arguments
		/// </summary>
		public RecentStringList Args
		{
			get { return m_Args; }
			set { m_Args = value; }
		}
	}
}