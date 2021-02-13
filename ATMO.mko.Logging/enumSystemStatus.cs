using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// Allgemeine Zustandsbeschreibung eines Systems
    /// </summary>
    public enum SystemStatus
    {
        /// <summary>
        /// Keine Fehlfunktionen im System sind aktuell bekannt
        /// Alle Bits sind 0
        /// </summary>
        ok = 0,

        /// <summary>
        /// Es sind kritische Zustände und entdeckt worden, die Fehlfunktionen zur Folge haben können
        /// Bit 8 ist gesetzt
        /// </summary>
        warnings = 256,

        /// <summary>
        /// Es wurden Fehlfunktionen detektiert.
        /// Bit 16 ist gesetzt
        /// </summary>
        errors = 256*256,

        /// <summary>
        /// Das System wurde gestoppt
        /// Bit 24 ist gesetzt
        /// </summary>
        halted = 256*256*256

    }
}
