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
    public class DTTime
        : DocuEntity,
        ITime
    {
        public DTTime(int Hour, int Minutes, int Sec, int MilliSec)
            : base(DocuEntityTypes.Time)
        {
            this.Hour = Hour;
            this.Minutes = Minutes;
            this.Seconds = Sec;
            this.Milliseconds = Milliseconds;
        }

        public int Hour { get; } = 0;

        public int Minutes { get; } = 0;

        public int Seconds { get; } = 0;

        public int Milliseconds { get; } = 0;
    }
}
