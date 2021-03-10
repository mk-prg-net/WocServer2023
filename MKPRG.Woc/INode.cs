using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Woc
{
    /// <summary>
    /// Schnittstelle eines Rechnerknotens
    /// </summary>
    public interface INode
    {
        long NodeId { get; }

        string NodeName { get; }

        string NodeIP6Address { get; }
    }
}
