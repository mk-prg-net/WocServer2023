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
    /// Metadata of currently used DZA roles
    /// 
    /// mko, 24.7.2018
    /// shifted to DFC3.DB
    /// </summary>

    public class UserXRoleTab : Table
    {
        public UserXRoleTab()
            : base("dza_admin.USERXROLE")
        {
            USERID = new ColName(TableName, "USERID");
            RoleID = new ColName(TableName, "ROLEID");
        }

        public ColName USERID { get; }
        public ColName RoleID { get; }
    }
}
