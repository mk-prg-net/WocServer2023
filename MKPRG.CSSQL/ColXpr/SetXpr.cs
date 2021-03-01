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
    /// mko, 19.6.2018
    /// Set- expression in Update statements
    /// </summary>
    public class SetXpr : ColXprBase, IColXpr
    {
        public SetXpr(IColXpr a, IColXpr b) : base(2)
        {
            Elements = new IColXpr[] { a, b };
        }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            var el = new EvaluatedElemsWrapper(EvaluatedElements);
            return NaLisp.Factories.Txt._.Create($" {el[0]} = {el[1]} ");
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new SetXpr((IColXpr)Elements[0], (IColXpr)Elements[1]);
        }
    }
}
