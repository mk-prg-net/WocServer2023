using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ANC = MKPRG.Naming;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 16.6.2020
    /// 
    /// mko, 12.7.2021
    /// Eventparameter werden jetzt direkt in der Event- Klasse gepspeichert, und nicht mehr in 
    /// den Childs von der DocuEntity- Basis
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
                if(eventParam is IKillEventParamIfNot k)
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
