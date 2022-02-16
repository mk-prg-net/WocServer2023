using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 8.9.2021
    /// </summary>
    public interface IEventWithNameAsNid
        : IEvent,
        IDocuEntityWithNameAsNid
    {
    }
}
