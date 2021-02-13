using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public interface IKillInstanceMemberIfNot
        : IKillIfNot,
        IInstanceMember
    {
        IInstanceMember InstanceMembers { get; }
    }
}
