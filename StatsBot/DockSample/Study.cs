using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace DockSample
{
    //Information about the whole study
    public class Study 
    {
        public string StudyName;
        public string StudyRootDir;
        public string StatBotFileLocation;
        public List<StudyDataSet> Datasets = new List<StudyDataSet>();
        public List<Libname> Libnames = new List<Libname>();
        public List<SASProgram> AutoInclude = new List<SASProgram>();
        public List<StudySDTM> StudySDTM = new List<StudySDTM>();
        public StudySDTM workingSDTM = new StudySDTM();

        public Study()
        {
        }

        public void SaveUpdatedStudy()
        {
            using (StreamWriter file = File.CreateText(StatBotFileLocation))
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.Serialize(file, this);
            }            
        }
    }

    //SAS library name
    public class Libname
    {
        public string Directory { get; set; }
        public string SASLibname { get; set; }
        public bool UseRelativePath { get; set; }

        public Libname()
        {
        }
    }

    //Information about SAS Programs
    public class SASProgram
    {
        public string Directory { get; set; }

        public SASProgram()
        {
        }
    }

    //Information about a specific dataset
    public class StudyDataSet
    {
        public Libname Libname;
        public string DatasetName;
        public List<StudyDataSetVariable> Variables;

        public StudyDataSet()
        {
        }
    }

    //Information about a specific variable
    public class StudyDataSetVariable
    {
        public string VarName;
        public string VarType;
        public string VarLength;
        public string VarNum;
        public string VarLabel;
        public string VarForamt;
        public string VarFormatLength;
        public string VarFormatDecimal;
        public string VarInformat;
        public string VarInformatLength;
        public string VarInformatDecimal;

        public bool IsKeyVar;
        public int KeyNum;

        public StudyDataSetVariable()
        {
        }

    }

    //commands to pass into/out of the background SAS worker
    public class SAScommand
    {
        public string customSASCode;
        public List<string> SasProgram;
    }

#region SDTM
    // overall SDTM information
    public class StudySDTM
    {
        public float VersionNumber;
        public string VersionComments;
        public List<SDTM_Domain> All_Domains = new List<SDTM_Domain>();

        public StudySDTM()
        {
        }
    }

    // individual SDTM Domain
    public class SDTM_Domain
    {
        public string Domain_Name;
        public string Domain_Class;
        public string Domain_Structure;
        public string Domain_Description;
        public List<SDTM_Variable> Domain_Variables = new List<SDTM_Variable>();
        public List<StudyDataSet> Custom_Datasets = new List<StudyDataSet>();
        public List<StudyDataSet> Raw_Datasets = new List<StudyDataSet>();

        public SDTM_Domain()
        {
        }
    }

    // individual SDTM Variable
    public class SDTM_Variable
    {
        public string Variable_Name;
        public string Variable_Label;
        public int Variable_Ord;
        public string Variable_Core;
        public int Variable_KeySeq;
        public string Variable_Role;
        public string Variable_DataType;
        public int Variable_Length;
        public int Variable_Decimals;
        public string Variable_Comment;
    }
#endregion 

#region SAScode
    // SAS program
    public class SAS_Program
    {
        public List<string> linesOfCode = new List<string>();
        public void AddNewCode(string s)
        {
            if (s.Length <= 128)
                linesOfCode.Add(s);
            else if (s.Length > 128)
            {
            }
        }

      /*  private List<string> BreakUpLongString(string s)
        {
        }*/
    }

#endregion

#region Custom_Dataset_Stuff
    // Create Custom DataSource
    public class Custom_Dataset_Source
    {
        bool selectAll;
        List<string> select = new List<string>();
        Libname libname;
        StudyDataSet dataset;
        string alias;
        string where;
    }
#endregion
}
