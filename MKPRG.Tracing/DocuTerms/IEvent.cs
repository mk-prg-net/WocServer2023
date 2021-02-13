using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 16.6.2020
    /// Markiert ein DT- Event als Listmember, Methodenparameter und Instanz- Member
    /// </summary>
    public interface IEvent
        : IListMember,
        IMethodParameter,
        IInstanceMember,
        IReturnValue
    {
        /// <summary>
        /// mko, 23.6.2020
        /// Bildet Event- Namen auf Event- Typ ab.
        /// </summary>
        DocuEntityHlp.EventTypes EventType { get; }

        IEventParameter EventParameter { get; }
    }
}
