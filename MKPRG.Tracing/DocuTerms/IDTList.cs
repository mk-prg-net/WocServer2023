using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 16.6.2020
    /// Liste
    /// </summary>
    public interface IDTList
        : IListMember,
        IPropertyValue,
        IEventParameter
    {
        IListMember[] ListMembers { get; }
    }
}
