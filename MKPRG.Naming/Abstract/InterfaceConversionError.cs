using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming
{
    public enum InterfaceConversionErrorTypes
    {
        /// <summary>
        /// Unter der angegebenen Naming- Id ist kein Naiming- Container definiert
        /// </summary>
        NIDisUndefined,

        /// <summary>
        /// Der Naming- Container unterstützt die geforderte Schnittstelle nicht.
        /// </summary>
        RequestedInterfaceIsNotSupportetdByNC,
    }



    /// <summary>
    /// mko, 10.5.2021
    /// 
    /// Dient zur Beschreibung fehlgeschlagenen Konvertierungen in Schnittstellen 
    /// der MKPRG.Naming Bibliothek.
    /// Anstatt das solche Fehlschläge unbehandelte Ausnahmen auslösen, und so die 
    /// ursächlichen Fehlermeldungen verdeckt werden, wird ein Objekt mit dieser Schnittstelle
    /// erzeugt, das den Konvertierungsfehler dokumentiert und zusätzlich einem 
    /// Formatter die Möglichkeit gibt, die Meldung  anzuzeigen
    /// </summary>
    public interface IInterfaceConversionError
    {
        InterfaceConversionErrorTypes ErrorType { get; }
    }

}
