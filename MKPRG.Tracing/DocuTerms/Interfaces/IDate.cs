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
    public interface IDate
        : IPropertyValue,
        INoSubTrees
    {
        int Year { get; }

        int Month { get; }

        int Day { get; }
    }
}
