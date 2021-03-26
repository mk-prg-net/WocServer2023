using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.Wocs.Nodes.DLL
{
    /// <summary>
    /// mko, 26.3.2021
    /// Node ID für Wocs, die in dieser DLL fest definiert werden
    /// </summary>
    public class MkprgNamingDll
        : NamingBase
    {
        public const long UID = 0xD36DF0CF;

        public MkprgNamingDll()
            : base(UID, 1, _TypeNode.UID, Authors.KorneffelMartin.UID, UID,
                  new (long RefTypeId, long WocId)[]
                  {
                      (_WocTypeNamespace.UID, _TypeNode.UID)
                  })
        {
        }

        public override string CNT => "MkprgNamingDll";

        public override string CN => EN;

        public override string DE => EN;

        public override string EN => "MKPRG.Naming";

        public override string ES => EN;

    }
}
