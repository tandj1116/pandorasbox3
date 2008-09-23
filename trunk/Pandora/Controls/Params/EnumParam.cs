using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace TheBox.Controls.Params
{
	/// <summary>
	/// Summary description for EnumParam.
	/// </summary>
	public class EnumParam : System.Windows.Forms.UserControl, IParam
	{
		private System.Windows.Forms.Label labName;
		private System.Windows.Forms.ComboBox cmb;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public EnumParam()
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

				Pandora.ToolTip.SetToolTip( labName, null );
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
			this.labName = new System.Windows.Forms.Label();
			this.cmb = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// labName
			// 
			this.labName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labName.Location = new System.Drawing.Point(0, 0);
			this.labName.Name = "labName";
			this.labName.Size = new System.Drawing.Size(96, 16);
			this.labName.TabIndex = 0;
			// 
			// cmb
			// 
			this.cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb.Location = new System.Drawing.Point(0, 16);
			this.cmb.Name = "cmb";
			this.cmb.Size = new System.Drawing.Size(96, 21);
			this.cmb.TabIndex = 1;
			this.cmb.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
			// 
			// EnumParam
			// 
			this.Controls.Add(this.cmb);
			this.Controls.Add(this.labName);
			this.Name = "EnumParam";
			this.Size = new System.Drawing.Size(96, 36);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Occurs when an enum value has been changed
		/// </summary>
		public event EventHandler EnumValueChanged;

		private void cmb_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( EnumValueChanged != null )
			{
				EnumValueChanged( this, new EventArgs() );
			}
		}

		/// <summary>
		/// Sets the list of the possible values for an enum
		/// </summary>
		public ArrayList EnumValues
		{
			set
			{
				if ( value != null && value.Count > 0 )
				{
					cmb.BeginUpdate();

					cmb.Items.Clear();

					foreach ( string s in value )
					{
						cmb.Items.Add( s ); 
					}

					cmb.EndUpdate();
					cmb.SelectedIndex = 0;
				}
			}
		}

		#region IParam Members

		public string ParamName
		{
			set
			{
				labName.Text = value;
				Pandora.ToolTip.SetToolTip( labName, value );
			}
		}

		public string Value
		{
			get
			{
				return cmb.SelectedItem as string;
			}
		}

		public bool IsDefined
		{
			get
			{
				return true;
			}
		}

		#endregion
	}
}
