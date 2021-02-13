using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Validation
{
    public class Validate : NamingBase
    {
        public const long UID = 0xE00DE815;

        public Validate()
            : base(UID)
        {
        }

        public override string CNT => "validate";
        public override string CN => EN;
        public override string DE => "Validieren";
        public override string EN => "validate";
        public override string ES => "Validar";
        
    }

    public class PreCondition : NamingBase
    {
        public const long UID = 0x24A509B0;

        public PreCondition()
            : base(UID)
        {
        }

        public override string CNT => "preCond";
        public override string CN => EN;
        public override string DE => "Vorbedingung";
        public override string EN => "Precondition";
        public override string ES => "Condición previa";
    }

    public class PostCondition : NamingBase
    {
        public const long UID = 0x68A2EF5D;

        public PostCondition()
            : base(UID)
        {
        }

        public override string CNT => "postCond";
        public override string CN => EN;
        public override string DE => "Nachbedingung";
        public override string EN => "Postcondition";
        public override string ES => "Postcondición";

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
        public override string CN => EN;
        public override string DE => "Prüfe ob Wert innerhalb der Bereichsgrenzen";
        public override string EN => "Check if value inside the range limits";
        public override string ES => "Comprobar si el valor dentro de los límites del rango";
    }

    public class CheckIfAccessible : NamingBase
    {
        public const long UID = 0x8A288897;

        public  CheckIfAccessible()
            : base(UID)
        {
        }

        public override string CNT => "checkIfAccessible";
        public override string CN => EN;
        public override string DE => "prüfen, ob erreichbar";
        public override string EN => "check if accessible";
        public override string ES => "compruebe si es accesible";
    }

}
