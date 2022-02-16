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
    public class DTListToken
        : DocuTermToken,
        IDTList,
        IListMemberToken,
        IPropertyValueToken,
        IEventParameterToken,
        IReturnValueToken
    {

        public DTListToken(IListMemberToken[] listMembers)
            : base(DocuEntityTypes.List)
        {
            ListMemberTokens = listMembers;
        }

        public IListMember[] ListMembers => ListMemberTokens;

        IListMemberToken[] ListMemberTokens { get; } = new IListMemberToken[] { };

        public override int CountOfEvaluatedTokens => ListMemberTokens.Sum(r => r.CountOfEvaluatedTokens);

    }
}
