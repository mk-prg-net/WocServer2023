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
    /// mko, 22.1.2018
    /// Hauptterm einer NaLisp- SQL Funktion
    /// </summary>
    public class Select : NaLisp.Core.NaLispNonTerminal
    {
        /// <summary>
        /// Generate a select clausel from a list of column expressions
        /// </summary>
        /// <param name="Elements"></param>
        public Select(IColXpr[] Elements)
        {
            base.Elements = Elements;
        }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            bool first = true;
            var bld = new StringBuilder();
            foreach (NaLisp.Data.IConstValue<string> el in EvaluatedElements)
            {
                // mko, 28.11.2018
                // MapIf(false, ...) -> Nop- Spalten werden übersprungen
                if (!string.IsNullOrWhiteSpace(el.Value))
                {
                    if (first)
                    {
                        bld.Append($" {el.Value} ");
                        first = false;
                    }
                    else
                    {
                        bld.Append($", {el.Value}");
                    }
                }
            }

            sql = bld.ToString();
            return NaLisp.Factories.Txt._.Create(sql);
        }


        string sql = "";

        public override Inspector.ProtocolEntry Validate(NaLispStack Stack, Inspector.ProtocolEntry[] ElemValidationResult)
        {
            return new Inspector.ProtocolEntry(
                this,
                ElemValidationResult.Any() && ElemValidationResult.All(r => r.NaLispTreeNode is IColXpr),
                ElemValidationResult.Any() && ElemValidationResult.All(r => r.IsTreeValid),
                typeof(NaLisp.Data.IConstValue<string>));
        }


        private Select(INaLisp[] Elements)
        {
            this.Elements = Elements;
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new Select(Elements);
        }

        public override string ToString()
        {
            return $"(SELECT {sql})";
        }
    }
}
