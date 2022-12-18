using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Grammar.Verbs
{
    public class Is
    : NamingBase, IInProgressActivity
    {
        public const long UID = 0x5BF0B62A;

        public Is()
            : base(UID)
        {
        }

        public override string CNT => "is";
        public override string CN => "是";
        public override string DE => "ist";
        public override string EN => "is";
        public override string ES => "es";
    }

    public class Was
    : NamingBase, IFinishedActivity
    {
        public const long UID = 0xE4BA1948;

        public Was()
            : base(UID)
        {
        }

        public override string CNT => "was";
        public override string CN => "是";
        public override string DE => "war";
        public override string EN => "was";
        public override string ES => "fue";
    }

    public class IsA
        : NamingBase, IInProgressActivity
    {
        public const long UID = 0x4F2734D8;



        public IsA()
            : base(UID)
        {
        }

        public override string CNT => "isA";
        public override string CN => "是一个";
        public override string DE => "ist ein";
        public override string EN => "is a";
        public override string ES => "es un";
    }

    /// <summary>
    /// mko, 18.5.2021
    /// </summary>
    public class IsNotA
    : NamingBase, IInProgressActivity
    {
        public const long UID = 0x8DF23D2B;

        public IsNotA()
            : base(UID)
        {
        }

        public override string CNT => "isNotA";
        public override string CN => "不是一个";
        public override string DE => "ist nicht ein";
        public override string EN => "is not a";
        public override string ES => "no es un";
    }

    public class IsNot
    : NamingBase, IInProgressActivity
    {
        public const long UID = 0x9AE7739;

        public IsNot()
            : base(UID)
        {
        }

        public override string CNT => "isNot";
        public override string CN => "是没有";
        public override string DE => "ist kein";
        public override string EN => "is not";
        public override string ES => "no es";
    }



    public class WasA
    : NamingBase, IFinishedActivity
    {
        public const long UID = 0xC31E2CDD;

        public WasA()
            : base(UID)
        {
        }

        public override string CNT => "wasA";
        public override string CN => "是一个";
        public override string DE => "war ein";
        public override string EN => "was a";
        public override string ES => "fue un";
    }


    public class CanBe
        : NamingBase, IModalPhrase
    {
        public const long UID = 0xAC28BAB7;

        public CanBe()
            : base(UID)
        {
        }

        public override string CNT => "canBe";
        public override string CN => "可能是一个";
        public override string DE => "kann sein ein";
        public override string EN => "can be a";
        public override string ES => "puede ser";
    }

    public class CantBe
        : NamingBase, IModalPhrase
    {
        public const long UID = 0x3F6906E3;

        public CantBe()
            : base(UID)
        {
        }

        public override string CNT => "cantBe";
        public override string CN => "不能成为一个";
        public override string DE => "kann nicht sein ein";
        public override string EN => "cannot be a";
        public override string ES => "no puede ser un";
    }

}
