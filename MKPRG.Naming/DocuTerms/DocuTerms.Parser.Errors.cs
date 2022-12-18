using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.DocuTerms.Parser.Errors
{
    public class NamedTermExpected
        : NamingBase
    {
        public const long UID = 0x8BF2AC77;

        public NamedTermExpected()
            : base(UID)
        { }

        public override string CNT => "namedTermExpected";

        public override string DE => "Ein benannter Dokuterm wie eine Instanz, eine Eigenschaft oder einer Methode wurden erwartet";

        public override string EN => "A named docuterm such as an instance, a property or a method was expected.";

        public override string ES => "Se esperaba un docuterm con nombre como una instancia, una propiedad o un método.";

        public override string CN => "一个命名的docuterm，如一个实例，一个属性或一个方法，被认为是";
    }


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

    public class InstanceExpected
        : NamingBase
    {
        public const long UID = 0x8D2628A1;

        public InstanceExpected()
            : base(UID)
        { }

        public override string CNT => "instanceExpected";

        public override string DE => "Eine Instanz wurde erwartet.";
                                    

        public override string EN => "Instance expected.";

        public override string ES => "Instancia esperada.";

        public override string CN => "实例预期。";
    }

    public class MethodExpected
    : NamingBase
    {
        public const long UID = 0x74DC4877;

        public MethodExpected()
            : base(UID)
        { }

        public override string CNT => "methodExpected";

        public override string DE => "Eine Methode/Funktion wurde erwartet.";


        public override string EN => "A method/function was expected.";

        public override string ES => "Se esperaba un método/función.";

        public override string CN => "预计会有一种方法/功能。";
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

        public override string Glyph => Glyphs.Validation.Invalid;
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

        public override string Glyph => Glyphs.Validation.Invalid;
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

        public override string Glyph => Glyphs.Validation.Invalid;
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

        public override string Glyph => Glyphs.Validation.Invalid;
    }

    public class Event_EventParameterAsTextExpected
    : NamingBase
    {
        public const long UID = 0xC4E3BE95;

        public Event_EventParameterAsTextExpected()
            : base(UID)
        { }

        public override string CNT => "eventParameterAsTextExpected";

        public override string DE => "Zu einem Ereignis wurde eine textuelle beschreibung der Ursache erwartet.";

        public override string EN => "A textual description of the cause of an event was expected.";

        public override string ES => "Se esperaba una descripción textual de la causa de un evento.";

        public override string CN => "希望能对事件的起因进行文字描述。";

        public override string Glyph => Glyphs.Validation.Invalid;
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

        public override string Glyph => Glyphs.Validation.Invalid;
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

        public override string Glyph => Glyphs.Validation.Invalid;
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

        public override string Glyph => Glyphs.Validation.Invalid;
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

        public override string Glyph => Glyphs.Validation.Invalid;
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

        public override string Glyph => Glyphs.Validation.Invalid;
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

        public override string Glyph => Glyphs.Validation.Invalid;
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

        public override string Glyph => Glyphs.Validation.Invalid;
    }

    public class BooleanExpected
        : NamingBase
    {
        public const long UID = 0x1C18752C;

        public BooleanExpected()
            : base(UID)
        { }

        public override string CNT => EN;

        public override string DE => "Ein Boolean wurde erwartet.";
        public override string EN => "An Boolean was expected.";
        public override string ES => "Se esperaba un boolean entero.";
        public override string CN => EN;

        public override string Glyph => Glyphs.Validation.Invalid;
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

        public override string Glyph => Glyphs.Validation.Invalid;
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

        public override string Glyph => Glyphs.Validation.Invalid;
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
        public override string Glyph => Glyphs.Validation.Invalid;
    }


    //at least name and one value expected

    public class NameValuePairExpected
        : NamingBase
    {
        public const long UID = 0xCE30F5F1;

        public NameValuePairExpected()
            : base(UID)
        { }

        public override string CNT => "nameValuePairExpected";
        public override string DE => "Ein Paar aus einem Namen und einen Wert wurden erwartet.";
        public override string EN => "At least name and one value expected.";
        public override string ES => "Se espera al menos un nombre y un valor.";
        public override string CN => "至少要有一个名称和一个数值";
        public override string Glyph => Glyphs.Validation.Invalid;
    }

    public class ParseRCfromDocuTerm
: NamingBase
    {
        public const long UID = 0x5A90C597;

        public ParseRCfromDocuTerm()
            : base(UID)
        { }

        public override string CNT => "parseRCV3fromDocuTerm";
        public override string DE => "Fehler beim Parsen eines RCV3 aus einem DocuTerm";
        public override string EN => "Error when parsing an RCV3 from a DocuTerm.";
        public override string ES => "Error al analizar un RCV3 de un DocuTerm";
        public override string CN => "从DocuTerm解析RCV3时出错。";
    }

    public class ParseRCfromDocuTerm_LogDateMissing
        : NamingBase
    {
        public const long UID = 0xA8FEDCCA;

        public ParseRCfromDocuTerm_LogDateMissing()
            : base(UID)
        { }

        public override string CNT => "parseRCV3fromDocuTerm_LogDateMissing";
        public override string DE => "Fehler beim Parsen eines RCV3 aus einem DocuTerm: Das Log Datum fehlt.";
        public override string EN => "Error when parsing an RCV3 from a DocuTerm: The log date is missing.";
        public override string ES => "Error al analizar un RCV3 de un DocuTerm: Falta la fecha del registro.";
        public override string CN => "从DocuTerm解析RCV3时出错。日志日期不见了。";
    }




    /// <summary>
    /// mko, 15.3.2021
    /// Parsen eines RCV3 aus einem dokuTerm.
    /// </summary>
    public class ParseRCfromDocuTerm_BaseStructureInstanceMethodReturnExpected
        : NamingBase
    {
        public const long UID = 0x4E967;

        public ParseRCfromDocuTerm_BaseStructureInstanceMethodReturnExpected()
            : base(UID)
        { }

        public override string CNT => "parseRCV3fromDocuTerm_BaseStructureInstanceMethodReturnExpected";
        public override string DE => "Fehler beim Parsen eines RCV3 aus einem DocuTerm: Es wird der Aufbau aus Instanz-Methode-Rückgabewert erwartet, jedoch nicht vorgefunden.";
        public override string EN => "Error when parsing an RCV3 from a DocuTerm: The structure from instance method return value is expected, but not found.";
        public override string ES => "Error al analizar un RCV3 de un DocuTerm: Se espera la estructura del valor de retorno del método de instancia, pero no se encuentra.";
        public override string CN => "从DocuTerm解析RCV3时出错。从实例方法返回的结构值是预期的，但没有找到。";
    }

    /// <summary>
    /// mko, 15.3.2021
    /// </summary>
    public class ParseRCfromDocuTerm_InstanceNameDoesNotContainAssemblyAndClassName
    : NamingBase
    {
        public const long UID = 0x7A2ED370;

        public ParseRCfromDocuTerm_InstanceNameDoesNotContainAssemblyAndClassName()
            : base(UID)
        { }

        public override string CNT => "parseRCV3fromDocuTerm_InstanceNameDoesNotContainAssemblyAndClassName";
        public override string DE => "Fehler beim Parsen eines RCV3 aus einem DocuTerm: Der Instanzname folgt nicht dem Aufbau *Assemblyname*.*Classname*";
        public override string EN => "Error when parsing an RCV3 from a DocuTerm: The instance name does not follow the structure *Assemblyname*.*Classname*.";
        public override string ES => "Error al analizar un RCV3 de un DocuTerm: El nombre de la instancia no sigue la estructura *Nombre del instance*.*Nombre de la class*.";
        public override string CN => "从DocuTerm解析RCV3时出错。实例名称不遵循*Assemblyname*.*Classname*的结构。";
    }

    public class ParseRCfromDocuTerm_InstanceNameIsIncomplete
        : NamingBase
    {
        public const long UID = 0x4EDDED75;

        public ParseRCfromDocuTerm_InstanceNameIsIncomplete()
            : base(UID)
        { }

        public override string CNT => "parseRCV3fromDocuTerm_InstanceNameIsIncomplete";
        public override string DE => "Fehler beim Parsen eines RCV3 aus einem DocuTerm: ist unvollständig!";
        public override string EN => "Error parsing an RCV3 from a DocuTerm: is incomplete!";
        public override string ES => "Error al analizar un RCV3 de un DocuTerm: ¡está incompleto!";
        public override string CN => "从DocuTerm解析RCV3时出错：不完整!";
    }
}
