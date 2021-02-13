using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    public class DTDate
        : DocuEntity,
        IDate
    {
        public DTDate(IFormater fmt, Integer Year, Integer month, Integer day)
            : base(fmt, DocuEntityTypes.Date, Year, month, day)
        { }

        public int Year => ((Integer)Childs.First()).ValueAsInteger;

        public int Month => ((Integer)Childs.Skip(1).First()).ValueAsInteger;

        public int Day => ((Integer)Childs.Skip(2).First()).ValueAsInteger;
    }
}
