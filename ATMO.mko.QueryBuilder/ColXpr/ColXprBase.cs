using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.NaLisp.Core;
using NaLisp = mko.NaLisp;

using Trc = mko.TraceHlp;

namespace ATMO.mko.QueryBuilder
{
    /// <summary>
    /// mko, 23.1.2018
    /// </summary>
    public abstract class ColXprBase : NaLisp.Core.NaLispNonTerminal, IColXpr
    {
        Evaluator _Evaluator = new Evaluator();
        Inspector _Inspector = new Inspector();

        public ColXprBase(int paramCount)
        {
            this.paramCount = paramCount;
        }

        public string Value
        {
            get
            {
                var pe = _Inspector.Validate(this);
                Trc.ThrowArgExIfNot(pe.IsCurrentValid && pe.IsTreeValid, $"{this.GetType().Name} is invalid");
                var res = _Evaluator.Eval(this);
                return ((NaLisp.Data.IConstValue<string>)res).Value;
            }
        }

        protected int paramCount;

        public override Inspector.ProtocolEntry Validate(NaLispStack Stack, Inspector.ProtocolEntry[] ElemValidationResult)
        {

            var cond1 = ElemValidationResult.Length == paramCount;
            var cond2 = ElemValidationResult.All(r => r.IsCurrentValid);

            return new Inspector.ProtocolEntry(
                this,
                cond1 && cond2,
                true,
                GetType());
        }               

        /// <summary>
        /// Simplifies access to evaluated elements in Eval- functions
        /// </summary>
        protected class EvaluatedElemsWrapper : INaLisp
        {
            NaLisp.Core.INaLisp[] Elems;

            public EvaluatedElemsWrapper(INaLisp[] Elems)
            {
                this.Elems = Elems;
                Length = Elems.Length;
            }

            public int Length
            {
                get;
            }

            public string Name => typeof(EvaluatedElemsWrapper).Name;

            public string this[int ix]
            {
                get
                {
                    return ((NaLisp.Data.IConstValue<string>)Elems[ix]).Value;
                }
            }

            public IEnumerable<string> Values
            {
                get
                {
                    for(int i = 0; i < Elems.Length; i++)
                    {
                        yield return ((NaLisp.Data.IConstValue<string>)Elems[i]).Value;
                    }
                }
            }
            


            public INaLisp Clone(bool deep = true)
            {
                throw new NotImplementedException();
            }
        }

    }
}
