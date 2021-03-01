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
    /// Implements the oracle sql in Expression
    /// </summary>
    public class InXpr : ColXprBase, IColXpr
    {
        public InXpr(IColXpr v, params IColXpr[] xmps) : base(xmps.Length + 1)
        {
            List<IColXpr> els = new List<IColXpr>(xmps.Length + 1);
            els.Add(v);
            els.AddRange(xmps);
            Elements = els.ToArray();
        }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            var el = new EvaluatedElemsWrapper(EvaluatedElements);

            var bld = new StringBuilder();
            bld.Append($" {el[0]} IN (");

            for(int i = 1; i < el.Length -1; i++)
            {
                bld.Append($"{el[i]}, ");
            }

            bld.Append($"{el[el.Length - 1]})");
            var inClausel = bld.ToString();

            return NaLisp.Factories.Txt._.Create(bld.ToString());
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new InXpr((IColXpr)Elements[0],  Elements.Select(r => (IColXpr)r).ToArray());
        }
    }
}
