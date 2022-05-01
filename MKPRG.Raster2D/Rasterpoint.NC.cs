using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MKPRG.Naming.TechTerms.Raster2D
{
    // Namenscontainer für das Raster2D
    public class Rasterpoint
        : NamingBase
    {
        public const long UID = 0xE4D24503;

        public Rasterpoint() : base(UID)            
        {
        }

        public override string CNT => "rasterPoint2D";

        public override string DE => "Punkt in einem zweidimensionalen Raster";

        public override string EN => "Point of a 2D Raster";

        public override string ES => EN;

        public override string CN => EN;
    }

    public class Left
    : NamingBase
    {
        public const long UID = 0xC1B1B04D;

        public Left() : base(UID)
        {
        }

        public override string CNT => "LeftRasterPoint2D";

        public override string DE => "Linker Nachbarpunkt in einem zweidimensionalen Raster";

        public override string EN => "Left Nighbour Point of a 2D Raster";

        public override string ES => EN;

        public override string CN => EN;
    }

    public class Right
        : NamingBase
    {
        public const long UID = 0xA1D99579;

        public Right() : base(UID)
        {
        }

        public override string CNT => "RightRasterPoint2D";

        public override string DE => "Rechter Nachbarpunkt in einem zweidimensionalen Raster";

        public override string EN => "Right Nighbour Point of a 2D Raster";

        public override string ES => EN;

        public override string CN => EN;
    }

}
