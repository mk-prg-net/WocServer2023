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
    /// 
    /// Verschiebeoperationen auf Würmern
    /// </summary>
    public interface IWormMover
    {
        /// <summary>
        /// Verschiebt einen Wurm im Raster. Die Verschiebung wird als Verschiebevektor
        /// definiert (shiftX, shiftY), und auf jedes Segment angewendet.
        /// 
        /// 
        ///  +---+            +---+
        ///  | o |            | o |
        ///  +---+ shiftX: 2  +---+
        ///  | o-|----------->| o | 
        ///  +---+ shiftY: 0  +---+
        ///  | o |            | o |
        ///  +---+            +---+
        ///  
        ///  +---+  
        ///  | o |  
        ///  +---+ shiftX: 1
        ///  | o-|\
        ///  +---+ \  +---+
        ///  | o |  \ | o |
        ///  +---+   \+---+ 
        ///           | o | shiftY: 1
        ///           +---+
        ///           | o |
        ///           +---+
        /// </summary>
        /// <param name="worm"></param>
        /// <param name="shiftX"></param>
        /// <param name="shiftY"></param>
        /// <returns></returns>
        TRC.RC<IWorm> ShiftWorm(IWorm worm, int shiftX, int shiftY);

        /// <summary>
        /// Kopiert einen Wurm im Raster. Die Verschiebung wird als Verschiebevektor
        /// definiert (shiftX, shiftY), und auf jedes Segment angewendet.
        /// Es entsteht eine Kopie des ursprünglichen Wurms.        
        /// 
        /// </summary>
        /// <param name="worm"></param>
        /// <param name="shiftX"></param>
        /// <param name="shiftY"></param>
        /// <returns></returns>
        TRC.RC<IWorm> CopyWorm(IWorm worm, ISegment DragPoint, Gridpoint DropPoint);

        /// <summary>
        /// Dreht einen Wurm um 90° im Uhrzeigersinn. Die Drehung erfolgt um den 
        /// **CenterPoint**. Dieser kann innerhalb und außerhalb des Wurmes liegen.
        /// </summary>
        /// <param name="worm"></param>
        /// <param name="CenterPoint"></param>
        /// <returns></returns>
        TRC.RC<IWorm> Rotate90Clockwise(IWorm worm, Gridpoint CenterPoint);

        /// <summary>
        /// Dreht einen Wurm um 90° gegen den Uhrzeigersinn. Die Drehung erfolgt um den 
        /// **CenterPoint**. Dieser kann innerhalb und außerhalb des Wurmes liegen.
        /// </summary>
        /// <param name="worm"></param>
        /// <param name="CenterPoint"></param>
        /// <returns></returns>
        TRC.RC<IWorm> Rotate90CounterClockwise(IWorm worm, Gridpoint CenterPoint);

    }
}
