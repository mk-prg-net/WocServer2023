using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Operators.Boolean
{
    /// <summary>
    /// mko, 9.7.2020    
    /// </summary>
    public class AND
        : NamingBase
    {
        public const long UID = 0xBF847DBC;

        public AND()
            : base(UID)
        {
        }

        public override string CNT => "boolAnd";
        public override string CN => EN;
        public override string DE => "UND";
        public override string EN => "AND";
        public override string ES => EN;
    }

    /// <summary>
    /// mko, 9.7.2020    
    /// </summary>
    public class OR
        : NamingBase
    {
        public const long UID = 0x32AF0389;

        public OR()
            : base(UID)
        {
        }

        public override string CNT => "boolOr";
        public override string CN => EN;
        public override string DE => "ODER";
        public override string EN => "OR";
        public override string ES => EN;
    }

    /// <summary>
    /// mko, 9.7.2020    
    /// </summary>
    public class NOT
        : NamingBase
    {
        public const long UID = 0xB626214A;

        public NOT()
            : base(UID)
        {
        }

        public override string CNT => "boolNot";
        public override string CN => EN;
        public override string DE => "NICHT";
        public override string EN => "NOT";
        public override string ES => EN;
    }

    /// <summary>
    /// mko, 9.7.2020    
    /// </summary>
    public class Implication
        : NamingBase
    {
        public const long UID = 0x90F37751;

        public Implication()
            : base(UID)
        {
        }

        public override string CNT => "boolImplicate";
        public override string CN => EN;
        public override string DE => "DARAUS FOLGT";
        public override string EN => "implies";
        public override string ES => EN;
    }

    /// <summary>
    /// mko, 16.9.2020
    /// Postulat
    /// </summary>
    public class ItIsValid
    : NamingBase
    {
        public const long UID = 0x486EC688;

        public ItIsValid()
            : base(UID)
        {
        }

        public override string CNT => "itIsValid";
        public override string CN => EN;
        public override string DE => "Es gilt";
        public override string EN => "It is valid";
        public override string ES => "Es válido";
    }


}
