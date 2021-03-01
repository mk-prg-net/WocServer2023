using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Patterns.Repository
{
    /// <summary>
    /// mko, 1.8.2019
    /// Builder zum erstellen einer Liste von Sortierkriterien, die auf einer Menge von 
    /// abgerufenen Objekten angewendet werden sollen.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISortOrderBuilder<T>
    {
        /// <summary>
        /// Liefert eine Menge von Asteroiden, die bezüglich  der zuvor
        /// eingestellten Filter- und Sortierkriterien gefiltert ist.
        /// </summary>
        /// <returns></returns>
        Task<IFilteredSortedSet<T>> GetFilteredSortedSet();
    }
}
