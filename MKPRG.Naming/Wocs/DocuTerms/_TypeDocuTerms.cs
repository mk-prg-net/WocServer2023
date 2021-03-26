using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.Wocs.DocuTerms
{
    /// <summary>
    /// mko, 26.3.2021
    /// Menge der Schlüsselwörter der DocuTerms
    /// </summary>
    public class _TypeDocuTerms
            : NamingBase
    {
        public const long UID = 0x6F1C5F6E;

        public _TypeDocuTerms()
            : base(UID, 1, _WocRoot.UID, Authors.KorneffelMartin.UID, Nodes.DLL.MkprgNamingDll.UID,
                  new (long WocType, long Ref)[]
            {
                (_WocTypeNamespace.UID, _WocRoot.UID)
            })
        {
            
        }

        public override string CNT => "DocuTerms";

        public override string CN => CNT;

        public override string DE => CNT;

        public override string EN => CNT;

        public override string ES => CNT;

    }

}
