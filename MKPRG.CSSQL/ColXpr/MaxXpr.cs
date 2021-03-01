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
    /// mko, 2.5.2019
    /// Implementierung der Max- Spaltenfunktion
    /// </summary>
    public class MaxXpr : ColXprBase, IColXpr
    {
        public MaxXpr(IColXpr a) : base(1)
        {
            Elements = new INaLisp[] { a };
        }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            var el = new EvaluatedElemsWrapper(EvaluatedElements);
            return NaLisp.Factories.Txt._.Create($" MAX({el[0]}) ");
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new IsNotNullXpr((IColXpr)Elements[0]);
        }

    }
}
