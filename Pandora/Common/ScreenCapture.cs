using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace TheBox.Common
{
	/// <summary>
	/// Provides screen capturing functions for Pandora's Box
	/// </summary>
	public class ScreenCapture
	{
		[ DllImport( "BoxCapture.dll" ) ]
		private static extern IntPtr CaptureWindow( int handle );

		[DllImport("Gdi32.dll")]
		private static extern uint DeleteObject(IntPtr hGdiObj);

		[DllImport("User32.dll")]
		private static extern bool RedrawWindow( IntPtr handle, IntPtr rect, IntPtr range, uint flags );

		private const uint RDW_INVALIDATE = 0x0001;

		/// <summary>
		/// Captures a screenshot of the UO window
		/// </summary>
		/// <returns>An Image object containing the screenshot, null if failed</returns>
		public static Image Capture()
		{
			IntPtr handle = TheBox.Common.Utility.GetClientWindow();

			if ( handle.ToInt32() == 0 )
			{
				return null; // Client not running
			}

			TheBox.Common.Utility.BringClientToFront();
			RedrawWindow( handle, IntPtr.Zero, IntPtr.Zero, RDW_INVALIDATE );

			if ( Pandora.Profile.General.TopMost )
			{
				Pandora.BoxForm.Visible = false;
			}

			Image img = null;

			IntPtr ptr = IntPtr.Zero;

			// Give the client time to refresh
			System.Threading.Thread.Sleep( 250 );

			try
			{
				ptr = CaptureWindow( handle.ToInt32() );
				img = Image.FromHbitmap( ptr );
			}
			catch ( Exception err )
			{
				Pandora.Log.WriteError( err, "The error occurred when trying to take a screenshot" );
				img = null;
			}

			if ( Pandora.Profile.General.TopMost )
			{
				Pandora.BoxForm.Visible = true;
			}

			DeleteObject( ptr );
			return img;
		}
	}
}