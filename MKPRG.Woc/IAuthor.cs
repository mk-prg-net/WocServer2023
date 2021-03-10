using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Woc
{
    /// <summary>
    /// Schnittstelle eines Autors
    /// </summary>
    public interface IAuthor
    {
        long AuthorId { get; }

        string AuthorFirstName { get; }

        string AuthorLastName { get; }

        DateTime AuthorBrithday { get; }

        string AuthorCity { get; }
    }
}
