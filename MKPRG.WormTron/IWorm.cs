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
    /// mko, 10.5.2022
    /// 
    /// Elektrische Leiterbahn als *Wurm* aus Segmenten
    /// </summary>
    public interface IWorm
    {
        /// <summary>
        /// Wurmnummer
        /// </summary>
        int WormId { get; }
        
        /// <summary>
        /// Liste der Segmente, aus denen der Wurm besteht
        /// </summary>
        IEnumerable<ISegment> Segments { get; }

        /// <summary>
        /// Fügt dem Wurm ein Segment an.
        /// Folgende Fälle sind möglich;
        /// 1) Segment ist das erste Segment vom Wurm
        /// 2) Segment wird vonr angefügt
        /// 3) Segment wird hinten angefügt
        /// 4) Alle anderen Fälle sind ein Fehler
        /// </summary>
        /// <param name="newSegment"></param>
        /// <returns></returns>
        TRC.RC AddSegment(ISegment newSegment);

        /// <summary>
        /// Kürzt den Wurm vom Kopf ausgehend um n Segmente
        /// </summary>
        /// <returns></returns>
        TRC.RC RemoveSegmentsFromHead(int n);

        /// <summary>
        /// Kürzt den Wurm vom Schwanzende her um n Segmente
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        TRC.RC RemoveSegmentsFromTail(int n);

        /// <summary>
        /// Trennt einen Wurm an der definierten Trennstelle in zwei Würmer auf.
        /// Wenn erfolgreich, dann werden die beiden neu entstandenen Würmer zurückgegeben, und dieser 
        /// Wurm ist aufzugeben.
        /// </summary>
        /// <param name="SeparationPoint"></param>
        /// <returns></returns>
        TRC.RC<(IWorm, IWorm)> DivideWormAtSegment(Gridpoint SeparationPoint);

    }
}
