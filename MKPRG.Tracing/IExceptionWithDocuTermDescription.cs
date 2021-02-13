using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKPRG.Tracing.DocuTerms;

namespace MKPRG.Tracing
{
    /// <summary>
    /// mko, 26.5.2020
    /// Schnittstelle zu Ausnahmeobjekten, welche den Fehler mittels DocuTerms beschreiben.
    /// </summary>
    public interface IExceptionWithDocuTermDescription
    {
        /// <summary>
        /// Beschreibung der Fehlerursache durch einen DocuTerm
        /// </summary>
        IDocuEntity MessageAsDocuTerm { get; }
    }
}
