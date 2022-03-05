using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Access
{
    public class DataType : NamingBase
    {
        public const long UID = 0xA81B616;

        public DataType()
            : base(UID)
        {
        }

        public override string CNT => "dataType";
        public override string CN => "数据类型";
        public override string DE => "Datentyp";
        public override string EN => "datatype";
        public override string ES => "tipo de datos";
    }

    /// <summary>
    /// mko, 18.6.2020
    /// Datensatz erzeugen
    /// </summary>
    public class Create : NamingBase, Grammar.IVerb, Grammar.IInProgressActivity
    {
        public const long UID = 0x590D8090;

        /// <summary>
        /// Statisches Objekt, kann in InProgressActivity- Sätzen eingesetzt werden
        /// </summary>
        public static Create I { get; } = new Create();

        public Create()
            : base(UID)
        {
        }

        public override string CNT => "create";
        public override string CN => "创造";
        public override string DE => "erstellen";
        public override string EN => "create";
        public override string ES => "crear";

        public override string Glyph => Glyphs.LiveCycle.Create_PlusInsideU;
    }

    public class Created : NamingBase, Grammar.IVerb, Grammar.IInProgressActivity
    {
        public const long UID = 0xCD9058B3;

        public static Created I { get; } = new Created();

        public Created()
            : base(UID)
        {
        }

        public override string CNT => "created";
        public override string CN => "创立";
        public override string DE => "wurde erstellt";
        public override string EN => "was created";
        public override string ES => "fue creado";

        public override string Glyph => Glyphs.LiveCycle.Create_PlusInsideU;
    }

    public class CanBeCreated : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0x4B25D66;

        public CanBeCreated()
            : base(UID)
        {
        }

        public override string CNT => "canBeCreated";
        public override string CN => "可以产生";
        public override string DE => "kann erstellt werden";
        public override string EN => "can be created";
        public override string ES => "se puede crear";

        public override string Glyph => Glyphs.LiveCycle.Create_PlusInsideU;
    }

    public class CantBeCreated : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0x1D45B408;

        public CantBeCreated()
            : base(UID)
        {
        }

        public override string CNT => "cantBeCreated";
        public override string CN => "不能产生";
        public override string DE => "kann nicht erstellt werden";
        public override string EN => "cannot be created";
        public override string ES => "no se puede crear";

        public override string Glyph => $"{Glyphs.Math.Bool.not}";
    }




    /// <summary>
    /// mko, 28.7.2020
    /// Datensatz erzeugen
    /// </summary>
    public class Creator : NamingBase
    {
        public const long UID = 0xB55330C9;

        public Creator()
            : base(UID)
        {
        }

        public override string CNT => "creator";
        public override string CN => "创作人";
        public override string DE => "Ersteller";
        public override string EN => "Creator";
        public override string ES => "Creador";

        public override string Glyph => Glyphs.LiveCycle.Creator;
    }



    /// <summary>
    /// mko, 18.6.2020
    /// Zustand, Datenquelle ist geöffnet
    /// </summary>
    public class Open : NamingBase

    {
        public const long UID = 0x2788AEFE;

        public static Open I { get; } = new Open();

        public Open()
            : base(UID)
        {
        }

        public override string CNT => "open";
        public override string CN => "开";
        public override string DE => "offen";
        public override string EN => "open";
        public override string ES => "abrir";
    }

    /// <summary>
    /// mko, 12.5.2021
    /// Datenquelle öffnen
    /// </summary>
    public class Opens : NamingBase, Grammar.IVerb, Grammar.IInProgressActivity

    {
        public const long UID = 0x439C2ECB;

        public Opens()
            : base(UID)
        {
        }

        public override string CNT => "opens";
        public override string CN => "打开";
        public override string DE => "öffne";
        public override string EN => "opens";
        public override string ES => "abre";
    }



    /// <summary>
    /// mko, 6.4.2020
    /// Datenquelle öffnen
    /// </summary>
    public class WasOpened : NamingBase, Grammar.IVerb, Grammar.IFinishedActivity

    {
        public const long UID = 0xCA749873;

        public WasOpened()
            : base(UID)
        {
        }

        public override string CNT => "opened";
        public override string CN => "被打开";
        public override string DE => "wurde göffnet";
        public override string EN => "was opened";
        public override string ES => "se abrió";
    }

    /// <summary>
    /// mko, 6.4.2020
    /// Kann die Datenquelle geöffnet werden
    /// </summary>
    public class CanBeOpened : NamingBase, Grammar.IModalPhrase

    {
        public const long UID = 0x85615B9F;

        public CanBeOpened()
            : base(UID)
        {
        }

        public override string CNT => "canBeOpened";
        public override string CN => "可开";
        public override string DE => "kann göffnet werden";
        public override string EN => "can be opened";
        public override string ES => "puede abrirse";
    }

    /// <summary>
    /// mko, 6.4.2020
    /// Kann die Datenquelle nicht geöffnet werden
    /// </summary>
    public class CantBeOpened : NamingBase, Grammar.IModalPhrase

    {
        public const long UID = 0x82AED644;

        public CantBeOpened()
            : base(UID)
        {
        }

        public override string CNT => "cantBeOpened";
        public override string CN => "打不开";
        public override string DE => "kann nicht göffnet werden";
        public override string EN => "cant be opened";
        public override string ES => "no se puede abrir";

        public override string Glyph => Glyphs.Authorization.Forbidden;
    }




    /// <summary>
    /// mko, 18.6.2020
    /// Zustand, Datenquelle ist geschlossen
    /// </summary>
    public class IsClose : NamingBase
    {
        public const long UID = 0xE515EA8C;

        public static IsClose I { get; } = new IsClose();

        public IsClose()
            : base(UID)
        {
        }

        public override string CNT => "isClosed";
        public override string CN => "已关闭";
        public override string DE => "ist geschlossen";
        public override string EN => "is closed";
        public override string ES => "está cerrado";
    }

    public class Closing : NamingBase, Grammar.IVerb, Grammar.IInProgressActivity
    {
        public const long UID = 0x63052579;

        public Closing()
            : base(UID)
        {
        }

        public override string CNT => "closing";
        public override string CN => "密切";
        public override string DE => "schließe";
        public override string EN => "close";
        public override string ES => "cerrar";

        public override string Glyph => Glyphs.Runtime.CancelX;
    }


    public class Close : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xE2ABC35D;

        public Close()
            : base(UID)
        {
        }

        public override string CNT => "close";
        public override string CN => "关闭";
        public override string DE => "schließen";
        public override string EN => "close";
        public override string ES => "cerrar";

        public override string Glyph => Glyphs.Runtime.CancelX;
    }

    public class Leave : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x7AD9253B;

        public Leave()
            : base(UID)
        {
        }

        public override string CNT => "leave";
        public override string CN => "离开";
        public override string DE => "verlassen";
        public override string EN => "leave";
        public override string ES => "dejar";

        public override string Glyph => Glyphs.Runtime.CancelX;
    }




    public class WasClosed : NamingBase, Grammar.IVerb, Grammar.IFinishedActivity
    {
        public const long UID = 0x7C210DEC;

        public WasClosed()
            : base(UID)
        {
        }

        public override string CNT => "closed";
        public override string CN => "被关闭";
        public override string DE => "wurde geschlossen";
        public override string EN => "has been closed";
        public override string ES => "se cerró";
    }

    public class CanBeClosed : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0x558BFA2D;

        public CanBeClosed()
            : base(UID)
        {
        }

        public override string CNT => "canBeClosed";
        public override string CN => "可以关闭";
        public override string DE => "kann geschlossen werden";
        public override string EN => "can be closed";
        public override string ES => "se puede cerrar";
    }

    public class CantBeClosed : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0x79ACE582;

        public CantBeClosed()
            : base(UID)
        {
        }

        public override string CNT => "cantBeClosed";
        public override string CN => "关不上";
        public override string DE => "kann nicht geschlossen werden";
        public override string EN => "cant be closed";
        public override string ES => "no se puede cerrar";

        public override string Glyph => Glyphs.Authorization.Forbidden;
    }

    /// <summary>
    /// mko, 18.6.2020
    /// Datensatz lesen
    /// </summary>
    public class Read : NamingBase, Grammar.IVerb, Grammar.IInProgressActivity
    {
        public const long UID = 0x46695AEC;

        public static Read I { get; } = new Read();

        public Read()
            : base(UID)
        {
        }

        public override string CNT => "read";
        public override string CN => "读";
        public override string DE => "lese";
        public override string EN => "read";
        public override string ES => "lea";

        public override string Glyph => $"{Glyphs.Access.Read}";
    }

    public class WillBeRead : NamingBase, Grammar.IVerb, Grammar.IFutureActivity
    {
        public const long UID = 0x18CD013B;

        public static Read I { get; } = new Read();

        public WillBeRead()
            : base(UID)
        {
        }

        public override string CNT => "willBeRead";
        public override string CN => "将被读取";
        public override string DE => "wird gelesen";
        public override string EN => "will be read";
        public override string ES => "se leerá";

        public override string Glyph => $"{Glyphs.Access.Read}";
    }


    public class WasRead : NamingBase, Grammar.IVerb, Grammar.IFinishedActivity
    {
        public const long UID = 0x3EBF61CC;

        public WasRead()
            : base(UID)
        {
        }

        public override string CNT => "wasRead";
        public override string CN => "已阅";
        public override string DE => "wurde gelesen";
        public override string EN => "has been read";
        public override string ES => "se ha leído";

        public override string Glyph => $"{Glyphs.Access.Read}";
    }

    public class CanRead : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0x78FD3DD4;

        public CanRead()
            : base(UID)
        {
        }

        public override string CNT => "canRead";
        public override string CN => "看得懂";
        public override string DE => "kann lesen";
        public override string EN => "can read";
        public override string ES => "puede leer";

        public override string Glyph => $"{Glyphs.Access.Read}";

    }


    public class CanBeRead : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0xBE6BA390;

        public CanBeRead()
            : base(UID)
        {
        }

        public override string CNT => "canBeRead";
        public override string CN => "可读";
        public override string DE => "kann gelesen werden";
        public override string EN => "can be read";
        public override string ES => "se puede leer";
    }

    public class CantRead : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0xDDF06789;

        public CantRead()
            : base(UID)
        {
        }

        public override string CNT => "cantRead";
        public override string CN => "看不懂";
        public override string DE => "kann nicht lesen";
        public override string EN => "cant read";
        public override string ES => "no puede leer";

        public override string Glyph => Glyphs.Authorization.Forbidden;

    }


    public class CantBeRead : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0x9234231D;

        public CantBeRead()
            : base(UID)
        {
        }

        public override string CNT => "cantBeRead";
        public override string CN => "看不懂";
        public override string DE => "kann nicht gelesen werden";
        public override string EN => "cant be read";
        public override string ES => "no se puede leer";

        public override string Glyph => Glyphs.Authorization.Forbidden;
    }

    /// <summary>
    /// mko, 18.6.2020
    /// Datensatz schreiben
    /// </summary>
    public class Write : NamingBase, Grammar.IVerb, Grammar.IInProgressActivity
    {
        public const long UID = 0x607D85C4;

        public Write()
            : base(UID)
        {
        }

        public override string CNT => "write";
        public override string CN => "写";
        public override string DE => "schreibe";
        public override string EN => "write";
        public override string ES => "escribe";

        public override string Glyph => Glyphs.Access.Write;
    }

    public class WillBeWritten : NamingBase, Grammar.IVerb, Grammar.IInProgressActivity
    {
        public const long UID = 0xFCA5192A;

        public WillBeWritten()
            : base(UID)
        {
        }

        public override string CNT => "willBeWritten";
        public override string CN => "将被写成";
        public override string DE => "wird geschrieben";
        public override string EN => "will be writtten";
        public override string ES => "se escribirá";

        public override string Glyph => Glyphs.Access.Write;
    }


    public class Written : NamingBase, Grammar.IVerb, Grammar.IFinishedActivity
    {
        public const long UID = 0xDF4DA8EB;

        public Written()
            : base(UID)
        {
        }

        public override string CNT => "written";
        public override string CN => "已写";
        public override string DE => "wurde geschrieben";
        public override string EN => "has been written";
        public override string ES => "fue escrito";

        public override string Glyph => Glyphs.Access.Write;
    }

    public class CanWrite : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0x26E5385E;

        public CanWrite()
            : base(UID)
        {
        }

        public override string CNT => "canWrite";
        public override string CN => "可写";
        public override string DE => "kann schreiben";
        public override string EN => "can write";
        public override string ES => "puede escribir";

        public override string Glyph => Glyphs.Access.Write;
    }


    public class CanBeWritten : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0x7B6E1F38;

        public CanBeWritten()
            : base(UID)
        {
        }

        public override string CNT => "canBeWritten";
        public override string CN => "可写";
        public override string DE => "kann geschrieben werden";
        public override string EN => "can be written";
        public override string ES => "Se puede escribir";

        public override string Glyph => Glyphs.Access.Write;
    }

    public class CantWrite : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0xB4FDEFB1;

        public CantWrite()
            : base(UID)
        {
        }

        public override string CNT => "cantWrite";
        public override string CN => "不能书写";
        public override string DE => "kann nicht schreiben";
        public override string EN => "cant write";
        public override string ES => "No puede escribir";

        public override string Glyph => Glyphs.Authorization.Forbidden;
    }


    public class CantBeWritten : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0x9FCA6FB3;

        public CantBeWritten()
            : base(UID)
        {
        }

        public override string CNT => "cantBeWritten";
        public override string CN => "不能书写";
        public override string DE => "kann nicht geschrieben werden";
        public override string EN => "cant be written";
        public override string ES => "No se puede escribir";

        public override string Glyph => Glyphs.Authorization.Forbidden;
    }

    /// <summary>
    /// mko, 18.6.2020
    /// Datensatz löschen
    /// </summary>
    public class Delete : NamingBase, Grammar.IVerb, Grammar.IInProgressActivity
    {
        public const long UID = 0x5E2D1D37;

        public Delete()
            : base(UID)
        {
        }

        public override string CNT => "delete";
        public override string CN => "删去";
        public override string DE => "entferne";
        public override string EN => "delete";
        public override string ES => "eliminar";

        public override string Glyph => Glyphs.Access.Delete;
    }

    public class WasDeleted : NamingBase, Grammar.IVerb, Grammar.IFinishedActivity
    {
        public const long UID = 0x4127E831;

        public WasDeleted()
            : base(UID)
        {
        }

        public override string CNT => "wasDeleted";
        public override string CN => "已被删除";
        public override string DE => "wurde gelöscht";
        public override string EN => "was deleted";
        public override string ES => "ha sido eliminado";

        public override string Glyph => Glyphs.Access.Delete;
    }

    public class CanBeDeleted : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0x75D2E37C;

        public CanBeDeleted()
            : base(UID)
        {
        }

        public override string CNT => "canBeDeleted";
        public override string CN => "可删除";
        public override string DE => "kann gelöscht werden";
        public override string EN => "can be deleted";
        public override string ES => "puede ser eliminado";

        public override string Glyph => Glyphs.Access.Delete;
    }

    public class CantBeDeleted : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0xB4D0B609;

        public CantBeDeleted()
            : base(UID)
        {
        }

        public override string CNT => "cantBeDeleted";
        public override string CN => "删不掉";
        public override string DE => "kann nicht gelöscht werden";
        public override string EN => "cant be deleted";
        public override string ES => "no se puede eliminar";

        public override string Glyph => Glyphs.Authorization.Forbidden;
    }

    /// <summary>
    /// mko, 18.6.2020
    /// Datensatz schreiben
    /// </summary>
    public class Fetch : NamingBase, Grammar.IVerb, Grammar.IInProgressActivity
    {
        public const long UID = 0x85B88B16;

        public Fetch()
            : base(UID)
        {
        }

        public override string CNT => "fetch";
        public override string CN => "取来";
        public override string DE => "hole";
        public override string EN => "fetch";
        public override string ES => "buscar";
    }

    public class WasFetched : NamingBase, Grammar.IVerb, Grammar.IFinishedActivity
    {
        public const long UID = 0x18D9EEDD;

        public WasFetched()
            : base(UID)
        {
        }

        public override string CNT => "wasFetched";
        public override string CN => "被取走";
        public override string DE => "wurde geholt";
        public override string EN => "was fetched";
        public override string ES => "se ha buscado";
    }

    public class CanBeFetched : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0x26E59288;

        public CanBeFetched()
            : base(UID)
        {
        }

        public override string CNT => "canBeFetched";
        public override string CN => "可见";
        public override string DE => "kann geholt werden";
        public override string EN => "can be fetched";
        public override string ES => "se puede buscar";
    }

    public class CantBeFetched : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0x7642B0A3;

        public CantBeFetched()
            : base(UID)
        {
        }

        public override string CNT => "cantBeFetched";
        public override string CN => "拿不到";
        public override string DE => "kann nicht geholt werden";
        public override string EN => "cant be fetched";
        public override string ES => "no se puede buscar";
    }



    /// <summary>
    /// mko, 18.6.2020
    /// Datensatz schreiben
    /// </summary>
    public class Move : NamingBase, Grammar.IVerb, Grammar.IInProgressActivity
    {
        public const long UID = 0x9E8D4C1A;

        public Move()
            : base(UID)
        {
        }

        public override string CNT => "move";
        public override string CN => "移动";
        public override string DE => "verschiebe";
        public override string EN => "move";
        public override string ES => "mover";
    }

    public class WasMoved : NamingBase, Grammar.IVerb, Grammar.IFinishedActivity
    {
        public const long UID = 0xAD858587;

        public WasMoved()
            : base(UID)
        {
        }

        public override string CNT => "wasMoved";
        public override string CN => "被推迟";
        public override string DE => "wurde verschoben";
        public override string EN => "was moved";
        public override string ES => "se ha aplazado";
    }

    public class CanBeMoved : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0xFB512C2A;

        public CanBeMoved()
            : base(UID)
        {
        }

        public override string CNT => "canBeMoved";
        public override string CN => "动辄";
        public override string DE => "kann verschoben werden";
        public override string EN => "can be moved";
        public override string ES => "se puede mover";
    }

    public class CantBeMoved : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0xB1579C1A;

        public CantBeMoved()
            : base(UID)
        {
        }

        public override string CNT => "cantBeMoved";
        public override string CN => "不容置疑";
        public override string DE => "kann nicht verschoben werden";
        public override string EN => "cant be moved";
        public override string ES => "no se puede desplazar";
    }

    public class Copy : NamingBase, Grammar.IVerb, Grammar.IInProgressActivity
    {
        public const long UID = 0xAC6A2273;

        public Copy()
            : base(UID)
        {
        }

        public override string CNT => "copy";
        public override string CN => "拷贝";
        public override string DE => "kopiere";
        public override string EN => "copy";
        public override string ES => "copia";
    }

    public class WasCopied : NamingBase, Grammar.IVerb, Grammar.IFinishedActivity
    {
        public const long UID = 0xDB9644D;

        public WasCopied()
            : base(UID)
        {
        }

        public override string CNT => "wasCopied";
        public override string CN => "被复制了";
        public override string DE => "wurde kopiert";
        public override string EN => "was copied";
        public override string ES => "fue copiado";
    }

    public class CanBeCopied : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0xC6401810;

        public CanBeCopied()
            : base(UID)
        {
        }

        public override string CNT => "canBeCopied";
        public override string CN => "可以复制";
        public override string DE => "kann kopiert werden";
        public override string EN => "can be copied";
        public override string ES => "puede copiarse";
    }

    public class CantBeCopied : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0x4516444F;

        public CantBeCopied()
            : base(UID)
        {
        }

        public override string CNT => "cantBeCopied";
        public override string CN => "不能复制";
        public override string DE => "kann nicht kopiert werden";
        public override string EN => "cant be copied";
        public override string ES => "no se puede copiar";
    }





    public class Load : NamingBase, Grammar.IVerb, Grammar.IInProgressActivity
    {
        public const long UID = 0x21301BD;

        public Load()
            : base(UID)
        {
        }

        public override string CNT => "load";
        public override string CN => "负荷";
        public override string DE => "lade";
        public override string EN => "load";
        public override string ES => "carga";

        public override string Glyph => Glyphs.Access.Read;
    }

    public class WillBeLoaded : NamingBase, Grammar.IVerb, Grammar.IFutureActivity
    {
        public const long UID = 0xD4A78835;

        public WillBeLoaded()
            : base(UID)
        {
        }

        public override string CNT => "willBeLoaded";
        public override string CN => "将被加载";
        public override string DE => "wird geladen";
        public override string EN => "will be loaded";
        public override string ES => "se cargará";

        public override string Glyph => Glyphs.Access.Read;
    }

    public class CanBeLoaded : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0xB5A14A2C;

        public CanBeLoaded()
            : base(UID)
        {
        }

        public override string CNT => "canBeLoaded";
        public override string CN => "可以加载";
        public override string DE => "kann geladen werden";
        public override string EN => "can be loaded";
        public override string ES => "puede ser cargado";

        public override string Glyph => Glyphs.Access.Read;
    }

    public class CantBeLoaded : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0x7D449CE;

        public CantBeLoaded()
            : base(UID)
        {
        }

        public override string CNT => "canNotBeLoaded";
        public override string CN => "无法加载";
        public override string DE => "kann nicht geladen werden";
        public override string EN => "can not be loaded";
        public override string ES => "no se puede cargar";

        public override string Glyph => Glyphs.Access.Read;
    }


    public class WasLoaded : NamingBase, Grammar.IVerb, Grammar.IFinishedActivity
    {
        public const long UID = 0xF8D78DE9;

        public WasLoaded()
            : base(UID)
        {
        }

        public override string CNT => "wasLoaded";
        public override string CN => "被加载";
        public override string DE => "wurde geladen";
        public override string EN => "was loaded";
        public override string ES => "fue cargado";

        public override string Glyph => Glyphs.Access.Read;
    }




    /// <summary>
    /// mko, 17.2.2021
    /// Datensatz schreiben
    /// </summary>
    public class Save : NamingBase, Grammar.IVerb, Grammar.IInProgressActivity
    {
        public const long UID = 0x871CD1F0;

        public Save()
            : base(UID)
        {
        }

        public override string CNT => "save";
        public override string CN => "商店";
        public override string DE => "speicher";
        public override string EN => "save";
        public override string ES => "guardar";

        public override string Glyph => Glyphs.Access.Save;
    }

    public class WasSaved : NamingBase, Grammar.IVerb, Grammar.IFinishedActivity
    {
        public const long UID = 0x8C9944F8;

        public WasSaved()
            : base(UID)
        {
        }

        public override string CNT => "wasSaved";
        public override string CN => "已获救";
        public override string DE => "wurde abgespeichert";
        public override string EN => "was saved";
        public override string ES => "se ha salvado";

        public override string Glyph => Glyphs.Access.Save;
    }

    public class CanBeSaved : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0x4039685D;

        public CanBeSaved()
            : base(UID)
        {
        }

        public override string CNT => "canBeSaved";
        public override string CN => "可以保存";
        public override string DE => "kann abgespeichert werden";
        public override string EN => "can be saved";
        public override string ES => "se puede guardar";

        public override string Glyph => Glyphs.Access.Save;
    }

    public class CantBeSaved : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0x55BE701A;

        public CantBeSaved()
            : base(UID)
        {
        }

        public override string CNT => "cantBeSaved";
        public override string CN => "挽救不了";
        public override string DE => "kann nicht abgespeichert werden";
        public override string EN => "cant be saved";
        public override string ES => "no se puede salvar";

        public override string Glyph => Glyphs.Authorization.Forbidden;
    }



    /// <summary>
    /// mko, 17.2.2021
    /// Datensatz schreiben
    /// </summary>
    public class SaveAs : NamingBase, Grammar.IVerb, Grammar.IInProgressActivity
    {
        public const long UID = 0xC1B01B9A;

        public SaveAs()
            : base(UID)
        {
        }

        public override string CNT => "saveAs";
        public override string CN => "存为 ";
        public override string DE => "speicher unter";
        public override string EN => "save as";
        public override string ES => "guardar como";

        public override string Glyph => Glyphs.Access.Save;
    }

    public class WasSavedAs : NamingBase, Grammar.IVerb, Grammar.IFinishedActivity
    {
        public const long UID = 0x17539E2B;

        public WasSavedAs()
            : base(UID)
        {
        }

        public override string CNT => "wasSavedAs";
        public override string CN => "被保存为";
        public override string DE => "wurde gespeichert unter";
        public override string EN => "was saved as";
        public override string ES => "se guardó como";

        public override string Glyph => Glyphs.Access.Save;
    }


    public class CanBeSavedAs : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0xDE2357E;

        public CanBeSavedAs()
            : base(UID)
        {
        }

        public override string CNT => "canBeSavedAs";
        public override string CN => "可以保存为";
        public override string DE => "kann gespeichert werden unter";
        public override string EN => "can be saved as";
        public override string ES => "puede guardarse como";

        public override string Glyph => Glyphs.Access.Save;
    }

    public class CantBeSavedAs : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0xF0ACB44A;

        public CantBeSavedAs()
            : base(UID)
        {
        }

        public override string CNT => "cantBeSavedAs";
        public override string CN => "不能保存为";
        public override string DE => "kann nicht gespeichert werden unter";
        public override string EN => "cant be saved as";
        public override string ES => "no se puede guardar como";

        public override string Glyph => Glyphs.Authorization.Forbidden;
    }



    /// <summary>
    /// mko, 3.9.2020
    /// Datensatz schreiben
    /// </summary>
    public class Visit : NamingBase, Grammar.IVerb, Grammar.IInProgressActivity
    {
        public const long UID = 0x7B74D8B7;

        public Visit()
            : base(UID)
        {
        }

        public override string CNT => "visit";
        public override string CN => "访";
        public override string DE => "besuche";
        public override string EN => "visit";
        public override string ES => "visite";
    }

    public class WasVisited : NamingBase, Grammar.IVerb, Grammar.IFinishedActivity
    {
        public const long UID = 0xD0773919;

        public WasVisited()
            : base(UID)
        {
        }

        public override string CNT => "wasVisited";
        public override string CN => "被访问";
        public override string DE => "wurde besucht";
        public override string EN => "was visited";
        public override string ES => "fue visitado";
    }


    public class CanBeVisited : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0x6F5FBE13;

        public CanBeVisited()
            : base(UID)
        {
        }

        public override string CNT => "canBeVisited";
        public override string CN => "可诣";
        public override string DE => "kann besucht werden";
        public override string EN => "can be visited";
        public override string ES => "se puede visitar";
    }

    public class CantBeVisited : NamingBase, Grammar.IVerb, Grammar.IModalPhrase
    {
        public const long UID = 0x6335419E;

        public CantBeVisited()
            : base(UID)
        {
        }

        public override string CNT => "cantBeVisited";
        public override string CN => "看不到";
        public override string DE => "kann nicht besucht werden";
        public override string EN => "cant be visited";
        public override string ES => "no se puede visitar";

        public override string Glyph => Glyphs.Authorization.Forbidden;
    }

    // Objekt- Eigenschaften
    public class SetPropertyValue : NamingBase, Grammar.IVerb, Grammar.IInProgressActivity
    {
        public const long UID = 0x2EB3F691;

        public SetPropertyValue()
            : base(UID)
        {
        }

        public override string CNT => "set";
        public override string CN => "设置属性";
        public override string DE => "Eigenschaft setzen";
        public override string EN => "set Property";
        public override string ES => "Configuración de las propiedades";
    }


    public class GetPropertyValue : NamingBase, Grammar.IVerb, Grammar.IInProgressActivity
    {
        public const long UID = 0x184EEB17;

        public GetPropertyValue()
            : base(UID)
        {
        }

        public override string CNT => "get";
        public override string CN => "获取属性值";
        public override string DE => "lese Eigenschaftswert";
        public override string EN => "get Property value";
        public override string ES => "obtener Valor de la propiedad";
    }

}
