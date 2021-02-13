using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// mko, 9.6.2020
/// Bennenung von Wahrheitswerten etc.
/// </summary>
namespace MKPRG.Naming.DocuTerms.Boolean
{
    /// <summary>
    /// Wahrheitswert für wahr
    /// </summary>
    public class True
        : NamingBase
    {
        public const long UID = 0x93EA7C6B;

        public True()
            : base(UID)
        {}

        public override string CNT => EN;

        public override string DE => "wahr";

        public override string EN => "true";

        public override string ES => "verdadero";

        public override string CN => "真正";
    }

    /// <summary>
    /// Wahrheitswert für falsch
    /// </summary>
    public class False
        : NamingBase
    {
        public const long UID = 0x5046A757;

        public False()
            : base(UID)
        { }

        public override string CNT => EN;

        public override string DE => "falsch";

        public override string EN => "false";

        public override string ES => "falso";

        public override string CN => "假的";
    }
}
