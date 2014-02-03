using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DockSample
{
    public partial class KeyDataSet_Tool : ToolWindow
    {
        public KeyDataSet_Tool()
        {
            InitializeComponent();
            /*foreach (Libname libname in Instance.study.Libnames)
            {
                comboBox1.Items.Add(libname.SASLibname);
            }*/
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
