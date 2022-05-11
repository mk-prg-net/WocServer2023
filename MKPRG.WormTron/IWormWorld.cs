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
    /// mko, 11.5.2022
    /// Welt der Würmer, die zu Leiterbahnen werden.
    /// </summary>
    public interface IWormWorld
    {
        /// <summary>
        /// Erzeugt einen neuen Wurm. Der Kopf muss als Rasterpunkt, und der 
        /// Schwanz als Nachbar vom Kopf definiert werden.
        /// </summary>
        /// <param name="pHead"></param>
        /// <param name="TailAsNeighborOfHead"></param>
        /// <returns></returns>
        TRC.RC<IWorm> CreateWorm(Gridpoint pHead, Neighbor TailAsNeighborOfHead);

        /// <summary>
        /// Löscht einen in der Wurmwelt existierenden Wurm.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        TRC.RC DeleteWorm(IWorm wormToDelete);

        /// <summary>
        /// Liefert alle aktuell in der Wurmwelt existenten Würmer
        /// </summary>
        IEnumerable<IWorm> Worms { get; }

        /// <summary>
        /// Berechnet alle Wurmknäuls, die aktuell existieren.
        /// </summary>
        /// <returns></returns>
        Task<TRC.RC<IEnumerable<IWormBall>>> GetAllWormBallsAsync();


    }
}
