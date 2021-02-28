using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    public class NN
        : DocuEntity,
        IPropertyValue
    //IEventParameter,
    //IReturnValue
    {
        public NN(ulong val, IFormater fmt)
            : base(fmt, DocuEntityTypes.NN)
        {
            ValueAsULong = val;
        }

        public override int CountOfEvaluatedTokens => 1;

        /// <summary>
        /// Der ganzahliger Wert als int
        /// </summary>
        public int ValueAsInteger => (int)ValueAsULong;

        public ulong ValueAsULong { get;}

        public override string ToString()
        {
            return ValueAsULong.ToString();
        }
    }
}
