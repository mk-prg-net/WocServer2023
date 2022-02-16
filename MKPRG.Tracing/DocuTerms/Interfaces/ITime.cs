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
    /// mko, 30.7.2021
    /// Mit INoSubTrees markiert
    /// </summary>
    public interface ITime
        : IPropertyValue, 
        INoSubTrees
        //IReturnValue,
        //IEventParameter
    {
        int Hour { get; }

        int Minutes { get; }

        int Seconds { get; }

        int Milliseconds { get; }
    }
}
