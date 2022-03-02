using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
<<<<<<< HEAD:MKPRG.Tracing/DocuTerms/Interfaces/IEventWithNameAsString.cs
    /// mko, 8.9.2021
    /// </summary>
    public interface IEventWithNameAsString
        : IEvent,
        IDocuTermWithNameAsString
=======
    /// mko, 27.11.2021
    /// </summary>
    public interface IDocuTermWithNameAsString
>>>>>>> af2386c06b65720cd64b77f2cb9c314013c58424:MKPRG.Tracing/DocuTerms/Interfaces/IDocuTermWithAsString.cs
    {
        string DocuTermName { get; }
    }
}
