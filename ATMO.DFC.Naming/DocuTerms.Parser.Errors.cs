using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.DocuTerms.Parser.Errors
{
    public class Name_NidOrStringTokenForNameExpected
    : NamingBase
    {
        public const long UID = 0xD66A72A1;

        public Name_NidOrStringTokenForNameExpected()
            : base(UID)
        { }

        public override string CNT => EN;

        public override string DE => "Ein DokuTerm- Name muss durch einen String oder einer NID (Naming- ID) dargestellt werden.";

        public override string EN => "A DocuTerm name must be represented by a string or an NID (Naming ID).";

        public override string ES => "El nombre de un DocuTerm debe ser representado por una cadena o un NID (Naming ID).";

        public override string CN => "DocuTerm名称必须用字符串或NID（命名ID）表示。";
    }



    public class Instance_NotAllChildsAreInstanceMembers
        : NamingBase
    {
        public const long UID = 0x73E3C3BF;

        public Instance_NotAllChildsAreInstanceMembers()
            : base(UID)
        { }

        public override string CNT => EN;

        public override string DE => "Nicht alle Kindelemente eines Instanz- Knotens sind zulässige Instanzmitglieder. "
                                    + "Nur Eigenschaften, Methoden und Ereignisse sind als Instanzmember zugelassen.";

        public override string EN => "Not all child elements of an instance node are valid instance members. Only properties, methods, and events are allowed as instance members.";

        public override string ES => "No todos los elementos hijos de un nodo de instancia son miembros válidos de la instancia. Sólo las propiedades, métodos y eventos están permitidos como miembros de la instancia.";

        public override string CN => "并非一个实例节点的所有子元素都是有效的实例成员。只有属性、方法和事件才允许作为实例成员。";
    }

    public class Method_NotAllChildsAreMethodMembers
    : NamingBase
    {
        public const long UID = 0x9F511BA9;

        public Method_NotAllChildsAreMethodMembers()
            : base(UID)
        { }

        public override string CNT => EN;

        public override string DE => "Nicht alle Kindelemente eines Methoden- Knotens sind zulässige Methodenparameter. "
                                    + "Nur Eigenschaften, Ereignisse und Returns sind als Methodenparameter zugelassen.";

        public override string EN => "Not all child elements of a method node are valid method parameters.  Only properties, events, and returns are allowed as method parameters.";

        public override string ES => "No todos los elementos hijos de un nodo de método son parámetros de método válidos.  Sólo las propiedades, eventos y retornos están permitidos como parámetros del método.";

        public override string CN => "并非一个方法节点的所有子元素都是有效的方法参数。 只有属性、事件和返回才允许作为方法参数。";
    }

    public class Property_ChildIsNotValidPropertyValue
        : NamingBase
    {
        public const long UID = 0x37797C28;

        public Property_ChildIsNotValidPropertyValue()
            : base(UID)
        { }

        public override string CNT => EN;

        public override string DE => "Das Kindelemente einer Eigenschaft ist kein gültiger Eigenschaftswert. Returns sind z.B. keine gültigen Eigenschaftswerte.";        

        public override string EN => "The child element of a property is not a valid property value. For example, returns are not valid property values.";

        public override string ES => "The child element of a property is not a valid property value. For example, returns are not valid property values.";

        public override string CN => "属性的子元素不是有效的属性值。例如，返回不是有效的属性值。";
    }

    public class Return_ReturnValueAsChildExpected
        : NamingBase
    {
        public const long UID = 0x3A6552E7;

        public Return_ReturnValueAsChildExpected()
            : base(UID)
        { }

        public override string CNT => EN;

        public override string DE => "Eine Methodenrückgabe muss einen gültigen Methodenrückgabewert als Kind haben.";

        public override string EN => "A method return must have a valid method return value as child.";

        public override string ES => "Una devolución de método debe tener un valor de devolución de método válido como hijo.";

        public override string CN => "一个方法返回必须有一个有效的方法返回值作为子方法。";
    }

    public class Event_EventParameterAsChildExpected
    : NamingBase
    {
        public const long UID = 0x642384D8;

        public Event_EventParameterAsChildExpected()
            : base(UID)
        { }

        public override string CNT => EN;

        public override string DE => "Ein Ereignis muss einen gültigen Ereignisparameter als Kind haben.";

        public override string EN => "An event must have a valid event parameter as child.";

        public override string ES => "Un evento debe tener un parámetro de evento válido de niño.";

        public override string CN => "一个事件必须有一个有效的事件参数作为子事件。";
    }


    public class List_NotAllChildsAreListMembers
    : NamingBase
    {
        public const long UID = 0x42195170;

        public List_NotAllChildsAreListMembers()
            : base(UID)
        { }

        public override string CNT => EN;

        public override string DE => "Nicht alle Kindelemente einer Liste sind gültige Listeneinträge.";

        public override string EN => "Not all child elements of a list are valid list entries.";

        public override string ES => "No todos los elementos hijos de una lista son entradas válidas de la lista.";

        public override string CN => "并非一个列表的所有子元素都是有效的列表项。";
    }


    public class NID_IntTokenExpected
        : NamingBase
    {
        public const long UID = 0x3968FDB8;

        public NID_IntTokenExpected()
            : base(UID)
        { }

        public override string CNT => EN;

        public override string DE => "Namenskonstanten (NID) müssen als Kindelement einen Integer mit dem Wert der Namenskonstante enthalten.";

        public override string EN => "Name constants (NID) must contain an integer with the value of the name constant as child element.";

        public override string ES => "Las constantes del nombre (NID) deben contener un número entero con el valor de la constante del nombre como elemento hijo.";

        public override string CN => "名称常量(NID)必须包含一个整数，名称常量的值作为子元素。";
    }

    public class Txt_SimpleValueAsPartExpected
    : NamingBase
    {
        public const long UID = 0xAFF9CED4;

        public Txt_SimpleValueAsPartExpected()
            : base(UID)
        { }

        public override string CNT => EN;

        public override string DE => "Ein einfacher Wert wie String, Zahl oder Datum als Textpartikel wurde erwartet";

        public override string EN => "A simple value like string, number or date as text particle was expected.";

        public override string ES => "Se esperaba un valor simple como una cadena, un número o una fecha como partícula de texto.";

        public override string CN => "期待一个简单的值，如字符串、数字或日期作为文本颗粒。";
    }

    public class Date_DateParticleExpected
        : NamingBase
    {
        public const long UID = 0xE5108B85;

        public Date_DateParticleExpected()
            : base(UID)
        { }

        public override string CNT => EN;

        public override string DE => "Zu einem Datum fehlt der erforderliche Datumspartikel wie Tag, Monat oder Jahr.";

        public override string EN => "The required date particle such as day, month or year is missing for a date.";

        public override string ES => "Falta la partícula de fecha requerida para una fecha, como el día, el mes o el año.";

        public override string CN => "一个日期缺少所需的日期颗粒，如日、月或年。";
    }

    public class Time_TimeParticleExpected
    : NamingBase
    {
        public const long UID = 0xF6B9FBA7;

        public Time_TimeParticleExpected()
            : base(UID)
        { }

        public override string CNT => EN;

        public override string DE => "Zu einem Zeitstempel fehlt der erforderliche Partikel wie Stunde, Minute oder Sekunde.";

        public override string EN => "For a time stamp the required particle such as hour, minute or second is missing.";

        public override string ES => "Para un sello de tiempo la partícula requerida como la hora, el minuto o el segundo falta.";

        public override string CN => "对于时间戳，缺少所需的粒子，如时、分、秒。";
    }

    public class Version_VersionNoAsStringExpected
        : NamingBase
    {
        public const long UID = 0x719FB240;

        public Version_VersionNoAsStringExpected()
            : base(UID)
        { }

        public override string CNT => EN;

        public override string DE => "Die Versionsnummer wird als String in einer Versionsnummerdefinition erwartet.";

        public override string EN => "The version number is expected as a string in a version number definition.";

        public override string ES => "El número de versión se espera como una cadena en una definición de número de versión.";

        public override string CN => "在版本号定义中，版本号应该是一个字符串。";
    }


    public class WildCard_ParameterMustBeAnComplexDocuTermAndNotASimpleValue
        : NamingBase
    {
        public const long UID = 0x4C172CF3;

        public WildCard_ParameterMustBeAnComplexDocuTermAndNotASimpleValue()
            : base(UID)
        { }

        public override string CNT => EN;

        public override string DE => "Wenn in einem Wildcard eine Einschränkung für den Inhalt definiert wird, dann muss diese ein DocuTerm sein. Aktuell wurde ein nicht- DocuTerm (z.B. Bool oder Integer )als Einschränkung gefunden.";

        public override string EN => "If a content restriction is defined in a wildcard, it must be a DocuTerm. Currently a non-DocuTerm (e.g. Bool or Integer )was found as a constraint.";

        public override string ES => "Si se define una restricción de contenido en un comodín, debe ser un DocuTerm. Actualmente se encontró un no-DocuTerm (por ejemplo, Bool o Integer ) como una restricción.";

        public override string CN => "如果内容限制是用通配符定义的，它必须是一个DocuTerm。目前发现一个非DocuTerm(如Bool或Integer)作为约束条件。";
    }


    public class IntegerExpected
    : NamingBase
    {
        public const long UID = 0x4EE27D47;

        public IntegerExpected()
            : base(UID)
        { }

        public override string CNT => EN;

        public override string DE => "Ein Integer wurde erwartet.";

        public override string EN => "An integer was expected.";

        public override string ES => "Se esperaba un número entero.";

        public override string CN => "预计是一个整数。";
    }

    public class DoubleExpected
        : NamingBase
    {
        public const long UID = 0x32F7110E;

        public DoubleExpected()
            : base(UID)
        { }

        public override string CNT => EN;

        public override string DE => "Ein Double wurde erwartet.";

        public override string EN => "An double was expected.";

        public override string ES => "Se esperaba un doble.";

        public override string CN => "预计会有一个双倍。";
    }


    public class StringExpected
    : NamingBase
    {
        public const long UID = 0xBF59ACC2;

        public StringExpected()
            : base(UID)
        { }

        public override string CNT => EN;

        public override string DE => "Ein String wurde erwartet.";

        public override string EN => "An string was expected.";

        public override string ES => "Se esperaba un string.";

        public override string CN => "预计会有一串。";
    }




}
