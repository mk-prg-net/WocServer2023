using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables
{
    /// <summary>
    /// Manuel Fak, 05.02.2020 - Verzeichnis aller ATMO-Abteilungen mit eindeutig zugewiesener ID
    /// 
    /// mko, 19.2.2020
    /// Konstruktor ohne Aliasname gelöscht. Neuer Konstruktor mit Aliasname angelegt.
    /// </summary>
    public class Dept : Table
    {
        /// <summary>
        /// Tablename mit Alias
        /// </summary>
        /// <param name="Alias"></param>
        public Dept(string Alias = null) : base("dza_admin.BOSCH106DEPT", Alias)
        {
            Id = new ColName(this, "ID");
            Department = new ColName(this, "DEPT");
            LastUpdate = new ColName(this, "LUP");

        }

        //string TabnameOrAlias => HasAlias ? Alias : TableName;


        /// <summary>
        /// Eindeutige ID der ATMO-Abteilungen - Verweis auf SFC-Tabelle (DEPTFROMID, DEPTTOID)
        /// </summary>
        public ColName Id { get; }
        /// <summary>
        /// Bezeichnungen der ATMO-Abteilungen
        /// </summary>
        public ColName Department { get; }
        /// <summary>
        /// Zeitpunkt an dem Abteilungsbezeichnungen geändert wurden
        /// </summary>
        public ColName LastUpdate { get; }
    }
}
