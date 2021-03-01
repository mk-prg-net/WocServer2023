using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables
{
    /// <summary>
    /// mko, 13.7.2018
    /// Scheme of Mara2- Table
    /// </summary>
    public class Mara2 : Table
    {

        /// <summary>
        /// Singelton
        /// </summary>
        public static Mara2 _
        {
            get
            {
                if (__ == null)
                {
                    __ = new Mara2();
                }
                return __;
            }
        }
        static Mara2 __;



        public Mara2(string Alias = null) : base("dza_admin.BOSCH106MARA2", Alias)
        {
            MatNr = new ColName(TableName, "MATNR");
            ATD_DocId = new ColName(TableName, "ATD_DOCID");
            ATZ_DocId = new ColName(TableName, "ATZ_DOCID");
            ATO_DocId = new ColName(TableName, "ATO_DOCID");
            CAT_DocId = new ColName(TableName, "CAT_DOCID");
            CTS_DocId = new ColName(TableName, "CTS_DOCID");
            ECA_DocId = new ColName(TableName, "ECA_DOCID");
            MAN_DocId = new ColName(TableName, "MAN_DOCID");
            Pos = new ColName(TableName, "POS");
            PA1 = new ColName(TableName, "PA1");
            PA2 = new ColName(TableName, "PA2");
            PA3 = new ColName(TableName, "PA3");
            PA4 = new ColName(TableName, "PA4");
            PA5 = new ColName(TableName, "PA5");
            PA6 = new ColName(TableName, "PA6");
            PA7 = new ColName(TableName, "PA7");
            PA8 = new ColName(TableName, "PA8");
            PA9 = new ColName(TableName, "PA9");
            PMH = new ColName(TableName, "PMH");
        }

        public ColName ATD_DocId { get; }
        public ColName ATZ_DocId { get; }
        public ColName ATO_DocId { get; }
        public ColName CAT_DocId { get; }
        public ColName CTS_DocId { get; }
        public ColName ECA_DocId { get; }
        public ColName MAN_DocId { get; }
        public ColName Pos { get; }
        public ColName MatNr { get; }
        public ColName PA1 { get; }
        public ColName PA2 { get; }
        public ColName PA3 { get; }
        public ColName PA4 { get; }
        public ColName PA5 { get; }
        public ColName PA6 { get; }
        public ColName PA7 { get; }
        public ColName PA8 { get; }
        public ColName PA9 { get; }
        public ColName PMH { get; }


    }
}
