using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text.RegularExpressions;

namespace SoundExplorer
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
        //Kons - Issue 8 : http://code.google.com/p/pandorasbox3/issues/detail?id=8
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TreeView tree;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.SaveFileDialog save;
		private System.Windows.Forms.OpenFileDialog load;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.button1 = new System.Windows.Forms.Button();
			this.tree = new System.Windows.Forms.TreeView();
			this.button3 = new System.Windows.Forms.Button();
			this.save = new System.Windows.Forms.SaveFileDialog();
			this.load = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(8, 8);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "Load";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// tree
			// 
			this.tree.FullRowSelect = true;
			this.tree.HideSelection = false;
			this.tree.ImageIndex = -1;
			this.tree.LabelEdit = true;
			this.tree.Location = new System.Drawing.Point(8, 40);
			this.tree.Name = "tree";
			this.tree.SelectedImageIndex = -1;
			this.tree.Size = new System.Drawing.Size(248, 376);
			this.tree.Sorted = true;
			this.tree.TabIndex = 2;
			this.tree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tree_KeyDown);
			this.tree.DoubleClick += new System.EventHandler(this.tree_DoubleClick);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(184, 8);
			this.button3.Name = "button3";
			this.button3.TabIndex = 6;
			this.button3.Text = "Save";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// save
			// 
			this.save.Filter = "Xml Files (*.xml)|*.xml";
			// 
			// load
			// 
			this.load.Filter = "Xml Files (*.xml)|*.xml";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(264, 421);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.tree);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if ( load.ShowDialog() == DialogResult.OK )
			{
				SoundData data = TheBox.Common.Utility.LoadXml( typeof( SoundData ), load.FileName ) as SoundData;
            tree.Nodes.AddRange( data.TreeNodes );
			}
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
		
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			if ( save.ShowDialog() == DialogResult.OK )
			{
				SoundData data = new SoundData( tree.Nodes );

				TheBox.Common.Utility.SaveXml( data, save.FileName );
			}
		}

		private void tree_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ( tree.SelectedNode != null )
			{
				if ( e.KeyCode == Keys.F2 )
					tree.SelectedNode.BeginEdit();
				else if ( e.KeyCode == Keys.V && e.Modifiers == Keys.Control )
				{
					string text = System.Windows.Forms.Clipboard.GetDataObject().GetData( DataFormats.Text ).ToString();
					tree.SelectedNode.Text = string.Format( "{0} {1}", text, m_Index++ );

					tree.SelectedNode.BeginEdit();
				}
				else if ( e.KeyCode == Keys.C && e.Modifiers == Keys.Control )
				{
					string text = tree.SelectedNode.Text;
					m_Index = 1;

					Clipboard.SetDataObject( tree.SelectedNode.Text );

					tree.SelectedNode.Text = string.Format( "{0} {1}", text, m_Index++ );
				}
				else if ( e.KeyCode == Keys.D && e.Modifiers == Keys.Control )
				{
					string text = tree.SelectedNode.Text;
					char first = char.ToUpper( text[ 0 ] );
					text = text.Substring( 1 );
					text = string.Format( "{0}{1}", first.ToString(), text );
					tree.SelectedNode.Text = text;
				}
			}
		}

		private int m_Index = 1;

		private void button2_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( e.Button == MouseButtons.Right )
				tree.SelectedNode = null;
		}

		private void tree_DoubleClick(object sender, System.EventArgs e)
		{
			TreeNode node = tree.SelectedNode;

			if ( node == null || node.Tag != null )
				return;

			string name = node.Text;
			int index = 1;

			foreach ( TreeNode child in node.Nodes )
			{
				child.Text = string.Format( "{0} {1}", name, index++ );
			}
		}
	}
}