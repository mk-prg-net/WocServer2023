using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 16.6.2020
    /// Allgemeine Listen aus Eigenschaften, Instanzen und Sublisten
    /// </summary>
    public class DTList
        : DocuEntity,
        IDTList
    {
        public DTList(IFormater fmt)
            : base(fmt, DocuEntityTypes.List) { }

        public DTList(IFormater fmt, params IListMember[] listMember)
            : base(fmt, DocuEntityTypes.List, listMember)
        {
        }

        public IListMember[] ListMembers => Childs?.Any() ?? false ? Childs.Select(c => (IListMember)c).ToArray() : new IListMember[] { };
    }
}
