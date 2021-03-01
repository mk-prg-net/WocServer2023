using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.NaLisp.Core;
using NaLisp = mko.NaLisp;

namespace ATMO.mko.QueryBuilder
{
    /// <summary>
    /// mko, 24.1.2018
    /// Calculates the average over all column values in oracle server
    /// </summary>
    public class AvgXpr : ColXprBase, IColXpr
    {
        public AvgXpr(IColXpr a) : base(1)
        {
            Elements = new INaLisp[] { a};
        }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            var el = new EvaluatedElemsWrapper(EvaluatedElements);
            return NaLisp.Factories.Txt._.Create($" AVG({el[0]})");
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new AvgXpr((IColXpr)Elements[0]);
        }
    }
}
