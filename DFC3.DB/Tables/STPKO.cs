using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables
{
    /// <summary>
    /// mko, 19.10.2018
    /// Tabelle mit den Stücklistenköpfen
    /// </summary>
    public class STKO : Table
    {

        public ColName MatNo { get; }
        public ColName StlStatus { get; }
        public ColName Lup { get; }
        public ColName Abt { get; }
        public ColName PI1 { get; }
        public ColName PI2 { get; }
        public ColName Pos { get; }
        public ColName ATBDocId { get; }
        public ColName Projektstrukturfehler { get; }

        public STKO(string Alias = null) : base("dza_admin.BOSCH106STKO", Alias)
        {
            MatNo = new ColName(TableName, "MATNR");
            StlStatus = new ColName(TableName, "STLSTATUS");
            Pos = new ColName(TableName, "POS");
            Lup = new ColName(TableName, "LUP");
            Abt = new ColName(TableName, "ABT");
            PI1 = new ColName(TableName, "PI1");
            PI2 = new ColName(TableName, "PI2");
            ATBDocId = new ColName(TableName, "ATB_DOCID");
            Projektstrukturfehler = new ColName(TableName, "PROJEKTSTRUKTURFEHLER");
        }

        /// <summary>
        /// Singelton
        /// </summary>
        public static STKO _
        {
            get
            {
                if (__ == null)
                {
                    __ = new STKO();
                }
                return __;
            }
        }
        static STKO __;



    }
}
