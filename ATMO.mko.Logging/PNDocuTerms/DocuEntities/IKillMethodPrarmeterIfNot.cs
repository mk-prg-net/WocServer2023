using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public interface IKillMethodPrarmeterIfNot
        : IKillIfNot,
        IMethodParameter
    {
        IMethodParameter MethodParameters { get; }
    }
}
