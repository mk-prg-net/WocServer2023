using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Validation.Errors
{
    /// <summary>
    /// mko, 2.7.2020
    /// Unbekannter Wert
    /// </summary>
    public class Unknown : NamingBase
    {
        public const long UID = 0x2734942D;

        public Unknown()
            : base(UID)
        {
        }

        public override string CNT => "unknown";
        public override string CN => "无名氏";
        public override string DE => "unbekannt";
        public override string EN => CNT;
        public override string ES => "desconocido";

        public override string Glyph => Glyphs.Validation.Invalid;
    }


    /// <summary>
    /// mko, 2.7.2020
    /// allg. Bereichsüberschreitung
    /// </summary>
    public class OutOfRange : NamingBase
    {
        public const long UID = 0x39C2E403;

        public OutOfRange()
            : base(UID)
        {
        }

        public override string CNT => "outOfRange";
        public override string CN => "不在状态";
        public override string DE => "Bereichsübrschreitung";
        public override string EN => "out of range";
        public override string ES => "fuera de rango";

        public override string Glyph => $"{Glyphs.Validation.OutOfRange}";

    }

    /// <summary>
    /// mko, 2.7.2020
    /// allg. Bereichsüberschreitung
    /// </summary>
    public class NotANumber : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x37EBD729;

        public NotANumber()
            : base(UID)
        {
        }

        public override string CNT => "notANumber";
        public override string CN => "字符串不是有效的数字表示";
        public override string DE => "Zeichenkette ist keine gültige Zahlendartstellung";
        public override string EN => "Character string is not a valid numeric representation";
        public override string ES => "La cadena no es una representación numérica válida";

        public override string Glyph => $"{Glyphs.Validation.NotANumber}";
    }



    public class StringIsNullOrWhitespace : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x193C01AC;

        public StringIsNullOrWhitespace()
            : base(UID)
        {
        }

        public override string CNT => "stringIsNullOrWhitespace";
        public override string CN => "字符串为空或空白";
        public override string DE => "Zeichenkette ist leer oder nicht vorhanden";
        public override string EN => "String is null or white space only";
        public override string ES => "La cuerda es un espacio nulo o blanco";

        public override string Glyph => $"{Glyphs.Validation.Invalid}";
    }

    public class StringIsNotNullOrWhitespace : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x6336C48E;

        public StringIsNotNullOrWhitespace()
            : base(UID)
        {
        }

        public override string CNT => "stringIsNotNullOrWhitespace";
        public override string CN => "字符串不是空的，也不是只有白色空间";
        public override string DE => "Zeichenkette existiert und ist nicht leer";
        public override string EN => "String is mot null nor white space";
        public override string ES => "La cadena no es nula ni tiene espacios en blanco";

        public override string Glyph => $"{Glyphs.Validation.Invalid}";
    }



    public class StringContainsIllegalCharacters : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x425D635F;

        public StringContainsIllegalCharacters()
            : base(UID)
        {
        }

        public override string CNT => "stringContainsIllegalChar";
        public override string CN => "该字符串包含无效字符";
        public override string DE => "Die Zeichenkette enthält ungültige Zeichen";
        public override string EN => "The string contains invalid characters";
        public override string ES => "La cadena contiene caracteres no válidos";

        public override string Glyph => $"{Glyphs.Validation.Invalid}";
    }

    public class DataInconsistency : NamingBase
    {
        public const long UID = 0x3C4C41D3;

        public DataInconsistency()
            : base(UID)
        {
        }

        public override string CNT => "dataInconsistency";
        public override string CN => "数据不一致";
        public override string DE => "Dateninkonsistenz";
        public override string EN => "Data inconsistency";
        public override string ES => "Incongruencia de los datos";

        public override string Glyph => $"{Glyphs.Validation.Invalid}";
    }




}
