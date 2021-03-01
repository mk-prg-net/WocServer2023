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
    public class OrXpr : ColXprBase, IColXpr
    {
        //public OrXpr(IColXpr a, IColXpr b) : base(2)
        //{
        //    Elements = new INaLisp[] { a, b };
        //}

        //public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        //{
        //    var el = new EvaluatedElemsWrapper(EvaluatedElements);
        //    return NaLisp.Factories.Txt._.Create($" ({el[0]} or {el[1]}) ");
        //}

        public OrXpr(params IColXpr[] a) : base(a.Length)
        {
            Elements = a.ToArray();
        }

        /// <summary>
        /// mko, 27.10.2018
        /// Kann jetzt auch mit leerer oder einstelliger Operandenliste umgehen. Hierdurch können And- Operationen zwischen 
        /// bedingten Operanden gebildet werden.
        /// </summary>
        /// <param name="EvaluatedElements"></param>
        /// <param name="StackInstance"></param>
        /// <param name="DebugOn"></param>
        /// <returns></returns>
        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            var els = new EvaluatedElemsWrapper(EvaluatedElements);

            var operands = els.Values.Where(op => !string.IsNullOrWhiteSpace(op));

            if (!operands.Any())
            {
                return NaLisp.Factories.Txt._.Create(" ");
            }
            else if (operands.Count() == 1)
            {
                return NaLisp.Factories.Txt._.Create(operands.First());
            }
            else
            {
                var bld = new StringBuilder();
                bld.Append($"({operands.First()}");
                foreach (var el in operands.Skip(1))
                {
                    bld.Append($" or {el}");
                }
                bld.Append(") ");

                return NaLisp.Factories.Txt._.Create(bld.ToString());

            }
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new OrXpr((IColXpr[])Elements);
        }
    }
}
