using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 27.7.2021
    /// </summary>
    public interface IReturnValueToken
        : IReturnValue,
        IToken
    {
    }
}
