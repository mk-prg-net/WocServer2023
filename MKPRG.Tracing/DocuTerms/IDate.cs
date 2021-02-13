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
    public interface IDate
        : IPropertyValue
        //IReturnValue,
        //IEventParameter
    {
        int Year { get; }

        int Month { get; }

        int Day { get; }
    }
}
