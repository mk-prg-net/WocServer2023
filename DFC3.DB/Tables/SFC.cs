using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables
{
    /// <summary>
    /// Manuel Fak, 10.02.2020 - Verzeichnis aller SFC's generiert aus SFCRecTAbObj-Objekten 
    /// </summary>
    public class SFC : Table
    {
        public SFC(string Alias=null) : base("dza_admin.BOSCH106SFC", Alias)
        {
            DocId = new ColName(this, "DOCID");
            DeptFromId = new ColName(this, "ID_DEPT_FROM");
            DeptToId = new ColName(this, "ID_DEPT_TO");
            SiteId = new ColName(this, "ID_SITE");
            UserStateId = new ColName(this, "ID_USERSTATE");
            ProjNo = new ColName(this, "PJNR");
            StatNo = new ColName(this, "STATNR");
            MatNo = new ColName(this, "MATNR");
            CreationTime = new ColName(this, "CREATIONTIME");
            StatusChanged = new ColName(this, "USERSTATE_LUP");
            //SFCUpdateId = new ColName(this, "ID_SFCLUP");
            UserFromId = new ColName(this, "USRID_FROM");
            UserToId = new ColName(this, "USRID_TO");
            LastUpdateSessionId = new ColName(this, "ID_SFCLUP");

        }
        /// <summary>
        /// SFC - DokumentenID
        /// </summary>
        public ColName DocId { get; }
        /// <summary>
        /// ID der Abteilung die den SFC erstellt hat - Verweis auf Dept-Tabelle
        /// </summary>
        public ColName DeptFromId { get; }
        /// <summary>
        /// ID der Abteilung die den SFC empfangen hat - Verweis auf Dept-Tabelle
        /// </summary>
        public ColName DeptToId { get; }
        /// <summary>
        /// ID des ATMO-Standortes - Verweis auf Site-Tabelle
        /// </summary>
        public ColName SiteId { get; }
        /// <summary>
        /// ID des SFC-Status - Verweis auf Userstate-Tabelle
        /// </summary>
        public ColName UserStateId { get; }
        /// <summary>
        /// SFC-Projektnummer - aus Path/Projektliste2
        /// </summary>
        public ColName ProjNo { get; }
        /// <summary>
        /// SFC-Stationsnummer - aus Path/Projektliste 2
        /// </summary>
        public ColName StatNo { get; }
        /// <summary>
        /// SFC-Materialnummer - aus Path/Projektliste 2
        /// </summary>
        public ColName MatNo { get; }
        /// <summary>
        /// SFC-Erstelldatum - aus Path (PDF_LUP)
        /// </summary>
        public ColName CreationTime { get; }
        /// <summary>
        /// SFC-Statusänderungsdatum - letzte Änderung des Felds USRSTATE in BOSCH106PATH
        /// </summary>
        public ColName StatusChanged { get; }
        /// <summary>
        /// ID der SFC Statusänderung - Verweis auf SFCLup-Tabelle
        /// </summary>
        //public ColName SFCUpdateId { get; }
        /// <summary>
        /// ID des Users, der SFC gestellt hat - Verweis auf User2 -Tabelle
        /// </summary>
        public ColName UserFromId { get; }
        /// <summary>
        /// ID des Users, der SFC empfangen hat und bearbeitet - Verweis auf User2 -Tabelle
        /// </summary>
        public ColName UserToId { get; }
        /// <summary>
        /// SessionId der letzten Aktualisierung. Unter dieser Sitzungnummer 
        /// können dann in der BOSCH106LOG_FS alle Details der Sitzung eingesehen werden.
        /// Zudem wird auf einen Eintrag in der SFCLup- Tabelle verwiesen, welcher 
        /// zu Begin eines FillAndUpdateJobs angelegt wird.
        /// </summary>
        public ColName LastUpdateSessionId { get; }
       
    }
}
