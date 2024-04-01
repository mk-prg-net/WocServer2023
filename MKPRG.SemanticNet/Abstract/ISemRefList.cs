using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.SemanticNet
{
    /// <summary>
    /// mko, 1.4.2024
    /// Listet alle semantischen Beziehungen eines Entites zu anderen auf.
    /// </summary>
    public interface ISemRefList
    {
        ISemRef[] SemanticRelations { get; }
    }
}
