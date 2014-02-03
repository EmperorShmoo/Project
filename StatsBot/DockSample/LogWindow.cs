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
    public partial class LogWindow : ToolWindow
    {
        string fullRTFcode;
        List<string> subsetErrorRTFList;
        List<string> subsetWarningRTFList;
        List<string> subsetSubmittedRTFList;

        public LogWindow()
        {
            InitializeComponent();
            comboSubsetLog.SelectedItem = comboSubsetLog.Items[0];
            subsetErrorRTFList = new List<string>();
            subsetWarningRTFList = new List<string>();
            subsetSubmittedRTFList = new List<string>();
        }

        public void AddToLog(Array lines, int i, Color textColor)
        {         
            rtbLogText.SelectionColor = textColor;
            rtbLogText.AppendText(string.Format("{0}{1}", lines.GetValue(i) as string, Environment.NewLine));
            fullRTFcode = rtbLogText.Rtf;
            if (textColor == Color.Red)
                subsetErrorRTFList.Add((string.Format("{0}{1}", lines.GetValue(i) as string, Environment.NewLine)));
            else if (textColor == Color.DarkCyan)
                subsetWarningRTFList.Add((string.Format("{0}{1}", lines.GetValue(i) as string, Environment.NewLine)));
            else if (textColor == ForeColor)
                subsetSubmittedRTFList.Add((string.Format("{0}{1}", lines.GetValue(i) as string, Environment.NewLine)));
        }

        public void AddToLog(string RTFstring)
        {
            rtbLogText.Rtf = RTFstring;
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            rtbLogText.Clear();
            fullRTFcode = "";
            subsetSubmittedRTFList.Clear();
            subsetWarningRTFList.Clear();
            subsetErrorRTFList.Clear();
        }

        private void comboSubsetLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (comboSubsetLog.SelectedItem.ToString() == "All")
            {
                rtbLogText.Clear();
                rtbLogText.Rtf = fullRTFcode;
            }
            else if (comboSubsetLog.SelectedItem.ToString() == "Submitted Statements")
            {
                rtbLogText.Clear();
                foreach (string line in subsetSubmittedRTFList)
                {
                    rtbLogText.AppendText(line);
                }
            }
            else if (comboSubsetLog.SelectedItem.ToString() == "Warnings")
            {
                rtbLogText.Clear();
                foreach (string line in subsetWarningRTFList)
                {
                    rtbLogText.AppendText(line);
                }
            }
            else if (comboSubsetLog.SelectedItem.ToString() == "Errors")
            {
                rtbLogText.Clear();
                foreach (string line in subsetErrorRTFList)
                {
                    rtbLogText.AppendText(line);
                }
            }
        }

    }
}
