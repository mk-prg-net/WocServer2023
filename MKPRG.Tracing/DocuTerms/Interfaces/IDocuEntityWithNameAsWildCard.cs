using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms.Interfaces
{
    /// <summary>
    /// Vmko, 27.11.2021
    /// </summary>
    public interface IDocuEntityWithNameAsWildCard
    {
        INID DocuTermNid { get; }
    }
}
