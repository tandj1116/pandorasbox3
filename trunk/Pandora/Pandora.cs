using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Reflection;
using System.Collections.Specialized;
using System.Xml.Serialization;
using System.Diagnostics;

using TheBox.Common;
using TheBox.MapViewer;
using TheBox.Options;
using TheBox.Forms;
using TheBox.Forms.ProfileWizard;
using TheBox.Buttons;
using TheBox.Data;
using TheBox.BoxServer;
using TheBox.Localization;

namespace TheBox
{
	public class Pandora
	{
		#region Log

		/// <summary>
		/// Gets the Log provider for Pandora's Box
		/// </summary>
		public static BoxLog Log
		{
			get
			{
				if (m_Log == null)
				{
					m_Log = new BoxLog(Path.Combine(Pandora.ApplicationDataFolder, "Log.txt"));
				}

				return m_Log;
			}
		}
		#endregion

		#region Map, Art, Hues, Props (Data & GUI)

		/// <summary>
		/// Gets or sets the Property manager panel
		/// </summary>
		public static TheBox.Controls.PropManager Prop
		{
			set
			{
				m_Prop = value;
			}
			get
			{
				if (m_Prop == null)
				{
					throw new System.NullReferenceException("Trying to access static field Pandora.Prop without initlizing it first.");
				}
				else
				{
					return m_Prop;
				}
			}
		}
		/// <summary>
		/// Gets the MapViewer control used to display the map in Pandora's Box
		/// </summary>
		public static TheBox.MapViewer.MapViewer Map
		{
			set
			{
				m_Map = value;
			}
			get
			{
				if (m_Map == null)
				{
					throw new System.NullReferenceException("Trying to access the static field Pandora.Map without initializing it first.");
				}
				else
				{
					return m_Map;
				}
			}
		}


        // Issue 31:  	 Pandora.Art exception on null - Tarion
        /// <summary>
        /// To check if the Art property is null
        /// </summary>
        public static bool ArtLoaded
        {
            get
            {
                return (m_Art != null);
            }
        }

		/// <summary>
		/// Gets or sets the ArtViewer control used to display the art in Pandora's Box
		/// </summary>
		public static TheBox.ArtViewer.ArtViewer Art
		{
			set
			{
				m_Art = value;
			}
			get
			{
				if (m_Art == null)
				{
					throw new System.NullReferenceException("Trying to access the static field Pandora.Art without initializing it first.");
				}
				else
				{
					return m_Art;
				}
			}
		}

		/// <summary>
		/// Gets the loaded hues
		/// </summary>
		public static TheBox.Mul.Hues Hues
		{
			get
			{
				if (m_Hues == null)
				{
					if (Pandora.Profile.MulManager["hues.mul"] != null)
						m_Hues = TheBox.Mul.Hues.Load(Pandora.Profile.MulManager["hues.mul"]);
				}

				return m_Hues;
			}
		}

		#endregion

		#region Localization

        public static LocalizationHelper Localization
        {
            get
            {
                if (m_Localization == null)
                {
                    m_Localization = new LocalizationHelper();
                }
                return m_Localization;
            }
        }

        
        #endregion

        #region ToolTip

        /// <summary>
        /// Gets the tool tip provider for this instance of Pandora
        /// </summary>
        public static ToolTip ToolTip
        {
            get
            {
                if (m_ToolTip == null)
                {
                    m_ToolTip = new ToolTip();

                    m_ToolTip.Active = true;
                    m_ToolTip.ShowAlways = true;
                }

                return m_ToolTip;
            }
        }

        #endregion


        #region Local Variables

        /// <summary>
		/// The log provider for Pandora
		/// </summary>
		private static BoxLog m_Log = null;

		/// <summary>
		/// The MapViewer control
		/// </summary>
		private static TheBox.MapViewer.MapViewer m_Map = null;

		/// <summary>
		/// The ArtViewer control
		/// </summary>
		private static TheBox.ArtViewer.ArtViewer m_Art = null;

