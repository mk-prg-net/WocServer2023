using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Runtime.ATMO.DFC
{

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class ErrFatalErrorsDfcSessionStops
     : NamingBase
    {

        public const long UID = 0x6AE12AFF;

        public ErrFatalErrorsDfcSessionStops()
            : base(UID)
        {
        }

        public override string CNT => "errFatalErrorsClientStops";
        public override string CN => EN;
        public override string DE => "Aufgrund zuvor aufgetretender, schwerwiegender Fehler wurde die aktuelle DFC- Sitzung beendet.";
        public override string EN => "The current DFC session was terminated due to serious errors that occurred previously.";
        public override string ES => "El actual período de sesiones del DFC se dio por terminado debido a graves errores que se produjeron anteriormente.";
    }



    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class ErrStartOfMainWindowFailed
     : NamingBase
    {

        public const long UID = 0xFA8EE5A6;

        public ErrStartOfMainWindowFailed()
            : base(UID)
        {
        }

        public override string CNT => "errStartMainWindowFailed";
        public override string CN => EN;
        public override string DE => "Start des DFC2 Hauptfensters ist fehlgeschlagen!";
        public override string EN => "Start of the DFC2 main window has failed!";
        public override string ES => "¡El inicio de la ventana principal del DFC2 falló!";
    }
}
