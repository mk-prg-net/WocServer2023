using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables
{
    /// <summary>
    /// mko, 19.11.2018
    /// </summary>
    public class CustGroupTab : Table
    {
        public CustGroupTab()
        : base("dza_admin.BOSCH106USR_CUST_GRP")
        {
            ID = new ColName(TableName, "ID");
            CustGroupId = new ColName(TableName, "CUSTID");
            CustGroupDescription = new ColName(TableName, "CUST_NAME");
            CustGroupAdmins = new ColName(TableName, "CUST_ADMIN1");
        }

        public ColName ID { get; }
        public ColName CustGroupId { get; }
        public ColName CustGroupDescription { get; }
        public ColName CustGroupAdmins { get; }
    }
}
