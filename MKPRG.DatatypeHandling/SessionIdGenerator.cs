using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKPRG.DatatypeHandling;

namespace MKPRG.Tracing
{
    /// <summary>
    /// mko, 15.11.2018
    /// Implementiert einen Generator zur Erzeugung von Sitzungsnummern.
    /// 
    /// mko, 10.2.2020
    /// Implementierung des Nummerngenerators in LongExt verallgemeinert.
    /// </summary>
    public class SessionIdGenerator
    {

        /// <summary>
        /// Liefert eine eindeutige Sitzungsnummer.
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public long Get(string userid)
        {
            return LongExt.NewGuid();
        }
    }
}
