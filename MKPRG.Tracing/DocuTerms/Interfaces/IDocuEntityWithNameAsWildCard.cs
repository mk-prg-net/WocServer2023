using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// Vmko, 27.11.2021
    /// </summary>
    public interface IDocuEntityWithNameAsWildCard
    {
        IWildCard DocuTermNameAsWildCard { get; }
    }
}
