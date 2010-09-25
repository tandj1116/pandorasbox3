using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TheBox
{
    /// <summary>
    /// Attempt to create an scalable form by Tarion
    /// Using newer .net functions like:
    /// - partial class
    /// - ContextMenuStrip
    /// 
    /// The UserControls have to be rebuilded, too. This is not deployed until it is finished.
    /// </summary>
    public partial class BoxForm : Form
    {
        public BoxForm()
        {
            InitializeComponent();
        }
    }
}
