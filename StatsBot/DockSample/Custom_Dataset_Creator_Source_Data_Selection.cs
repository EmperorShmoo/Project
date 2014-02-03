using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataGridViewAutoFilter;

namespace DockSample
{
    public partial class Custom_Dataset_Creator_Source_Data_Selection : Form
    {
        private BindingSource bindingSource1 = new BindingSource();
        public Custom_Dataset_Creator_Source_Data_Selection()
        {
            InitializeComponent();
            dataGridView1.BindingContextChanged += new EventHandler(dataGridView1_BindingContextChanged);
            dataGridView1.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dataGridView1_DataBindingComplete);
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

        public void loadData(DataTable table)
        {
            if (table != null)
                bindingSource1.DataSource = table;
            dataGridView1.DataSource = bindingSource1;
        }


    }
}
