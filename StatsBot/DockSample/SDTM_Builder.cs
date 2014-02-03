using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Newtonsoft.Json;
using System.IO;
using DataGridViewAutoFilter;

namespace DockSample
{
    public partial class SDTM_Builder : DockContent
    {
        private BindingSource bindingSource1 = new BindingSource();
        public SDTM_Builder()
        {
            InitializeComponent();
            dataGridView1.BindingContextChanged += new EventHandler(dataGridView1_BindingContextChanged);
            dataGridView1.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dataGridView1_DataBindingComplete);
            using (StreamReader file = File.OpenText("SDTM_Template.statds"))
            {
                JsonSerializer serializer = new JsonSerializer();
                bindingSource1.DataSource = (DataTable)serializer.Deserialize(file, typeof(DataTable));
            }
            dataGridView1.DataSource = bindingSource1;
        }

        private void dataGridView1_DataBindingComplete(object sender,
    DataGridViewBindingCompleteEventArgs e)
        {
            String filterStatus = DataGridViewAutoFilterColumnHeaderCell
                .GetFilterStatus(dataGridView1);
        }

        private void dataGridView1_BindingContextChanged(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null) return;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderCell = new
                    DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
            }
            dataGridView1.AutoResizeColumns();
        }
    }
}
