using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.Wocs
{
    /// mko, 25.3.2021
    /// Wurzel aller Wocs
    /// </summary>
    public class _WocRoot
        : NamingBase
    {
        public const long UID = 0x57C7C853;

        public _WocRoot()
            : base(UID, UID, UID, UID)
        {
        }

        public override string CNT => "Wocs";

        public override string CN => "WOC根";

        public override string DE => "WOC Wurzel";

        public override string EN => "WOC root";

        public override string ES => "Raíz del WOC";

    }
}
