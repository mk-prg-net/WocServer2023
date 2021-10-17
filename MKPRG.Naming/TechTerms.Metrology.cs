using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Metrology
{
    /// <summary>
    /// mko, 17.6.2020
    /// </summary>
    public class Counter : NamingBase
    {
        public const long UID = 0xB0537E8C;

        public Counter()
            : base(UID)
        {
        }

        public override string CNT => "counter";

        public override string CN => "柜台";

        public override string DE => "Zähler";

        public override string EN => "Counter";

        public override string ES => "Contador";

        public override string Glyph => Glyphs.Metrology.StopWatch;
    }

    /// <summary>
    /// mko, 17.6.2020
    /// </summary>
    public class Measure : NamingBase
    {
        public const long UID = 0xE3151028;

        public Measure()
            : base(UID)
        {
        }

        public override string CNT => "measure";

        public override string CN => "计量值";

        public override string DE => "Messwert";

        public override string EN => "Measured value";

        public override string ES => "Valor medido";

        public override string Glyph => Glyphs.Metrology.Ruler;
    }


}
