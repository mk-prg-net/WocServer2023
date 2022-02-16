using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 6.8.2021
    /// </summary>
    public class InstanceWithNameAsNID
        : Instance,
        IInstanceWithNameAsNid
    {
        public InstanceWithNameAsNID(INID nid)
        {
            if(nid != null)
                DocuTermNid = nid;
        }

        public InstanceWithNameAsNID(INID nid, IInstanceMember[] instanceMembers)
            :base(instanceMembers)
        {
            if(nid != null)
                DocuTermNid = nid;
        }


        public INID DocuTermNid { get; } = NID.UndefinedNID;

        /// <summary>
        /// Standarwert für nicht initialisierte Member
        /// </summary>
        public static InstanceWithNameAsNID UndefinedDocuTerm = new InstanceWithNameAsNID(new NID(TTD.Types.UndefinedDocuTerm.UID));
    }
}
