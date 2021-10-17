using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Markup.Semantic
{
    public class Article
        : NamingBase
    {

        public const long UID = 0x6C0CB6D3;

        public Article()
            : base(UID)
        {
        }

        public override string CNT => "article";
        public override string CN => "文章";
        public override string DE => "Artikel";
        public override string EN => "Article";
        public override string ES => "Artículo";

        public override string Glyph => Glyphs.DataAndDocuments.SemanticMarkup.Article;
    }

    public class TextParagraph
    : NamingBase
    {

        public const long UID = 0x49264A1;

        public TextParagraph()
            : base(UID)
        {
        }

        public override string CNT => "paragraph";
        public override string CN => "案文段落";
        public override string DE => "Absatz";
        public override string EN => "Text paragraph";
        public override string ES => "Artículo";

        public override string Glyph => Glyphs.DataAndDocuments.SemanticMarkup.TextParagraph;
    }
}
