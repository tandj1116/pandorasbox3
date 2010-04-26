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
using TheBox.Forms.Editors;

namespace TheBox.Pages
{
	/// <summary>
	/// Summary description for Items.
	/// </summary>
	public class Items : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Panel panelTrees;
		private System.Windows.Forms.TreeView tCat;
		private System.Windows.Forms.Splitter splitter;
		private System.Windows.Forms.TreeView tItems;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private TheBox.Controls.Params.ConstructorsViewer Ctors;
		private System.Windows.Forms.Button bAdd;
		private System.Windows.Forms.Button bToPack;
		private System.Windows.Forms.CheckBox chkCustomParams;
		private System.Windows.Forms.ComboBox cmbCustomParams;
		private System.Windows.Forms.ContextMenu cmCat;
		private System.Windows.Forms.MenuItem cmAddMainCat;
		private System.Windows.Forms.MenuItem cmAddSub;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem cmCatRename;
		private System.Windows.Forms.MenuItem cmCatDelete;
		private System.Windows.Forms.ContextMenu cmItems;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem cmAddItem;
		private System.Windows.Forms.MenuItem cmEditItem;
		private System.Windows.Forms.MenuItem cmDeleteItem;
		private System.Windows.Forms.MenuItem cmSetItemID;

		private BoxItem m_SelectedItem;
		private TheBox.Buttons.BoxButton boxButton1;
		private System.Windows.Forms.Button bDown;
		private System.Windows.Forms.NumericUpDown numNudge;
		private System.Windows.Forms.Button bUp;
		private System.Windows.Forms.MenuItem cmToSpawn;
		private System.Windows.Forms.Button bConfigSpawn;
		private TheBox.Common.SearchResults m_Results;
		private System.Windows.Forms.Button bFind;
		private System.Windows.Forms.Button bTile;
		private System.Windows.Forms.NumericUpDown nTile;
		private TheBox.Forms.SpawnForm m_SpawnForm;

		/// <summary>
		/// Gets or sets the item currently selected by the user
		/// </summary>
		private BoxItem SelectedItem
		{
			get { return m_SelectedItem; }
			set
			{
				m_SelectedItem = value;

				if ( m_SelectedItem != null )
				{
					Pandora.BoxForm.SelectSmallTab( SmallTabs.Art );

					Pandora.Art.ArtIndex = m_SelectedItem.Item.Art;
					Pandora.Art.Hue = m_SelectedItem.Item.Hue;

					Pandora.Profile.Items.ArtIndex = m_SelectedItem.Item.Art;
					Pandora.Profile.Items.ArtHue = m_SelectedItem.Item.Hue;
				}

				// Enable/disable buttons
				bAdd.Enabled = m_SelectedItem != null;
				bToPack.Enabled = m_SelectedItem != null;
				bConfigSpawn.Enabled = m_SelectedItem != null;
				bTile.Enabled = m_SelectedItem != null;

				Ctors.Item = m_SelectedItem;
			}
		}

		/// <summary>
		/// Sets the art index displayed by the items tab
		/// </summary>
		public static int ArtIndex
		{
			set
			{
				Pandora.Profile.Items.ArtIndex = value;
				Pandora.Art.ArtIndex = value;
			}
		}

		/// <summary>
		/// Sets the item hue displayed by the items tab
		/// </summary>
		public static int ArtHue
		{
			set
			{
				Pandora.Profile.Items.ArtHue = value;
				Pandora.Art.Hue = value;
			}
		}

