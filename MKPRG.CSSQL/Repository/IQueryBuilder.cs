using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.CSSQL
{
    /// <summary>
    /// mko, 1.8.2019
    /// Builder zum definieren komplexer Abfrageausdrücken.
    /// Abgeleitet aus der Entwicklung von MkPrgNet.Pattern.Repository.
    /// Abgeleitete Klassen von dieser Schnittstelle können eine Menge
    /// von nur schreibbaren Eigenschaften definieren, über welche die 
    /// Filtereinschränkungen festgelegt werden
    /// </summary>
    public interface IQueryBuilder<T, TSortOrderBuilder>
        where TSortOrderBuilder : ISortOrderBuilder<T>
    {
        /// <summary>
        /// Wenn alle Filter in dem QueryBuilder definiert wurden,
        /// dann wird diese Methode aufgerufen, um einen SortOrderBuilder
        /// zu erhalten, mit dem man die Sortierreihenfolgen definieren kann.
        /// </summary>
        /// <returns></returns>
        TSortOrderBuilder GetSortOrderBuilder();
    }
}
