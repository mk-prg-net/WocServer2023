using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables.DZA
{
    /// <summary>
    /// mko, 12.6.2018
    /// Metadata of currently used DZA usertable.
    /// 
    /// mko, 24.7.2018
    /// shifted to DFC3.DB
    /// </summary>
    public class XUserTab : Table
    {
        public XUserTab()
            : base("dza_admin.XUSER")
        {
            ID = new ColName(TableName, "ID");
            UserName = new ColName(TableName, "NAME");
            FirstName = new ColName(TableName, "FIRSTNAME");
            LastName = new ColName(TableName, "LASTNAME");
            Title = new ColName(TableName, "TITLE");
            Language = new ColName(TableName, "LANGUAGE");
        }

        public ColName ID { get; }
        public ColName UserName { get; }
        public ColName FirstName { get; }
        public ColName LastName { get; }
        public ColName Title { get; } 
        public ColName Language { get; }
    }


}
