using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Woc
{
    /// <summary>
    /// mko, 25.3.2021
    /// Liste der wichtigsten eigenschaften eines Web- Documents (Woc)
    /// </summary>
    public interface IWoc  
        : IWocVersion,
        IWocContext
    {
        
        /// <summary>
        /// mko, 25.3.2021
        /// Klassifizierung des Wocs
        /// </summary>
        long WocTypeId { get; }

        /// <summary>
        /// mko, 25.3.2021
        /// Bezüge auf andere Wocs.
        /// Die Referenzen werden Klassifiziert (RefTypeId), indem auf ein Woc als Klassenhaupt verwiesen wird.
        /// Jede Referenz zeigt auf ein spezielles Woc.
        /// </summary>
        IEnumerable<(long RefTypeId, long WocId)> WocRefs { get; }

    }
}
