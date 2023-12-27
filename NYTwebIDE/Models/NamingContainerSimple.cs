
using MKPRG.Edit.Abstract;

namespace NYT.Models
{
    /// <summary>
    /// mko, 25.4.02023
    /// Implementierung eines Namenscontainers als POCO
    /// 
    /// mko, 27.12.2023
    /// Auf die Sprachen CNT, EN, und DE zunächst eingeschränkt.
    /// Schnittstellen IEditShortCut, IGlyph und IGlyphUniCode hinzugefügt.
    /// </summary>     
    public class NamingContainerSimple
        :ILangCNT, ILangDE, ILangEN, IEditShortCut, IGlyph, IGlyphUniCode
    {
        public string NIDstr { get; set; }

        public string DE { get; set; }

        public string EN { get; set; }

        public string CNT { get; set; }

        public string EditShortCut { get; set; }

        public string Glyph { get; set; }

        public string GlyphUniCode { get; set; }
    }
}
