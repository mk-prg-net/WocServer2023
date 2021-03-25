using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.Wocs.Nodes.Home
{
    public class AsusRog80_2015
        : NamingBase
    {
        public const long UID = 0xE1F4DC2C;

        public AsusRog80_2015()
            : base(UID, _TypeNode.UID, Authors.KorneffelMartin.UID, ZotacI7_2021_01.UID)
        {
        }

        public override string CNT => "asusRog80";

        public override string CN => EN;

        public override string DE => EN;

        public override string EN => "Asus ROG 80";

        public override string ES => EN;

    }
}
