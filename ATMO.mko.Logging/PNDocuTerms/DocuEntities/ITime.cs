using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 16.6.2020
    /// </summary>
    public interface ITime
        : IPropertyValue
        //IReturnValue,
        //IEventParameter
    {
        int Hour { get; }

        int Minutes { get; }

        int Seconds { get; }

        int Milliseconds { get; }
    }
}
