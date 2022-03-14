using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Packaging
{
    public class Assembly
    : NamingBase
    {

        public const long UID = 0xE5BC1581;

        public Assembly()
            : base(UID)
        {
        }

        public override string CNT => "assembly";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "Assembly";
        public override string ES => EN;

        public override string Glyph => Glyphs.Runtime.Job2;
    }

}
