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
    public partial class SDTM_New_Domain_Form : Form
    {
        DataTable dt = new DataTable();
        List<string> domainsInStudy = new List<string>();
        new SDTM_Overview Parent;

        public SDTM_New_Domain_Form(SDTM_Overview parent)
        {
            Parent = parent;
            InitializeComponent();
            //Populate listbox with unused domains
            using (StreamReader file = File.OpenText("SDTM_Datasets_Template.statds"))
            {
                JsonSerializer serializer = new JsonSerializer();
                dt = (DataTable)serializer.Deserialize(file, typeof(DataTable));
            }
            foreach (SDTM_Domain domain in Instance.study.workingSDTM.All_Domains)
            {
                domainsInStudy.Add(domain.Domain_Name);
            }
            List<string> allSDTMdomains = dt.AsEnumerable().Select(x => x["Dataset"].ToString()).ToList();

            IEnumerable<string> notAlreadyInStudy = allSDTMdomains.Except(domainsInStudy).Distinct();
            foreach (string s in notAlreadyInStudy)
            {
                listBox1.Items.Add(s);
            }
            listBox1.Items.Add("Custom Domain");
            //end populate listboxs
            listBox1.SetSelected(0, true);
        }

        private void Populate_based_on_query(string selection)
        {
            if (selection != "Custom Domain")
            {
                var QueryResults =
                       from DataRowView rowView in dt.AsDataView()
                       where rowView.Row.Field<string>("Dataset") == selection
                       select rowView;
                //Disable user input for pre-defined domains
                comboBox1.Enabled = false;
                textBox1.Enabled = false;
                richTextBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
                //Populate pre-defined domain info
                comboBox1.Text = QueryResults.First().Row["Dataset"].ToString(); //Domain Name
                textBox1.Text = QueryResults.First().Row["Description"].ToString(); //Description
                richTextBox1.Text = QueryResults.First().Row["Structure"].ToString(); //Structure
                comboBox2.Text = QueryResults.First().Row["Class"].ToString(); //Class
            }
            else //custom domain
            {
                //Enable user input for custom domains
                comboBox1.Enabled = true;
                textBox1.Enabled = true;
                richTextBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                //Clear any pre-exsisting info from fields
                richTextBox1.Text = "One record per ";
                textBox1.Text = "<Custom Domain>";
                comboBox1.ResetText();
                comboBox2.ResetText();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Populate_based_on_query(listBox1.SelectedItem.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool pass = true;
            //perform validation that all fields are populated before saving dataset
            if (listBox1.SelectedItem.ToString() == "Custom Domain" && (comboBox1.Text.Length != 2 ||
                !(comboBox1.Text.StartsWith("X") || comboBox1.Text.StartsWith("Y") || comboBox1.Text.StartsWith("Z"))))
            {
                pass = false;
                MessageBox.Show("Custom Domains must have 2 letter domain name starting with X Y or Z");
            }
            else if (comboBox2.Text == "")
            {
                pass = false;
                MessageBox.Show("Dataset Observation Class MUST be provided");
            }
            else if (textBox1.Text.Length > 40)
            {
                pass = false;
                MessageBox.Show("Dataset Description must be less than 40 characters");
            }
            else if (richTextBox1.Text == "")
            {
                pass = false;
                MessageBox.Show("Dataset Structure must be defined");
            }
            //Save dataset to study if it passed.
            if (pass == true)
            {
                SDTM_Domain thisDomain = new SDTM_Domain();
                thisDomain.Domain_Name = comboBox1.Text;
                thisDomain.Domain_Description = textBox1.Text;
                thisDomain.Domain_Class = comboBox2.Text;
                thisDomain.Domain_Structure = richTextBox1.Text;
                Instance.study.workingSDTM.All_Domains.Add(thisDomain);
                Parent.Reload_Domains();
                this.Close();
            }
            
                
        }

    }
}
