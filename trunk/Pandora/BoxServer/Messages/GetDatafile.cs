using System;
using TheBox.Data;

namespace TheBox.BoxServer
{
	/// <summary>
	/// Defines the datafiles that can be retrieved from the server
	/// </summary>
	public enum BoxDatafile
	{
		BoxData,
		PropsData,
		SpawnData
	}

	[ Serializable ]
	/// <summary>
	/// This message is used to retrieve a datafile from the server
	/// </summary>
	public class GetDatafile : BoxMessage
	{
		/// <summary>
		/// Creates a new GetPropsData message
		/// </summary>
		public GetDatafile()
		{
		}

		private BoxDatafile m_DataType;

		/// <summary>
		/// Gets or sets the datafile type to retrieve
		/// </summary>
		public BoxDatafile DataType
		{
			get { return m_DataType; }
			set { m_DataType = value; }
		}
	}
}
