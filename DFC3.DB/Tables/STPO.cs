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
    /// mko, 1.8.2019
    /// Tabelle mit den Stücklistenpositionen
    /// </summary>
    public class STPO : Table
    {
        public ColName BGMatNr { get; }
        public ColName PosNr { get; }
        public ColName Menge { get; }
        public ColName MatNr { get; }
        public ColName EVW { get; }
        public ColName Doku { get; }
        public ColName Beschaff { get; }
        public ColName Lup { get; }
        public ColName LupTrans { get; }        

        public STPO(string Alias = null) : base("dza_admin.BOSCH106STPO", Alias)
        {
            BGMatNr = new ColName(TableName, "BGMATNR");
            PosNr = new ColName(TableName, "POSNR");
            Menge = new ColName(TableName, "MENGE");
            MatNr = new ColName(TableName, "MATNR");
            EVW = new ColName(TableName, "EVW");
            Doku = new ColName(TableName, "DOKU");
            Beschaff = new ColName(TableName, "BESCHAFF");
            Lup = new ColName(TableName, "LUP");
            LupTrans = new ColName(TableName, "LUPTRANS");
        }

    }
}
