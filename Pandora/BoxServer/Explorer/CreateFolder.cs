using System;

namespace TheBox.BoxServer
{
	[ Serializable ]
	/// <summary>
	/// Creates a new folder on the server
	/// </summary>
	public class CreateFolder : ExplorerMessage
	{
		private string m_Folder;

		/// <summary>
		/// Gets or sets the name of the new folder
		/// </summary>
		public string Folder
		{
			get { return m_Folder; }
			set { m_Folder = value; }
		}

		/// <summary>
		/// Creates a new CreateFolder message
		/// </summary>
		public CreateFolder()
		{
		}
	}
}
