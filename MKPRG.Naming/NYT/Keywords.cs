using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.Naming.NYT.Keywords
{
    public class Comment
        : NamingBase,
        IGlyphUniCode
    {
        public const long UID = 0x7F4A0920DBFC29C7L;

        public Comment()
            : base(UID)
        {
        }

        public override string CNT => "nytComment";
        public override string DE => "Kommentar";
        public override string EN => "Comment";

        public string GlyphUniCode => Glyphs.NYT.Comment;
        public override string Glyph => Glyphs.NYT.CommentHtm;
    }

    public class ArrayBegin
        : NamingBase,
        IGlyphUniCode
    {
        public const long UID = 0x67869646DE224E4AL;

        public ArrayBegin()
            : base(UID)
        {
        }

        public override string CNT => "nytArrayBeginn";
        public override string DE => "Feldanfang";
        public override string EN => "Array Begin";

        public string GlyphUniCode => Glyphs.NYT.YArrayBegin;
        public override string Glyph => Glyphs.NYT.YArrayBeginHtm;
    }

    public class Define
        : NamingBase,
        IGlyphUniCode
    {
        public const long UID = 0x6F8AAE7E191CEA0BL;

        public Define()
            : base(UID)
        {
        }

        public override string CNT => "nytDefine";
        public override string DE => "Benennen";
        public override string EN => "Define";

        public string GlyphUniCode => Glyphs.NYT.OthalanDefine;
        public override string Glyph => Glyphs.NYT.OthalanDefineHtm;
    }




}
