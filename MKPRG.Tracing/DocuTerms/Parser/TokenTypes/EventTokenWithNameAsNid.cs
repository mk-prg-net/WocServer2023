using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing.DocuTerms.Parser
{
    /// <summary>
    /// mko, 23.7.2021
    /// 
    /// mko, 9.8.2021
    /// `DocuTermNid` gegen Nullwerte gefestigt.
    /// </summary>
    public class EventTokenWithNameAsNid
        : EventToken,
        IEventWithNameAsNid
    {
        public INID DocuTermNid => DocuTermNidAsToken;
        public NIDToken DocuTermNidAsToken { get; } = NIDToken.UndefinedNID;

        public EventTokenWithNameAsNid(NIDToken nid)                
        {
            if(nid != null)
                DocuTermNidAsToken = nid;
        }

        public EventTokenWithNameAsNid(NIDToken nid, IEventParameterToken eventParam)
            : base(eventParam)
        {
            if(nid != null)
                DocuTermNidAsToken = nid;
        }

        public override DocuEntityHlp.EventTypes EventType => DocuEntityHlp.EventNidToEventType(DocuTermNid.NamingId);

    }
}
