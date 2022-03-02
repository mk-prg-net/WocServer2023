using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 23.11.2021
    /// 
    /// mko, 30.7.2021
    /// Mit INoSubTrees markiert
    /// </summary>
    public interface IEventWithNameAsNid
        : IEvent,
        IDocuEntityWithNameAsNid
=======
    /// mko, 27.11.2021
    /// </summary>
    public interface IDouble
        : IPropertyValue,
        INoSubTrees
>>>>>>> af2386c06b65720cd64b77f2cb9c314013c58424:MKPRG.Tracing/DocuTerms/Interfaces/IDouble.cs
    {
        double ValueAsDouble { get; }
    }
}
