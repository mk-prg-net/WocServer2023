using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Operators.Arguments
{




    /// <summary>
    /// mko, 1.7.2020
    /// Präposition, die auf das Objekt verweist, auf welches eine Operation angewendet wird
    /// </summary>
    public class AppliedTo
        : NamingBase
    {
        public const long UID = 0x952E8B71;

        public AppliedTo()
            : base(UID)
        {
        }

        public override string CNT => "appliedTo";
        public override string CN => EN;
        public override string DE => "angewendet auf";
        public override string EN => "applied to";
        public override string ES => "aplicado a";
    }

    /// <summary>
    /// mko, 15.7.2020
    /// Verweist auf einen Kontext, in dem eine Operation stattfindet
    /// </summary>
    public class RefersTo
        : NamingBase
    {
        public const long UID = 0xD2DE9DD4;

        public RefersTo()
            : base(UID)
        {
        }

        public override string CNT => "refersTo";
        public override string CN => EN;
        public override string DE => "bezieht sich auf";
        public override string EN => "refers to";
        public override string ES => "se refiere a";
    }

}
