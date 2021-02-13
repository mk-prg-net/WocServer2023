using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 15.6.2020
    /// DocuTerm, der einen Platzhalter für den Wert eines Eigenschaftsausdruckes darstellt.
    /// Wird beim Pattern- Matching berücksichtigt.
    /// </summary>
    public class WildCard
                : DocuEntity,
                  IWildCard
    {
        /// <summary>
        /// Erzeugt einen Wildcard, welcher beim Pattern- Matching durch jeden beliebigen Dokuterm
        /// ersetzt werden kann.
        /// </summary>
        /// <param name="fmt"></param>
        public WildCard(IFormater fmt)
            : base(fmt, DocuEntityTypes.WildCard)
        {            
        }

        /// <summary>
        /// Erzeugt einen Wildcard mit der Einschränkung beim Pattern- Matching, das an seine 
        /// Stelle nur DokuTerms auftreten dürfen, die den im zweiten Parameter übergebenen
        /// SubTree enthalten.
        /// </summary>
        /// <param name="fmt"></param>
        /// <param name="subTree"></param>
        public WildCard(IFormater fmt, IDocuEntity subTree)
           : base(fmt, DocuEntityTypes.WildCard, subTree)
        {
        }


        public override int CountOfEvaluatedTokens => 1;
    }
}
