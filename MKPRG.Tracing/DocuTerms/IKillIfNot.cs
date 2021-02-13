using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 16.6.2020
    /// </summary>
    public interface IKillIfNot
        : IListMember,
        IMethodParameter,
        IInstanceMember        
    {
        /// <summary>
        /// Bedingung, unter der die Löschung erfolgen soll
        /// </summary>
        bool Condition { get; }

        /// <summary>
        /// Listenelement, welches bedingt angelegt werden soll
        /// </summary>
        IListMember DocuEntity { get; }
    }
}
