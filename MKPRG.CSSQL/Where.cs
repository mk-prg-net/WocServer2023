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
    /// mko, 22.01.2018
    /// </summary>
    public class Where : NaLisp.Core.NaLispNonTerminal
    {
        public Where(IColXpr columnExpression)
        {
            Elements = new IColXpr[] { columnExpression };
        }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            var bld = new StringBuilder();

            foreach (NaLisp.Data.IConstValue<string> el in EvaluatedElements)
            {
                bld.Append($" {el.Value} ");
            }
            sql = bld.ToString();
            return NaLisp.Factories.Txt._.Create(sql);

        }

        string sql = "";

        public override Inspector.ProtocolEntry Validate(NaLispStack Stack, Inspector.ProtocolEntry[] ElemValidationResult)
        {
            return new Inspector.ProtocolEntry(
                this,
                !ElemValidationResult.Any() || ElemValidationResult.All(r => r.NaLispTreeNode is IColXpr && r.IsCurrentValid),
                !ElemValidationResult.Any() || ElemValidationResult.All(r => r.IsTreeValid),
                typeof(Where));
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            Trc.ThrowArgExIfNot(Elements.Length == 1, "Only on root col- expression in where clause allowed");
            return new Where((IColXpr)Elements[0]);
        }

        public override string ToString()
        {
            // mko, 27.11.2018
            // where Klausel wird nur im Falle eines nichtleeren sql- Ausdrucks erzeugt.
            if (string.IsNullOrWhiteSpace(sql))
            {
                return " ";
            }
            else
            {
                return $"(where {sql})";
            }
        }

    }
}
