using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 27.11.2021
    /// </summary>
    public class Integer
        : DocuEntity,
        IInteger
    {
        public Integer(long val)
            : base(DocuEntityTypes.Int)
        {
            ValueAsLong = val;
        }

        /// <summary>
        /// Der ganzahliger Wert als int
        /// </summary>
        public int ValueAsInteger => (int)ValueAsLong;

        /// <summary>
        /// Der ganzzahlige Wert als Long
        /// </summary>
        public long ValueAsLong { get; } = 0L;

    }
}
