using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables
{

    /// <summary>
    /// mko, 7.12.2018
    /// Master Tabelle mit Systemkonstanten und Systemparametern von DFC2
    /// </summary>
    public class Master : Table
    {

        public Master(string Alias = null) : base("DZA_ADMIN.BOSCH106DFCMASTER", Alias)
        {
            Operand = new ColName(TableName, "OPERAND");
            Wert = new ColName(TableName, "WERT");
            Description = new ColName(TableName, "DESCRIPTION");
        }

        /// <summary>
        /// Attributname 
        /// </summary>
        public ColName Operand { get; set; }

        /// <summary>
        /// Wert
        /// </summary>
        public ColName Wert { get; set; }

        /// <summary>
        /// Parameterbeschreibung
        /// </summary>
        public ColName Description { get; set; }


    }
}