        /// <summary>
        /// The localization provider
        /// </summary>
        private static LocalizationHelper m_Localization;

		/// <summary>
		/// The working folder for the program
		/// </summary>
		private static string m_Folder = null;

		/// <summary>
		/// The Box form
		/// </summary>
		private static Box m_TheBox;

		/// <summary>
		/// The loaded hues
		/// </summary>
		private static TheBox.Mul.Hues m_Hues;

		/// <summary>
		/// The data provider for travel locations
		/// </summary>
		private static TheBox.Data.TravelAgent m_TravelAgent;

		/// <summary>
		/// The data provider for custom buttons
		/// </summary>
		private static TheBox.Data.ButtonManager m_ButtonManager;

		/// <summary>
		/// The tool tips provider
		/// </summary>
		private static System.Windows.Forms.ToolTip m_ToolTip;

		/// <summary>
		/// The BoxData holding the information about scripted objects
		/// </summary>
		private static TheBox.Data.BoxData m_BoxData;

		/// <summary>
		/// The list of scripted mobiles
		/// </summary>
		private static TheBox.Data.ScriptList m_Mobiles;

		/// <summary>
		/// The list of scripted items
		/// </summary>
		private static TheBox.Data.ScriptList m_Items;

		/// <summary>
		/// The property panel
		/// </summary>
		private static TheBox.Controls.PropManager m_Prop;

		/// <summary>
		/// The spawn groups loaded by the profile
		/// </summary>
		private static SpawnGroups m_SpawnGroups;

		/// <summary>
		/// Specifies whether Pandora is connected to the BoxServer
		/// </summary>
		private static bool m_Connected = false;

		/// <summary>
		/// The context governing the application
		/// </summary>
		private static StartingContext m_Context;

		/// <summary>
		/// The assembly containing general purpose data
		/// </summary>
		private static Assembly m_DataAssembly = null;

		/// <summary>
		/// The form used to manipulate builder structures on the server
		/// </summary>
		private static TheBox.Forms.BuilderControl m_BuilderControl = null;


		#endregion

		#region Misc properties and methods

		/// <summary>
		/// Gets the instance of Pandora that is already running
		/// </summary>
		public static Process ExistingInstance
		{
			get
			{
                // Issue 6:  	 Improve error management - Tarion
                // Catch a possible exception here and return null.
                try
                {
				    Splash.SetStatusText("Searching existing instances");

				    Process current = Process.GetCurrentProcess();

				    Process[] processes = Process.GetProcessesByName(current.ProcessName);

				    //Loop through the running processes in with the same name 
				    foreach (Process process in processes)
				    {
					    //Ignore the current process 
					    if (process.Id != current.Id)
					    {
						    return process;
					    }
				    }
                }
                catch (Exception err)
                {
                    Pandora.Log.WriteError(err, "Error when enumerating instances");
                }

				return null;
			}
		}

		/// <summary>
		/// Gets the working folder for Pandora's Box
		/// </summary>
		public static string Folder
		{
			get
			{
				if (m_Folder == null)
				{
					string file = Environment.GetCommandLineArgs()[0];

					m_Folder = Path.GetDirectoryName(file);
				}

				return m_Folder;
			}
		}

		/// <summary>
		/// Gets the Box form
		/// </summary>
		public static Box BoxForm
		{
			get
			{
				return m_TheBox;
			}
            set
            {
                m_TheBox = value;
            }
		}

		/// <summary>
		/// States whether Pandora is connected to the BoxServer
		/// </summary>
		public static bool Connected
		{
			get { return m_Connected; }
			set
			{
				if (m_Connected != value)
				{
					m_Connected = value;

					if (m_TheBox != null)
					{
						m_TheBox.Text = string.Format(Pandora.Localization.TextProvider["Misc.BoxTitle"], Pandora.Profile.Name, Pandora.Connected ? Pandora.Localization.TextProvider["Misc.Online"] : Pandora.Localization.TextProvider["Misc.Offline"]);
					}

					if (OnlineChanged != null)
					{
						OnlineChanged(null, new EventArgs());
					}
				}
			}
		}

