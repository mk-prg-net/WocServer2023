using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Monitoring.LogTypes
{
    /// <summary>
    /// Stellt Kompatibilität mit den im April 2018
    /// mit Joachim vereinbarten Typen von Logmeldungen.
    /// </summary>

    public class Error : NamingBase
    {
        public const long UID = 0xA0E4577C;

        public Error()
            : base(UID)
        {
        }

        public override string CNT => "error";
        public override string CN => "误差";
        public override string DE => "Fehler";
        public override string EN => "Error";
        public override string ES => "Error";

        public override string Glyph => Glyphs.Events.Error;
    }

    public class Info : NamingBase
    {
        public const long UID = 0xC28BDFB9;

        public Info()
            : base(UID)
        {
        }

        public override string CNT => "info";
        public override string CN => "信息";
        public override string DE => "Information";
        public override string EN => "Information";
        public override string ES => "Información";        

        public override string Glyph => Glyphs.Events.Info;
    }

    public class Status : NamingBase
    {
        public const long UID = 0x51D0AC44;

        public Status()
            : base(UID)
        {
        }

        public override string CNT => "status";
        public override string CN => "状况";
        public override string DE => "Status";
        public override string EN => "Information";
        public override string ES => "Estado";

        public override string Glyph => Glyphs.Events.Status;
    }

    public class Log : NamingBase
    {
        public const long UID = 0xAD9A2576;

        public Log()
            : base(UID)
        {
        }

        public override string CNT => "log";
        public override string CN => "纪录";
        public override string DE => "Log";
        public override string EN => "Log";
        public override string ES => "Registro";
        public override string Glyph => Glyphs.DataAndDocuments.Scroll;
    }

    public class Telemetry : NamingBase
    {
        public const long UID = 0xD1892247;

        public Telemetry()
            : base(UID)
        {
        }

        public override string CNT => "telemetry";
        public override string CN => "遥测";
        public override string DE => "Telemetrie";
        public override string EN => "Telemetry";
        public override string ES => "Telemetría";

        public override string Glyph => Glyphs.Communication.Send;
    }

}
