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
    /// mko, 24.1.2018
    /// </summary>
    public class GtXpr : ColXprBase, IColXpr
    {
        public GtXpr(IColXpr a, IColXpr b) : base(2)
        {
            Elements = new IColXpr[] { a, b };
        }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            var el = new EvaluatedElemsWrapper(EvaluatedElements);
            return NaLisp.Factories.Txt._.Create($" {el[0]} > {el[1]} ");
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new GtXpr((IColXpr)Elements[0], (IColXpr)Elements[1]);
        }
    }
}
