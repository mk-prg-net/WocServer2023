using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC3.DB.Bo
{
    /// <summary>
    /// mko, 5.11.2019
    /// 
    /// Stücklistenkopf- Datensatz
    /// </summary>
    public class StKoBo
    {
        /// <summary>
        /// Materialnummer (Projekt, Station oder Baugruppe)
        /// </summary>
        public string MatNr { get; set; }

        /// <summary>
        /// Stücklistenstatus
        /// </summary>
        public string StlStatus { get; set; }

        /// <summary>
        /// Zeitpunkt der letzten Aktualisierung
        /// </summary>
        public DateTime LUP { get; set; }

        /// <summary>
        /// Stücklistenposition ?
        /// </summary>
        public int Pos { get; set; }       

    }
}
