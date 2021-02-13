using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 16.6.2020
    /// </summary>
    public class Property
        : DocuEntity,
        IProperty
    {
        public Property(IFormater fmt, NID nid, IPropertyValue propertyValue)
            : base(fmt, DocuEntityTypes.Property, nid, propertyValue)
        {
        }

        public Property(IFormater fmt, String name, IPropertyValue propertyValue)
            : base(fmt, DocuEntityTypes.Property, name, propertyValue)
        {
        }

        public IPropertyValue PropertyValue => (IPropertyValue)Childs.Skip(1).FirstOrDefault();
    }
}
