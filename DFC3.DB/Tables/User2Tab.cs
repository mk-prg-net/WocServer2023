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
    /// moved from co worker here
    /// 
    /// mko, 26.10.2018
    /// added RASUserId
    /// </summary>
    public class User2Tab : Table
    {
        public User2Tab(string Alias = null)
            : base("dza_admin.BOSCH106USR2", Alias)
        {
            UserID = new ColName(TableName, "USRID");
            FirstName = new ColName(TableName, "USRFIRSTNAME");
            LastName = new ColName(TableName, "USRLASTNAME");
            EMail = new ColName(TableName, "USRMAIL");
            Phone = new ColName(TableName, "USRPHONE");
            Department = new ColName(TableName, "USRDEPT");
            CostCenter = new ColName(TableName, "USRKST");
            LastLogin = new ColName(TableName, "DFC2_LASTLOGON");
            LastDFCClientUpdate = new ColName(TableName, "DFC2_VER_CURRENT_LUP");
            DFC2FixAssignedVersion = new ColName(TableName, "DFC2_VER");
            DFC2CurrentUsedVersion = new ColName(TableName, "DFC2_VER_CURRENT");
            RASUserId = new ColName(TableName, "EXT_RAS_ACCESS");

            // if content not null the user is disabled. That means, DFC access for user is forbidden.
            Disabled = new ColName(TableName, "USRDISABLED");

            // Last used SessionId of Client.
            // Helps to analyze recently created logfiles in LOG_FS Tab.
            LastSessionId = new ColName(TableName, "DFC2_WS_USAGE");


            DFCInstallerCurrentUsedVersion = new ColName(TableName, "DFC2_UPDOWN_VER");
            LastDFCInstallerUpdate = new ColName(TableName, "DFC2_UPDOWN_VER_LUP");
        }

        /// <summary>
        /// Bosch- Userid
        /// </summary>
        public ColName UserID { get; }

        public ColName FirstName { get; }
        public ColName LastName { get; }
        public ColName EMail { get; }

        public ColName Phone { get; }

        public ColName Department { get; }

        public ColName CostCenter { get; }

        /// <summary>
        /// Datum des letzten Logins
        /// </summary>
        public ColName LastLogin { get; }

        /// <summary>
        /// DFC2 Client Version, welcher der User nutzen soll
        /// </summary>
        public ColName DFC2FixAssignedVersion { get; }

        /// <summary>
        /// Versionsnummer des aktuell vom User verwendeten Client
        /// </summary>
        public ColName DFC2CurrentUsedVersion { get; }

        /// <summary>
        /// Datum des letzten DFC2 Client Updates
        /// </summary>
        public ColName LastDFCClientUpdate { get; }

        /// <summary>
        /// Externe RAS- Kennung. Ist die beim Login über RAS vom DFC- Client verwendete Userid. 
        /// Wird durch die Authentifizierung in eine Bosch- Userid umgewandelt.
        /// </summary>
        public ColName RASUserId { get; }

        /// <summary>
        /// if content not null the user is disabled. That means, DFC access for user is forbidden. 
        /// </summary>
        public ColName Disabled { get; }

        /// <summary>
        /// Last used SessionId of Client.
        /// Helps to analyze recently created logfiles in LOG_FS Tab.
        /// </summary>
        public ColName LastSessionId { get; }

        /// <summary>
        /// mko, 23.12.2019
        /// Aktuell installierte Version des DFCinstallers auf dem Clientrechner
        /// </summary>
        public ColName DFCInstallerCurrentUsedVersion { get; }

        /// <summary>
        /// mko, 23.12.2019
        /// Datum der letzten Aktualisierung der DFCInstaller instanz auf dem Clientrechner.
        /// </summary>
        public ColName LastDFCInstallerUpdate { get; }
    }
}
