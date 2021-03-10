using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Woc
{
    public class Author : IAuthor
    {
        public long AuthorId { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public DateTime AuthorBrithday { get; set; }

        public string AuthorCity { get; set; }
    }
}
