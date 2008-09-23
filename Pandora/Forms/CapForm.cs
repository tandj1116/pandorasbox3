using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace TheBox.Forms
{
	/// <summary>
	/// Summary description for CapForm.
	/// </summary>
	public class CapForm : System.Windows.Forms.Form
	{
		#region ImageInfo

		/// <summary>
		/// Defines an image in a file
		/// </summary>
		private class ImageInfo
		{
			private string m_File;

			public ImageInfo( string file )
			{
				m_File = file;
			}

			/// <summary>
			/// Gets the image
			/// </summary>
			public Image Image
			{
				get
				{
					Image img = null;

					if ( File.Exists( m_File ) )
					{
						try
						{
							img = Image.FromFile( m_File );
						}
						catch {}
					}

					return img;
				}
			}

			/// <summary>
			/// Gets the name without extension of the file
			/// </summary>
			public string Name
			{
				get
				{
					return Path.GetFileNameWithoutExtension( m_File );
				}
			}

			/// <summary>
			/// Renames a file
			/// </summary>
			/// <param name="newName">The new name of the file</param>
			/// <returns>True if succesful</returns>
			public bool Rename( string newName )
			{
				if ( !File.Exists( m_File ) )
				{
					return false;
				}

				newName += ".jpg";

				string folder = Path.GetDirectoryName( m_File );

				newName = Path.Combine( folder, newName );

				try
				{
					File.Move( m_File, newName );
				}
				catch
				{
					return false;
				}

				return true;
			}

			/// <summary>
			/// Moves the file into the specified folder
			/// </summary>
			/// <param name="folder">The destination folder</param>
			/// <returns>True if the move has been succesful</returns>
			public bool MoveToFolder( string folder )
			{
				if ( !File.Exists( m_File ) )
				{
					return false;
				}

				string name = Path.GetFileName( m_File );

				string newPath = Path.Combine( folder, name );

				try
				{
					File.Move( m_File, newPath );
				}
				catch ( Exception err )
				{
					MessageBox.Show( err.ToString() );
					return false;
				}

				return true;
			}

			/// <summary>
			/// Deletes the file
			/// </summary>
			/// <returns>True if succesful</returns>
			public bool Delete()
			{
				if ( !File.Exists( m_File ) )
				{
					return true;
				}

				try
				{
					File.Delete( m_File );
				}
				catch
				{
					return false;
				}

				return true;
			}

			/// <summary>
			/// Opens the image
			/// </summary>
			public void Open()
			{
				try
				{
					System.Diagnostics.Process.Start( m_File );
				}
				catch ( Exception err )
				{
					Pandora.Log.WriteError( err, "An error occurred when trying to open the file {0}", m_File );
				}
			}

			/// <summary>
			/// Saves the image to a different file
			/// </summary>
			/// <param name="file">The destination filename</param>
			public void SaveAs( string file )
			{
				if ( File.Exists( m_File ) )
				{
					try
					{
						File.Copy( m_File, file, true );
					}
					catch ( Exception err )
					{
						Pandora.Log.WriteError( err, "Couldn't copy {0} to {1}", m_File, file );
					}
				}
			}
		}

		#endregion

		private System.Windows.Forms.TreeView tCat;
		private System.Windows.Forms.TreeView tImg;
		private System.Windows.Forms.Button bRefresh;
		private System.Windows.Forms.PictureBox pct;
		private System.Windows.Forms.Button bClose;
		private System.Windows.Forms.ImageList imgList;
		private System.Windows.Forms.ContextMenu cmCat;
		private System.Windows.Forms.ContextMenu cmImg;
		private System.Windows.Forms.MenuItem miCatRename;
		private System.Windows.Forms.MenuItem miCatDelete;
		private System.Windows.Forms.MenuItem miCatCreate;
		private System.Windows.Forms.MenuItem miImgRename;
		private System.Windows.Forms.MenuItem miImgDelete;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem miImgOpen;
		private System.Windows.Forms.MenuItem miImgSave;
		private System.Windows.Forms.SaveFileDialog SaveFile;
		private System.Windows.Forms.Button bFolder;
		private System.ComponentModel.IContainer components;

		public CapForm()
		{
			InitializeComponent();

			Pandora.LocalizeControl( this );
			Pandora.LocalizeMenu( cmCat );
			Pandora.LocalizeMenu( cmImg );
		}

		private ImageInfo m_Image;

		/// <summary>
		/// Gets or sets the currently viewed image
		/// </summary>
		private ImageInfo CurrentImage
		{
			get { return m_Image; }
			set
			{
				m_Image = value;

				if ( m_Image != null )
				{
					Image img = m_Image.Image;
					pct.Image = new Bitmap( img );
					img.Dispose();
				}
				else
				{
					pct.Image = null;
				}
			}
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CapForm));
			this.tCat = new System.Windows.Forms.TreeView();
			this.cmCat = new System.Windows.Forms.ContextMenu();
			this.miCatRename = new System.Windows.Forms.MenuItem();
			this.miCatDelete = new System.Windows.Forms.MenuItem();
			this.miCatCreate = new System.Windows.Forms.MenuItem();
			this.imgList = new System.Windows.Forms.ImageList(this.components);
			this.tImg = new System.Windows.Forms.TreeView();
			this.cmImg = new System.Windows.Forms.ContextMenu();
			this.miImgRename = new System.Windows.Forms.MenuItem();
			this.miImgDelete = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.miImgOpen = new System.Windows.Forms.MenuItem();
			this.miImgSave = new System.Windows.Forms.MenuItem();
			this.bRefresh = new System.Windows.Forms.Button();
			this.pct = new System.Windows.Forms.PictureBox();
			this.bClose = new System.Windows.Forms.Button();
			this.SaveFile = new System.Windows.Forms.SaveFileDialog();
			this.bFolder = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tCat
			// 
			this.tCat.AllowDrop = true;
			this.tCat.ContextMenu = this.cmCat;
			this.tCat.ImageIndex = 1;
			this.tCat.ImageList = this.imgList;
			this.tCat.Location = new System.Drawing.Point(4, 4);
			this.tCat.Name = "tCat";
			this.tCat.SelectedImageIndex = 1;
			this.tCat.Size = new System.Drawing.Size(128, 176);
			this.tCat.Sorted = true;
			this.tCat.TabIndex = 0;
			this.tCat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tCat_KeyDown);
			this.tCat.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tCat_AfterSelect);
			this.tCat.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tCat_AfterLabelEdit);
			this.tCat.DragEnter += new System.Windows.Forms.DragEventHandler(this.tCat_DragEnter);
			this.tCat.DragDrop += new System.Windows.Forms.DragEventHandler(this.tCat_DragDrop);
			// 
			// cmCat
			// 
			this.cmCat.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				  this.miCatRename,
																				  this.miCatDelete,
																				  this.miCatCreate});
			this.cmCat.Popup += new System.EventHandler(this.cmCat_Popup);
			// 
			// miCatRename
			// 
			this.miCatRename.Index = 0;
			this.miCatRename.Text = "Common.Rename";
			this.miCatRename.Click += new System.EventHandler(this.miCatRename_Click);
			// 
			// miCatDelete
			// 
			this.miCatDelete.Index = 1;
			this.miCatDelete.Text = "Common.Delete";
			this.miCatDelete.Click += new System.EventHandler(this.miCatDelete_Click);
			// 
			// miCatCreate
			// 
			this.miCatCreate.Index = 2;
			this.miCatCreate.Text = "Cap.CreateFold";
			this.miCatCreate.Click += new System.EventHandler(this.miCatCreate_Click);
			// 
			// imgList
			// 
			this.imgList.ImageSize = new System.Drawing.Size(16, 16);
			this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
			this.imgList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tImg
			// 
			this.tImg.ContextMenu = this.cmImg;
			this.tImg.HideSelection = false;
			this.tImg.ImageList = this.imgList;
			this.tImg.Location = new System.Drawing.Point(136, 4);
			this.tImg.Name = "tImg";
			this.tImg.ShowLines = false;
			this.tImg.ShowPlusMinus = false;
			this.tImg.ShowRootLines = false;
			this.tImg.Size = new System.Drawing.Size(128, 176);
			this.tImg.Sorted = true;
			this.tImg.TabIndex = 1;
			this.tImg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tImg_KeyDown);
			this.tImg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tImg_MouseDown);
			this.tImg.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tImg_AfterSelect);
			this.tImg.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tImg_AfterLabelEdit);
			this.tImg.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tImg_MouseMove);
			// 
			// cmImg
			// 
			this.cmImg.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				  this.miImgRename,
																				  this.miImgDelete,
																				  this.menuItem1,
																				  this.miImgOpen,
																				  this.miImgSave});
			this.cmImg.Popup += new System.EventHandler(this.cmImg_Popup);
			// 
			// miImgRename
			// 
			this.miImgRename.Index = 0;
			this.miImgRename.Text = "Common.Rename";
			this.miImgRename.Click += new System.EventHandler(this.miImgRename_Click);
			// 
			// miImgDelete
			// 
			this.miImgDelete.Index = 1;
			this.miImgDelete.Text = "Common.Delete";
			this.miImgDelete.Click += new System.EventHandler(this.miImgDelete_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 2;
			this.menuItem1.Text = "-";
			// 
			// miImgOpen
			// 
			this.miImgOpen.Index = 3;
			this.miImgOpen.Text = "Common.Open";
			this.miImgOpen.Click += new System.EventHandler(this.miImgOpen_Click);
			// 
			// miImgSave
			// 
			this.miImgSave.Index = 4;
			this.miImgSave.Text = "Cap.SaveAs";
			this.miImgSave.Click += new System.EventHandler(this.miImgSave_Click);
			// 
			// bRefresh
			// 
			this.bRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bRefresh.Location = new System.Drawing.Point(268, 4);
			this.bRefresh.Name = "bRefresh";
			this.bRefresh.Size = new System.Drawing.Size(52, 23);
			this.bRefresh.TabIndex = 2;
			this.bRefresh.Text = "Common.Refresh";
			this.bRefresh.Click += new System.EventHandler(this.bRefresh_Click);
			// 
			// pct
			// 
			this.pct.Location = new System.Drawing.Point(268, 28);
			this.pct.Name = "pct";
			this.pct.Size = new System.Drawing.Size(200, 150);
			this.pct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pct.TabIndex = 3;
			this.pct.TabStop = false;
			this.pct.Paint += new System.Windows.Forms.PaintEventHandler(this.pct_Paint);
			// 
			// bClose
			// 
			this.bClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bClose.Location = new System.Drawing.Point(416, 4);
			this.bClose.Name = "bClose";
			this.bClose.Size = new System.Drawing.Size(48, 23);
			this.bClose.TabIndex = 4;
			this.bClose.Text = "Common.Close";
			this.bClose.Click += new System.EventHandler(this.bClose_Click);
			// 
			// SaveFile
			// 
			this.SaveFile.Filter = "JPG Image (*.jpg)|*.jpg";
			// 
			// bFolder
			// 
			this.bFolder.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bFolder.Location = new System.Drawing.Point(328, 4);
			this.bFolder.Name = "bFolder";
			this.bFolder.Size = new System.Drawing.Size(80, 23);
			this.bFolder.TabIndex = 5;
			this.bFolder.Text = "Open Folder";
			this.bFolder.Click += new System.EventHandler(this.bFolder_Click);
			// 
			// CapForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(470, 181);
			this.Controls.Add(this.bFolder);
			this.Controls.Add(this.bClose);
			this.Controls.Add(this.pct);
			this.Controls.Add(this.bRefresh);
			this.Controls.Add(this.tImg);
			this.Controls.Add(this.tCat);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.Name = "CapForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Cap.Title";
			this.Load += new System.EventHandler(this.CapForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Draw border for image
		/// </summary>
		private void pct_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			TheBox.Common.Utility.DrawBorder( pct, e.Graphics );
		}

		/// <summary>
		/// Close
		/// </summary>
		private void bClose_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Reloads the trees
		/// </summary>
		private void RefreshTrees()
		{			
			tCat.BeginUpdate();
			tImg.BeginUpdate();

			tCat.Nodes.Clear();
			tImg.Nodes.Clear();

			tCat.Nodes.Add( GetDirectoryNode( Pandora.Profile.Screenshots.BaseFolder ) );
			tCat.Nodes[ 0 ].Expand();

			tCat.EndUpdate();
			tImg.EndUpdate();
		}

		private string GetDirName( string path )
		{
			string[] items = path.Split( Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar );

			if ( items.Length == 0 )
			{
				return "";
			}

			return items[ items.Length - 1 ];
		}

		/// <summary>
		/// Gets a TreeNode corresponding to a specified folder
		/// </summary>
		/// <param name="path">The folder to examine</param>
		/// <returns>A TreeNode object</returns>
		private TreeNode GetDirectoryNode( string path )
		{
			string[] folders = Directory.GetDirectories( path );

			TreeNode node = new TreeNode( GetDirName( path ) );
			node.Tag = path;

			foreach( string folder in folders )
			{
				node.Nodes.Add( GetDirectoryNode( folder ) );
			}

			return node;
		}

		/// <summary>
		/// Fills the images tree
		/// </summary>
		/// <param name="path">The current path</param>
		private void FillImageTree( string path )
		{
			tImg.BeginUpdate();
			tImg.Nodes.Clear();

			if ( path != null )
			{
				string[] files = Directory.GetFiles( path );

				foreach( string file in files )
				{
					ImageInfo ii = new ImageInfo( file );

					TreeNode node = new TreeNode( ii.Name );
					node.Tag = ii;

					tImg.Nodes.Add( node );
				}
			}

			tImg.EndUpdate();
		}

		/// <summary>
		/// Cat tree selection
		/// </summary>
		private void tCat_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if ( e.Node == null )
			{
				FillImageTree( null );
				return;
			}

			string path = e.Node.Tag as string;
			FillImageTree( path );
		}

		/// <summary>
		/// Img tree selection
		/// </summary>
		private void tImg_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if ( e.Node != null )
			{
				CurrentImage = e.Node.Tag as ImageInfo;
			}
			else
			{
				CurrentImage = null;
			}
		}

		/// <summary>
		/// OnLoad refresh
		/// </summary>
		private void CapForm_Load(object sender, System.EventArgs e)
		{
			RefreshTrees();
		}

		/// <summary>
		/// RefreshTrees
		/// </summary>
		private void bRefresh_Click(object sender, System.EventArgs e)
		{
			RefreshTrees();
		}

		/// <summary>
		/// After Label Edit: rename folder
		/// </summary>
		private void tCat_AfterLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
		{
			if ( e.Label == null || e.Label.Length == 0 )
			{
				tCat.LabelEdit = false;
				e.CancelEdit = true;
				return;
			}

			string old = e.Node.Tag as string;
			string baseFolder = Path.GetDirectoryName( old );
			string newFolder = Path.Combine( baseFolder, e.Label );

			try
			{
				Directory.Move( old, newFolder );
				e.Node.Tag = newFolder;
			}
			catch
			{
				e.CancelEdit = true;
			}

			tCat.LabelEdit = false;
		}

		private void miCatRename_Click(object sender, System.EventArgs e)
		{
			if ( tCat.SelectedNode != null )
			{
				tCat.LabelEdit = true;
				tCat.SelectedNode.BeginEdit();
			}
		}

		private void miCatDelete_Click(object sender, System.EventArgs e)
		{
			string path = tCat.SelectedNode.Tag as string;

			if ( DeleteDirectory( path ) )
			{
				tCat.Nodes.Remove( tCat.SelectedNode );
				FillImageTree( null );
			}
		}

		private bool DeleteDirectory( string path )
		{
			if ( MessageBox.Show( this, Pandora.TextProvider[ "Cap.DelFolder" ], "",
				MessageBoxButtons.YesNo, MessageBoxIcon.Warning ) == DialogResult.Yes )
			{
				try
				{
					Directory.Delete( path, true );
				}
				catch ( Exception err )
				{
					Pandora.Log.WriteError( err, "Couldn't delete folder: {0}", path );
					return false;
				}

				return true;
			}

			return false;
		}

		private void miCatCreate_Click(object sender, System.EventArgs e)
		{
			string path = tCat.SelectedNode.Tag as string;
			path = Path.Combine( path, "NewFolder" );

			try
			{
				Directory.CreateDirectory( path );
				TreeNode node = new TreeNode( GetDirName( path ) );
				node.Tag = path;
				tCat.SelectedNode.Nodes.Add( node );
				tCat.SelectedNode.Expand();

				node.BeginEdit();
			}
			catch {}
		}

		private Point m_DragStart = Point.Empty;

		private void tImg_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			tImg.SelectedNode = tImg.GetNodeAt( e.X, e.Y );
			
			if ( tImg.SelectedNode == null )
			{
				return;
			}

			if ( e.Button == MouseButtons.Left && e.Clicks == 1 )
			{
				m_DragStart = new Point( e.X, e.Y );
			}
			else if ( e.Clicks == 2 )
			{
				( tImg.SelectedNode.Tag as ImageInfo ).Open();
			}
		}

		private void tImg_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( e.Button != MouseButtons.Left || tImg.SelectedNode == null )
				return;

			int dx = Math.Abs( e.X - m_DragStart.X );
			int dy = Math.Abs( e.Y - m_DragStart.Y );

			if ( dx > 5 || dy > 5 )
			{
				tImg.DoDragDrop( tImg.SelectedNode.Tag as ImageInfo, DragDropEffects.Move );
			}
		}

		private void tCat_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if ( tCat.SelectedNode != null && e.Data.GetData( typeof( ImageInfo ) ) is ImageInfo )
			{
				e.Effect = DragDropEffects.Move;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		private void tCat_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			tCat.SelectedNode = tCat.GetNodeAt( tCat.PointToClient( Control.MousePosition ) );
			TreeNode node = tCat.SelectedNode;

			if ( node == null )
				return;

			string path = node.Tag as string;
			ImageInfo ii = e.Data.GetData( typeof( ImageInfo ) ) as ImageInfo;

			ii.MoveToFolder( path );

			tCat.SelectedNode = null;
			tCat.SelectedNode = node;
			CurrentImage = null;
		}

		private void cmCat_Popup(object sender, System.EventArgs e)
		{
			foreach( MenuItem mi in cmCat.MenuItems )
			{
				mi.Enabled = false;
			}

			if ( tCat.SelectedNode == null )
				return;

			miCatCreate.Enabled = true;
			miCatDelete.Enabled = tCat.SelectedNode != tCat.Nodes[ 0 ];
			miCatRename.Enabled = tCat.SelectedNode != tCat.Nodes[ 0 ];
		}

		private void tCat_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			TreeNode node = tCat.SelectedNode;

			if ( node == null )
			{
				return;
			}

			switch ( e.KeyCode )
			{
				case Keys.Delete:

					if ( DeleteDirectory( node.Tag as string ) )
					{
						tCat.Nodes.Remove( tCat.SelectedNode );
						FillImageTree( null );
					}
					break;

				case Keys.F2:

					tCat.LabelEdit = true;
					node.BeginEdit();
					break;
			}
		}

		private void cmImg_Popup(object sender, System.EventArgs e)
		{
			tImg.SelectedNode = tImg.GetNodeAt( tImg.PointToClient( Control.MousePosition ) );

			foreach( MenuItem mi in cmImg.MenuItems )
			{
				mi.Enabled = tImg.SelectedNode != null;
			}
		}

		private void miImgRename_Click(object sender, System.EventArgs e)
		{
			tImg.LabelEdit = true;
			tImg.SelectedNode.BeginEdit();
		}

		private void tImg_AfterLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
		{
			if ( e.Label == null || e.Label.Length == 0  )
			{
				e.CancelEdit = true;
				tImg.LabelEdit = false;
				return;
			}

			ImageInfo ii = tImg.SelectedNode.Tag as ImageInfo;

			if ( !ii.Rename( e.Label ) )
			{
				e.CancelEdit = true;
			}

			tImg.LabelEdit = false;
		}

		private void miImgDelete_Click(object sender, System.EventArgs e)
		{
			ImageInfo ii = tImg.SelectedNode.Tag as ImageInfo;

			if ( ii.Delete() )
			{
				tImg.Nodes.Remove( tImg.SelectedNode );
			}
		}

		private void miImgOpen_Click(object sender, System.EventArgs e)
		{
			ImageInfo ii = tImg.SelectedNode.Tag as ImageInfo;
			ii.Open();
		}

		private void miImgSave_Click(object sender, System.EventArgs e)
		{
			if ( SaveFile.ShowDialog() == DialogResult.OK )
			{
				ImageInfo ii = tImg.SelectedNode.Tag as ImageInfo;
				ii.SaveAs( SaveFile.FileName );
			}
		}

		private void tImg_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			TreeNode node = tImg.SelectedNode;

			if ( node == null )
				return;

			ImageInfo ii = tImg.SelectedNode.Tag as ImageInfo;

			switch ( e.KeyCode )
			{
				case Keys.Delete:

					if ( ii.Delete() )
					{
						tImg.Nodes.Remove( tImg.SelectedNode );
					}
					break;

				case Keys.F2:

					tImg.LabelEdit = true;
					tImg.SelectedNode.BeginEdit();
					break;
			}
		}

		private void bFolder_Click(object sender, System.EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start( Pandora.Profile.Screenshots.BaseFolder );
			}
			catch ( Exception err )
			{
				Pandora.Log.WriteError( err, "Unable to start the screenshots folder" );
			}
		}
	}
}