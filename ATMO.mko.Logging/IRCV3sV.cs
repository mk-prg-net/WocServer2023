using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 1.8.2019
    /// 
    /// mko, 6.10.2020
    /// Erweitert um die Eigenschaft ValueOrException (war schon vorher in RCV3sV implementiert)
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface IRCV3sV<out TValue>
        : IRCV2,
          IValue<TValue>
    {
        /// <summary>
        /// RCV3sV unterscheidet sich von RCV3WithValue dadurch, das InnerRC_T stets vom Typ RCV3 ist.
        /// </summary>
        RCV3 InnerRC_T { get; }

        /// <summary>
        /// Liefert den von der aufgerufenen Funktion berechneten Wert. Falls die aufgerufene Funktion, die den hier 
        /// betrachteten Wert berechnen sollte, fehlschlug, dann wird eine Ausnahme geworfen.
        /// </summary>
        TValue ValueOrException { get; }
    }
}
