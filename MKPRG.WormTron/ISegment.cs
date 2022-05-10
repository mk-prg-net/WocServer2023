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
        /// <summary>
        /// Soll gebohrt werdenam Rasterpunkt
        /// </summary>
        bool ToBeDrilled { get; }

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
        /// Ein Wurm ist eine nathlose Aneinanderreihung von Segmenten. Jeder Wurm hat eine 
        /// eindeutige ID, die Wurmnummer
        /// </summary>
        /// <param name="WormNo"></param>
        /// <returns></returns>
        TRC.RC InsertIntoWorm(int WormNo);

        /// <summary>
        /// Wurmnummer
        /// </summary>
        int WormNo { get; }

        /// <summary>
        /// Liefert eine Liste aller aktuell existenter Segmentgrenzen.
        /// Stossen Segmente beim Aufbau eines Wurmes zusammen, dann verschwinden
        /// die Segmengrenzen an der Stossstelle.
        /// Werden Segmente voneinander getrennt, dann bilden sich neue Segmentgrenzen
        /// an den Trennstellen.
        /// </summary>
        IEnumerable<SegmentBorders> Borders { get; }

        /// <summary>
        /// mko, 10.5.2022
        /// Fügt eine neue Segmentgrenze hinzu, z.B. nach dem Teilen eines Wurmes.
        /// </summary>
        /// <param name="newBorder"></param>
        /// <returns></returns>
        TRC.RC AddBorder(SegmentBorders newBorder);

        /// <summary>
        /// mko, 10.5.2022
        /// 
        /// Löscht eine Grenze, z.B. nach dem Verschmelzen von zwei Segmenten
        /// zu einem Wurm.
        /// </summary>
        /// <param name="lostBorder"></param>
        /// <returns></returns>
        TRC.RC RemoveBorder(SegmentBorders lostBorder);

        
    }
}
