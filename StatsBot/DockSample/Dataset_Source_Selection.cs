using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;


namespace DockSample
{
    public partial class Dataset_Source_Selection : Form
    {

        private List<StudyDataSet> AvaliableDatasets = new List<StudyDataSet>();

        public Dataset_Source_Selection(int source_num, List<StudyDataSet> avaliableDatasets)
        {
            AvaliableDatasets = avaliableDatasets;
            InitializeComponent();
            if (source_num == 1) //1st data source, remove join options and increase size of data viewer.
            {
                label6.Visible = false;
                cmb_Join.Visible = false;
                label7.Visible = false;
                cmb_JoinableDatasets.Visible = false;
                checkBox1.Visible = false;
                checkBox2.Visible = false;
                dataGridView2.Visible = false;
                PrimaryDataGridView1.Width = 828;
                textBox1.Visible = false;
                button2.Visible = false;
                textBox2.Visible = false;
                label4.Visible = false;
                cmb_Relationship.Visible = false;
                label3.Visible = false;
                richTextBox2.Visible = false;
            }

            //libnames
            List<string> datasetLibnames = new List<string>();
            foreach (StudyDataSet dataset in AvaliableDatasets)
            {
                datasetLibnames.Add(dataset.Libname.SASLibname.ToUpper());
            }
            var unique_datasetLibnames = new HashSet<string>(datasetLibnames);
            foreach (string s in unique_datasetLibnames)
            {
                cmb_Libname.Items.Add(s);
            }

        }

        private void loadDataTable()
        {
            //Proc Freq variable.
            List<string> sasProgram = new List<string>();
            StringBuilder sasCode = new StringBuilder();
            sasCode.Append("data loadData;");
            sasProgram.Add(sasCode.ToString());
            sasCode.Clear();
            sasCode.Append("   set " + cmb_Libname.SelectedItem.ToString() + "." + cmb_Dataset.SelectedItem.ToString() + ";");
            sasProgram.Add(sasCode.ToString());
            sasCode.Clear();
            sasCode.Append("run;");
            sasProgram.Add(sasCode.ToString());
            sasCode.Clear();
            //Delegate references to callback log and dataTable of non-uniques.
                //Instance.customLogNotificationReturn(HandleLogNotificationFeedback);
            Instance.dataTableReturn(setPrimaryTable);

            string where = "";
            if (richTextBox1.Text.Length >= 1)
                where = " WHERE " + richTextBox1.Text;
            string command = string.Format("select * from loadData" + where);
            Instance.CustomSASProgramToRun(sasProgram, command);
        }

        private void setPrimaryTable(DataTable dt)
        {
            PrimaryDataGridView1.DataSource = dt;
            checkedListBox1.Items.Clear();
            checkedListBox1.Items.AddRange(dt.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray());
        }

        private void selectAll()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadDataTable();
        }

        private void cmb_Libname_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_Dataset.Items.Clear();
            foreach (StudyDataSet dataset in AvaliableDatasets)
            {
                if (dataset.Libname.SASLibname.ToUpper() == cmb_Libname.SelectedItem.ToString())
                    cmb_Dataset.Items.Add(dataset.DatasetName.ToUpper());
            }

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
                selectAll();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //since event fires before event change, we can assume that if some value is changing and all values are currently
            //all TRUE, then 1 must be changing to FALSE, so uncheck.
            if (checkedListBox1.CheckedItems.Count == checkedListBox1.Items.Count)
                checkBox3.Checked = false;
        }

    }
}
