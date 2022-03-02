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
    public interface IInstance
        : IPropertyValue,
        IListMember,
        IEventParameter,
        IReturnValue,
        // mko, 26.5.2020
        // Hinzugefügt, um TTL- Termen zu genügen
        IInstanceMember,
        IMethodParameter
    {

        IInstanceMember[] InstanceMembers { get; }
    }
}
