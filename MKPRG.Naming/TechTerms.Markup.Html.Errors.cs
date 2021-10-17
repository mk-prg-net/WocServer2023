using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Markup.Html.Errors
{
    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class ClosingTagIsMissing
        : NamingBase, Grammar.IInProgressActivity
    {

        public const long UID = 0x969FC80F;

        public static ClosingTagIsMissing I { get; } = new ClosingTagIsMissing();

        public ClosingTagIsMissing()
            : base(UID)
        {
        }

        public override string CNT => "closingHtmlTagIsMissing";
        public override string CN => "缺少结尾标签";
        public override string DE => "Das schließende HTML- Tag fehlt";
        public override string EN => "The closing HTML Tag is missing";
        public override string ES => "Falta la etiqueta html de cierre";

        public override string Glyph => Glyphs.Signalization.ErrorOccured;
    }

}
