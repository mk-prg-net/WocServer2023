using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 16.6.2020
    /// Markiert eine Eigenschaft als Mehtodenparameter, Instanzmember und ListMember
    /// </summary>
    public interface IProperty
        : IMethodParameter,
        IInstanceMember,
        IListMember
    {
        IPropertyValue PropertyValue { get; }
    }
}
