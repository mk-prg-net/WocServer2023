using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 17.6.2020
    /// </summary>
    public interface IKillEventParamIfNot
        : IKillIfNot
    {
        IEventParameter EventParameter { get; }
    }
}
