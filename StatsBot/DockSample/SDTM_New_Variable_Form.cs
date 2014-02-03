using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;


namespace DockSample
{
    public partial class SDTM_New_Variable_Form : Form
    {
        DataTable dt = new DataTable();
        List<string> varsInStudy = new List<string>();
        SDTM_Domain Domain;
        new SDTM_Domain_Form Parent;

        public SDTM_New_Variable_Form(SDTM_Domain domain, SDTM_Domain_Form parent)
        {
            InitializeComponent();
            Domain = domain;
            Parent = parent;
            //Populate listbox with unused domains
            using (StreamReader file = File.OpenText("SDTM_Variable_Template.statds"))
            {
                JsonSerializer serializer = new JsonSerializer();
                dt = (DataTable)serializer.Deserialize(file, typeof(DataTable));
                var rows = from row in dt.AsEnumerable()
                           where row.Field<string>("Domain_Prefix") == domain.Domain_Name
                           select row;
                dt = rows.CopyToDataTable();
            }
            foreach (SDTM_Variable variable in domain.Domain_Variables)
            {
                varsInStudy.Add(variable.Variable_Name);
            }
            List<string> allSDTMvariables = dt.AsEnumerable().Select(x => x["Variable_Name"].ToString()).ToList();

            IEnumerable<string> notAlreadyInDomain = allSDTMvariables.Except(varsInStudy).Distinct();
            foreach (string s in notAlreadyInDomain)
            {
                lst_Variables.Items.Add(s);
            }
            //end populate listboxs
            lst_Variables.SetSelected(0, true);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set up all fields based on selection
            SetFields();
        }

        private void SetFields()
        {
            var QueryResults =
                   from DataRowView rowView in dt.AsDataView()
                   where rowView.Row.Field<string>("Variable_Name") == lst_Variables.SelectedItem.ToString()
                   select rowView;
            //Populate pre-defined variable info
            txt_VarName.Text = QueryResults.First().Row["Variable Label"].ToString(); //Var Description
            txt_VarOrdNum.Text = QueryResults.First().Row["Seq#_For_Order"].ToString(); //Order
            cmb_VarCore.Text = QueryResults.First().Row["Core"].ToString(); //Core
            cmb_VarRole.Text = QueryResults.First().Row["Role"].ToString(); //Role
            cmb_VarType.Text = QueryResults.First().Row["Type"].ToString(); //Type
            txt_Var_Length.Text = QueryResults.First().Row["Length"].ToString(); //Length
        }

        private void ClearFields()
        {
            txt_VarName.Text = "";
            txt_VarOrdNum.Text = "";
            cmb_VarCore.Text = "";
            cmb_VarRole.Text = "";
            cmb_VarType.Text = "";
            txt_Var_Length.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lst_Variables.SelectedItem != null)
            {
            var QueryResults =
                   from DataRowView rowView in dt.AsDataView()
                   where rowView.Row.Field<string>("Variable_Name") == lst_Variables.SelectedItem.ToString()
                   select rowView;

                SDTM_Variable var = new SDTM_Variable
                {
                    Variable_Name = QueryResults.First().Row["Variable_Name"].ToString(),
                    Variable_Label = QueryResults.First().Row["Variable Label"].ToString(),
                    Variable_Ord = int.Parse(QueryResults.First().Row["Seq#_For_Order"].ToString()),
                    Variable_Role = QueryResults.First().Row["Role"].ToString(),
                    Variable_Length = int.Parse(QueryResults.First().Row["Length"].ToString()),
                    Variable_DataType = QueryResults.First().Row["Type"].ToString(),
                    Variable_Core = QueryResults.First().Row["Core"].ToString(),
                    //Keyseq and comment have been intentionally left off as they do not come from the pre-defined list.
                    //Decimals and Datatype will be populated now, but may be reset later on based on SAS results.
                };
                Domain.Domain_Variables.Add(var);
                Parent.PopulateVariableList();
                this.Close();
            }
        }

    }
}
