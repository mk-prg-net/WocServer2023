using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    public interface IKillInstanceMemberIfNot
        : IKillIfNot,
        IInstanceMember
    {
        IInstanceMember InstanceMember { get; }
    }
}
