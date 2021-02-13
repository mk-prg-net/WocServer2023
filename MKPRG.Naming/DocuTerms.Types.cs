using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.DocuTerms.Types
{
    /// <summary>
    /// mko, 24.6.2020
    /// </summary>
    public class Instance
        : NamingBase
    {
        public const long UID = 0x28CEB803;

        public Instance()
            : base(UID)
        { }

        public override string CNT => "Instance";

        public override string DE => "Instanz";

        public override string EN => CNT;

        public override string ES => "Instancia";

        public override string CN => "实例";

        public override string Glyph => Glyphs.DocuTerms.Instance;
    }

    /// <summary>
    /// mko, 24.6.2020
    /// </summary>
    public class Method
        : NamingBase
    {
        public const long UID = 0x19909587;

        public Method()
            : base(UID)
        { }

        public override string CNT => "Method";

        public override string DE => "Methode";

        public override string EN => CNT;

        public override string ES => "método";

        public override string CN => "办法";

        public override string Glyph => Glyphs.DocuTerms.Method;
    }


    /// <summary>
    /// mko, 24.6.2020
    /// </summary>
    public class Event
        : NamingBase
    {
        public const long UID = 0xF0C1BAC9;

        public Event()
            : base(UID)
        { }

        public override string CNT => "Event";

        public override string DE => "Ereignis";

        public override string EN => CNT;

        public override string ES => "Evento";

        public override string CN => "事件";

        public override string Glyph => Glyphs.DocuTerms.Event;
    }

    /// <summary>
    /// mko, 24.6.2020
    /// </summary>
    public class Property
        : NamingBase
    {
        public const long UID = 0xD16F1FC3;

        public Property()
            : base(UID)
        { }

        public override string CNT => "property";

        public override string DE => "Eigenschaft";

        public override string EN => "Property";

        public override string ES => "Propiedad";

        public override string CN => "财产";

        public override string Glyph => Glyphs.DocuTerms.Property;
    }

    /// <summary>
    /// mko, 25.6.2020
    /// </summary>
    public class PropertySet
        : NamingBase
    {
        public const long UID = 0xC584CA41;

        public PropertySet()
            : base(UID)
        { }

        public override string CNT => "propertySet";

        public override string DE => CNT;

        public override string EN => CNT;

        public override string ES => CNT;

        public override string CN => CNT;
    }


    /// <summary>
    /// mko, 24.6.2020
    /// </summary>
    public class Return
        : NamingBase
    {
        public const long UID = 0xC237F683;

        public Return()
            : base(UID)
        { }

        public override string CNT => "methodRreturn";

        public override string DE => "Rücksprung";

        public override string EN => "return";

        public override string ES => "devolver";

        public override string CN => "返回";

        public override string Glyph => Glyphs.DocuTerms.Return;
    }

    /// <summary>
    /// mko, 24.6.2020
    /// </summary>
    public class List
        : NamingBase
    {
        public const long UID = 0x1D7D7A22;

        public List()
            : base(UID)
        { }

        public override string CNT => "list";

        public override string DE => "Liste";

        public override string EN => "List";

        public override string ES => "Lista";

        public override string CN => "列表";
    }


    /// <summary>
    /// mko, 24.6.2020
    /// </summary>
    public class Text
        : NamingBase
    {
        public const long UID = 0xCCA8FF09;

        public Text()
            : base(UID)
        { }

        public override string CNT => "text";

        public override string DE => "Text";

        public override string EN => "Text";

        public override string ES => "Texto";

        public override string CN => "案文";
    }

    /// <summary>
    /// mko, 24.6.2020
    /// </summary>
    public class NID
        : NamingBase
    {
        public const long UID = 0x97868948;

        public NID()
            : base(UID)
        { }

        public override string CNT => "Naming ID";

        public override string DE => CNT;

        public override string EN => CNT;

        public override string ES => CNT;

        public override string CN => CNT;
    }

    /// <summary>
    /// mko, 25.6.2020
    /// </summary>
    public class Time
        : NamingBase
    {
        public const long UID = 0x82624960;

        public Time()
            : base(UID)
        { }

        public override string CNT => "time";

        public override string DE => "Zeit";

        public override string EN => "Time";

        public override string ES => "Tiempo";

        public override string CN => "时间";

        public override string Glyph => Glyphs.DateAndTime.Time;
    }

    /// <summary>
    /// mko, 25.6.2020
    /// </summary>
    public class Date
        : NamingBase
    {
        public const long UID = 0x13B7CFF3;

        public Date()
            : base(UID)
        { }

        public override string CNT => "date";

        public override string DE => "Datum";

        public override string EN => "Date";

        public override string ES => "Fecha";

        public override string CN => "日期";

        public override string Glyph => Glyphs.DateAndTime.Date;
    }

    /// <summary>
    /// mko, 25.6.2020
    /// </summary>
    public class Version
        : NamingBase
    {
        public const long UID = 0x344611F7;

        public Version()
            : base(UID)
        { }

        public override string CNT => "Version";

        public override string DE => CNT;

        public override string EN => CNT;

        public override string ES => CNT;

        public override string CN => CNT;
    }

    /// <summary>
    /// mko, 25.6.2020
    /// </summary>
    public class Name
        : NamingBase
    {
        public const long UID = 0x3CCB6574;

        public Name()
            : base(UID)
        { }

        public override string CNT => "name";

        public override string DE => "Name";

        public override string EN => "Name";

        public override string ES => "Nombre";

        public override string CN => "名称";


    }

    /// <summary>
    /// mko, 25.6.2020
    /// </summary>
    public class WildCard
        : NamingBase
    {
        public const long UID = 0x52301BB4;

        public WildCard()
            : base(UID)
        { }

        public override string CNT => "WildCard";

        public override string DE => CNT;

        public override string EN => CNT;

        public override string ES => CNT;

        public override string CN => CNT;
    }



}
