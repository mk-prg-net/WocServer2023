using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.Wocs
{
    /// mko, 25.3.2021
    /// Typ für Referenzen, die auf den übergeordneten Namespace verweisen
    /// </summary>
    public class _WocTypeNamespace
        : NamingBase
    {
        public const long UID = 0x997C663E;

        public _WocTypeNamespace()
            : base(UID, 1, UID, UID, UID)
        {
        }

        public override string CNT => DE;

        public override string CN => "命名空间";

        public override string DE => "Namensraum";

        public override string EN => "Namespace";

        public override string ES => "Espacio de nombres";

    }
}
