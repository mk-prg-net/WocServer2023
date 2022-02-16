using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 15.6.2020
    /// DocuTerm, der einen Platzhalter für den Wert eines Eigenschaftsausdruckes darstellt.
    /// Wird beim Pattern- Matching berücksichtigt.
    /// 
    /// mko, 9.8.2021
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
        public WildCard()
            : base(DocuEntityTypes.WildCard)
        {
            HasSubTreeConstraint = false;
        }

        /// <summary>
        /// Erzeugt einen Wildcard mit der Einschränkung beim Pattern- Matching, das an seine 
        /// Stelle nur DokuTerms auftreten dürfen, die den im zweiten Parameter übergebenen
        /// SubTree enthalten.
        /// </summary>
        /// <param name="fmt"></param>
        /// <param name="subTree"></param>
        public WildCard(IDocuEntity subTree)
           : base(DocuEntityTypes.WildCard)
        {
            HasSubTreeConstraint = true;

            if(subTree != null)
                SubTreeConstraint = subTree;
        }

        public bool HasSubTreeConstraint { get; }

        public IDocuEntity SubTreeConstraint { get; } = new NID(TTD.Types.UndefinedPropertyValue.UID);
    }
}
