using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Woc
{
    /// <summary>
    /// mko, 26.3.2021
    /// Methoden zum Aufspüren veralteter Woc Versionen
    /// </summary>
    public interface IWocVersion
    {
        /// <summary>
        /// Eineindeutige Id eines Web- Documents (WOC)
        /// </summary>
        long WocId { get; }

        /// <summary>
        /// mko, 25.3.2021
        /// Versionsnummer des Woc
        /// </summary>
        int WocVersion { get; }
    }
}
