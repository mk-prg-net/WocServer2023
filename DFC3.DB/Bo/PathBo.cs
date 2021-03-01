using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC3.DB.Bo
{
    /// <summary>
    /// mko, 21.06.2018
    /// Business object for PATH- Tab
    /// </summary>
    public class PathBo
    {
        public long DocId { get; set; }
        public DZAUtilities_Dictionaries.GlobalDictionaries.DocTypeSAP DocType { get; set; }
        public string ProjectNo { get; set; }
        public string StationNo { get; set; }
        public string MatNo { get; set; }
        public DateTime SyncLUP { get; set; }
        public DZAUtilities_Dictionaries.GlobalDictionaries.DfcDocStates UserState { get; set; }
        public string CreatorId { get; set; }
        public string StatusChangeOriginator { get; set; }
        public string XPath { get; set; }
        public string FS1 { get; set; }
        public string FS2 { get; set; }
        public string FS3 { get; set; }
        public string FS4 { get; set; }
        public string FS5 { get; set; }
        public string FilePdfName { get; set; }
        public long FilePdfSize { get; set; }
        public string FileName2 { get; set; }
        public long  File2Size { get; set; }
        public string Infos { get; set; }
    }

    public class PathBo2
    {
        public long DocId { get; set; }
        public DZAUtilities_Dictionaries.GlobalDictionaries.DocTypeSAP DocType { get; set; }
        public long ProjectNo { get; set; }
        public long StationNo { get; set; }
        public string MatNo { get; set; }
        public DateTime SyncLUP { get; set; }
        public DZAUtilities_Dictionaries.GlobalDictionaries.DfcDocStates UserState { get; set; }
        public string CreatorId { get; set; }
        public string StatusChangeOriginator { get; set; }
        public string XPath { get; set; }
        public string FS1 { get; set; }
        public string FS2 { get; set; }
        public string FS3 { get; set; }
        public string FS4 { get; set; }
        public string FS5 { get; set; }
        public string FilePdfName { get; set; }
        public long FilePdfSize { get; set; }
        public string FileName2 { get; set; }
        public long File2Size { get; set; }
        public string Infos { get; set; }
    }

}
