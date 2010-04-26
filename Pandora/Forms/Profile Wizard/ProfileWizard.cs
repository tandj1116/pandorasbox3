using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TSWizards;

namespace TheBox.Forms.ProfileWizard
{
	public class ProfileWizard : TSWizards.BaseWizard
	{
		private System.ComponentModel.IContainer components = null;

		private bool m_Succesful = false;
		private static TheBox.Common.Localization.TextProvider m_TextProvider = null;
		private TheBox.Options.Profile m_Profile;

		public static TheBox.Common.Localization.TextProvider TextProvider
		{
			get
			{
				return m_TextProvider;
			}
		}

		public bool Succesful
		{
			get
			{
				return m_Succesful;
			}
			set
			{
				m_Succesful = value;
			}
		}

		/// <summary>
		/// Creates a new Profile Wizard
		/// </summary>
		public ProfileWizard( TheBox.Options.Profile profile )
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			m_Profile = profile;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProfileWizard));
			// 
			// wizardTop
			// 
			this.wizardTop.Name = "wizardTop";
			// 
			// cancel
			// 
			this.cancel.Name = "cancel";
			// 
			// back
			// 
			this.back.Name = "back";
			// 
			// next
			// 
			this.next.Name = "next";
			// 
			// panelStep
			// 
			this.panelStep.BackColor = System.Drawing.SystemColors.Control;
			this.panelStep.DockPadding.All = 8;
			this.panelStep.Location = new System.Drawing.Point(0, 66);
			this.panelStep.Name = "panelStep";
			this.panelStep.Size = new System.Drawing.Size(488, 249);
			// 
			// ProfileWizard
			// 
			this.AllowClose = TSWizards.AllowClose.Ask;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(488, 355);
			this.FirstStepName = "Step1";
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "ProfileWizard";
			this.SideBarImage = ((System.Drawing.Image)(resources.GetObject("$this.SideBarImage")));
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Profile Wizard";
			this.Title = "Welcome to Pandora\'s Box Profile Wizard";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ProfileWizard_Closing);
			this.LoadSteps += new System.EventHandler(this.ProfileWizard_LoadSteps);

		}
		#endregion

		private void ProfileWizard_LoadSteps(object sender, System.EventArgs e)
		{
			this.AddStep( "Step1", new pwStep1() );
			this.AddStep( "Step3Name", new pwStep3Name() );
			this.AddStep( "Step4Folder", new pwStep4Folder() );
			this.AddStep( "Step5CustomMap", new pwStep5CustomMap() );
			this.AddStep( "Step5aMapNames", new pwStep5MapNames() );
			this.AddStep( "Step6Images", new pwStep6Images() );
			this.AddStep( "Step6bServer", new pwStep6bServer() );
			this.AddStep( "Step7End", new pwStep7End() );


            m_TextProvider = Pandora.Localization.GetLanguage(m_Profile.Language);

			LocalizeControl( this );

			foreach ( BaseStep c in this.Steps.Values )
			{
				LocalizeControl( c );

				if ( c is BaseInteriorStep )
					c.StepTitle = m_TextProvider[ c.StepTitle ];
			}

			//Pandora.Profile = null;
		}

		/// <summary>
		/// Localizes the text of a control and all of its children controls
		/// </summary>
		/// <param name="control">The control that should be localized</param>
		private void LocalizeControl( Control control )
		{
			string text = control.Text;

			string[] path = text.Split( new char[] { '.' } );

			if ( path.Length == 2 )
				control.Text = m_TextProvider[ text ];

			if ( control.Controls.Count > 0 )
			{
				foreach ( Control c in control.Controls )
					LocalizeControl( c );
			}
		}

		private void ProfileWizard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if ( !m_Succesful && Profile != null )
			{
				if ( System.IO.Directory.Exists( Profile.BaseFolder ) )
				{
					System.IO.Directory.Delete( Profile.BaseFolder, true );
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the profile created by the wizard
		/// </summary>
		public TheBox.Options.Profile Profile
		{
			get { return m_Profile; }
			set { m_Profile = value; }
		}
	}
}