using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 16.6.2020
    /// 
    /// mko, 22.7.2021
    /// Daten direkt in den Eigenschaften Year, Month, Day gepseichert, und nicht mehr als 
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
