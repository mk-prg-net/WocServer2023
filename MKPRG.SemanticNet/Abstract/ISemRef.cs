using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.SemanticNet
{
    /// <summary>
    /// mko, 1.4.2024
    /// Eine Semantische Referenz ist ein benannter Zeiger von einer Naming Id auf eine andere 
    /// Naming Id.
    /// </summary>
    public interface ISemRef
    {        
        /// <summary>
        /// Naming Id der semantischen Relation 
        /// </summary>
        long SemanticRelationNid { get; }

        /// <summary>
        /// Naming Id, auf die die semantische Relation verweist.
        /// </summary>
        long ReferredNid { get; }
    }

    /// <summary>
    /// Vollqualifizierter, semantischer Pfeil
    /// </summary>
    public interface ISemRefFullQualified 
        : ISemRef
    {
        /// <summary>
        /// Start des semantischen Pfeils
        /// </summary>
        long ReferringNid { get; }
    }
}