		/// <summary>
		/// Gets the version of this assembly
		/// </summary>
		public static string Version
		{
			get { return Application.ProductVersion; }
		}

		/// <summary>
		/// Sends text to UO automatically adding the \n at the end of the line
		/// </summary>
		/// <param name="text">The text that must be sent</param>
		/// <param name="UsePrefix">Specifies whether to send the command prefix in front of the text</param>
		// Issue 38:  	 Message when client not found - Tarion
		// Use SendToUO return value, and warn user if false
		// End Issue 38
		public static void SendToUO(string text, bool UsePrefix)
		{
			bool success = false;

			if (Profile != null)
			{
				if (UsePrefix)
				{
					success = Utility.SendToUO(string.Format("{0}{1}\r\n", Profile.General.CommandPrefix, text));
				}
				else
				{
					success = Utility.SendToUO(string.Format("{0}\r\n", text));
				}
			}

			if (!success)
			{
				MessageBox.Show("Client handle not found. If UO is running, try to set Tools -> Options -> Advanced -> Use a custom client");
			}

		}

		public static void ClosePandora()
		{
			Process process = Process.GetCurrentProcess();
			process.Kill();
		}

		/// <summary>
		/// Restarts Pandora's Box
		/// </summary>
		public static void Restart()
		{
			m_Context.MainForm = null;

			if (m_TheBox != null)
			{
				m_TheBox.Close();
				m_TheBox.Dispose();
			}

			m_Context.DoProfile();
		}

		/// <summary>
		/// Gets the assembly containing the program data
		/// </summary>
		public static Assembly DataAssembly
		{
			get
			{
				if (m_DataAssembly == null)
				{
					string filename = Path.Combine(Pandora.Folder, "Data");
					filename = Path.Combine(filename, "Data.dll");

					if (!File.Exists(filename))
					{
						Pandora.Log.WriteError(null, "Data file {0} doesn't exist", filename);
						throw new FileNotFoundException("A required file could not be found, please reinstall", filename);
					}

					m_DataAssembly = Assembly.LoadFile(filename);
				}

				return m_DataAssembly;
			}
		}

