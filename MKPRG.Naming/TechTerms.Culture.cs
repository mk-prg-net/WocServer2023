using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Culture
{
    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Language
     : NamingBase
    {

        public const long UID = 0x4A942539;

        public Language()
            : base(UID)
        {
        }


        public override string CNT => "lng";
        public override string CN => "语种";
        public override string DE => "Sprache";
        public override string EN => "Language";
        public override string ES => "Idioma";

        public override string Glyph => Glyphs.Communication.SpeakingHead;

    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class LanguageSettings
     : NamingBase
    {

        public const long UID = 0xA35DE8C8;

        public LanguageSettings()
            : base(UID)
        {
        }

        public override string CNT => "lngSettings";
        public override string CN => "语言设置";
        public override string DE => "Spracheinstellungen";
        public override string EN => "Language settings";
        public override string ES => "Configuración del idioma";

        public override string Glyph => Glyphs.Communication.SpeakingHead + Glyphs.Algorithm.alternate;
    }

}
