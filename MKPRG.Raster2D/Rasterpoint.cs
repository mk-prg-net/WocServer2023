﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Raster2D
{
    /// <summary>
    /// mko, 1.5.2022
    /// Stellt einen Rasterpunkt dar.
    /// </summary>
    public struct Rasterpoint
    {
        /// <summary>
        /// X- Koordinate des Rasterpunktes
        /// </summary>
        public int X { get; init; }

        /// <summary>
        /// Y- Koordinate des Rasterpunktes
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Steht für einen undefinierten/ungültigen Rasterpunkt
        /// </summary>
        /// <returns></returns>
        public static Rasterpoint Undefined => new Rasterpoint() { X = -1, Y = -1 };
    }
}
