using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.CSSQL.Test
{
    /// <summary>
    /// Tabelle der Dokumente
    /// </summary>
    public class TabDocuments
        : Table
    {
        public ColName DocId { get; }

        public ColName AuthorId { get; }

        public ColName NodeId { get; }

        public ColName Title { get; }

        public TabDocuments(string Aliasname)
            : base("Dokumente", Aliasname)
        {
            DocId = new ColName(TableName, "Id");
            AuthorId = new ColName(TableName, "fkAuthorId");
            NodeId = new ColName(TableName, "fkRechnerId");
            Title = new ColName(TableName, "Title");
        }

        public TabDocuments()
            : this(null)
        { }
    }
}
