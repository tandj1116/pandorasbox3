using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using TheBox.Common;
using TheBox.Options;
using System.Windows.Forms;
using TheBox.Forms.ProfileWizard;

namespace TheBox
{
    /// <summary>
    /// Class to handle the Profilemanagement
    /// </summary>
    public class ProfileManager
    {
        // Issue 28:  	 Refactoring Pandora.cs - Tarion
        // This whole class contains code from Pandora.cs

        private static ProfileManager instance = null;

        public static ProfileManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProfileManager();
                }

                return instance;
            }
        }

        public bool ProfileLoaded
        {
            get { return (profile != null); }
        }

        private Profile profile = null;

        /// <summary>
        /// Gets the profile currently loaded
        /// </summary>
        public Profile Profile
        {
            get { return profile; }
        }

        /// <summary>
        /// Gets the location of the Profiles folder for this machine
        /// </summary>
        private string profilesFoler = String.Empty;
        public string ProfilesFolder
        {
            get
            {
                // Tarion: speed up the property by caching the folder in a local variable
                if (profilesFoler == String.Empty)
                {
                    string folder = Path.Combine(Pandora.ApplicationDataFolder, "Profiles");

                    Utility.EnsureDirectory(folder);
                    profilesFoler = folder;
                }

                return profilesFoler;
            }
        }

        /// <summary>
        /// Gets or sets the default profile
        /// </summary>
        public string DefaultProfile
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

        /// <summary>
        /// Gets an array of string representing the names of the existing profiles
        /// </summary>
        public string[] ExistingProfiles
        {
            get
            {
                string[] dirs = Directory.GetDirectories(ProfilesFolder);

                string[] profiles = new string[dirs.Length];

                for (int i = 0; i < dirs.Length; i++)
                {
                    string[] path = dirs[i].Split(Path.DirectorySeparatorChar);

                    profiles[i] = path[path.Length - 1];
                }

                return profiles;
            }
        }

        private ProfileManager()
        {
            // Private constuctor, it's a singleton. Use: ProfileManager.Instance
        }

        /// <summary>
        /// Closes Pandora's Box and creates a new profile
        /// </summary>
        public void CreateNewProfile(string language)
        {
            Profile profile = new Profile();
            profile.Language = language;
            // TODO: Display GUI to create a new profile. 
            ProfileWizard wiz = new ProfileWizard(profile);
            wiz.ShowDialog();
            profile = wiz.Profile;

            profile.Save();
            profile.CreateData();
        }

        public void LoadProfile(string name)
        {
            profile = Profile.Load(name);
        }


        /// <summary>
        /// Delete the current profile
        /// </summary>
        public void DeleteCurrentProfile()
        {
            Profile.DeleteProfile(profile.Name);
            profile = null;
        }

        /// <summary>
        /// Imports a new profile
        /// </summary>
        /// <param name="filename">The filename to the .pbp file</param>
        /// <returns>True if the profile has been imported and loaded successfully</returns>
        public bool ImportProfile(string filename)
        {
            Pandora.Log.WriteEntry("Importing Profile...");
            Splash.SetStatusText("Importing profile");

            Profile p = null;

            try { p = ProfileIO.Load(filename); }
            catch (Exception err) { MessageBox.Show(err.ToString()); }

            if (p == null)
            {
                return false;
            }
            else
            {
                profile = p;
            }

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
                System.Windows.Forms.MessageBox.Show("Profile imported correctly.");
            }

            if (!run)
            {
                profile = null;
            }

            return run;
        }

        #region Profile Import/Export

        /// <summary>
        /// Exports a Pandora's Box profile
        /// </summary>
        /// <param name="p">The profile to export</param>
        public void ExportProfile(Profile p)
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
        public Profile ImportProfile()
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

        /// <summary>
        /// Moves the profiles from the old program file based profiles folder to the application data folder
        /// </summary>
        public void MoveOldProfiles()
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
                string newFolder = Path.Combine(ProfilesFolder, name);

                int index = 1; // Adjust name if there's already a match
                while (Directory.Exists(newFolder))
                {
                    newFolder = Path.Combine(ProfilesFolder, string.Format("{0} {1}", name, index++));
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

        
    }
}
