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
    /// mko, 2.5.2019
    /// Diese Tabelle ist eine m:n Beziehung zwischen Manuals und Baugruppen.
    /// Die Materialnummer einer Baugruppe wird dabei der Materialnummer (DokuMatNr) eines Manuals zugerodnet und umgekehrt
    /// </summary>
    public class DocuMat : Table
    {
        public ColName ID { get; }
        public ColName MatNo { get; }
        public ColName DocMatNo { get; }
        public ColName ManualLanguageISO { get; }
        public ColName LastUpdate { get; }
        public ColName UpdatedBy { get; }
        public ColName Created { get; }
        public ColName Creator { get; }

        public DocuMat(string Alias = null) : base("dza_admin.BOSCH106DOKUMAT", Alias)
        {
            ID = new ColName(TableName, "MATNRLAISO");
            MatNo = new ColName(TableName, "MATNR");
            DocMatNo = new ColName(TableName, "DOKUMATNR");
            ManualLanguageISO = new ColName(TableName, "LAISO");
            LastUpdate = new ColName(TableName, "LUP");
            UpdatedBy = new ColName(TableName, "AENAM");
            Created = new ColName(TableName, "ANDAT");
            Creator = new ColName(TableName, "ANNAM");
        }

    }
}
