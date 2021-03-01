using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.NaLisp.Core;
using NaLisp = mko.NaLisp;


namespace MKPRG.CSSQL
{
    /// <summary>
    /// mko, 24.1.2018
    /// </summary>
    public class BetweenXpr : ColXprBase, IColXpr
    {
        public BetweenXpr(IColXpr v, IColXpr b, IColXpr e) : base(3)
        {
            Elements = new INaLisp[] { v, b, e };
        }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            var el = new EvaluatedElemsWrapper(EvaluatedElements);
            return NaLisp.Factories.Txt._.Create($" {el[0]} BETWEEN {el[1]} AND {el[2]} ");
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new BetweenXpr((IColXpr)Elements[0], (IColXpr)Elements[1], (IColXpr)Elements[2]);
        }
    }
}
