using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 27.11.2021
    /// Liste einzubettender Instanz- Member
    /// </summary>
    public interface IInstanceMembersToEmbed
        : IInstanceMember
    {
        IEnumerable<IInstanceMember> InstanceMembersToEmbed { get; }
    }
}
