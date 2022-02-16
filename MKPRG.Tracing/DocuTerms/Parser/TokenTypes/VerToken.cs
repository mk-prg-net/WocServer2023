using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MKPRG.Tracing.DocuTerms.Parser
{
    /// <summary>
    /// mko, 10.8.2021
    /// </summary>
    public class VerToken
        : DocuTermToken,
        IVer,
        IInstanceMemberToken,
        IMethodParameterToken,
        IListMemberToken,
        IPropertyValueToken,
        IReturnValueToken
    {
        public VerToken(StringToken verString)
            : base(DocuEntityTypes.Version)
        {
            if (verString != null)
            {
                VersionString = verString.ValueAsString;
                CountOfEvaluatedTokens = verString.CountOfEvaluatedTokens + 1;
            }
        }

        public VerToken(string verString)
            : base(DocuEntityTypes.Version)
        {
            if (verString != null)
            {
                VersionString = verString;
                CountOfEvaluatedTokens = 1;
            }
        }

        public string VersionString { get; } = "0.0.0";

        public override int CountOfEvaluatedTokens { get; }

    }
}
