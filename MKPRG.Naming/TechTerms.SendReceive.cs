using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.SendReceive
{
    /// <summary>
    /// mko, 28.7.2020
    /// </summary>
    public class Send
        : NamingBase
    {
        public const long UID = 0xE0CD2F9;

        public Send()
            : base(UID)
        {
        }

        public override string CNT => "send";
        public override string CN => EN;
        public override string DE => "senden";
        public override string EN => "send";
        public override string ES => "enviar";
    }

    /// <summary>
    /// mko, 28.7.2020
    /// </summary>
    public class Receive
    : NamingBase
    {
        public const long UID = 0x51CD9ABC;

        public Receive()
            : base(UID)
        {
        }

        public override string CNT => "receive";
        public override string CN => EN;
        public override string DE => "empfangen";
        public override string EN => "receive";
        public override string ES => "recibido";
    }

    /// <summary>
    /// mko, 28.7.2020
    /// </summary>
    public class From
        : NamingBase
    {
        public const long UID = 0xED73B5C;

        public From()
            : base(UID)
        {
        }

        public override string CNT => "from";
        public override string CN => EN;
        public override string DE => "von";
        public override string EN => "from";
        public override string ES => "de";
    }

    /// <summary>
    /// mko, 28.7.2020
    /// </summary>
    public class To
        : NamingBase
    {
        public const long UID = 0x56FBAB48;

        public To()
            : base(UID)
        {
        }

        public override string CNT => "to";
        public override string CN => EN;
        public override string DE => "an";
        public override string EN => "to";
        public override string ES => "a";
    }


}
