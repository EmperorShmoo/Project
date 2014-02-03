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

namespace DockSample
{
    public partial class KeyDataSet_Dynamic : DockContent
    {
        BindingSource bindingSource1 = new BindingSource();
        SasServer activeSession;
        DataTable issueTable = null;
        List<UniqueDataSetObject> keyVars;
        //BackgroundWorker bg;
        string[] nums = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" };
        DataGridViewComboBoxColumn cmCol;

        public KeyDataSet_Dynamic(SasServer session)
        {
            InitializeComponent();
            activeSession = session;
            cmCol = new DataGridViewComboBoxColumn();
            //Set libnames combobox to be libnames from SAS
            foreach (Libname libname in Instance.study.Libnames)
            {
                comboBox1.Items.Add(libname.SASLibname);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            string pathDirectory = Instance.study.Libnames[comboBox1.SelectedIndex].Directory;
            string[] files = Directory.GetFiles(pathDirectory, "*.sas7bdat");
            foreach (string s in files)
            {
                comboBox2.Items.Add(Path.GetFileNameWithoutExtension(s));
            }
        }

        private void btn_Load_Click(object sender, EventArgs e)
        {
            StringBuilder sasCode = new StringBuilder();
            sasCode.Append("proc contents data=" + comboBox1.SelectedItem.ToString() + "." + comboBox2.SelectedItem.ToString() + "  out=sb_cont;");
            sasCode.Append("run;");
            string command =  string.Format("select * from sb_cont");
            Instance.CustomSASCodeToRun(sasCode.ToString(), command);
            this.Text = (comboBox1.SelectedItem.ToString() + "." + comboBox2.SelectedItem.ToString() + " Key");
            Instance.dataTableReturn(ShowDatasetForKeying);
        }

        private void ShowDatasetForKeying(DataTable DT)
        {
            Cursor old = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                DataTable ds = DT;
                if (ds != null)
                {
                    bindingSource1.DataSource = ds;
                    dataGridView1.DataSource = bindingSource1;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (col.Name == "NAME" || col.Name == "TYPE" || col.Name == "LENGTH" || col.Name == "LABEL" || col.Name == "LABEL" || col.Name == "FORMAT" || col.Name == "SORTEDBY")
                        {
                            col.Visible = true;
                            col.ReadOnly = true;
                        }
                        else if (col.Name == cmCol.Name)
                            col.Visible = true;
                        else
                            col.Visible = false;
                    }
                    if (dataGridView1.Columns.Contains("KeyNumber") == false)  //add combo box only add key column the 1st load
                    {
                        cmCol.Name = "KeyNumber";
                        cmCol.HeaderText = "Key Number";
                        cmCol.Items.AddRange(nums);
                        dataGridView1.Columns.Add(cmCol);
                    }

                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (col.Name == cmCol.Name)
                        {
                            col.ReadOnly = false;
                            col.DisplayIndex = 0;
                            col.Frozen = true;
                        }
                    }
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (dataGridView1.Rows[row.Index].Cells["SORTEDBY"].Value.ToString() != "")
                            dataGridView1.Rows[row.Index].Cells[cmCol.Name].Value = dataGridView1.Rows[row.Index].Cells["SORTEDBY"].Value.ToString();
                    }
                }
                else MessageBox.Show("NO tables found!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error opening table");
            }
            finally
            {
                Cursor.Current = old;
            }
        }

        private void setIssueTable(DataTable dt)
        {
            issueTable = dt;
        }

        private void HandleLogNotificationFeedback(List<string> strings)
        {
            foreach (string s in strings)
            {
                if (s == "NOTE: 0 observations with duplicate key values were deleted.")
                {
                    MessageBox.Show("Unique Key Found!");
                    SaveKeysToStudy();
                }
                else if (s.Contains("observations with duplicate key values were deleted.") && s != "NOTE: 0 observations with duplicate key values were deleted.")
                {
                    MessageBox.Show("Unique Key Not Found.");
                    DataView dv = new DataView();
                    dv.Show();
                    dv.loadData(issueTable);
                }
            }
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

        private void btnTestKeys_Click(object sender, EventArgs e)
        {
        }


        private void button1_Click(object sender, EventArgs e)
        {

            keyVars = new List<UniqueDataSetObject>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (dataGridView1.Rows[row.Index].Cells[cmCol.Name].Value != null)
                {
                    UniqueDataSetObject storeMe = new UniqueDataSetObject
                    {
                        key = dataGridView1.Rows[row.Index].Cells["NAME"].Value.ToString(),
                        keyNum = Int32.Parse(dataGridView1.Rows[row.Index].Cells[cmCol.Name].Value.ToString())
                    };
                    keyVars.Add(storeMe);
                }
            }
            var sortedKeyVars = from element in keyVars
                                orderby element.keyNum
                                select element;
            //Construct SAS code to test the keys.
            if (keyVars.Count > 0)
            {
                StringBuilder sasCode = new StringBuilder();
                sasCode.Append("proc sort data=" + comboBox1.SelectedItem.ToString() + "." + comboBox2.SelectedItem.ToString() + "  out=sb_freq nodupkey dupout=sb_dupout;");
                sasCode.Append("   by ");
                foreach (UniqueDataSetObject value in sortedKeyVars)
                {
                    sasCode.Append(value.key + " ");
                }
                sasCode.Append(";");
                sasCode.Append("run;");
                sasCode.Append("data sb_issue;");
                sasCode.Append("   merge " + comboBox1.SelectedItem.ToString() + "." + comboBox2.SelectedItem.ToString() + " sb_dupout(in=in1);");
                sasCode.Append("   by ");
                foreach (UniqueDataSetObject value in sortedKeyVars)
                {
                    sasCode.Append(value.key + " ");
                }
                sasCode.Append(";");
                sasCode.Append("if in1;");
                sasCode.Append("run;");
                sasCode.Append("proc sql;   select count(*) into :nobs from sb_issue;   quit;");
                sasCode.Append("%macro a;");
                sasCode.Append("%if &nobs =0 %then %do;");
                sasCode.Append("   data sb_issue;");
                sasCode.Append("      info = 'UNIQUE!';");
                sasCode.Append("   run;");
                sasCode.Append("%end;");
                sasCode.Append("%mend a;");
                sasCode.Append("%a;");
                //Delegate references to callback log and dataTable of non-uniques.
                Instance.customLogNotificationReturn(HandleLogNotificationFeedback);
                Instance.dataTableReturn(setIssueTable);

                string command = string.Format("select * from sb_issue");
                Instance.CustomSASCodeToRun(sasCode.ToString(), command);
            }
            else
            {
                MessageBox.Show("At least 1 key variable must be provided for this dataset.");
            }
        }

        private void SaveKeysToStudy()
        {
            StudyDataSet studyDataSet = new StudyDataSet
            {
                Libname = Instance.study.Libnames[comboBox1.SelectedIndex],
                DatasetName = comboBox2.SelectedItem.ToString(),
                Variables = new List<StudyDataSetVariable>()
            };

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool isKey = false;
                int keynum = 0;
                foreach (UniqueDataSetObject keyVar in keyVars)
                {
                    if (keyVar.key == dataGridView1.Rows[row.Index].Cells["NAME"].Value.ToString())
                    {
                        isKey = true;
                        keynum = keyVar.keyNum;
                    }

                }
                StudyDataSetVariable var = new StudyDataSetVariable
                {
                    VarName = dataGridView1.Rows[row.Index].Cells["NAME"].Value.ToString(),
                    VarType = dataGridView1.Rows[row.Index].Cells["TYPE"].Value.ToString(),
                    VarLength = dataGridView1.Rows[row.Index].Cells["LENGTH"].Value.ToString(),
                    VarNum = dataGridView1.Rows[row.Index].Cells["VARNUM"].Value.ToString(),
                    VarLabel = dataGridView1.Rows[row.Index].Cells["LABEL"].Value.ToString(),
                    VarForamt = dataGridView1.Rows[row.Index].Cells["FORMAT"].Value.ToString(),
                    VarFormatLength = dataGridView1.Rows[row.Index].Cells["FORMATL"].Value.ToString(),
                    VarFormatDecimal = dataGridView1.Rows[row.Index].Cells["FORMATD"].Value.ToString(),
                    VarInformat = dataGridView1.Rows[row.Index].Cells["INFORMAT"].Value.ToString(),
                    VarInformatLength = dataGridView1.Rows[row.Index].Cells["INFORML"].Value.ToString(),
                    VarInformatDecimal = dataGridView1.Rows[row.Index].Cells["INFORMD"].Value.ToString(),
                    IsKeyVar = isKey,
                    KeyNum = keynum
                };
                studyDataSet.Variables.Add(var);
            }
            Instance.study.Datasets.Add(studyDataSet);
            Instance.study.SaveUpdatedStudy();
        }

        private void comboBox2_DrawItem(object sender, DrawItemEventArgs e)
        {

            // Get the item text
            string text = ((ComboBox)sender).Items[e.Index].ToString();

            // Determine the forecolor based on whether or not the item is selected    
            Brush brush = Brushes.Black;
            Brush backgroundColor = Brushes.Transparent;
            foreach (StudyDataSet dataset in Instance.study.Datasets)
            {
                if (dataset.DatasetName == text)
                {
                    backgroundColor = Brushes.Green;
                }
            }
            // Draw the background 
            e.Graphics.FillRectangle(backgroundColor, e.Bounds);
            //e.DrawBackground();

            // Draw the text    
            e.Graphics.DrawString(text, ((Control)sender).Font, brush, e.Bounds.X, e.Bounds.Y);
        }
    }

    class UniqueDataSetObject
    {
        public string key { get; set; }
        public int keyNum { get; set; }
    }
}
