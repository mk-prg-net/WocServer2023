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
    /// 19.6.2018
    /// Implements a Update SQL Statement as NaLisp Expression
    /// </summary>
    public class Update : NaLisp.Core.NaLispNonTerminal
    {
        /// <summary>
        /// Generate a select clausel from a list of column expressions
        /// </summary>
        /// <param name="Elements"></param>
        public Update(Table tab, SetXpr[] Elements)
        {
            this.tab = tab;
            base.Elements = Elements;
        }

        Table tab;

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            bool first = true;
            var bld = new StringBuilder($"UPDATE {tab.TableName}");
            foreach (NaLisp.Data.IConstValue<string> el in EvaluatedElements)
            {
                if (first)
                {
                    bld.Append($" SET {el.Value} ");
                    first = false;
                }
                else
                {
                    bld.Append($", {el.Value}");
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
                ElemValidationResult.Any() && ElemValidationResult.All(r => r.NaLispTreeNode is SetXpr),
                ElemValidationResult.Any() && ElemValidationResult.All(r => r.IsTreeValid),
                typeof(NaLisp.Data.IConstValue<string>));
        }


        private Update(INaLisp[] Elements)
        {
            this.Elements = Elements;
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new Update(Elements);
        }

        public override string ToString()
        {
            return $"(UPDATE  {sql})";
        }

    }
}