		/// <summary>
		/// Sends a message to the BoxServer
		/// </summary>
		/// <param name="message">The message that must be sent</param>
		/// <returns>The message outcome</returns>
		public static BoxMessage SendToServer(BoxMessage message)
		{
			if (!Pandora.Connected)
			{
				// Not connected, request connection
				if (MessageBox.Show(null, Pandora.Localization.TextProvider["Misc.RequestConnection"], "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					BoxServerForm form = new BoxServerForm(false);
					form.ShowDialog();
				}

				if (!Pandora.Connected)
				{
					return null;
				}
			}

			Pandora.Profile.Server.FillBoxMessage(message);

			BoxServerForm msgForm = new BoxServerForm(message);
			msgForm.ShowDialog();

			TheBox.Common.Utility.BringClientToFront();

			return msgForm.Response;
		}

		/// <summary>
		/// Shows the Builder Control form
		/// </summary>
		public static void ShowBuilderControl()
		{
			if (m_BuilderControl == null)
			{
				m_BuilderControl = new BuilderControl();
			}

			m_BuilderControl.Visible = true;
		}

		/// <summary>
		/// Gets the location of the PB2 application data folder
		/// </summary>
		public static string ApplicationDataFolder
		{
			get
			{
				// Issue 11 - Change profile directory - http://code.google.com/p/pandorasbox3/issues/detail?id=11 - Smjert
				string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Pandora's Box 3");
				// Issue 11 - End
				Utility.EnsureDirectory(folder);
				return folder;
			}
		}

		

		#endregion

		#region Profile managment

		/// <summary>
		/// Gets the profile currently loaded
		/// </summary>
		public static Profile Profile
		{
            // Workaround, there are over 640 refferences... - Tarion
			get { return ProfileManager.Instance.Profile; }
		}

        /// <summary>
        /// Closes Pandora's Box and creates a new profile
        /// </summary>
        public static void CreateNewProfile()
        {
            m_Context.MainForm = null;

            if (m_TheBox != null)
            {
                m_TheBox.Close();
                m_TheBox.Dispose();
            }

            m_Context.MakeNewProfile();
        }

        /// <summary>
        /// Closes the current application, deletes the current profile and restarts Pandora
        /// </summary>
        public static void DeleteCurrentProfile()
        {
            // Have to be refactored when we have a more global GUI handling - Tarion
            m_Context.MainForm = null;

            if (m_TheBox != null)
            {
                m_TheBox.Close();
                m_TheBox.Dispose();
            }

            ProfileManager.Instance.DeleteCurrentProfile();
            m_Context.DoProfile();
        }

		#endregion

		#region Data

		#region Travel Agent

		/// <summary>
		/// Gets or sets the travel data provider
		/// </summary>
		public static TheBox.Data.TravelAgent TravelAgent
		{
			get
			{
				if (m_TravelAgent == null)
				{
					Pandora.Log.WriteEntry("Creating Travel Agent");
					m_TravelAgent = new TheBox.Data.TravelAgent();
				}

				return m_TravelAgent;
			}
			set
			{
				m_TravelAgent = value;
			}
		}

		#endregion

		#region Button Manager

		/// <summary>
		/// Gets the data provider for custom buttons
		/// </summary>
		public static TheBox.Data.ButtonManager Buttons
		{
			get
			{
				if (m_ButtonManager == null)
					m_ButtonManager = new TheBox.Data.ButtonManager();

				return m_ButtonManager;
			}
		}

		#endregion

		#region BoxData, Mobiles and Items

		/// <summary>
		/// Gets or sets a new BoxData object
		/// </summary>
		public static BoxData BoxData
		{
			get
			{
				if (m_BoxData == null)
				{
					m_BoxData = TheBox.Data.BoxData.Load();
				}

				return m_BoxData;
			}
			set
			{
				m_BoxData = value;
				m_Mobiles = null;
				m_Items = null;
			}
		}

		/// <summary>
		/// Gets the object representing the scripted mobiles
		/// </summary>
		public static TheBox.Data.ScriptList Mobiles
		{
			get
			{
				if (m_BoxData == null)
				{
					m_BoxData = TheBox.Data.BoxData.Load();
				}

				if (m_Mobiles == null)
				{
					m_Mobiles = new TheBox.Data.ScriptList(m_BoxData.Mobiles);
					m_Mobiles.Saving += new EventHandler(m_Mobiles_Saving);
				}

				return m_Mobiles;
			}
			set
			{
				m_Mobiles = value;
			}
		}

		/// <summary>
		/// Handles the updating of the Mobiles list
		/// </summary>
		private static void m_Mobiles_Saving(object sender, EventArgs e)
		{
			m_BoxData.Mobiles = m_Mobiles.List;
			m_BoxData.Save();
		}

		/// <summary>
		/// Gets the object representing the scripted items
		/// </summary>
		public static TheBox.Data.ScriptList Items
		{
			get
			{
				if (m_BoxData == null)
				{
					m_BoxData = TheBox.Data.BoxData.Load();
				}

				if (m_Items == null)
				{
					m_Items = new TheBox.Data.ScriptList(m_BoxData.Items);
					m_Items.Saving += new EventHandler(m_Items_Saving);
				}

				return m_Items;
			}
			set
			{
				m_Items = value;
			}
		}

		/// <summary>
		/// Handles the updating of the Items list
		/// </summary>
		private static void m_Items_Saving(object sender, EventArgs e)
		{
			m_BoxData.Items = m_Items.List;
			m_BoxData.Save();
		}

		#endregion

		#region Spawn Groups

		/// <summary>
		/// Gets or sets the Spawn Groups for the current profile
		/// </summary>
		public static SpawnGroups SpawnGroups
		{
			get
			{
				if (m_SpawnGroups == null)
				{
					m_SpawnGroups = SpawnGroups.Load();
				}

				return m_SpawnGroups;
			}
			set
			{
				m_SpawnGroups = value;
				m_SpawnGroups.Save();
			}
		}

		#endregion

		#region Sounds

		/// <summary>
		/// The SoundData object providing information about UO Sounds
		/// </summary>
		private static SoundData m_SoundData;

		/// <summary>
		/// Gets the SoundData object providing information about the UO Sounds
		/// </summary>
		public static SoundData SoundData
		{
			get
			{
				if (m_SoundData == null)
				{
					Stream stream = Pandora.DataAssembly.GetManifestResourceStream("Data.SoundData.xml");
					XmlSerializer serializer = new XmlSerializer(typeof(SoundData));
					m_SoundData = serializer.Deserialize(stream) as SoundData;
					stream.Close();

					/*SupportSound s = new SupportSound();
					s.StructureS = new List<GenericNode>();

					for(int i = 0; i < m_SoundData.Structure.Count; i++)
					{
						GenericNode n = m_SoundData.Structure[i] as GenericNode;
						s.StructureS.Add(n);
					}
					
					TextWriter w = new StreamWriter(@"C:\SoundData.xml");
					try
					{
					XmlSerializer ser = new XmlSerializer(typeof(SupportSound));
					
						ser.Serialize(w, s);
					}
					catch (System.Exception e)
					{
						MessageBox.Show(e.ToString());
					}
					
					w.Close();*/
				}



				return m_SoundData;
			}
		}

		#endregion

		#region Skills

		/// <summary>
		/// The skills list
		/// </summary>
		private static SkillsData m_Skills;

		/// <summary>
		/// Gets the skills list
		/// </summary>
		public static SkillsData Skills
		{
			get
			{
				if (m_Skills == null)
				{
					m_Skills = new SkillsData();
				}

				return m_Skills;
			}
		}

		#endregion

		#region Lights

		/// <summary>
		/// The lights provides
		/// </summary>
		private static LightsData m_Lights;

		/// <summary>
		/// Gets the lights provides
		/// </summary>
		public static LightsData Lights
		{
			get
			{
				if (m_Lights == null)
				{
					m_Lights = new LightsData();
				}

				return m_Lights;
			}
		}

		#endregion

		#region Doors

		private static DoorsData m_Doors;

		/// <summary>
		/// Gets the data provider for doors
		/// </summary>
		public static DoorsData Doors
		{
			get
			{
				if (m_Doors == null)
				{
					m_Doors = new DoorsData();
				}

				return m_Doors;
			}
		}

		#endregion

		#endregion

		#region Modifiers

		/// <summary>
		/// The modifiers menu
		/// </summary>
		private static ContextMenu m_cmModifiers;

		/// <summary>
		/// Refreshes the modifiers menu
		/// </summary>
		public static void RefreshModifiersMenu()
		{
			if (m_cmModifiers == null)
			{
				MakeModifiersMenu();
				return;
			}

			m_cmModifiers.MenuItems.Clear();

			foreach (string modifier in Pandora.Profile.General.Modifiers)
			{
				MenuItem item = new MenuItem(modifier);
				item.Click += new EventHandler(OnModifierMenu);

				m_cmModifiers.MenuItems.Add(item);
			}
		}

		/// <summary>
		/// Creates the modifiers menu according to the options
		/// </summary>
		private static void MakeModifiersMenu()
		{
			if (m_cmModifiers != null)
			{
				m_cmModifiers.Dispose();
			}

			m_cmModifiers = new ContextMenu();

			foreach (string modifier in Pandora.Profile.General.Modifiers)
			{
				MenuItem item = new MenuItem(modifier);
				item.Click += new EventHandler(OnModifierMenu);

				m_cmModifiers.MenuItems.Add(item);
			}
		}

		/// <summary>
		/// Handles the selection of a specific modifier
		/// </summary>
		private static void OnModifierMenu(object sender, EventArgs e)
		{
			if (m_cmModifiers.SourceControl.Tag != null)
			{
				CommandCallback callback = m_cmModifiers.SourceControl.Tag as CommandCallback;

				if (callback != null)
				{
					MenuItem mi = sender as MenuItem;

					int index = m_cmModifiers.MenuItems.IndexOf(mi);

					if (Pandora.Profile.General.ModifiersWarnings[index])
					{
						if (MessageBox.Show(Pandora.BoxForm,
							string.Format(Pandora.Localization.TextProvider["Errors.ModifierWarn"], mi.Text),
							"",
							MessageBoxButtons.YesNo) == DialogResult.No)
						{
							return;
						}
					}

					// Do
					callback.DynamicInvoke(new object[] { mi.Text });
				}
			}
		}

		/// <summary>
		/// Gets the modifiers menu
		/// </summary>
		public static ContextMenu cmModifiers
		{
			get
			{
				if (m_cmModifiers == null)
				{
					MakeModifiersMenu();
					Pandora.Profile.General.ModifiersChanged += new EventHandler(OnModifiersChanged);
				}

				return m_cmModifiers;
			}
		}

		/// <summary>
		/// Handles changes in the list of modifiers
		/// </summary>
		private static void OnModifiersChanged(object sender, EventArgs e)
		{
			RefreshModifiersMenu();
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when the online state of Pandora's Box is changed
		/// </summary>
		public static event EventHandler OnlineChanged;

		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				Log.WriteEntry("Starting");
				Splash.Show();
				AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

				// Delete any temp files created during compilation of profile IO
				string temp = Path.Combine(Pandora.Folder, "temp.dll");

				if (File.Exists(temp))
				{
					Splash.SetStatusText("Deleting temporary files");
					File.Delete(temp);
				}

				// Issue 28:  	 Refactoring Pandora.cs - Tarion
				// Move any profiles resulting from previous versions
				ProfileManager.Instance.MoveOldProfiles();
				// End Issue 28:

				if (args.Length == 1 && File.Exists(args[0]) && Path.GetExtension(args[0]).ToLower() == ".pbp")
				{
					ProfileManager.Instance.ImportProfile(args[0]);
				}

				if (ProfileManager.Instance.ProfileLoaded)
				{
					Pandora.Log.WriteEntry("Import startup initiated");
					m_Context = new StartingContext(ProfileManager.Instance.Profile.Name);
					Application.Run(m_Context);
				}
				else
				{
					Pandora.Log.WriteEntry("Normal startup initiated");

					// Move on with normal startup
					Process proc = Pandora.ExistingInstance;
					if (proc != null) // Single instance check
					{
						Pandora.Log.WriteError(null, "Double instance detected");
						System.Windows.Forms.MessageBox.Show("You can't run two instances of Pandora's Box at the same time");
						//  Issue 33:  	 Bring to front if already started - Tarion
						ProcessExtension.BringToFront(proc);
					}
					else
					{
						Pandora.Log.WriteEntry("Double instances check passed");
						m_Context = new StartingContext();
						Application.Run(m_Context);
					}
				}
			}
			catch (Exception err)
			{
				Clipboard.SetDataObject(err.ToString(), true);
				MessageBox.Show("An error occurred. The error text has been placed on your clipboard, use CTRL+V to paste it in a text file.");
				// Issue 6:  	 Improve error management - Tarion
				Environment.Exit(1);
				// End Issue 6:
			}
		}

		// Issue 6:  	 Improve error management - Tarion
		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Clipboard.SetDataObject("UnhandledException: \n" + e.ToString(), true);
			MessageBox.Show("An error occurred. The error text has been placed on your clipboard, use CTRL+V to paste it in a text file.");
			Environment.Exit(1);
		}
		
	}
}