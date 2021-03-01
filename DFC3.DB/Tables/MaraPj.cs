using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables
{
    /// <summary>
    /// mko, 12.7.2018
    /// Metadata of MaraPj- Table
    /// MaraPj stores project related metadata of Bom Nodes
    /// 
    /// mko, 30.1.2020
    /// Erweitert um Spalten CV, CVBG und CVPOS für den Zugriff auf die Merkmalsbewertungen von Baugruppen.
    /// </summary>
    public class MaraPj : Table
    {
        /// <summary>
        /// Singelton
        /// </summary>
        public static MaraPj _
        {
            get
            {
                if (__ == null)
                {
                    __ = new MaraPj();
                }
                return __;
            }
        }
        static MaraPj __;

        public MaraPj(string Alias = null) : base("dza_admin.BOSCH106MARAPJ", Alias)
        {
            PjNrStatNrMatNr = new ColName(TableName, "PJNRSTATNRMATNR");
            PjNr = new ColName(TableName, "PJNR");
            StatNr = new ColName(TableName, "STATNR");
            MatNr = new ColName(TableName, "MATNR");
            StlStatus = new ColName(TableName, "STLSTATUS");
            EVW = new ColName(TableName, "EVW");
            Doku = new ColName(TableName, "DOKU");
            StlNr = new ColName(TableName, "STLNR");
            Lup = new ColName(TableName, "LUP");

            CVBG = new ColName(TableName, "CVBG");
            CVPOS = new ColName(TableName, "CVPOS");
            CV = new ColName(TableName, "CV");
        }

        public ColName PjNrStatNrMatNr { get; }
        public ColName PjNr { get; }
        public ColName StatNr { get; }
        public ColName MatNr { get; }
        public ColName StlStatus { get; }

        /// <summary>
        /// Baugruppe (MatNr) einer Station eines Projektes, für die Merkmale bewertet wurden
        /// </summary>
        public ColName CVBG { get; }

        /// <summary>
        /// Positionsnummer der Baugruppe innerhalb einer Station, dessen Merkmale bewertet wurden
        /// </summary>
        public ColName CVPOS { get; }

        /// <summary>
        /// CSV- Liste von Attribut- Wertepaaren mit den bewerteten Merkmalen
        /// </summary>
        public ColName CV { get; }

        /// <summary>
        /// Ersatzteil/Verschleisteil- Kennung
        /// </summary>
        public ColName EVW { get; }

        /// <summary>
        /// Doku- Haken
        /// </summary>
        public ColName Doku { get; }

        public ColName Lup { get; }
        public ColName StlNr { get; }
    }
}
