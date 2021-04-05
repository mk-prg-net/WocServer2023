using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.Wocs.Nodes.Home
{
    /// <summary>
    /// mko, 25.3.2021
    /// Node ID von Marinas PC ZOTAC
    /// </summary>
    public class ZotacI7_2021_01
        : NamingBase
    {
        public const long UID = 0xF31D207F;

        public ZotacI7_2021_01()
            : base(UID, 1, _TypeNode.UID, Authors.KorneffelMartin.UID, DLL.MkprgNamingDll.UID)
        {
        }

        public override string CNT => "ZotacZboxI7Nvidia2080";

        public override string CN => EN;

        public override string DE => EN;

        public override string EN => "Zotac ZBOX I7 NVIDIA 2080";

        public override string ES => EN;

    }
}
