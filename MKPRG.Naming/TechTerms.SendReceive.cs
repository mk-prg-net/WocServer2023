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
        public override string CN => "发出";
        public override string DE => "senden";
        public override string EN => "send";
        public override string ES => "enviar";
    }

    public class Sender
        : NamingBase
    {
        public const long UID = 0xDBB726C4;

        public Sender()
            : base(UID)
        {
        }

        public override string CNT => "sender";
        public override string CN => "发件人";
        public override string DE => "Absender";
        public override string EN => "Sender";
        public override string ES => "Remitente";
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
        public override string CN => "接受";
        public override string DE => "empfangen";
        public override string EN => "receive";
        public override string ES => "recibido";
    }

    /// <summary>
    /// mko, 19.3.2021
    /// </summary>
    public class Receiver
        : NamingBase
    {
        public const long UID = 0x9B406AE4;

        public Receiver()
            : base(UID)
        {
        }

        public override string CNT => "receiver";
        public override string CN => "接收器";
        public override string DE => "Empfänger";
        public override string EN => "Receiver";
        public override string ES => "Receptor";
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
        public override string CN => "从";
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
        public override string CN => "到";
        public override string DE => "an";
        public override string EN => "to";
        public override string ES => "a";
    }


}
