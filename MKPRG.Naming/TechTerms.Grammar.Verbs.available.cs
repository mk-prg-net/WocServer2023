using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Grammar.Verbs
{
    /// <summary>
    /// mko, 18.5.2021
    /// </summary>
    public class Available
        : NamingBase, IInProgressActivity
    {
        public const long UID = 0xBECA1F8B;

        public Available()
            : base(UID)
        {
        }

        public override string CNT => "available";
        public override string CN => "可用";
        public override string DE => "verfügbar";
        public override string EN => "available";
        public override string ES => "disponible";
    }

    public class NotAvailable
    : NamingBase, IInProgressActivity
    {
        public const long UID = 0x1E2B8293;

        public NotAvailable()
            : base(UID)
        {
        }

        public override string CNT => "notAvailable";
        public override string CN => "不提供";
        public override string DE => "nict verfügbar";
        public override string EN => "not available";
        public override string ES => "no disponible";
    }

    public class WillBeAvailable
    : NamingBase, IFutureActivity
    {
        public const long UID = 0x6BDFF7FF;

        public WillBeAvailable()
            : base(UID)
        {
        }

        public override string CNT => "willBeAvailable";
        public override string CN => "将可获得";
        public override string DE => "wird verfügbar sein";
        public override string EN => "will be available";
        public override string ES => "estará disponible";
    }

}
