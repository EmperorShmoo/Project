using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DockSample
{
    public partial class Custom_Dataset_Creator : Form
    {
        private List<StudyDataSet> AvaliableDatasets = new List<StudyDataSet>();
        private List<Libname> AvaliableLibnames = new List<Libname>();
        private int rowCounter;
        private SDTM_Domain Domain;
        private string selectedLibname;
        private int colCounter;

        public Custom_Dataset_Creator(SDTM_Domain domain)
        {
            InitializeComponent();
            Domain = domain;
            rowCounter = 0;
            colCounter = 0;
            setupLibnamesAndDatasetLists();
            /*Button joinButton = new Button();
            joinButton.Text = "Join New Datasource";
            tableLayoutPanel1.Controls.Add(joinButton, 0, rowCounter);
            rowCounter++;
            tableLayoutPanel1.Controls.Add(new Label { Text = "Source 1: " }, 0, rowCounter);
            tableLayoutPanel1.Controls.Add(makeLibnameComboBox(rowCounter.ToString()), 1, rowCounter);
            tableLayoutPanel1.Controls.Add(makeDatasetComboBox(rowCounter.ToString()), 2, rowCounter);
            rowCounter++;
             */
            makeJoinColumn();
             
        }

        private void makeJoinColumn()
        {
            Button joinButton = new Button();
            joinButton.Text = "Join New Datasource";
            joinButton.Name = "joinButton" + colCounter.ToString();
            joinButton.Dock = DockStyle.Fill;
            joinButton.Click += new System.EventHandler(joinbutton_clk);
            tableLayoutPanel1.Controls.Add(joinButton, colCounter, 0);
            tableLayoutPanel1.SetColumnSpan(joinButton, 5);

            tableLayoutPanel1.Controls.Add(new Label { Text = "Select: ", Dock=DockStyle.Fill}, colCounter, 1);
            RichTextBox selectBox = new RichTextBox();
            selectBox.ReadOnly = true;
            selectBox.Name = "selectBox" + colCounter.ToString();
            selectBox.Dock = DockStyle.Fill;
            selectBox.Height = 78;
            tableLayoutPanel1.Controls.Add(selectBox, colCounter + 1, 1);
            tableLayoutPanel1.SetColumnSpan(selectBox, 4);

            tableLayoutPanel1.Controls.Add(new Label { Text = "Where: ", Dock = DockStyle.Fill }, colCounter, 2);
            RichTextBox whereBox = new RichTextBox();
            whereBox.ReadOnly = true;
            whereBox.Dock = DockStyle.Fill;
            whereBox.Height = 78;
            whereBox.Name = "whereBox" + colCounter.ToString();
            tableLayoutPanel1.Controls.Add(whereBox, colCounter + 1, 2);
            tableLayoutPanel1.SetColumnSpan(whereBox, 4);

            tableLayoutPanel1.Controls.Add(new Label { Text = "Group By: ", Dock = DockStyle.Fill }, colCounter, 3);
            RichTextBox groupByBox = new RichTextBox();
            groupByBox.ReadOnly = true;
            groupByBox.Name = "groupByBox" + colCounter.ToString();
            groupByBox.Dock = DockStyle.Fill;
            groupByBox.Height = 78;
            tableLayoutPanel1.Controls.Add(groupByBox, colCounter + 1, 3);
            tableLayoutPanel1.SetColumnSpan(groupByBox, 4);

            addDataSourceRow(colCounter, 4);

            addUnionButton(colCounter + 6);


            foreach (RowStyle style in tableLayoutPanel1.RowStyles)
                style.SizeType = SizeType.AutoSize;
            foreach (ColumnStyle style in tableLayoutPanel1.ColumnStyles)
                style.SizeType = SizeType.AutoSize;
        }

        /*      void cb1_SelectedIndexChanged(object sender, EventArgs e)
              {
                  MessageBox.Show("TEST STUFF");
                  tableLayoutPanel1.Controls.Add(new Label { Text = string.Format("Source {0}: ", rowCounter + 1) }, 0, rowCounter);
                  tableLayoutPanel1.Controls.Add(makeDatasetComboBox(AvaliableDatasets, "cb" + rowCounter), 1, rowCounter);
                  rowCounter++;
                  foreach (RowStyle style in tableLayoutPanel1.RowStyles)
                      style.SizeType = SizeType.AutoSize;
              }

              private ComboBox makeDatasetComboBox(List<string> avaliableDatasets, string comboDatasetBoxName, string rowCounter)
              {
                  ComboBox cb = new ComboBox();
                  cb.DropDownStyle = ComboBoxStyle.DropDownList;
                  cb.Items.AddRange(avaliableDatasets.ToArray());
                  cb.SelectedIndexChanged += cb1_SelectedIndexChanged;
                  cb.Name = comboDatasetBoxName;
                  return cb;
              }
        */

        private void addDataSourceRow(int columnct, int rowct)
        {
            string label = string.Format("Source {0}: ", (rowct - 3).ToString());
            tableLayoutPanel1.Controls.Add(new Label { Text = label }, columnct, rowct);
            tableLayoutPanel1.Controls.Add(makeLibnameComboBox((rowct - 3).ToString()), columnct+1, rowct);
            tableLayoutPanel1.Controls.Add(makeDatasetComboBox((rowct - 3).ToString()), columnct+2, rowct);
            Button editButton = new Button();
            editButton.Text = "Edit";
            editButton.Name = "EditRow" + (rowct - 3).ToString() + "Col" + columnct.ToString();
            tableLayoutPanel1.Controls.Add(editButton, colCounter + 3, rowct);
            Button removeButton = new Button();
            removeButton.Text = "Remove";
            removeButton.Name = "RemoveRow" + (rowct - 3).ToString() + "Col" + columnct.ToString();
            tableLayoutPanel1.Controls.Add(removeButton, colCounter + 4, rowct);
        }

        private void addUnionButton(int unionColumn)
        {
            Button UnionButton = new Button();
            UnionButton.Text = "Union";
            tableLayoutPanel1.Controls.Add(UnionButton, unionColumn, 0);
        }

        private ComboBox makeLibnameComboBox(string rowCounter)
        {
            ComboBox libnames = new ComboBox();
            libnames.SelectedIndexChanged += new System.EventHandler(cmb_Libname_SelectedIndexChanged);
            libnames.DropDownStyle = ComboBoxStyle.DropDownList;
            libnames.Name = "libname" + rowCounter;

            List<string> datasetLibnames = new List<string>();
            foreach (StudyDataSet dataset in AvaliableDatasets)
            {
                datasetLibnames.Add(dataset.Libname.SASLibname.ToUpper());
            }
            var unique_datasetLibnames = new HashSet<string>(datasetLibnames);
            foreach (string s in unique_datasetLibnames)
            {
                libnames.Items.Add(s);
            }

            return libnames;
        }

        private ComboBox makeDatasetComboBox(string rowCounter)
        {
            ComboBox datasets = new ComboBox();
            //libnames.SelectedIndexChanged += new System.EventHandler(cmb_Libname_SelectedIndexChanged);
            datasets.DropDownStyle = ComboBoxStyle.DropDownList;
            datasets.Items.AddRange(AvaliableDatasets.ToArray());
            //datasets.SelectedIndexChanged += cb1_SelectedIndexChanged;
            datasets.Name = "dataset" + rowCounter;

            return datasets;
        }

        private void CreateRadioButton(string rowCounter, string text, string name)
        {
            RadioButton rad = new RadioButton();
            rad.Text = text;
            rad.Name = name + rowCounter;
            rad.CheckedChanged += new EventHandler(rad_CheckedChanged);
        }

        private void setupLibnamesAndDatasetLists()
        {
            List<Libname> tempLibnames = new List<Libname>();
            foreach (StudyDataSet dataset in Instance.study.Datasets)
            {
                AvaliableDatasets.Add(dataset);
            }
            //Get custom/temp datasets
            foreach (StudyDataSet dataset in Domain.Custom_Datasets)
            {
                AvaliableDatasets.Add(dataset);
            }
        }

        private void cmb_Libname_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combobox = (ComboBox)sender;
            selectedLibname = combobox.SelectedItem.ToString();
            string cmb_dataset_name = combobox.Name.Replace("libname", "dataset");
            ComboBox datasets = (ComboBox)tableLayoutPanel1.Controls[cmb_dataset_name];
            datasets.Items.Clear();
            foreach (StudyDataSet dataset in AvaliableDatasets)
            {
                if (dataset.Libname.SASLibname.ToUpper() == combobox.SelectedItem.ToString())
                    datasets.Items.Add(dataset.DatasetName.ToUpper());
            }
        }

        void rad_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rad = (RadioButton)sender;
            if (rad.Checked)
            {
                if (rad.Text == "Inner Join")
                {
                    //handle inner join
                }
                else if (rad.Text == "Left Join")
                {
                    //handle left join
                }
                else if (rad.Text == "Right Join")
                {
                    //handle right join
                }
                else if (rad.Text == "Full Join")
                {
                    //handle Full Join
                }
                else if (rad.Text == "Union")
                {
                    //handle union
                }
                else if (rad.Text == "Except")
                {
                    //handle Except
                }
                else if (rad.Text == "Intersect")
                {
                    //handle Intersect
                }
                else if (rad.Text == "Outer Union")
                {
                    //handle Outer Union
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TEST STUFF");
            /*create join/union options*/
            string label = string.Format("Source {0}: ", (rowCounter + 1).ToString());
            tableLayoutPanel1.Controls.Add(new Label { Text = label }, 0, rowCounter);
            tableLayoutPanel1.Controls.Add(makeLibnameComboBox(rowCounter.ToString()), 1, rowCounter);
            tableLayoutPanel1.Controls.Add(makeDatasetComboBox(rowCounter.ToString()), 2, rowCounter);
            rowCounter++;

            foreach (RowStyle style in tableLayoutPanel1.RowStyles)
                style.SizeType = SizeType.AutoSize;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.ColumnCount += 1;
            tableLayoutPanel1.Controls.Add(new Label { Text = "test" + tableLayoutPanel1.ColumnCount }, tableLayoutPanel1.ColumnCount - 1, 0);
            foreach (ColumnStyle style in tableLayoutPanel1.ColumnStyles)
                style.SizeType = SizeType.AutoSize;
            foreach (RowStyle style in tableLayoutPanel1.RowStyles)
                style.SizeType = SizeType.AutoSize;
        }


        private void joinbutton_clk(object sender, EventArgs e)
        {
            Dataset_Source_Selection source = new Dataset_Source_Selection(1, AvaliableDatasets);
            source.Show();
        }
    }
}
