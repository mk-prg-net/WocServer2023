using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// mko, 5.10.2020
/// Begriffe zum Thema Prozessbeobachtung, Soll/Ist Vergleich
/// </summary>
namespace MKPRG.Naming.TechTerms.Monitoring
{
    /// <summary>
    /// mko, 5.10.2020
    /// Sollwert
    /// </summary>
    public class SetPoint : NamingBase
    {
        public const long UID = 0x6D2A593E;

        public SetPoint()
            : base(UID)
        {
        }

        public override string CNT => "setPoint";
        public override string CN => "设定点";
        public override string DE => "Sollwert";
        public override string EN => "Setpoint";
        public override string ES => "punto de ajuste";
    }

    /// <summary>
    /// mko, 5.10.2020
    /// Istwert
    /// </summary>
    public class ActualValue : NamingBase
    {
        public const long UID = 0xC77D5BEF;

        public ActualValue()
            : base(UID)
        {
        }

        public override string CNT => "actualValue";
        public override string CN => "实际价值";
        public override string DE => "Ist-Wert";
        public override string EN => "Actual Value";
        public override string ES => "Valor real";
    }

    /// <summary>
    /// mko, 7.10.2020
    /// Logbuch
    /// </summary>
    public class LogBook : NamingBase
    {
        public const long UID = 0xCC84FD5B;

        public LogBook()
            : base(UID)
        {
        }

        public override string CNT => "logBook";
        public override string CN => "日志";
        public override string DE => "Logbuch";
        public override string EN => "Logbook";
        public override string ES => "Cuaderno de bitácora";
    }

    /// <summary>
    /// mko, 7.10.2020
    /// Logbuch
    /// </summary>
    public class LogEntry : NamingBase
    {
        public const long UID = 0xB540C4CC;

        public LogEntry()
            : base(UID)
        {
        }

        public override string CNT => "logEntry";
        public override string CN => "日志条目";
        public override string DE => "Logbucheintrag";
        public override string EN => "Log entry";
        public override string ES => "Entrada en la bitácora";
    }
}

