using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 27.11.2021
    /// </summary>
    public class DTDate
        : DocuEntity,
        IDate
    {
        public DTDate(int Year, int Month, int Day)
            : base(DocuEntityTypes.Date)
        {
            this.Year = Year;
            this.Month = Month;
            this.Day = Day;
        }

        public int Year { get; } = 1900;

        public int Month { get; } = 1;

        public int Day { get; } = 1;
    }
}
