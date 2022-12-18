using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// mko, 4.1.2021
/// </summary>
namespace MKPRG.Naming.TechTerms.Markup
{
    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Markup
        : NamingBase
    {

        public const long UID = 0xE7E4BAC;

        public Markup()
            : base(UID)
        {
        }

        public override string CNT => "markup";
        public override string CN => "标记";
        public override string DE => "Textauszeichnung";
        public override string EN => "Markup";
        public override string ES => "Markup";
    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Tag
        : NamingBase
    {

        public const long UID = 0x268B767B;

        public Tag()
            : base(UID)
        {
        }

        public override string CNT => "tag";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class OpeningTag
        : NamingBase
    {

        public const long UID = 0xA1240358;

        public OpeningTag()
            : base(UID)
        {
        }

        public override string CNT => "openingTag";
        public override string CN => "开场白";
        public override string DE => "öffnendes Tag";
        public override string EN => "opening Tag";
        public override string ES => "etiqueta de apertura";
    }


    public class ClosingTag
    : NamingBase
    {

        public const long UID = 0xECC5B0CB;

        public ClosingTag()
            : base(UID)
        {
        }

        public override string CNT => "closingTag";
        public override string CN => "结束语";
        public override string DE => "schließendes Tag";
        public override string EN => "closing Tag";
        public override string ES => "etiqueta de cierre";
    }


}
