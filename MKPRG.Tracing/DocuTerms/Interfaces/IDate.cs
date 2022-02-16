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
    public interface IDate
        : IPropertyValue,
        INoSubTrees
        //IReturnValue,
        //IEventParameter
    {
        int Year { get; }

        int Month { get; }

        int Day { get; }
    }
}
