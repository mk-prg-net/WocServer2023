using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables
{
    /// <summary>
    /// Manuel Fak, 06.02.2020 - Tabelle zur Statusanzeige von SFC's
    /// </summary>
    public class SFCUserstate
        : Table
    {
        public SFCUserstate(string Alias = null):base("dza_admin.BOSCH106SFCUSERSTATE", Alias)
        {
            Id = new ColName(this, "ID");
            Description = new ColName(this, "DESCR");

        }
        /// <summary>
        /// Eindeutige Id der SFC-Status - Verweis auf SFC-Tabelle unter USERSTATEID
        /// </summary>
        public ColName Id { get; }
        /// <summary>
        /// Statusbezeichnung (open, solved, declined)
        /// </summary>
        public ColName Description { get; }

    }
}
