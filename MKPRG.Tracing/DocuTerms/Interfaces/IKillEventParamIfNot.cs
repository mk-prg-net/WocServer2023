using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 17.6.2020
    /// 
    /// mko, 12.7.2021
    /// Strenger typisiert.
    /// </summary>
    public interface IKillEventParamIfNot
        : IKillIfNot,
        IEventParameter
    {
        IEventParameter EventParameter { get; }
    }
}
