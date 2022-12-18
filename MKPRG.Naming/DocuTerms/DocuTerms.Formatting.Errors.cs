using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.DocuTerms.Formatting.Errors
{
    public class FormattingError
        : NamingBase
    {

        public const long UID = 0x21F3A8C3;

        public FormattingError()
            : base(UID)
        {
        }

        public override string CNT => "formattingError";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "DocuEntity formatting Error";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }

    public class TriedToRequestANameOfAnEntityThatIsUnnamed
        : NamingBase
    {

        public const long UID = 0x90E1E8C8;

        public TriedToRequestANameOfAnEntityThatIsUnnamed()
            : base(UID)
        {
        }

        public override string CNT => "nameOfAnEntityRequiredThatIsUnnamed";
        public override string CN => EN;
        public override string DE => "Es wurde versucht, den Namen eines DocuTerms anzufordern, der nicht benannt ist.";
        public override string EN => "An attempt was made to request the name of a DocuTerm that is not named.";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }

    public class TriedToRequestANameOfAnEntityThatLacksInterfacesForNameAccess
    : NamingBase
    {

        public const long UID = 0x38D431B;

        public TriedToRequestANameOfAnEntityThatLacksInterfacesForNameAccess()
            : base(UID)
        {
        }

        public override string CNT => "triedToRequestANameOfAnEntityThatLacksInterfacesForNameAccess";
        public override string CN => EN;
        public override string DE => "Der benannte DocuTerm hat keine Schnittstelle für den Namensabruf";
        public override string EN => "The named DocuTerm has no interface for name retrieval";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }

}
