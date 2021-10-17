using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming
{
    /// <summary>
    /// mko, 15.2.2021
    /// </summary>
    public abstract class LanguageBase
        : NamingBase
    {
        public LanguageBase(Language lng, long NID)
            : base(NID)
        {
            this.Language = lng;
        }

        public Naming.Language Language { get; }
    }

    /// <summary>
    /// Neutrale Sprache
    /// </summary>
    public class CultureNeutral
     : LanguageBase
    {

        public const long UID = 0x56DB883;

        public CultureNeutral()
            : base(Naming.Language.CNT, UID)
        {
        }

        public override string CNT => "CNT";
        public override string CN => "文化中立";
        public override string DE => "Kulturneutral";
        public override string EN => "Culture-neutral";
        public override string ES => "Culturalmente neutro";

        public override string Glyph => Glyphs.Culture.Language.CNT;
    }


    //
    public class Chinese
        : LanguageBase
    {

        public const long UID = 0x1601DCA7;

        public Chinese()
            : base(Naming.Language.CN, UID)
        {
        }


        public override string CNT => "CN";
        public override string CN => "中文";
        public override string DE => "Chinesisch";
        public override string EN => "chinese";
        public override string ES => "Chino";

        public override string Glyph => Glyphs.Culture.Language.Chiniese;
    }

    public class German
        : LanguageBase
    {

        public const long UID = 0x8795A37B;

        public German()
            : base(Naming.Language.DE, UID)
        {
        }


        public override string CNT => "DE";
        public override string CN => "德国";
        public override string DE => "Deutsch";
        public override string EN => "German";
        public override string ES => "Alemán";

        public override string Glyph => Glyphs.Culture.Language.German; //"&#x1FC0CF";
    }

    public class English
        : LanguageBase
    {

        public const long UID = 0xE3DA6881;

        public English()
            : base(Naming.Language.EN, UID)
        {
        }


        public override string CNT => "EN";
        public override string CN => "英文";
        public override string DE => "Englisch";
        public override string EN => "English";
        public override string ES => "Inglés";

        public override string Glyph => Glyphs.Culture.Language.English;
    }

    public class Spanish
        : LanguageBase
    {

        public const long UID = 0x340F6F87;

        public Spanish()
            : base(Naming.Language.ES, UID)
        {
        }


        public override string CNT => "ES";
        public override string CN => "西班牙文";
        public override string DE => "Spanisch";
        public override string EN => "Spanish";
        public override string ES => "Español";

        public override string Glyph => Glyphs.Culture.Language.Spanish;
    }
}
