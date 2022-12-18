using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.DocuTerms.Composer.Errors
{
    public class ComposerError
        : NamingBase
    {

        public const long UID = 0x4C35BFF2;

        public ComposerError()
            : base(UID)
        {
        }

        public override string CNT => "composerError";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "DocuEntity Composer Error";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }

    public class ComposingDocuTermWithMembersFailed
        : NamingBase, TechTerms.Grammar.IFinishedActivity
    {

        public const long UID = 0xD7DA8F0D;

        public ComposingDocuTermWithMembersFailed()
            : base(UID)
        {
        }

        public override string CNT => "composingDocuTermWithMembersFailed";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "Composing DocuTerm with Members failed";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }


    public class NameIsNull
        : NamingBase
    {

        public const long UID = 0x2777D757;

        public NameIsNull()
            : base(UID)
        {
        }

        public override string CNT => "nameIsNull";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "Name is null!";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }

    public class WildCardInstanceIsNull
        : NamingBase
    {

        public const long UID = 0xBA0EFC68;

        public WildCardInstanceIsNull()
            : base(UID)
        {
        }

        public override string CNT => "wildCardInstanceIsNull";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "WildCard Symbol is null (not defined)";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }


    public class NidIsUnknown
    : NamingBase
    {

        public const long UID = 0xA8FA1865;

        public NidIsUnknown()
            : base(UID)
        {
        }

        public override string CNT => "nidIsUnknown";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "Naming ID is not defined in Naming Container!";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }


    public class ValueIsNull
    : NamingBase
    {

        public const long UID = 0xBD087650;

        public ValueIsNull()
            : base(UID)
        {
        }

        public override string CNT => "valueIsNull";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "value is null!";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }

    public class KillIfNotParamIsNull
         : NamingBase
    {

        public const long UID = 0x192EFB92;

        public KillIfNotParamIsNull()
            : base(UID)
        {
        }

        public override string CNT => "killIfNotParamIsNull";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "KillIfNot Paramter is null!";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }


    public class NidIsUnknownAndValueIsNull
        : NamingBase
    {

        public const long UID = 0xD36FB7EF;

        public NidIsUnknownAndValueIsNull()
            : base(UID)
        {
        }

        public override string CNT => "nidIsUnknownAndValueIsNull";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "Naming ID is not defined in Naming Container! Value of Docuterm is also NULL!";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }

    public class NidIsUnknownAndKillIfNotIsNull
    : NamingBase
    {

        public const long UID = 0x9DBBE2BC;

        public NidIsUnknownAndKillIfNotIsNull()
            : base(UID)
        {
        }

        public override string CNT => "nidIsUnknownAndKillIfNotIsNull";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "Naming ID is not defined in Naming Container! KillIfNot Paramter is null!";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }


    public class NameIsNullAndValueIsNull
    : NamingBase
    {

        public const long UID = 0xE12479BE;

        public NameIsNullAndValueIsNull()
            : base(UID)
        {
        }

        public override string CNT => "nameIsNullAndValueIsNull";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "Name is null! Value of Docuterm is also NULL!";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }

    public class NameIsNullAndKillIfNotIsNull
        : NamingBase
    {

        public const long UID = 0xE6628830;

        public NameIsNullAndKillIfNotIsNull()
            : base(UID)
        {
        }

        public override string CNT => "nameIsNullAndKillIfNotIsNull";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "Name is null! KillIfNot Paramter is null!";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }

    public class WildCardInstanceIsNullAndValueIsNull
        : NamingBase
    {

        public const long UID = 0xC76A25B7;

        public WildCardInstanceIsNullAndValueIsNull()
            : base(UID)
        {
        }

        public override string CNT => "wildCardInstanceIsNullAndValueIsNull";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "WildCard Symbol is null (not defined)! Value of Docuterm is also NULL!";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }

    public class DocuTermAsEventRequired
    : NamingBase
    {

        public const long UID = 0x65A1A9D6;

        public DocuTermAsEventRequired()
            : base(UID)
        {
        }

        public override string CNT => "DocuTermAsEventRequired";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "DocuTerm as Event required";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }

    public class VersionRequestedForAnElementForWichVersionIsNotDefined
        : NamingBase
    {

        public const long UID = 0x23406982;

        public VersionRequestedForAnElementForWichVersionIsNotDefined()
            : base(UID)
        {
        }

        public override string CNT => "versionRequestedForAnElementForWichVersionIsNotDefined";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "version requested for an element for wich version is not defined";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }

    /// <summary>
    /// mko, 9.8.2021
    /// </summary>
    public class CantEncapsulateAsEventParameter
        : NamingBase
    {

        public const long UID = 0x572BBCCB;

        public CantEncapsulateAsEventParameter()
            : base(UID)
        {
        }

        public override string CNT => "CantEncapsulateAsEventParameter";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "There exists a detaild description of error, but it can't be encapsulated as docuTerm EventParameter";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }

    public class CantEncapsulateAsPropertyValue
        : NamingBase
    {

        public const long UID = 0xEF92BA81;

        public CantEncapsulateAsPropertyValue()
            : base(UID)
        {
        }

        public override string CNT => "CantEncapsulateAsPropertyValue";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "There exists a detaild description of error, but it can't be encapsulated as docuTerm PropertyValue";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }

    public class CantEncapsulateAsInstance
        : NamingBase
    {

        public const long UID = 0xDC108B84;

        public CantEncapsulateAsInstance()
            : base(UID)
        {
        }

        public override string CNT => "CantEncapsulateAsInstance";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "There exists a detaild description of error, but it can't be encapsulated as docuTerm Instance";
        public override string ES => EN;

        public override string Glyph => Glyphs.DocuTerms.InvalidDocuTerm;
    }

}
