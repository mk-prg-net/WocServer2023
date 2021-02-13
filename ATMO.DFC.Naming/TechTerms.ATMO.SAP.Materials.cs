using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.ATMO.SAP.Materials
{
    /// <summary>
    /// mko, 14.9.2020
    /// </summary>
    public class MatClass : NamingBase
    {
        public const long UID = 0x255C928;

        public MatClass()
            : base(UID)
        {
        }

        public override string CNT => "matClass";
        public override string CN => EN;
        public override string DE => "ATMO SAP Materialklasse";
        public override string EN => "ATMO SAP material class";
        public override string ES => "ATMO Clase de material SAP";
    }
}
