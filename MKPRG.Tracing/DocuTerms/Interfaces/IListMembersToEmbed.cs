using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 18.6.2020
    /// 
    /// mko, 10.8.2021
    /// Umbenannt von `IListToEmbed` in `IListMembersToEmbed`
    /// Zudem wird nur noch von IListMember geerbt, und nicht mehr von 
    /// IMethodParameter und IInstanceMember. Damit strenger typisiert
    /// </summary>
    public interface IListMembersToEmbed
        :IListMember
    {
        IEnumerable<IListMember> ListMembersToEmbed { get; }
    }
}
