using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    public class PropertyWithNameAsNID
        : Property,
        IPropertyWithNameAsNID
    {
        public PropertyWithNameAsNID(INID nid)
        {
            if (nid != null)
                DocuTermNid = nid;
        }

        public PropertyWithNameAsNID(INID nid, IPropertyValue propertyValue)
            : base(propertyValue)
        {
            if (nid != null)
                DocuTermNid = nid;
        }

        public INID DocuTermNid { get; } = NID.UndefinedNID;
    }
}
