using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.MSSQLServer
{
    /// <summary>
    /// mko, 26.7.2018
    /// Describes result of a query. Contains status information about query execution,
    /// and if available, query result as list of entities.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultSet<T>
    {
        public ResultSet()
        {
            // 16.4.2020
            // IsEmpty Initialwert auf true gesetzt. Vorher: false.
            IsEmpty = true;
            Entities = null;
        }

        public ResultSet(IEnumerable<T> entities)
        {
            this.Entities = entities;

            // 16.4.2020
            // IsEmpty- Initialwert Berechnung abgesichert gegen Nullwerte in entities.
            IsEmpty = !(entities?.Any() ?? false);
        }

        public bool IsEmpty
        {
            get;
            set;
        }

        public IEnumerable<T> Entities { get; set; }
    }
}
