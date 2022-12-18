using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Support
{
    public class Support
        : NamingBase
    {

        public const long UID = 0xE95D3087;

        public Support()
            : base(UID)
        {
        }

        public override string CNT => "support";
        public override string CN => "支持";
        public override string DE => "Unterstützung";
        public override string EN => "Support";
        public override string ES => "Soporte";

        public override string Glyph => Glyphs.Support.Rescuer;
    }

    public class Supporting
        : NamingBase,
        Grammar.IInProgressActivity
    {

        public const long UID = 0x9D7912A6;

        public Supporting()
            : base(UID)
        {
        }


        public override string CNT => "supporting";
        public override string CN => "支持";
        public override string DE => "unterstützen";
        public override string EN => "supporting";
        public override string ES => "soporte";

        public override string Glyph => Glyphs.Support.Rescuer;
    }

    public class SOS
        : NamingBase,
        Grammar.IInProgressActivity
    {

        public const long UID = 0xBDAB582A;

        public SOS()
            : base(UID)
        {
        }

        public override string CNT => "sos";
        public override string CN => "建议";
        public override string DE => "dringende Supportanfrage";
        public override string EN => "urgent support request";
        public override string ES => "solicitud de apoyo urgente";

        public override string Glyph => Glyphs.Support.SOS;
    }

    public class Help
        : NamingBase
    {

        public const long UID = 0xFACE4C0C;

        public Help()
            : base(UID)
        {
        }

        public override string CNT => "help";
        public override string CN => "帮助";
        public override string DE => "Hilfe";
        public override string EN => "Help";
        public override string ES => "Ayuda";

        public override string Glyph => Glyphs.DataAndDocuments.Book;
    }

    public class Documentation
        : NamingBase
    {

        public const long UID = 0xB37EEC35;

        public Documentation()
            : base(UID)
        {
        }

        public override string CNT => "docu";
        public override string CN => "文件";
        public override string DE => "Dokumentation";
        public override string EN => "Documentation";
        public override string ES => "Documentación";

        public override string Glyph => Glyphs.DataAndDocuments.Books;
    }




    public class Advice
        : NamingBase
    {

        public const long UID = 0xE6777AB7;

        public Advice()
            : base(UID)
        {
        }

        public override string CNT => "advice";
        public override string CN => "建议";
        public override string DE => "Tipp";
        public override string EN => "Advice";
        public override string ES => "Consejo";

        public override string Glyph => Glyphs.Events.Info;
    }
}
