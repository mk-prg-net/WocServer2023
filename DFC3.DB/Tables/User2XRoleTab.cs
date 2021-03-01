using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables
{
    /// <summary>
    /// mko, 14.9.2018
    /// Metadata of user2xrole tab
    /// </summary>
    public class User2XRoleTab : Table
    {
        
        public User2XRoleTab(string Alias = null)
            : base("dza_admin.BOSCH106USR2XROLE", Alias)
        {
            UserID = new ColName(TableName, "USRID");
            RoleID = new ColName(TableName, "ROLEID");
            Description = new ColName(TableName, "DESCRIPTION");
        }

        public ColName UserID { get; }
        public ColName RoleID { get; }
        public ColName Description { get; }


    }
}
