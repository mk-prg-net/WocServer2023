using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Timeline
{
    /// <summary>
    /// Uhrzeit, zu der ein Ergeignis geschah
    /// </summary>
    public class TimeStamp : NamingBase
    {
        public const long UID = 0xD4621073;

        public TimeStamp()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => "time";
        public override string DE => "Zeitstempel";
        public override string EN => "Timestamp";
        public override string ES => "Sello de tiempo";
    }

    /// <summary>
    /// Datum, zu der ein Ergeignis geschah
    /// </summary>
    public class DateStamp : NamingBase
    {
        public const long UID = 0x83CA1743;

        public DateStamp()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => "date";
        public override string DE => "Datum";
        public override string EN => "Date";
        public override string ES => "Fecha";
    }

    /// <summary>
    /// Zeitraum von ...
    /// </summary>
    public class Period : NamingBase
    {
        public const long UID = 0x98DC7A44;

        public Period()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => "period";
        public override string DE => "Zeitraum";
        public override string EN => "Period";
        public override string ES => "Período";
    }


    /// <summary>
    /// Zeitraum von ...
    /// </summary>
    public class PeriodFrom : NamingBase
    {
        public const long UID = 0x5E2A2821;

        public PeriodFrom()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => "period_from";
        public override string DE => "Zeitraum von";
        public override string EN => "Period from";
        public override string ES => "El período desde";
    }

    /// <summary>
    /// Zeitraum bis ...
    /// </summary>
    public class PeriodTo : NamingBase
    {
        public const long UID = 0x5349C4F8;

        public PeriodTo()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => "period_to";
        public override string DE => "Zeitraum bis";
        public override string EN => "Period to";
        public override string ES => "período hasta";
    }

    /// <summary>
    /// Stunde
    /// </summary>
    public class Hour : NamingBase
    {
        public const long UID = 0x728E6FD6;

        public Hour()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => EN;
        public override string DE => "Stunde";
        public override string EN => "hour";
        public override string ES => "hora";
    }

    /// <summary>
    /// Minute
    /// </summary>
    public class Minute : NamingBase
    {
        public const long UID = 0x3FC522FC;

        public Minute()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => EN;
        public override string DE => "Minute";
        public override string EN => "minutes";
        public override string ES => "Minuto";
    }

    /// <summary>
    /// Sekunde
    /// </summary>
    public class Second : NamingBase
    {
        public const long UID = 0x6B52491E;

        public Second()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => EN;
        public override string DE => "Sekunde";
        public override string EN => "second";
        public override string ES => "Segundo";
    }

    /// <summary>
    /// Millisekunde
    /// </summary>
    public class Millisecond : NamingBase
    {
        public const long UID = 0x2644D385;

        public Millisecond()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => EN;
        public override string DE => "Millisekunde";
        public override string EN => "millisecond";
        public override string ES => "Milisegundo";
    }

    /// <summary>
    /// Tag
    /// </summary>
    public class Day : NamingBase
    {
        public const long UID = 0xCDB4AAB7;

        public Day()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => EN;
        public override string DE => "Tag";
        public override string EN => "day";
        public override string ES => "Día";
    }

    /// <summary>
    /// Tag
    /// </summary>
    public class Month : NamingBase
    {
        public const long UID = 0x54EA0BF;

        public Month()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => EN;
        public override string DE => "Monat";
        public override string EN => "month";
        public override string ES => "Mes";
    }

    /// <summary>
    /// Tag
    /// </summary>
    public class Year : NamingBase
    {
        public const long UID = 0x8982C173;

        public Year()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => EN;
        public override string DE => "Jahr";
        public override string EN => "year";
        public override string ES => "Año";
    }

    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class Current : NamingBase
    {
        public const long UID = 0x63E8433;

        public Current()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => "current";
        public override string DE => "aktuell";
        public override string EN => "current";
        public override string ES => "actual";
    }

    /// <summary>
    /// mko, 3.9.2020
    /// </summary>
    public class Old : NamingBase
    {
        public const long UID = 0xEBB63BFC;

        public Old()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => "old";
        public override string DE => "alte";
        public override string EN => "old";
        public override string ES => "antigua";
    }

    /// <summary>
    /// mko, 3.9.2020
    /// </summary>
    public class New : NamingBase
    {
        public const long UID = 0x85CC994B;

        public New()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => "new";
        public override string DE => "neue";
        public override string EN => "old";
        public override string ES => "nuevo";
    }




}
