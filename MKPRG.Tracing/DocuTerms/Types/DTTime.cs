using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 16.6.2020
    /// </summary>
    public class DTTime
        : DocuEntity,
        ITime
    {
        public DTTime(IFormater fmt, Integer Hour, Integer Minute, Integer Sec, Integer MilliSec)
            : base(fmt, DocuEntityTypes.Time, Hour, Minute, Sec, MilliSec)
        { }

        public int Hour => ((Integer)Childs.First()).ValueAsInteger;

        public int Minutes => ((Integer)Childs.Skip(1).First()).ValueAsInteger;

        public int Seconds => ((Integer)Childs.Skip(2).First()).ValueAsInteger;

        public int Milliseconds => ((Integer)Childs.Skip(3)?.FirstOrDefault())?.ValueAsInteger ?? 0;
    }
}
