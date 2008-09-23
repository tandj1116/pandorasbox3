using System;
using System.IO;

namespace TheBox.Buttons
{
	/// <summary>
	/// Summary description for ButtonID.
	/// </summary>
	internal class ButtonIDs
	{
		private static int m_Current = 0;

		private static string m_FileName = @"D:\Dev\Pandora 2.0\Data\ButtonID.txt";

		private static bool m_FileOpen = false;

		/// <summary>
		/// Gets the next unique Button ID
		/// </summary>
		public static int NextID
		{
			get
			{
				if ( m_FileOpen )
				{
					m_Current++;
					Save();
				}
				else
				{
					Load();
					m_Current++;
					Save();
				}
				return m_Current;
			}
		}

		private static void Save()
		{
			StreamWriter writer = new StreamWriter( m_FileName, false );
			writer.WriteLine( m_Current.ToString() );
			writer.Close();
		}

		private static void Load()
		{
			if ( File.Exists( m_FileName ) )
			{
				StreamReader reader = new StreamReader( m_FileName );
				m_Current = Convert.ToInt32( reader.ReadLine() );
				reader.Close();
			}

			m_FileOpen = true;
		}
	}
}