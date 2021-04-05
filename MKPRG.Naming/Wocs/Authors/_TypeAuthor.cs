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
    public class _TypeAuthor
        : NamingBase
    {
        public const long UID = 0x79F333F6;

        public _TypeAuthor()
            : base(UID, 1, _WocRoot.UID, _WocRoot.UID, _WocRoot.UID,
                 new (long WocType, long Ref)[]
            {
                (_WocTypeNamespace.UID, _WocRoot.UID)
            })

        {
        }

        public override string CNT => "Authors";

        public override string CN => "撰稿人";

        public override string DE => "Autor";

        public override string EN => "Author";

        public override string ES => "Autor";

    }
}
