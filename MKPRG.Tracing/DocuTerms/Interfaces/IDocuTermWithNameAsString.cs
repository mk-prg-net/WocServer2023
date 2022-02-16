using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 23.7.2021
    /// Verpasst einem DocuTerm einen Namen als String
    /// </summary>
    public interface IDocuTermWithNameAsString
    {
        string DocuTermName { get; }
    }
}
