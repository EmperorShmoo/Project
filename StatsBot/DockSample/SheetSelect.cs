using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using Newtonsoft.Json;

namespace DockSample
{
    public partial class SheetSelect : DockContent
    {
        private System.Data.DataTable dt;
        private DataView creator;

        public SheetSelect()
        {
            InitializeComponent();
        }

        internal void WhoMadeMe(DataView Creator)
        {
            creator = Creator;
        }

        private String[] SheetNames(string fileName)
        {
            String[] Sheets = null;
            OleDbConnection objConn = null;
            OleDbConnectionStringBuilder sbConnection = new OleDbConnectionStringBuilder();
            sbConnection.DataSource = fileName;
            String strExtendedProperties = Path.GetExtension(fileName);
            try
            {
                if (Path.GetExtension(fileName).Equals(".xls") || Path.GetExtension(fileName).Equals(".xlsx"))//any excel sheet
                {
                    if (Path.GetExtension(fileName).Equals(".xls"))//for 97-03 Excel file
                    {
                        sbConnection.Provider = "Microsoft.Jet.OLEDB.4.0";
                        strExtendedProperties = "Excel 8.0;HDR=Yes;IMEX=1";//HDR=ColumnHeader,IMEX=InterMixed
                    }
                    else if (Path.GetExtension(fileName).Equals(".xlsx"))  //for 2007 Excel file
                    {
                        sbConnection.Provider = "Microsoft.ACE.OLEDB.12.0";
                        strExtendedProperties = "Excel 12.0;HDR=Yes;IMEX=1";
                    }
                    sbConnection.Add("Extended Properties", strExtendedProperties);
                    objConn = new OleDbConnection(sbConnection.ToString());
                    objConn.Open();
                    dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    if (dt == null)
                    {
                        return null;
                    }

                    Sheets = new String[dt.Rows.Count];
                    int i = 0;

                    // Add the sheet name to the string array.
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["TABLE_NAME"].ToString().Contains("$")
                            && !row["TABLE_NAME"].ToString().Contains("$Print")
                            && !row["TABLE_NAME"].ToString().Contains("$'Print")
                             && !row["TABLE_NAME"].ToString().Contains("$_"))//checks whether row contains '_xlnm#_FilterDatabase' or sheet name(i.e. sheet name always ends with $ sign)
                        {
                            Sheets[i] = row["TABLE_NAME"].ToString();
                            //Sheets[i] = Sheets[i].Replace("$", "");
                            //Sheets[i] = Sheets[i].Replace("'", "");
                            i++;
                        }
                    }
                }
                return Sheets;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                // Clean up.
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        private DataTable loadExcelFile(string fileName, string sheetName)
        {
            OleDbConnection objConn = null;
            OleDbConnectionStringBuilder sbConnection = new OleDbConnectionStringBuilder();
            sbConnection.DataSource = fileName;
            String strExtendedProperties = Path.GetExtension(fileName);
            try
            {
               DataTable table = new DataTable();
               if (Path.GetExtension(fileName).Equals(".xls"))//for 97-03 Excel file
                  {
                      sbConnection.Provider = "Microsoft.Jet.OLEDB.4.0";
                      strExtendedProperties = "Excel 8.0;HDR=Yes;IMEX=1";//HDR=ColumnHeader,IMEX=InterMixed
                  }
                  else if (Path.GetExtension(fileName).Equals(".xlsx"))  //for 2007 Excel file
                  {
                      sbConnection.Provider = "Microsoft.ACE.OLEDB.12.0";
                      strExtendedProperties = "Excel 12.0;HDR=Yes;IMEX=1";
                  }
                  sbConnection.Add("Extended Properties", strExtendedProperties);
                  objConn = new OleDbConnection(sbConnection.ToString());
                  objConn.Open();
                  using (OleDbDataAdapter dbAdapter = new OleDbDataAdapter("SELECT * FROM [" + sheetName + "]", objConn)) //rename sheet if required!
                      dbAdapter.Fill(table);

                if (table == null)
                {
                    return null;
                }
                creator.loadData(table);
                return table;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                // Clean up.
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }



        private DataTable loadSASFile(string fileName)
        {
            OleDbConnection objConn = null;
            try
            {
                string directoryName = Path.GetDirectoryName(fileName); // fileName.Substring(0, fileName.LastIndexOf("\\"));
                string datasetName = Path.GetFileNameWithoutExtension(fileName);
                comboBox1.Items.Add(directoryName);
                DataTable table = new DataTable();
                DataSet sasDS = new DataSet();
                objConn = new OleDbConnection(@"Provider=sas.LocalProvider; Data Source=" + directoryName);

                objConn.Open();
                OleDbCommand sasCommand = objConn.CreateCommand();
                sasCommand.CommandType = CommandType.TableDirect;
                sasCommand.CommandText = datasetName;
                OleDbDataAdapter da = new OleDbDataAdapter(sasCommand);
                da.Fill(sasDS, "SasData");

                objConn.Close();
                table = sasDS.Tables["SasData"];

                if (table == null)
                {
                    return null;
                }
                creator.loadData(table);
                return table;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                // Clean up.
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        private DataTable loadSTATDS(string fileName)
        {
            DataTable dt = new DataTable();
            using (StreamReader file = File.OpenText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                dt = (DataTable)serializer.Deserialize(file, typeof(DataTable));
            }
            creator.loadData(dt);
            return dt;
        }
        private void btnDirectory_Click(object sender, EventArgs e)
        {

            OpenFileDialog fd1 = new OpenFileDialog();
            fd1.Title = "Select file";
            fd1.Filter = "xls files (*.xls, *.xlsx)|*.xls;*.xlsx|sas files (*.sas7bdat)|*.sas7bdat;|StatBot DataSource (*.statds)|*.statds|All files (*.*)|*.*";
            if (fd1.ShowDialog() == DialogResult.OK)
            {
                comboBox1.Items.Clear();
                textBox1.Text = fd1.FileName;
                if (Path.GetExtension(fd1.FileName).Equals(".xls") || Path.GetExtension(fd1.FileName).Equals(".xlsx"))//any excel sheet
                {
                    string[] xlssheetnames = SheetNames(fd1.FileName);
                    foreach (string s in xlssheetnames)
                    {
                        if (s != null)
                            comboBox1.Items.Add(s);
                    }
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && Path.GetExtension(textBox1.Text).Contains(".xls") && comboBox1.SelectedItem != null)
            {
                loadExcelFile(textBox1.Text, comboBox1.SelectedItem.ToString());
            }
            else if (textBox1.Text != null && Path.GetExtension(textBox1.Text).Equals(".sas7bdat"))
            {
                loadSASFile(textBox1.Text);
            }
            else if (textBox1.Text != null && Path.GetExtension(textBox1.Text).Equals(".statds"))
            {
                loadSTATDS(textBox1.Text);
            }


            this.Close();
        }
    }
}
