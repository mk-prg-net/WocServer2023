using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 27.11.2021
    /// Mit INoSubTrees markiert
    /// </summary>
    public interface ITime
        : IPropertyValue,
        INoSubTrees
    {
        int Hour { get; }

        int Minutes { get; }

        int Seconds { get; }

        int Milliseconds { get; }
    }
}
