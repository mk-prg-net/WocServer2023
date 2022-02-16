using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing.DocuTerms.Parser
{
    /// <summary>
    /// mko, 28.7.2021
    /// </summary>
    public class WildCardToken
        : DocuTermToken,
        IWildCard,
        IPropertyValueToken,
        IEventParameterToken,
        IReturnValueToken
    {
        public WildCardToken()
            : base(DocuEntityTypes.WildCard)
        {
            HasSubTreeConstraint = false;
        }

        public WildCardToken(IDocuEntityToken subTree)
            : base(DocuEntityTypes.WildCard)
        {
            HasSubTreeConstraint = true;

            if(subTree != null)
                SubTreeConstraintToken = subTree;
        }

        public bool HasSubTreeConstraint { get; }

        public IDocuEntity SubTreeConstraint => SubTreeConstraintToken;

        public IDocuEntityToken SubTreeConstraintToken { get; } = new NIDToken(TTD.Types.UndefinedPropertyValue.UID);

        public override int CountOfEvaluatedTokens => 1 + (HasSubTreeConstraint ? SubTreeConstraintToken.CountOfEvaluatedTokens : 1);
    }
}
