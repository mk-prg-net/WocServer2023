using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Grammar.Prepositions
{
    /// <summary>
    /// mko, 3.9.2020
    /// </summary>
    public class With
    : NamingBase
    {

        public const long UID = 0x3CFE98C1;

        public With()
            : base(UID)
        {
        }

        public override string CNT => "with";
        public override string CN => CNT;
        public override string DE => "mit";
        public override string EN => "with";
        public override string ES => "con";
    }

    /// <summary>
    /// mko, 3.9.2020
    /// </summary>
    public class At
        : NamingBase
    {

        public const long UID = 0x17B42433;

        public At()
            : base(UID)
        {
        }

        public override string CNT => "at";
        public override string CN => CNT;
        public override string DE => "am";
        public override string EN => "at";
        public override string ES => "en";
    }

    /// <summary>
    /// mko, 3.9.2020
    /// </summary>
    public class In
        : NamingBase
    {

        public const long UID = 0x54269A20;

        public In()
            : base(UID)
        {
        }

        public override string CNT => "in";
        public override string CN => CNT;
        public override string DE => "im";
        public override string EN => "in";
        public override string ES => "en";
    }

    /// <summary>
    /// mko, 3.9.2020
    /// </summary>
    public class Above
        : NamingBase
    {

        public const long UID = 0xF7889DC1;

        public Above()
            : base(UID)
        {
        }

        public override string CNT => "above";
        public override string CN => CNT;
        public override string DE => "über";
        public override string EN => "above";
        public override string ES => "sobre";
    }

    /// <summary>
    /// mko, 3.9.2020
    /// </summary>
    public class Under
        : NamingBase
    {

        public const long UID = 0x980FAAD9;

        public Under()
            : base(UID)
        {
        }

        public override string CNT => "under";
        public override string CN => CNT;
        public override string DE => "unter";
        public override string EN => "above";
        public override string ES => "bajo";
    }

    /// <summary>
    /// mko, 3.9.2020
    /// </summary>
    public class For
        : NamingBase
    {

        public const long UID = 0xACB8B1D1;

        public For()
            : base(UID)
        {
        }

        public override string CNT => "for";
        public override string CN => CNT;
        public override string DE => "für";
        public override string EN => "for";
        public override string ES => "para";
    }



}
