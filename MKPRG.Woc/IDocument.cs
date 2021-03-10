using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Woc
{
    /// <summary>
    /// Schnittstelle eines Dokuments
    /// </summary>
    public interface IDocument
    {
        long DocId { get; }

        long DocNodeId { get; }

        long DocAuthorId { get; }

        string Title { get; }

    }
}
