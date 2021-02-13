using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Sets
{
    /// <summary>
    /// mko, 28.5.2020
    /// Bezeichner für leere Mengen
    /// </summary>
    public class EmptySet
        :NamingBase
    {
        public const long UID = 0x57B3E7E6;

        public EmptySet()
            :base(UID)
        { }

        public override string CN => "empty Set";
        public override string CNT => "EmptySet";
        public override string DE => "leere Menge";
        public override string EN => "empty set";
        public override string ES => "conjunto vacío";
    }

    /// <summary>
    /// mko, 17.7.2020
    /// Bezeichner für die Gesamtheit
    /// </summary>
    public class All
        : NamingBase
    {
        public const long UID = 0x622D6BB5;

        public All()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => "all";
        public override string DE => "alle";
        public override string EN => "all";
        public override string ES => "todos";
    }


    /// <summary>
    /// mko, 19.6.2020
    /// Nullwert
    /// </summary>
    public class NullValue
        : NamingBase
    {
        public const long UID = 0xB5D75415;

        public NullValue()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "null";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// <summary>
    /// mko, 13.7.2020
    /// None ist ein Nullwert für Aufzählungstypen
    /// </summary>
    public class None
        : NamingBase
    {
        public const long UID = 0x46F1FC2D;

        public None()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "none";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }


    /// <summary>
    /// mko, 3.7.2020
    /// Minimalwert
    /// </summary>
    public class MinValue
        : NamingBase
    {
        public const long UID = 0xD18FA8F8;

        public MinValue()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "min";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// <summary>
    /// mko, 3.7.2020
    /// Maximalwert
    /// </summary>
    public class MaxValue
        : NamingBase
    {
        public const long UID = 0xE1F3E82;

        public MaxValue()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "max";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }



    /// <summary>
    /// mko, 28.5.2020
    /// Bezeichner für Intervalle
    /// </summary>
    public class Interval
        :NamingBase
    {
        public const long UID = 0xF5D435A6;

        public Interval()
            :base(UID)
        { }

        public override string CN => "interval";
        public override string CNT => "Interval";
        public override string DE => "Intervall";
        public override string EN => "interval";
        public override string ES => "Intervalo";
    }


    /// <summary>
    /// mko, 19.6.2020
    /// Bezeichner für Intervalle
    /// </summary>
    public class Range
        : NamingBase
    {
        public const long UID = 0x28167839;

        public Range()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => EN;
        public override string DE => "Bereich";
        public override string EN => "Range";
        public override string ES => EN;
    }


    public class Begin
        :NamingBase
    {
        public const long UID = 0xC748BE0D;

        public Begin()
            :base(UID)
        { }

        public override string CN => "begin";
        public override string CNT => "IntervalBegin";
        public override string DE => "Anfang";
        public override string EN => "begin";
        public override string ES => "Comienza";
    }

    public class End
        : NamingBase
    {
        public const long UID = 0x7F3E1B11;

        public End()
            : base(UID)
        { }

        public override string CN => "end";
        public override string CNT => "IntervalEnd";
        public override string DE => "Ende";
        public override string EN => "end";
        public override string ES => "Fin";
    }



}
