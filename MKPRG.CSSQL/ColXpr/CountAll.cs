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
    public class CountAllXpr : ColXprBase, IColXpr
    {
        public CountAllXpr() : base(0)
        {
            Elements = new INaLisp[] {};
        }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            return NaLisp.Factories.Txt._.Create($" Count(*)");
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new CountAllXpr();
        }
    }
}
