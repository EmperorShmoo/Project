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
    public partial class SDTM_Variable_Form : Form
    {
        SDTM_Domain Domain;
        SDTM_Variable Variable;

        public SDTM_Variable_Form(SDTM_Domain domain, string variable_name)
        {
            InitializeComponent();
            Domain = domain;
            Variable = domain.Domain_Variables.Find(x => x.Variable_Name == variable_name);
            PopulateVarInfo();
            HideAll();
            PopulateStudyDatasets();
            PopualteStudyLibnames();
            PopulateFormatList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                tlp_Codelist.Controls.Add(new Label { Text = "label " + i });
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Custom_Dataset_Creator form = new Custom_Dataset_Creator(Domain);
            form.Show();
        }

        private List<string> avaliableDatasets()
        {
            List<string> avaliableDatasets = new List<string>();
            avaliableDatasets.Add("test1");
            avaliableDatasets.Add("test2");
            avaliableDatasets.Add("test3");
            return avaliableDatasets;
        }

        private void PopulateVarInfo()
        {
            txt_domain.Text = Domain.Domain_Name;
            Txt_VarName.Text = Variable.Variable_Name;
            txt_VarOrd.Text = Variable.Variable_Ord.ToString();
            txt_Label.Text = Variable.Variable_Label;
            txt_Length.Text = Variable.Variable_Length.ToString();
            cmb_Role.SelectedItem = Variable.Variable_Role;
            cmb_Core.SelectedItem = Variable.Variable_Core;
            cmb_Type.SelectedItem = Variable.Variable_DataType;
        }

        private void cmb_Use_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cmb_Use.SelectedItem.ToString() == "Assigned Constant Value")
            {
                txt_Constant.Visible = true;
                txt_Constant.Enabled = true;
                lbl_Constant.Visible = true;

                lbl_SourceLibname.Visible = false;
                cmb_SourceLibname.Visible = false;
                lbl_StudyDatasets.Visible = false;
                cmb_StudyDatasets.Visible = false;
                lbl_StudyDatasetVar.Visible = false;
                cmb_StudyDatasetVar.Visible = false;
                lbl_CodeListFormat.Visible = false;
                cmb_Format.Visible = false;
                lbl_CodelistInput.Visible = false;
                btn_Codelist.Visible = false;
                lbl_CodelistOutput.Visible = false;
                tlp_Codelist.Visible = false;

                lbl_SourceData.Visible = false;
                cmb_SourceData.Visible = false;
                btn_NewSourceData.Visible = false;
                btn_ModSourceData.Visible = false;
                lbl_Derived.Visible = false;
                btn_When.Visible = false;
                btn_Else.Visible = false;
                rtb_CustomDerivation.Visible = false;
                btn_TestDerivation.Visible = false;
            }
            else if (cmb_Use.SelectedItem.ToString() == "Assigned CodeList Value (1 input variable)")
            {
                txt_Constant.Visible = false;
                txt_Constant.Enabled = false;
                lbl_Constant.Visible = false;

                lbl_SourceLibname.Visible = true;
                cmb_SourceLibname.Visible = true;
                lbl_StudyDatasets.Visible = true;
                cmb_StudyDatasets.Visible = true;
                lbl_StudyDatasetVar.Visible = true;
                cmb_StudyDatasetVar.Visible = true;
                lbl_CodeListFormat.Visible = true;
                cmb_Format.Visible = true;
                lbl_CodelistInput.Visible = true;
                btn_Codelist.Visible = true;
                lbl_CodelistOutput.Visible = true;
                tlp_Codelist.Visible = true;

                lbl_SourceData.Visible = false;
                cmb_SourceData.Visible = false;
                btn_NewSourceData.Visible = false;
                btn_ModSourceData.Visible = false;
                lbl_Derived.Visible = false;
                btn_When.Visible = false;
                btn_Else.Visible = false;
                rtb_CustomDerivation.Visible = false;
                btn_TestDerivation.Visible = false;
            }
            else if (cmb_Use.SelectedItem.ToString() == "Derived")
            {
                txt_Constant.Visible = false;
                txt_Constant.Enabled = false;
                lbl_Constant.Visible = false;

                lbl_SourceLibname.Visible = false;
                cmb_SourceLibname.Visible = false;
                lbl_StudyDatasets.Visible = false;
                cmb_StudyDatasets.Visible = false;
                lbl_StudyDatasetVar.Visible = false;
                cmb_StudyDatasetVar.Visible = false;
                lbl_CodeListFormat.Visible = false;
                cmb_Format.Visible = false;
                lbl_CodelistInput.Visible = false;
                btn_Codelist.Visible = false;
                lbl_CodelistOutput.Visible = false;
                tlp_Codelist.Visible = false;

                lbl_SourceData.Visible = true;
                cmb_SourceData.Visible = true;
                btn_NewSourceData.Visible = true;
                btn_ModSourceData.Visible = true;
                lbl_Derived.Visible = true;
                btn_When.Visible = true;
                btn_Else.Visible = true;
                rtb_CustomDerivation.Visible = true;
                btn_TestDerivation.Visible = true;
            }
        }

        private void HideAll()
        {
            txt_Constant.Visible = false;
            txt_Constant.Enabled = false;
            lbl_Constant.Visible = false;

            lbl_SourceLibname.Visible = false;
            cmb_SourceLibname.Visible = false;
            lbl_StudyDatasets.Visible = false;
            cmb_StudyDatasets.Visible = false;
            lbl_StudyDatasetVar.Visible = false;
            cmb_StudyDatasetVar.Visible = false;
            lbl_CodeListFormat.Visible = false;
            cmb_Format.Visible = false;
            lbl_CodelistInput.Visible = false;
            btn_Codelist.Visible = false;
            lbl_CodelistOutput.Visible = false;
            tlp_Codelist.Visible = false;

            lbl_SourceData.Visible = false;
            cmb_SourceData.Visible = false;
            btn_NewSourceData.Visible = false;
            btn_ModSourceData.Visible = false;
            lbl_Derived.Visible = false;
            btn_When.Visible = false;
            btn_Else.Visible = false;
            rtb_CustomDerivation.Visible = false;
            btn_TestDerivation.Visible = false;
        }

        private void PopulateStudyDatasets()
        {
            foreach (StudyDataSet dataset in Instance.study.Datasets)
            {
                cmb_StudyDatasets.Items.Add(dataset.DatasetName.ToUpper());
            }
        }

        private void PopualteStudyLibnames()
        {
            List<string> datasetLibnames = new List<string>();
            //Get study datasets
            foreach (StudyDataSet dataset in Instance.study.Datasets)
            {
                datasetLibnames.Add(dataset.Libname.SASLibname.ToUpper());
            }
            //Get custom/temp datasets
            foreach (StudyDataSet dataset in Domain.Custom_Datasets)   
            {
                datasetLibnames.Add(dataset.Libname.SASLibname.ToUpper());
            }
            var unique_datasetLibnames = new HashSet<string>(datasetLibnames);
            foreach (string s in unique_datasetLibnames)
            {
                cmb_SourceLibname.Items.Add(s);
            }
        }

        private void cmb_StudyDatasets_SelectedValueChanged(object sender, EventArgs e)
        {
            /*StudyDataSet dataset = Instance.study.Datasets.Find(x => x.DatasetName == cmb_StudyDatasets.SelectedItem.ToString());
            foreach (StudyDataSetVariable var in dataset.Variables)
            {
                cmb_StudyDatasetVar.Items.Add(var.VarName.ToUpper());
            }*/
        }

        private void PopulateFormatList()
        {
            //TODO
        }

        private void btn_Codelist_Click(object sender, EventArgs e)
        {
            //Proc Freq variable.
            List<string> sasProgram = new List<string>();
            StringBuilder sasCode = new StringBuilder();
            sasCode.Append("proc freq data=" + cmb_SourceLibname.SelectedItem.ToString() + "." + cmb_StudyDatasets.SelectedItem.ToString() + " noprint;");
            sasProgram.Add(sasCode.ToString());
            sasCode.Clear();
            sasCode.Append("   tables " + cmb_StudyDatasetVar.SelectedItem.ToString() + "/out=sbfreq;");
            sasProgram.Add(sasCode.ToString());
            sasCode.Clear();
            sasCode.Append("run;");
            sasProgram.Add(sasCode.ToString());
            sasCode.Clear();
            //Delegate references to callback log and dataTable of non-uniques.
                //Instance.customLogNotificationReturn(HandleLogNotificationFeedback);
            Instance.dataTableReturn(PopulateCodeList);

            string command = string.Format("select " + cmb_StudyDatasetVar.SelectedItem.ToString() + " from sbfreq");
            Instance.CustomSASProgramToRun(sasProgram, command);
        }

        private void PopulateCodeList(DataTable dt)
        {
            tlp_Codelist.Controls.Clear();
            tlp_Codelist.AutoScroll = false;//reset scrollbar
            tlp_Codelist.AutoScroll = true; 
            int rowCounter = 0;
            foreach (DataRow row in dt.Rows)
            {
                tlp_Codelist.Controls.Add(makeTextBox(row[cmb_StudyDatasetVar.SelectedItem.ToString()].ToString(), rowCounter + "start", true), 0, rowCounter);
                tlp_Codelist.Controls.Add(makeTextBox("", rowCounter + "end", false), 1, rowCounter);
                rowCounter++;
            }
            foreach (RowStyle style in tlp_Codelist.RowStyles)
                style.SizeType = SizeType.AutoSize;
        }

        private TextBox makeTextBox(string Text, string TextBoxName, bool locked)
        {
            TextBox tb = new TextBox();
            if (locked)
                tb.ReadOnly = true;
            tb.Text = Text;
            tb.Name = TextBoxName;
            
            tb.Width = (int)(tlp_Codelist.Width/2.4);
            return tb;
        }

        private void cmb_SourceLibname_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Libname changed, re-populate datasets and blank var list.
            cmb_StudyDatasetVar.Items.Clear();
            cmb_StudyDatasets.Items.Clear();
            //Get study datasets with matching libname
            foreach (StudyDataSet dataset in Instance.study.Datasets)
            {
                if (dataset.Libname.SASLibname.ToUpper() == cmb_SourceLibname.SelectedItem.ToString())
                    cmb_StudyDatasets.Items.Add(dataset.DatasetName.ToUpper());
            }
            //Get custom/temp datasets with matching libname
            foreach (StudyDataSet dataset in Domain.Custom_Datasets)
            {
                if (dataset.Libname.SASLibname.ToUpper() == cmb_SourceLibname.SelectedItem.ToString())
                    cmb_StudyDatasets.Items.Add(dataset.DatasetName.ToUpper());
            }
        }

        private void cmb_StudyDatasets_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Dataset changed - blank var list
            cmb_StudyDatasetVar.Items.Clear();
            var dataset = (from x in Instance.study.Datasets
                                                    where x.Libname.SASLibname.ToUpper() == cmb_SourceLibname.SelectedItem.ToString()
                                                    && x.DatasetName.ToUpper() == cmb_StudyDatasets.SelectedItem.ToString()
                                                    select x).Union(from x in Domain.Custom_Datasets
                                                                              where x.Libname.SASLibname.ToUpper() == cmb_SourceLibname.SelectedItem.ToString()
                                                                               && x.DatasetName.ToUpper() == cmb_StudyDatasets.SelectedItem.ToString()
                                                                              select x).FirstOrDefault();
            foreach (StudyDataSetVariable variable in dataset.Variables)
            {
                cmb_StudyDatasetVar.Items.Add(variable.VarName.ToUpper());
            }

        }



    }
}
