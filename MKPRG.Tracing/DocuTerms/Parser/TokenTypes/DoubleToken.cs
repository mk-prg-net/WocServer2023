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
    public class DoubleToken
        : DocuTermToken,
        IDouble,
        IPropertyValueToken
    { 
    
        public DoubleToken(double value)
            : base(DocuEntityTypes.Int)
        {

        }

        public double ValueAsDouble { get; } = 0;

        public override int CountOfEvaluatedTokens => 1;
    }
}
