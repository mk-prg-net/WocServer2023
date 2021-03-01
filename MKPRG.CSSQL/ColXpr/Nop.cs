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
    /// mko, 29.10.2018
    /// Implements No Operation.
    /// </summary>
    public class Nop : ColXprBase, IColXpr
    {
        public Nop() : base(1)
        {
            Elements = new IColXpr[] {new Constant("")};
        }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            var el = new EvaluatedElemsWrapper(EvaluatedElements);
            return NaLisp.Factories.Txt._.Create($"");
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new Nop();
        }
    }
}
