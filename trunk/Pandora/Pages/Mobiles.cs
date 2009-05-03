using System;
// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
using System.Collections.Generic;
// Issue 10 - End
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using TheBox.Data;
using TheBox.Common;
using TheBox.BoxServer;

namespace TheBox.Pages
{
	/// <summary>
	/// Summary description for Mobiles.
	/// </summary>
	public class Mobiles : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.CheckBox chkName;
		private System.Windows.Forms.Button bAdd;
		private System.Windows.Forms.Button bToSpawn;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private TheBox.Buttons.BoxButton boxButton1;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.NumericUpDown nTeam;
		private System.Windows.Forms.NumericUpDown nMaxDelay;
		private System.Windows.Forms.NumericUpDown nMinDelay;
		private System.Windows.Forms.NumericUpDown nRange;
		private System.Windows.Forms.NumericUpDown nAmount;
		private System.Windows.Forms.TreeView tCat;
		private System.Windows.Forms.TreeView tMob;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown nRange2;
		private System.Windows.Forms.LinkLabel linkSpawn;
		private System.Windows.Forms.ComboBox cbNames;
		private System.Windows.Forms.ContextMenu cmCat;
		private System.Windows.Forms.ContextMenu cmMob;
		private System.Windows.Forms.MenuItem mCatAddSub;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem mCatRename;
		private System.Windows.Forms.MenuItem mCatDelete;
		private System.Windows.Forms.MenuItem mMobAddMob;
		private System.Windows.Forms.MenuItem mMobAddSpawn;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem mMobEdit;
		private System.Windows.Forms.MenuItem mMobDelete;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem mMobSetBody;
		private System.Windows.Forms.MenuItem mCatAddCat;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter;
		private System.Windows.Forms.MenuItem mMobSetBodyMod;

