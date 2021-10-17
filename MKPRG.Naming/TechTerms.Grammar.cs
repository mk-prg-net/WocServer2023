using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Grammar
{
    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Subject
        : NamingBase
    {

        public const long UID = 0xEA9EC638;

        public Subject()
            : base(UID)
        {
        }

        public override string CNT => "subject";
        public override string CN => "课题";
        public override string DE => "Subjekt";
        public override string EN => "Subject";
        public override string ES => "Asunto";
    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Object
        : NamingBase
    {

        public const long UID = 0x3A9DAAFB;

        public Object()
            : base(UID)
        {
        }

        public override string CNT => "object";
        public override string CN => "对象";
        public override string DE => "Objekt";
        public override string EN => "Object";
        public override string ES => "Objeto";
    }

    /// <summary>
    /// mko, 14.4.2021
    /// 
    /// Objekte als Merkmale einer Entität/eines Objekts. 
    /// z.B.
    /// ... Details zur Dateiablage...
    /// ... Sicherheitsmerkmale der Zeichnung ...
    /// ... Anschrift des Benutzers
    /// 
    /// </summary>
    public class Attribute
        : NamingBase
    {

        public const long UID = 0x658FF89;

        public Attribute()
            : base(UID)
        {
        }

        public override string CNT => "attribute";
        public override string CN => "属性";
        public override string DE => "Attribut";
        public override string EN => "Attribute";
        public override string ES => "Atributo";
    }


    public interface IVerb
        : INaming
    {

    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Verb
        : NamingBase
    {

        public const long UID = 0x6B447D49;

        public Verb()
            : base(UID)
        {
        }

        public override string CNT => "verb";
        public override string CN => "动词";
        public override string DE => "Verb";
        public override string EN => "Verb";
        public override string ES => "Verbo";
    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Preposition
        : NamingBase
    {

        public const long UID = 0x856D3CC5;

        public Preposition()
            : base(UID)
        {
        }

        public override string CNT => "prepos";
        public override string CN => "介词";
        public override string DE => "Präposition";
        public override string EN => "Preposition";
        public override string ES => "Preposición";
    }

    /// <summary>
    /// Adverbiale Bestimmung
    /// </summary>
    public class AdverbialClause
    : NamingBase
    {

        public const long UID = 0x55F99EB3;

        public AdverbialClause()
            : base(UID)
        {
        }

        public override string CNT => "adverbialClause";
        public override string CN => "副词句";
        public override string DE => "adverbiale Bestimmung";
        public override string EN => "adverbial clause";
        public override string ES => "cláusula adverbial";
    }





    /// <summary>
    /// mko, 6.4.2021
    /// Aussagesatz
    /// </summary>
    public class Statement
            : NamingBase
    {

        public const long UID = 0xC61776BE;

        public Statement()
            : base(UID)
        {
        }

        public override string CNT => "statement";
        public override string CN => "声明";
        public override string DE => "Aussage";
        public override string EN => "Statement";
        public override string ES => "Declaración";

        public override string Glyph => ".";
    }

    /// <summary>
    /// mko, 6.2.2021
    /// Markiert einen Naming- Term als Beschreibung eines abgeschlossenen Prozesses
    /// </summary>
    public interface IFinishedActivity
        : INaming, IVerb
    { }

    public class FinishedActivity
        : NamingBase
    {

        public const long UID = 0xE3A76615;

        public FinishedActivity()
            : base(UID)
        {
        }

        public override string CNT => "finishedActivity";
        public override string CN => "actividad terminada";
        public override string DE => "beendete Aktivität";
        public override string EN => "finished Activity";
        public override string ES => "actividad terminada";

        public override string Glyph => ".";
    }

    /// <summary>
    /// mko, 10.5.2021
    /// </summary>
    public class FinishedActivityConversationError
        : InterfaceConversionErrorBase,
        IFinishedActivity
    {
        public FinishedActivityConversationError
            (
                InterfaceConversionErrorTypes ErrorType,
                long NIDofInProgressActivity,
                string CNT,
                string CN,
                string DE,
                string EN,
                string ES
            ) : base(ErrorType, NIDofInProgressActivity, CNT, CN, DE, EN, ES) { }
    }

    /// <summary>
    /// mko, 18.5.2021
    /// Markiert eine Aktivität in der Zukunft.
    /// </summary>
    public interface IFutureActivity
    : IVerb, INaming
    { }


    public class FutureActivity
        : NamingBase
    {

        public const long UID = 0x3B7CF331;

        public FutureActivity()
            : base(UID)
        {
        }

        public override string CNT => "futureActivity";
        public override string CN => "未来的行动";
        public override string DE => "zukünftige Handlung";
        public override string EN => "future activity";
        public override string ES => "acción futura";

        public override string Glyph => ".";
    }

    /// <summary>
    /// mko, 10.5.2021
    /// Dynamisch erstellter Namenscontainer, um einen Fehler beim Aufbau eines Satzes zu beschreiben
    /// </summary>
    public class FutureActivityConversationError
        : InterfaceConversionErrorBase,
        IFutureActivity
    {
        public FutureActivityConversationError
            (
                InterfaceConversionErrorTypes ErrorType,
                long NIDofInFutureActivity,
                string CNT,
                string CN,
                string DE,
                string EN,
                string ES
            ) : base(ErrorType, NIDofInFutureActivity, CNT, CN, DE, EN, ES) { }
    }



    /// <summary>
    /// Markiert einen Naming- Term als Beschreibung eines aktiven Prozesses
    /// </summary>
    public interface IInProgressActivity
        : IVerb, INaming
    { }

    public class InProgressActivity
        : NamingBase
    {

        public const long UID = 0x31975E43;

        public InProgressActivity()
            : base(UID)
        {
        }

        public override string CNT => "inProgressActivity";
        public override string CN => "活动中";
        public override string DE => "laufende Tätigkeit";
        public override string EN => "in progress activity";
        public override string ES => "actividad en curso";

        public override string Glyph => ".";
    }

    /// <summary>
    /// mko, 10.5.2021
    /// Dynamisch erstellter Namenscontainer, um einen FEhler beim Aufbau eines Satzes zu beschreiben
    /// </summary>
    public class InProgressActivityConversationError
        : InterfaceConversionErrorBase,
        IInProgressActivity
    {
        public InProgressActivityConversationError
            (
                InterfaceConversionErrorTypes ErrorType,
                long NIDofInProgressActivity,
                string CNT,
                string CN,
                string DE,
                string EN,
                string ES
            ) : base(ErrorType, NIDofInProgressActivity, CNT, CN, DE, EN, ES) { }
    }

    /// <summary>
    /// mko, 6.4.2021
    /// Fragesatz
    /// </summary>
    public class Question
            : NamingBase
    {

        public const long UID = 0xDB6325DF;

        public Question()
            : base(UID)
        {
        }

        public override string CNT => "question";
        public override string CN => "问题";
        public override string DE => "Frage";
        public override string EN => "Question";
        public override string ES => "Pregunta";

        public override string Glyph => "?";
    }

    /// <summary>
    /// mko, 6.5.2021
    /// Definiert Docuterm- Strukturen, die Aufzählungen von Objekten darstellt
    /// </summary>
    public class EnumerationOfObjects
        : NamingBase
    {

        public const long UID = 0x1D972C0A;

        public EnumerationOfObjects()
            : base(UID)
        {
        }

        public override string CNT => "enumerationOfObjects";
        public override string CN => "对象的列举";
        public override string DE => "Aufzählung von Objekten";
        public override string EN => "Enumeration of objects";
        public override string ES => "Enumeración de objetos";

        public override string Glyph => Glyphs.Math.Sets.Ellipsis;
    }

    /// <summary>
    /// mko, 7.5.2021
    /// Definiert Docutermstrukturen, die Objekte in der Pluralform ausdrücken
    /// </summary>
    public class ObjectsInPluralForm
        : NamingBase
    {

        public const long UID = 0x9E6A0FFA;

        public ObjectsInPluralForm()
            : base(UID)
        {
        }

        public override string CNT => "objectsInPluralForm";
        public override string CN => "对象的列举";
        public override string DE => "Objekte Plural";
        public override string EN => "Objects in PluralForm";
        public override string ES => "Objetos en plural";

        public override string Glyph => Glyphs.Math.Sets.Ellipsis;
    }

    public class ObjectsInRelationship
    : NamingBase
    {

        public const long UID = 0xE25DC752;

        public ObjectsInRelationship()
            : base(UID)
        {
        }

        public override string CNT => "objectsInRelationship";
        public override string CN => "关系中的对象";
        public override string DE => "Objekte in einer Beziehung";
        public override string EN => "Objects in a relationship";
        public override string ES => "Objetos en una relación";

        public override string Glyph => Glyphs.Math.Functions.Mapping;
    }




    /// <summary>
    /// mko, 6.4.2021
    /// Modale Fragen wie "Kann geöffnet werden" oder 
    /// </summary>
    public interface IModalPhrase
        : INaming, IVerb
    {
    }

    public class ModalPhraseConversionError
        : InterfaceConversionErrorBase,
        IModalPhrase
    {
        public ModalPhraseConversionError(
                InterfaceConversionErrorTypes ErrorType,
                long NID,
                string CNT,
                string CN,
                string DE,
                string EN,
                string ES
            )
            : base(ErrorType, NID, CNT, CN, DE, EN, ES) { }
    }


    /// <summary>
    /// mko, 6.4.2021
    /// Fragesatz
    /// </summary>
    public class ModalQuestion
            : NamingBase
    {

        public const long UID = 0x92CE06B7;

        public ModalQuestion()
            : base(UID)
        {
        }

        public override string CNT => "modalQuestion";
        public override string CN => "语气词";
        public override string DE => "modale Frage";
        public override string EN => "modal Question";
        public override string ES => "pregunta modal";

        public override string Glyph => "?";
    }

    public class ModalResponse
        : NamingBase
    {

        public const long UID = 0x7C952E15;

        public ModalResponse()
            : base(UID)
        {
        }

        public override string CNT => "modalResponse";
        public override string CN => "模态反应";
        public override string DE => "modale Frage";
        public override string EN => "modal response";
        public override string ES => "respuesta modal";

        public override string Glyph => ".";
    }





    public class Negation
        : NamingBase
    {

        public const long UID = 0x78E3C617;

        public Negation()
            : base(UID)
        {
        }

        public override string CNT => "neg";
        public override string CN => "拒绝";
        public override string DE => "Verneinung";
        public override string EN => "Negation";
        public override string ES => "Negación";

        public override string Glyph => "!";
    }


    /// <summary>
    /// mko, 6.4.2021
    /// Befehlssatz
    /// </summary>
    public class Exclamation
        : NamingBase
    {

        public const long UID = 0x781CD9A1;

        public Exclamation()
            : base(UID)
        {
        }

        public override string CNT => "exclamation";
        public override string CN => "感叹句";
        public override string DE => "Ausruf";
        public override string EN => "Exclamation";
        public override string ES => "Frase de exclamación";

        public override string Glyph => "!";
    }


    public class Phrase
        : NamingBase
    {

        public const long UID = 0x39D7F9DB;

        public Phrase()
            : base(UID)
        {
        }

        public override string CNT => "phrase";
        public override string CN => "感叹句";
        public override string DE => "Satzglied";
        public override string EN => "Phrase";
        public override string ES => "Frase";

        public override string Glyph => "!";
    }


}
