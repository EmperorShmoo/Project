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
    public partial class SDTM_Overview : DockContent
    {
        DockPanel theDockpanel;
        public SDTM_Overview()
        {
            InitializeComponent();
            //Check if any SDTM is already associated with this study
            if (Instance.study.StudySDTM.Count > 0)
            {
                //populate version #'s
                //versionNumber = Instance.study.StudySDTM.Max(t => t.VersionNumber);
                foreach (StudySDTM sdtmVersion in Instance.study.StudySDTM)
                {
                    cmbVersionNumber.Items.Add(string.Format("{0,5:#,###.0}", sdtmVersion.VersionNumber));
                }
            }
            else
            {
                Instance.study.workingSDTM.VersionNumber = 1.0f;
            }
            cmbVersionNumber.Text = string.Format("{0,5:#,###.0}", Instance.study.workingSDTM.VersionNumber);
            Reload_Domains();
        }

        public void Reload_Domains()
        {
            checkedListBox1.Items.Clear();
            foreach (SDTM_Domain domain in Instance.study.workingSDTM.All_Domains)
            {
                checkedListBox1.Items.Add(domain.Domain_Name);
            }
        }
        public void setDockPanel(DockPanel dockPanel)
        {
            theDockpanel = dockPanel;
        }
 
        private void button2_Click(object sender, EventArgs e)
        {
            SDTM_New_Domain_Form new_SDTM_Domain_Form = new SDTM_New_Domain_Form(this);
            new_SDTM_Domain_Form.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Save SDTM version to study
            var obj = Instance.study.StudySDTM.FirstOrDefault(x => x.VersionNumber == Instance.study.workingSDTM.VersionNumber);
            if(obj != null) //Update current record in list 
            {
                obj = Instance.study.workingSDTM;
            }
            else //Create new record in list
            {
                Instance.study.StudySDTM.Add(Instance.study.workingSDTM);
            }
            Instance.study.SaveUpdatedStudy();
        }

        private void cmbVersionNumber_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Load previous version
            var obj = Instance.study.StudySDTM.FirstOrDefault(x => x.VersionNumber == float.Parse(cmbVersionNumber.SelectedItem.ToString()));
            if (obj != null) //Update current record in list 
            {
                Instance.study.workingSDTM.All_Domains = obj.All_Domains;
                Instance.study.workingSDTM.VersionComments = obj.VersionComments;
                Instance.study.workingSDTM.VersionNumber = obj.VersionNumber;
            }
            Reload_Domains();
        }

        private void cmbVersionNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Validate user input in the version combobox
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            //only allow 1 decimal point
            if (e.KeyChar == '.'
                && (sender as ComboBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
            string.Format("{0,5:#,###.0}", cmbVersionNumber.Text);
        }

        private void cmbVersionNumber_TextChanged(object sender, EventArgs e)
        {
            //updates working version # whenever user inputs values into SDTM version #
            Instance.study.workingSDTM.VersionNumber = float.Parse(cmbVersionNumber.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var obj = Instance.study.workingSDTM.All_Domains.FirstOrDefault(x => x.Domain_Name == checkedListBox1.SelectedItem.ToString());
            if (obj != null) //Update current record in list 
            {
                SDTM_Domain_Form form = new SDTM_Domain_Form(obj);
                form.Show(theDockpanel);
            }
            else
            {
                MessageBox.Show("Must select 1 domain to edit from list");
            }
        }
    }
}
