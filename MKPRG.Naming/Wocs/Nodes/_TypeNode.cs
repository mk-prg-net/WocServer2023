using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.Wocs.Nodes
{
    public class _TypeNode
            : NamingBase
    {
        public const long UID = 0x35218D13;

        public _TypeNode()
            : base(UID, _WocRoot.UID, _WocRoot.UID, _WocRoot.UID,
                  new (long WocType, long Ref)[]
            {
                (_WocTypeNamespace.UID, _WocRoot.UID)
            })
        {
            
        }

        public override string CNT => "Nodes";

        public override string CN => CNT;

        public override string DE => CNT;

        public override string EN => CNT;

        public override string ES => CNT;

    }

}
