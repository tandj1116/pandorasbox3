using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TheBox.Forms.ProfileWizard
{
	public class pwStep3Name : TSWizards.BaseInteriorStep
	{
		private System.Windows.Forms.TextBox txProfileName;
		private System.ComponentModel.IContainer components = null;

		public pwStep3Name()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
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
			this.txProfileName = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// Description
			// 
			this.Description.Name = "Description";
			this.Description.Text = "WizProfile.EnterName";
			// 
			// txProfileName
			// 
			this.txProfileName.Location = new System.Drawing.Point(40, 24);
			this.txProfileName.Name = "txProfileName";
			this.txProfileName.Size = new System.Drawing.Size(208, 20);
			this.txProfileName.TabIndex = 1;
			this.txProfileName.Text = "";
			this.txProfileName.TextChanged += new System.EventHandler(this.txProfileName_TextChanged);
			// 
			// pwStep3Name
			// 
			this.Controls.Add(this.txProfileName);
			this.Name = "pwStep3Name";
			this.NextStep = "Step4Folder";
			this.PreviousStep = "Step1";
			this.StepDescription = "WizProfile.EnterName";
			this.StepTitle = "WizProfile.Name";
			this.ValidateStep += new System.ComponentModel.CancelEventHandler(this.Step3Name_ValidateStep);
			this.ShowStep += new TSWizards.ShowStepEventHandler(this.pwStep3Name_ShowStep);
			this.Controls.SetChildIndex(this.txProfileName, 0);
			this.Controls.SetChildIndex(this.Description, 0);
			this.ResumeLayout(false);

		}
		#endregion

		private string m_ProfileName = "";

		/// <summary>
		/// Gets the name of the profile
		/// </summary>
		public string ProfileName
		{
			get { return m_ProfileName; }
		}

		private void Step3Name_ValidateStep(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if ( m_ProfileName.Length == 0 )
			{
				MessageBox.Show( ProfileWizard.TextProvider[ "WizProfile.EmptyName" ] );
				e.Cancel = true;
			}

			if ( TheBox.Options.Profile.ExistingProfiles.Contains( m_ProfileName ) )
			{
				MessageBox.Show( string.Format( ProfileWizard.TextProvider[ "WizProfile.ProfileExists" ], m_ProfileName ) );
				txProfileName.Text = "";
				e.Cancel = true;
			}

			ProfileWizard wiz = Wizard as ProfileWizard;

			wiz.Profile.Name = m_ProfileName;
		}

		private void txProfileName_TextChanged(object sender, System.EventArgs e)
		{
			m_ProfileName = txProfileName.Text;
		}

		private void pwStep3Name_ShowStep(object sender, TSWizards.ShowStepEventArgs e)
		{
			txProfileName.Focus();
		}
	}
}

