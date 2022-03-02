using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using mko.RPN;


namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 6.8.2021
    /// </summary>
    public class EventWithNameAsString
    : Event,
    IEventWithNameAsString
    {

        public EventWithNameAsString(string name)
        {
            if(name != null)
                DocuTermName = name;
        }

        public EventWithNameAsString(string name, IEventParameter eventParameter)
            : base(eventParameter)
        {
            if(name != null)
                DocuTermName = name;
        }

        public string DocuTermName { get; } = String.NameIsNull.ValueAsString;

        public override DocuEntityHlp.EventTypes EventType => this.GetEventType();

    }
}
