using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    public class PropertyWithNameAsString
        : Property,
        IPropertyWithNameAsString
    {
        public PropertyWithNameAsString(string name)
        {
            if (name != null)
                DocuTermName = name;
        }

        public PropertyWithNameAsString(string name, IPropertyValue propValue)
            : base(propValue)
        {
            if (name != null)
                DocuTermName = name;
        }

        public string DocuTermName { get; } = String.NameIsNull.ValueAsString;
    }
}
