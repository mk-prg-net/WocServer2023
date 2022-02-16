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
    /// </summary>
    public class MethodToken
        : DocuTermToken,
        IMethod,
        IInstanceMemberToken,
        IListMemberToken,        
        IPropertyValueToken
    {

        public MethodToken()
            : base(DocuEntityTypes.Method)
        {}

        public MethodToken(IMethodParameterToken[] methodParameters)
            : base(DocuEntityTypes.Method)
        {
            if(methodParameters != null)
                ParametersAsTokens = methodParameters;
        }


        public IMethodParameter[] Parameters => ParametersAsTokens;

        public IMethodParameterToken[] ParametersAsTokens { get; } = new IMethodParameterToken[] { };

        public override int CountOfEvaluatedTokens => ParametersAsTokens.Sum(r => r.CountOfEvaluatedTokens);
    }
}
