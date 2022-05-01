using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MKPRG.Naming.TechTerms.Grid2D
{
    // Namenscontainer für das Raster2D
    public class Gridpoint
        : NamingBase
    {
        public const long UID = 0xE4D24503;

        public Gridpoint() : base(UID)            
        {
        }

        public override string CNT => "rasterPoint2D";

        public override string DE => "Punkt in einem zweidimensionalen Raster";

        public override string EN => "Point of a 2D Raster";

        public override string ES => EN;

        public override string CN => EN;
    }

    public class CursorCurrentlyAtGridpoint
    : NamingBase
    {
        public const long UID = 0xC1F1F6D4;

        public CursorCurrentlyAtGridpoint() : base(UID)
        {
        }

        public override string CNT => "cursorCurrentlyAtGridpoint";

        public override string DE => "Cursor ist aktuell am Rasterpunkt";

        public override string EN => "Cursor currently at Gridpoint";

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

        public override string CNT => "LeftGridPoint2D";

        public override string DE => "Linker Nachbarpunkt in einem zweidimensionalen Raster";

        public override string EN => "Left Nighbour of a Gridpoint";

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

        public override string CNT => "RightGridPoint2D";

        public override string DE => "Rechter Nachbarpunkt in einem zweidimensionalen Raster";

        public override string EN => "Right Nighbour of a Gridpoint";

        public override string ES => EN;

        public override string CN => EN;
    }

    public class Upper
    : NamingBase
    {
        public const long UID = 0x4C569BE2;

        public Upper() : base(UID)
        {
        }

        public override string CNT => "UpperGridPoint2D";

        public override string DE => "Oberer Nachbarpunkt in einem zweidimensionalen Raster";

        public override string EN => "Upper Nighbour of a Gridpoint";

        public override string ES => EN;

        public override string CN => EN;
    }

    public class Lower
        : NamingBase
    {
        public const long UID = 0x9533DBA4;

        public Lower() : base(UID)
        {
        }

        public override string CNT => "LowerGridPoint2D";

        public override string DE => "Unterer Nachbarpunkt in einem zweidimensionalen Raster";

        public override string EN => "Lower Nighbour of a Gridpoint";

        public override string ES => EN;

        public override string CN => EN;
    }


}
