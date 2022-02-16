using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 8.6.2020
    /// 
    /// mko, 6.7.2020
    /// Primär werden jetzt long- Werte gespeichert. Bei Bedarf können diese als Long oder Integer ausgelesen werden.
    /// 
    /// mko, 9.8.2021
    /// Implementierung vereinfacht
    /// </summary>
    public class Integer
        : DocuEntity,
        IInteger
    {
        public Integer(long val)
            : base(DocuEntityTypes.Int)
        {
            ValueAsLong   = val;
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
