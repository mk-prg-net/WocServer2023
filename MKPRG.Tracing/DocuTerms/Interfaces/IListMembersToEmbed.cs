using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms.Interfaces
{
    /// <summary>
    /// mko, 27.11.2021
    /// </summary>
    public interface IListMembersToEmbed
        : IListMember
    {
        IEnumerable<IListMember> ListMembersToEmbed { get; }
    }
}
