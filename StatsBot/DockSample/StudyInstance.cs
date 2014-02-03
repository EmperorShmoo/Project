using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using SASWorkspaceManager;

namespace DockSample
{
    public static class Instance
    {
        private static SasServer ActiveSession = null;
        private static BackgroundWorker _bgSASWorker;
        private static LogWindow _SASLogWindow;
        static Study _instance = new Study();
        public static System.Action<DataTable> DataTableBack;
        public static System.Action<List<string>> LogNotificationFeedback;
        private static OleDbConnection connection = null;
        private static OleDbCommand oleCommand = null;
        private static List<string> logReturn;

        /*arrays for format list*/
        private static Array ImplTypesFilter { get; set; }
        private static Array FormattingMethodsFilter { get; set; }
        private static Array IFTypesFilter { get; set; }
        private static Array BaseTypesFilter { get; set; }

        public static Study study
        {
            get { return _instance; }
            set { _instance = value; }
        }

        public static SasServer ActiveSASSession
        {
            get { return ActiveSession; }
            set { ActiveSession = value; }
        }

        public static LogWindow SASLogWindow 
        {
            get {return _SASLogWindow;}
            set { _SASLogWindow = value; }
        }

        public static MainForm mainForm { get; set; }
   
        public static void ConnectBGWorkerToSAS(SasServer activeSession)
        {
            if (activeSession != null && activeSession.Workspace != null)
            {
                ActiveSession = activeSession;
                connection = new System.Data.OleDb.OleDbConnection("Provider=sas.iomprovider.1; SAS Workspace ID=" + ActiveSession.Workspace.UniqueIdentifier);
                // if we don't use a background thread when running this program
                // we'll BLOCK the UI of the app while a long-running
                // SAS job completes.
                // This allows us to keep the UI responsive.
                _bgSASWorker = new BackgroundWorker();
                _bgSASWorker.DoWork += bg_DoWork;
                _bgSASWorker.RunWorkerCompleted += bg_RunWorkerCompleted;
                //ActiveSession.Workspace.LanguageService.DatastepComplete += new SAS.CILanguageEvents_DatastepCompleteEventHandler(LanguageService_DatastepComplete);
            }

        }

        //set up libnames
        public static void SetUpLIBNAMESinSAS()
        {
            StringBuilder sasCode = new StringBuilder();
            foreach (Libname libname in Instance.study.Libnames)
            {
                sasCode.Append("libname " + libname.SASLibname + "\"" + libname.Directory + "\";");
            }
                //HARDCODE ***NEED TO REMOVE LATER ON WHEN I ADD SYSTEM OPTIONS
            sasCode.Append("  options fmtsearch=(rawdata library work format);");
            SAScommand command = new SAScommand
            {
                customSASCode = sasCode.ToString()
            };
            oleCommand = null;
            _bgSASWorker.RunWorkerAsync(command);
        }

        //Return DataTable
        public static void dataTableReturn(System.Action<DataTable> databack)
        {
            DataTableBack = databack;
        }
        //Return Log Notification Feedback Requests
        public static void customLogNotificationReturn(System.Action<List<string>> logNotifications)
        {
            LogNotificationFeedback = logNotifications;
        }
        //Accept custom code to run
        public static void CustomSASCodeToRun(string s, string OLECommand)
        {
            SAScommand command = new SAScommand
            {
                customSASCode = s
            };
            if(_bgSASWorker.IsBusy == false)
                _bgSASWorker.RunWorkerAsync(command);
            oleCommand = null;
            if (OLECommand != null)
                oleCommand = new OleDbCommand(OLECommand, connection);
        }

        //Accept custom programs to run
        public static void CustomSASProgramToRun(List<string> s, string OLECommand)
        {
            SAScommand command = new SAScommand
            {
                customSASCode = null,
                SasProgram = s
            };
            if (_bgSASWorker.IsBusy == false)
                _bgSASWorker.RunWorkerAsync(command);
            oleCommand = null;
            if (OLECommand != null)
                oleCommand = new OleDbCommand(OLECommand, connection);
        }

        static void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            SAScommand currentCommand = e.Argument as SAScommand;
            if (currentCommand.customSASCode != null) //if custom code was sent, run it.
                ActiveSession.Workspace.LanguageService.Submit(currentCommand.customSASCode);
            if (currentCommand.SasProgram != null && currentCommand.SasProgram.Count > 0)
            {
                ActiveSASSession.Workspace.LanguageService.SubmitLines(currentCommand.SasProgram.ToArray());
            }
        }

        static void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FetchResults();
            if (oleCommand != null)
            {
                DataTable ds = new DataTable();
                OleDbDataAdapter adapter = new OleDbDataAdapter(oleCommand);
                adapter.Fill(ds);
                DataTableBack(ds);
            }
            if (LogNotificationFeedback != null)
                LogNotificationFeedback(logReturn);

            DataTableBack = null;
            LogNotificationFeedback = null;
        }

        static void FetchResults()
        {
            bool hasErrors = false, hasWarnings = false; Color textColor;
            logReturn = new List<string>();

            // when code is complete, update the log viewer
            Array carriage, lineTypes, lines;
            do
            {
                ActiveSession.Workspace.LanguageService.FlushLogLines(int.MaxValue,
                    out carriage,
                    out lineTypes,
                    out lines);
                for (int i = 0; i < lines.GetLength(0); i++)
                {
                    SAS.LanguageServiceLineType pre =
                        (SAS.LanguageServiceLineType)lineTypes.GetValue(i);
                    switch (pre)
                    {
                        case SAS.LanguageServiceLineType.LanguageServiceLineTypeError:   //Errors
                            hasErrors = true;
                            textColor = Color.Red;
                            break;
                        case SAS.LanguageServiceLineType.LanguageServiceLineTypeNote:   //SAS notifications
                            textColor = Color.DarkGreen;
                            break;
                        case SAS.LanguageServiceLineType.LanguageServiceLineTypeWarning: //Warnings
                            hasWarnings = true;
                            textColor = Color.DarkCyan;
                            break;
                        case SAS.LanguageServiceLineType.LanguageServiceLineTypeTitle:
                        case SAS.LanguageServiceLineType.LanguageServiceLineTypeFootnote:
                            textColor = Color.Blue;
                            break;
                        default:
                            textColor = MainForm.DefaultForeColor;
                            break;
                    }
                    _SASLogWindow.AddToLog(lines, i, textColor);
                    logReturn.Add(lines.GetValue(i) as string);
                }
            }

            while (lines != null && lines.Length > 0);

            if (hasWarnings && hasErrors)
                mainForm.UpdateStatusMsg("Program complete - has ERRORS and WARNINGS");
            else if (hasErrors)
                mainForm.UpdateStatusMsg("Program complete - has ERRORS");
            else if (hasWarnings)
                mainForm.UpdateStatusMsg("Program complete - has WARNINGS");
            else
                mainForm.UpdateStatusMsg("Program complete - no warnings or errors!");
        }

    }
}
