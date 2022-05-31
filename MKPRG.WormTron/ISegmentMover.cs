using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKPRG.Grid2D;

using TRC = MKPRG.Tracing;

namespace MKPRG.WormTron
{
    /// <summary>
    /// mko, 31.5.2022
    /// VErschiebeoperationen auf Segmenten
    /// </summary>
    public interface ISegmentMover
    {

        /// <summary>
        /// Verschiebt das Segment an den Rasterpunkt p, falls möglich.
        /// Wenn nicht, dann wird eine Fehlermeldung zurückgegeben.
        /// 
        /// Die Operation verschiebt das originale Segment und erstellt keine Kopie!
        /// </summary>
        /// <param name="segment">Das zu verschiebende Segment</param>
        /// <param name="p">Der Rasterpunkt, an den das Segment verschoben werden soll</param>
        /// <returns>Das Verschobene Segment, falls erfolgreich. Sonst das ursprüngliche Segment</returns>
        TRC.RC<ISegment> MoveTo(ISegment segment, Gridpoint p);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment">Das zu kopierende Segment</param>
        /// <param name="p">Der Rasterpunkt, an dem die Kopie erstellt werden soll</param>
        /// <returns>Kopie des Segments am definierten Rasterpunkt</returns>
        TRC.RC<ISegment> CopyTo(ISegment segment, Gridpoint p);

    }
}
