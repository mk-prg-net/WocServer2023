using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NM = MKPRG.Naming;
using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

using TRC = MKPRG.Tracing;
using DT = MKPRG.Tracing.DocuTerms;
using static MKPRG.Tracing.DocuTerms.ComposerSubTrees;

namespace MKPRG.Raster2D
{

    /// <summary>
    /// mko, 1.5.2022
    /// Modelliert ein Raster als Menge von Wertepaaren über ℤ.
    /// </summary>
    public class Raster2D
    {
        DT.IComposer pnL;

        NM.NamingHelper NH;

        /// <summary>
        /// Maximale Ausdehnung des Rasters in X- Richtung
        /// </summary>
        readonly int maxX;

        /// <summary>
        /// Maximale Ausdehnung des Rasters in Y-Richtung
        /// </summary>
        readonly int maxY;

        public Raster2D(DT.IComposer pnL, NM.NamingHelper NH, int maxX, int maxY)
        {
            this.pnL = pnL;
            this.NH = NH;
            this.maxX = maxX;
            this.maxY = maxY;
        }

        public bool IsRasterpoint(Rasterpoint p)
            => p.X <= maxX && p.Y <= maxY;

        public TRC.RC<Rasterpoint> LeftOf(Rasterpoint p)
        {
            var ret = TRC.RC<Rasterpoint>.Failed(Rasterpoint.Undefined, ErrorDescription: pnL.eNotCompleted());
            if(p.X - 1 >= 0)
            {
                ret = TRC.RC<Rasterpoint>.Ok(new Rasterpoint() { X = p.X - 1, Y = p.Y });
            }
            else
            {
                ret = TRC.RC<Rasterpoint>.Failed(p, pnL.InProgressActivityStatement(
                            pnL.DefObject(TT.Raster2D.Rasterpoint.UID),

                        )
            }

        }
        


    }
}
