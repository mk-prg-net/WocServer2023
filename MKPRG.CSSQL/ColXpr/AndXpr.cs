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
    /// 
    /// mko, 2.8.2018
    /// Replaced constructor with two parameters by constructor with variadic parameterlist
    /// </summary>
    public class AndXpr : ColXprBase, IColXpr
    {
        //public AndXpr(IColXpr a, IColXpr b): base(2)
        //{            
        //    Elements = new INaLisp[] { a, b };
        //}

        public AndXpr(params IColXpr[] a) : base(a.Length)
        {
            Elements = a.ToArray();
        }

        /// <summary>
        /// mko, 2.8.2018
        /// </summary>
        /// <param name="Stack"></param>
        /// <param name="ElemValidationResult"></param>
        /// <returns></returns>
        public override Inspector.ProtocolEntry Validate(NaLispStack Stack, Inspector.ProtocolEntry[] ElemValidationResult)
        {
            //if (ElemValidationResult.Length < 2)
            //{
            //    return new Inspector.ProtocolEntry(this, false, ElemValidationResult.All(r => r.IsTreeValid), typeof(AndXpr));
            //}
            //else
            //{
                return base.Validate(Stack, ElemValidationResult);
            //}
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
                    bld.Append($" and {el}");
                }
                bld.Append(") ");

                return NaLisp.Factories.Txt._.Create(bld.ToString());

            }
        }

        //protected override INaLisp Create(INaLisp[] Elements)
        //{
        //    return new AndXpr((IColXpr)Elements[0], (IColXpr)Elements[1]);
        //}

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new AndXpr((IColXpr[])Elements);
        }
    }
}
