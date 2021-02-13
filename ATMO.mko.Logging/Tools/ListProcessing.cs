using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.Tools
{
    /// <summary>
    /// mko, 13.12.2019
    /// 
    /// Funktionen, welche die Listenverarbeitung vereinfachen sollen.
    /// </summary>
    public static class ListProcessingExt
    {
        /// <summary>
        /// Stellt einen Wert oder ein Objekt in eine einelementiges Array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <returns></returns>
        public static T[] AsArray<T>(this T x)
        {
            return new T[] { x };
        }

        /// <summary>
        /// Stellt einen Wert oder ein Objekt in eine einelementige Liste.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <returns></returns>
        public static List<T> AsList<T>(this T x)
        {
            return new List<T> { x };
        }

        /// <summary>
        /// mko, 13.12.2019
        /// 
        /// Fügt einer Liste ein Element hinzu und gibt die Liste wieder zurück
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="L"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static List<T> AddFn<T>(this List<T> L, T x)
        {
            L.Add(x);
            return L;
        }

    }
}
