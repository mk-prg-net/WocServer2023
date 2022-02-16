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
    public class MethodTokenWithNameAsNid
        : MethodToken,
        IMethodWithNameAsNid
    {
        public INID DocuTermNid => DocuTermNidToken;
        NIDToken DocuTermNidToken { get; } = NIDToken.UndefinedNID;

        public MethodTokenWithNameAsNid(NIDToken nid)
        {
            if(nid != null)
                DocuTermNidToken = nid;
        }

        public MethodTokenWithNameAsNid(NIDToken nid, IMethodParameterToken[] methodParameter)
            : base(methodParameter)
        {
            if(nid != null)
                DocuTermNidToken = nid;
        }

        public override int CountOfEvaluatedTokens => base.CountOfEvaluatedTokens + DocuTermNidToken.CountOfEvaluatedTokens;

    }
}
