using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.NaLisp.Core;
using NaLisp = mko.NaLisp;

using Trc = mko.TraceHlp;

namespace MKPRG.CSSQL
{
    /// <summary>
    /// mko, 24.1.2017
    /// </summary>
    public class NotStrEqXpr : ColXprBase, IColXpr
    {
        public NotStrEqXpr(IColXpr a, IColXpr b) : base(2)
        {
            Elements = new IColXpr[] { a, b };
        }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            var el = new EvaluatedElemsWrapper(EvaluatedElements);
            return NaLisp.Factories.Txt._.Create($" LOWER({el[0]}) != LOWER({el[1]}) ");
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new NotStrEqXpr((IColXpr)Elements[0], (IColXpr)Elements[1]);
        }
    }
}
