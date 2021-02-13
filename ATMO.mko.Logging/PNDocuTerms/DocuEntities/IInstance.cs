using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 16.6.2020
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
