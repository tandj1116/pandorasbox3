using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace TheBox.Options
{
	/// <summary>
	/// Summary description for ScreenshotOptions.
	/// </summary>
	public class ScreenshotOptions
	{
		private string m_BaseFolder;
		private string m_Name;

		[ XmlIgnore ]
		/// <summary>
		/// Gets or sets the base folder for the screenshots
		/// </summary>
		public string BaseFolder
		{
			get
			{
				if ( m_BaseFolder == null )
				{
					string path = Path.Combine( Pandora.Profile.BaseFolder, "Screenshots" );

					TheBox.Common.Utility.EnsureDirectory( path );

					return path;
				}

				TheBox.Common.Utility.EnsureDirectory( m_BaseFolder );

				return m_BaseFolder;
			}
			set { m_BaseFolder = value; }
		}

		[ XmlAttribute ]
		/// <summary>
		/// Gets or sets the custom folder
		/// </summary>
		public string CustomFolder
		{
			get { return m_BaseFolder; }
			set { m_BaseFolder = value; }
		}

		[ XmlAttribute ]
		/// <summary>
		/// Gets or sets the base name for the screenshots files
		/// </summary>
		public string Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}

		public ScreenshotOptions()
		{
		}

		/// <summary>
		/// Converts an integer to a string representation
		/// </summary>
		/// <param name="value">The integer to convert to string</param>
		/// <returns></returns>
		private string ConvertToString( int value )
		{
			int count = value / 10 + 1;

			if ( count > 3 )
				return value.ToString();

			System.Text.StringBuilder sb = new System.Text.StringBuilder( "0000", 4 );
			sb.Remove( 0, count );
			sb.Append( value );
			
			return sb.ToString();			
		}

		/// <summary>
		/// Gets the file name for the screenshot given the index
		/// </summary>
		/// <param name="index">The index of the screenshot</param>
		/// <returns>The file name for the speicified index</returns>
		private string GetFileName( int index )
		{
			return string.Format( "{0}{1}.jpg", m_Name, ConvertToString( index ) );
		}

		/// <summary>
		/// Gets the next available screenshot name
		/// </summary>
		private string NextScreenshotName
		{
			get
			{
				System.Collections.Specialized.StringCollection files = new System.Collections.Specialized.StringCollection();
				files.AddRange( Directory.GetFiles( BaseFolder, string.Format( "{0}*.jpg", m_Name ) ) );

				int index = 1;

				while ( files.Contains( Path.Combine( BaseFolder, GetFileName( index ) ) ) )
				{
					if ( index < int.MaxValue )
					{
						index++;
					}
					else
					{
						Pandora.Log.WriteError( null, "Too many screenshots" );
						return "DeleteSomeScreenshotsSilly.jpg";
					}
				}

				return GetFileName( index );
			}
		}

		/// <summary>
		/// Captures a screenshot of the UO window
		/// </summary>
		public void Capture()
		{
			Image img = TheBox.Common.ScreenCapture.Capture();

			if ( img == null )
			{
				Pandora.Log.WriteEntry( "Screenshot attempt failed" );
				return;
			}

			string file = Path.Combine( BaseFolder, NextScreenshotName );

			try
			{
				img.Save( file, System.Drawing.Imaging.ImageFormat.Jpeg );
			}
			catch ( Exception err )
			{
				Pandora.Log.WriteError( err, "Couldn't save a screenshot to disk" );
			}

			img.Dispose();
		}
	}
}