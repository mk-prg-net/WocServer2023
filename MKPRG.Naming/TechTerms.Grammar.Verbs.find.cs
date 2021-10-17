using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Grammar.Verbs
{
    public class find
        : NamingBase, IInProgressActivity
    {
        public const long UID = 0x697096D0;

        public find()
            : base(UID)
        {
        }

        public override string CNT => "find";
        public override string CN => "发现";
        public override string DE => "finde";
        public override string EN => "find";
        public override string ES => "encontrar";

        public override string Glyph => Glyphs.VariousSigns.Magnifier;
    }

    public class found
        : NamingBase, IFinishedActivity
    {
        public const long UID = 0xB9863C3C;

        public found()
            : base(UID)
        {
        }

        public override string CNT => "found";
        public override string CN => "发现";
        public override string DE => "gefunden";
        public override string EN => "found";
        public override string ES => "encontrdo";

        public override string Glyph => Glyphs.VariousSigns.Magnifier;
    }

}
