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
    /// 19.6.2018
    /// Implements a Insert SQL Statement as NaLisp Expression
    /// </summary>
    class Insert : NaLisp.Core.NaLispNonTerminal
    {
        /// <summary>
        /// Generate a select clausel from a list of column expressions
        /// </summary>
        /// <param name="Elements"></param>
        public Insert(Table tab, NewValueXpr[] Elements)
        {
            this.tab = tab;
            base.Elements = Elements;
        }

        Table tab;

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            bool first = true;
            var bld = new StringBuilder($"INSERT INTO  {tab.TableName} (");
            foreach (NewValueXpr el in EvaluatedElements)
            {
                if (first)
                {
                    bld.Append($" {el.ColName} ");
                    first = false;
                }
                else
                {
                    bld.Append($", {el.ColName}");
                }
            }
            bld.Append(") VALUES ( ");

            first = true;
            foreach (NewValueXpr el in EvaluatedElements)
            {
                if (first)
                {
                    bld.Append($" {el.ColVal} ");
                    first = false;
                }
                else
                {
                    bld.Append($", {el.ColVal}");
                }
            }

            bld.Append(")");

            sql = bld.ToString();
            return NaLisp.Factories.Txt._.Create(sql);
        }


        string sql = "";

        public override Inspector.ProtocolEntry Validate(NaLispStack Stack, Inspector.ProtocolEntry[] ElemValidationResult)
        {
            return new Inspector.ProtocolEntry(
                this,
                ElemValidationResult.Any() && ElemValidationResult.All(r => r.NaLispTreeNode is NewValueXpr),
                ElemValidationResult.Any() && ElemValidationResult.All(r => r.IsTreeValid),
                typeof(NaLisp.Data.IConstValue<string>));
        }


        private Insert(INaLisp[] Elements)
        {
            this.Elements = Elements;
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new Insert(Elements);
        }

        public override string ToString()
        {
            return $"(INSERT  {sql})";
        }

    }
}
