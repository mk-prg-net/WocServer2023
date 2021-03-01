using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC3.DB.Bo
{
    /// <summary>
    /// mko, 23.10.2019
    /// DFC- Systemstatus aus der Mastertabelle
    /// </summary>
    public class DFCSystemStatusBo
    {
        public ATMO.mko.Logging.SystemStatus Status { get; set; }        

        /// <summary>
        /// True, wenn in der DFC- Master die Details als DocuTerms definiert wurden
        /// </summary>
        public bool AreDetailsInPNFormat { get; set; }
        
        /// <summary>
        /// Details zum Systemzustand in Form eines DocuTerms beschrieben
        /// </summary>
        public ATMO.mko.Logging.PNDocuTerms.DocuEntities.IDocuEntity DetailsPN { get; set; }

        /// <summary>
        /// Details zum Systemzustand durch einen einfachen String beschrieben
        /// </summary>
        public string DetailsString { get; set; }


    }

}
