using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.Wocs.Authors
{
    /// mko, 25.3.2021
    /// Author ID
    /// </summary>
    public class KorneffelMarina
        : AuthorsBase
    {
        public const long UID = 0xE684A023;

        public KorneffelMarina()
            : base(UID, KorneffelMartin.UID, Nodes.Home.ZotacI7_2021_01.UID)
        {           

        }

        public override string CNT => DE;

        public override string CN => DE;

        public override string DE => "Marina Korneffel";

        public override string EN => DE;

        public override string ES => DE;

    }
}
