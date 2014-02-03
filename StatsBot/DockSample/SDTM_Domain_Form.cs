using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DockSample
{
    public partial class SDTM_Domain_Form : DockContent
    {
        private SDTM_Domain Domain;

        public SDTM_Domain_Form(SDTM_Domain domain)
        {
            InitializeComponent();
            this.Name = domain.Domain_Name;
            txt_DomainName.Text = domain.Domain_Name;
            txt_DomainDescription.Text = domain.Domain_Description;
            txt_DomainClass.Text = domain.Domain_Class;
            txt_DomainStructure.Text = domain.Domain_Structure;
            Domain = domain;
            PopulateVariableList();
        }

        public void PopulateVariableList()
        {
            listbx_Variables.Items.Clear();
            if (Domain.Domain_Variables.Count != 0)
            {
                foreach (SDTM_Variable var in Domain.Domain_Variables)
                {
                    listbx_Variables.Items.Add(var.Variable_Name);
                }
            }
        }

        private void btn_AddVar_Click(object sender, EventArgs e)
        {
            SDTM_New_Variable_Form form = new SDTM_New_Variable_Form(Domain, this);
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listbx_Variables.SelectedItem != null)
            {
                SDTM_Variable_Form form = new SDTM_Variable_Form(Domain, listbx_Variables.SelectedItem.ToString());
                form.Show();
            }
            else
            {
                MessageBox.Show("Must select variable before editing.");
            }
        }
    }
}
