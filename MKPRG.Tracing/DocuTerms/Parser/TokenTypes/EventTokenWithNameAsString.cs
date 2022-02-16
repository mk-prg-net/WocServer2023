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
    /// </summary>
    public class EventTokenWithNameAsString
        : EventToken,
        IEventWithNameAsString
    {

        public string DocuTermName => DocuTermNameAsToken.ValueAsString;
        public StringToken DocuTermNameAsToken { get; } = StringToken.NameIsNull;


        public EventTokenWithNameAsString(StringToken name)
        {
            if(name != null)
                DocuTermNameAsToken = name;
        }

        public EventTokenWithNameAsString(StringToken name, IEventParameterToken eventParameter)
            : base(eventParameter)
        {
            if(name != null)
                DocuTermNameAsToken = name;
        }

        public override DocuEntityHlp.EventTypes EventType => this.GetEventType();        
    }
}
