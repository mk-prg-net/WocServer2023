using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Lifecycle
{
    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class Lifecycle
        : NamingBase
    {

        public const long UID = 0xC7DAB22E;

        public Lifecycle()
            : base(UID)
        {
        }

        public override string CNT => "lifecycle";
        public override string CN => EN;
        public override string DE => "Lebenszyklus";
        public override string EN => "Lifecycle";
        public override string ES => "Ciclo de vida";
    }

    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class Refresh
        : NamingBase
    {

        public const long UID = 0xEE8C1275;

        public Refresh()
            : base(UID)
        {
        }

        public override string CNT => "refresh";
        public override string CN => EN;
        public override string DE => "erneuern";
        public override string EN => "refresh";
        public override string ES => "renovar";
    }

    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class Aging
        : NamingBase
    {

        public const long UID = 0xA668EB35;

        public Aging()
            : base(UID)
        {
        }

        public override string CNT => "refresh";
        public override string CN => EN;
        public override string DE => "altern";
        public override string EN => "aging";
        public override string ES => "envejecer";
    }
}