		public Mobiles()
		{
			InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tCat = new System.Windows.Forms.TreeView();
			this.cmCat = new System.Windows.Forms.ContextMenu();
			this.mCatAddCat = new System.Windows.Forms.MenuItem();
			this.mCatAddSub = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.mCatRename = new System.Windows.Forms.MenuItem();
			this.mCatDelete = new System.Windows.Forms.MenuItem();
			this.tMob = new System.Windows.Forms.TreeView();
			this.cmMob = new System.Windows.Forms.ContextMenu();
			this.mMobAddMob = new System.Windows.Forms.MenuItem();
			this.mMobAddSpawn = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.mMobEdit = new System.Windows.Forms.MenuItem();
			this.mMobDelete = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.mMobSetBody = new System.Windows.Forms.MenuItem();
			this.mMobSetBodyMod = new System.Windows.Forms.MenuItem();
			this.chkName = new System.Windows.Forms.CheckBox();
			this.bAdd = new System.Windows.Forms.Button();
			this.bToSpawn = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.nRange2 = new System.Windows.Forms.NumericUpDown();
			this.linkSpawn = new System.Windows.Forms.LinkLabel();
			this.nTeam = new System.Windows.Forms.NumericUpDown();
			this.nMaxDelay = new System.Windows.Forms.NumericUpDown();
			this.nMinDelay = new System.Windows.Forms.NumericUpDown();
			this.nRange = new System.Windows.Forms.NumericUpDown();
			this.nAmount = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.boxButton1 = new TheBox.Buttons.BoxButton();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.cbNames = new System.Windows.Forms.ComboBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitter = new System.Windows.Forms.Splitter();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nRange2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nTeam)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nMaxDelay)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nMinDelay)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nRange)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nAmount)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tCat
			// 
			this.tCat.ContextMenu = this.cmCat;
			this.tCat.Dock = System.Windows.Forms.DockStyle.Left;
			this.tCat.HideSelection = false;
			this.tCat.ImageIndex = -1;
			this.tCat.Location = new System.Drawing.Point(0, 0);
			this.tCat.Name = "tCat";
			this.tCat.SelectedImageIndex = -1;
			this.tCat.Size = new System.Drawing.Size(152, 140);
			this.tCat.TabIndex = 0;
			this.tCat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.tCat.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tCat_AfterSelect);
			this.tCat.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tCat_AfterLabelEdit);
			// 
			// cmCat
			// 
			this.cmCat.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																										this.mCatAddCat,
																										this.mCatAddSub,
																										this.menuItem3,
																										this.mCatRename,
																										this.mCatDelete});
			this.cmCat.Popup += new System.EventHandler(this.cmCat_Popup);
			// 
			// mCatAddCat
			// 
			this.mCatAddCat.Index = 0;
			this.mCatAddCat.Text = "NPCs.mCatAddCat";
			this.mCatAddCat.Click += new System.EventHandler(this.mCatAddCat_Click);
			// 
			// mCatAddSub
			// 
			this.mCatAddSub.Index = 1;
			this.mCatAddSub.Text = "NPCs.mCatAddSub";
			this.mCatAddSub.Click += new System.EventHandler(this.mCatAddSub_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "-";
			// 
			// mCatRename
			// 
			this.mCatRename.Index = 3;
			this.mCatRename.Text = "NPCs.mCatRename";
			this.mCatRename.Click += new System.EventHandler(this.mCatRename_Click);
			// 
			// mCatDelete
			// 
			this.mCatDelete.Index = 4;
			this.mCatDelete.Text = "NPCs.mCatDelete";
			this.mCatDelete.Click += new System.EventHandler(this.mCatDelete_Click);
			// 
			// tMob
			// 
			this.tMob.AllowDrop = true;
			this.tMob.ContextMenu = this.cmMob;
			this.tMob.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tMob.HideSelection = false;
			this.tMob.ImageIndex = -1;
			this.tMob.Location = new System.Drawing.Point(0, 0);
			this.tMob.Name = "tMob";
			this.tMob.SelectedImageIndex = -1;
			this.tMob.ShowLines = false;
			this.tMob.ShowRootLines = false;
			this.tMob.Size = new System.Drawing.Size(284, 140);
			this.tMob.TabIndex = 1;
			this.tMob.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.tMob.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tMob_MouseDown);
			this.tMob.DoubleClick += new System.EventHandler(this.tMob_DoubleClick);
			this.tMob.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tMob_AfterSelect);
			this.tMob.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tMob_MouseMove);
			// 
			// cmMob
			// 
			this.cmMob.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																										this.mMobAddMob,
																										this.mMobAddSpawn,
																										this.menuItem4,
																										this.mMobEdit,
																										this.mMobDelete,
																										this.menuItem7,
																										this.mMobSetBody,
																										this.mMobSetBodyMod});
			this.cmMob.Popup += new System.EventHandler(this.cmMob_Popup);
			// 
			// mMobAddMob
			// 
			this.mMobAddMob.Index = 0;
			this.mMobAddMob.Text = "NPCs.mMobAddMob";
			this.mMobAddMob.Click += new System.EventHandler(this.mMobAddMob_Click);
			// 
			// mMobAddSpawn
			// 
			this.mMobAddSpawn.Index = 1;
			this.mMobAddSpawn.Text = "NPCs.mMobAddSpawn";
			this.mMobAddSpawn.Click += new System.EventHandler(this.mMobAddSpawn_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "-";
			// 
			// mMobEdit
			// 
			this.mMobEdit.Index = 3;
			this.mMobEdit.Text = "NPCs.mMobEdit";
			this.mMobEdit.Click += new System.EventHandler(this.mMobEdit_Click);
			// 
			// mMobDelete
			// 
			this.mMobDelete.Index = 4;
			this.mMobDelete.Text = "NPCs.mMobDelete";
			this.mMobDelete.Click += new System.EventHandler(this.mMobDelete_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 5;
			this.menuItem7.Text = "-";
			// 
			// mMobSetBody
			// 
			this.mMobSetBody.Index = 6;
			this.mMobSetBody.Text = "NPCs.mMobBody";
			this.mMobSetBody.Click += new System.EventHandler(this.mMobSetBody_Click);
			// 
			// mMobSetBodyMod
			// 
			this.mMobSetBodyMod.Index = 7;
			this.mMobSetBodyMod.Text = "NPCs.mMobBodyMod";
			this.mMobSetBodyMod.Click += new System.EventHandler(this.mMobSetBodyMod_Click);
			// 
			// chkName
			// 
			this.chkName.Enabled = false;
			this.chkName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkName.Location = new System.Drawing.Point(288, 24);
			this.chkName.Name = "chkName";
			this.chkName.Size = new System.Drawing.Size(72, 16);
			this.chkName.TabIndex = 2;
			this.chkName.Text = "Common.Name";
			this.chkName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.chkName.CheckedChanged += new System.EventHandler(this.chkName_CheckedChanged);
			// 
			// bAdd
			// 
			this.bAdd.Enabled = false;
			this.bAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bAdd.Location = new System.Drawing.Point(288, 0);
			this.bAdd.Name = "bAdd";
			this.bAdd.Size = new System.Drawing.Size(84, 23);
			this.bAdd.TabIndex = 4;
			this.bAdd.Text = "Common.Add";
			this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
			this.bAdd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			// 
			// bToSpawn
			// 
			this.bToSpawn.Enabled = false;
			this.bToSpawn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bToSpawn.Location = new System.Drawing.Point(288, 68);
			this.bToSpawn.Name = "bToSpawn";
			this.bToSpawn.Size = new System.Drawing.Size(84, 24);
			this.bToSpawn.TabIndex = 5;
			this.bToSpawn.Text = "NPCs.ToSpawn";
			this.bToSpawn.Click += new System.EventHandler(this.bToSpawn_Click);
			this.bToSpawn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.nRange2);
			this.groupBox1.Controls.Add(this.linkSpawn);
			this.groupBox1.Controls.Add(this.nTeam);
			this.groupBox1.Controls.Add(this.nMaxDelay);
			this.groupBox1.Controls.Add(this.nMinDelay);
			this.groupBox1.Controls.Add(this.nRange);
			this.groupBox1.Controls.Add(this.nAmount);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(376, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(116, 140);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "NPCs.Spawn";
			// 
			// nRange2
			// 
			this.nRange2.Location = new System.Drawing.Point(64, 100);
			this.nRange2.Maximum = new System.Decimal(new int[] {
																					 10000,
																					 0,
																					 0,
																					 0});
			this.nRange2.Name = "nRange2";
			this.nRange2.Size = new System.Drawing.Size(48, 20);
			this.nRange2.TabIndex = 19;
			this.nRange2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.nRange2.ValueChanged += new System.EventHandler(this.nRange2_ValueChanged);
			// 
			// linkSpawn
			// 
			this.linkSpawn.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkSpawn.Location = new System.Drawing.Point(8, 122);
			this.linkSpawn.Name = "linkSpawn";
			this.linkSpawn.Size = new System.Drawing.Size(104, 16);
			this.linkSpawn.TabIndex = 17;
			this.linkSpawn.TabStop = true;
			this.linkSpawn.Text = "NPCs.BigSpawn";
			this.linkSpawn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.linkSpawn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSpawn_LinkClicked);
			// 
			// nTeam
			// 
			this.nTeam.Location = new System.Drawing.Point(8, 100);
			this.nTeam.Maximum = new System.Decimal(new int[] {
																				  10000,
																				  0,
																				  0,
																				  0});
			this.nTeam.Name = "nTeam";
			this.nTeam.Size = new System.Drawing.Size(48, 20);
			this.nTeam.TabIndex = 16;
			this.nTeam.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.nTeam.ValueChanged += new System.EventHandler(this.nTeam_ValueChanged);
			// 
			// nMaxDelay
			// 
			this.nMaxDelay.Location = new System.Drawing.Point(64, 64);
			this.nMaxDelay.Maximum = new System.Decimal(new int[] {
																						100000,
																						0,
																						0,
																						0});
			this.nMaxDelay.Name = "nMaxDelay";
			this.nMaxDelay.Size = new System.Drawing.Size(48, 20);
			this.nMaxDelay.TabIndex = 15;
			this.nMaxDelay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.nMaxDelay.ValueChanged += new System.EventHandler(this.nMaxDelay_ValueChanged);
			// 
			// nMinDelay
			// 
			this.nMinDelay.Location = new System.Drawing.Point(8, 64);
			this.nMinDelay.Maximum = new System.Decimal(new int[] {
																						100000,
																						0,
																						0,
																						0});
			this.nMinDelay.Name = "nMinDelay";
			this.nMinDelay.Size = new System.Drawing.Size(48, 20);
			this.nMinDelay.TabIndex = 14;
			this.nMinDelay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.nMinDelay.ValueChanged += new System.EventHandler(this.nMinDelay_ValueChanged);
			// 
			// nRange
			// 
			this.nRange.Location = new System.Drawing.Point(64, 28);
			this.nRange.Maximum = new System.Decimal(new int[] {
																					10000,
																					0,
																					0,
																					0});
			this.nRange.Name = "nRange";
			this.nRange.Size = new System.Drawing.Size(48, 20);
			this.nRange.TabIndex = 13;
			this.nRange.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.nRange.ValueChanged += new System.EventHandler(this.nRange_ValueChanged);
			// 
			// nAmount
			// 
			this.nAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.nAmount.Location = new System.Drawing.Point(8, 28);
			this.nAmount.Maximum = new System.Decimal(new int[] {
																					 10000,
																					 0,
																					 0,
																					 0});
			this.nAmount.Name = "nAmount";
			this.nAmount.Size = new System.Drawing.Size(48, 20);
			this.nAmount.TabIndex = 12;
			this.nAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.nAmount.ValueChanged += new System.EventHandler(this.nAmount_ValueChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 86);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 16);
			this.label5.TabIndex = 11;
			this.label5.Text = "NPCs.Team";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(64, 14);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 16);
			this.label4.TabIndex = 10;
			this.label4.Text = "NPCs.Range";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "NPCs.MinTime";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 7;
			this.label1.Text = "NPCs.Amount";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(64, 86);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 16);
			this.label6.TabIndex = 20;
			this.label6.Text = "NPCs.SpawnRange";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Name = "label3";
			this.label3.TabIndex = 0;
			// 
			// boxButton1
			// 
			this.boxButton1.AllowEdit = true;
			this.boxButton1.ButtonID = 36;
			this.boxButton1.Def = null;
			this.boxButton1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton1.IsActive = true;
			this.boxButton1.Location = new System.Drawing.Point(288, 100);
			this.boxButton1.Name = "boxButton1";
			this.boxButton1.Size = new System.Drawing.Size(84, 24);
			this.boxButton1.TabIndex = 7;
			this.boxButton1.Text = "boxButton1";
			this.boxButton1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			// 
			// linkLabel1
			// 
			this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel1.LinkColor = System.Drawing.SystemColors.WindowText;
			this.linkLabel1.Location = new System.Drawing.Point(288, 124);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(80, 12);
			this.linkLabel1.TabIndex = 8;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Common.Find";
			this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// cbNames
			// 
			this.cbNames.Enabled = false;
			this.cbNames.Location = new System.Drawing.Point(288, 44);
			this.cbNames.Name = "cbNames";
			this.cbNames.Size = new System.Drawing.Size(84, 21);
			this.cbNames.TabIndex = 9;
			this.cbNames.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.cbNames.Enter += new System.EventHandler(this.cbNames_Enter);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tMob);
			this.panel1.Controls.Add(this.splitter);
			this.panel1.Controls.Add(this.tCat);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(284, 140);
			this.panel1.TabIndex = 10;
			// 
			// splitter
			// 
			this.splitter.Location = new System.Drawing.Point(152, 0);
			this.splitter.Name = "splitter";
			this.splitter.Size = new System.Drawing.Size(3, 140);
			this.splitter.TabIndex = 1;
			this.splitter.TabStop = false;
			this.splitter.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter_SplitterMoved);
			// 
			// Mobiles
			// 
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.cbNames);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.boxButton1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.bToSpawn);
			this.Controls.Add(this.bAdd);
			this.Controls.Add(this.chkName);
			this.Name = "Mobiles";
			this.Size = new System.Drawing.Size(496, 142);
			this.Load += new System.EventHandler(this.Mobiles_Load);
			this.Enter += new System.EventHandler(this.Mobiles_Enter);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.nRange2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nTeam)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nMaxDelay)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nMinDelay)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nRange)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nAmount)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Variables

		/// <summary>
		/// The node representing spawn groups on the tCat
		/// </summary>
		private TreeNode m_GroupNode = null;

		/// <summary>
		/// Specifies whether a spawn group node has been last clicked
		/// </summary>
		private bool m_SpawnNode;

		/// <summary>
		/// Specifies whether the spawn group has been edited, or newly created
		/// </summary>
		private bool m_SpawnEdit;

		/// <summary>
		/// The TreeNode the currently edited/created spawn group belongs to
		/// </summary>
		private TreeNode m_SpawnParent;

		/// <summary>
		/// Specifies whenever the spawn editor dialog is open
		/// </summary>
		private bool m_ManagingSpawns = false;

		/// <summary>
		/// The form used to edit spawns
		/// </summary>
		private TheBox.Forms.Editors.QuickSpawnGroup m_SpawnForm = null;

		#endregion

		/// <summary>
		/// Loads the data from the BoxData
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Mobiles_Load(object sender, System.EventArgs e)
		{
			if ( tCat.Nodes.Count > 0 )
				return;

			try
			{
				// Reset hue
				Pandora.Art.Hue = 0;

				if ( Pandora.Profile.General.MobilesSplitter > 0 )
				{
					splitter.SplitPosition = Pandora.Profile.General.MobilesSplitter;
				}

				Pandora.LocalizeMenu( cmCat );
				Pandora.LocalizeMenu( cmMob );

				foreach ( Control c in nRange2.Controls )
				{
					Pandora.ToolTip.SetToolTip( c, Pandora.TextProvider[ "NPCs.Range2ToolTip" ] );
				}

				RefreshData();

				// Spawn information
				try
				{
					nAmount.Value = Pandora.Profile.Mobiles.Amount;
					nTeam.Value = Pandora.Profile.Mobiles.Team;
					nRange.Value = Pandora.Profile.Mobiles.Range;
					nRange2.Value = Pandora.Profile.Mobiles.Extra;
					nMinDelay.Value = Pandora.Profile.Mobiles.MinDelay;
					nMaxDelay.Value = Pandora.Profile.Mobiles.MaxDelay;
				}
				catch ( Exception err )
				{
					Pandora.Log.WriteError( err, "Couldn't load spawn information properly" );
					MessageBox.Show( Pandora.TextProvider[ "Errors.Spawn" ] );
				}

				// Recent names
				cbNames.Items.AddRange( Pandora.Profile.Mobiles.RecentNames.GetArray() );
				if ( cbNames.Items.Count > 0 )
				{
					cbNames.SelectedIndex = 0;
				}

				chkName.Checked = Pandora.Profile.Mobiles.NameMount;
			}
			catch
			{
			}
		}

		/// <summary>
		/// Refreshes the items on the mobiles tree
		/// </summary>
		public void RefreshData()
		{
			tCat.BeginUpdate();

			tCat.Nodes.Clear();
			tMob.Nodes.Clear();

			// Spawn groups
			m_GroupNode = new TreeNode( Pandora.TextProvider[ "NPCs.SpawnGroups" ] );
			m_GroupNode.Nodes.AddRange( Pandora.SpawnGroups.GetNodes() );
			tCat.Nodes.Add( m_GroupNode );

			// Mobiles data
			TreeNode[] nodes = Pandora.Mobiles.GetNodes();
			tCat.Nodes.AddRange( nodes );

			tCat.EndUpdate();
		}

		/// <summary>
		/// User has selected a node on the mobiles list
		/// </summary>
		private void tMob_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if ( tCat.SelectedNode == m_GroupNode || ( tCat.SelectedNode != null && tCat.SelectedNode.Parent == m_GroupNode ) )
			{
				if ( e.Node != null )
				{
					bAdd.Enabled = true;
					linkSpawn.Enabled = true;
				}
			}
			else
			{
				TheBox.Data.BoxMobile mob = tMob.SelectedNode.Tag as TheBox.Data.BoxMobile;

				if ( mob != null )
				{
					Pandora.Art.ArtIndex = mob.Art;
					Pandora.Art.Hue = mob.Hue;
					Pandora.Profile.Mobiles.ArtIndex = mob.Art;

					chkName.Enabled = mob.CanBeNamed;
					cbNames.Enabled = mob.CanBeNamed;

					bAdd.Enabled = true;
					bToSpawn.Enabled = true;
					linkSpawn.Enabled = true;
				}
			}
		}

		/// <summary>
		/// User has selected a node on the categories list
		/// </summary>
		private void tCat_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			// Clear list on the right first of all
			tMob.Nodes.Clear();

			if ( e.Node != null )
			{
				// A node is selected

				if ( NodeIsGroup( e.Node ) )
				{
					// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
					// Spawn Groups
					if ( e.Node.Tag is List<object> )
					{
						List<object> list = e.Node.Tag as List<object>;
						// Issue 10 - End

						foreach ( BoxSpawn spawn in list )
						{
							TreeNode node = new TreeNode( spawn.Name );
							node.Tag = spawn;
							tMob.Nodes.Add( node );
						}
					}
				}
				else
				{
					// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
					if ( e.Node.Tag is List<object> )
					// Issue 10 - End
					{
						tMob.BeginUpdate();
						tMob.Nodes.Clear();

						// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
						TreeNode[] nodes = Pandora.Mobiles.GetDataNodes( e.Node.Tag as List<object> );
						// Issue 10 - End

						if ( nodes != null )
						{
							tMob.Nodes.AddRange( nodes );
						}

						tMob.EndUpdate();
					}
				}
			}

			bAdd.Enabled = false;
			bToSpawn.Enabled = false;
			linkSpawn.Enabled = false;
		}

		/// <summary>
		/// Focus gained: display the art tab
		/// </summary>
		private void Mobiles_Enter(object sender, System.EventArgs e)
		{
            // Issue 27:  	 Designer warnings - Tarion
            try
            {
                Pandora.BoxForm.SelectSmallTab(SmallTabs.Art);
                // Issue 31:  	 Pandora.Art exception on null - Tarion
                if (Pandora.ArtLoaded)
                {
                    Pandora.Art.Art = ArtViewer.Art.NPCs;
                    Pandora.Art.ArtIndex = Pandora.Profile.Mobiles.ArtIndex;
                    Pandora.Art.Hue = 0;
                }
            }
            catch { }
            // End Issue 27
		}

		#region Update spawn information

		/// <summary>
		/// Updates the profile variables of all spawn paramenters
		/// </summary>
		private void UpdateSpawnInfo()
		{
			Pandora.Profile.Mobiles.Amount = (int) nAmount.Value;
			Pandora.Profile.Mobiles.Range = (int) nRange.Value;
			Pandora.Profile.Mobiles.MinDelay = (int) nMinDelay.Value;
			Pandora.Profile.Mobiles.MaxDelay = (int) nMaxDelay.Value;
			Pandora.Profile.Mobiles.Team = (int) nTeam.Value;
			Pandora.Profile.Mobiles.Extra = (int) nRange2.Value;
		}

		/// <summary>
		/// Amount
		/// </summary>
		private void nAmount_ValueChanged(object sender, System.EventArgs e)
		{
			Pandora.Profile.Mobiles.Amount = (int) nAmount.Value;
		}

		/// <summary>
		/// Range
		/// </summary>
		private void nRange_ValueChanged(object sender, System.EventArgs e)
		{
			Pandora.Profile.Mobiles.Range = (int) nRange.Value;
		}

		/// <summary>
		/// Min delay
		/// </summary>
		private void nMinDelay_ValueChanged(object sender, System.EventArgs e)
		{
			Pandora.Profile.Mobiles.MinDelay = (int) nMinDelay.Value;
		}

		/// <summary>
		/// Max delay
		/// </summary>
		private void nMaxDelay_ValueChanged(object sender, System.EventArgs e)
		{
			Pandora.Profile.Mobiles.MaxDelay = (int) nMaxDelay.Value;
		}

		/// <summary>
		/// Team
		/// </summary>
		private void nTeam_ValueChanged(object sender, System.EventArgs e)
		{
			Pandora.Profile.Mobiles.Team = (int) nMaxDelay.Value;
		}

		/// <summary>
		/// Extra
		/// </summary>
		private void nRange2_ValueChanged(object sender, System.EventArgs e)
		{
			Pandora.Profile.Mobiles.Extra = (int) nRange2.Value;
		}

		#endregion

		#region Private Properties

		/// <summary>
		/// Gets the mobile currently selected on the tree. Is null is none is selected
		/// </summary>
		private TheBox.Data.BoxMobile SelectedMobile
		{
			get
			{
				if ( tMob.SelectedNode != null && tMob.SelectedNode.Tag != null )
				{
					return tMob.SelectedNode.Tag as TheBox.Data.BoxMobile;
				}
				else
				{
					return null;
				}
			}
		}

		/// <summary>
		/// Gets the spawn group currently selected on the tree. Null if none is selected
		/// </summary>
		private TheBox.Data.BoxSpawn SelectedSpawn
		{
			get
			{
				if ( tMob.SelectedNode != null && NodeIsGroup( tCat.SelectedNode ) )
				{
					return tMob.SelectedNode.Tag as BoxSpawn;
				}

				return null;
			}
		}

		#endregion

		/// <summary>
		/// User clicked the spawn link
		/// </summary>
		private void linkSpawn_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			UpdateSpawnInfo();

			if ( SelectedMobile != null )
			{
				// Simple mobile node
				Pandora.Profile.Commands.DoSpawn( SelectedMobile.Name );
				return;
			}
			
			if ( SelectedSpawn != null )
			{
				if ( !Pandora.Connected )
					BoxConnection.RequestConnection();

				if ( Pandora.Connected )
				{
					// Spawn group
					BoxSpawn spawn = SelectedSpawn.Clone() as BoxSpawn;

					spawn.Count = Pandora.Profile.Mobiles.Amount;
					spawn.MinDelay = Pandora.Profile.Mobiles.MinDelay;
					spawn.MaxDelay = Pandora.Profile.Mobiles.MaxDelay;
					spawn.Team = Pandora.Profile.Mobiles.Team;
					spawn.HomeRange = Pandora.Profile.Mobiles.Range;

					SpawnMessage msg = new SpawnMessage();
					Pandora.Profile.Server.FillBoxMessage( msg );

					msg.Spawn = spawn;

					if ( BoxConnection.ProcessMessage( msg ) == null ) // null answer = ok
						Utility.BringClientToFront();
				}
			}
		}

		/// <summary>
		/// Add a npc
		/// </summary>
		private void bAdd_Click(object sender, System.EventArgs e)
		{
			if ( SelectedMobile != null )
			{
				if ( SelectedMobile.CanBeNamed && chkName.Checked && cbNames.Text.Length > 0 )
				{
					// Mobile + name
					Pandora.Profile.Commands.DoAddMobile( SelectedMobile.Name, cbNames.Text );

					UpdateNames();
				}
				else
				{
					// Mobile only
					Pandora.Profile.Commands.DoAddMobile( SelectedMobile.Name );
				}
			}

			if ( SelectedSpawn != null )
			{
				if ( ! Pandora.Connected )
				{
					BoxConnection.RequestConnection();
				}

				if ( Pandora.Connected )
				{
					TheBox.BoxServer.SpawnMessage msg = new SpawnMessage();
					Pandora.Profile.Server.FillBoxMessage( msg );

					msg.Spawn = SelectedSpawn;

					object response = BoxConnection.ProcessMessage( msg );

					// null outcome means that the spawn has been processed
					if ( response == null )
						Utility.BringClientToFront();
				}
			}
		}

		/// <summary>
		/// Send only the name to the spawn
		/// </summary>
		private void bToSpawn_Click(object sender, System.EventArgs e)
		{
			if ( SelectedMobile != null )
			{
				Pandora.SendToUO( SelectedMobile.Name , false );
			}
		}

		/// <summary>
		/// Update the recently used names
		/// </summary>
		private void UpdateNames()
		{
			if ( cbNames.Text.Length > 0 )
			{
				Pandora.Profile.Mobiles.RecentNames.AddString( cbNames.Text );

				cbNames.BeginUpdate();
				cbNames.Items.Clear();
				cbNames.Items.AddRange( Pandora.Profile.Mobiles.RecentNames.GetArray() );
				cbNames.EndUpdate();

				cbNames.SelectedIndex = 0;
			}
		}

		/// <summary>
		/// Select text when entering the focus combo
		/// </summary>
		private void cbNames_Enter(object sender, System.EventArgs e)
		{
			if ( cbNames.Text.Length > 0 )
			{
				cbNames.SelectAll();
			}
		}

		/// <summary>
		/// Update the name mount check
		/// </summary>
		private void chkName_CheckedChanged(object sender, System.EventArgs e)
		{
			Pandora.Profile.Mobiles.NameMount = chkName.Checked;
		}

		/// <summary>
		/// Sorts the categories tree
		/// </summary>
		private void SortTree()
		{
			tCat.BeginUpdate();

			tCat.Sorted = true;
			tCat.Sorted = false;
			tCat.Nodes.Remove( m_GroupNode );
			tCat.Nodes.Insert( 0, m_GroupNode );

			tCat.EndUpdate();
		}

		/// <summary>
		/// Verifies if a TreeNode belongs to the Spawn Groups structure
		/// </summary>
		/// <param name="node">The TreeNode to evaluate</param>
		/// <returns>True if the TreeNode belongs to the spawn groups</returns>
		private bool NodeIsGroup( TreeNode node )
		{
			if ( node != null )
			{
				if ( node == m_GroupNode )
				{
					return true;
				}
				else if ( node.Parent == m_GroupNode )
				{
					return true;
				}
			}

			return false;
		}

		#region Searching

		private SearchResults m_Results;

		/// <summary>
		/// Search for mobile
		/// </summary>
		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			TheBox.Forms.SearchForm sf = new TheBox.Forms.SearchForm( TheBox.Forms.SearchForm.SearchType.Mobile );

			if ( sf.ShowDialog() == DialogResult.OK )
			{
				string text = sf.SearchString.Replace( " ", "" );

				m_Results = TreeSearch.Find( tCat, sf.SearchString );

				if ( m_Results.Count == 0 )
				{
					MessageBox.Show( Pandora.TextProvider[ "Misc.NoResults" ] );
					m_Results = null;
				}
				else
				{
					NextSearchResult();
				}
			}		
		}

		/// <summary>
		/// Displays the next result in the list
		/// </summary>
		private void NextSearchResult()
		{
			if ( m_Results != null )
			{
				Result res = m_Results.GetNext();

				if ( res != null )
				{
					DisplayResult( res );
				}
			}
		}

		/// <summary>
		/// Displays the previous result in the list
		/// </summary>
		private void PrevSearchResult()
		{
			if ( m_Results != null )
			{
				Result res = m_Results.GetPrevious();

				if ( res != null )
				{
					DisplayResult( res );
				}
			}
		}

		/// <summary>
		/// Displays a search result
		/// </summary>
		/// <param name="res"></param>
		private void DisplayResult( Result res )
		{
			try
			{
				tCat.SelectedNode = res.Node;
				tMob.SelectedNode = tMob.Nodes[ res.Index ];
			}
			catch
			{
				MessageBox.Show( Pandora.TextProvider[ "Misc.SearchError" ] );
				m_Results = null;
			}
		}

		/// <summary>
		/// Processes the usage of keys to perform actions on the travel tab
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void DoKeys(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch ( e.KeyCode )
			{
				case Keys.F5:

					PrevSearchResult();
					break;

				case Keys.F8:

					NextSearchResult();
					break;

				case Keys.Left:

					if ( sender.Equals( tMob ) )
						tCat.Focus();
					break;

				case Keys.Right:

					if ( sender.Equals( tCat ) && tMob.Nodes.Count > 0 )
						tMob.Focus();
					break;

				case Keys.Enter:

					if ( sender.Equals( tCat ) )
					{
						TreeNode node = tCat.SelectedNode;

						if ( node != null )
						{
							if ( node.IsExpanded )
								node.Collapse();
							else
								node.Expand();
						}
					}
					break;
			}
		}

		#endregion

		/// <summary>
		/// Updates the BoxData object and saves it to disk
		/// </summary>
		private void UpdateData()
		{
			Pandora.Mobiles.Update( tCat.Nodes, m_GroupNode );
		}

		/// <summary>
		/// Updates spawn groups and saves it to disk
		/// </summary>
		private void UpdateSpawns()
		{
			Pandora.SpawnGroups.Update( m_GroupNode.Nodes );
		}

		/// <summary>
		/// Category menu popup
		/// </summary>
		private void cmCat_Popup(object sender, System.EventArgs e)
		{
			tCat.SelectedNode = tCat.GetNodeAt( tCat.PointToClient( Control.MousePosition ) );

			foreach ( MenuItem mi in cmCat.MenuItems )
			{
				mi.Enabled = false;
			}

			if ( tCat.SelectedNode != null )
			{
				if ( NodeIsGroup( tCat.SelectedNode ) )
				{
					if ( ! m_ManagingSpawns )
					{
						// Spawn Groups
						m_SpawnNode = true;

						mCatAddSub.Enabled = true;

						if ( tCat.SelectedNode != m_GroupNode )
						{
							mCatRename.Enabled = true;
							mCatDelete.Enabled = true;
						}
					}
				}
				else
				{
					m_SpawnNode = false;

					mCatAddCat.Enabled = true;
					mCatAddSub.Enabled = true;
					mCatRename.Enabled = true;
					mCatDelete.Enabled = true;
				}
			}
		}

		/// <summary>
		/// Cat -> Add main category ( Mobiles only )
		/// </summary>
		private void mCatAddCat_Click(object sender, System.EventArgs e)
		{
			TreeNode node = new TreeNode( "NewCategory" );
			// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
			node.Tag = new List<object>();
			// Issue 10 - End

			tCat.Nodes.Add( node );

			tCat.SelectedNode = node;

			tCat.LabelEdit = true;

			node.BeginEdit();
		}

		/// <summary>
		/// End of label edit on cat tree
		/// </summary>
		private void tCat_AfterLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
		{
			if ( e.Label != null && e.Label.Length == 0 )
			{
				e.CancelEdit = true;
			}
			else
			{
				if ( e.Label != null )
					e.Node.Text = e.Label;

				SortTree();

				if ( m_SpawnNode )
				{
					UpdateSpawns();
				}
				else
				{
					UpdateData();
				}
			}

			tCat.LabelEdit = false;
		}

		/// <summary>
		/// Add subsection
		/// </summary>
		private void mCatAddSub_Click(object sender, System.EventArgs e)
		{
			TreeNode node = new TreeNode( "NewSubsection" );
			// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
			node.Tag = new List<object>();
			// Issue 10 - End

			if ( m_SpawnNode )
			{
				// Add node to spawn groups
				m_GroupNode.Nodes.Add( node );
			}
			else
			{
				tCat.SelectedNode.Nodes.Add( node );
			}

			tCat.SelectedNode = node;
			tCat.LabelEdit = true;

			node.BeginEdit();
		}

		/// <summary>
		///  Rename a node on the category tree
		/// </summary>
		private void mCatRename_Click(object sender, System.EventArgs e)
		{
			tCat.LabelEdit = true;
			tCat.SelectedNode.BeginEdit();
		}

		/// <summary>
		/// Delete a node on the category tree
		/// </summary>
		private void mCatDelete_Click(object sender, System.EventArgs e)
		{
			if ( MessageBox.Show( 
				this,
				Pandora.TextProvider[ m_SpawnNode ? "NPCs.DeleteSpawns" : "NPCs.DeleteCat" ],
				"",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning ) == DialogResult.Yes )
			{
				TreeNode next = tCat.SelectedNode.NextNode;
				if ( next == null )
					next = tCat.SelectedNode.PrevNode;

				tCat.Nodes.Remove( tCat.SelectedNode );

				if ( m_SpawnNode )
				{
					UpdateSpawns();
				}
				else
				{
					UpdateData();
				}

				tCat.SelectedNode = next;
			}
		}

		/// <summary>
		/// Mobiles menu popup
		/// </summary>
		private void cmMob_Popup(object sender, System.EventArgs e)
		{
			tMob.SelectedNode = tMob.GetNodeAt( tMob.PointToClient( Control.MousePosition ) );

			foreach( MenuItem mi in cmMob.MenuItems )
			{
				mi.Enabled = false;
			}

			if ( tCat.SelectedNode != null )
			{
				if ( NodeIsGroup( tCat.SelectedNode ) )
				{
					if ( ! m_ManagingSpawns )
					{
						if ( tCat.SelectedNode != m_GroupNode )
						{
							mMobAddMob.Enabled = false;
							mMobAddSpawn.Enabled = true;

							if ( tMob.SelectedNode != null )
							{
								mMobEdit.Enabled = true;
								mMobDelete.Enabled = true;
							}

							mMobSetBody.Enabled = false;
							mMobSetBodyMod.Enabled = false;
						}
					}
				}
				else
				{
					mMobAddSpawn.Enabled = false;
					mMobAddMob.Enabled = true;

					if ( tMob.SelectedNode != null )
					{
						mMobEdit.Enabled = true;
						mMobDelete.Enabled = true;
						mMobSetBody.Enabled = true;
						mMobSetBodyMod.Enabled = true;
					}
				}
			}
		}

		/// <summary>
		/// Add new mobile
		/// </summary>
		private void mMobAddMob_Click(object sender, System.EventArgs e)
		{
			TheBox.Forms.Editors.QuickMobile qm = new TheBox.Forms.Editors.QuickMobile();

			if ( qm.ShowDialog() == DialogResult.OK )
			{
				// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
				List<object> list = tCat.SelectedNode.Tag as List<object>;
				// Issue 10 - End

				list.Add( qm.Mobile );
				list.Sort();

            TreeNode node = tCat.SelectedNode;
				tCat.SelectedNode = null;
				tCat.SelectedNode = node;

				tMob.SelectedNode = tMob.Nodes[ list.IndexOf( qm.Mobile ) ];

				UpdateData();
			}
		}

		/// <summary>
		/// Edit existing mobile
		/// </summary>
		private void mMobEdit_Click(object sender, System.EventArgs e)
		{
			if ( tMob.SelectedNode.Tag is BoxMobile )
			{
				TheBox.Forms.Editors.QuickMobile qm = new TheBox.Forms.Editors.QuickMobile();
				qm.Mobile = tMob.SelectedNode.Tag as BoxMobile;

				if ( qm.ShowDialog() == DialogResult.OK )
				{
					// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
					List<object> list = tCat.SelectedNode.Tag as List<object>;
					// Issue 10 - End

					list.Sort();

					TreeNode node = tCat.SelectedNode;
					tCat.SelectedNode = null;
					tCat.SelectedNode = node;

					tMob.SelectedNode = tMob.Nodes[ list.IndexOf( qm.Mobile ) ];
					UpdateData();
				}
			}
			else if ( tMob.SelectedNode.Tag is BoxSpawn )
			{
				if ( m_SpawnForm == null )
				{
					m_SpawnForm = new TheBox.Forms.Editors.QuickSpawnGroup();
					m_SpawnForm.Spawn = tMob.SelectedNode.Tag as BoxSpawn;

					m_SpawnEdit = true;
					m_ManagingSpawns = true;

					m_SpawnParent = tCat.SelectedNode;

					m_SpawnForm.SpawnReady += new EventHandler(m_SpawnForm_SpawnReady);

					m_SpawnForm.Show();
				}
			}
		}

		/// <summary>
		/// Delete mobile
		/// </summary>
		private void mMobDelete_Click(object sender, System.EventArgs e)
		{
			if ( MessageBox.Show( this,
				Pandora.TextProvider[ "NPCs.ConfirmDel" ],
				"",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question ) == DialogResult.Yes )
			{
				bool spawn = ( tMob.SelectedNode.Tag is BoxSpawn );

				int index = tMob.Nodes.IndexOf( tMob.SelectedNode );

				// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
				( tCat.SelectedNode.Tag as List<object> ).RemoveAt( index );
				// Issue 10 - End
			
				TreeNode next = tMob.SelectedNode.NextNode;
				if ( next == null )
					next = tMob.SelectedNode.PrevNode;

				tMob.Nodes.Remove( tMob.SelectedNode );
				tMob.SelectedNode = next;

				UpdateData();

				if ( spawn )
				{
					UpdateSpawns();
				}
				else
				{
					UpdateData();
				}
			}
		}

		/// <summary>
		/// Send set body command
		/// </summary>
		private void mMobSetBody_Click(object sender, System.EventArgs e)
		{
			Pandora.Profile.Commands.DoSet( "BodyValue", SelectedMobile.Art.ToString(), null );
			Pandora.Prop.SetProperty( "BodyValue", SelectedMobile.Art.ToString(), null );
		}

		/// <summary>
		/// Set body mod command
		/// </summary>
		private void mMobSetBodyMod_Click(object sender, System.EventArgs e)
		{
			Pandora.Profile.Commands.DoSet( "BodyMod", SelectedMobile.Art.ToString(), null );
			Pandora.Prop.SetProperty( "BodyMod", SelectedMobile.Art.ToString(), null );
		}

		private Point m_DragPoint = Point.Empty;

		/// <summary>
		/// Mouse Down on tMob: Start drag and drop
		/// </summary>
		private void tMob_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( e.Button == MouseButtons.Left )
			{
				m_DragPoint = new Point( e.X, e.Y );
			}
			else if ( e.Button == MouseButtons.Right )
			{
				tMob.SelectedNode = tMob.GetNodeAt( e.X, e.Y );
			}
		}

		private void tMob_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( e.Button == MouseButtons.None )
				return;

			if ( Math.Abs( e.X - m_DragPoint.X ) > 5 || Math.Abs( e.Y - m_DragPoint.Y ) > 5 )
			{
				tMob.SelectedNode = tMob.GetNodeAt( m_DragPoint );

				if ( tMob.SelectedNode != null )
				{
					tMob.DoDragDrop( ( tMob.SelectedNode.Tag as BoxMobile ).Name, DragDropEffects.Copy );
				}
				m_DragPoint = Point.Empty;
			}
		}

		/// <summary>
		/// Create new spawn
		/// </summary>
		private void mMobAddSpawn_Click(object sender, System.EventArgs e)
		{
			m_SpawnForm = new TheBox.Forms.Editors.QuickSpawnGroup();

			m_SpawnForm.SpawnReady += new EventHandler(m_SpawnForm_SpawnReady);

			m_SpawnParent = tCat.SelectedNode;
			m_SpawnEdit = false;
			m_ManagingSpawns = true;

			m_SpawnForm.Show();
		}

		/// <summary>
		/// Occurs when the user is finished with editing the spawn group
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_SpawnForm_SpawnReady(object sender, EventArgs e)
		{
			m_ManagingSpawns = false;

			if ( ! m_SpawnEdit )
			{
				// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
				// Add a new spawn
				( m_SpawnParent.Tag as List<object> ).Add( m_SpawnForm.Spawn );
				( m_SpawnParent.Tag as List<object> ).Sort();
				// Issue 10 - End
			}

			if ( tCat.SelectedNode == m_SpawnParent )
			{
				tCat.SelectedNode = null;
				tCat.SelectedNode = m_SpawnParent;
			}

			m_SpawnForm.Dispose();
			m_SpawnForm = null;
			UpdateSpawns();
		}

		/// <summary>
		/// Splitter being moved: Update option
		/// </summary>
		private void splitter_SplitterMoved(object sender, System.Windows.Forms.SplitterEventArgs e)
		{
			Pandora.Profile.General.MobilesSplitter = splitter.SplitPosition;
		}

		public void RefreshArt()
		{
			if ( tMob.SelectedNode != null && tMob.SelectedNode.Tag != null )
			{
				BoxMobile m = tMob.SelectedNode.Tag as BoxMobile;

				Pandora.Art.ArtIndex = m.Art;
				Pandora.Art.Hue = m.Hue;
			}
		}

		private void tMob_DoubleClick(object sender, System.EventArgs e)
		{
			bAdd.PerformClick();
		}
	}
}