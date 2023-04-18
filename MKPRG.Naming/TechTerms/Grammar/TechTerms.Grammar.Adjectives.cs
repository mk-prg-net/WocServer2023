using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Grammar.Adjectives
{

    /// <summary>
    /// mko, 17.5.2021
    /// 
    /// Klassifiziert einen Namenscontainer als Adjektiv
    /// </summary>
    public interface IAdjective
        : INaming
    { }


    /// <summary>
    /// mko, 17.5.2021
    /// Wortklasse der Adjektive
    /// </summary>
    public class Adjective : NamingBase
    {
        public const long UID = 0x1D92FF01;

        public Adjective()
            : base(UID)
        { }

        public override string CN => "形容词";
        public override string CNT => "adjective";
        public override string DE => "Adjektiv";
        public override string EN => "Adjective";
        public override string ES => "Adjetivo";
    }

    public class AdjectiveConversationError
       : InterfaceConversionErrorBase,
       IAdjective
    {
        public AdjectiveConversationError
            (
                InterfaceConversionErrorTypes ErrorType,
                long NIDofAdjective,
                string CNT,
                string CN,
                string DE,
                string EN,
                string ES
            ) : base(ErrorType, NIDofAdjective, CNT, CN, DE, EN, ES) { }
    }


    // **allgemeine Adjektive**

    public class Good : NamingBase, IAdjective
    {
        public const long UID = 0xD873B5A5;

        public Good()
            : base(UID)
        { }

        public override string CN => "很好";
        public override string CNT => "good";
        public override string DE => "gute";
        public override string EN => "good";
        public override string ES => "bueno";
    }


    public class Bad : NamingBase, IAdjective
    {
        public const long UID = 0x90FB898C;

        public Bad()
            : base(UID)
        { }

        public override string CN => "差劲地";
        public override string CNT => "bad";
        public override string DE => "schlechte";
        public override string EN => "bad";
        public override string ES => "mal";
    }

    public class Exclusive : NamingBase, IAdjective
    {
        public const long UID = 0xF31C8891;

        public Exclusive()
            : base(UID)
        { }

        public override string CN => "专有";
        public override string CNT => "exclusive";
        public override string DE => "exklusive";
        public override string EN => "exclusive";
        public override string ES => "exclusivo";
    }

    public class NotExclusive : NamingBase, IAdjective
    {
        public const long UID = 0x7F017C64;

        public NotExclusive()
            : base(UID)
        { }

        public override string CN => "非排他性";
        public override string CNT => "notExclusive";
        public override string DE => "nicht exklusive";
        public override string EN => "not exclusive";
        public override string ES => "no exclusivo";
    }

    public class Secured : NamingBase, IAdjective
    {
        public const long UID = 0xCE06A8BC;

        public Secured()
            : base(UID)
        { }

        public override string CN => "确保";
        public override string CNT => "secured";
        public override string DE => "gesicherte";
        public override string EN => "secured";
        public override string ES => "asegurado";
    }

    public class Unsecured : NamingBase, IAdjective
    {
        public const long UID = 0xD593ECD7;

        public Unsecured()
            : base(UID)
        { }

        public override string CN => "无担保";
        public override string CNT => "unsecured";
        public override string DE => "unesicherte";
        public override string EN => "unsecured";
        public override string ES => "sin garantía";
    }

}
