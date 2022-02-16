using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing.DocuTerms.Parser
{
    /// <summary>
    /// mko, 23.7.2021
    /// 
    /// mko, 27.7.2021
    /// NID von TT.Sets.NullValue gegen TTD.Types.UndefinedEventParameter ersetzt.
    /// </summary>
    public abstract class EventToken
        : DocuTermToken,
        IEvent,
        IMethodParameterToken,
        IInstanceMemberToken,
        IReturnValueToken,
        IListMemberToken
    {
        public EventToken()
            : base(DocuEntityTypes.Event)
        { }


        public EventToken(IEventParameterToken eventParameterToken)
            : base(DocuEntityTypes.Event)
        {
            if (eventParameterToken != null)
            {                
                EventParameterToken = eventParameterToken;
            }
        }

        /// <summary>
        /// mko, 6.8.2021
        /// Optimierte Implementierung des default- Wertes für Events
        /// </summary>
        protected static InstanceTokenWithNameAsNid _defaultValue = new InstanceTokenWithNameAsNid(new NIDToken(TTD.Types.UndefinedEventParameter.UID));

        public IEventParameter DocuTermDefaultValue => _defaultValue;

        public abstract DocuEntityHlp.EventTypes EventType { get; }              

        public IEventParameter EventParameter => EventParameterToken;        

        public IEventParameterToken EventParameterToken { get; } = new InstanceTokenWithNameAsNid(new NIDToken(TTD.Types.UndefinedEventParameter.UID));

        public override int CountOfEvaluatedTokens => EventParameterToken.CountOfEvaluatedTokens + 1;

        public bool IsSetToDefaultValue => EventParameter is IDocuEntityWithNameAsNid dt && dt.DocuTermNid.NamingId == _defaultValue.DocuTermNid.NamingId;

        
    }
}
