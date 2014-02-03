using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using DataGridViewAutoFilter;
using SAS;
using SASWorkspaceManager;

namespace DockSample
{
    public partial class DataView : DockContent
    {
        private BindingSource bindingSource1 = new BindingSource();
        public DataView()
        {
            InitializeComponent();
            dataGridView1.BindingContextChanged += new EventHandler(dataGridView1_BindingContextChanged);
            dataGridView1.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dataGridView1_DataBindingComplete);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SheetSelect sheetSelect = new SheetSelect();
            sheetSelect.Show();
            sheetSelect.WhoMadeMe(this);
        }
        public void loadData(DataTable table)
        {
            if (table != null)
                bindingSource1.DataSource = table;
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

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd1 = new SaveFileDialog();
            sfd1.Title = "Save DataSource";
            sfd1.Filter = "StatBot DataSource (*.statds)|*.statds";
            sfd1.DefaultExt = ".statdt";
            sfd1.InitialDirectory = Instance.study.StatBotFileLocation;
            if (sfd1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter file = File.CreateText(sfd1.FileName))
                {
                    Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                    serializer.Serialize(file, bindingSource1.DataSource);
                }
            }
        }
    }
}
