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
    /// mko, 02.07.2018
    /// Stucture of common log table
    /// </summary>
    public class DFCLog2 : Table
    {

        /// <summary>
        /// Singelton
        /// </summary>
        public static DFCLog2 _
        {
            get
            {
                if (__ == null)
                {
                    __ = new DFCLog2();
                }
                return __;
            }
        }
        static DFCLog2 __;



        public DFCLog2(string Alias = null) : base("dza_admin.BOSCH106DFCLOG2", Alias)
        {
            Id = new ColName(TableName, "ID");
            UserId = new ColName(TableName, "USRID");
            CustId = new ColName(TableName, "CUSTID");
            Computername = new ColName(TableName, "COMPUTERNAME");
            PgmVersion = new ColName(TableName, "PGM_VERSION");
            PSP = new ColName(TableName, "PSP");
            MatNo = new ColName(TableName, "MATNR");
            TimeClient = new ColName(TableName, "TIME_CLIENT");
            TimeServer = new ColName(TableName, "TIME_SERVER");
            Transaction = new ColName(TableName, "TRANSACTION");
            TransactionId = new ColName(TableName, "TRANSACTION_ID");
            TransactionValue = new ColName(TableName, "TRANSACTION_VALUE");
            XKey = new ColName(TableName, "XKEY");
            Description = new ColName(TableName, "DESCRIPTION");
        }

        public ColName Id { get; }
        public ColName UserId { get; }
        public ColName CustId { get; }
        public ColName Computername { get; }
        public ColName PgmVersion { get; }
        public ColName PSP { get; }
        public ColName MatNo { get; }
        public ColName TimeClient { get; }
        public ColName TimeServer { get; }
        public ColName Transaction { get; }
        public ColName TransactionId { get; }
        public ColName TransactionValue { get; }
        public ColName XKey { get; }
        public ColName Description { get; }


    }
}
