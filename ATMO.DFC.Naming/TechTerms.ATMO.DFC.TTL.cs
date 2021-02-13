using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Bezeichner der Tree Transformation Language
/// </summary>
namespace MKPRG.Naming.TechTerms.ATMO.TTL
{
    public class TTL
    : NamingBase
    {

        public const long UID = 0x21B61EEA;

        public TTL
()
            : base(UID)
        {
        }

        public override string CNT => "ttl";
        public override string CN => EN;
        public override string DE => "Baumtransformations- Ausdrücke";
        public override string EN => "Tree transformation language";
        public override string ES => "Expresiones de transformación de árboles";        
    }

    public class PropValWildCard
        : NamingBase
    {

        public const long UID = 0x41D0D4EB;

        public PropValWildCard
()
            : base(UID)
        {
        }

        public override string CNT => "_";
        public override string CN => EN;
        public override string DE => "Beliebiger Wert";
        public override string EN => "Wildcard";
        public override string ES => "Cualquier valor";
    }

    public class Ref
    : NamingBase
    {

        public const long UID = 0x21C995AA;

        public Ref
()
            : base(UID)
        {
        }

        public override string CNT => "ref";
        public override string CN => EN;
        public override string DE => "verweist auf";
        public override string EN => "refers to";
        public override string ES => "se refiere a";
    }


    public class mAllFitsPatterns
        : NamingBase
    {

        public const long UID = 0x795F2936;

        public mAllFitsPatterns
()
            : base(UID)
        {
        }

        public override string CNT => "allFitsPatterns";
        public override string CN => EN;
        public override string DE => "passt auf alles Muster";
        public override string EN => "all fits pattern";
        public override string ES => "encaja en todos los patrones";
    }
}
