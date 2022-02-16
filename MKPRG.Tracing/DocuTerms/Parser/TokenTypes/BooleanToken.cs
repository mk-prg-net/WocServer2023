using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

namespace MKPRG.Tracing.DocuTerms.Parser
{
    /// <summary>
    /// mko, 23.7.2021
    /// </summary>
    public class BooleanToken
        : DocuTermToken,
        IBoolean,
        IPropertyValueToken
    {
        public BooleanToken(bool decision)
            : base(DocuEntityTypes.Bool)
        {
            ValueAsBool = decision;
        }

        public bool ValueAsBool { get; } = false;

        public override int CountOfEvaluatedTokens => 1;
    }
}
