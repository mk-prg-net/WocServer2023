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
    public class Create : NamingBase
    {
        public const long UID = 0x590D8090;

        public Create()
            : base(UID)
        {
        }

        public override string CNT => "create";
        public override string CN => "创造";
        public override string DE => "erstellen";
        public override string EN => "create";
        public override string ES => "crear";
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
    }



    /// <summary>
    /// mko, 18.6.2020
    /// Datenquelle öffnen
    /// </summary>
    public class Open : NamingBase
    {
        public const long UID = 0x2788AEFE;

        public Open()
            : base(UID)
        {
        }

        public override string CNT => "open";
        public override string CN => "开";
        public override string DE => "öffnen";
        public override string EN => "open";
        public override string ES => "abrir";
    }

    /// <summary>
    /// mko, 18.6.2020
    /// Datenquelle öffnen
    /// </summary>
    public class Close : NamingBase
    {
        public const long UID = 0xE515EA8C;

        public Close()
            : base(UID)
        {
        }

        public override string CNT => "close";
        public override string CN => "密切";
        public override string DE => "schließen";
        public override string EN => "close";
        public override string ES => "cerrar";
    }

    /// <summary>
    /// mko, 18.6.2020
    /// Datensatz lesen
    /// </summary>
    public class Read : NamingBase
    {
        public const long UID = 0x46695AEC;

        public Read()
            : base(UID)
        {
        }

        public override string CNT => "read";
        public override string CN => "读";
        public override string DE => "lesen";
        public override string EN => "read";
        public override string ES => "lea";
    }

    /// <summary>
    /// mko, 18.6.2020
    /// Datensatz schreiben
    /// </summary>
    public class Write : NamingBase
    {
        public const long UID = 0x607D85C4;

        public Write()
            : base(UID)
        {
        }

        public override string CNT => "write";
        public override string CN => "写";
        public override string DE => "schreiben";
        public override string EN => "write";
        public override string ES => "escribe";
    }

    /// <summary>
    /// mko, 18.6.2020
    /// Datensatz löschen
    /// </summary>
    public class Delete : NamingBase
    {
        public const long UID = 0x5E2D1D37;

        public Delete()
            : base(UID)
        {
        }

        public override string CNT => "delete";
        public override string CN => "删去";
        public override string DE => "entfernen";
        public override string EN => "delete";
        public override string ES => "eliminar";     
    }

    /// <summary>
    /// mko, 18.6.2020
    /// Datensatz schreiben
    /// </summary>
    public class Fetch : NamingBase
    {
        public const long UID = 0x85B88B16;

        public Fetch()
            : base(UID)
        {
        }

        public override string CNT => "fetch";
        public override string CN => "取来";
        public override string DE => "holen";
        public override string EN => "fetch";
        public override string ES => "buscar";
    }

    /// <summary>
    /// mko, 18.6.2020
    /// Datensatz schreiben
    /// </summary>
    public class Move : NamingBase
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


    /// <summary>
    /// mko, 3.9.2020
    /// Datensatz schreiben
    /// </summary>
    public class Visit : NamingBase
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

}
