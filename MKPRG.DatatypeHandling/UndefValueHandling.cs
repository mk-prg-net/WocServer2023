using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.Tools
{
    /// <summary>
    /// mko, 24.9.2020
    /// Wenn Eigenschaften noch nie ein Wert zugewiesen wurde, 
    /// </summary>
    public static class UndefValueHandling
    {
        /// <summary>
        /// Wirft eine NullReferenceException, wenn value null ist.
        /// Kann zur Implementierung von Gettern eingesetzt werden, um Sicherzustellen, das diese 
        /// nur dann aufgerufen werden, nachdem die Eigenschaften mit einem sinnvollen Wert initialisiert wurden.
        /// Der Versuch, den Wert einer Eigenschaft auf Null zu prüfen, ist dann nicht mehr möglich (gewollt)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T NullRefExceptionIfUndefined<T>(this T value)            
        {
            if (value == null)
                throw new NullReferenceException();
            else
                return value;
        }

    }
}
