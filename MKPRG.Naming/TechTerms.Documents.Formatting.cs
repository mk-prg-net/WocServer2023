using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Documents.Formatting
{
    public class Formatting
        : NamingBase
    {
        public const long UID = 0xEB83CBBD;

        public Formatting()
            : base(UID)
        {
        }

        public override string CNT => "formatting";
        public override string CN => "格式化";
        public override string DE => "Formatierung";
        public override string EN => "Formatting";
        public override string ES => "Formato";

        public override string Glyph => Glyphs.Edit.Underline;
    }

    public class CanFormat
        : NamingBase,
        Grammar.IModalPhrase
    {
        public const long UID = 0x639E0EC4;

        public CanFormat()
            : base(UID)
        {
        }

        public override string CNT => "canFormat";
        public override string CN => "可以格式化";
        public override string DE => "kann formatieren";
        public override string EN => "can format ";
        public override string ES => "puede formatear";
    }

    public class CantFormat
        : NamingBase,
        Grammar.IModalPhrase
    {
        public const long UID = 0x7712D987;

        public CantFormat()
            : base(UID)
        {
        }

        public override string CNT => "cantFormat";
        public override string CN => "无法格式化";
        public override string DE => "kann nicht formatieren";
        public override string EN => "can not format ";
        public override string ES => "no puede formatear";

    }
}
