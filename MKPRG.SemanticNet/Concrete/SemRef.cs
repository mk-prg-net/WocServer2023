using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.SemanticNet
{
    /// <summary>
    /// mko, 1.4.2024
    /// Triviale Implementierung der semantischen Referenz
    /// </summary>
    public partial class SemRef
        : ISemRef
    {       

        public SemRef(long SemanticRelationNid, long ReferredNid) 
        { 
            this.SemanticRelationNid = SemanticRelationNid;
            this.ReferredNid = ReferredNid;
        }

        public long SemanticRelationNid { get; }

        public long ReferredNid { get; }
    }
}
