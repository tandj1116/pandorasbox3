using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using TheBox.Data;
using TheBox.Common;

namespace TheBox.Pages
{
	/// <summary>
	/// Summary description for Deco.
	/// </summary>
	public class Deco : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Panel panel;
		private System.Windows.Forms.Splitter splitter;
		private System.Windows.Forms.NumericUpDown numTile;
		private System.Windows.Forms.Button bTile;
		private System.Windows.Forms.TreeView tDeco;
		private System.Windows.Forms.TreeView tCat;
		private TheBox.Controls.DecoMover decoMover1;
		private System.Windows.Forms.LinkLabel lnkFind;
		private TheBox.Buttons.BoxButton boxButton3;
		private TheBox.Buttons.BoxButton boxButton4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown numNudge;
		private System.Windows.Forms.Button bAdd;
		private System.Windows.Forms.CheckBox chkHued;
		private System.Windows.Forms.CheckBox chkStatic;
		private System.Windows.Forms.ContextMenu cmCustom;
		private System.Windows.Forms.MenuItem cmCustomCat;
		private System.Windows.Forms.MenuItem cmCustomItem;
		private System.Windows.Forms.MenuItem cmCustomDel;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button bNudgeUp;
		private System.Windows.Forms.Button bNudgeDown;

		private SearchResults m_Results;
		private System.Windows.Forms.ContextMenu cmItem;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.CheckBox chkRnd;
		private System.Windows.Forms.NumericUpDown nRnd;
		private BoxDeco m_SelectedDeco;

		private BoxDeco SelectedDeco
		{
			get { return m_SelectedDeco; }
			set
			{
				m_SelectedDeco = value;

				if ( m_SelectedDeco != null )
				{
					Pandora.BoxForm.SelectSmallTab( SmallTabs.Art );

					Pandora.Profile.Deco.ArtIndex = m_SelectedDeco.ID;
					Pandora.Art.ArtIndex = m_SelectedDeco.ID;

					if ( Pandora.Profile.Deco.Hued )
					{
						Pandora.Art.Hue = Pandora.Profile.Hues.SelectedIndex;
					}
				}
				
				bAdd.Enabled = m_SelectedDeco != null;
				bTile.Enabled = m_SelectedDeco != null;
			}
		}

