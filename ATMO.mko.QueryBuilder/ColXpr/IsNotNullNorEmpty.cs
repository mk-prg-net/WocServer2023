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
    /// </summary>
    public class IsNotNullNorEmptyXpr : ColXprBase, IColXpr
    {
        public IsNotNullNorEmptyXpr(IColXpr a) : base(1)
        {
            Elements = new INaLisp[] { a};
        }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            var el = new EvaluatedElemsWrapper(EvaluatedElements);
            return NaLisp.Factories.Txt._.Create($" ({el[0]} IS NOT NULL and  {el[0]} <> '') ");
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new IsNotNullNorEmptyXpr((IColXpr)Elements[0]);
        }
    }
}
