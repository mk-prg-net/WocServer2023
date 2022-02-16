using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

namespace MKPRG.Tracing.DocuTerms.Parser
{
    public class IntegerToken
        : DocuTermToken,
        IInteger,
        IPropertyValueToken
    {
        public IntegerToken(int Value)
            : base(DocuEntityTypes.Int)
        {
            ValueAsLong = Value;
        }

        public IntegerToken(long Value)
            : base(DocuEntityTypes.Int)
        {
            ValueAsLong = Value;
        }

        public int ValueAsInteger => (int)ValueAsLong;

        public long ValueAsLong { get; } = 0L;

        public override int CountOfEvaluatedTokens => 1;
    }
}
