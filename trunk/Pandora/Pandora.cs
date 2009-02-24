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

		#region Map, Art, Hues, Props

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

		#region Localization and TextProvider

		/// <summary>
		/// Gets the TextProvider object used to retrieve localized text
		/// </summary>
		public static TheBox.Lang.TextProvider TextProvider
		{
			get
			{
				if (m_TextProvider == null)
				{
					// m_TextProvider = TheBox.Lang.TextProvider.Deserialize( @"D:\Dev\Pandora 2.0\Pandora\Language\English.xml" );
                    // Issue 6:  	 Improve error management - Tarion
                    try
                    {
                        m_TextProvider = TheBox.Lang.TextProvider.GetLanguage();
                    }
                    catch
                    {
                        return null;
                    }
                    // End Issue 6
				}

				return m_TextProvider;
			}
			set { m_TextProvider = value; }
		}

		/// <summary>
		/// Gets a StringCollection representing the languages available
		/// </summary>
		public static StringCollection SupportedLanguages
		{
			get
			{
				StringCollection languages = new StringCollection();

				languages.Add("English");

				// TODO : Add code to correctly detect supported languages

				return languages;
			}
		}

		/// <summary>
		/// Localizes the text of a control and all of its children controls
		/// </summary>
		/// <param name="control">The control that should be localized</param>
		public static void LocalizeControl(Control control)
		{
			//			ButtonBase bb = control as ButtonBase;
			//
			//			if ( bb != null )
			//				bb.FlatStyle = Pandora.Profile.General.FlatButtons ? FlatStyle.Flat : FlatStyle.System;

			if (control is Form)
			{
				// Set options on controls
				Form f = control as Form;

				f.TopMost = Pandora.Profile.General.TopMost;
				f.Opacity = (double)Pandora.Profile.General.Opacity / 100.0;
			}

			if (control is TheBox.Buttons.BoxButton)
			{
				// Box button
				TheBox.Buttons.BoxButton b = control as TheBox.Buttons.BoxButton;

				ButtonDef def = null;

				if (b.ButtonID >= 0)
					def = Buttons[b];

				Profile.ButtonIndex.DoButton(b);
			}
			else
			{
				// Classic control
				string text = control.Text;

				string[] path = text.Split(new char[] { '.' });

				if (path.Length == 2)
					control.Text = Pandora.TextProvider[text];

				if (control is LinkLabel)
				{
					(control as LinkLabel).LinkColor = Pandora.Profile.General.Links.Color;
					(control as LinkLabel).VisitedLinkColor = Pandora.Profile.General.Links.Color;
					(control as LinkLabel).LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
				}

				if (control.Controls.Count > 0)
				{
					foreach (Control c in control.Controls)
						LocalizeControl(c);
				}
			}
		}

		/// <summary>
		/// Localizes a menu and all of its submenus
		/// </summary>
		/// <param name="menu">The menu that must be localized</param>
		public static void LocalizeMenu(Menu menu)
		{
			foreach (MenuItem mi in menu.MenuItems)
			{
				string text = mi.Text;

				string localizedText = Pandora.TextProvider[text];

				if (localizedText != null)
					mi.Text = localizedText;

				if (mi.MenuItems.Count > 0)
					LocalizeMenu(mi);
			}
		}

		/// <summary>
		/// Gets the tool tip provider for this instance of Pandora
		/// </summary>
		public static System.Windows.Forms.ToolTip ToolTip
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

		/// <summary>
		/// Updates the color used to display links
		/// </summary>
		/// <param name="c">The Control that contains links to be changed</param>
		public static void UpdateLinks(Control control)
		{
			if (control is LinkLabel)
			{
				(control as LinkLabel).LinkColor = Pandora.Profile.General.Links.Color;
				(control as LinkLabel).VisitedLinkColor = Pandora.Profile.General.Links.Color;
			}

			foreach (Control c in control.Controls)
			{
				UpdateLinks(c);
			}
		}

		#endregion

		#region Variables

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
		private static TheBox.Lang.TextProvider m_TextProvider = null;

		/// <summary>
		/// The currently loaded profile
		/// </summary>
		private static TheBox.Options.Profile m_Profile = null;

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
		private static Process ExistingInstance
		{
			get
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
						m_TheBox.Text = string.Format(Pandora.TextProvider["Misc.BoxTitle"], Pandora.Profile.Name, Pandora.Connected ? Pandora.TextProvider["Misc.Online"] : Pandora.TextProvider["Misc.Offline"]);
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
		public static void SendToUO(string text, bool UsePrefix)
		{
			if (UsePrefix)
			{
				Utility.SendToUO(string.Format("{0}{1}\r\n", m_Profile.General.CommandPrefix, text));
			}
			else
			{
				Utility.SendToUO(string.Format("{0}\r\n", text));
			}
		}

		/// <summary>
		/// Gets an array of string representing the names of the existing profiles
		/// </summary>
		public static string[] ExistingProfiles
		{
			get
			{
				string[] dirs = Directory.GetDirectories(Pandora.ProfilesFolder);

				string[] profiles = new string[dirs.Length];

				for (int i = 0; i < dirs.Length; i++)
				{
					string[] path = dirs[i].Split(Path.DirectorySeparatorChar);

					profiles[i] = path[path.Length - 1];
				}

				return profiles;
			}
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
		/// Closes the current application, deletes the current profile and restarts Pandora
		/// </summary>
		public static void DeleteCurrentProfile()
		{
			m_Context.MainForm = null;

			if (m_TheBox != null)
			{
				m_TheBox.Close();
				m_TheBox.Dispose();
			}

			Profile.DeleteProfile(Pandora.Profile.Name);
			Pandora.Profile = null;

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
				if (MessageBox.Show(null, Pandora.TextProvider["Misc.RequestConnection"], "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
		/// Gets the location of the Profiles folder for this machine
		/// </summary>
		public static string ProfilesFolder
		{
			get
			{
				string folder = Path.Combine(ApplicationDataFolder, "Profiles");

				TheBox.Common.Utility.EnsureDirectory(folder);

				return folder;
			}
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

		/// <summary>
		/// Moves the profiles from the old program file based profiles folder to the application data folder
		/// </summary>
		private static void MoveProfiles()
		{
			#region Move Log.txt

			string log = Path.Combine(Pandora.Folder, "Log.txt");

			if (File.Exists(log))
			{
				Splash.SetStatusText("Removing old log file");
				try
				{
					File.Delete(log);
				}
				catch { }
			}

			#endregion

			#region Move INI file

			string iniFile = Path.Combine(Pandora.Folder, "Pandora.ini");

			if (File.Exists(iniFile))
			{
				Splash.SetStatusText("Removing old ini file");

				string newIni = Path.Combine(Pandora.ApplicationDataFolder, "Pandora.ini");

				if (!File.Exists(newIni))
				{
					try
					{
						File.Move(iniFile, newIni);
						Pandora.Log.WriteEntry("Ini file moved to application data folder");
					}
					catch (Exception err)
					{
						Pandora.Log.WriteError(err, "Couldn't move ini file from {0} to {1}", iniFile, newIni);
					}
				}
				else
				{
					try
					{
						File.Delete(iniFile);
						Pandora.Log.WriteEntry("Ini file {0} deleted", iniFile);
					}
					catch (Exception err)
					{
						Pandora.Log.WriteError(err, "Couldn't delete ini file {0}", iniFile);
					}
				}
			}

			#endregion

			string oldFolder = Path.Combine(Pandora.Folder, "Profiles");

			if (!Directory.Exists(oldFolder))
				return;

			string[] profiles = Directory.GetDirectories(oldFolder);

			if (profiles.Length == 0)
				return;

			Splash.SetStatusText("Moving old profiles");

			Pandora.Log.WriteEntry("Found {0} profiles in the old profiles folder", profiles.Length);

			foreach (string profile in profiles)
			{
				string name = Utility.GetDirectoryName(profile);
				string newFolder = Path.Combine(Pandora.ProfilesFolder, name);

				int index = 1; // Adjust name if there's already a match
				while (Directory.Exists(newFolder))
				{
					newFolder = Path.Combine(Pandora.ProfilesFolder, string.Format("{0} {1}", name, index++));
				}

				try
				{
					if (Utility.CopyDirectory(profile, newFolder))
					{
						Pandora.Log.WriteEntry("Profile {0} copied from {1} to {2}", name, profile, newFolder);

						// Profile copied. Now delete.
						try
						{
							Directory.Delete(profile, true);
							Pandora.Log.WriteEntry("Old profile folder deleted: {0}", profile);
						}
						catch (Exception err)
						{
							Pandora.Log.WriteError(err, "Couldn't delete old profile folder: {0}", profile);
						}
					}
				}
				catch (Exception err)
				{
					Pandora.Log.WriteError(err, "Couldn't move profile {0} to {1}", name, newFolder);
				}
			}

			// Finally delete folder (if empty)
			try
			{
				if (Directory.GetDirectories(oldFolder).Length == 0)
				{
					Directory.Delete(oldFolder, true);
					Pandora.Log.WriteEntry("Deleted old profile directory: {0}", oldFolder);
				}
				else
				{
					Pandora.Log.WriteEntry("Can't delete profiles folder because some profiles hasn't been moved");
				}
			}
			catch (Exception err)
			{
				Pandora.Log.WriteError(err, "Couldn't delete old profiles folder: {0}", oldFolder);
			}
		}

		#endregion

		#region Profile managment

		/// <summary>
		/// Gets the profile currently loaded
		/// </summary>
		public static Profile Profile
		{
			get { return m_Profile; }
			set { m_Profile = value; }
		}

		/// <summary>
		/// Gets or sets the default profile
		/// </summary>
		public static string DefaultProfile
		{
			get
			{
				string ini = Path.Combine(Pandora.ApplicationDataFolder, "Pandora.ini");

				if (File.Exists(ini))
				{
					StreamReader reader = new StreamReader(ini);

					try
					{
						string defaultprofile = reader.ReadLine();
						reader.Close();

						string[] args = defaultprofile.Split(new char[] { '=' });

						if (args.Length == 2 && args[0].ToLower() == "defaultprofile")
						{
							if (args[1].Length > 0)
								return args[1];
							else
								return null;
						}
					}
					catch { }
				}

				return null;
			}
			set
			{
				string ini = Path.Combine(Pandora.ApplicationDataFolder, "Pandora.ini");

				StreamWriter writer = new StreamWriter(ini);

				writer.WriteLine(string.Format("DefaultProfile={0}", value));

				writer.Close();
			}
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
							string.Format(Pandora.TextProvider["Errors.ModifierWarn"], mi.Text),
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

				// Delete any temp files created during compilation of profile IO
				string temp = Path.Combine(Pandora.Folder, "temp.dll");

				if (File.Exists(temp))
				{
					Splash.SetStatusText("Deleting temporary files");
					File.Delete(temp);
				}

				// Move any profiles resulting from previous versions
				MoveProfiles();

				bool newProfile = false;

				if (args.Length == 1 && File.Exists(args[0]) && Path.GetExtension(args[0]).ToLower() == ".pbp")
				{
					Pandora.Log.WriteEntry("Importing Profile...");
					newProfile = ImportProfile(args[0]);
				}

				if (!newProfile)
				{
					Pandora.Log.WriteEntry("Normal startup initiated");

					Process existing = null;

					try
					{
						existing = Pandora.ExistingInstance;
					}
					catch (Exception err)
					{
						Pandora.Log.WriteError(err, "Error when enumerating instances");
					}

					// Move on with normal startup
					if (existing != null)
					{
						Pandora.Log.WriteError(null, "Double instance detected");
						System.Windows.Forms.MessageBox.Show("You can't run two instances of Pandora's Box at the same time");
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

		/// <summary>
		/// Imports a new profile
		/// </summary>
		/// <param name="filename">The filename to the .pbp file</param>
		/// <returns>True if the profile has been imported and loaded successfully</returns>
		private static bool ImportProfile(string filename)
		{
			Splash.SetStatusText("Importing profile");

			Profile p = null;

			try { p = ProfileIO.Load(filename); }
			catch (Exception err) { MessageBox.Show(err.ToString()); }

			if (p == null)
				return false;

			bool run = false;

			if (Pandora.ExistingInstance != null)
			{
				// Already running: Close?

				if (MessageBox.Show(null,
					"Another instance of Pandora's Box is currently running. Would you like to close it and load the profile you have just imported?",
					"Profile import succesful",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question) == DialogResult.Yes)
				{
					if (Pandora.ExistingInstance != null)
					{
						Pandora.ExistingInstance.Close();
					}

					run = true;
				}
			}
			else
			{
				run = true;
			}

			if (run)
			{
				System.Windows.Forms.MessageBox.Show("Profile imported correctly. Now loading...");
				m_Context = new StartingContext(p.Name);
				Application.Run(m_Context);
			}

			return run;
		}

		

		#region Profile Import/Export

		/// <summary>
		/// Exports a Pandora's Box profile
		/// </summary>
		/// <param name="p">The profile to export</param>
		public static void ExportProfile(Profile p)
		{
			System.Windows.Forms.SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Pandora's Box Profile (*.pbp)|*.pbp";

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				ProfileIO pio = new ProfileIO(p);
				pio.Save(dlg.FileName);
			}

			dlg.Dispose();
		}

		/// <summary>
		/// Imports a profile from a PBP file
		/// </summary>
		/// <returns>The Profile object imported</returns>
		public static Profile ImportProfile()
		{
			System.Windows.Forms.OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Pandora's Box Profile (*.pbp)|*.pbp";
			Profile p = null;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				p = ProfileIO.Load(dlg.FileName);
			}

			dlg.Dispose();

			return p;
		}

		#endregion
	}
}