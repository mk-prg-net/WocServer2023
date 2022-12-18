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
    public class Provide
    : NamingBase, IInProgressActivity
    {
        public const long UID = 0x4A37FC42;

        public Provide()
            : base(UID)
        {
        }

        public override string CNT => "provide";
        public override string CN => "提供";
        public override string DE => "bereitstellen";
        public override string EN => "provide";
        public override string ES => "proporcionar";
    }



    public class Provides
        : NamingBase, IInProgressActivity
    {
        public const long UID = 0x9EC51AC;

        public Provides()
            : base(UID)
        {
        }

        public override string CNT => "provides";
        public override string CN => "提供";
        public override string DE => "stellt bereit";
        public override string EN => "provides";
        public override string ES => "proporciona";
    }


    public class CanProvide
        : NamingBase, IModalPhrase
    {
        public const long UID = 0x9AD9AB3A;

        public CanProvide()
            : base(UID)
        {
        }

        public override string CNT => "canProvide";
        public override string CN => "能够提供";
        public override string DE => "kann bereitstellen";
        public override string EN => "can provide";
        public override string ES => "puede proporcionar";
    }

    public class CantProvide
    : NamingBase, IModalPhrase
    {
        public const long UID = 0xEC1824F4;

        public CantProvide()
            : base(UID)
        {
        }

        public override string CNT => "cantProvide";
        public override string CN => "不能为其提供";
        public override string DE => "kann nicht bereitstellen";
        public override string EN => "can not provide";
        public override string ES => "puede proporcionar";
    }

    public class WillBeProvided
        : NamingBase, IFutureActivity
    {
        public const long UID = 0x8AE64927;

        public WillBeProvided()
            : base(UID)
        {
        }

        public override string CNT => "willBeProvided";
        public override string CN => "将来会提供";
        public override string DE => "wird bereitstellt";
        public override string EN => "will be provided";
        public override string ES => "se proporciona";
    }

}
