using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// mko, 12.10.2020
/// Besondere Zeitpunkte im Lebenszyklus eines Objketes
/// </summary>
namespace MKPRG.Naming.TechTerms.Lifecycle.Timeline
{
    /// <summary>
    /// mko, 10.2.2021    
    /// </summary>
    public class Constructor : NamingBase
    {
        public const long UID = 0xFB7FEE7E;

        public Constructor()
            : base(UID)
        {
        }

        public override string CNT => "ctor";
        public override string CN => "构造者";
        public override string DE => "Konstruktor";
        public override string EN => "Constructor";
        public override string ES => "Constructor";
    }


    public class Destructor : NamingBase
    {
        public const long UID = 0x17FD76AC;

        public Destructor()
            : base(UID)
        {
        }

        public override string CNT => "destruct";
        public override string CN => "破坏者";
        public override string DE => "Vernichter";
        public override string EN => "Destructor";
        public override string ES => "Destructor";
    }

    /// <summary>
    /// mko, 3.2.2021    
    /// </summary>
    public class Created : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0x5BD6FB47;

        public Created()
            : base(UID)
        {
        }

        public override string CNT => "created";
        public override string CN => "创建于";
        public override string DE => "erstellt am";
        public override string EN => "created on";
        public override string ES => "creado el";
    }


    /// <summary>
    /// mko, 3.2.2021    
    /// </summary>
    public class Deleted : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0xD6B32EF2;

        public Deleted()
            : base(UID)
        {
        }

        public override string CNT => "deleted";
        public override string CN => "删去";
        public override string DE => "gelöscht am";
        public override string EN => "deleted on";
        public override string ES => "borrado en";
    }

    /// <summary>
    /// mko, 3.2.2021    
    /// </summary>
    public class Updated : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0xC78D623C;

        public Updated()
            : base(UID)
        {
        }

        public override string CNT => "updated";
        public override string CN => "更新于";
        public override string DE => "aktualisiert am";
        public override string EN => "updated on";
        public override string ES => "actualizado en";
    }

    /// <summary>
    /// mko, 3.2.2021    
    /// </summary>
    public class Opened : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0xB25DE757;

        public Opened()
            : base(UID)
        {
        }

        public override string CNT => "opened";
        public override string CN => "开在";
        public override string DE => "geöffnet am";
        public override string EN => "opened on";
        public override string ES => "abrir en";
    }

    /// <summary>
    /// mko, 3.2.2021    
    /// </summary>
    public class Closed : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0xF4685BC;

        public Closed()
            : base(UID)
        {
        }

        public override string CNT => "closed";
        public override string CN => "闭上";
        public override string DE => "geschlossen am";
        public override string EN => "closed on";
        public override string ES => "cerrado el";
    }

    public class Fetched : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0x6F0242E;

        public Fetched()
            : base(UID)
        {
        }

        public override string CNT => "fetched";
        public override string CN => "撷取";
        public override string DE => "geholt am";
        public override string EN => "fetched on";
        public override string ES => "buscado en";
    }

    /// <summary>
    /// mko, 18.6.2020
    /// Datensatz schreiben
    /// </summary>
    public class Moved : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0x22D1656D;

        public Moved()
            : base(UID)
        {
        }

        public override string CNT => "moved";
        public override string CN => "动不动就";
        public override string DE => "verschoben am";
        public override string EN => "moved on";
        public override string ES => "movido en";
    }


    /// <summary>
    /// mko, 3.9.2020
    /// Datensatz schreiben
    /// </summary>
    public class Visited : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0x23DD0E08;

        public Visited()
            : base(UID)
        {
        }

        public override string CNT => "visited";
        public override string CN => "访";
        public override string DE => "besuchet am";
        public override string EN => "visited on";
        public override string ES => "visitado en";
    }



}
