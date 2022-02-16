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
    public class DTDateToken
        : DocuTermToken,
        IDate,
        IPropertyValueToken
    {
        public DTDateToken(int Year, int Month, int Day)
            : base(DocuEntityTypes.Date)
        {
            this.Year = Year;
            this.Month = Month;
            this.Day = Day;
        }

        public int Year { get; } = 1900;

        public int Month { get; } = 1;

        public int Day { get; } = 1;

        public override int CountOfEvaluatedTokens => 4;
    }
}
