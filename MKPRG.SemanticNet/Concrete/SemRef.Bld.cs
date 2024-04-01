using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.SemanticNet
{
    /// <summary>
    /// mko, 1.4.2024
    /// Builder für semantische Relationen
    /// </summary>
    partial class SemRef
    {
        public static ISemRef MemberOfSemCtx(long ReferredNid)
            => new SemRef((long)SemanticRefTypeNids.MemberOfSemCtx, ReferredNid);

        public static ISemRef PartOf(long ReferredNid)
            => new SemRef((long)SemanticRefTypeNids.PartOf, ReferredNid);

        public static ISemRef InstanceOf(long ReferredNid)
            => new SemRef((long)SemanticRefTypeNids.InstanceOf, ReferredNid);
    }
}
