using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 16.6.2020
    /// </summary>
    public class Instance
        : DocuEntity,
        IInstance
    {
        public Instance(IFormater fmt, NID nid, IDTList ListEncapsulatedMembers)
            : base(fmt, DocuEntityTypes.Instance, nid, ListEncapsulatedMembers)
        { }

        public Instance(IFormater fmt, String name, IDTList ListEncapsulatedMembers)
            : base(fmt, DocuEntityTypes.Instance, name, ListEncapsulatedMembers)
        { }

        public IInstanceMember[] InstanceMembers 
            => ((IDTList)Childs.Skip(1).First()).ListMembers.Select(m => (IInstanceMember)m).ToArray();
    }
}
