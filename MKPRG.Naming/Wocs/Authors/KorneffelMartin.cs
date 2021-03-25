using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.Wocs.Authors
{

    /// <summary>
    /// mko, 25.3.2021
    /// Author ID
    /// </summary>
    public class KorneffelMartin
        : AuthorsBase
    {
        public const long UID = 0x323AB956;

        public KorneffelMartin()
            : base(UID, UID, Nodes.Home.ZotacI7_2021_01.UID)
        {
        }

        public override string CNT => DE;

        public override string CN => DE;

        public override string DE => "Martin Korneffel";

        public override string EN => DE;

        public override string ES => DE;

    }

}
