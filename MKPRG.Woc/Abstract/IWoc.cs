using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Woc
{
    /// <summary>
    /// mko, 25.3.2021
    /// Liste der wichtigsten Eigenschaften eines Web- Documents (Woc)
    /// 
    /// mko, 27.01.2024
    /// Woc als Knoten in einem semantischen Netzwerk redefinieren
    /// </summary>
    public interface IWoc        
    {        
        /// <summary>
        /// mko, 25.3.2021
        /// Klassifizierung des Wocs
        /// 
        /// mko, 27.1.2024
        /// Jedes Woc hat eine Id (analog den Namingcontainern, bzw. jeder Namingcontainer ist
        /// ein Woc, dessen WocId die NamingId ist)
        /// </summary>
        long WocId { get; }


        /// <summary>
        /// mko, 25.3.2021
        /// Bezüge auf andere Wocs (semantisches Netzwerk).
        /// Die Referenzen werden Klassifiziert (RefTypeId = WocId des Wocs, das die semantischen Referenz definiert).
        ///
        /// mko, 27.1.2024
        /// Als Verarbeitungsstufe im **Stack Flow** Style neu definiert
        /// </summary>
        /// <param name="RefTypeId"></param>
        /// <param name="ifRefersToWocId">wird aufgerufen, wenn bezüglich RefTypeId Wocs existieren. Die ReftypeId und die IDs der gefundenen Wocs werden übergeben</param>
        /// <param name="ifNotRefersToAny">wird aufgerufen, wenn bezüglich RefTypeId keine Wocs existieren. Die RefTypeId wird übergeben.</param>
        /// <param name="ifErrorOccured"></param>
        /// <returns></returns>
        IStEx GetReferredWocFor(long RefTypeId, Func<long, IEnumerable<long>, IStEx> ifRefersToWocId, Func<long, IStEx> ifNotRefersToAny, Func<IErr, IStEx> ifErrorOccured);

    }
}
