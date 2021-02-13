using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Operators.Sets
{
    /// mko, 19.6.2020
    /// Name des Prädikates für die Prüfung auf Nullwert.
    /// </summary>
    public class IsNullValue
        : NamingBase
    {
        public const long UID = 0xC2FBCD4;

        public IsNullValue()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "isNull";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// mko, 19.6.2020
    /// Name des Prädikates für die Prüfung auf Nullwert.
    /// </summary>
    public class NotIsNullValue
        : NamingBase
    {
        public const long UID = 0xAC00463B;

        public NotIsNullValue()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "notIsNull";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }


    /// mko, 19.6.2020
    /// Vereinigung mehrerer Mengen
    /// </summary>
    public class Union
        : NamingBase
    {
        public const long UID = 0xB0808913;

        public Union()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "union";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// mko, 19.6.2020
    /// Durchschnitt meherer Mengen
    /// </summary>
    public class Intersect
        : NamingBase
    {
        public const long UID = 0x34404586;

        public Intersect()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "intersect";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// mko, 19.6.2020
    /// Komplement  einer Menge
    /// </summary>
    public class Complement
        : NamingBase
    {
        public const long UID = 0xEE6DEE51;

        public Complement()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "complement";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// mko, 19.6.2020
    /// X enthalten in Menge Operator
    /// </summary>
    public class Contains
        : NamingBase
    {
        public const long UID = 0x84CADF6D;

        public Contains()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "contains";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// mko, 19.6.2020
    /// X nicht enthalten in Menge Operator
    /// </summary>
    public class NotContains
        : NamingBase
    {
        public const long UID = 0x16CD6D2B;

        public NotContains()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "notContains";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }


    /// mko, 19.6.2020
    /// Erstes Element einer geordneten Menge
    /// </summary>
    public class First
        : NamingBase
    {
        public const long UID = 0x107C344E;

        public First()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "first";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }


    /// mko, 19.6.2020
    /// Lestztes Element einer geordneten Menge
    /// </summary>
    public class Last
        : NamingBase
    {
        public const long UID = 0x61E30AC7;

        public Last()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "last";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// <summary>
    /// mko, 19.6.2020
    /// Index eines Elements einer Menge
    /// </summary>
    public class Index
        : NamingBase
    {
        public const long UID = 0x499C4660;

        public Index()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "index";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// <summary>
    /// mko, 3.8.20
    /// Elemente aus einer Menge aussortieren, herausnehmen
    /// </summary>
    public class CleanUp
        : NamingBase
    {
        public const long UID = 0xA525020B;

        public CleanUp()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "cleanup";
        public override string DE => "bereinigen";
        public override string EN => CNT;
        public override string ES => "limpieza";
    }

    /// <summary>
    /// mko, 3.8.20
    /// </summary>
    public class Sort
        : NamingBase
    {
        public const long UID = 0x965E1370;

        public Sort()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "sort";
        public override string DE => "Sortieren";
        public override string EN => CNT;
        public override string ES => "clasificar";
    }


    /// mko, 14.9.2020
    /// Klassifizieren von Mengen (Klassenbildung)
    /// </summary>
    public class Classify
        : NamingBase
    {
        public const long UID = 0xDDC49903;

        public Classify()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "classify";
        public override string DE => "Klassifizieren";
        public override string EN => "classify";
        public override string ES => "Clasificación";
    }


}
