using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.WormTron
{
    /// <summary>
    ///  mko, 10.5.2022
    ///  
    /// Wurmknäuel: Besteht aus mehreren, übereinanderliegenden Würmern, und stellt damit einen 
    /// Leiterbahnzug dar, die einem baumähnlichen Gebilde entspricht.
    /// An den Kontaktstellen der Würmer untereinander werden besondere Kreuzungssegmente eingesetzt,
    /// so daß die Würmer zu einem Leiterbahnenzug verschmelzen.
    /// 
    ///    +----++----+
    ///    |H + || +  | 
    ///    +----++----+
    ///          +----+
    ///          | +  |
    ///          +----+
    ///    +----++----++----++----++----+
    ///    |H + || X  || +  || +  || + T| 
    ///    +----++----++----++----++----+
    ///          +----+
    ///          | +  |
    ///          +----+
    ///          +----++----++----+
    ///          | +  || +  || + T| 
    ///          +----++----++----+
    ///          
    /// </summary>
    public interface IWormBall
    {
        IEnumerable<IWorm> WormBallWorms { get; }
    }
}
