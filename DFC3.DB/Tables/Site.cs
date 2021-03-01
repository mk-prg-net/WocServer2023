using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables
{
    /// <summary>
    /// Manuel Fak, 10.02.2020 - Verzeichnis aller ATMO-Standorte mit eindeutig zugewiesener ID
    /// </summary>
    public class Site : Table
    {
        public Site(string Alias = null) : base("dza_admin.BOSCH106SITE", Alias)
        {
            Id = new ColName(this, "ID");
            SiteNameShort = new ColName(this, "SITE_SHORT");
            SiteName = new ColName(this, "SITE");
            Description = new ColName(this, "DESCR");
            Plant = new ColName(this, "PLANT");
        }
             /// <summary>
             /// Eindeutige ID der ATMO-Standorte - Verweis auf SFC-Tabelle (SITEID)
             /// </summary>
            public ColName Id { get; }
            /// <summary>
            /// Kurzbezeichner der Standorte (A1, A2...)
             /// </summary>
            public ColName SiteNameShort { get; }
            /// <summary>
            /// Reguläre Standortbezeichnung (ATMO-1DE,...)
            /// </summary>
            public ColName SiteName { get; }
            /// <summary>
            /// Benennung der jeweiligen Standorte mit Ortsnamen (Feuerbach,...)
            /// </summary>
            public ColName Description { get; }
            /// <summary>
            /// Nummer der Standorte (1060, 9651,...)
            /// </summary>
            public ColName Plant { get; }
    }
}
