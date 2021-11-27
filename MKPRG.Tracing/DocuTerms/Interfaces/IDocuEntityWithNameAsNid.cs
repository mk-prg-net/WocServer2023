using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 23.2.2021
    /// Verpasst einem DocuTerm eine eindeutige Naming ID
    /// </summary>

    public interface IDocuEntityWithNameAsNid
    {
        INID DocuTermNid { get; }
    }
}
