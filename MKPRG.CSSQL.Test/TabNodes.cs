using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.CSSQL.Test
{
    /// <summary>
    /// Tabelle der an einem Dokumentenverwaltung beteiligten Server in einem Netz
    /// </summary>
    public class TabNodes
        : Table
    {
        public ColName NodeId { get; }

        /// <summary>
        /// Name des Rechnerknotens
        /// </summary>
        public ColName NodeName { get; }

        /// <summary>
        /// IP6- Adresse des Rechners
        /// </summary>
        public ColName IP6 { get; }

        public TabNodes(string Aliasname)
            : base("Nodes", Aliasname)
        {
            NodeId = new ColName(TableName, "Id");
            NodeName = new ColName(TableName, "Name");
            IP6 = new ColName(TableName, "IP6");
        }

        public TabNodes()
            : this(null)
        { }
    }
}
