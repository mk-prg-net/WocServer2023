using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Auszeichnung von Partikeln aus Zahlendarstellungen in DocuTerms
/// </summary>
namespace MKPRG.Naming.DocuTerms.Numbers
{
    /// <summary>
    /// Vorzeichen einer Zahlendarstellung
    /// </summary>
    public class Sng
        : NamingBase
    {
        public const long UID = 0x5F44CF91;

        public Sng()
            : base(UID)
        {
        }

        public override string CNT => "sng";

        public override string CN => "标志";

        public override string DE => "Vorzeichen";

        public override string EN => "Sign";

        public override string ES => "Signo";

        public static Sng _() => new Sng();
    }

    /// <summary>
    /// Vorzeichen einer Zahlendarstellung
    /// </summary>
    public class Mantissa
        : NamingBase
    {
        public const long UID = 0xC06C2B1;

        public Mantissa()
            : base(UID)
        {
        }

        public override string CNT => "m";

        public override string CN => "咒语";

        public override string DE => "Vorzeichen";

        public override string EN => "Mantissa";

        public override string ES => "Mantissa";

        public static Mantissa _() => new Mantissa();
    }

    /// <summary>
    /// Vorzeichen einer Zahlendarstellung
    /// </summary>
    public class Exp
        : NamingBase
    {

        public const long UID = 0x29B66822;

        public Exp()
            : base(UID)
        {
        }

        public override string CNT => "exp";

        public override string CN => "指数";

        public override string DE => "Exponent";

        public override string EN => "Exponent";

        public override string ES => "Exponente";

        public static Mantissa _() => new Mantissa();
    }




}
