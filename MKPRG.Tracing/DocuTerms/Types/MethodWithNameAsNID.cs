using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    public class MethodWithNameAsNID
        : Method,
        IMethodWithNameAsNid
    {
        public MethodWithNameAsNID(INID nid)
        {
            if (nid != null)
                DocuTermNid = nid;
        }

        public MethodWithNameAsNID(INID nid, IMethodParameter[] methodParameter)
            : base(methodParameter)
        {
            if (nid != null)
                DocuTermNid = nid;
        }


        public INID DocuTermNid { get; } = NID.UndefinedNID;
    }
}
