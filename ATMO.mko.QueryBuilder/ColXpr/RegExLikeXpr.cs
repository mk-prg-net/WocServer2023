using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.NaLisp.Core;
using NaLisp = mko.NaLisp;

using Trc = mko.TraceHlp;

namespace ATMO.mko.QueryBuilder
{
    public class RegExLikeXpr : ColXprBase, IColXpr
    {
        public RegExLikeXpr(IColXpr col, IColXpr pattern) : base(2)
        {
            Elements = new IColXpr[] { col, pattern };
        }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            var el = new EvaluatedElemsWrapper(EvaluatedElements);
            return NaLisp.Factories.Txt._.Create($" REGEXP_LIKE({el[0]}, {el[1]}) ");
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new RegExLikeXpr((IColXpr)Elements[0], (IColXpr)Elements[1]);
        }
    }
}
