using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ANC = MKPRG.Naming;
using static MKPRG.Tracing.DocuTerms.DocuEntityHlp;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 27.11.2021
    /// </summary>
    public abstract class Event
        : DocuEntity,
        IEvent
    {
        public Event()
            : base(DocuEntityTypes.Event)
        {
        }

        public Event(IEventParameter eventParam)
            : base(DocuEntityTypes.Event)
        {
            if (eventParam != null)
            {
                if (eventParam is IKillEventParamIfNot k)
                {
                    if (k.Condition)
                    {
                        EventParameter = k.EventParameter;
                    }
                }
                else
                {
                    EventParameter = eventParam;
                }
            }
        }

        protected static InstanceWithNameAsNID _defaultValue = new InstanceWithNameAsNID(new NID(TTD.Types.UndefinedEventParameter.UID));

        public abstract DocuEntityHlp.EventTypes EventType { get; }

        public IEventParameter EventParameter { get; } = _defaultValue;

        public IEventParameter DocuTermDefaultValue => _defaultValue;

        public bool IsSetToDefaultValue => EventParameter is IDocuEntityWithNameAsNid dt && dt.DocuTermNid.NamingId == _defaultValue.DocuTermNid.NamingId;

    }
}
