using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 27.11.2021
    /// Markiert eine Eigenschaft als Mehtodenparameter, Instanzmember und ListMember
    /// </summary>
    public interface IProperty
        : IDocuTermWithValue<IPropertyValue>,
        IMethodParameter,
        IInstanceMember,
        IListMember
    {
        IPropertyValue PropertyValue { get; }
    }
}
