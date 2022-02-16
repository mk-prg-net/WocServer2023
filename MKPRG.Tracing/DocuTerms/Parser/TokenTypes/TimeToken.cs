using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using rpn = mko.RPN;

namespace MKPRG.Tracing.DocuTerms.Parser
{
    /// <summary>
    /// mko, 11.8.2021
    /// </summary>
    public class TimeToken
        : DocuTermToken,
        ITime,
        IPropertyValueToken
    {
        public TimeToken(rpn.IntToken hh, rpn.IntToken mm, rpn.IntToken ss, rpn.IntToken ms)
            : base(DocuEntityTypes.Time)
        {
            Hour = hh.ValueAsInt;
            Minutes = mm.ValueAsInt;
            Seconds = ss.ValueAsInt;
            Milliseconds = ms.ValueAsInt;

            CountOfEvaluatedTokens = hh.CountOfEvaluatedTokens + mm.CountOfEvaluatedTokens + ss.CountOfEvaluatedTokens + ms.CountOfEvaluatedTokens;
        }

        public int Hour { get; } = 0;

        public int Minutes { get; } = 0;

        public int Seconds { get; } = 0;

        public int Milliseconds { get; } = 0;

        public override int CountOfEvaluatedTokens { get; } = 1;
    }
}
