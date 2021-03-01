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
    /// mko, 19.6.2018
    /// New Value expression in insert statements
    /// </summary>
    public class NewValueXpr : ColXprBase, IColXpr
    {
        public NewValueXpr(IColXpr a, IColXpr b) : base(2)
        {
            Elements = new IColXpr[] { a, b };
        }

        public string ColName { get; private set; }
        public string ColVal { get; private set; }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            ColName = ((NaLisp.Data.ConstVal<string>)EvaluatedElements[0]).Value;
            ColVal = ((NaLisp.Data.ConstVal<string>)EvaluatedElements[1]).Value;
            return this;
        }

        public override Inspector.ProtocolEntry Validate(NaLispStack Stack, Inspector.ProtocolEntry[] ElemValidationResult)
        {
            return new Inspector.ProtocolEntry(this, ElemValidationResult.All(r => r.NaLispTreeNode is NaLisp.Data.ConstVal<string>), ElemValidationResult.All(r => r.IsTreeValid), typeof(NewValueXpr));            
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new NewValueXpr((IColXpr)Elements[0], (IColXpr)Elements[1]);
        }
    }
}
