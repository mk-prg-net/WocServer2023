using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Grammar.Prepositions
{
    /// <summary>
    /// Präpositionales Objket
    /// 
    /// z.B. 
    /// </summary>
    public class PrepositionalObject
        : NamingBase,
        IPre
    {

        public const long UID = 0xA6B6E12E;

        public PrepositionalObject()
            : base(UID)
        {
        }

        public override string CNT => "prepositionalObject";
        public override string CN => "介词宾语";
        public override string DE => "präpositionales Objket";
        public override string EN => "prepositional Object";
        public override string ES => "objeto preposicional";
    }

    /// <summary>
    /// mko, 6.4.2021
    /// Schnittstelle einer Präposition
    /// </summary>
    public interface IPre
        : INaming
    { }


    /// <summary>
    /// mko, 10.5.2021
    /// </summary>
    public class PreConversationError
        : InterfaceConversionErrorBase,
        IPre
    {
        public PreConversationError
            (
                InterfaceConversionErrorTypes ErrorType,
                long NIDofInProgressActivity,
                string CNT,
                string CN,
                string DE,
                string EN,
                string ES
            ) : base(ErrorType, NIDofInProgressActivity, CNT, CN, DE, EN, ES) { }
    }


    /// <summary>
    /// mko, 3.9.2020
    /// </summary>
    public class With
        : NamingBase,
        IPre
    {

        public const long UID = 0x3CFE98C1;

        public With()
            : base(UID)
        {
        }

        public override string CNT => "with";
        public override string CN => "与";
        public override string DE => "mit";
        public override string EN => "with";
        public override string ES => "con";
    }

    /// <summary>
    /// mko, 14.4.2021
    /// Platzhalter für Funktionen, die eine Preposition erfordern, wo jedoch eine Preposition nicht
    /// erforderlich ist
    /// </summary>
    public class NullPreposition
        : NamingBase, IPre
    {

        public const long UID = 0xBED7C352;

        public NullPreposition()
            : base(UID)
        {
        }

        public override string CNT => "";
        public override string CN => "";
        public override string DE => "";
        public override string EN => "";
        public override string ES => "";
    }


    /// <summary>
    /// mko, 3.9.2020
    /// </summary>
    public class At
        : NamingBase, IPre
    {

        public const long UID = 0x17B42433;

        public At()
            : base(UID)
        {
        }

        public override string CNT => "at";
        public override string CN => "在";
        public override string DE => "am";
        public override string EN => "at";
        public override string ES => "en";
    }

    /// <summary>
    /// mko, 3.9.2020
    /// </summary>
    public class In
        : NamingBase,
        IPre
    {

        public const long UID = 0x54269A20;

        public In()
            : base(UID)
        {
        }

        public override string CNT => "in";
        public override string CN => "在";
        public override string DE => "im";
        public override string EN => "in";
        public override string ES => "en";
    }

    /// <summary>
    /// mko, 3.9.2020
    /// </summary>
    public class Above
        : NamingBase,
        IPre
    {

        public const long UID = 0xF7889DC1;

        public Above()
            : base(UID)
        {
        }

        public override string CNT => "above";
        public override string CN => "以上";
        public override string DE => "über";
        public override string EN => "above";
        public override string ES => "sobre";
    }

    /// <summary>
    /// mko, 3.9.2020
    /// </summary>
    public class Under
        : NamingBase,
        IPre
    {

        public const long UID = 0x980FAAD9;

        public Under()
            : base(UID)
        {
        }

        public override string CNT => "under";
        public override string CN => "在...之下";
        public override string DE => "unter";
        public override string EN => "under";
        public override string ES => "bajo";
    }

    /// <summary>
    /// mko, 3.9.2020
    /// </summary>
    public class For
        : NamingBase,
        IPre
    {

        public const long UID = 0xACB8B1D1;

        public For()
            : base(UID)
        {
        }

        public override string CNT => "for";
        public override string CN => "对于";
        public override string DE => "für";
        public override string EN => "for";
        public override string ES => "para";
    }


    public class From
        : NamingBase,
        IPre
    {

        public const long UID = 0xC6E1F87;

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


    public class Of
        : NamingBase,
    IPre
    {

        public const long UID = 0xCA622D34;

        public Of()
            : base(UID)
        {
        }

        public override string CNT => "of";
        public override string CN => "的";
        public override string DE => "von";
        public override string EN => "of";
        public override string ES => "de";
    }

    public class Beside
        : NamingBase,
        IPre
    {

        public const long UID = 0x521620D1;

        public Beside()
            : base(UID)
        {
        }

        public override string CNT => "beside";
        public override string CN => "旁边";
        public override string DE => "neben";
        public override string EN => "beside";
        public override string ES => "junto a";
    }

    public class TogetherWith
        : NamingBase,
        IPre
    {

        public const long UID = 0x29071989;

        public TogetherWith()
            : base(UID)
        {
        }

        public override string CNT => "together with";
        public override string CN => "在";
        public override string DE => "zusammen mit";
        public override string EN => "together with";
        public override string ES => "en el contexto de";
    }

    public class Outside
        : NamingBase,
        IPre
    {

        public const long UID = 0x679CC675;

        public Outside()
            : base(UID)
        {
        }

        public override string CNT => "outside";
        public override string CN => "外界";
        public override string DE => "außerhalb";
        public override string EN => "outside";
        public override string ES => "en el exterior";
    }

    public class OutsideOf
        : NamingBase,
        IPre
    {

        public const long UID = 0x46E3995E;

        public OutsideOf()
            : base(UID)
        {
        }

        public override string CNT => "outsideOf";
        public override string CN => "之外";
        public override string DE => "außerhalb von";
        public override string EN => "outside of";
        public override string ES => "fuera de";
    }

    public class Inside
    : NamingBase,
    IPre
    {

        public const long UID = 0xB24CFF42;

        public Inside()
            : base(UID)
        {
        }

        public override string CNT => "inside";
        public override string CN => "内幕";
        public override string DE => "innerhalb";
        public override string EN => "inside";
        public override string ES => "dentro de";
    }

    public class InsideOf
        : NamingBase,
        IPre
    {

        public const long UID = 0x3E0BF748;

        public InsideOf()
            : base(UID)
        {
        }

        public override string CNT => "insideOf";
        public override string CN => "里面的";
        public override string DE => "innerhalb von";
        public override string EN => "inside of";
        public override string ES => "en el interior de";
    }


}
