using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Metrology.DimensionsAnWeights.Lenght
{
    public class Length : NamingBase
    {
        public const long UID = 0x72B0540E;

        public Length()
            : base(UID)
        {
        }

        public override string CNT => "lenght";

        public override string CN => "长度";

        public override string DE => "Länge";

        public override string EN => "length";

        public override string ES => "longitud";

        public override string Glyph => Glyphs.Metrology.Ruler;
    }


}
