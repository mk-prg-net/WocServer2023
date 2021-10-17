using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Sets
{
    /// <summary>
    /// mko, 6.4.2021
    /// Menge
    /// </summary>
    public class Set
        : NamingBase
    {
        public const long UID = 0x6D96C4F7;

        public Set()
            : base(UID)
        { }

        public override string CN => "数量";
        public override string CNT => "set";
        public override string DE => "Menge";
        public override string EN => "Quantity";
        public override string ES => "Cantidad";

        public override string Glyph => Glyphs.Math.Sets.Set;
    }


    /// <summary>
    /// mko, 11.5.2021
    /// Aufzählung
    /// </summary>
    public class Enumeration
        : NamingBase
    {
        public const long UID = 0x12D9EE6E;

        public Enumeration()
            : base(UID)
        { }

        public override string CN => "枚举";
        public override string CNT => "enum";
        public override string DE => "Aufzählung";
        public override string EN => "Enumeration";
        public override string ES => "Enumeración";

        public override string Glyph => Glyphs.Math.Sets.Set;
    }

    /// <summary>
    /// mko, 7.10.2021
    /// </summary>
    public class Count
        : NamingBase
    {
        public const long UID = 0x7D3EDF74;

        public Count()
            : base(UID)
        { }

        public override string CN => "数";
        public override string CNT => "count";
        public override string DE => "Anzahl";
        public override string EN => "count";
        public override string ES => "cuenta";

        public override string Glyph => Glyphs.Math.Count;
    }


    /// <summary>
    /// mko, 28.5.2020
    /// Bezeichner für leere Mengen
    /// </summary>
    public class EmptySet
        : NamingBase
    {
        public const long UID = 0x57B3E7E6;

        public EmptySet()
            : base(UID)
        { }

        public override string CN => "空套";
        public override string CNT => "EmptySet";
        public override string DE => "leere Menge";
        public override string EN => "empty set";
        public override string ES => "conjunto vacío";

        public override string Glyph => Glyphs.Math.Sets.EmptySet;
    }


    public class Element
    : NamingBase
    {
        public const long UID = 0x6DF7D654;

        public Element()
            : base(UID)
        { }

        public override string CN => "元件";
        public override string CNT => "element";
        public override string DE => "Element";
        public override string EN => "Element";
        public override string ES => "Elemento";

        public override string Glyph => Glyphs.Math.Sets.IsElementOf;
    }





    /// <summary>
    /// mko, 17.7.2020
    /// Bezeichner für die Gesamtheit
    /// </summary>
    public class All
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {
        public const long UID = 0x622D6BB5;

        public All()
            : base(UID)
        { }

        public override string CN => "都";
        public override string CNT => "all";
        public override string DE => "alle";
        public override string EN => "all";
        public override string ES => "todos";

        public override string Glyph => Glyphs.Sets.Quantors.ForEach;
    }

    public class Any
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {
        public const long UID = 0x662C2A43;

        public Any()
            : base(UID)
        { }

        public override string CN => "任何";
        public override string CNT => "any";
        public override string DE => "irgendein";
        public override string EN => "any";
        public override string ES => "cualquier";

        public override string Glyph => Glyphs.Sets.Quantors.Exists;
    }


    public class Many
        : NamingBase,
        Grammar.Adverbs.IAdverb
    {
        public const long UID = 0x1EAC61DC;

        public Many()
            : base(UID)
        { }

        public override string CN => "许多";
        public override string CNT => "many";
        public override string DE => "viele";
        public override string EN => "many";
        public override string ES => "muchos";

        public override string Glyph => Glyphs.Math.Sets.Ellipsis;
    }

    public class One
    : NamingBase,
        Grammar.Adverbs.IAdverb
    {
        public const long UID = 0xBC0C4DF;

        public One()
            : base(UID)
        { }

        public override string CN => "一";
        public override string CNT => "one";
        public override string DE => "ein";
        public override string EN => "one";
        public override string ES => "un";

        public override string Glyph => "1";
    }

    public class First
        : NamingBase
    {
        public const long UID = 0xBD837B24;

        public First()
            : base(UID)
        { }

        public override string CN => "首先";
        public override string CNT => "first";
        public override string DE => "Erster";
        public override string EN => "First";
        public override string ES => "Primero";

        public override string Glyph => "I.";
    }

    public class Second
        : NamingBase
    {
        public const long UID = 0x91C398EB;

        public Second()
            : base(UID)
        { }

        public override string CN => "第二次";
        public override string CNT => "second";
        public override string DE => "Zweiter";
        public override string EN => "Second";
        public override string ES => "Segundo";

        public override string Glyph => "II.";
    }

    public class Third
    : NamingBase
    {
        public const long UID = 0xEA7F89E3;

        public Third()
            : base(UID)
        { }

        public override string CN => "第三次";
        public override string CNT => "third";
        public override string DE => "Dritter";
        public override string EN => "Third";
        public override string ES => "Tercero";

        public override string Glyph => "III.";
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

        public override string Glyph => Glyphs.Math.Sets.EmptySet;
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

        public override string CN => "无";
        public override string CNT => "none";
        public override string DE => "kein";
        public override string EN => "none";
        public override string ES => "ninguno";
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

        public override string CN => "最低限度";
        public override string CNT => "min";
        public override string DE => "Minimum";
        public override string EN => "Minimum";
        public override string ES => "Mínimo";
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

        public override string CN => "最多";
        public override string CNT => "max";
        public override string DE => "Maximum";
        public override string EN => "Maximum";
        public override string ES => "Máximo";
    }



    /// <summary>
    /// mko, 28.5.2020
    /// Bezeichner für Intervalle
    /// </summary>
    public class Interval
        : NamingBase
    {
        public const long UID = 0xF5D435A6;

        public Interval()
            : base(UID)
        { }

        public override string CN => "间隔";
        public override string CNT => "Interval";
        public override string DE => "Intervall";
        public override string EN => "interval";
        public override string ES => "Intervalo";

        public override string Glyph => Glyphs.Math.Sets.CloseSet;
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

        public override string CN => "范围";
        public override string CNT => EN;
        public override string DE => "Bereich";
        public override string EN => "Range";
        public override string ES => "Gama";
    }


    public class Begin
        : NamingBase
    {
        public const long UID = 0xC748BE0D;

        public Begin()
            : base(UID)
        { }

        public override string CN => "开始";
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

        public override string CN => "结束";
        public override string CNT => "IntervalEnd";
        public override string DE => "Ende";
        public override string EN => "end";
        public override string ES => "Fin";
    }


}
