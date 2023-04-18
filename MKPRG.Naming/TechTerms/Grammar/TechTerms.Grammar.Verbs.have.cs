using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Grammar.Verbs
{

    /// <summary>
    /// mko, 19.4.2021
    /// Haben im Sinne von besitzen
    /// </summary>
    public class Have
    : NamingBase, IInProgressActivity
    {
        public const long UID = 0xBACE0C4C;

        public Have()
            : base(UID)
        {
        }

        public override string CNT => "have";
        public override string CN => "有";
        public override string DE => "hat";
        public override string EN => "have a ";
        public override string ES => "tiene";
    }


    public class HaveNot
        : NamingBase, IInProgressActivity
    {
        public const long UID = 0x5190A723;

        public HaveNot()
            : base(UID)
        {
        }

        public override string CNT => "haveNot";
        public override string CN => "没有";
        public override string DE => "hat nicht";
        public override string EN => "have not ";
        public override string ES => "no tiene";
    }


    public class WillHave
    : NamingBase, IFutureActivity
    {
        public const long UID = 0x7FF037F9;

        public WillHave()
            : base(UID)
        {
        }

        public override string CNT => "willHave";
        public override string CN => "将有";
        public override string DE => "wird haben";
        public override string EN => "will have ";
        public override string ES => "tendrá";
    }


    public class WillNotHave
        : NamingBase, IFutureActivity
    {
        public const long UID = 0x558269A6;

        public WillNotHave()
            : base(UID)
        {
        }

        public override string CNT => "willNotHave";
        public override string CN => "将没有";
        public override string DE => "wird nicht haben";
        public override string EN => "will not have ";
        public override string ES => "no tendrá";
    }

}
