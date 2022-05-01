using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKPRG.Grid2D;

using TRC = MKPRG.Tracing;


namespace MKPRG.WormTron
{
    

    public interface ISegment
    {
        Gridpoint SegmentCenterPoint { get; }

        /// <summary>
        /// Verschiebt das Segment an den Rasterpunkt p, falls möglich.
        /// Wenn nicht, dann wird eine Fehlermeldung zurückgegeben.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        TRC.RC<Gridpoint> MoveTo(Gridpoint p);

        /// <summary>
        /// Fügt das Segment einem Wurm hinzu.
        /// Ein Wurm ist eine nathlose aneinanderreihung von Segmenten. Jeder Wurm hat eine 
        /// eindeutige ID, die Wurmnummer
        /// </summary>
        /// <param name="WormNo"></param>
        /// <returns></returns>
        TRC.RC InsertIntoWorm(int WormNo);

        
    }
}
