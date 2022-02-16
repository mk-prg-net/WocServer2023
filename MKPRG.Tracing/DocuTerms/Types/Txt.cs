using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 16.6.2020
    /// 
    /// mko, 9.8.2021
    /// 
    /// mko, 27.9.2021
    /// Behandlung des Falles `params String[] words == null` implementiert!
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
