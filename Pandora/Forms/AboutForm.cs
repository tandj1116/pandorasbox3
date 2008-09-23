using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TheBox.Forms
{
	/// <summary>
	/// Summary description for AboutForm.
	/// </summary>
	public class AboutForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button bClose;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel pnl;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labVersion;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.LinkLabel linkLabel3;
		private System.Windows.Forms.LinkLabel linkLabel4;
		private System.Windows.Forms.LinkLabel linkLabel5;
		private System.Windows.Forms.LinkLabel linkLabel6;
		private System.Windows.Forms.LinkLabel linkLabel7;
		private System.Windows.Forms.LinkLabel linkLabel8;
		private System.Windows.Forms.LinkLabel linkLabel9;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.PictureBox pictureBox4;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.LinkLabel linkLabel10;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AboutForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			labVersion.Text = Application.ProductVersion;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutForm));
			this.bClose = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.pnl = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.pictureBox4 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.linkLabel10 = new System.Windows.Forms.LinkLabel();
			this.linkLabel9 = new System.Windows.Forms.LinkLabel();
			this.linkLabel8 = new System.Windows.Forms.LinkLabel();
			this.linkLabel7 = new System.Windows.Forms.LinkLabel();
			this.linkLabel6 = new System.Windows.Forms.LinkLabel();
			this.linkLabel5 = new System.Windows.Forms.LinkLabel();
			this.linkLabel4 = new System.Windows.Forms.LinkLabel();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.labVersion = new System.Windows.Forms.Label();
			this.pnl.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bClose
			// 
			this.bClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bClose.Location = new System.Drawing.Point(344, 224);
			this.bClose.Name = "bClose";
			this.bClose.TabIndex = 7;
			this.bClose.Text = "Close";
			this.bClose.Click += new System.EventHandler(this.button1_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(560, 80);
			this.pictureBox1.TabIndex = 9;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.label1.Location = new System.Drawing.Point(16, 96);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(544, 23);
			this.label1.TabIndex = 10;
			this.label1.Text = "A GM tool for RunUO by Arya";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// pnl
			// 
			this.pnl.BackColor = System.Drawing.Color.Black;
			this.pnl.Controls.Add(this.panel1);
			this.pnl.Location = new System.Drawing.Point(16, 120);
			this.pnl.Name = "pnl";
			this.pnl.Size = new System.Drawing.Size(544, 272);
			this.pnl.TabIndex = 11;
			this.pnl.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Paint);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.groupBox3);
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.linkLabel1);
			this.panel1.Controls.Add(this.bClose);
			this.panel1.Location = new System.Drawing.Point(8, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(528, 256);
			this.panel1.TabIndex = 0;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.pictureBox4);
			this.groupBox3.Controls.Add(this.pictureBox2);
			this.groupBox3.Controls.Add(this.pictureBox3);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(248, 32);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(272, 72);
			this.groupBox3.TabIndex = 8;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Respected Sites";
			// 
			// pictureBox4
			// 
			this.pictureBox4.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
			this.pictureBox4.Location = new System.Drawing.Point(168, 24);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new System.Drawing.Size(88, 32);
			this.pictureBox4.TabIndex = 5;
			this.pictureBox4.TabStop = false;
			this.pictureBox4.Tag = "http://www.uogateway.com/";
			this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
			// 
			// pictureBox2
			// 
			this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(8, 24);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(64, 32);
			this.pictureBox2.TabIndex = 3;
			this.pictureBox2.TabStop = false;
			this.pictureBox2.Tag = "http://www.runuo.com/";
			this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
			// 
			// pictureBox3
			// 
			this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
			this.pictureBox3.Location = new System.Drawing.Point(72, 24);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(88, 32);
			this.pictureBox3.TabIndex = 4;
			this.pictureBox3.TabStop = false;
			this.pictureBox3.Tag = "http://www.orbsydia.com/";
			this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.linkLabel10);
			this.groupBox2.Controls.Add(this.linkLabel9);
			this.groupBox2.Controls.Add(this.linkLabel8);
			this.groupBox2.Controls.Add(this.linkLabel7);
			this.groupBox2.Controls.Add(this.linkLabel6);
			this.groupBox2.Controls.Add(this.linkLabel5);
			this.groupBox2.Controls.Add(this.linkLabel4);
			this.groupBox2.Controls.Add(this.linkLabel3);
			this.groupBox2.Controls.Add(this.linkLabel2);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(248, 104);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(272, 112);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Shards";
			// 
			// linkLabel10
			// 
			this.linkLabel10.ActiveLinkColor = System.Drawing.Color.DeepSkyBlue;
			this.linkLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel10.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel10.LinkColor = System.Drawing.Color.MediumBlue;
			this.linkLabel10.Location = new System.Drawing.Point(8, 16);
			this.linkLabel10.Name = "linkLabel10";
			this.linkLabel10.Size = new System.Drawing.Size(256, 24);
			this.linkLabel10.TabIndex = 8;
			this.linkLabel10.TabStop = true;
			this.linkLabel10.Tag = "http://uodreams.gamesnet.it/";
			this.linkLabel10.Text = "P H A N T A S Y A";
			this.linkLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.linkLabel10.VisitedLinkColor = System.Drawing.Color.MediumBlue;
			this.linkLabel10.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel10_LinkClicked);
			// 
			// linkLabel9
			// 
			this.linkLabel9.ActiveLinkColor = System.Drawing.Color.DeepSkyBlue;
			this.linkLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel9.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel9.LinkColor = System.Drawing.Color.MediumBlue;
			this.linkLabel9.Location = new System.Drawing.Point(152, 88);
			this.linkLabel9.Name = "linkLabel9";
			this.linkLabel9.Size = new System.Drawing.Size(112, 16);
			this.linkLabel9.TabIndex = 7;
			this.linkLabel9.TabStop = true;
			this.linkLabel9.Tag = "http://www.imagine-nation.net/";
			this.linkLabel9.Text = "Imagine Nation";
			this.linkLabel9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.linkLabel9.VisitedLinkColor = System.Drawing.Color.MediumBlue;
			this.linkLabel9.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// linkLabel8
			// 
			this.linkLabel8.ActiveLinkColor = System.Drawing.Color.DeepSkyBlue;
			this.linkLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel8.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel8.LinkColor = System.Drawing.Color.MediumBlue;
			this.linkLabel8.Location = new System.Drawing.Point(152, 72);
			this.linkLabel8.Name = "linkLabel8";
			this.linkLabel8.Size = new System.Drawing.Size(112, 16);
			this.linkLabel8.TabIndex = 6;
			this.linkLabel8.TabStop = true;
			this.linkLabel8.Tag = "http://www.fttguild.com/legends/";
			this.linkLabel8.Text = "Legends of Old";
			this.linkLabel8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.linkLabel8.VisitedLinkColor = System.Drawing.Color.MediumBlue;
			this.linkLabel8.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// linkLabel7
			// 
			this.linkLabel7.ActiveLinkColor = System.Drawing.Color.DeepSkyBlue;
			this.linkLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel7.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel7.LinkColor = System.Drawing.Color.MediumBlue;
			this.linkLabel7.Location = new System.Drawing.Point(152, 56);
			this.linkLabel7.Name = "linkLabel7";
			this.linkLabel7.Size = new System.Drawing.Size(112, 16);
			this.linkLabel7.TabIndex = 5;
			this.linkLabel7.TabStop = true;
			this.linkLabel7.Tag = "http://britannia.valorknights.com";
			this.linkLabel7.Text = "Britannia";
			this.linkLabel7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.linkLabel7.VisitedLinkColor = System.Drawing.Color.MediumBlue;
			this.linkLabel7.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// linkLabel6
			// 
			this.linkLabel6.ActiveLinkColor = System.Drawing.Color.DeepSkyBlue;
			this.linkLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel6.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel6.LinkColor = System.Drawing.Color.MediumBlue;
			this.linkLabel6.Location = new System.Drawing.Point(120, 40);
			this.linkLabel6.Name = "linkLabel6";
			this.linkLabel6.Size = new System.Drawing.Size(144, 16);
			this.linkLabel6.TabIndex = 4;
			this.linkLabel6.TabStop = true;
			this.linkLabel6.Tag = "http://www.uogamers.com/";
			this.linkLabel6.Text = "UOGamers: Hybrid";
			this.linkLabel6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.linkLabel6.VisitedLinkColor = System.Drawing.Color.MediumBlue;
			this.linkLabel6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// linkLabel5
			// 
			this.linkLabel5.ActiveLinkColor = System.Drawing.Color.DeepSkyBlue;
			this.linkLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel5.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel5.LinkColor = System.Drawing.Color.MediumBlue;
			this.linkLabel5.Location = new System.Drawing.Point(8, 88);
			this.linkLabel5.Name = "linkLabel5";
			this.linkLabel5.Size = new System.Drawing.Size(128, 16);
			this.linkLabel5.TabIndex = 3;
			this.linkLabel5.TabStop = true;
			this.linkLabel5.Tag = "http://www.darkother.com";
			this.linkLabel5.Text = "Dark Other";
			this.linkLabel5.VisitedLinkColor = System.Drawing.Color.MediumBlue;
			this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// linkLabel4
			// 
			this.linkLabel4.ActiveLinkColor = System.Drawing.Color.DeepSkyBlue;
			this.linkLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel4.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel4.LinkColor = System.Drawing.Color.MediumBlue;
			this.linkLabel4.Location = new System.Drawing.Point(8, 72);
			this.linkLabel4.Name = "linkLabel4";
			this.linkLabel4.Size = new System.Drawing.Size(128, 16);
			this.linkLabel4.TabIndex = 2;
			this.linkLabel4.TabStop = true;
			this.linkLabel4.Tag = "http://www.mailstation.de/";
			this.linkLabel4.Text = "The Divine Sunrise";
			this.linkLabel4.VisitedLinkColor = System.Drawing.Color.MediumBlue;
			this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// linkLabel3
			// 
			this.linkLabel3.ActiveLinkColor = System.Drawing.Color.DeepSkyBlue;
			this.linkLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel3.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel3.LinkColor = System.Drawing.Color.MediumBlue;
			this.linkLabel3.Location = new System.Drawing.Point(8, 56);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new System.Drawing.Size(136, 16);
			this.linkLabel3.TabIndex = 1;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Tag = "http://www.uogamers.com/site/";
			this.linkLabel3.Text = "UOGamers: Rebirth";
			this.linkLabel3.VisitedLinkColor = System.Drawing.Color.MediumBlue;
			this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// linkLabel2
			// 
			this.linkLabel2.ActiveLinkColor = System.Drawing.Color.DeepSkyBlue;
			this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel2.LinkColor = System.Drawing.Color.MediumBlue;
			this.linkLabel2.Location = new System.Drawing.Point(8, 40);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(100, 16);
			this.linkLabel2.TabIndex = 0;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Tag = "http://uodreams.gamesnet.it/";
			this.linkLabel2.Text = "UODreams";
			this.linkLabel2.VisitedLinkColor = System.Drawing.Color.MediumBlue;
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label14);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 32);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(232, 216);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Credits";
			// 
			// label14
			// 
			this.label14.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label14.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.label14.Location = new System.Drawing.Point(8, 192);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(72, 16);
			this.label14.TabIndex = 11;
			this.label14.Text = "ZLib";
			this.label14.Click += new System.EventHandler(this.label14_Click);
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label13.Location = new System.Drawing.Point(80, 168);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(120, 16);
			this.label13.TabIndex = 10;
			this.label13.Text = "James T. Johnson";
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label12.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.label12.Location = new System.Drawing.Point(8, 168);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(72, 16);
			this.label12.TabIndex = 9;
			this.label12.Text = "TSWizard";
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label11.Location = new System.Drawing.Point(80, 144);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(56, 16);
			this.label11.TabIndex = 8;
			this.label11.Text = "Krrios";
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label10.Location = new System.Drawing.Point(8, 120);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(144, 16);
			this.label10.TabIndex = 7;
			this.label10.Text = "Third party libraries";
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label9.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.label9.Location = new System.Drawing.Point(8, 144);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(72, 16);
			this.label9.TabIndex = 6;
			this.label9.Text = "Ultima.dll";
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label8.Location = new System.Drawing.Point(80, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(128, 16);
			this.label8.TabIndex = 5;
			this.label8.Text = "Arya";
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.label7.Location = new System.Drawing.Point(8, 16);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(56, 16);
			this.label7.TabIndex = 4;
			this.label7.Text = "Code";
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(80, 96);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(128, 16);
			this.label6.TabIndex = 3;
			this.label6.Text = "Knightshade";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.label5.Location = new System.Drawing.Point(8, 96);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 16);
			this.label5.TabIndex = 2;
			this.label5.Text = "Artwork:";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(80, 40);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(144, 48);
			this.label4.TabIndex = 1;
			this.label4.Text = "Philantrop, Wolverana, JadeFist, ASyre8, Lyephsa Bytche";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.label3.Location = new System.Drawing.Point(8, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Testers:";
			// 
			// linkLabel1
			// 
			this.linkLabel1.ActiveLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(255)));
			this.linkLabel1.DisabledLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(40, 22);
			this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.linkLabel1.Location = new System.Drawing.Point(8, 8);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(512, 23);
			this.linkLabel1.TabIndex = 0;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Tag = "http://arya.runuo.com/";
			this.linkLabel1.Text = "For updates, support and feedback visit http://arya.runuo.com/";
			this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabel1.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(488, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 12;
			this.label2.Text = "Version :";
			// 
			// labVersion
			// 
			this.labVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labVersion.Location = new System.Drawing.Point(528, 88);
			this.labVersion.Name = "labVersion";
			this.labVersion.Size = new System.Drawing.Size(40, 16);
			this.labVersion.TabIndex = 13;
			this.labVersion.Text = "2.0.0.3";
			// 
			// AboutForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(576, 408);
			this.Controls.Add(this.labVersion);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.pnl);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AboutForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Pandora\'s Box";
			this.Load += new System.EventHandler(this.AboutForm_Load);
			this.Closed += new System.EventHandler(this.AboutForm_Closed);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.AboutForm_Paint);
			this.pnl.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void AboutForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			using ( Pen pen = new Pen( Color.Black ) )
			{
				e.Graphics.DrawRectangle( pen, 0, 0, Width - 1, Height - 1 );
			}
		}

		private void pnl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			using ( Pen pen = new Pen( Color.White ) )
			{
				e.Graphics.DrawRectangle( pen, 1, 1, pnl.Width - 3, pnl.Height - 3 );
			}
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			LinkLabel lnk = sender as LinkLabel;

			if ( lnk == null || lnk	.Tag == null || ! ( lnk.Tag is string ) )
				return;

			string url = lnk.Tag as string;

			System.Diagnostics.Process.Start( url );
		}

		private void pictureBox2_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start( "http://www.runuo.com/" );
		}

		private void pictureBox3_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start( "http://www.orbsydia.com/" );
		}

		private void pictureBox4_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start( "http://www.uogateway.com/" );
		}

		private void label14_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start( "http://www.gzip.org/zlib/" );
		}

		private void linkLabel10_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start( "http://www.phantasya.org/" );
		}

		private void AboutForm_Load(object sender, System.EventArgs e)
		{
			if ( Pandora.BoxForm != null )
				Pandora.BoxForm.Visible = false;
		}

		private void AboutForm_Closed(object sender, System.EventArgs e)
		{
			if ( Pandora.BoxForm != null )
				Pandora.BoxForm.Visible = true;
		}
	}
}