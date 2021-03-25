using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Woc
{
    /// <summary>
    /// mko, 25.3.2021
    /// Kontext, in dem sich ein Woc befindet, oder in dem ein Woc erstellt wurde
    /// </summary>
    public interface IWocContext
    {
        long WocAuthorId { get; }

        long WocNodeId { get; }
    }
}
