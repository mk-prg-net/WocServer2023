using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.WormTron
{
    public interface IWormTronCompiler
    {
        /// <summary>
        /// mko, 10.5.2022
        /// Fährt die Umrandung des Wurmes ab und erzeugt daraus einen **GCode**
        /// </summary>
        /// <param name="worm"></param>
        /// <returns></returns>
        IEnumerable<CNC.GCode.IGCode> WromToGCode(IWorm worm);

        /// <summary>
        /// Wandelt ein Wurmknäuel in einen GCode zum Ausfräsen einer entsprechenden 
        /// Leiterbahn.
        /// </summary>
        /// <param name="wormball"></param>
        /// <returns></returns>
        IEnumerable<CNC.GCode.IGCode> WromballToGCode(IWormBall wormball);
    }
}
