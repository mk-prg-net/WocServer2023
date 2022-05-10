using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TRC = MKPRG.Tracing;
using DT = MKPRG.Tracing.DocuTerms;
using NM = MKPRG.Naming;

namespace MKPRG.WormTron
{

    /// <summary>
    /// mko, 1.5.2022
    /// 
    /// Eckpunkte eines Segments
    /// 
    ///    d +---+ c 
    ///      | 𝐒 |
    ///    a +---+ b
    ///
    /// </summary>
    public enum SegmentCornerstones
    {
        /// <summary>
        /// Linke, untere Ecke
        /// </summary>
        a = 0,

        /// <summary>
        /// Rechte, untere Ecke
        /// </summary>
        b = 1,

        /// <summary>
        /// Rechte, obere Ecke
        /// </summary>
        c = 2,

        /// <summary>
        /// Linke, obere Ecke
        /// </summary>
        d = 3
    }

    /// <summary>
    /// mko, 10.5.2022
    /// 
    /// Eckpunkte eines Segments
    /// 
    ///    d +---+ c 
    ///      | 𝐒 |
    ///    a +---+ b
    ///
    /// </summary>

    public enum SegmentBorders
    {
        ab,
        bc,
        cd,
        da
    }
}
