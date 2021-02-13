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
    public interface IVer
        : IInstanceMember,
        IMethodParameter,
        IListMember,
        IPropertyValue,
        IReturnValue
    {
        string VersionString { get; }
    }
}
