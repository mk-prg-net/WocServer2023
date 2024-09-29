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

        public override string CN => "时间戳";
        public override string CNT => "time";
        public override string DE => "Zeitstempel";
        public override string EN => "Timestamp";
        public override string ES => "Sello de tiempo";

        public override string Glyph => Glyphs.DateAndTime.Time;

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

        public override string CN => "日期";
        public override string CNT => "date";
        public override string DE => "Datum";
        public override string EN => "Date";
        public override string ES => "Fecha";

        public override string Glyph => Glyphs.DateAndTime.Date;

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

        public override string CN => "时期";
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

        public override string CN => "期间开始";
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

        public override string CN => "期末";
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

        public override string CN => "小时";
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

        public override string CN => "分钟";
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

        public override string CN => "第二次";
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

        public override string CN => "毫秒";
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

        public override string CN => "日";
        public override string CNT => EN;
        public override string DE => "Tag";
        public override string EN => "day";
        public override string ES => "Día";

        public override string Glyph => Glyphs.DateAndTime.Day;

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

        public override string CN => "月份";
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

        public override string CN => "年份";
        public override string CNT => EN;
        public override string DE => "Jahr";
        public override string EN => "year";
        public override string ES => "Año";
    }

    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class Current : NamingBase, Grammar.Adjectives.IAdjective
    {
        public const long UID = 0x63E8433;

        public Current()
            : base(UID)
        { }

        public override string CN => "当前";
        public override string CNT => "current";
        public override string DE => "aktuell";
        public override string EN => "current";
        public override string ES => "actual";
    }

    /// <summary>
    /// mko, 3.9.2020
    /// </summary>
    public class Old : NamingBase, Grammar.Adjectives.IAdjective
    {
        public const long UID = 0xEBB63BFC;

        public Old()
            : base(UID)
        { }

        public override string CN => "旧的";
        public override string CNT => "old";
        public override string DE => "alte";
        public override string EN => "old";
        public override string ES => "antigua";
    }

    public class Older : NamingBase, Grammar.Adverbs.IAdverbTemporal
    {
        public const long UID = 0xCFDCD287;

        public Older()
            : base(UID)
        { }

        public override string CN => "较旧的";
        public override string CNT => "older";
        public override string DE => "älter";
        public override string EN => "older";
        public override string ES => "más antiguo";
    }

    public class OlderThan : NamingBase, Grammar.Adverbs.IAdverbTemporal
    {
        public const long UID = 0x872273FC;

        public OlderThan()
            : base(UID)
        { }

        public override string CN => "较旧的";
        public override string CNT => "olderThan";
        public override string DE => "älter als";
        public override string EN => "older than";
        public override string ES => "más antigua que";
    }

    // jung

    public class Young : NamingBase, Grammar.Adjectives.IAdjective
    {
        public const long UID = 0x48F0EE63;

        public Young()
            : base(UID)
        { }

        public override string CN => "年轻";
        public override string CNT => "young";
        public override string DE => "jung";
        public override string EN => "young";
        public override string ES => "joven";
    }

    public class Younger : NamingBase, Grammar.Adverbs.IAdverbTemporal
    {
        public const long UID = 0x1BC59D0B;

        public Younger()
            : base(UID)
        { }

        public override string CN => "更加年轻";
        public override string CNT => "younger";
        public override string DE => "jünger";
        public override string EN => "younger";
        public override string ES => "más joven";
    }

    public class YoungerThan : NamingBase, Grammar.Adverbs.IAdverbTemporal
    {
        public const long UID = 0x5E28A51C;

        public YoungerThan()
            : base(UID)
        { }

        public override string CN => "她比我小";
        public override string CNT => "youngerThan";
        public override string DE => "jünger als";
        public override string EN => "younger than";
        public override string ES => "más joven que";
    }




    /// <summary>
    /// mko, 3.9.2020
    /// </summary>
    public class New : NamingBase, Grammar.Adjectives.IAdjective
    {
        public const long UID = 0x85CC994B;

        public New()
            : base(UID)
        { }

        public override string CN => "新产品";
        public override string CNT => "new";
        public override string DE => "neue";
        public override string EN => "old";
        public override string ES => "nuevo";

        public override string Glyph => Glyphs.Access.New;

    }

    /// <summary>
    /// mko, 17.5.2021
    /// </summary>
    public class Yesterday : NamingBase, Grammar.Adverbs.IAdverb
    {
        public const long UID = 0xEFA5EE84;

        public Yesterday()
            : base(UID)
        { }

        public override string CN => "昨天";
        public override string CNT => "yesterday";
        public override string DE => "gestern";
        public override string EN => "yesterday";
        public override string ES => "ayer";

        public override string Glyph => Glyphs.DateAndTime.Date;
    }

    /// <summary>
    /// mko, 17.5.2021
    /// </summary>
    public class Today : NamingBase, Grammar.Adverbs.IAdverb
    {
        public const long UID = 0x65CECCD7;

        public Today()
            : base(UID)
        { }

        public override string CN => "今天";
        public override string CNT => "today";
        public override string DE => "heute";
        public override string EN => "today";
        public override string ES => "hoy";

        public override string Glyph => Glyphs.DateAndTime.Date;
    }

    public class Tomorrow : NamingBase, Grammar.Adverbs.IAdverb
    {
        public const long UID = 0x84DEC698;

        public Tomorrow()
            : base(UID)
        { }

        public override string CN => "明天";
        public override string CNT => "tomorrow";
        public override string DE => "morgen";
        public override string EN => "tomorrow";
        public override string ES => "mañana";

        public override string Glyph => Glyphs.DateAndTime.Date;
    }

    public class FirstTime : NamingBase, Grammar.Adverbs.IAdverb
    {
        public const long UID = 0x567D32F8;

        public FirstTime()
            : base(UID)
        { }

        public override string CN => "首次";
        public override string CNT => "firstTime";
        public override string DE => "zum ersten Mal";
        public override string EN => "for the first time";
        public override string ES => "por primera vez";

        public override string Glyph => Glyphs.Math.CircledNumbers.One;
    }

    public class MostRecently : NamingBase, Grammar.Adverbs.IAdverb
    {
        public const long UID = 0x715FBE32;

        public MostRecently()
            : base(UID)
        { }

        public override string CN => "最后一次";
        public override string CNT => "mostRecently";
        public override string DE => "zuletzt";
        public override string EN => "most recently";
        public override string ES => "último";

        public override string Glyph => Glyphs.Math.CircledNumbers.One;
    }



}
