using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using TheBox.Common;
using System.IO;
using TheBox.Forms.ProfileWizard;
using TheBox.Options;
using TheBox.Forms;

// Issue 28:  	 Refactoring Pandora.cs - Tarion
namespace TheBox
{
    
    /// <summary>
    /// The class dealing with the Pandora's startup
    /// </summary>
    public class StartingContext : ApplicationContext
    {
        private bool m_Started = false;

        public StartingContext()
        {
            DoProfile();
        }

        public StartingContext(string profile)
        {
            LoadProfile(profile);
        }

        public void DoProfile()
        {
            if (ProfileManager.Instance.DefaultProfile != null)
            {
                Splash.SetStatusText("Loading default profile");
                LoadProfile(ProfileManager.Instance.DefaultProfile);
            }
            else
            {
                // No default profile specified. Either choose one or create a new one
                string[] profiles = Directory.GetDirectories(ProfileManager.Instance.ProfilesFolder);

                if (profiles.Length == 0)
                    MakeNewProfile();
                else
                    ChooseProfile(profiles);
            }
        }

        /// <summary>
        /// Brings up a dialog that will create a new user profile
        /// </summary>
        public void MakeNewProfile()
        {
            Splash.SetStatusText("Creating new profile");

            MainForm = new LanguageSelector();

            if (m_Started)
                MainForm.Show();

            m_Started = true;
        }

        /// <summary>
        /// Loads a profile
        /// </summary>
        /// <param name="name">The name of the profile</param>
        private void LoadProfile(string name)
        {
            Splash.SetStatusText("Loading profile");

            try
            {
                ProfileManager.Instance.LoadProfile(name);
            }
            catch (Exception err)
            {
                Pandora.Log.WriteError(err, "Couldn't load profile {0}", name);

                if (name == ProfileManager.Instance.DefaultProfile)
                {
                    ProfileManager.Instance.DefaultProfile = "";
                }

                DoProfile();
                return;
            }

            if (Pandora.Profile == null)
            {
                string msg = string.Format("The profile {0} is corrupt, therefore it can't be loaded. Would you like to attempt to restore it?", name);

                if (MessageBox.Show(null, msg, "Profile Error", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Profile p = new Profile();
                    p.Name = name;
                    p.Save();
                    LoadProfile(name);
                }
                else
                    DoProfile();
                return;
            }

            try
            {
                MainForm = new Box();
                Pandora.BoxForm = (Box)MainForm;

                if (m_Started)
                    MainForm.Show();

                m_Started = true;
            }
            catch (Exception err)
            {
                Pandora.Log.WriteError(err, string.Format("Profile {0} failed.", name));
                // Issue 6:  	 Improve error management - Tarion
                // Application.Exit(); Did not worked correctly
                // We could use:  Environment.Exit(1);
                // But better forward the exception to the main function an cancel program there
                throw err;
                // End Issue 6
            }
        }

        protected override void OnMainFormClosed(object sender, EventArgs e)
        {
            // Action was NEW PROFILE: check if valid, if not exit
            if (sender is LanguageSelector)
            {
                if (Pandora.Profile != null) // Profile creation succesful
                {
                    MainForm = new Box();
                    Pandora.BoxForm = (Box)MainForm;

                    if (m_Started)
                        MainForm.Show();

                    m_Started = true;

                    return;
                }
                else
                {
                    Pandora.Log.WriteError(null, "Profile creation aborted");
                }
            }

            // PROFILE CHOOSER: exit/new profile/load profile
            if (sender is ProfileChooser)
            {
                ProfileChooser chooser = sender as ProfileChooser;

                switch (chooser.Action)
                {
                    case ProfileChooser.Actions.Exit:

                        break;

                    case ProfileChooser.Actions.LoadProfile:

                        LoadProfile(chooser.SelectedProfile);
                        return;

                    case ProfileChooser.Actions.MakeNewProfile:

                        MakeNewProfile();

                        return;
                }
            }

            if (sender is Box)
            {
                string next = (sender as Box).NextProfile;

                if (next != null)
                {
                    LoadProfile(next);
                    return;
                }
            }

            base.OnMainFormClosed(sender, e);
        }

        /// <summary>
        /// Brings up the choose profile dialog
        /// </summary>
        /// <param name="profiles">A list of possible profile names</param>
        private void ChooseProfile(string[] profiles)
        {
            Splash.SetStatusText("Profile selection");

            string[] pnames = new string[profiles.Length];

            for (int i = 0; i < profiles.Length; i++)
            {
                string[] items = profiles[i].Split(new char[] { Path.DirectorySeparatorChar });

                pnames[i] = items[items.Length - 1];
            }

            MainForm = new ProfileChooser(pnames);

            if (m_Started)
                MainForm.Show();

            m_Started = true;
        }
    }
}
