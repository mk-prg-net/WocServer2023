using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.SizeRatios
{
    public class SizeRatio
        : NamingBase
    {

        public const long UID = 0xD212E222;

        public SizeRatio()
            : base(UID)
        {
        }

        public override string CNT => "sizeRatio";
        public override string CN => "尺寸比例";
        public override string DE => "Größenverhältnis";
        public override string EN => "Size Ratio";
        public override string ES => "Ratios de tamaño";

        public override string Glyph => Glyphs.Metrology.Balance;
    }

    public class Long
        : NamingBase,
    Grammar.Adverbs.IAdverb
    {

        public const long UID = 0xCBF3CAA9;

        public Long()
            : base(UID)
        {
        }

        public override string CNT => "Long";
        public override string CN => "郎";
        public override string DE => "lang";
        public override string EN => "long";
        public override string ES => "largo";

    }


    public class ToLong
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0xDF4556B4;

        public ToLong()
            : base(UID)
        {
        }

        public override string CNT => "toLong";
        public override string CN => "时间太长";
        public override string DE => "zu lang";
        public override string EN => "to long";
        public override string ES => "demasiado largo";

        public override string Glyph => Glyphs.Validation.Invalid;
    }

    public class Short
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0xA6537D97;

        public Short()
            : base(UID)
        {
        }

        public override string CNT => "short";
        public override string CN => "短期";
        public override string DE => "kurz";
        public override string EN => "short";
        public override string ES => "corto";

    }


    public class ToShort
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0x668D2889;

        public ToShort()
            : base(UID)
        {
        }

        public override string CNT => "toShort";
        public override string CN => "太短";
        public override string DE => "zu kurz";
        public override string EN => "to short";
        public override string ES => "demasiado corto";

        public override string Glyph => Glyphs.Validation.Invalid;
    }

    public class Big
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0x6DB77F3C;

        public Big()
            : base(UID)
        {
        }

        public override string CNT => "big";
        public override string CN => "大";
        public override string DE => "groß";
        public override string EN => "big";
        public override string ES => "grande";
    }


    public class ToBig
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0x9151542A;

        public ToBig()
            : base(UID)
        {
        }

        public override string CNT => "toBig";
        public override string CN => "太大";
        public override string DE => "zu groß";
        public override string EN => "to big";
        public override string ES => "demasiado grande";

        public override string Glyph => Glyphs.Validation.Invalid;
    }

    public class Small
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0xFBC41B8A;

        public Small()
            : base(UID)
        {
        }

        public override string CNT => "Small";
        public override string CN => "小";
        public override string DE => "klein";
        public override string EN => "small";
        public override string ES => "pequeño";
    }

    public class ToSmall
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0x83A689A9;

        public ToSmall()
            : base(UID)
        {
        }

        public override string CNT => "toSmall";
        public override string CN => "太小";
        public override string DE => "zu klein";
        public override string EN => "to small";
        public override string ES => "demasiado pequeño";

        public override string Glyph => Glyphs.Validation.Invalid;
    }

    public class Fast
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0xF38E745F;

        public Fast()
            : base(UID)
        {
        }

        public override string CNT => "fast";
        public override string CN => "快速";
        public override string DE => "schnell";
        public override string EN => "fast";
        public override string ES => "rápido";

    }

    public class ToFast
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0xB9496478;

        public ToFast()
            : base(UID)
        {
        }

        public override string CNT => "toFast";
        public override string CN => "太快了";
        public override string DE => "zu schnell";
        public override string EN => "to fast";
        public override string ES => "demasiado rápido";

        public override string Glyph => Glyphs.Validation.Invalid;
    }

    public class Slow
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0xB6D51020;

        public Slow()
            : base(UID)
        {
        }

        public override string CNT => "slow";
        public override string CN => "缓慢";
        public override string DE => "langsam";
        public override string EN => "slow";
        public override string ES => "lento";
    }

    public class ToSlow
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0xD27A582D;

        public ToSlow()
            : base(UID)
        {
        }

        public override string CNT => "toSlow";
        public override string CN => "太慢了";
        public override string DE => "zu langsam";
        public override string EN => "to slow";
        public override string ES => "demasiado lento";

        public override string Glyph => Glyphs.Validation.Invalid;
    }

    public class Wide
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0xD8DF2F54;

        public Wide()
            : base(UID)
        {
        }

        public override string CNT => "wide";
        public override string CN => "广泛的";
        public override string DE => "breit";
        public override string EN => "wide";
        public override string ES => "amplia";
    }


    public class TooWide
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0xF4B5F5E1;

        public TooWide()
            : base(UID)
        {
        }

        public override string CNT => "tooWide";
        public override string CN => "太宽";
        public override string DE => "zu breit";
        public override string EN => "too wide";
        public override string ES => "demasiado amplio";

        public override string Glyph => Glyphs.Validation.Invalid;
    }

    public class Narrow
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0xC3CAB1B5;

        public Narrow()
            : base(UID)
        {
        }

        public override string CNT => "Narrow";
        public override string CN => "狭窄的";
        public override string DE => "schmal";
        public override string EN => "narrow";
        public override string ES => "estrecho";

    }

    public class TooNarrow
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0x82D55ECB;

        public TooNarrow()
            : base(UID)
        {
        }

        public override string CNT => "tooNarrow";
        public override string CN => "太窄";
        public override string DE => "zu schmal";
        public override string EN => "too narrow";
        public override string ES => "demasiado estrecho";

        public override string Glyph => Glyphs.Validation.Invalid;
    }

    public class Heavy
        : NamingBase,
    Grammar.Adverbs.IAdverb
    {

        public const long UID = 0xE5AAF479;

        public Heavy()
            : base(UID)
        {
        }

        public override string CNT => "heavy";
        public override string CN => "沉重";
        public override string DE => "schwer";
        public override string EN => "heavy";
        public override string ES => "pesado";
    }


    public class TooHeavy
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0x9F71F6A9;

        public TooHeavy()
            : base(UID)
        {
        }

        public override string CNT => "tooHeavy";
        public override string CN => "太宽";
        public override string DE => "zu schwer";
        public override string EN => "too heavy";
        public override string ES => "demasiado pesado";

        public override string Glyph => Glyphs.Validation.Invalid;
    }

    public class Light
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0x42F76B44;

        public Light()
            : base(UID)
        {
        }

        public override string CNT => "light";
        public override string CN => "容易";
        public override string DE => "leicht";
        public override string EN => "light";
        public override string ES => "fácil";

    }

    public class TooLight
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0xF48F754E;

        public TooLight()
            : base(UID)
        {
        }

        public override string CNT => "tooLight";
        public override string CN => "太轻了";
        public override string DE => "zu leicht";
        public override string EN => "too light";
        public override string ES => "demasiado ligero";

        public override string Glyph => Glyphs.Validation.Invalid;
    }

    public class Easy
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0xA9EAADBA;

        public Easy()
            : base(UID)
        {
        }

        public override string CNT => "easy";
        public override string CN => "顺利";
        public override string DE => "einfach";
        public override string EN => "easy";
        public override string ES => "easy";

    }

    public class TooEasy
        : NamingBase,
    Grammar.Adverbs.IAdverb
    {

        public const long UID = 0x9AC7EBE5;

        public TooEasy()
            : base(UID)
        {
        }

        public override string CNT => "tooEasy";
        public override string CN => "太容易了";
        public override string DE => "zu einfach";
        public override string EN => "too easy";
        public override string ES => "demasiado fácil";

        public override string Glyph => Glyphs.Validation.Invalid;
    }

    public class Complicated
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0xA26F63B9;

        public Complicated()
            : base(UID)
        {
        }

        public override string CNT => "complicated";
        public override string CN => "复杂的";
        public override string DE => "kompliziert";
        public override string EN => "complicated";
        public override string ES => "complicado";

    }

    public class TooComplicated
        : NamingBase,
    Grammar.Adverbs.IAdverb
    {

        public const long UID = 0x75452593;

        public TooComplicated()
            : base(UID)
        {
        }

        public override string CNT => "tooComplicated";
        public override string CN => "太复杂了";
        public override string DE => "zu kompliziert";
        public override string EN => "too complicated";
        public override string ES => "demasiado complicado";

        public override string Glyph => Glyphs.Validation.Invalid;
    }

    public class Complex
    : NamingBase,
    Grammar.Adverbs.IAdverb
    {

        public const long UID = 0xBC6BDD5C;

        public Complex()
            : base(UID)
        {
        }

        public override string CNT => "complex";
        public override string CN => "复杂的";
        public override string DE => "komplex";
        public override string EN => "complex";
        public override string ES => "complejo";

    }

    public class TooComplex
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {

        public const long UID = 0xB397587C;

        public TooComplex()
            : base(UID)
        {
        }

        public override string CNT => "tooComplex";
        public override string CN => "太复杂了";
        public override string DE => "zu komplex";
        public override string EN => "too complex";
        public override string ES => "demasiado complejo";

        public override string Glyph => Glyphs.Validation.Invalid;
    }

}