		public Deco()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Deco));
			this.panel = new System.Windows.Forms.Panel();
			this.tDeco = new System.Windows.Forms.TreeView();
			this.cmItem = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.splitter = new System.Windows.Forms.Splitter();
			this.tCat = new System.Windows.Forms.TreeView();
			this.bAdd = new System.Windows.Forms.Button();
			this.numTile = new System.Windows.Forms.NumericUpDown();
			this.bTile = new System.Windows.Forms.Button();
			this.chkHued = new System.Windows.Forms.CheckBox();
			this.chkStatic = new System.Windows.Forms.CheckBox();
			this.decoMover1 = new TheBox.Controls.DecoMover();
			this.lnkFind = new System.Windows.Forms.LinkLabel();
			this.numNudge = new System.Windows.Forms.NumericUpDown();
			this.boxButton3 = new TheBox.Buttons.BoxButton();
			this.boxButton4 = new TheBox.Buttons.BoxButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.bNudgeUp = new System.Windows.Forms.Button();
			this.bNudgeDown = new System.Windows.Forms.Button();
			this.cmCustom = new System.Windows.Forms.ContextMenu();
			this.cmCustomCat = new System.Windows.Forms.MenuItem();
			this.cmCustomItem = new System.Windows.Forms.MenuItem();
			this.cmCustomDel = new System.Windows.Forms.MenuItem();
			this.chkRnd = new System.Windows.Forms.CheckBox();
			this.nRnd = new System.Windows.Forms.NumericUpDown();
			this.panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numTile)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numNudge)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nRnd)).BeginInit();
			this.SuspendLayout();
			// 
			// panel
			// 
			this.panel.Controls.Add(this.tDeco);
			this.panel.Controls.Add(this.splitter);
			this.panel.Controls.Add(this.tCat);
			this.panel.Location = new System.Drawing.Point(0, 0);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(320, 140);
			this.panel.TabIndex = 0;
			// 
			// tDeco
			// 
			this.tDeco.ContextMenu = this.cmItem;
			this.tDeco.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tDeco.HideSelection = false;
			this.tDeco.ImageIndex = -1;
			this.tDeco.Location = new System.Drawing.Point(147, 0);
			this.tDeco.Name = "tDeco";
			this.tDeco.SelectedImageIndex = -1;
			this.tDeco.ShowLines = false;
			this.tDeco.ShowPlusMinus = false;
			this.tDeco.ShowRootLines = false;
			this.tDeco.Size = new System.Drawing.Size(173, 140);
			this.tDeco.TabIndex = 2;
			this.tDeco.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.tDeco.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tDeco_MouseDown);
			this.tDeco.DoubleClick += new System.EventHandler(this.tDeco_DoubleClick);
			this.tDeco.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tDeco_AfterSelect);
			this.tDeco.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tDeco_MouseMove);
			// 
			// cmItem
			// 
			this.cmItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.menuItem1});
			this.cmItem.Popup += new System.EventHandler(this.cmItem_Popup);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Items.ItemID";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// splitter
			// 
			this.splitter.Location = new System.Drawing.Point(144, 0);
			this.splitter.Name = "splitter";
			this.splitter.Size = new System.Drawing.Size(3, 140);
			this.splitter.TabIndex = 1;
			this.splitter.TabStop = false;
			this.splitter.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter_SplitterMoved);
			// 
			// tCat
			// 
			this.tCat.Dock = System.Windows.Forms.DockStyle.Left;
			this.tCat.HideSelection = false;
			this.tCat.ImageIndex = -1;
			this.tCat.Location = new System.Drawing.Point(0, 0);
			this.tCat.Name = "tCat";
			this.tCat.SelectedImageIndex = -1;
			this.tCat.Size = new System.Drawing.Size(144, 140);
			this.tCat.TabIndex = 0;
			this.tCat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.tCat.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tCat_MouseDown);
			this.tCat.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tCat_AfterSelect);
			this.tCat.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tCat_AfterLabelEdit);
			// 
			// bAdd
			// 
			this.bAdd.Enabled = false;
			this.bAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bAdd.Location = new System.Drawing.Point(324, 0);
			this.bAdd.Name = "bAdd";
			this.bAdd.Size = new System.Drawing.Size(52, 23);
			this.bAdd.TabIndex = 1;
			this.bAdd.Text = "Common.Add";
			this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
			// 
			// numTile
			// 
			this.numTile.Location = new System.Drawing.Point(440, 0);
			this.numTile.Maximum = new System.Decimal(new int[] {
																	127,
																	0,
																	0,
																	0});
			this.numTile.Minimum = new System.Decimal(new int[] {
																	128,
																	0,
																	0,
																	-2147483648});
			this.numTile.Name = "numTile";
			this.numTile.Size = new System.Drawing.Size(52, 20);
			this.numTile.TabIndex = 2;
			this.numTile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.numTile.ValueChanged += new System.EventHandler(this.numTile_ValueChanged);
			// 
			// bTile
			// 
			this.bTile.Enabled = false;
			this.bTile.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bTile.Location = new System.Drawing.Point(384, 0);
			this.bTile.Name = "bTile";
			this.bTile.Size = new System.Drawing.Size(52, 23);
			this.bTile.TabIndex = 3;
			this.bTile.Text = "Deco.Tile";
			this.bTile.Click += new System.EventHandler(this.bTile_Click);
			// 
			// chkHued
			// 
			this.chkHued.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkHued.Location = new System.Drawing.Point(440, 24);
			this.chkHued.Name = "chkHued";
			this.chkHued.Size = new System.Drawing.Size(56, 20);
			this.chkHued.TabIndex = 4;
			this.chkHued.Text = "Deco.Hued";
			this.chkHued.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.chkHued.CheckedChanged += new System.EventHandler(this.chkHued_CheckedChanged);
			// 
			// chkStatic
			// 
			this.chkStatic.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkStatic.Location = new System.Drawing.Point(384, 24);
			this.chkStatic.Name = "chkStatic";
			this.chkStatic.Size = new System.Drawing.Size(52, 20);
			this.chkStatic.TabIndex = 5;
			this.chkStatic.Text = "Deco.Static";
			this.chkStatic.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.chkStatic.CheckedChanged += new System.EventHandler(this.chkStatic_CheckedChanged);
			// 
			// decoMover1
			// 
			this.decoMover1.BackColor = System.Drawing.SystemColors.Control;
			this.decoMover1.EventMode = false;
			this.decoMover1.Location = new System.Drawing.Point(424, 48);
			this.decoMover1.Name = "decoMover1";
			this.decoMover1.Size = new System.Drawing.Size(72, 92);
			this.decoMover1.TabIndex = 6;
			this.decoMover1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			// 
			// lnkFind
			// 
			this.lnkFind.Location = new System.Drawing.Point(324, 28);
			this.lnkFind.Name = "lnkFind";
			this.lnkFind.Size = new System.Drawing.Size(52, 16);
			this.lnkFind.TabIndex = 7;
			this.lnkFind.TabStop = true;
			this.lnkFind.Text = "Common.Find";
			this.lnkFind.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.lnkFind.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFind_LinkClicked);
			// 
			// numNudge
			// 
			this.numNudge.Location = new System.Drawing.Point(24, 16);
			this.numNudge.Maximum = new System.Decimal(new int[] {
																	 127,
																	 0,
																	 0,
																	 0});
			this.numNudge.Minimum = new System.Decimal(new int[] {
																	 1,
																	 0,
																	 0,
																	 0});
			this.numNudge.Name = "numNudge";
			this.numNudge.Size = new System.Drawing.Size(48, 20);
			this.numNudge.TabIndex = 11;
			this.numNudge.Value = new System.Decimal(new int[] {
																   1,
																   0,
																   0,
																   0});
			this.numNudge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.numNudge.ValueChanged += new System.EventHandler(this.numNudge_ValueChanged);
			// 
			// boxButton3
			// 
			this.boxButton3.AllowEdit = true;
			this.boxButton3.ButtonID = 45;
			this.boxButton3.Def = null;
			this.boxButton3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton3.IsActive = true;
			this.boxButton3.Location = new System.Drawing.Point(324, 76);
			this.boxButton3.Name = "boxButton3";
			this.boxButton3.Size = new System.Drawing.Size(48, 23);
			this.boxButton3.TabIndex = 14;
			this.boxButton3.Text = "boxButton3";
			// 
			// boxButton4
			// 
			this.boxButton4.AllowEdit = true;
			this.boxButton4.ButtonID = 46;
			this.boxButton4.Def = null;
			this.boxButton4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton4.IsActive = true;
			this.boxButton4.Location = new System.Drawing.Point(372, 76);
			this.boxButton4.Name = "boxButton4";
			this.boxButton4.Size = new System.Drawing.Size(48, 23);
			this.boxButton4.TabIndex = 15;
			this.boxButton4.Text = "boxButton4";
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
			this.groupBox1.Controls.Add(this.bNudgeUp);
			this.groupBox1.Controls.Add(this.bNudgeDown);
			this.groupBox1.Controls.Add(this.numNudge);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(324, 100);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(96, 40);
			this.groupBox1.TabIndex = 16;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Nudge";
			// 
			// bNudgeUp
			// 
			this.bNudgeUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.bNudgeUp.Image = ((System.Drawing.Image)(resources.GetObject("bNudgeUp.Image")));
			this.bNudgeUp.Location = new System.Drawing.Point(76, 16);
			this.bNudgeUp.Name = "bNudgeUp";
			this.bNudgeUp.Size = new System.Drawing.Size(16, 20);
			this.bNudgeUp.TabIndex = 13;
			this.bNudgeUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bNudgeUp_MouseDown);
			// 
			// bNudgeDown
			// 
			this.bNudgeDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.bNudgeDown.Image = ((System.Drawing.Image)(resources.GetObject("bNudgeDown.Image")));
			this.bNudgeDown.Location = new System.Drawing.Point(4, 16);
			this.bNudgeDown.Name = "bNudgeDown";
			this.bNudgeDown.Size = new System.Drawing.Size(16, 20);
			this.bNudgeDown.TabIndex = 12;
			this.bNudgeDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bNudgeDown_MouseDown);
			// 
			// cmCustom
			// 
			this.cmCustom.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.cmCustomCat,
																					 this.cmCustomItem,
																					 this.cmCustomDel});
			this.cmCustom.Popup += new System.EventHandler(this.cmCustom_Popup);
			// 
			// cmCustomCat
			// 
			this.cmCustomCat.Index = 0;
			this.cmCustomCat.Text = "Deco.cmCat";
			this.cmCustomCat.Click += new System.EventHandler(this.cmCustomCat_Click);
			// 
			// cmCustomItem
			// 
			this.cmCustomItem.Index = 1;
			this.cmCustomItem.Text = "Deco.cmItem";
			this.cmCustomItem.Click += new System.EventHandler(this.cmCustomItem_Click);
			// 
			// cmCustomDel
			// 
			this.cmCustomDel.Index = 2;
			this.cmCustomDel.Text = "Deco.cmDelete";
			this.cmCustomDel.Click += new System.EventHandler(this.cmCustomDel_Click);
			// 
			// chkRnd
			// 
			this.chkRnd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkRnd.Location = new System.Drawing.Point(324, 48);
			this.chkRnd.Name = "chkRnd";
			this.chkRnd.Size = new System.Drawing.Size(48, 20);
			this.chkRnd.TabIndex = 17;
			this.chkRnd.Text = "Rnd";
			this.chkRnd.CheckedChanged += new System.EventHandler(this.chkRnd_CheckedChanged);
			// 
			// nRnd
			// 
			this.nRnd.Location = new System.Drawing.Point(376, 48);
			this.nRnd.Maximum = new System.Decimal(new int[] {
																 15000,
																 0,
																 0,
																 0});
			this.nRnd.Minimum = new System.Decimal(new int[] {
																 15000,
																 0,
																 0,
																 -2147483648});
			this.nRnd.Name = "nRnd";
			this.nRnd.Size = new System.Drawing.Size(48, 20);
			this.nRnd.TabIndex = 18;
			this.nRnd.ValueChanged += new System.EventHandler(this.nRnd_ValueChanged);
			// 
			// Deco
			// 
			this.Controls.Add(this.nRnd);
			this.Controls.Add(this.chkRnd);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.boxButton4);
			this.Controls.Add(this.boxButton3);
			this.Controls.Add(this.lnkFind);
			this.Controls.Add(this.decoMover1);
			this.Controls.Add(this.chkStatic);
			this.Controls.Add(this.chkHued);
			this.Controls.Add(this.bTile);
			this.Controls.Add(this.numTile);
			this.Controls.Add(this.bAdd);
			this.Controls.Add(this.panel);
			this.Name = "Deco";
			this.Size = new System.Drawing.Size(496, 142);
			this.Load += new System.EventHandler(this.Deco_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numTile)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numNudge)).EndInit();
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.nRnd)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// On Load: Set up options
		/// </summary>
		private void Deco_Load(object sender, System.EventArgs e)
		{
			if ( tCat.Nodes.Count > 0 )
				return;

			try
			{
				Pandora.LocalizeMenu( cmCustom );
				Pandora.LocalizeMenu( cmItem );

				// Tool tips for nudge button
				Pandora.ToolTip.SetToolTip( bNudgeUp, Pandora.TextProvider[ "Deco.TipNudgeUp" ] );
				Pandora.ToolTip.SetToolTip( bNudgeDown, Pandora.TextProvider[ "Deco.TipNudgeDown" ] );

				if ( Pandora.Profile.General.DecoSplitter > 0 )
				{
					splitter.SplitPosition = Pandora.Profile.General.DecoSplitter;
				}

				// Manage data
				RefreshTrees();
				Pandora.Profile.Deco.ShowCustomDecoChanged += new EventHandler(Deco_ShowCustomDecoChanged);

				if ( Pandora.Profile.Deco.ShowCustomDeco )
				{
					tCat.ContextMenu = cmCustom;
				}

				chkStatic.Checked = Pandora.Profile.Deco.Static;
				chkHued.Checked = Pandora.Profile.Deco.Hued;

				numTile.Value = Pandora.Profile.Deco.TileHeight;
				numNudge.Value = Pandora.Profile.Deco.NudgeAmount;

				// Monitor hue changes
				if ( Pandora.Profile.Deco.Hued )
				{
					Pandora.Art.Hue = Pandora.Profile.Hues.SelectedIndex;
				}

				Pandora.Profile.Hues.HueChanged += new EventHandler(Hues_HueChanged);

				// Nudge buttons
				bNudgeUp.ContextMenu = Pandora.cmModifiers;
				bNudgeDown.ContextMenu = Pandora.cmModifiers;
				bNudgeUp.Tag = new CommandCallback( PerformNudgeUp );
				bNudgeDown.Tag = new CommandCallback( PerformNudgeDown );

				// Randomize
				chkRnd.Checked = Pandora.Profile.Deco.UseRandomizer;
				nRnd.Value = Pandora.Profile.Deco.RandomizeAmount;

				// Modifiers for add
				bAdd.Tag = new CommandCallback( PerformAdd );
				bAdd.ContextMenu = Pandora.cmModifiers;
			}
			catch
			{
				// Issue with VS
			}
		}

		/// <summary>
		/// Splitter moved: update option
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void splitter_SplitterMoved(object sender, System.Windows.Forms.SplitterEventArgs e)
		{
			Pandora.Profile.General.DecoSplitter = splitter.SplitPosition;
		}

		/// <summary>
		/// Refreshes the trees
		/// </summary>
		private void RefreshTrees()
		{
			tCat.BeginUpdate();
			tDeco.BeginUpdate();

			tCat.Nodes.Clear();
			tDeco.Nodes.Clear();

			tCat.Nodes.AddRange( Decorator.TreeNodes );

			tCat.EndUpdate();
			tDeco.EndUpdate();
		}

		/// <summary>
		/// The show custom deco has changed value. Refresh the tree.
		/// </summary>
		private void Deco_ShowCustomDecoChanged(object sender, EventArgs e)
		{
			RefreshTrees();

			if ( Pandora.Profile.Deco.ShowCustomDeco )
			{
				tCat.ContextMenu = cmCustom;
			}
			else
			{
				tCat.ContextMenu = null;
			}
		}

		/// <summary>
		/// Node has been selected on the categories list
		/// </summary>
		private void tCat_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			tCat.LabelEdit = false;

			if ( e.Node != null )
			{
				tDeco.BeginUpdate();
				tDeco.Nodes.Clear();

				if ( e.Node.Tag != null )
				{
					ArrayList list = e.Node.Tag as ArrayList;

					foreach( BoxDeco deco in list )
					{
						tDeco.Nodes.Add( deco.TreeNode );
					}
				}

				tDeco.EndUpdate();

				SelectedDeco = null;

				tCat.LabelEdit = IsCustom( e.Node );
			}
		}

		/// <summary>
		/// User selected an item to view
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tDeco_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if ( e.Node != null )
			{
				SelectedDeco = e.Node.Tag as BoxDeco;
			}
			else
			{
				SelectedDeco = null;
			}
		}

		#region Custom Entries

		/// <summary>
		/// Verifies if a given node is part of the custom section
		/// </summary>
		/// <param name="node">The TreeNode to evaluate</param>
		/// <returns>True if it's part of the custom section</returns>
		private bool IsCustom( TreeNode node )
		{
			if ( !Pandora.Profile.Deco.ShowCustomDeco )
				return false;

			if ( node.Parent == null )
				return false;

			return ( tCat.Nodes.IndexOf( node.Parent ) == 0 );
		}

		/// <summary>
		/// Custom Menu popup
		/// </summary>
		private void cmCustom_Popup(object sender, System.EventArgs e)
		{
			// Select the node first
			tCat.SelectedNode = tCat.GetNodeAt( tCat.PointToClient( Control.MousePosition ) );
			TreeNode node = tCat.SelectedNode;

			cmCustomCat.Enabled = false;
			cmCustomItem.Enabled = false;
			cmCustomDel.Enabled = false;

			if ( node == null )
				return;

			cmCustomCat.Enabled = true;

			if ( node.Parent != null )
			{
				if ( tCat.Nodes.IndexOf( node.Parent ) == 0 )
				{
					cmCustomItem.Enabled = true;
					cmCustomDel.Enabled = true;
				}
			}
		}

		/// <summary>
		/// User has changed the name of a category
		/// </summary>
		private void tCat_AfterLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
		{
			if ( e.Label != null && e.Label.Length > 0 )
			{
				e.CancelEdit = false;

				e.Node.Text = e.Label;
			}
			else
			{
				e.CancelEdit = true;
			}

			Decorator.CustomDeco = tCat.Nodes[ 0 ];
		}

		/// <summary>
		/// The user adds a new category to the custom items
		/// </summary>
		private void cmCustomCat_Click(object sender, System.EventArgs e)
		{
			TreeNode n = new TreeNode( "New Category" );
			n.Tag = new ArrayList();

			tCat.Nodes[ 0 ].Nodes.Add( n );

			tCat.SelectedNode = n;

			n.BeginEdit();
		}

		/// <summary>
		/// The users chooses to add a new custom deco item
		/// </summary>
		private void cmCustomItem_Click(object sender, System.EventArgs e)
		{
			TheBox.Forms.Editors.QuickDeco form = new TheBox.Forms.Editors.QuickDeco();

			if ( form.ShowDialog() == DialogResult.OK )
			{
				BoxDeco deco = form.Deco;

				tDeco.Nodes.Add( deco.TreeNode );

				( tCat.SelectedNode.Tag as ArrayList ).Add( deco );
				( tCat.SelectedNode.Tag as ArrayList ).Sort();

				int index = ( tCat.SelectedNode.Tag as ArrayList ).IndexOf( deco );

				TreeNode n = tCat.SelectedNode;
				tCat.SelectedNode = null;
				tCat.SelectedNode = n;

				tDeco.SelectedNode = tDeco.Nodes[ index ];

				Decorator.CustomDeco = tCat.Nodes[ 0 ];
			}
		}

		/// <summary>
		/// Deletes a category from the custom items
		/// </summary>
		private void cmCustomDel_Click(object sender, System.EventArgs e)
		{
			if ( MessageBox.Show( this, Pandora.TextProvider[ "Deco.ConfirmDelCat" ], "",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes )
			{
				tCat.SelectedNode.Parent.Nodes.Remove( tCat.SelectedNode );
				Decorator.CustomDeco = tCat.Nodes[ 0 ];
			}
		}

		#endregion

		/// <summary>
		/// The nudge value has been changed
		/// </summary>
		private void numNudge_ValueChanged(object sender, System.EventArgs e)
		{
			Pandora.Profile.Deco.NudgeAmount = (int) numNudge.Value;
		}

		/// <summary>
		/// Nudge Up Button
		/// </summary>
		private void bNudgeUp_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			PerformNudgeUp( null );
		}

		/// <summary>
		/// Nudge Down Button
		/// </summary>
		private void bNudgeDown_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			PerformNudgeDown( null );
		}

		private void PerformNudgeUp( string modifier )
		{
			Pandora.Profile.Deco.NudgeAmount = (int) numNudge.Value;
			Pandora.Profile.Commands.DoNudgeUp( Pandora.Profile.Deco.NudgeAmount, modifier );
		}

		private void PerformNudgeDown( string modifier )
		{
			Pandora.Profile.Deco.NudgeAmount = (int) numNudge.Value;
			Pandora.Profile.Commands.DoNudgeDown( Pandora.Profile.Deco.NudgeAmount, modifier );
		}

		/// <summary>
		/// Tile Height Changed
		/// </summary>
		private void numTile_ValueChanged(object sender, System.EventArgs e)
		{
			Pandora.Profile.Deco.TileHeight = (int) numTile.Value;
		}

		/// <summary>
		/// Add an item
		/// </summary>
		private void bAdd_Click(object sender, System.EventArgs e)
		{
			PerformAdd( null );
		}

		/// <summary>
		/// Adds a decoration item
		/// </summary>
		private void PerformAdd( string modifier )
		{
			if ( SelectedDeco != null )
			{
				Pandora.Profile.Deco.RandomizeAmount = (int) nRnd.Value;

				bool movable = !Pandora.Profile.Deco.Static;
				int hue = 0;

				if ( Pandora.Profile.Deco.Hued )
				{
					hue = Pandora.Profile.Hues.SelectedIndex;
				}

				Pandora.Profile.Commands.DoAddDeco( SelectedDeco.ID, movable, hue, Pandora.Profile.Deco.UseRandomizer ? Pandora.Profile.Deco.RandomizeAmount : 0, modifier );
			}
		}

		/// <summary>
		/// Performs the tile command
		/// </summary>
		private void PerformTile()
		{
			if ( SelectedDeco != null )
			{
				bool movable = !Pandora.Profile.Deco.Static;
				int hue = 0;

				if ( Pandora.Profile.Deco.Hued )
				{
					hue = Pandora.Profile.Hues.SelectedIndex;
				}

				Pandora.Profile.Commands.DoTile( Pandora.Profile.Deco.TileHeight, SelectedDeco.ID, movable, hue, Pandora.Profile.Deco.UseRandomizer ? Pandora.Profile.Deco.RandomizeAmount : 0 );
			}
		}

		/// <summary>
		/// Tile an item
		/// </summary>
		private void bTile_Click(object sender, System.EventArgs e)
		{
			Pandora.Profile.Deco.TileHeight = (int) numTile.Value;
			Pandora.Profile.Deco.RandomizeAmount = (int) nRnd.Value;
			PerformTile();
		}

		/// <summary>
		/// Updates the static option
		/// </summary>
		private void chkStatic_CheckedChanged(object sender, System.EventArgs e)
		{
			Pandora.Profile.Deco.Static = chkStatic.Checked;
		}

		/// <summary>
		/// Updates the hues option
		/// </summary>
		private void chkHued_CheckedChanged(object sender, System.EventArgs e)
		{
			Pandora.Profile.Deco.Hued = chkHued.Checked;

			if ( chkHued.Checked )
			{
				Pandora.Art.Hue = Pandora.Profile.Hues.SelectedIndex;
			}
			else
			{
				Pandora.Art.Hue = 0;
			}
		}

		/// <summary>
		/// Search!
		/// </summary>
		private void lnkFind_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			TheBox.Forms.SearchForm form = new TheBox.Forms.SearchForm( TheBox.Forms.SearchForm.SearchType.Deco );

			if ( form.ShowDialog() == DialogResult.OK )
			{
				if ( form.SearchForIDs && form.SearchID != -1 )
				{
					// Search for ID, this can have only one result
					FindID( form.SearchID );
				}
				else
				{
					if ( form.SearchString != null )
					{
						SearchFor( form.SearchString );
					}
				}
			}
		}

		/// <summary>
		/// Searches for an ID
		/// </summary>
		/// <param name="id">The ID to look for</param>
		private void FindID( int id )
		{
			TreeNode result = null;
			int index = -1;

			foreach ( TreeNode cat in tCat.Nodes )
			{
				foreach ( TreeNode sub in cat.Nodes )
				{
					foreach ( BoxDeco deco in sub.Tag as ArrayList )
					{
						if ( deco.ID == id )
						{
							result = sub;
							index = ( sub.Tag as ArrayList ).IndexOf( deco );
						}
					}
				}
			}

			if ( result != null )
			{
				tCat.SelectedNode = result;
				tDeco.SelectedNode = tDeco.Nodes[ index ];
			}
			else
			{
				MessageBox.Show( string.Format( Pandora.TextProvider[ "Deco.NoIDFound" ], id ) );
			}
		}

		/// <summary>
		/// Searches for a given item
		/// </summary>
		/// <param name="text">Part of the name of the item</param>
		private void SearchFor( string text )
		{
			text = text.ToLower();

			m_Results = new SearchResults();

			foreach ( TreeNode cat in tCat.Nodes )
			{
				foreach ( TreeNode sub in cat.Nodes )
				{
					foreach ( BoxDeco deco in sub.Tag as ArrayList )
					{
						if ( deco.Name.ToLower().IndexOf( text ) > -1 )
						{
							// Result
							Result r = new Result( sub, ( sub.Tag as ArrayList ).IndexOf( deco ) );
							m_Results.Add( r );
						}
					}
				}
			}

			if ( m_Results.Count > 0 )
			{
				ShowNextResult();
			}
			else
			{
				MessageBox.Show( Pandora.TextProvider[ "Deco.NoResults" ] );
			}
		}

		/// <summary>
		/// Displays the next search result
		/// </summary>
		private void ShowNextResult()
		{
			if ( m_Results != null )
			{
				Result r = m_Results.GetNext();

				if ( r != null )
				{
					tCat.SelectedNode = r.Node;
					tDeco.SelectedNode = tDeco.Nodes[ r.Index ];
				}
			}
		}

		/// <summary>
		/// Displays the previous seach result
		/// </summary>
		private void ShowPrevResult()
		{
			if ( m_Results != null )
			{
				Result r = m_Results.GetPrevious();

				if ( r != null )
				{
					tCat.SelectedNode = r.Node;
					tDeco.SelectedNode = tDeco.Nodes[ r.Index ];
				}
			}
		}

		/// <summary>
		/// Monitor keys for search results navigation
		/// </summary>
		public void DoKeys(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.F5 )
			{
				ShowPrevResult();
				e.Handled = true;
			}
			else if ( e.KeyCode == Keys.F8 )
			{
				ShowNextResult();
				e.Handled = true;
			}
			else if ( sender.Equals( tCat ) && e.KeyCode == Keys.Right && tDeco.Nodes.Count > 0 )
			{
				tDeco.Focus();
			}
			else if ( sender.Equals( tDeco ) && e.KeyCode == Keys.Left )
			{
				tCat.Focus();
			}
			else if ( sender.Equals( tCat ) && e.KeyCode == Keys.Enter )
			{
				TreeNode node = tCat.SelectedNode;

				if ( node != null )
				{
					if ( node.IsExpanded )
					{
						node.Collapse();
					}
					else
					{
						node.Expand();
					}
				}
			}
		}

		/// <summary>
		/// User has changed the selected hue
		/// </summary>
		private void Hues_HueChanged(object sender, EventArgs e)
		{
			if ( Pandora.Profile.Deco.Hued )
			{
				Pandora.Art.Hue = Pandora.Profile.Hues.SelectedIndex;
			}
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			Pandora.SendToUO( string.Format( "Set ItemID {0}", SelectedDeco.ID ), true );
		}

		private void cmItem_Popup(object sender, System.EventArgs e)
		{
			menuItem1.Enabled = SelectedDeco != null;
		}

		private Point m_DragPoint = Point.Empty;

		/// <summary>
		/// Mouse down: do drag and drop
		/// </summary>
		private void tDeco_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( e.Button == MouseButtons.Left )
			{
				m_DragPoint = new Point( e.X, e.Y );
			}
			else if ( e.Button == MouseButtons.Right )
			{
				tDeco.SelectedNode = tDeco.GetNodeAt( e.X, e.Y );
			}
		}

		/// <summary>
		/// Mouse move, if away from the start dragging point, do drag and drop
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tDeco_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( e.Button == MouseButtons.None )
				return;

			if ( Math.Abs( e.X - m_DragPoint.X ) > 5 || Math.Abs( e.Y - m_DragPoint.Y ) > 5 )
			{
				tDeco.SelectedNode = tDeco.GetNodeAt( m_DragPoint );

				if ( SelectedDeco != null )
				{
					tDeco.DoDragDrop( SelectedDeco, DragDropEffects.Copy );
				}
				m_DragPoint = Point.Empty;
			}
		}

		private void tDeco_DoubleClick(object sender, System.EventArgs e)
		{
			bAdd.PerformClick();
		}

		private void chkRnd_CheckedChanged(object sender, System.EventArgs e)
		{
			Pandora.Profile.Deco.UseRandomizer = chkRnd.Checked;
		}

		private void nRnd_ValueChanged(object sender, System.EventArgs e)
		{
			Pandora.Profile.Deco.RandomizeAmount = (int) nRnd.Value;
		}

		private void tCat_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			tCat.SelectedNode = tCat.GetNodeAt( e.X, e.Y );

			if ( tCat.SelectedNode == null )
				return;

			string cat = null;
			string sub = null;
			string data = null;

			if ( tCat.SelectedNode.Parent != null )
			{
				sub = tCat.SelectedNode.Text;
				cat = tCat.SelectedNode.Parent.Text;
				data = cat + "|" + sub;
			}
			else
			{
				cat = tCat.SelectedNode.Text;
				data = cat;
			}

			tCat.DoDragDrop( data, DragDropEffects.Copy );
			tCat.Focus();
		}
	}
}