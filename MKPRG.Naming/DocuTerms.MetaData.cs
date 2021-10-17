using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// mko, 27.2.2020
/// Metadaten zum Klassifizieren von Methodenaufrufen
/// </summary>
namespace MKPRG.Naming.DocuTerms.MetaData
{
    /// <summary>
    /// Typname
    /// </summary>
    public class Type
        : NamingBase
    {
        public const long UID = 0xAFC52BFA;

        public Type()
            : base(UID)
        {
        }

        public override string CNT => "type";

        public override string CN => "种类";

        public override string DE => "Typ";

        public override string EN => "Type";

        public override string ES => "Tipo";

    }


    /// <summary>
    /// mko, 21.2.2020
    /// Allgemeiner Bezeichner für eine Nachricht
    /// </summary>
    public class Msg
        : NamingBase
    {
        public const long UID = 0xAC62960E;

        public Msg()
            : base(UID)
        {
        }

        public override string CNT => "msg";

        public override string CN => "留言内容";

        public override string DE => "Nachricht";

        public override string EN => "Message";

        public override string ES => "Mensaje";

    }

    /// <summary>
    /// mko, 3.2.2021
    /// Kommentar
    /// </summary>
    public class Comment
    : NamingBase
    {
        public const long UID = 0x6F73673A;

        public Comment()
            : base(UID)
        {
        }

        public override string CNT => "comment";

        public override string CN => "评论";

        public override string DE => "Kommentar";

        public override string EN => "Comment";

        public override string ES => "Comentario";

    }


    /// <summary>
    /// mko, 21.2.2020
    /// Allgemeiner bezeichner für ein Argument
    /// </summary>
    public class Arg
        : NamingBase
    {
        public const long UID = 0xAEE3255D;

        public Arg()
            : base(UID)
        {
        }

        public override string CNT => "arg";

        public override string CN => "争论";

        public override string DE => "Argument";

        public override string EN => "Argument";

        public override string ES => "Argumento";

    }

    /// <summary>
    /// Allgemeiner Bezeichner für einen Wert
    /// </summary>
    public class Val
    : NamingBase
    {
        public const long UID = 0xA9417A61;

        public Val()
            : base(UID)
        {
        }

        public override string CNT => "val";

        public override string CN => "价值";

        public override string DE => "Wert";

        public override string EN => "Value";

        public override string ES => "Valor";
    }

    /// <summary>
    /// Dokumentiert Ergebnis einer Berechnung/Transaktion.
    /// </summary>
    public class Result
        : NamingBase
    {
        public const long UID = 0x14B8DC5B;

        public Result()
            : base(UID)
        {
        }

        public override string CNT => "result";

        public override string CN => "结果";

        public override string DE => "Ergebnis";

        public override string EN => "Result";

        public override string ES => "Resultado";
    }

    public class Error
        : NamingBase
    {
        public const long UID = 0xD9780603;

        public Error()
            : base(UID)
        {
        }

        public override string CNT => "Error";

        public override string CN => "错误";

        public override string DE => "Fehler";

        public override string EN => CNT;

        public override string ES => CNT;
    }



    /// <summary>
    /// Allg. Anzeige von Detail- Informationen zu einem 
    /// Ergebnis/Fehler
    /// </summary>
    public class Details
        : NamingBase
    {
        public const long UID = 0x8DEF155;

        public Details()
            : base(UID)
        {
        }

        public override string CNT => "details";

        public override string CN => "细节";

        public override string DE => "Details";

        public override string EN => "Details";

        public override string ES => "Detalles";
    }

    /// <summary>
    /// Kompnente/Modul
    /// Kopfzeile, Überschrift, Thematik
    /// </summary>
    public class Header
        : NamingBase
    {
        public const long UID = 0xF59EB59F;

        public Header()
            : base(UID)
        {
        }

        public override string CNT => "header";
        public override string CN => "头部";
        public override string DE => "Kopfzeile";
        public override string EN => "header";
        public override string ES => "encabezado";
    }



    /// <summary>
    /// Kompnente/Modul
    /// </summary>
    public class Module
    : NamingBase
    {
        public const long UID = 0x3941DEF9;

        public Module()
            : base(UID)
        {
        }

        public override string CNT => "module";

        public override string CN => "模块";

        public override string DE => "Modul";

        public override string EN => "Module";

        public override string ES => "Módulo";
    }


    /// <summary>
    /// Definiert einen anonymen Block von Eigenschaften und Methoden
    /// </summary>
    public class Block
    : NamingBase
    {
        public const long UID = 0x89B0DA2E;

        public Block()
            : base(UID)
        {
        }

        public override string CNT => "block";

        public override string CN => "阻止";

        public override string DE => "Block";

        public override string EN => "Block";

        public override string ES => "Bloque";
    }

    /// <summary>
    /// Name einer Eigenschalft, welche z.B als  Methodenparameter den semantischen Kontext des Methodenaufrufes.
    /// </summary>
    public class SemCtx
: NamingBase
    {
        public const long UID = 0xB8025B9D;

        public SemCtx()
            : base(UID)
        {
        }

        public override string CNT => "semCtx";

        public override string CN => "语境";

        public override string DE => "Kontext";

        public override string EN => "Context";

        public override string ES => "Contexto";
    }

    /// <summary>
    /// Name einer Eigenschalft, welche z.B als  Methodenparameter den semantischen Kontext des Methodenaufrufes.
    /// </summary>
    public class NameSpace
    : NamingBase
    {
        public const long UID = 0x960D31F6;

        public NameSpace()
            : base(UID)
        {
        }

        public override string CNT => "namespace";

        public override string CN => "命名空间";

        public override string DE => "Namensraum";

        public override string EN => "Namespace";

        public override string ES => EN;
    }

    /// <summary>
    /// Name einer Eigenschaft, welche z.B als  Methodenparameter den semantischen Kontext des Methodenaufrufes.
    /// </summary>
    public class Name
        : NamingBase
    {
        public const long UID = 0x199CB24D;

        public Name()
            : base(UID)
        {
        }

        public override string CNT => "Name";

        public override string CN => "名称";

        public override string DE => CNT;

        public override string EN => CNT;

        public override string ES => "Nombre";
    }

    /// <summary>
    /// Beschreibung eines Schaverhaltes, Gegenstandes
    /// </summary>
    public class Description
        : NamingBase
    {
        public const long UID = 0x851E0B68;

        public Description()
            : base(UID)
        {
        }

        public override string CNT => "descr";

        public override string CN => "说明";

        public override string DE => "Beschreibung";

        public override string EN => "Description";

        public override string ES => "Descripción";
    }

    /// <summary>
    /// mko, 12.4.2021
    /// </summary>
    public class ObjectContent
    : NamingBase
    {
        public const long UID = 0xD937A5BE;

        public ObjectContent()
            : base(UID)
        {
        }

        public override string CNT => "objectContent";
        public override string CN => "对象内容";
        public override string DE => "Inhlt eines Objektes";
        public override string EN => "Object content";
        public override string ES => "Contenido del objeto";
    }

}
