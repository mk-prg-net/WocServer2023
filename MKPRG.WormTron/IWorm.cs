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
    /// Elektrische Leiterbahn als *Wurm* aus Segmenten.
    /// Der Kleinste Wurm besteht stets aus einem Kopf- und einem
    /// Schwanzsegment (Header, Tail):
    /// 
    ///    +----++----+
    ///    |H + || + T| 
    ///    +----++----+
    ///    
    /// Würmer können wachsen, indem am Kopf oder am Schwanz Segmente hinzugefügt werden.
    /// Diese neuen Segmente ünernehmen dann die Rolle des Kopfes bzw. des Schwanzes.
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
        /// Holt das Kopfsegment des Wurmes.        
        /// </summary>
        /// <returns></returns>
        TRC.RC<ISegment> GetHead();

        /// <summary>
        /// Fügt ein neues Segment am Kopfsegment in der definierten Nachbarschaft hinzu,
        /// und macht dieses zum neuen Kopfsegment. Das alte Kopfsegment wird dabei zu einem gewöhnlichen Segment.
        /// </summary>
        /// <returns></returns>
        TRC.RC<ISegment> AddNewHead(Neighbor neighbor);

        /// <summary>
        /// Holt die 
        /// </summary>
        /// <returns></returns>
        TRC.RC<ISegment> GetTail();

        /// <summary>
        /// Fügt ein neues Segment in der definierten Nachbarschaft am Schwanzende hinzu, und macht dieses zum neuen 
        /// Schwanzende. Das alte Schwanzende wird dabei zu einem gewöhnlichen Segment.
        /// </summary>
        /// <returns></returns>
        TRC.RC<ISegment> AddNewTail(Neighbor neighbor);

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
