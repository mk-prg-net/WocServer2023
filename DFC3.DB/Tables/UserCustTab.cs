using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.QueryBuilder;


namespace DFC3.DB.Tables
{
    /// <summary>
    /// mko, 25.5.2018
    /// Created
    /// 
    /// mko, 25.10.2018
    /// Shifted from CustomerControler here
    /// Relational (physical) model
    /// </summary>
    class UserCustTab : Table
    {
        public UserCustTab(string Alias = null)
            : base("dza_admin.BOSCH106USR_CUST", Alias)
        {
            UserID = new ColName(TableName, "USRID");
            FirstName = new ColName(TableName, "USRFIRSTNAME");
            LastName = new ColName(TableName, "USRLASTNAME");
            EMail = new ColName(TableName, "USRMAIL");
            Phone = new ColName(TableName, "USRPHONE");
            Department = new ColName(TableName, "USRDEPT");
            CostCenter = new ColName(TableName, "USRKST");
            CustomerGroup = new ColName(TableName, "CUSTID");
            DFC2FixAssignedVersion = new ColName(TableName, "DFC2_VER");
            DFC2CurrentUsedVersion = new ColName(TableName, "DFC2_VER_CURRENT");

            // mko, 10.1.2020
            LastDFCClientUpdate = new ColName(TableName, "DFC2_VER_CURRENT_LUP");

            DFCInstallerCurrentUsedVersion = new ColName(TableName, "DFC2_UPDOWN_VER");
            LastDFCInstallerUpdate = new ColName(TableName, "DFC2_UPDOWN_VER_LUP");

            // mko, 10.1.2020
            //LastLogin = new ColName(TableName, "DFC2_VER_CURRENT_LUP");
            LastLogin = new ColName(TableName, "LOGIN_DFC2");

            RequestCount = new ColName(TableName, "REQUESTS");
            RASUserId = new ColName(TableName, "EXT_RAS_ACCESS");

            // Last used SessionId of Client.
            // Helps to analyze recently created logfiles in LOG_FS Tab.
            // mko, 19.12.2019
            // Vorher wurden die Sitzungsnummer tatsächlich in der Spalte DFC2_UPDOWN_VER protokolliert.
            // Jetzt wird sie in der neu angelegten Spalte SessionID hinterlegt.
            LastSessionId = new ColName(TableName, "SESSIONID");

        }

        public ColName UserID { get; }

        public ColName FirstName { get; }

        public ColName LastName { get; }        

        public ColName EMail { get; }

        public ColName Phone { get; }

        public ColName Department { get; }

        public ColName CostCenter { get; }

        public ColName CustomerGroup { get; }

        public ColName DFC2FixAssignedVersion { get; }

        public ColName DFC2CurrentUsedVersion { get; }

        /// <summary>
        /// mko, 10.1.2020
        /// </summary>
        public ColName LastDFCClientUpdate { get; }

        /// <summary>
        /// Versionsnummer des Updowngrades- Tools, welches der User aktuell einsetzt.
        /// </summary>
        public ColName DFCInstallerCurrentUsedVersion { get; }

        public ColName LastDFCInstallerUpdate { get; }

        public ColName LastLogin { get; }

        public ColName RequestCount { get; }

        /// <summary>
        /// mko, 26.10.2018
        /// </summary>
        public ColName RASUserId { get; }

        /// <summary>
        /// Last used SessionId of Client.
        /// Helps to analyze recently created logfiles in LOG_FS Tab.
        /// </summary>
        public ColName LastSessionId { get; }

    }
}
