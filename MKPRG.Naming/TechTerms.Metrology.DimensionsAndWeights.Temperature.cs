using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Metrology.DimensionsAnWeights.Temperature
{
    public class Temperature : NamingBase
    {
        public const long UID = 0x149E3361;

        public Temperature()
            : base(UID)
        {
        }

        public override string CNT => "temperature";

        public override string CN => "温度";

        public override string DE => "Temperatur";

        public override string EN => "Temperature";

        public override string ES => "Temperatura";

        public override string Glyph => Glyphs.Metrology.Ruler;
    }
}
