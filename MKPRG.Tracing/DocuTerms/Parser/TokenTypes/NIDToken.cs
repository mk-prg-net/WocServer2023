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
    /// mko, 22.7.2021
    /// </summary>
    public class NIDToken
        : DocuTermToken, 
        INID,
        IPropertyValueToken
    {
        public NIDToken(long nid)
            : base(DocuEntityTypes.NID)
        {
            NamingId = nid;
        }

        public override int CountOfEvaluatedTokens => 1;

        /// <summary>
        /// Die exakte Naming- ID
        /// </summary>
        public long NamingId { get; } = TTD.Types.UndefinedNID.UID;

        public static NIDToken UndefinedNID = new NIDToken(TTD.Types.UndefinedNID.UID);

    }
}
