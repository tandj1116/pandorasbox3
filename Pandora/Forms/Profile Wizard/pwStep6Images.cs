using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TheBox.Forms.ProfileWizard
{
	public class pwStep6Images : TSWizards.BaseInteriorStep
	{
		private System.Windows.Forms.ProgressBar PBar;
		private System.ComponentModel.IContainer components = null;

		public pwStep6Images()
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
			this.PBar = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// Description
			// 
			this.Description.Name = "Description";
			this.Description.Text = "WizProfile.ImgDescription";
			// 
			// PBar
			// 
			this.PBar.Location = new System.Drawing.Point(40, 192);
			this.PBar.Maximum = 7;
			this.PBar.Name = "PBar";
			this.PBar.Size = new System.Drawing.Size(392, 23);
			this.PBar.Step = 1;
			this.PBar.TabIndex = 1;
			// 
			// pwStep6Images
			// 
			this.Controls.Add(this.PBar);
			this.Name = "pwStep6Images";
			this.NextStep = "Step6bServer";
			this.PreviousStep = "Step5CustomMap";
			this.StepDescription = "WizProfile.ImgDescription";
			this.StepTitle = "WizProfile.ImgTtitle";
			this.ValidateStep += new System.ComponentModel.CancelEventHandler(this.pwStep6Images_ValidateStep);
			this.Controls.SetChildIndex(this.PBar, 0);
			this.Controls.SetChildIndex(this.Description, 0);
			this.ResumeLayout(false);

		}
		#endregion

		private void pwStep6Images_ValidateStep(object sender, System.ComponentModel.CancelEventArgs e)
		{
			ProfileWizard wiz = Wizard as ProfileWizard;
            //Kons
            wiz.NextEnabled = false;
            //End
			wiz.Profile.GenerateMaps( PBar );
			wiz.Profile.CreateMapFiles();
        }
	}
}

