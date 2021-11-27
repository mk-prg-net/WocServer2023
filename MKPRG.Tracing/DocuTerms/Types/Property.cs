using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 16.6.2020
    /// </summary>
    public class Property
    : DocuEntity,
    IProperty
    {
        public Property()
            : base(DocuEntityTypes.Property)
        {
        }

        public Property(IPropertyValue propertyValue)
            : base(DocuEntityTypes.Property)
        {
            if (propertyValue != null)
                PropertyValue = propertyValue;
        }


        protected static InstanceWithNameAsNID _defaultValue = new InstanceWithNameAsNID(new NID(TTD.Types.UndefinedPropertyValue.UID));

        public IPropertyValue PropertyValue { get; } = _defaultValue;

        public IPropertyValue DocuTermDefaultValue => _defaultValue;

        public bool IsSetToDefaultValue => PropertyValue is IDocuEntityWithNameAsNid nid && nid.DocuTermNid.NamingId == TTD.Types.UndefinedPropertyValue.UID;
    }

}
