using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Patterns.Repository
{
    /// <summary>
    /// mko, 1.8.2019
    /// Wird von Objekten implementiert, die Mengen darstellen, welche durch Filterbedingungen und Sortierkriterien 
    /// eingeschränkt wurden.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFilteredSortedSet<T>
    {
        /// <summary>
        /// True, wenn die Menge Elemente enthält
        /// </summary>
        /// <returns></returns>
        bool Any();

        /// <summary>
        /// Anzahl der Elemente in der Menge
        /// </summary>
        /// <returns></returns>
        long Count();


        /// <summary>
        /// Liefert alle Elemente der Menge
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Get(int skip = 0, int take = -1);

    }
}
