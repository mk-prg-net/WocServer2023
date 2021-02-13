using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.PatternMatching
{
    /// <summary>
    /// 19.6.2020
    /// Musterausdruck
    /// </summary>
    public class Pattern
        : NamingBase
    {

        public const long UID = 0xF48F6979;

        public Pattern()
            : base(UID)
        {
        }

        public override string CNT => "pattern";
        public override string CN => EN;
        public override string DE => "Vergleichsmuster";
        public override string EN => "Pattern";
        public override string ES => EN;
        
    }

    public class mTestIfMatch
    : NamingBase
    {

        public const long UID = 0x52D9D367;

        public mTestIfMatch()
            : base(UID)
        {
        }

        public override string CNT => "testIfMatch";
        public override string CN => EN;
        public override string DE => "Prüfen auf Übereinstimmung mit dem Muster";
        public override string EN => "Test if pattern is matches";
        public override string ES => "Pruebe si el patrón coincide";

    }

    public class mIsNullEmptyOrWhitespace
        : NamingBase
    {

        public const long UID = 0x88283068;

        public mIsNullEmptyOrWhitespace()
            : base(UID)
        {
        }

        public override string CNT => "isNullEmptyOrWhitespace";

        public override string CN => EN;

        public override string DE => "Prüfen auf Nullwerte bzw. Leerzeichen";

        public override string EN => "Is Null Empty Or Whitespace";

        public override string ES => "Compruebe si hay valores cero o espacios";

    }

    public class pRegularExpression
    : NamingBase
    {

        public const long UID = 0xEFE95F47;

        public pRegularExpression()
            : base(UID)
        {
        }

        public override string CNT => "regEx";

        public override string CN => EN;

        public override string DE => "regulärer Ausdruck";

        public override string EN => "regular Expression";

        public override string ES => "La expresión regular";
    }

    public class pSimilarityLevel
        : NamingBase
    {

        public const long UID = 0x89D99095;

        public pSimilarityLevel()
            : base(UID)
        {
        }

        public override string CNT => "similarityLevel";

        public override string CN => EN;

        public override string DE => "Grad der Ähnlichkeit";

        public override string EN => "degree of similarity";

        public override string ES => "grado de similitud";
    }


    public class vSimilarityLevel_Equal
    : NamingBase
    {

        public const long UID = 0xC81D2819;

        public vSimilarityLevel_Equal()
            : base(UID)
        {
        }

        public override string CNT => "identical";

        public override string CN => EN;

        public override string DE => "identisch";

        public override string EN => "identical";

        public override string ES => "idéntico";
    }

    public class vSimilarityLevel_SimilarButNotEqual
        : NamingBase
    {

        public const long UID = 0x5C63EC2A;

        public vSimilarityLevel_SimilarButNotEqual()
            : base(UID)
        {
        }

        public override string CNT => "similar";

        public override string CN => EN;

        public override string DE => "sehr ähnlich, aber nicht identisch";

        public override string EN => "very similar, but not identical";

        public override string ES => "muy similar, pero no idéntico";
    }

    public class vSimilarityLevel_PartlySimilar
        : NamingBase
    {

        public const long UID = 0x49EA9F41;

        public vSimilarityLevel_PartlySimilar()
            : base(UID)
        {
        }

        public override string CNT => "partlySimilar";

        public override string CN => EN;

        public override string DE => "ähnlich";

        public override string EN => "partly similar";

        public override string ES => "similar a";
    }

    public class vSimilarityLevel_NotSimilar
    : NamingBase
    {

        public const long UID = 0xD6A4CD66;

        public vSimilarityLevel_NotSimilar()
            : base(UID)
        {
        }

        public override string CNT => "notSimilar";

        public override string CN => EN;

        public override string DE => "ähnlich";

        public override string EN => "partly similar";

        public override string ES => "similar a";
    }
}
