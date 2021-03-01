using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables
{
    /// <summary>
    /// Manuel Fak, 13.03.2020 - Verzeichnis der Statusänderungen von SFCs mit eindeutiger ID
    /// </summary>
    public class SFCLup : Table
    {

        public SFCLup(string Alias = null) : base("dza_admin.BOSCH106SFCLUP", Alias)
        {
            Id = new ColName(this, "ID");
            LUP = new ColName(this, "LUP");
            ComputerName = new ColName(this, "COMPUTERNAME");
        }
        /// <summary>
        /// Eindeutige ID zu SFC-Statusänderungen
        /// </summary>
        public ColName Id { get; }
        /// <summary>
        /// Datum der Statusänderung
        /// </summary>
        public ColName LUP { get; }
        /// <summary>
        /// Computername - Knotenname des Rechners
        /// </summary>
        public ColName ComputerName { get; }
    }
}
