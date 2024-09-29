using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Locations
{
    public class Location
        : NamingBase
    {
        public const long UID = 0x548C1EB4;

        public Location()
            : base(UID)
        {
        }

        public override string CNT => "location";
        public override string CN => "地理位置";
        public override string DE => "Ort";
        public override string EN => "Location";
        public override string ES => "Ubicación";

        public override string Glyph => Glyphs.Shapes.Circled_Bullet;
    }

    public class Home
        : NamingBase
    {
        public const long UID = 0x4D47B331;

        public Home()
            : base(UID)
        {
        }

        public override string CNT => "home";
        public override string CN => "家里";
        public override string DE => "Heim";
        public override string EN => "Home";
        public override string ES => "Inicio";

        public override string Glyph => Glyphs.VariousSigns.Home;
    }

    public class Office
        : NamingBase
    {
        public const long UID = 0x2A72102A;

        public Office()
            : base(UID)
        {
        }

        public override string CNT => "office";
        public override string CN => "办公室";
        public override string DE => "Büro";
        public override string EN => "Office";
        public override string ES => "Oficina";

    }

    public class Shopfloor
        : NamingBase
    {
        public const long UID = 0xEA5DD83D;

        public Shopfloor()
            : base(UID)
        {
        }

        public override string CNT => "shopfloor";
        public override string CN => "讲习班";
        public override string DE => "Werkstatt";
        public override string EN => "Shop Floor";
        public override string ES => "Taller";

        public override string Glyph => Glyphs.Tools.HammerAndWrench;

    }

}