		public Items()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Items));
			this.panelTrees = new System.Windows.Forms.Panel();
			this.tItems = new System.Windows.Forms.TreeView();
			this.cmItems = new System.Windows.Forms.ContextMenu();
			this.cmAddItem = new System.Windows.Forms.MenuItem();
			this.cmEditItem = new System.Windows.Forms.MenuItem();
			this.cmDeleteItem = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.cmSetItemID = new System.Windows.Forms.MenuItem();
			this.cmToSpawn = new System.Windows.Forms.MenuItem();
			this.splitter = new System.Windows.Forms.Splitter();
			this.tCat = new System.Windows.Forms.TreeView();
			this.cmCat = new System.Windows.Forms.ContextMenu();
			this.cmAddMainCat = new System.Windows.Forms.MenuItem();
			this.cmAddSub = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.cmCatRename = new System.Windows.Forms.MenuItem();
			this.cmCatDelete = new System.Windows.Forms.MenuItem();
			this.Ctors = new TheBox.Controls.Params.ConstructorsViewer();
			this.bAdd = new System.Windows.Forms.Button();
			this.bToPack = new System.Windows.Forms.Button();
			this.chkCustomParams = new System.Windows.Forms.CheckBox();
			this.cmbCustomParams = new System.Windows.Forms.ComboBox();
			this.boxButton1 = new TheBox.Buttons.BoxButton();
			this.bDown = new System.Windows.Forms.Button();
			this.numNudge = new System.Windows.Forms.NumericUpDown();
			this.bUp = new System.Windows.Forms.Button();
			this.bConfigSpawn = new System.Windows.Forms.Button();
			this.bTile = new System.Windows.Forms.Button();
			this.nTile = new System.Windows.Forms.NumericUpDown();
			this.bFind = new System.Windows.Forms.Button();
			this.panelTrees.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numNudge)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nTile)).BeginInit();
			this.SuspendLayout();
			// 
			// panelTrees
			// 
			this.panelTrees.Controls.Add(this.tItems);
			this.panelTrees.Controls.Add(this.splitter);
			this.panelTrees.Controls.Add(this.tCat);
			this.panelTrees.Location = new System.Drawing.Point(0, 0);
			this.panelTrees.Name = "panelTrees";
			this.panelTrees.Size = new System.Drawing.Size(284, 140);
			this.panelTrees.TabIndex = 0;
			// 
			// tItems
			// 
			this.tItems.AllowDrop = true;
			this.tItems.ContextMenu = this.cmItems;
			this.tItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tItems.HideSelection = false;
			this.tItems.ImageIndex = -1;
			this.tItems.Location = new System.Drawing.Point(135, 0);
			this.tItems.Name = "tItems";
			this.tItems.SelectedImageIndex = -1;
			this.tItems.ShowLines = false;
			this.tItems.ShowRootLines = false;
			this.tItems.Size = new System.Drawing.Size(149, 140);
			this.tItems.Sorted = true;
			this.tItems.TabIndex = 2;
			this.tItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.tItems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tItems_MouseDown);
			this.tItems.DoubleClick += new System.EventHandler(this.tItems_DoubleClick);
			this.tItems.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tItems_AfterSelect);
			this.tItems.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tItems_MouseMove);
			// 
			// cmItems
			// 
			this.cmItems.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																										  this.cmAddItem,
																										  this.cmEditItem,
																										  this.cmDeleteItem,
																										  this.menuItem10,
																										  this.cmSetItemID,
																										  this.cmToSpawn});
			this.cmItems.Popup += new System.EventHandler(this.cmItems_Popup);
			// 
			// cmAddItem
			// 
			this.cmAddItem.Index = 0;
			this.cmAddItem.Text = "Items.Add";
			this.cmAddItem.Click += new System.EventHandler(this.cmAddItem_Click);
			// 
			// cmEditItem
			// 
			this.cmEditItem.Index = 1;
			this.cmEditItem.Text = "Common.Edit";
			this.cmEditItem.Click += new System.EventHandler(this.cmEditItem_Click);
			// 
			// cmDeleteItem
			// 
			this.cmDeleteItem.Index = 2;
			this.cmDeleteItem.Text = "Common.Delete";
			this.cmDeleteItem.Click += new System.EventHandler(this.cmDeleteItem_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 3;
			this.menuItem10.Text = "-";
			// 
			// cmSetItemID
			// 
			this.cmSetItemID.Index = 4;
			this.cmSetItemID.Text = "Items.ItemID";
			this.cmSetItemID.Click += new System.EventHandler(this.cmSetItemID_Click);
			// 
			// cmToSpawn
			// 
			this.cmToSpawn.Index = 5;
			this.cmToSpawn.Text = "NPCs.ToSpawn";
			this.cmToSpawn.Click += new System.EventHandler(this.cmToSpawn_Click);
			// 
			// splitter
			// 
			this.splitter.Location = new System.Drawing.Point(132, 0);
			this.splitter.Name = "splitter";
			this.splitter.Size = new System.Drawing.Size(3, 140);
			this.splitter.TabIndex = 1;
			this.splitter.TabStop = false;
			this.splitter.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter_SplitterMoved);
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
			this.tCat.Size = new System.Drawing.Size(132, 140);
			this.tCat.Sorted = true;
			this.tCat.TabIndex = 0;
			this.tCat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.tCat.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tCat_AfterSelect);
			this.tCat.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tCat_AfterLabelEdit);
			// 
			// cmCat
			// 
			this.cmCat.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																										this.cmAddMainCat,
																										this.cmAddSub,
																										this.menuItem8,
																										this.cmCatRename,
																										this.cmCatDelete});
			this.cmCat.Popup += new System.EventHandler(this.cmCat_Popup);
			// 
			// cmAddMainCat
			// 
			this.cmAddMainCat.Index = 0;
			this.cmAddMainCat.Text = "NPCs.mCatAddCat";
			this.cmAddMainCat.Click += new System.EventHandler(this.cmAddMainCat_Click);
			// 
			// cmAddSub
			// 
			this.cmAddSub.Index = 1;
			this.cmAddSub.Text = "NPCs.mCatAddSub";
			this.cmAddSub.Click += new System.EventHandler(this.cmAddSub_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 2;
			this.menuItem8.Text = "-";
			// 
			// cmCatRename
			// 
			this.cmCatRename.Index = 3;
			this.cmCatRename.Text = "NPCs.mCatRename";
			this.cmCatRename.Click += new System.EventHandler(this.cmCatRename_Click);
			// 
			// cmCatDelete
			// 
			this.cmCatDelete.Index = 4;
			this.cmCatDelete.Text = "NPCs.mCatDelete";
			this.cmCatDelete.Click += new System.EventHandler(this.cmCatDelete_Click);
			// 
			// Ctors
			// 
			this.Ctors.Location = new System.Drawing.Point(288, 80);
			this.Ctors.Name = "Ctors";
			this.Ctors.Size = new System.Drawing.Size(204, 60);
			this.Ctors.TabIndex = 1;
			this.Ctors.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			// 
			// bAdd
			// 
			this.bAdd.Enabled = false;
			this.bAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bAdd.Location = new System.Drawing.Point(288, 0);
			this.bAdd.Name = "bAdd";
			this.bAdd.Size = new System.Drawing.Size(52, 23);
			this.bAdd.TabIndex = 2;
			this.bAdd.Text = "Common.Add";
			this.bAdd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.bAdd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bAdd_MouseDown);
			// 
			// bToPack
			// 
			this.bToPack.Enabled = false;
			this.bToPack.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bToPack.Location = new System.Drawing.Point(288, 28);
			this.bToPack.Name = "bToPack";
			this.bToPack.Size = new System.Drawing.Size(52, 23);
			this.bToPack.TabIndex = 3;
			this.bToPack.Text = "Items.ToPack";
			this.bToPack.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.bToPack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bToPack_MouseDown);
			// 
			// chkCustomParams
			// 
			this.chkCustomParams.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkCustomParams.Location = new System.Drawing.Point(292, 56);
			this.chkCustomParams.Name = "chkCustomParams";
			this.chkCustomParams.Size = new System.Drawing.Size(20, 20);
			this.chkCustomParams.TabIndex = 4;
			this.chkCustomParams.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.chkCustomParams.CheckedChanged += new System.EventHandler(this.chkCustomParams_CheckedChanged);
			// 
			// cmbCustomParams
			// 
			this.cmbCustomParams.Location = new System.Drawing.Point(312, 56);
			this.cmbCustomParams.Name = "cmbCustomParams";
			this.cmbCustomParams.Size = new System.Drawing.Size(180, 21);
			this.cmbCustomParams.TabIndex = 5;
			this.cmbCustomParams.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			// 
			// boxButton1
			// 
			this.boxButton1.AllowEdit = true;
			this.boxButton1.ButtonID = 47;
			this.boxButton1.Def = null;
			this.boxButton1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton1.IsActive = true;
			this.boxButton1.Location = new System.Drawing.Point(344, 28);
			this.boxButton1.Name = "boxButton1";
			this.boxButton1.Size = new System.Drawing.Size(40, 23);
			this.boxButton1.TabIndex = 7;
			this.boxButton1.Text = "boxButton1";
			// 
			// bDown
			// 
			this.bDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.bDown.Image = ((System.Drawing.Image)(resources.GetObject("bDown.Image")));
			this.bDown.Location = new System.Drawing.Point(408, 0);
			this.bDown.Name = "bDown";
			this.bDown.Size = new System.Drawing.Size(16, 23);
			this.bDown.TabIndex = 9;
			this.bDown.Click += new System.EventHandler(this.bDown_Click);
			// 
			// numNudge
			// 
			this.numNudge.Location = new System.Drawing.Point(428, 2);
			this.numNudge.Maximum = new System.Decimal(new int[] {
																					  127,
																					  0,
																					  0,
																					  0});
			this.numNudge.Name = "numNudge";
			this.numNudge.Size = new System.Drawing.Size(44, 20);
			this.numNudge.TabIndex = 10;
			this.numNudge.Value = new System.Decimal(new int[] {
																					127,
																					0,
																					0,
																					0});
			this.numNudge.ValueChanged += new System.EventHandler(this.numNudge_ValueChanged);
			// 
			// bUp
			// 
			this.bUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.bUp.Image = ((System.Drawing.Image)(resources.GetObject("bUp.Image")));
			this.bUp.Location = new System.Drawing.Point(476, 0);
			this.bUp.Name = "bUp";
			this.bUp.Size = new System.Drawing.Size(16, 23);
			this.bUp.TabIndex = 11;
			this.bUp.Click += new System.EventHandler(this.bUp_Click);
			// 
			// bConfigSpawn
			// 
			this.bConfigSpawn.Enabled = false;
			this.bConfigSpawn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.bConfigSpawn.Image = ((System.Drawing.Image)(resources.GetObject("bConfigSpawn.Image")));
			this.bConfigSpawn.Location = new System.Drawing.Point(476, 28);
			this.bConfigSpawn.Name = "bConfigSpawn";
			this.bConfigSpawn.Size = new System.Drawing.Size(16, 23);
			this.bConfigSpawn.TabIndex = 13;
			this.bConfigSpawn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bConfigSpawn_MouseDown);
			// 
			// bTile
			// 
			this.bTile.Enabled = false;
			this.bTile.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bTile.Location = new System.Drawing.Point(388, 28);
			this.bTile.Name = "bTile";
			this.bTile.Size = new System.Drawing.Size(36, 23);
			this.bTile.TabIndex = 14;
			this.bTile.Text = "Deco.Tile";
			this.bTile.Click += new System.EventHandler(this.bTile_Click);
			// 
			// nTile
			// 
			this.nTile.Location = new System.Drawing.Point(428, 30);
			this.nTile.Maximum = new System.Decimal(new int[] {
																				  127,
																				  0,
																				  0,
																				  0});
			this.nTile.Minimum = new System.Decimal(new int[] {
																				  128,
																				  0,
																				  0,
																				  -2147483648});
			this.nTile.Name = "nTile";
			this.nTile.Size = new System.Drawing.Size(44, 20);
			this.nTile.TabIndex = 15;
			this.nTile.Value = new System.Decimal(new int[] {
																				128,
																				0,
																				0,
																				-2147483648});
			this.nTile.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// bFind
			// 
			this.bFind.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bFind.Location = new System.Drawing.Point(344, 0);
			this.bFind.Name = "bFind";
			this.bFind.Size = new System.Drawing.Size(60, 23);
			this.bFind.TabIndex = 16;
			this.bFind.Text = "Common.Find";
			this.bFind.Click += new System.EventHandler(this.lnkFind_LinkClicked);
			// 
			// Items
			// 
			this.Controls.Add(this.bFind);
			this.Controls.Add(this.nTile);
			this.Controls.Add(this.bTile);
			this.Controls.Add(this.bConfigSpawn);
			this.Controls.Add(this.bUp);
			this.Controls.Add(this.numNudge);
			this.Controls.Add(this.bDown);
			this.Controls.Add(this.boxButton1);
			this.Controls.Add(this.cmbCustomParams);
			this.Controls.Add(this.chkCustomParams);
			this.Controls.Add(this.bToPack);
			this.Controls.Add(this.bAdd);
			this.Controls.Add(this.Ctors);
			this.Controls.Add(this.panelTrees);
			this.Name = "Items";
			this.Size = new System.Drawing.Size(496, 142);
			this.Load += new System.EventHandler(this.Items_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeys);
			this.panelTrees.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numNudge)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nTile)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Control OnLoad: apply the options
		/// </summary>
		private void Items_Load(object sender, System.EventArgs e)
		{
			if ( tCat.Nodes.Count > 0 )
				return;

			try
			{
				// Initialize delegates for the modifier enabled buttons
				bToPack.Tag = new CommandCallback( PerformAddToPack );
				bAdd.Tag = new CommandCallback( PerformAdd );
				bUp.Tag = new CommandCallback( PerformNudgeUp );
				bDown.Tag = new CommandCallback( PerformNudgeDown );

				Pandora.Localization.LocalizeMenu( cmItems );
				Pandora.Localization.LocalizeMenu( cmCat );

				bToPack.ContextMenu = Pandora.cmModifiers;
				bAdd.ContextMenu = Pandora.cmModifiers;
				bUp.ContextMenu = Pandora.cmModifiers;
				bDown.ContextMenu = Pandora.cmModifiers;

				// Display the items
				RefreshTrees();

				// Apply the options
				TheBox.Options.ItemsOptions Options = Pandora.Profile.Items;

				// Splitter: apply only if not default
				if ( Options.Splitter > 0 )
				{
					splitter.SplitPosition = Options.Splitter;
				}

				// Load recently used string lists
				chkCustomParams.Checked = Options.UseCustomParams;
				cmbCustomParams.Items.AddRange( Options.CustomParams.GetArray() );

				// Nudge
				numNudge.Value = Options.Nudge;
				nTile.Value = Options.Tile;
			}
			catch {} // VS
		}

		/// <summary>
		/// Refreshes the BoxData
		/// </summary>
		public void UpdateBoxData()
		{
			RefreshTrees();
		}

		/// <summary>
		/// Clears all the tree nodes and reloads the scripts from Pandora
		/// </summary>
		private void RefreshTrees()
		{
			tCat.BeginUpdate();
			tItems.BeginUpdate();

			tCat.Nodes.Clear();
			tItems.Nodes.Clear();

			tCat.Nodes.AddRange( Pandora.Items.GetNodes() );

			tCat.EndUpdate();
			tItems.EndUpdate();
		}

		/// <summary>
		/// Updates the items list
		/// </summary>
		/// <param name="items">A List of BoxItems objects</param>
		// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
		private void UpdateItems( List<object> items )
		// Issue 10 - End
		{
			if ( items != null && items.Count > 0 )
			{
				tItems.BeginUpdate();
				tItems.Nodes.Clear();

				TreeNode[] nodes = Pandora.Items.GetDataNodes( items );

				if ( nodes != null )
				{
					tItems.Nodes.AddRange( nodes );
				}

				tItems.EndUpdate();
			}
		}

		/// <summary>
		/// The splitter is moving update the options
		/// </summary>
		private void splitter_SplitterMoved(object sender, System.Windows.Forms.SplitterEventArgs e)
		{
			Pandora.Profile.Items.Splitter = splitter.SplitPosition;
		}

		/// <summary>
		/// User selects a category: refresh item list and clear art
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tCat_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			tItems.BeginUpdate();
			tItems.Nodes.Clear();
			tItems.EndUpdate();

			if ( e.Node != null )
			{
				// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
				UpdateItems( e.Node.Tag as List<object> );
				// Issue 10 - End
			}

			SelectedItem = null;
		}

		/// <summary>
		/// User selects an item: display the art and the constructors
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tItems_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if ( e.Node != null )
			{
				SelectedItem = e.Node.Tag as BoxItem;
			}
			else
			{
				SelectedItem = null;
			}
		}

		/// <summary>
		/// Updates the custom parameters displayed in the combo box
		/// </summary>
		private void UpdateCustomParams()
		{
			if ( cmbCustomParams.Text != null && cmbCustomParams.Text.Length > 0 )
			{
				Pandora.Profile.Items.CustomParams.AddString( cmbCustomParams.Text );

				cmbCustomParams.BeginUpdate();
				cmbCustomParams.Items.Clear();

				cmbCustomParams.Items.AddRange( Pandora.Profile.Items.CustomParams.GetArray() );

				cmbCustomParams.EndUpdate();
			}
		}

		#region Command Handlers

		/// <summary>
		/// Sends the AddToPack command to the server on the currently selected item
		/// </summary>
		/// <param name="modifier">Specifies a modifier global/local modifier for the command. Can be null.</param>
		private void PerformAddToPack( string modifier )
		{
			if ( m_SelectedItem != null )
			{
				string options = Ctors.Parameters;

				if ( chkCustomParams.Checked && cmbCustomParams.Text != null && cmbCustomParams.Text.Length > 0 )
				{
					// Use custom parameters
					if ( options != null )
					{
						Pandora.Profile.Commands.DoAddToPack( m_SelectedItem.Name, modifier, options, cmbCustomParams.Text );
					}
					else
					{
						Pandora.Profile.Commands.DoAddToPack( m_SelectedItem.Name, modifier, cmbCustomParams.Text );
					}

					// Update the text
					UpdateCustomParams();
				}
				else
				{
					// Don't use custom parameters
					if ( options != null )
					{
						Pandora.Profile.Commands.DoAddToPack( m_SelectedItem.Name, modifier, options );
					}
					else
					{
						Pandora.Profile.Commands.DoAddToPack( m_SelectedItem.Name, modifier );
					}
				}
			}
		}

		/// <summary>
		/// Sends the Add command to the server on the currently selected item
		/// </summary>
		private void PerformAdd( string modifier )
		{
			if ( m_SelectedItem != null )
			{
				string options = Ctors.Parameters;

				if ( chkCustomParams.Checked && cmbCustomParams.Text != null && cmbCustomParams.Text.Length > 0 )
				{
					// Use custom parameters
					if ( options != null )
					{
						Pandora.Profile.Commands.DoAddItem( m_SelectedItem.Name, modifier, options, cmbCustomParams.Text );
					}
					else
					{
						Pandora.Profile.Commands.DoAddItem( m_SelectedItem.Name, modifier, cmbCustomParams.Text );
					}

					// Update the text
					UpdateCustomParams();
				}
				else
				{
					// Don't use custom parameters
					if ( options != null )
					{
						Pandora.Profile.Commands.DoAddItem( m_SelectedItem.Name, modifier, options );
					}
					else
					{
						Pandora.Profile.Commands.DoAddItem( m_SelectedItem.Name, modifier );
					}
				}
			}
		}

		/// <summary>
		/// Performs a nudge up action
		/// </summary>
		/// <param name="modifier">The modifier for the command</param>
		private void PerformNudgeUp( string modifier )
		{
			Pandora.Profile.Items.Nudge = (int) numNudge.Value;
			Pandora.Profile.Commands.DoNudgeUp( Pandora.Profile.Items.Nudge, modifier );
		}

		/// <summary>
		/// Performs a nudge down action
		/// </summary>
		/// <param name="modifier">The modifier for the command</param>
		private void PerformNudgeDown( string modifier )
		{
			Pandora.Profile.Items.Nudge = (int) numNudge.Value;
			Pandora.Profile.Commands.DoNudgeDown( Pandora.Profile.Items.Nudge, modifier );
		}

		#endregion

		/// <summary>
		/// Check specifying to use additional custom parameters
		/// </summary>
		private void chkCustomParams_CheckedChanged(object sender, System.EventArgs e)
		{
			Pandora.Profile.Items.UseCustomParams = chkCustomParams.Checked;
		}

		/// <summary>
		/// User clicked the Add to pack button
		/// </summary>
		private void bToPack_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( e.Button == MouseButtons.Left )
			{
				PerformAddToPack( null );
			}
		}

		/// <summary>
		/// User clicked the Add button
		/// </summary>
		private void bAdd_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( e.Button == MouseButtons.Left )
			{
				PerformAdd( null );
			}
		}

		#region BoxData menu items

		/// <summary>
		/// Saves the data to the BoxData after the user has modified it
		/// </summary>
		private void UpdateData()
		{
			Pandora.Items.Update( tCat.Nodes );
		}

		/// <summary>
		/// Add a new subsection
		/// </summary>
		private void cmAddSub_Click(object sender, System.EventArgs e)
		{
			TreeNode node = new TreeNode( "NewSubsection" );
			// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
			node.Tag = new List<object>();
			// Issue 10 - End

			tCat.SelectedNode.Nodes.Add( node );

			tCat.LabelEdit = true;

			node.BeginEdit();
		}

		/// <summary>
		/// The user just renamed a node
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
				{
					e.Node.Text = e.Label;
				}

				tCat.Sorted = false;
				tCat.Sorted = true;

				tCat.SelectedNode = e.Node;

				UpdateData();
			}

			tCat.LabelEdit = false;
		}

		/// <summary>
		/// Renames the selected tree node
		/// </summary>
		private void cmCatRename_Click(object sender, System.EventArgs e)
		{
			tCat.LabelEdit = true;
			tCat.SelectedNode.BeginEdit();
		}

		/// <summary>
		/// Deletes the selected tree node
		/// </summary>
		private void cmCatDelete_Click(object sender, System.EventArgs e)
		{
			if ( MessageBox.Show( 
				this,
				Pandora.Localization.TextProvider[ "Items.DelCat" ],
				"",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning ) == DialogResult.Yes )
			{
				TreeNode next = tCat.SelectedNode.NextNode;
				if ( next == null )
					next = tCat.SelectedNode.PrevNode;

				tCat.Nodes.Remove( tCat.SelectedNode );

				UpdateData();

				tCat.SelectedNode = next;
			}
		}

		/// <summary>
		/// Add a new category to the list
		/// </summary>
		private void cmAddMainCat_Click(object sender, System.EventArgs e)
		{
			TreeNode node = new TreeNode( "NewCategory" );
			// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
			node.Tag = new List<object>();
			// Issue 10 - End

			tCat.Nodes.Add( node );
			tCat.LabelEdit = true;

			tCat.SelectedNode = node;
			node.BeginEdit();
		}

		/// <summary>
		/// Cateogry menu popup
		/// </summary>
		private void cmCat_Popup(object sender, System.EventArgs e)
		{
			cmAddSub.Enabled = tCat.SelectedNode != null;
			cmCatRename.Enabled = tCat.SelectedNode != null;
			cmCatDelete.Enabled = tCat.SelectedNode != null;
		}

		/// <summary>
		/// Items menu popup
		/// </summary>
		private void cmItems_Popup(object sender, System.EventArgs e)
		{
			cmAddItem.Enabled = tCat.SelectedNode != null;
			cmEditItem.Enabled = tItems.SelectedNode != null;
			cmDeleteItem.Enabled = tItems.SelectedNode != null;
			cmSetItemID.Enabled = tItems.SelectedNode != null;
			cmToSpawn.Enabled = tItems.SelectedNode != null;
		}

		/// <summary>
		/// Create a new item entry
		/// </summary>
		private void cmAddItem_Click(object sender, System.EventArgs e)
		{
			QuickItem form = new QuickItem();

			if ( form.ShowDialog() == DialogResult.OK )
			{
				// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
				( tCat.SelectedNode.Tag as List<object> ).Add( form.Item );
				// Issue 10 - End

				TreeNode catNode = tCat.SelectedNode;

				tCat.SelectedNode = null;
				tCat.SelectedNode = catNode;

				foreach ( TreeNode t in tItems.Nodes )
				{
					if ( t.Tag as BoxItem == form.Item )
					{
						tItems.SelectedNode = t;
						break;
					}
				}

				UpdateData();
			}
		}

		/// <summary>
		/// Edit an existing item
		/// </summary>
		private void cmEditItem_Click(object sender, System.EventArgs e)
		{
			if ( SelectedItem != null )
			{
				QuickItem form = new QuickItem();

				form.Item = SelectedItem;

				if ( form.ShowDialog() == DialogResult.OK )
				{
					TreeNode catNode = tCat.SelectedNode;

					tCat.SelectedNode = null;
					tCat.SelectedNode = catNode;

					foreach ( TreeNode t in tItems.Nodes )
					{
						if ( t.Tag as BoxItem == form.Item )
						{
							tItems.SelectedNode = t;
							break;
						}
					}

					UpdateData();
				}
			}
		}

		/// <summary>
		/// Delete an existing item
		/// </summary>
		private void cmDeleteItem_Click(object sender, System.EventArgs e)
		{
			if ( MessageBox.Show( this,
				Pandora.Localization.TextProvider[ "Items.DelItem" ],
				"",
				MessageBoxButtons.YesNo ) == DialogResult.Yes )
			{
				// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
				( tCat.SelectedNode.Tag as List<object> ).Remove( SelectedItem );
				// Issue 10 - End

				TreeNode next = tItems.SelectedNode.NextNode;

				if ( next == null )
				{
					next = tItems.SelectedNode.PrevNode;
				}

				tItems.Nodes.Remove( tItems.SelectedNode );

				if ( next != null )
				{
					tItems.SelectedNode = next;
				}

				UpdateData();
			}
		}

		#endregion

		/// <summary>
		/// Set the ItemID
		/// </summary>
		private void cmSetItemID_Click(object sender, System.EventArgs e)
		{
			Pandora.SendToUO( string.Format( "Set ItemID {0}", SelectedItem.ItemID ), true );

			Pandora.Prop.DisplayedProp = "ItemID";
			Pandora.Prop.DisplayedValue = SelectedItem.ItemID.ToString();
		}

		private void cmToSpawn_Click(object sender, System.EventArgs e)
		{
			Pandora.SendToUO( SelectedItem.Name, false );
		}

		#region Searching

		/// <summary>
		/// Search for an item
		/// </summary>
		private void lnkFind_LinkClicked(object sender, EventArgs e)
		{
			TheBox.Forms.SearchForm form = new TheBox.Forms.SearchForm( TheBox.Forms.SearchForm.SearchType.Item );

			if ( form.ShowDialog() == DialogResult.OK )
			{
				string text = form.SearchString.Replace( " ", "" );

				m_Results = TheBox.Data.TreeSearch.Find( tCat, text );

				if ( m_Results.Count == 0 )
				{
					MessageBox.Show( Pandora.Localization.TextProvider[ "Misc.NoResults" ] );
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
				
				foreach ( TreeNode node in tItems.Nodes )
				{
					// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert
					if ( ( node.Tag as BoxItem ) == ( tCat.SelectedNode.Tag as List<object> )[ res.Index ] as BoxItem )
					// Issue 10 - End
					{
						tItems.SelectedNode = node;
						break;
					}
				}
			}
			catch
			{
				MessageBox.Show( Pandora.Localization.TextProvider[ "Misc.SearchError" ] );
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

					if ( sender.Equals( tItems ) )
						tCat.Focus();
					break;

				case Keys.Right:

					if ( sender.Equals( tCat ) && tItems.Nodes.Count > 0 )
						tItems.Focus();
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
		/// User changes the amount to nudge
		/// </summary>
		private void numNudge_ValueChanged(object sender, System.EventArgs e)
		{
			Pandora.Profile.Items.Nudge = (int) numNudge.Value;
		}

		/// <summary>
		/// Nudge up button clicked
		/// </summary>
		private void bUp_Click(object sender, System.EventArgs e)
		{
			PerformNudgeUp( null );
		}

		/// <summary>
		/// Nudge down button clicked
		/// </summary>
		private void bDown_Click(object sender, System.EventArgs e)
		{
			PerformNudgeDown( null );
		}

		#region Spawning

		/// <summary>
		/// Shows the spawn configuration dialog
		/// </summary>
		private void ShowSpawnConfig()
		{
			if ( m_SpawnForm != null && m_SpawnForm.Visible )
				return;

			if ( m_SpawnForm != null && !m_SpawnForm.Disposing )
			{
				m_SpawnForm.Dispose();
				m_SpawnForm = null;
			}

			m_SpawnForm = new TheBox.Forms.SpawnForm();

			m_SpawnForm.Amount = Pandora.Profile.Items.Amount;
			m_SpawnForm.Range = Pandora.Profile.Items.Range;
			m_SpawnForm.MinDelay = Pandora.Profile.Items.MinDelay;
			m_SpawnForm.MaxDelay = Pandora.Profile.Items.MaxDelay;
			m_SpawnForm.Team = Pandora.Profile.Items.Team;
			m_SpawnForm.Extra = Pandora.Profile.Items.Extra;

			Point location = PointToScreen( new Point( 380, 28 ) );
			m_SpawnForm.Location = location;

			m_SpawnForm.Closed += new EventHandler(m_SpawnForm_Closed);
			m_SpawnForm.OnSpawn += new EventHandler(m_SpawnForm_OnSpawn);

			m_SpawnForm.Show();
		}

		/// <summary>
		/// Spawn confi closed: update options
		/// </summary>
		private void m_SpawnForm_Closed(object sender, EventArgs e)
		{
			Pandora.Profile.Items.Amount = m_SpawnForm.Amount;
			Pandora.Profile.Items.Range = m_SpawnForm.Range;
			Pandora.Profile.Items.MinDelay = m_SpawnForm.MinDelay;
			Pandora.Profile.Items.MaxDelay = m_SpawnForm.MaxDelay;
			Pandora.Profile.Items.Team = m_SpawnForm.Team;
			Pandora.Profile.Items.Extra = m_SpawnForm.Extra;
		}

		private void m_SpawnForm_OnSpawn(object sender, EventArgs e)
		{
			Pandora.Profile.Items.Amount = m_SpawnForm.Amount;
			Pandora.Profile.Items.Range = m_SpawnForm.Range;
			Pandora.Profile.Items.MinDelay = m_SpawnForm.MinDelay;
			Pandora.Profile.Items.MaxDelay = m_SpawnForm.MaxDelay;
			Pandora.Profile.Items.Team = m_SpawnForm.Team;
			Pandora.Profile.Items.Extra = m_SpawnForm.Extra;

			Pandora.Profile.Commands.DoSpawnItem( m_SelectedItem.Name );
		}

		private void bConfigSpawn_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( e.Button == MouseButtons.Left )
			{
				ShowSpawnConfig();
			}
			else
			{
				Pandora.Profile.Commands.DoSpawnItem( m_SelectedItem.Name );
			}
		}

		#endregion

		#region Tiling

		private void numericUpDown1_ValueChanged(object sender, System.EventArgs e)
		{
			Pandora.Profile.Items.Tile = (int) nTile.Value;
		}

		private void bTile_Click(object sender, System.EventArgs e)
		{
			if ( m_SelectedItem != null )
			{
				string options = Ctors.Parameters;
				string item = m_SelectedItem.Name;

				if ( options != null )
				{
					item += " " + options;
				}

				if ( chkCustomParams.Checked && cmbCustomParams.Text != null && cmbCustomParams.Text.Length > 0 )
				{
					item += " " + cmbCustomParams.Text;
					UpdateCustomParams();
				}

				Pandora.Profile.Commands.DoTileItem( Pandora.Profile.Items.Tile, item );
			}
		}

		#endregion

		private Point m_DragPoint = Point.Empty;

		/// <summary>
		/// Enable drag and drop
		/// </summary>
		private void tItems_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( e.Button == MouseButtons.Left )
			{
				m_DragPoint = new Point( e.X, e.Y );
			}
			else if ( e.Button == MouseButtons.Right )
			{
				tItems.SelectedNode = tItems.GetNodeAt( e.X, e.Y );
			}
		}

		/// <summary>
		/// Mouse move: start drag and drop if needed
		/// </summary>
		private void tItems_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( e.Button == MouseButtons.None )
				return;

			if ( Math.Abs( e.X - m_DragPoint.X ) > 5 || Math.Abs( e.Y - m_DragPoint.Y ) > 5 )
			{
				tItems.SelectedNode = tItems.GetNodeAt( m_DragPoint );

				if ( SelectedItem != null )
				{
					tItems.DoDragDrop( SelectedItem.Name, DragDropEffects.Copy );
				}
				m_DragPoint = Point.Empty;
			}
		}

		private void tItems_DoubleClick(object sender, System.EventArgs e)
		{
			bAdd.PerformClick();
		}
	}
}