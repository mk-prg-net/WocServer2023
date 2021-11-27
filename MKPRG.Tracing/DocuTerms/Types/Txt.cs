using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 16.6.2020
    /// </summary>
    public class Txt
        : DocuEntity,
        ITxt
    {
        public Txt(params String[] words)
            : base(DocuEntityTypes.Text)
        {
            if (words != null)
            {
                Words = words;
            }
        }

        public IString[] Words { get; } = new String[] { };
    }
}
