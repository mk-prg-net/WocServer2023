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
        public Txt(IFormater fmt, params String[] words)
            : base(fmt, DocuEntityTypes.Text, words)
        { }

        public String[] Words => Childs.Select(w => (String)w).ToArray();
    }
}
