using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.ATMO.DFC.Tree
{

    /// <summary>
    /// mko, 5.10.2020
    /// Name des allgemeinen DFC- Tree Traversierers, der die ATMO- Geschäftslogik für den Zugriff berücksichtigt
    /// </summary>
    public class DfcTreeVisitor
     : NamingBase
    {

        public const long UID = 0xCA3B1DE3;

        public DfcTreeVisitor()
            : base(UID)
        {
        }


        public override string CNT => "dfcTreeVisitor";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }
}
