using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.MSSQLServer
{
    /// <summary>
    /// mko, 26.7.2018
    /// Beschreibt das Ergebnis eines Datensatzabrufes über einen Schlüssel.
    /// 
    /// </summary>
    public class Result<T>
        where T : new()
    {
        public Result()
        {
            Entity = new T();
            IsEmpty = true;
        }

        public Result(T v)
        {
            Entity = v;
            IsEmpty = false;
        }

        /// <summary>
        /// Wenn true, dann konnte zu einem Schlüssel kein Datensatz gefunden werden
        /// </summary>
        public bool IsEmpty { get; }

        /// <summary>
        /// Der dem Schlüssel zugeordnete Datensatz
        /// </summary>
        public T Entity { get; }
    }
}
