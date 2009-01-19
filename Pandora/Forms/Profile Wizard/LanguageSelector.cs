using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TheBox.Forms.ProfileWizard
{
	/// <summary>
	/// Summary description for LanguageSelector.
	/// </summary>
	public class LanguageSelector : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ComboBox cmbLang;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public LanguageSelector()
		{
			InitializeComponent();
			DialogResult = DialogResult.Cancel;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(LanguageSelector));
			this.cmbLang = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cmbLang
			// 
			this.cmbLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbLang.Location = new System.Drawing.Point(24, 16);
			this.cmbLang.Name = "cmbLang";
			this.cmbLang.Size = new System.Drawing.Size(152, 21);
			this.cmbLang.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(64, 48);
			this.button1.Name = "button1";
			this.button1.TabIndex = 1;
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// LanguageSelector
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(202, 82);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.cmbLang);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LanguageSelector";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Pandora\'s Box";
			this.Load += new System.EventHandler(this.LanguageSelector_Load);
			this.ResumeLayout(false);

			// Issue 17 - Language selector appears behind SplashScreen - http://code.google.com/p/pandorasbox3/issues/detail?id=17 - Smjert
			this.TopMost = true;
			// Issue 17 - End

		}
		#endregion

		private void LanguageSelector_Load(object sender, System.EventArgs e)
		{
			// Read available languages
			string path = Path.Combine( Pandora.Folder, "Lang" );

			string[] files = Directory.GetFiles( path );

			foreach ( string s in files )
			{
				if ( s.ToLower().EndsWith( ".dll" ) )
				{
					// Possible language file
					string lang = Path.GetFileNameWithoutExtension( s );

					cmbLang.Items.Add( lang );
				}
			}

			cmbLang.SelectedIndex = 0;

			BringToFront();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			// Close and run the profile wizard
			TheBox.Options.Profile profile = new TheBox.Options.Profile();
			profile.Language = cmbLang.Text;

			ProfileWizard wiz = new ProfileWizard( profile );
			Visible = false;
			wiz.ShowDialog();
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
