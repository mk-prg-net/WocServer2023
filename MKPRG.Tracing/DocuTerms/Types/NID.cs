using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 8.6.2020
    /// DokuTerm, der eine NID (= Naming ID) speichert. Details siehe MKPRG.Naming
    /// </summary>
    public class NID
      : DocuEntity,
        IPropertyValue       
        //IEventParameter,
        //IReturnValue
    {
        public NID(IFormater fmt, long nid)
            : base(fmt, DocuEntityTypes.NID)
        {
            NamingId = nid;            
        }

        public override int CountOfEvaluatedTokens => 1;

        /// <summary>
        /// Die exakte Naming- ID
        /// </summary>
        public long NamingId { get; }


    }
}
