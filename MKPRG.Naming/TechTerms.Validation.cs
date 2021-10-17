using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Validation
{
    public class Validate : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xE00DE815;

        public Validate()
            : base(UID)
        {
        }

        public override string CNT => "validate";
        public override string CN => "鉴定";
        public override string DE => "Validieren";
        public override string EN => "validate";
        public override string ES => "Validar";

        public override string Glyph => Glyphs.Validation.Check;

    }

    public class Valid : NamingBase, Grammar.Adverbs.IAdverb, Grammar.Adjectives.IAdjective
    {
        public const long UID = 0x408F7A21;

        public Valid()
            : base(UID)
        {
        }

        public override string CNT => "valid";
        public override string CN => "有效";
        public override string DE => "gültige";
        public override string EN => "valid";
        public override string ES => "válido";

        public override string Glyph => Glyphs.Validation.Valid;

    }

    public class Invalid : NamingBase, Grammar.Adverbs.IAdverb, Grammar.Adjectives.IAdjective
    {
        public const long UID = 0x764E7F1A;

        public Invalid()
            : base(UID)
        {
        }

        public override string CNT => "invalid";
        public override string CN => "无效的";
        public override string DE => "ungültige";
        public override string EN => "invalid";
        public override string ES => "inválido";

        public override string Glyph => Glyphs.Validation.Invalid;

    }

    public class WasValidated : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0x1505E72A;

        public WasValidated()
            : base(UID)
        {
        }

        public override string CNT => "wasValidated";
        public override string CN => "已验证";
        public override string DE => "wurde validieret";
        public override string EN => "was validated";
        public override string ES => "fue validado";

        public override string Glyph => Glyphs.Validation.Check;

    }


    public class PreCondition : NamingBase
    {
        public const long UID = 0x24A509B0;

        public PreCondition()
            : base(UID)
        {
        }

        public override string CNT => "preCond";
        public override string CN => "先决条件";
        public override string DE => "Vorbedingung";
        public override string EN => "Precondition";
        public override string ES => "Condición previa";

        public override string Glyph => Glyphs.Validation.CheckPreCond;

    }

    public class PostCondition : NamingBase
    {
        public const long UID = 0x68A2EF5D;

        public PostCondition()
            : base(UID)
        {
        }

        public override string CNT => "postCond";
        public override string CN => "后置条件";
        public override string DE => "Nachbedingung";
        public override string EN => "Postcondition";
        public override string ES => "Postcondición";

        public override string Glyph => Glyphs.Validation.CheckPostCond;


    }

    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class CheckIfValueInRange : NamingBase
    {
        public const long UID = 0xA98EB52C;

        public CheckIfValueInRange()
            : base(UID)
        {
        }

        public override string CNT => "checkIfValueInsideRange";
        public override string CN => "检查值是否在范围内";
        public override string DE => "Prüfe ob Wert innerhalb der Bereichsgrenzen";
        public override string EN => "Check if value inside the range limits";
        public override string ES => "Comprobar si el valor dentro de los límites del rango";

        public override string Glyph => Glyphs.Validation.CheckNotOutOfRange;

    }

    public class CheckIfAccessible : NamingBase
    {
        public const long UID = 0x8A288897;

        public CheckIfAccessible()
            : base(UID)
        {
        }

        public override string CNT => "checkIfAccessible";
        public override string CN => "验明正身";
        public override string DE => "prüfen, ob erreichbar";
        public override string EN => "check if accessible";
        public override string ES => "compruebe si es accesible";

    }

}
