using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables
{
    /// <summary>
    /// mko, 18.6.2018
    /// Metadata of dza_admin.BOSCH106Path table.
    /// The Path table serves all versions of a document
    /// 
    /// mko, 12.7.2018
    /// XOrder column added
    /// </summary>
    public class Path : Table, IPath
    {
        /// <summary>
        /// Singelton
        /// </summary>
        public static Path _
        {
            get
            {
                if(__ == null)
                {
                    __ = new Path();
                }
                return __;
            }
        }
        static Path __;

        public Path(string Alias = null) : base("dza_admin.BOSCH106PATH", Alias)
        {
            Baselocation = new ColName(TableName, "BASELOCATION");
            DocId = new ColName(TableName, "DOCID");
            XType = new ColName(TableName, "XTYPE");

            // mko, 11.10.2018
            // XORDER war für die Versionsverwaltung von DZA bestimmt. ITERATIONNUMBER übernimmt nun diesen Part.
            //XOrder = new ColName(TableName, "XORDER");
            IterationNo = new ColName(TableName, "ITERATIONNUMBER");

            TDPTyp = new ColName(TableName, "TYP");
            ProjectNo = new ColName(TableName, "PJNR");            
            StationNo = new ColName(TableName, "STATNR");
            MatNr = new ColName(TableName, "MATNR");
            SyncLUP = new ColName(TableName, "SYNC_LUP");
            UserState = new ColName(TableName, "USERSTATE");
            CreatorId = new ColName(TableName, "CREATORID");

            // mko, 28.7.2020
            CreationTime = new ColName(TableName, "CREATIONTIME");

            StatusChangeOriginator = new ColName(TableName, "STATUSAENDERER");
            XPath = new ColName(TableName, "XPATH");
            FS1 = new ColName(TableName, "FS1");
            FS2 = new ColName(TableName, "FS2");
            FS3 = new ColName(TableName, "FS3");
            FS4 = new ColName(TableName, "FS4");
            FS5 = new ColName(TableName, "FS5");
            FilePdfName = new ColName(TableName, "FILE_PDF_NAME");
            FilePdfSize = new ColName(TableName, "FILE_PDF_SIZE");
            FileName2 = new ColName(TableName, "FILE_2_NAME");
            FileName3 = new ColName(TableName, "FILE_3_NAME");
            File2Size = new ColName(TableName, "FILE_2_SIZE");
            Infos = new ColName(TableName, "INFOS");
            Int1 = new ColName(TableName, "INT1");
            Int2 = new ColName(TableName, "INT2");


            // mko, 12.4.2019
            FilePDFLup = new ColName(TableName, "FILE_PDF_LUP");
            File2Lup = new ColName(TableName, "FILE_2_LUP");
            File3Lup = new ColName(TableName, "FILE_3_LUP");

            StorageType = new ColName(TableName, "STORAGETYPE");
            LatestVersion = new ColName(TableName, "LATESTVERSION");
        }

        public ColName StorageType { get; }
        public ColName Baselocation { get; }
        public ColName DocId { get; }
        public ColName XType { get; }
        public ColName IterationNo { get; }
        public ColName TDPTyp { get; }
        public ColName ProjectNo { get; }
        public ColName ProjectDescr { get; }
        public ColName StationNo { get; }
        public ColName MatNr { get; }
        public ColName SyncLUP { get; }
        public ColName UserState { get; }
        public ColName CreatorId { get; }

        // mko, 28.7.2020
        public ColName CreationTime { get; }

        public ColName StatusChangeOriginator { get; }
        public ColName XPath { get; }
        public ColName FS1 { get; }
        public ColName FS2 { get; }
        public ColName FS3 { get; }
        public ColName FS4 { get; }
        public ColName FS5 { get; }
        public ColName FilePdfName { get; }
        public ColName FilePdfSize { get;}
        public ColName FileName2 { get; }
        public ColName FileName3 { get; }
        public ColName File2Size { get; }

        // mko, 12.4.2019
        /// <summary>
        /// Datum letzte Aktualisierung der PDF- Datei
        /// </summary>
        public ColName FilePDFLup { get; }

        /// <summary>
        /// Datum letzte Aktualisierung der Datei in Kammer 2
        /// </summary>
        public ColName File2Lup { get; }

        /// <summary>
        /// Datum letzte Aktualisierung der Datei in Kammer 3
        /// </summary>
        public ColName File3Lup { get; }

        /// <summary>
        /// Liste von attributierten Zusatzinformationen
        /// </summary>
        public ColName Infos { get; }

        /// <summary>
        /// mko, 27.11.2018
        /// Inidikator, der true liefert für die aktuellste Dateiversion
        /// </summary>
        public ColName LatestVersion { get; }

        /// <summary>
        /// 13.02.2020 - Neu angelegte Spalte, die UserId des Konstrukteurs aus Spalte Info separiert enthält - wichtig für SFC Auswertung
        /// </summary>
        public ColName Int1 { get; }
        
        /// <summary>
        /// 13.02.2020 - Neu angelegte Spalte, die UserId des Werkstattaenderers aus Spalte Info enthält (Achtung nur neue SFC's die über DFC kommen!)
        /// </summary>
        public ColName Int2 { get; }


    }
}
