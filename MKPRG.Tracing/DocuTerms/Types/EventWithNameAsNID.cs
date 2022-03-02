using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 6.8.2021
    /// </summary>
    public class EventWithNameAsNID
        : Event,
        IEventWithNameAsNid
    {
        public EventWithNameAsNID(INID nid)
        {
            if (nid != null)
                DocuTermNid = nid;
        }

        public EventWithNameAsNID(INID nid, IEventParameter eventParam)
            : base(eventParam)
        {
            if (nid != null)
                DocuTermNid = nid;
        }

        public INID DocuTermNid { get; } = NID.UndefinedNID;

        public override DocuEntityHlp.EventTypes EventType => DocuEntityHlp.EventNidToEventType(DocuTermNid.NamingId);
    }
}
