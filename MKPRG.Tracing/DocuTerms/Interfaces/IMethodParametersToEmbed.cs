using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 10.8.2021
    /// Liste einzubettender Methodenparameter
    /// </summary>
    public interface IMethodParametersToEmbed
        : IMethodParameter
    {
        IEnumerable<IMethodParameter> MethodParametersToEmbed { get; }
    }
}
