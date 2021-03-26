using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.Wocs.TechTerms
{
    /// <summary>
    /// mko, 2.3.2021
    /// Menge der technischen Fachausdrücke
    /// </summary>
    public class _TypeTechTerms
            : NamingBase
    {
        public const long UID = 0x7B4E0278;

        public _TypeTechTerms()
            : base(UID, _WocRoot.UID, Authors.KorneffelMartin.UID, Nodes.DLL.MkprgNamingDll.UID,
                  new (long WocType, long Ref)[]
            {
                (_WocTypeNamespace.UID, _WocRoot.UID)
            })
        {
            
        }

        public override string CNT => "TechTerms";

        public override string CN => CNT;

        public override string DE => CNT;

        public override string EN => CNT;

        public override string ES => CNT;

    }

}
