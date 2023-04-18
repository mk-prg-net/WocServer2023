using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Grammar.Verbs
{
    public class Expect
        : NamingBase, Grammar.IInProgressActivity
    {

        public const long UID = 0x7B7145AD;

        public Expect()
            : base(UID)
        {
        }

        public override string CNT => "expect";
        public override string CN => "预期";
        public override string DE => "erwarte";
        public override string EN => "expect";
        public override string ES => "esperado";

    }

    public class WasExpected
        : NamingBase, Grammar.IFinishedActivity
    {

        public const long UID = 0xB22F2267;

        public WasExpected()
            : base(UID)
        {
        }

        public override string CNT => "wasExpected";
        public override string CN => "预计";
        public override string DE => "wurde erwartet";
        public override string EN => "was expected";
        public override string ES => "se esperaba";
    }

    public class DoNotExpect
    : NamingBase, Grammar.IInProgressActivity
    {

        public const long UID = 0xBE2511D9;

        public DoNotExpect()
            : base(UID)
        {
        }

        public override string CNT => "notExpect";
        public override string CN => "别指望";
        public override string DE => "tue nicht erwarten";
        public override string EN => "do not expect";
        public override string ES => "no esperes";

    }

    public class NotExpected
        : NamingBase, Grammar.IFinishedActivity
    {

        public const long UID = 0xFD365833;

        public NotExpected()
            : base(UID)
        {
        }

        public override string CNT => "wasNotExpected";
        public override string CN => "没想到";
        public override string DE => "wurde nicht erwartet";
        public override string EN => "was not expected";
        public override string ES => "no se esperaba";
    }

    public class WillBeExpected
    : NamingBase, Grammar.IFutureActivity
    {

        public const long UID = 0x5A651946;

        public WillBeExpected()
            : base(UID)
        {
        }

        public override string CNT => "willBeExpected";
        public override string CN => "可望有";
        public override string DE => "wird erwartet werden";
        public override string EN => "will be expected";
        public override string ES => "se espera";
    }

    public class WillNotBeExpected
        : NamingBase, Grammar.IFinishedActivity
    {

        public const long UID = 0xC216A0B3;

        public WillNotBeExpected()
            : base(UID)
        {
        }

        public override string CNT => "willNotBeExpected";
        public override string CN => "预计不会有";
        public override string DE => "wird nicht erwartet werden";
        public override string EN => "will not be expected";
        public override string ES => "no se espera";
    }

}
