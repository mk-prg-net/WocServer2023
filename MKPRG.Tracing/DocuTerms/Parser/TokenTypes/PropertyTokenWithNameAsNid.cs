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
    public class PropertyTokenWithNameAsNid
        : PropertyToken,
        IPropertyWithNameAsNID
    {
        public INID DocuTermNid => DocuTermNidAsToken;
        public NIDToken DocuTermNidAsToken { get; } = NIDToken.UndefinedNID;

        public PropertyTokenWithNameAsNid(NIDToken nid, IPropertyValueToken propValue)
            : base(propValue)
        {
            if(nid != null)
                DocuTermNidAsToken = nid;
        }

        public override int CountOfEvaluatedTokens => base.CountOfEvaluatedTokens + DocuTermNidAsToken.CountOfEvaluatedTokens;

    }
}
