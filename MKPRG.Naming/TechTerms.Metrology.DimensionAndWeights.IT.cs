using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// mko, 3.2.2021
/// </summary>
namespace MKPRG.Naming.TechTerms.Metrology.DimensionsAnWeights.IT
{

    public class FileSize : NamingBase
    {
        public const long UID = 0xF2B398F2;

        public FileSize()
            : base(UID)
        {
        }

        public override string CNT => "fetch";
        public override string CN => "文件大小";
        public override string DE => "Dateigröße";
        public override string EN => "File size";
        public override string ES => "Tamaño del fichero";
    }

    public class Bit : NamingBase
    {
        public const long UID = 0x260CB074;

        public Bit()
            : base(UID)
        {
        }

        public override string CNT => "bit";
        public override string CN => "比特";
        public override string DE => EN;
        public override string EN => "Bit";
        public override string ES => EN;

        public override string Glyph => "bit";
    }


    public class Byte : NamingBase
    {
        public const long UID = 0x89FB1817;

        public Byte()
            : base(UID)
        {
        }

        public override string CNT => "byte";
        public override string CN => "字节";
        public override string DE => EN;
        public override string EN => "Byte";
        public override string ES => EN;

        public override string Glyph => "byte";
    }

    public class KiloByte : NamingBase
    {
        public const long UID = 0x7F8493D3;

        public KiloByte()
            : base(UID)
        {
        }

        public override string CNT => "kiloByte";
        public override string CN => "千字节";
        public override string DE => EN;
        public override string EN => "Kilo byte";
        public override string ES => EN;

        public override string Glyph => "KB";
    }

    public class MegaByte : NamingBase
    {
        public const long UID = 0xF6545724;

        public MegaByte()
            : base(UID)
        {
        }

        public override string CNT => "megaByte";
        public override string CN => "兆字节";
        public override string DE => EN;
        public override string EN => "mega byte";
        public override string ES => EN;

        public override string Glyph => "MB";
    }

    public class GigaByte : NamingBase
    {
        public const long UID = 0xBBE5C1CD;

        public GigaByte()
            : base(UID)
        {
        }

        public override string CNT => "gigaByte";
        public override string CN => "千兆字节";
        public override string DE => EN;
        public override string EN => "giga byte";
        public override string ES => EN;

        public override string Glyph => "GByte";
    }

    public class TerraByte : NamingBase
    {
        public const long UID = 0x2894A864;

        public TerraByte()
            : base(UID)
        {
        }

        public override string CNT => "terraByte";
        public override string CN => "泰拉字节";
        public override string DE => EN;
        public override string EN => "terra byte";
        public override string ES => EN;

        public override string Glyph => "TByte";
    }

}
