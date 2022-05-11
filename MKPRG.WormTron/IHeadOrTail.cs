using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.WormTron
{
    /// <summary>
    /// 11.5.2022
    /// Zusätzliche Schnittstelle von Wurmsegmenten. Definiert, ab ein Segment 
    /// ein Kopf-, Mittel- oder Schwanzsegment ist.
    /// </summary>
    public interface IHeadOrTail
    {
        bool IsHead { get; }

        bool IsTail { get; }
    }
}
