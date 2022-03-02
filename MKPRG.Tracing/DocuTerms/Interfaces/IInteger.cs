using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 23.7.2021
    /// 
    /// mko, 30.7.2021
    /// Mit INoSubTrees markiert
    /// </summary>
    public interface IInteger
            : IPropertyValue,
            INoSubTrees
    {
        /// <summary>
        /// Der ganzahliger Wert als int
        /// </summary>
        int ValueAsInteger { get; }

        /// <summary>
        /// Der ganzzahlige Wert als Long
        /// </summary>
        long ValueAsLong { get; }
    }
}
