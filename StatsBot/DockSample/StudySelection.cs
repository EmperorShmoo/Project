using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using WeifenLuo.WinFormsUI.Docking;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace DockSample
{
    public partial class StudySelection : Form
    {
        Study thisStudy = Instance.study;

        public StudySelection()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd1 = new OpenFileDialog();
            fd1.Title = "Select StatBot Mapping";
            fd1.Filter = "StatBot Mapping (*.statbot)|*.statbot";
            if (fd1.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader file = File.OpenText(fd1.FileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    Study thisStudy = (Study)serializer.Deserialize(file, typeof (Study));
                    Instance.study = thisStudy;
                }
            }
            listBox1.Items.Clear();
            textBox1.Text = Instance.study.StudyName;
            textBox1.ReadOnly = true;
            textBox2.Text = Instance.study.StudyRootDir;
            textBox2.ReadOnly = true;
            btnCreateNew.Visible = false;
            foreach (StudyDataSet dataset in Instance.study.Datasets)
            {
                listBox1.Items.Add(dataset.DatasetName);;
            }
            updateLibnamesViewer();
            updateAutoInclude();
        }

        private void btnCreateNew_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                thisStudy.StudyName = textBox1.Text;
                thisStudy.StudyRootDir = textBox2.Text;
                SaveFileDialog sfd1 = new SaveFileDialog();
                sfd1.Title = "Save Statbot Mapping";
                sfd1.Filter = "StatBot Mapping (*.statbot)|*.statbot";
                sfd1.DefaultExt = ".statbot";
                sfd1.InitialDirectory = textBox2.Text;
                if (sfd1.ShowDialog() == DialogResult.OK)
                {
                    thisStudy.StatBotFileLocation = sfd1.FileName;
                    using (StreamWriter file = File.CreateText(sfd1.FileName))
                    {
                        Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                        serializer.Serialize(file, thisStudy);
                    }
                    textBox1.ReadOnly = true;
                    textBox2.ReadOnly = true;
                }
            }
            else MessageBox.Show("Must enter study name and directory path!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd1 = new FolderBrowserDialog();
            if (fbd1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = fbd1.SelectedPath;
            }
        }

        private void btnAddLibref_Click(object sender, EventArgs e)
        {
            if (txtLibRef.Text != "")
            {
                FolderBrowserDialog fbd1 = new FolderBrowserDialog();
                Libname libname = new Libname();
                if (fbd1.ShowDialog() == DialogResult.OK)
                {
                    libname.Directory = fbd1.SelectedPath;
                    libname.SASLibname = txtLibRef.Text;
                    libname.UseRelativePath = chkRelativePath.Checked;
                    if (Instance.study.Libnames != null)
                        Instance.study.Libnames.Add(libname);
                    else
                    {
                        List<Libname> liblist = new List<Libname>();
                        liblist.Add(libname);
                        Instance.study.Libnames = liblist;
                    }
                    updateLibnamesViewer();
                    txtLibRef.Text = "";
                    chkRelativePath.Checked = false;
                }
            }
            else MessageBox.Show("SAS Libref name required.");
        }

        private void updateLibnamesViewer()
        {
            if (Instance.study.Libnames != null)
            {
                var test = new BindingList<Libname>();
                foreach (Libname libname in Instance.study.Libnames)
                {
                    test.Add(libname);
                }
                dataGridView1.DataSource = test;
                dataGridView1.AutoResizeColumns();
            }
        }

        private void btnRemoveLibref_Click(object sender, EventArgs e)
        {
            Instance.study.Libnames.RemoveAt(dataGridView1.CurrentCell.RowIndex);
            updateLibnamesViewer();
        }

        private void btnAddAutoInclude_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd1 = new OpenFileDialog();
            fd1.Title = "Select SAS File";
            fd1.Filter = "SAS file (*.sas)|*.sas";
            if (fd1.ShowDialog() == DialogResult.OK)
            {
                SASProgram sasprogram = new SASProgram();
                sasprogram.Directory = fd1.FileName;
                if (Instance.study.AutoInclude != null)
                    Instance.study.AutoInclude.Add(sasprogram);
                else
                {
                    List<SASProgram> saslist = new List<SASProgram>();
                    saslist.Add(sasprogram);
                    Instance.study.AutoInclude = saslist;
                }
                updateAutoInclude();
            }
        }

        private void updateAutoInclude()
        {
            if (Instance.study.AutoInclude != null)
            {
                var test = new BindingList<SASProgram>();
                foreach (SASProgram autoinclude in Instance.study.AutoInclude)
                {
                    test.Add(autoinclude);
                }
                dataGridView2.DataSource = test;
                dataGridView2.AutoResizeColumns();
            }
        }

        private void btnUseStudy_Click(object sender, EventArgs e)
        {
            if (Instance.study.StudyName != null)
                this.Close();
            else
                MessageBox.Show("A Study must be referenced!");
        }
    }
}
