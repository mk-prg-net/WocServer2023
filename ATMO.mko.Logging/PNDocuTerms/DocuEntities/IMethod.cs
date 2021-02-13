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
    public interface IMethod
        : IInstanceMember,
        IListMember,     
        IPropertyValue
    {
        /// <summary>
        /// Listet alle Parameter einer Mehtode auf
        /// </summary>
        IMethodParameter[] Parameters { get; }
    }
}
