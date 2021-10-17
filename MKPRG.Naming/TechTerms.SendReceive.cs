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
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xE0CD2F9;

        public static Send I { get; } = new Send();

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

    public class WasSent
    : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0x5A8202B6;

        public static WasSent I { get; } = new WasSent();

        public WasSent()
            : base(UID)
        {
        }

        public override string CNT => "wasSent";
        public override string CN => "已发";
        public override string DE => "wurde gesendet";
        public override string EN => "was sent";
        public override string ES => "ha sido enviado";
    }

    public class CanBeSent
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0xDECE2F89;

        public CanBeSent()
            : base(UID)
        {
        }

        public override string CNT => "canBeSent";
        public override string CN => "可发";
        public override string DE => "kann gesendet werden";
        public override string EN => "can be sent";
        public override string ES => "puede ser enviado";
    }

    public class CantBeSent
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0x5A8FF8D8;

        public CantBeSent()
            : base(UID)
        {
        }

        public override string CNT => "cantBeSent";
        public override string CN => "发不出";
        public override string DE => "kann nicht gesendet werden";
        public override string EN => "cant be sent";
        public override string ES => "no se puede enviar";
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
    : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x51CD9ABC;

        public static Receive I { get; } = new Receive();

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

    public class WasReceived
        : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0xD04DCCA2;

        public static WasReceived I { get; } = new WasReceived();

        public WasReceived()
            : base(UID)
        {
        }

        public override string CNT => "WasReceived";
        public override string CN => "已收到";
        public override string DE => "wurde empfangen";
        public override string EN => "was received";
        public override string ES => "se recibió";
    }

    public class CanBeReceived
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0xA9888510;

        public CanBeReceived()
            : base(UID)
        {
        }

        public override string CNT => "CanBeReceived";
        public override string CN => "可收";
        public override string DE => "kann empfangen werden";
        public override string EN => "can be received";
        public override string ES => "puede recibirse";
    }

    public class CantBeReceived
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0x626F5D6F;

        public CantBeReceived()
            : base(UID)
        {
        }

        public override string CNT => "CantBeReceived";
        public override string CN => "收不到";
        public override string DE => "kann nicht empfangen werden";
        public override string EN => "cant be received";
        public override string ES => "no se puede recibir";
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
        : NamingBase, Grammar.Prepositions.IPre
    {
        public const long UID = 0xED73B5C;

        public static From I { get; } = new From();

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
        : NamingBase, Grammar.Prepositions.IPre
    {
        public const long UID = 0x56FBAB48;

        public static To I { get; } = new To();

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
