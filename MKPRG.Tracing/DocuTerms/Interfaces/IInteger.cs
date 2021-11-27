using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
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
