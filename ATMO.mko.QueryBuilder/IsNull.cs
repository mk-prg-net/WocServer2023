using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.NaLisp.Core;
using NaLisp = mko.NaLisp;

namespace ATMO.mko.QueryBuilder.ColXpr
{
    /// <summary>
    /// mko, 24.1.2018
    /// </summary>
    public class IsNullXpr : ColXprBase, IColXpr
    {
        public IsNullXpr(IColXpr a) : base(1)
        {
            Elements = new INaLisp[] { a};
        }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            return NaLisp.Factories.Txt._.Create($" {((NaLisp.Data.IConstValue<string>)EvaluatedElements[0]).Value} IS NULL ");
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new IsNullXpr((IColXpr)Elements[0]);
        }
    }
}
