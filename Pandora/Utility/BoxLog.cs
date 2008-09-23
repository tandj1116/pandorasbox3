using System;
using System.IO;
using System.Runtime.InteropServices;

namespace TheBox.Common
{
	/// <summary>
	/// Provides logging functionality for Pandora's Box
	/// </summary>
	public class BoxLog
	{
		#region Imports

		//Struct to retrive system info
		[StructLayout(LayoutKind.Sequential)]   
		private struct SYSTEM_INFO 
		{
			public  uint  dwOemId;
			public  uint  dwPageSize;
			public  uint  lpMinimumApplicationAddress;
			public	uint  lpMaximumApplicationAddress;
			public  uint  dwActiveProcessorMask;
			public  uint  dwNumberOfProcessors;
			public  uint  dwProcessorType;
			public  uint  dwAllocationGranularity;
			public  uint  dwProcessorLevel;
			public  uint  dwProcessorRevision;		
		}
		//struct to retrive memory status
		[StructLayout(LayoutKind.Sequential)]   
		private struct MEMORYSTATUS
		{
			public  uint dwLength;
			public  uint dwMemoryLoad;
			public  uint dwTotalPhys;
			public  uint dwAvailPhys;
			public  uint dwTotalPageFile;
			public  uint dwAvailPageFile;
			public  uint dwTotalVirtual;
			public  uint dwAvailVirtual;
		}

		// Constants used for processor types
		private const int PROCESSOR_INTEL_386 = 386;
		private const int PROCESSOR_INTEL_486 = 486;
		private const int PROCESSOR_INTEL_PENTIUM = 586;
		private const int PROCESSOR_MIPS_R4000 = 4000;
		private const int PROCESSOR_ALPHA_21064 = 21064;

		//To get system information
		[DllImport("kernel32")]
		static extern void GetSystemInfo(ref SYSTEM_INFO pSI);
		
		//To get Memory status
		[DllImport("kernel32")]
		static extern void GlobalMemoryStatus(ref MEMORYSTATUS buf);

		#endregion

		private StreamWriter m_Stream;

		public BoxLog( string filename )
		{
			string folder = Path.GetDirectoryName( filename );

			// Ensure directory
			if ( !Directory.Exists( folder ) )
				Directory.CreateDirectory( folder );

			try
			{
				m_Stream = new StreamWriter( filename, false );
			}
			catch
			{
				m_Stream = null;
				return;
			}

			m_Stream.WriteLine( "Pandora's Box - Log" );
			m_Stream.WriteLine( string.Format( "Pandora version {0}", Pandora.Version ) );
			m_Stream.WriteLine( "" );
			m_Stream.WriteLine( DateTime.Now.ToString() );
			m_Stream.WriteLine( "Windows version: " + Environment.OSVersion.Version.ToString() );

			// System info
			SYSTEM_INFO pSI = new SYSTEM_INFO();
			GetSystemInfo(ref pSI);
			string CPUType;
			switch (pSI.dwProcessorType)
			{				
				case PROCESSOR_INTEL_386	:
					CPUType= "Intel 386";
					break;
				case PROCESSOR_INTEL_486	:
					CPUType = "Intel 486" ;
					break;
				case PROCESSOR_INTEL_PENTIUM	:
					CPUType =  "Intel Pentium";
					break;
				case PROCESSOR_MIPS_R4000	:
					CPUType =  "MIPS R4000";
					break;
				case PROCESSOR_ALPHA_21064	:
					CPUType =  "DEC Alpha 21064";
					break;
				default :
					CPUType = "(unknown)";
					break;
			}
			m_Stream.WriteLine("Processor family: " + CPUType);

			MEMORYSTATUS memSt = new MEMORYSTATUS ();				
			GlobalMemoryStatus (ref memSt);
			m_Stream.WriteLine( "Physical memory: " + (memSt.dwTotalPhys/1024).ToString() );
			m_Stream.WriteLine();
		}

		private string CurrentTime
		{
			get
			{
				return string.Format( "[{0}:{1}:{2}]", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second );
			}
		}

		public void WriteEntry( string text )
		{
			if ( m_Stream != null )
			{
				m_Stream.WriteLine( string.Format( "{0} {1}", CurrentTime, text ) );
				m_Stream.Flush();
			}
		}

		public void WriteEntry( string format, params object[] args )
		{
			WriteEntry( string.Format( format, args ) );
		}

		public void WriteError( Exception error, string additionalInfo )
		{
			if ( m_Stream != null )
			{
				WriteEntry( "**** ERROR ****" );

				if ( error != null )
					m_Stream.WriteLine( error.ToString() );

				if ( additionalInfo != null )
					m_Stream.WriteLine( string.Format( "Additional information: {0}", additionalInfo ) );

				m_Stream.Flush();
			}
		}

		public void WriteError( Exception error, string format, params object[] args )
		{
			WriteError( error, string.Format( format, args ) );
		}

		public void Close()
		{
			if ( m_Stream != null )
			{
				WriteEntry( "Session closed" );
				m_Stream.Close();
				m_Stream = null;
			}
		}
	}
}