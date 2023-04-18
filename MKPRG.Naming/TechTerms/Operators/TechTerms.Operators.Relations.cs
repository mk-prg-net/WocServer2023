using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Operators.Relations
{
    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class Eq : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x9FB8C34B;

        public Eq()
            : base(UID)
        {
        }

        public override string CNT => "eq";
        public override string CN => "同等";
        public override string DE => "gleich";
        public override string EN => "equal to";
        public override string ES => "igual";

        public override string Glyph => Glyphs.Math.Relations.equal;
    }

    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class NotEq : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xF852EE4C;

        public NotEq()
            : base(UID)
        {
        }

        public override string CNT => "notEq";
        public override string CN => "不等于";
        public override string DE => "ungleich";
        public override string EN => "not equal to";
        public override string ES => "no es igual";

        public override string Glyph => Glyphs.Math.Relations.notEqual;
    }

    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class Lt : NamingBase
    {
        public const long UID = 0xFD382E6F;

        public Lt()
            : base(UID)
        {
        }

        public override string CNT => "lt";
        public override string CN => "下面";
        public override string DE => "kleiner als";
        public override string EN => "lower than";
        public override string ES => "más bajo que";

        public override string Glyph => Glyphs.Math.Relations.lower;
    }

    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class LtEq : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xEE09194F;

        public LtEq()
            : base(UID)
        {
        }

        public override string CNT => "ltEq";
        public override string CN => "小于或等于";
        public override string DE => "kleiner als oder gleich";
        public override string EN => "lower than or equal";
        public override string ES => "inferior o igual";

        public override string Glyph => Glyphs.Math.Relations.lowerOrEqual;
    }


    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class Gt : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x4897F231;

        public Gt()
            : base(UID)
        {
        }

        public override string CNT => "gt";
        public override string CN => "大于";
        public override string DE => "größer als";
        public override string EN => "greater than";
        public override string ES => "mayor que";

        public override string Glyph => Glyphs.Math.Relations.greater;

    }

    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class GtEq : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xFD5BBA4C;

        public GtEq()
            : base(UID)
        {
        }

        public override string CNT => "gtEq";
        public override string CN => "大于或等于";
        public override string DE => "größer als oder gleich";
        public override string EN => "greater than or equal";
        public override string ES => "mayor o igual";

        public override string Glyph => Glyphs.Math.Relations.greaterOrEqual;
    }


    /// <summary>
    /// mko, 13.7.2020
    /// </summary>
    public class IsOfType : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x7E2A80B2;

        public IsOfType()
            : base(UID)
        {
        }

        public override string CNT => "isOfType";
        public override string CN => "属于";
        public override string DE => "ist vom Type";
        public override string EN => "is of type";
        public override string ES => "Es de tipo";
    }

    /// <summary>
    /// mko, 13.7.2020
    /// </summary>
    public class IsNotOfType : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xBA2C7A96;

        public IsNotOfType()
            : base(UID)
        {
        }

        public override string CNT => "isOfType";
        public override string CN => "不属";
        public override string DE => "ist nicht vom Type";
        public override string EN => "is not of type";
        public override string ES => "No es del tipo";
    }



}
