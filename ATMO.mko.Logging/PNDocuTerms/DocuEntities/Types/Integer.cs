using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 8.6.2020
    /// 
    /// mko, 6.7.2020
    /// Primär werden jetzt long- Werte gespeichert. Bei Bedarf können diese als Long oder Integer ausgelesen werden.
    /// </summary>
    public class Integer
        : DocuEntity,
        IPropertyValue
        //IEventParameter,
        //IReturnValue
    {
        public Integer(long val, IFormater fmt)
            : base(fmt, DocuEntityTypes.Int)
        {
            ValueAsLong   = val;
        }

        public override int CountOfEvaluatedTokens => 1;

        /// <summary>
        /// Der ganzahliger Wert als int
        /// </summary>
        public int ValueAsInteger => (int)ValueAsLong;

        /// <summary>
        /// Der ganzzahlige Wert als Long
        /// </summary>
        public long ValueAsLong { get; }

        public override string ToString()
        {
            return ValueAsLong.ToString();
        }
    }
}
