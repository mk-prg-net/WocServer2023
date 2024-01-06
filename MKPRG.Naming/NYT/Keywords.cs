using MKPRG.Edit.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.Naming.NYT.Keywords
{
    public class CrossWriter
        : NamingBase,
        IGlyphUniCode
    {
        public const long UID = 0x15C1E6B3BCE37C0CL;

        public CrossWriter()
            : base(UID)
        {
        }

        public override string CNT => "crossWriter";
        public override string DE => EN;
        public override string EN => "Cross᛭Writer ";

        public string GlyphUniCode => "᛭ᚥ";
        public override string Glyph => "&#x16ED;&#x16A5;";
    }

    public class Comment
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
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

        public string EditShortCut => "#k";        
    }

    public class ArrayBegin
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
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

        public string EditShortCut => "#y";
    }

    public class Define
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
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

        public string EditShortCut => "#o";
    }

    public class Deref
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut

    {
        public const long UID = 0x761A0C28101A39C4L;

        public Deref()
            : base(UID)
        {
        }

        public override string CNT => "nytDeref";
        public override string DE => "dereferenzieren";
        public override string EN => "derefer";

        public string GlyphUniCode => Glyphs.NYT.IorDereference;
        public override string Glyph => Glyphs.NYT.IorDereferenceHtm;

        public string EditShortCut => "#i";
    }

    public class BeginStage
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x6F0142034855C7B4L;

        public BeginStage()
            : base(UID)
        {
        }

        public override string CNT => "nytBeginStage";
        public override string DE => "Start der Verarbeitungsstufe";
        public override string EN => "Begin of Processing Stage";

        public string GlyphUniCode => Glyphs.NYT.CalcBeginStage;
        public override string Glyph => Glyphs.NYT.CalcBeginStageHtm;

        public string EditShortCut => "#c";
    }

    public class StackAssignment
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x1DC2633CA0B19454L;

        public StackAssignment()
            : base(UID)
        {
        }

        public override string CNT => "nytStackAssignment";
        public override string DE => "Musterbelegung für den Stapelspeicher";
        public override string EN => "Stack Model assignment";

        public string GlyphUniCode => Glyphs.NYT.IngwazModelStackAssigment;
        public override string Glyph => Glyphs.NYT.IngwazModelStackAssigmentHtm;

        public string EditShortCut => "#n";
    }

    public class EndStage
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x183EF800FCDECBDL;

        public EndStage()
            : base(UID)
        {
        }

        public override string CNT => "nytEndStage";
        public override string DE => "Ende der Verarbeitungsstufe";
        public override string EN => "End of Processing Stage";

        public string GlyphUniCode => Glyphs.NYT.EolhxEndStage;
        public override string Glyph => Glyphs.NYT.EolhxEndStageHtm;

        public string EditShortCut => "#x";
    }

    public class ModulBegin
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x13588A67EC20448DL;

        public ModulBegin()
            : base(UID)
        {
        }

        public override string CNT => "nytBeginModul";
        public override string DE => "Begin einer Moduldefinition";
        public override string EN => "Begin of Modul definition";

        public string GlyphUniCode => Glyphs.NYT.MModuleBegin;
        public override string Glyph => Glyphs.NYT.MModuleBeginHtm;

        public string EditShortCut => "#m";
    }

    public class ModulEnd
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x28A658AB389D2743L;

        public ModulEnd()
            : base(UID)
        {
        }

        public override string CNT => "nytEndModul";
        public override string DE => "Ende einer Moduldefinition";
        public override string EN => "End of Modul definition";

        public string GlyphUniCode => Glyphs.NYT.EhwazMooduleEnd;
        public override string Glyph => Glyphs.NYT.EhwazMooduleEnd;

        public string EditShortCut => "#e";
    }

    public class SiegelBranch
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x69C8B7B4FA7977F7L;

        public SiegelBranch()
            : base(UID)
        {
        }

        public override string CNT => "nytSiegelBranch";
        public override string DE => "Siegel Zweig";
        public override string EN => "Siegel Branch";

        public string GlyphUniCode => Glyphs.NYT.SiegelBranch;
        public override string Glyph => Glyphs.NYT.SiegelBranchHtm;

        public string EditShortCut => "#s";
    }

    public class SowiloBranch
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x17188BF74A99EC0BL;

        public SowiloBranch()
            : base(UID)
        {
        }

        public override string CNT => "nytSowiloBranch";
        public override string DE => "Sowilo Zweig";
        public override string EN => "Sowilo Branch";

        public string GlyphUniCode => Glyphs.NYT.SowiloBranch;
        public override string Glyph => Glyphs.NYT.SowiloBranchHtm;

        public string EditShortCut => "#l";
    }

    public class SwitchToSiegelBranch
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x70CCAA3FC76F9359L;

        public SwitchToSiegelBranch()
            : base(UID)
        {
        }

        public override string CNT => "nytSwitchToSiegelBranch";
        public override string DE => "Im Siegel Zweig fortsetzen";
        public override string EN => "Switch to Siegel Branch";

        public string GlyphUniCode => Glyphs.NYT.SwitchToSiegelBranch;
        public override string Glyph => Glyphs.NYT.SwitchToSiegelBranchHtm;

        public string EditShortCut => "#gs";
    }

    public class SwitchToSowiloBranch
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x3B23D49F17CB2BB5L;

        public SwitchToSowiloBranch()
            : base(UID)
        {
        }

        public override string CNT => "nytSwitchToSowiloBranch";
        public override string DE => "Im Sowilo Zweig fortsetzen";
        public override string EN => "Switch to Sowilo Branch";

        public string GlyphUniCode => Glyphs.NYT.SwitchToSowiloBranch;
        public override string Glyph => Glyphs.NYT.SwitchToSowiloBranchHtm;

        public string EditShortCut => "#gl";
    }

    public class StringBegin
        : NamingBase,
        IGlyphUniCode, 
        IEditShortCut
    {
        public const long UID = 0x10B4F165BD72A143L;

        public StringBegin()
            : base(UID)
        {
        }

        public override string CNT => "nytStringBegin";
        public override string DE => "Zeichenkette eröffnen";
        public override string EN => "Begin of String";

        public string GlyphUniCode => Glyphs.NYT.IwazStringBegin;
        public override string Glyph => Glyphs.NYT.IwazStringBeginHtm;

        public string EditShortCut => "#z";
    }

    public class StringCat
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x3CFEC5C4301DD722L;

        public StringCat()
            : base(UID)
        {
        }

        public override string CNT => "nytStringCat";
        public override string DE => "Zeichenkette anfügen";
        public override string EN => "Concatenate String";

        public string GlyphUniCode => Glyphs.NYT.CweorthStringCat;
        public override string Glyph => Glyphs.NYT.CweorthStringCatHtm;

        public string EditShortCut => "#v";
    }

    public class ListBegin
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x32C882437685EE5FL;

        public ListBegin()
            : base(UID)
        {
        }

        public override string CNT => "nytListBegin";
        public override string DE => "Listenanfang";
        public override string EN => "List begin";

        public string GlyphUniCode => Glyphs.NYT.WynnListBegin;
        public override string Glyph => Glyphs.NYT.WynnListBeginHtm;

        public string EditShortCut => "#w";
    }

    public class ListEnd
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x4B8111BA0FA659AFL;

        public ListEnd()
            : base(UID)
        {
        }

        public override string CNT => "nytListEnd";
        public override string DE => "Listenende";
        public override string EN => "List end";

        public string GlyphUniCode => Glyphs.NYT.QListEnd;
        public override string Glyph => Glyphs.NYT.QListEndHtm;

        public string EditShortCut => "#q";
    }

    public class Hirarchy
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x50B0120600626095L;

        public Hirarchy()
            : base(UID)
        {
        }

        public override string CNT => "nytHirarchy";
        public override string DE => "Anfang einer Hierarchie- Definition";
        public override string EN => "Begin of definition of a Hirarchy";

        public string GlyphUniCode => Glyphs.NYT.FehuHirachy;
        public override string Glyph => Glyphs.NYT.FehuHirachyHtm;

        public string EditShortCut => "#f";
    }

    public class BoolValue
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x369ADE021FA69E6DL;

        public BoolValue()
            : base(UID)
        {
        }

        public override string CNT => "nytBool";
        public override string DE => "boolscher Wert";
        public override string EN => "boolean Value";

        public string GlyphUniCode => Glyphs.NYT.BjarkanBool;
        public override string Glyph => Glyphs.NYT.BjarkanBoolHtm;

        public string EditShortCut => "#b";
    }

    public class BoolType
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x189B35AF7C22A624L;

        public BoolType()
            : base(UID)
        {
        }

        public override string CNT => "nytBoolType";
        public override string DE => "Datentyp Boolean";
        public override string EN => "Bool Datatype";

        public string GlyphUniCode => Glyphs.NYT.BjarkanBoolType;
        public override string Glyph => Glyphs.NYT.BjarkanBoolTypeHtm;

        public string EditShortCut => "#tb";
    }

    public class IntValue
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x15B07A6E59F48A94L;

        public IntValue()
            : base(UID)
        {
        }

        public override string CNT => "nytIntValue";
        public override string DE => "ganzzahliger Wert";
        public override string EN => "int Value";

        public string GlyphUniCode => Glyphs.NYT.PInt;
        public override string Glyph => Glyphs.NYT.PIntHtm;

        public string EditShortCut => "#p";
    }


    public class IntType
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x51935CA74016A779L;

        public IntType()
            : base(UID)
        {
        }

        public override string CNT => "nytIntType";
        public override string DE => "Datentyp Integer";
        public override string EN => "Integer Datatype";

        public string GlyphUniCode => Glyphs.NYT.PIntType;
        public override string Glyph => Glyphs.NYT.PIntHtmType;

        public string EditShortCut => "#tp";
    }

    public class Fraction
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x2042A07A32E3B5BBL;

        public Fraction()
            : base(UID)
        {
        }

        public override string CNT => "nytFraction";
        public override string DE => "gebrochene Zahl";
        public override string EN => "Fraction";

        public string GlyphUniCode => Glyphs.NYT.RadFraction;
        public override string Glyph => Glyphs.NYT.RadFractionHtm;

        public string EditShortCut => "#r";
    }


    public class FractionType
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0xE52697AEA1E3735L;

        public FractionType()
            : base(UID)
        {
        }

        public override string CNT => "nytFractionType";
        public override string DE => "Datentyp Bruchzahl";
        public override string EN => "Fraction Datatype";

        public string GlyphUniCode => Glyphs.NYT.RadFractionType;
        public override string Glyph => Glyphs.NYT.RadFractionHtmType;

        public string EditShortCut => "#tr";
    }

    public class FloatingPoint
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x6D13E1AB7C474281L;

        public FloatingPoint()
            : base(UID)
        {
        }

        public override string CNT => "nytFloatingPoint";
        public override string DE => "Gleitpunktzahl";
        public override string EN => "Floating Point Number";

        public string GlyphUniCode => Glyphs.NYT.AcFloatingPointNum;
        public override string Glyph => Glyphs.NYT.AcFloatingPointNumHtm;

        public string EditShortCut => "#a";
    }

    public class FloatingPointType
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x36895688E57B50B4L;

        public FloatingPointType()
            : base(UID)
        {
        }

        public override string CNT => "nytFloatingPointType";
        public override string DE => "Datentyp Gleitpunktzahl";
        public override string EN => "Floating Point  Datatype";

        public string GlyphUniCode => Glyphs.NYT.AcFloatingPointNumType;
        public override string Glyph => Glyphs.NYT.AcFloatingPointNumHtmType;

        public string EditShortCut => "#ta";
    }

    public class NamingId
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x172C6ABFB28B47BFL;

        public NamingId()
            : base(UID)
        {
        }

        public override string CNT => "nytNamingId";
        public override string DE => "Namens- ID";
        public override string EN => "Naming ID";

        public string GlyphUniCode => Glyphs.NYT.HaeglNID;
        public override string Glyph => Glyphs.NYT.HaeglNIDHtm;

        public string EditShortCut => "#h";
    }

    public class NamingIDType
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x69DD59EC5C1EC8FCL;

        public NamingIDType()
            : base(UID)
        {
        }

        public override string CNT => "nytNamingIDType";
        public override string DE => "Datentyp Namens- ID";
        public override string EN => "Naming ID Type";

        public string GlyphUniCode => Glyphs.NYT.HaeglNIDType;
        public override string Glyph => Glyphs.NYT.HaeglNIDHtmType;

        public string EditShortCut => "#th";
    }

    public class SemanticRef
        : NamingBase,
        IGlyphUniCode,
        IEditShortCut
    {
        public const long UID = 0x6A92F35E6C10880L;

        public SemanticRef()
            : base(UID)
        {
        }

        public override string CNT => "nytSemanticRef";
        public override string DE => "Semantische Beziehung";
        public override string EN => "Semantic Reference";

        public string GlyphUniCode => Glyphs.NYT.TvimadurSemanticRef;
        public override string Glyph => Glyphs.NYT.TvimadurSemanticRefHtm;

        public string EditShortCut => "#t";
    }






}
