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
        public override string CN => EN;
        public override string DE => "unbekannt";
        public override string EN => CNT;
        public override string ES => "desconocido";
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
        public override string CN => EN;
        public override string DE => "Bereichsübrschreitung";
        public override string EN => "out of range";
        public override string ES => "fuera de rango";
    }

    /// <summary>
    /// mko, 2.7.2020
    /// allg. Bereichsüberschreitung
    /// </summary>
    public class NotANumber : NamingBase
    {
        public const long UID = 0x37EBD729;

        public NotANumber()
            : base(UID)
        {
        }

        public override string CNT => "notANumber";
        public override string CN => EN;
        public override string DE => "Zeichenkette ist keine gültige Zahlendartstellung";
        public override string EN => "Character string is not a valid numeric representation";
        public override string ES => "La cadena no es una representación numérica válida";
    }



    public class StringIsNullOrWhitespace : NamingBase
    {
        public const long UID = 0x193C01AC;

        public StringIsNullOrWhitespace()
            : base(UID)
        {
        }

        public override string CNT => "stringIsNullOrWhitespace";
        public override string CN => EN;
        public override string DE => "Die Zeichenkette ist leer oder nicht vorhanden";
        public override string EN => "String is null or white space";
        public override string ES => "La cuerda es un espacio nulo o blanco";
    }

    public class StringContainsIllegalCharacters : NamingBase
    {
        public const long UID = 0x425D635F;

        public StringContainsIllegalCharacters()
            : base(UID)
        {
        }

        public override string CNT => "stringContainsIllegalChar";
        public override string CN => EN;
        public override string DE => "Die Zeichenkette enthält ungültige Zeichen";
        public override string EN => "The string contains invalid characters";
        public override string ES => "La cadena contiene caracteres no válidos";
    }




}
