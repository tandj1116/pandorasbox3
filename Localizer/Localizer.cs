using System;
using System.Drawing;
using System.Collections;
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
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.OpenFileDialog OpenFile;
		private System.Windows.Forms.SaveFileDialog SaveFile;
		private System.Windows.Forms.TextBox txLanguage;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox CheckMinimize;

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
			this.lCat = new System.Windows.Forms.ListBox();
			this.txNewCat = new System.Windows.Forms.TextBox();
			this.bAddCat = new System.Windows.Forms.Button();
			this.lDef = new System.Windows.Forms.ListBox();
			this.txDef = new System.Windows.Forms.TextBox();
			this.txText = new System.Windows.Forms.TextBox();
			this.bAddEntry = new System.Windows.Forms.Button();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
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
			this.SuspendLayout();
			// 
			// lCat
			// 
			this.lCat.Location = new System.Drawing.Point(8, 32);
			this.lCat.Name = "lCat";
			this.lCat.Size = new System.Drawing.Size(144, 316);
			this.lCat.Sorted = true;
			this.lCat.TabIndex = 0;
			this.lCat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lCat_KeyDown);
			this.lCat.SelectedIndexChanged += new System.EventHandler(this.lCat_SelectedIndexChanged);
			// 
			// txNewCat
			// 
			this.txNewCat.Location = new System.Drawing.Point(8, 8);
			this.txNewCat.Name = "txNewCat";
			this.txNewCat.TabIndex = 1;
			this.txNewCat.Text = "";
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
			this.lDef.Location = new System.Drawing.Point(160, 32);
			this.lDef.Name = "lDef";
			this.lDef.Size = new System.Drawing.Size(144, 316);
			this.lDef.Sorted = true;
			this.lDef.TabIndex = 3;
			this.lDef.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lDef_KeyDown);
			this.lDef.DoubleClick += new System.EventHandler(this.lDef_DoubleClick);
			this.lDef.SelectedIndexChanged += new System.EventHandler(this.lDef_SelectedIndexChanged);
			// 
			// txDef
			// 
			this.txDef.Location = new System.Drawing.Point(352, 32);
			this.txDef.Name = "txDef";
			this.txDef.Size = new System.Drawing.Size(136, 20);
			this.txDef.TabIndex = 4;
			this.txDef.Text = "";
			// 
			// txText
			// 
			this.txText.Location = new System.Drawing.Point(312, 56);
			this.txText.Multiline = true;
			this.txText.Name = "txText";
			this.txText.Size = new System.Drawing.Size(272, 136);
			this.txText.TabIndex = 5;
			this.txText.Text = "";
			// 
			// bAddEntry
			// 
			this.bAddEntry.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bAddEntry.Location = new System.Drawing.Point(400, 200);
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
			this.txLanguage.Text = "";
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
			// Localizer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(592, 357);
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
			this.Text = "Form1";
			this.ResumeLayout(false);

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

			Hashtable hash = (Hashtable) m_TextProvider.Data[ (string) lCat.SelectedItem ];

			lDef.Items.Clear();

			if ( hash != null )
				foreach ( string s in hash.Keys )
					lDef.Items.Add( s );
			txDef.Text = "";
			txText.Text = "";
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