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
    /// Counts all distinct values in a column
    /// </summary>
    public class CountXpr : ColXprBase, IColXpr
    {
        public CountXpr(IColXpr a) : base(1)
        {
            Elements = new INaLisp[] { a};
        }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            return NaLisp.Factories.Txt._.Create($" COUNT({((NaLisp.Data.IConstValue<string>)EvaluatedElements[0]).Value})");
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new CountXpr((IColXpr)Elements[0]);
        }
    }
}
