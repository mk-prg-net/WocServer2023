using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 24.6.2024
    /// Markiert komplexe DocuTerme.
    /// Komplexe DocuTerme haben Member. Aktuell sind IList, IMethod und IInstance komplexe DocuTerme
    /// </summary>
    public interface IComplexType
        : IPropertyValue
    {
    }
}
