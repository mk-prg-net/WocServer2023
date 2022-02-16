using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;


namespace MKPRG.Tracing.DocuTerms.Parser
{
    /// <summary>
    /// mko, 23.7.2021
    /// Instanzen, die mit einem NID bezeichnet sind
    /// 
    /// mko, 9.8.2021
    /// NID gegen Nullwerte gehärtet
    /// </summary>
    public class InstanceTokenWithNameAsNid
        : InstanceToken,
        IInstanceWithNameAsNid
    {
        public INID DocuTermNid => DocuTermNidAsToken;
        public NIDToken DocuTermNidAsToken { get; } = NIDToken.UndefinedNID;

        public InstanceTokenWithNameAsNid(NIDToken nid)            
        {
            if(nid != null)
                DocuTermNidAsToken = nid;            
        }

        public InstanceTokenWithNameAsNid(NIDToken nid, IInstanceMemberToken[] ListEncapsulatedMembers)
            : base(ListEncapsulatedMembers)
        {
            if(nid != null)
                DocuTermNidAsToken = nid;
        }

        public override int CountOfEvaluatedTokens => base.CountOfEvaluatedTokens + DocuTermNidAsToken.CountOfEvaluatedTokens;

    }
}
