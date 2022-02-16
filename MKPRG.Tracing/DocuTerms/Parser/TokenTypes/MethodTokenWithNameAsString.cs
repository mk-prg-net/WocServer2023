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
    /// mko, 9.8.2021
    /// Name gegen Nullwerte gehärtet
    /// </summary>
    public class MethodTokenWithNameAsString
        : MethodToken,
        IMethodWithNameAsString
    {
        public string DocuTermName => DocuTermNameToken.ValueAsString;
        public StringToken DocuTermNameToken { get; } = StringToken.NameIsNull;

        public MethodTokenWithNameAsString(StringToken methodName)
        {
            if(methodName != null)
                DocuTermNameToken = methodName;
        }

        public MethodTokenWithNameAsString(StringToken methodName, IMethodParameterToken[] methodParameters)
            : base(methodParameters)
        {
            if(methodName != null)
                DocuTermNameToken = methodName;            
        }

        public override int CountOfEvaluatedTokens => base.CountOfEvaluatedTokens + DocuTermNameToken.CountOfEvaluatedTokens;
    }
}
