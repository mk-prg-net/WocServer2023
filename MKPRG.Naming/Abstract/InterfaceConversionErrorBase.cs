using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming
{
    /// <summary>
    /// mko, 10.5.2021
    /// Basisklasse für alle Namenscontainer, die dynamisch erzeugt werden, wenn sich ein
    /// Namenscontainer nicht in eine Zielschnittstelle konvertieren lässt.
    /// Meldungen und ID des Namenscontainers werden im Konstruktor übergeben.
    /// Zusätzlich ist diese Klasse mit der Schnittstelle IInterfaceConversionError markiert.
    /// </summary>
    public class InterfaceConversionErrorBase
        : NamingBase,
        IInterfaceConversionError
    {
        public override string CNT => _CNT;
        string _CNT;

        public override string DE => _DE;
        string _DE;

        public override string EN => _EN;
        string _EN;

        public override string ES => _ES;
        string _ES;

        public override string CN => _CN;

        public InterfaceConversionErrorTypes ErrorType { get; }

        string _CN;

        internal InterfaceConversionErrorBase(
                InterfaceConversionErrorTypes ErrorType,
                long NID,
                string CNT,
                string CN,
                string DE,
                string EN,
                string ES
            )
            : base(NID)
        {
            this.ErrorType = ErrorType;
            _CNT = CNT;
            _DE = DE;
            _EN = EN;
            _ES = ES;
            _CN = CN;
        }
    }
}
