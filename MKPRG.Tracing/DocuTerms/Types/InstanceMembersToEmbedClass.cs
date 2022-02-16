using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{

    /// <summary>
    /// mko, 10.8.2021
    /// </summary>
    public class InstanceMembersToEmbedClass
        : DocuEntity,
        IInstanceMembersToEmbed
    {

        public InstanceMembersToEmbedClass(IEnumerable<IInstanceMember> ToEmbed)
            : base(DocuEntityTypes.ListToEmbed)
        {
            if(ToEmbed!= null)
            {
                InstanceMembersToEmbed = ToEmbed;
            }
        }

        public IEnumerable<IInstanceMember> InstanceMembersToEmbed { get; } = new IInstanceMember[] { };
    }
}
