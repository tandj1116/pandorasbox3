using System;
using System.Drawing;
// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
using System.Collections.Generic;
// Issue 10 - End
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;

namespace TheBox.Lang
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Localizer : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox lCat;
		private System.Windows.Forms.TextBox txNewCat;
		private System.Windows.Forms.Button bAddCat;
		private System.Windows.Forms.ListBox lDef;
		private System.Windows.Forms.TextBox txDef;
		private System.Windows.Forms.TextBox txText;
		private System.Windows.Forms.Button bAddEntry;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private IContainer components;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.OpenFileDialog OpenFile;
		private System.Windows.Forms.SaveFileDialog SaveFile;
		private System.Windows.Forms.TextBox txLanguage;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox CheckMinimize;
		private Label label3;
		private Label label4;
		private Label label5;

		private TextProvider m_TextProvider;

		public Localizer()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			m_TextProvider = new TextProvider();
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.lCat = new System.Windows.Forms.ListBox();
			this.txNewCat = new System.Windows.Forms.TextBox();
			this.bAddCat = new System.Windows.Forms.Button();
			this.lDef = new System.Windows.Forms.ListBox();
			this.txDef = new System.Windows.Forms.TextBox();
			this.txText = new System.Windows.Forms.TextBox();
			this.bAddEntry = new System.Windows.Forms.Button();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.OpenFile = new System.Windows.Forms.OpenFileDialog();
			this.SaveFile = new System.Windows.Forms.SaveFileDialog();
			this.txLanguage = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.CheckMinimize = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lCat
			// 
			this.lCat.Location = new System.Drawing.Point(8, 58);
			this.lCat.Name = "lCat";
			this.lCat.Size = new System.Drawing.Size(144, 316);
			this.lCat.Sorted = true;
			this.lCat.TabIndex = 0;
			this.lCat.SelectedIndexChanged += new System.EventHandler(this.lCat_SelectedIndexChanged);
			this.lCat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lCat_KeyDown);
			// 
			// txNewCat
			// 
			this.txNewCat.Location = new System.Drawing.Point(8, 8);
			this.txNewCat.Name = "txNewCat";
			this.txNewCat.Size = new System.Drawing.Size(100, 20);
			this.txNewCat.TabIndex = 1;
			// 
			// bAddCat
			// 
			this.bAddCat.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bAddCat.Location = new System.Drawing.Point(112, 6);
			this.bAddCat.Name = "bAddCat";
			this.bAddCat.Size = new System.Drawing.Size(40, 23);
			this.bAddCat.TabIndex = 2;
			this.bAddCat.Text = "Add";
			this.bAddCat.Click += new System.EventHandler(this.bAddCat_Click);
			// 
			// lDef
			// 
			this.lDef.Location = new System.Drawing.Point(158, 58);
			this.lDef.Name = "lDef";
			this.lDef.Size = new System.Drawing.Size(144, 316);
			this.lDef.Sorted = true;
			this.lDef.TabIndex = 3;
			this.lDef.SelectedIndexChanged += new System.EventHandler(this.lDef_SelectedIndexChanged);
			this.lDef.DoubleClick += new System.EventHandler(this.lDef_DoubleClick);
			this.lDef.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lDef_KeyDown);
			// 
			// txDef
			// 
			this.txDef.Location = new System.Drawing.Point(352, 32);
			this.txDef.Name = "txDef";
			this.txDef.Size = new System.Drawing.Size(136, 20);
			this.txDef.TabIndex = 4;
			// 
			// txText
			// 
			this.txText.Location = new System.Drawing.Point(312, 96);
			this.txText.Multiline = true;
			this.txText.Name = "txText";
			this.txText.Size = new System.Drawing.Size(272, 136);
			this.txText.TabIndex = 5;
			// 
			// bAddEntry
			// 
			this.bAddEntry.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bAddEntry.Location = new System.Drawing.Point(400, 238);
			this.bAddEntry.Name = "bAddEntry";
			this.bAddEntry.Size = new System.Drawing.Size(88, 23);
			this.bAddEntry.TabIndex = 6;
			this.bAddEntry.Text = "Add / Update";
			this.bAddEntry.Click += new System.EventHandler(this.bAddEntry_Click);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3,
            this.menuItem4,
            this.menuItem5});
			this.menuItem1.Text = "File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "New";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "Open";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "Save";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 3;
			this.menuItem5.Text = "-";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(312, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 16);
			this.label1.TabIndex = 7;
			this.label1.Text = "Key";
			// 
			// OpenFile
			// 
			this.OpenFile.Filter = "Xml files (*.xml)|*.xml";
			// 
			// SaveFile
			// 
			this.SaveFile.Filter = "Xml files (*.xml)|*.xml";
			// 
			// txLanguage
			// 
			this.txLanguage.Location = new System.Drawing.Point(368, 304);
			this.txLanguage.Name = "txLanguage";
			this.txLanguage.Size = new System.Drawing.Size(216, 20);
			this.txLanguage.TabIndex = 8;
			this.txLanguage.TextChanged += new System.EventHandler(this.txLanguage_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(312, 304);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 9;
			this.label2.Text = "Language";
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(312, 328);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(112, 24);
			this.checkBox1.TabIndex = 10;
			this.checkBox1.Text = "Always on top";
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// CheckMinimize
			// 
			this.CheckMinimize.Checked = true;
			this.CheckMinimize.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CheckMinimize.Location = new System.Drawing.Point(424, 328);
			this.CheckMinimize.Name = "CheckMinimize";
			this.CheckMinimize.Size = new System.Drawing.Size(160, 24);
			this.CheckMinimize.TabIndex = 11;
			this.CheckMinimize.Text = "Minimize on set";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(5, 39);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 13);
			this.label3.TabIndex = 12;
			this.label3.Text = "Sections";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(155, 39);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(30, 13);
			this.label4.TabIndex = 13;
			this.label4.Text = "Keys";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(308, 70);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(28, 13);
			this.label5.TabIndex = 14;
			this.label5.Text = "Text";
			// 
			// Localizer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(592, 386);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.CheckMinimize);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txLanguage);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.bAddEntry);
			this.Controls.Add(this.txText);
			this.Controls.Add(this.txDef);
			this.Controls.Add(this.lDef);
			this.Controls.Add(this.bAddCat);
			this.Controls.Add(this.txNewCat);
			this.Controls.Add(this.lCat);
			this.Menu = this.mainMenu1;
			this.Name = "Localizer";
			this.Text = "Localizer";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.EnableVisualStyles();
			Application.Run(new Localizer());
		}

		private string CurrentDef
		{
			get
			{
				if ( lCat.SelectedItem != null && lDef.SelectedItem != null )
				{
					string one = (string) lCat.SelectedItem;
					string two = (string) lDef.SelectedItem;

					return string.Format( "{0}.{1}", one, two );
				}

				throw new Exception( "No chosen definition" );
			}
		}

		private void bAddCat_Click(object sender, System.EventArgs e)
		{
			if ( txNewCat.Text.Length == 0 )
			{
				MessageBox.Show( "Can't add an empty category" );
				return;
			}

			foreach ( string s in lCat.Items )
			{
				if ( s == txNewCat.Text )
				{
					MessageBox.Show( "Can't add duplicate items" );
					return;
				}
			}

			lCat.Items.Add( txNewCat.Text );
			txNewCat.Text = "";
		}

		private void lCat_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch( e.KeyCode )
			{
				case Keys.Delete:

					if ( lCat.SelectedItem != null )
					{
						m_TextProvider.DeleteSection( (string) lCat.SelectedItem );
						lCat.Items.Remove( lCat.SelectedItem );
					}

					break;
			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			NewDocument();
		}

		private void NewDocument()
		{
			m_TextProvider = new TextProvider();
			txNewCat.Text = "";
			txDef.Text = "";
			txText.Text = "";
			lCat.Items.Clear();
			lDef.Items.Clear();
		}

		private void bAddEntry_Click(object sender, System.EventArgs e)
		{
			string one = (string) lCat.SelectedItem;

			if ( one == null )
			{
				MessageBox.Show( "Please select a category first" );
				return;
			}

			string two = txDef.Text;

			if ( two.Length == 0 )
			{
				MessageBox.Show( "The key can't be empty" );
				return;
			}

			if ( txText.Text.Length == 0 )
			{
				MessageBox.Show( "The text can't be empty" );
				return;
			}

			m_TextProvider[ string.Format( "{0}.{1}", one, two ) ] = txText.Text;

			foreach ( string s in lDef.Items )
			{
				if ( s == two )
					return;
			}

			lDef.Items.Add( two );
			txDef.Text = "";
			txText.Text = "";
		}

		private void lCat_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( lCat.SelectedItem == null )
				return;

			// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert

			// Issue 45 - Localizer isn't totally working - http://code.google.com/p/pandorasbox3/issues/detail?id=45 - Smjert
			lDef.Items.Clear();
			txDef.Text = "";
			txText.Text = "";
			// Issue 45 - End

			Dictionary<string, string> hash;
			if (!m_TextProvider.Data.TryGetValue((string)lCat.SelectedItem, out hash))
				return;

			if ( hash != null )
				foreach ( string s in hash.Keys )
					lDef.Items.Add( s );
		}

		private void lDef_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( lDef.SelectedItem != null )
			{
				txDef.Text = (string) lDef.SelectedItem;

				txText.Text = m_TextProvider[ CurrentDef ];
			}
			else
			{
				txDef.Text = "";
				txText.Text = "";
			}
		}

		private void lDef_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch ( e.KeyCode )
			{
				case Keys.Delete:

					if ( lDef.SelectedItem != null )
					{
						m_TextProvider.RemoveItem( CurrentDef );
					}

					break;
			}
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			if ( m_TextProvider.Language == null || m_TextProvider.Language.Length == 0 )
			{
				MessageBox.Show( "Please enter a language" );
				return;
			}

			if ( SaveFile.ShowDialog() == DialogResult.OK )
			{
				try
				{
					m_TextProvider.Serialize( SaveFile.FileName );
				}
				catch ( Exception err )
				{
					MessageBox.Show( err.ToString() );
				}
			}
		}

		private void txLanguage_TextChanged(object sender, System.EventArgs e)
		{
			m_TextProvider.Language = txLanguage.Text;
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			if ( OpenFile.ShowDialog() == DialogResult.OK )
			{
				NewDocument();

				try
				{
					XmlDocument dom = new XmlDocument();
					dom.Load( OpenFile.FileName );
					m_TextProvider = TextProvider.Deserialize( dom );
				}
				catch
				{
					MessageBox.Show( "Wrong file type" );
					return;
				}

				txLanguage.Text = m_TextProvider.Language;

				lCat.Items.Clear();

				foreach ( string s in m_TextProvider.Data.Keys )
				{
					lCat.Items.Add( s );
				}
			}
		}

		private void lDef_DoubleClick(object sender, System.EventArgs e)
		{
			string one = (string) lCat.SelectedItem;
			string two = (string) lDef.SelectedItem;
			Clipboard.SetDataObject( string.Format( "{0}.{1}", one, two ) );

			if ( CheckMinimize.Checked )
			{
				if ( this.TopMost )
					this.WindowState = FormWindowState.Minimized;
				else
					this.SendToBack();
			}

			txDef.Text = "";
			txText.Text = "";
		}

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			this.TopMost = checkBox1.Checked;
		}
	}
}