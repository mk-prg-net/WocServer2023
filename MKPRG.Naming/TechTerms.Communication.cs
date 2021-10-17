using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Communication
{
    /// <summary>
    /// mko, 18.5.2021
    /// </summary>
    public class Message
        : NamingBase
    {
        public const long UID = 0x1638F454;

        public Message()
            : base(UID)
        {
        }

        public override string CNT => "message";
        public override string CN => "留言";
        public override string DE => "Nachricht";
        public override string EN => "Message";
        public override string ES => "Mensaje";

        public override string Glyph => Glyphs.Communication.Mail;
    }

    public class Messages
        : PluralForm
    {
        public const long UID = 0x77DFC61E;

        public Messages()
            : base(UID)
        {
        }

        public override string CNT => "messages";
        public override string CN => "许多信息";
        public override string DE => "Nachrichten";
        public override string EN => "Messages";
        public override string ES => "Mensajes";

        public override string Glyph => Glyphs.Communication.Mail;

        public override long PluralFormOfNameInSingluarNID => Message.UID;
    }

    public class Sender
        : NamingBase
    {
        public const long UID = 0xAEFC07C0;

        public Sender()
            : base(UID)
        {
        }

        public override string CNT => "sender";
        public override string CN => "寄件人";
        public override string DE => "Absender";
        public override string EN => "sender";
        public override string ES => "Remitente";

        public override string Glyph => Glyphs.Communication.Sender;
    }

    public class CanBeSend
        : NamingBase,
        Grammar.IModalPhrase
    {
        public const long UID = 0x3CA8449B;

        public CanBeSend()
            : base(UID)
        {
        }

        public override string CNT => "canBeSend";
        public override string CN => "可以发送";
        public override string DE => "kann gesendet werden";
        public override string EN => "can be send";
        public override string ES => "puede ser enviado";

        public override string Glyph => Glyphs.Communication.Send;
    }

    public class CantBeSend
    : NamingBase,
    Grammar.IModalPhrase
    {
        public const long UID = 0xB6300F46;

        public CantBeSend()
            : base(UID)
        {
        }

        public override string CNT => "cantBeSend";
        public override string CN => "不能发送";
        public override string DE => "kann nicht gesendet werden";
        public override string EN => "can't be send";
        public override string ES => "no se puede enviar";

        public override string Glyph => Glyphs.Communication.Send;
    }


    public class Send
        : NamingBase,
        Grammar.IInProgressActivity
    {
        public const long UID = 0xE64CA126;

        public Send()
            : base(UID)
        {
        }

        public override string CNT => "send";
        public override string CN => "发送";
        public override string DE => "senden";
        public override string EN => "send";
        public override string ES => "enviar";

        public override string Glyph => Glyphs.Communication.Send;
    }

    public class WasSend
        : NamingBase,
        Grammar.IFinishedActivity
    {
        public const long UID = 0xEE81E34B;

        public WasSend()
            : base(UID)
        {
        }

        public override string CNT => "wasSend";
        public override string CN => "已发送";
        public override string DE => "wurde abgesendet";
        public override string EN => "was send";
        public override string ES => "fue enviado";

        public override string Glyph => Glyphs.Communication.Send;
    }


    public class Receiver
    : NamingBase
    {
        public const long UID = 0x2188DBB7;

        public Receiver()
            : base(UID)
        {
        }

        public override string CNT => "receiver";
        public override string CN => "接收器";
        public override string DE => "Empfänger";
        public override string EN => "receiver";
        public override string ES => "Receptor";

        public override string Glyph => Glyphs.Communication.Receiver;
    }


    public class CanBeReceived
        : NamingBase,
        Grammar.IModalPhrase
    {
        public const long UID = 0x5EFEB3E9;

        public CanBeReceived()
            : base(UID)
        {
        }

        public override string CNT => "canBeReceived";
        public override string CN => "不能发送";
        public override string DE => "kann empfangen werden";
        public override string EN => "can be received";
        public override string ES => "puede recibirse";

        public override string Glyph => Glyphs.Communication.Receive;
    }

    public class CantBeReceived
        : NamingBase,
    Grammar.IModalPhrase
    {
        public const long UID = 0x985F3AC5;

        public CantBeReceived()
            : base(UID)
        {
        }

        public override string CNT => "cantBeReceived";
        public override string CN => "不能发送";
        public override string DE => "kann nicht empfangen werden";
        public override string EN => "can't be received";
        public override string ES => "no se puede recibir";

        public override string Glyph => Glyphs.Communication.Receive;
    }




    public class Receive
        : NamingBase,
        Grammar.IInProgressActivity
    {
        public const long UID = 0xF13065A1;

        public Receive()
            : base(UID)
        {
        }

        public override string CNT => "receive";
        public override string CN => "收到";
        public override string DE => "empfangen";
        public override string EN => "receive";
        public override string ES => "recibido";

        public override string Glyph => Glyphs.Communication.Receive;
    }

    public class WasReceived
    : NamingBase,
    Grammar.IInProgressActivity
    {
        public const long UID = 0xD6F81CFC;

        public WasReceived()
            : base(UID)
        {
        }

        public override string CNT => "wasReceived";
        public override string CN => "已收到";
        public override string DE => "wurde empfangen";
        public override string EN => "was received";
        public override string ES => "se recibió";

        public override string Glyph => Glyphs.Communication.Receive;
    }
}
