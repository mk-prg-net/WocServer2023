using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;


namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 16.6.2020
    /// 
    /// mko, 22.7.2021
    /// PropertyValue wird jetzt direkt in der Eigenschaft gespeichert und nicht mehr als 
    /// IDocuEntity in den Childs
    /// 
    /// mko, 9.8.2021
    /// In streng typisierter, regulärer Form reimplementiert
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
