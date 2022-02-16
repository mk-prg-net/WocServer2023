using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

namespace MKPRG.Tracing.DocuTerms.Parser
{
    public class InstanceToken
        : DocuTermToken,
        IInstance,
        IListMemberToken,
        IPropertyValueToken,
        IEventParameterToken,        
        IReturnValueToken,
        IInstanceMemberToken,
        IMethodParameterToken
    {
        public InstanceToken()
            : base(DocuEntityTypes.Instance)
        {
        }

        public InstanceToken(IInstanceMemberToken[] instanceMembers)
            : base(DocuEntityTypes.Instance)
        {
            if(instanceMembers != null)
                InstanceMembersAsTokens = instanceMembers;
        }

        public override int CountOfEvaluatedTokens => InstanceMembersAsTokens.Sum(r => r.CountOfEvaluatedTokens) + 1;


        /// <summary>
        /// Instanz- Member. 
        /// 
        /// mko, 12.7.2021
        /// Default ist anstatt null die leere Liste!
        /// </summary>
        public IInstanceMember[] InstanceMembers => InstanceMembersAsTokens;

        public IInstanceMemberToken[] InstanceMembersAsTokens { get; } = new IInstanceMemberToken[] { };

    }

}
