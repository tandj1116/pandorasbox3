using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace TheBox.Editors
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Starter
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.EnableVisualStyles();
			Application.Run(new TravelEditor());
		}
	}
}
