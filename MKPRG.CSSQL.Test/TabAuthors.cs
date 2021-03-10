using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.CSSQL.Test
{
    /// <summary>
    /// Tabelle der Autoren
    /// </summary>
    public class TabAuthors
        : Table
    {
        public ColName AuthorId { get; }

        public ColName FirstName { get; }

        public ColName LastName { get; }

        public ColName Birthday { get; }

        public ColName City { get; }

        public TabAuthors(string Aliasname)
            : base("Autoren", Aliasname)
        {
            AuthorId = new ColName(TableName, "Id");
            FirstName = new ColName(TableName, "Vorname");
            LastName = new ColName(TableName, "Name");
            City = new ColName(TableName, "Stadt");
        }

        public TabAuthors()
            : this(null)
        { }       

    }
}
