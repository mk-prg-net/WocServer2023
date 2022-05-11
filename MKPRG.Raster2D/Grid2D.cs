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

namespace MKPRG.Grid2D
{

    /// <summary>
    /// mko, 1.5.2022
    /// Modelliert ein Raster als Menge von Wertepaaren über ℤ.
    /// </summary>
    public class Grid2D
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

        public Grid2D(DT.IComposer pnL, NM.NamingHelper NH, int maxX, int maxY)
        {
            this.pnL = pnL;
            this.NH = NH;
            this.maxX = maxX;
            this.maxY = maxY;
        }

        public bool IsGridpoint(Gridpoint p)
            => p.X <= maxX && p.Y <= maxY;


        public TRC.RC<Gridpoint> Create(int X, int Y)
        {
            var ret = TRC.RC<Gridpoint>.Failed(Gridpoint.Undefined, ErrorDescription: pnL.eNotCompleted());
            if(-1 < X && X <= maxX && -1 < Y && Y <= maxY)
            {
                ret = TRC.RC<Gridpoint>.Ok(new Gridpoint() { X = X, Y = Y });
            }
            else
            {
                ret = TRC.RC<Gridpoint>.Failed(Gridpoint.Undefined,
                                            ErrorDescription: 
                                                pnL.m(TT.Grid2D.Create.UID,
                                                    pnL.p(TT.Grid2D.Gridpoint.UID,
                                                            pnL.List(
                                                                pnL.p("X", X),
                                                                pnL.p("Y", Y))),
                                                    pnL.InProgressActivityStatement(
                                                        pnL.DefObject(TT.Grid2D.Gridpoint.UID),
                                                        NH.pA(TT.Operators.Sets.IsOutOfRange.UID))));
            }

            return ret;
        }


        /// <summary>
        /// Liefert einen Nachbarn eines gegeben Rasterpunktes. Die Nachbarschaftsbeziehung kann über einen Parameter definiert werden.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="neighbor"></param>
        /// <returns></returns>
        public TRC.RC<Gridpoint> NeighborOf(Gridpoint p, Neighbor neighbor)
        {
            var ret = TRC.RC<Gridpoint>.Failed(Gridpoint.Undefined,
                        ErrorDescription: pnL.ReturnValidatePreconditionFailedArgumentOutOfRange(pnL.p("Neighbor", neighbor.ToString())));

            switch (neighbor)
            {
                case Neighbor.Left:
                    ret = LeftOf(p);
                    break;
                case Neighbor.Right:
                    ret = RigthOf(p);
                    break;
                case Neighbor.Up:
                    ret = UpperOf(p);
                    break;
                case Neighbor.Down:
                    ret = LowerOf(p);
                    break;                    
            }

            return ret;
        }

        /// <summary>
        /// Gibt linken Rasterpunkt zurück
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public TRC.RC<Gridpoint> LeftOf(Gridpoint p)
        {
            var ret = TRC.RC<Gridpoint>.Failed(Gridpoint.Undefined, ErrorDescription: pnL.eNotCompleted());
            if(p.X - 1 >= 0)
            {
                ret = TRC.RC<Gridpoint>.Ok(new Gridpoint() { X = p.X - 1, Y = p.Y });
            }
            else
            {
                ret = TRC.RC<Gridpoint>.Failed(p, ErrorDescription:
                            pnL.m(TT.Grid2D.Left.UID,
                                pnL.p(TT.Grid2D.CursorCurrentlyAtGridpoint.UID, 
                                        pnL.List(
                                            pnL.p("X", p.X),
                                            pnL.p("Y", p.Y))),
                        pnL.InProgressActivityStatement(
                            pnL.DefObject(TT.Grid2D.Left.UID),
                            NH.pA(TT.Operators.Sets.IsOutOfRange.UID))));                        
            }

            return ret;

        }

        /// <summary>
        /// Gibt linken Rasterpunkt zurück
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public TRC.RC<Gridpoint> RigthOf(Gridpoint p)
        {
            var ret = TRC.RC<Gridpoint>.Failed(Gridpoint.Undefined, ErrorDescription: pnL.eNotCompleted());
            if (p.X + 1 <= maxX)
            {
                ret = TRC.RC<Gridpoint>.Ok(new Gridpoint() { X = p.X + 1, Y = p.Y });
            }
            else
            {
                ret = TRC.RC<Gridpoint>.Failed(p, ErrorDescription:
                            pnL.m(TT.Grid2D.Right.UID,
                                pnL.p(TT.Grid2D.CursorCurrentlyAtGridpoint.UID,
                                        pnL.List(
                                            pnL.p("X", p.X),
                                            pnL.p("Y", p.Y))),
                        pnL.InProgressActivityStatement(
                            pnL.DefObject(TT.Grid2D.Right.UID),
                            NH.pA(TT.Operators.Sets.IsOutOfRange.UID))));
            }

            return ret;
        }

        /// <summary>
        /// Gibt oberen Rasterpunkt zurück
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public TRC.RC<Gridpoint> UpperOf(Gridpoint p)
        {
            var ret = TRC.RC<Gridpoint>.Failed(Gridpoint.Undefined, ErrorDescription: pnL.eNotCompleted());
            if (p.Y + 1 <= maxY)
            {
                ret = TRC.RC<Gridpoint>.Ok(new Gridpoint() { X = p.X, Y = p.Y + 1 });
            }
            else
            {
                ret = TRC.RC<Gridpoint>.Failed(p, ErrorDescription:
                            pnL.m(TT.Grid2D.Right.UID,
                                pnL.p(TT.Grid2D.CursorCurrentlyAtGridpoint.UID,
                                        pnL.List(
                                            pnL.p("X", p.X),
                                            pnL.p("Y", p.Y))),
                        pnL.InProgressActivityStatement(
                            pnL.DefObject(TT.Grid2D.Upper.UID),
                            NH.pA(TT.Operators.Sets.IsOutOfRange.UID))));
            }

            return ret;
        }

        /// <summary>
        /// Gibt unteren Rasterpunkt zurück
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public TRC.RC<Gridpoint> LowerOf(Gridpoint p)
        {
            var ret = TRC.RC<Gridpoint>.Failed(Gridpoint.Undefined, ErrorDescription: pnL.eNotCompleted());
            if (p.Y - 1 >= 0)
            {
                ret = TRC.RC<Gridpoint>.Ok(new Gridpoint() { X = p.X, Y = p.Y - 1 });
            }
            else
            {
                ret = TRC.RC<Gridpoint>.Failed(p, ErrorDescription:
                            pnL.m(TT.Grid2D.Right.UID,
                                pnL.p(TT.Grid2D.CursorCurrentlyAtGridpoint.UID,
                                        pnL.List(
                                            pnL.p("X", p.X),
                                            pnL.p("Y", p.Y))),
                        pnL.InProgressActivityStatement(
                            pnL.DefObject(TT.Grid2D.Lower.UID),
                            NH.pA(TT.Operators.Sets.IsOutOfRange.UID))));
            }

            return ret;
        }
    }
}
