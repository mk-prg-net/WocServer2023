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
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xC2FBCD4;

        public IsNullValue()
            : base(UID)
        { }

        public override string CN => "为零";
        public override string CNT => "isNull";
        public override string DE => "ist null";
        public override string EN => "is null";
        public override string ES => "es nulo";
    }

    /// mko, 19.6.2020
    /// Name des Prädikates für die Prüfung auf Nullwert.
    /// </summary>
    public class NotIsNullValue
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xAC00463B;

        public NotIsNullValue()
            : base(UID)
        { }

        public override string CN => "不是空的";
        public override string CNT => "notIsNull";
        public override string DE => "ist nicht null";
        public override string EN => "is not null";
        public override string ES => "no es nulo";
    }


    /// mko, 19.6.2020
    /// Vereinigung mehrerer Mengen
    /// </summary>
    public class Union
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xB0808913;

        public Union()
            : base(UID)
        { }

        public override string CN => "团结";
        public override string CNT => "union";
        public override string DE => "vereinigen";
        public override string EN => "union";
        public override string ES => "unificar";

        public override string Glyph => Glyphs.Math.Sets.union;
    }

    /// mko, 19.6.2020
    /// Durchschnitt meherer Mengen
    /// </summary>
    public class Intersect
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x34404586;

        public Intersect()
            : base(UID)
        { }

        public override string CN => "相交";
        public override string CNT => "intersect";
        public override string DE => "Schnittmenge bilden";
        public override string EN => "intersect";
        public override string ES => "intersección";

        public override string Glyph => Glyphs.Math.Sets.intersect;
    }

    /// mko, 19.6.2020
    /// Komplement  einer Menge
    /// </summary>
    public class Complement
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xEE6DEE51;

        public Complement()
            : base(UID)
        { }

        public override string CN => "补品";
        public override string CNT => "complement";
        public override string DE => "Komplement";
        public override string EN => "Complement";
        public override string ES => "Complemento";
    }

    /// mko, 19.6.2020
    /// X enthalten in Menge Operator
    /// </summary>
    public class Contains
        : NamingBase,
        TechTerms.Grammar.Prepositions.IPre, Grammar.IInProgressActivity
    {
        public const long UID = 0x84CADF6D;

        public Contains()
            : base(UID)
        { }

        public override string CN => "包含";
        public override string CNT => "contains";
        public override string DE => "enthält";
        public override string EN => "contains";
        public override string ES => "contiene";

        public override string Glyph => Glyphs.Sets.Operators.LeftIncludesRight;
    }

    /// mko, 19.6.2020
    /// X nicht enthalten in Menge Operator
    /// </summary>
    public class NotContains
        : NamingBase, Grammar.Prepositions.IPre, Grammar.IInProgressActivity
    {
        public const long UID = 0x16CD6D2B;

        public NotContains()
            : base(UID)
        { }

        public override string CN => "不含";
        public override string CNT => "notContains";
        public override string DE => "enthält nicht";
        public override string EN => "not contains";
        public override string ES => "no contiene";

        public override string Glyph => Glyphs.Sets.Operators.LeftNotIncludesRight;
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

        public override string CN => "第一";
        public override string CNT => "first";
        public override string DE => "erstes";
        public override string EN => "first";
        public override string ES => "primero";

        public override string Glyph => Glyphs.Navigation.SkipToBeginning;
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

        public override string CN => "最后的";
        public override string CNT => "last";
        public override string DE => "letztes";
        public override string EN => "last";
        public override string ES => "último";

        public override string Glyph => Glyphs.Navigation.SkipToEnd;
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

        public override string CN => "指数";
        public override string CNT => "index";
        public override string DE => "Index";
        public override string EN => "index";
        public override string ES => "índice";
    }

    /// <summary>
    /// mko, 3.8.20
    /// Elemente aus einer Menge aussortieren, herausnehmen
    /// </summary>
    public class CleanUp
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xA525020B;

        public CleanUp()
            : base(UID)
        { }

        public override string CN => "清理";
        public override string CNT => "cleanup";
        public override string DE => "bereinigen";
        public override string EN => CNT;
        public override string ES => "limpieza";
    }

    /// <summary>
    /// mko, 3.8.20
    /// </summary>
    public class Sort
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x965E1370;

        public Sort()
            : base(UID)
        { }

        public override string CN => "捃";
        public override string CNT => "sort";
        public override string DE => "Sortieren";
        public override string EN => CNT;
        public override string ES => "ordenar";

        public override string Glyph => Glyphs.Sets.Operators.SortAscending;
    }

    /// <summary>
    /// mko, 17.2.2021
    /// </summary>
    public class SortAscending
    : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x836BC714;

        public SortAscending()
            : base(UID)
        { }

        public override string CN => "升序";
        public override string CNT => "sortAscending";
        public override string DE => "aufsteigend sortieren";
        public override string EN => "sort ascending";
        public override string ES => "ordenación ascendente";

        public override string Glyph => Glyphs.Sets.Operators.SortAscending;
    }

    /// <summary>
    /// mko, 17.2.2021
    /// </summary>
    public class SortDescending
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xDFB33283;

        public SortDescending()
            : base(UID)
        { }

        public override string CN => "降序";
        public override string CNT => "sortDescending";
        public override string DE => "absteigend sortieren";
        public override string EN => "sort descending";
        public override string ES => "ordenación descendente";

        public override string Glyph => Glyphs.Sets.Operators.SortDescending;
    }


    /// <summary>
    /// mko, 14.9.2020
    /// Klassifizieren von Mengen (Klassenbildung)
    /// </summary>
    public class Classify
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xDDC49903;

        public Classify()
            : base(UID)
        { }

        public override string CN => "古典";
        public override string CNT => "classify";
        public override string DE => "klassifiziere";
        public override string EN => "classify";
        public override string ES => "Clasificación";
    }

    public class WasClassified
        : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0x9188F453;

        public WasClassified()
            : base(UID)
        { }

        public override string CN => "被归类为";
        public override string CNT => "wasClassified";
        public override string DE => "wurde klassifiziert";
        public override string EN => "was classified";
        public override string ES => "fue clasificado";
    }


    public class IsClassifiedAs
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x103AA02D;

        public IsClassifiedAs()
            : base(UID)
        { }

        public override string CN => "被归类为";
        public override string CNT => "isClassifiedAs";
        public override string DE => "ist klassifiziert als";
        public override string EN => "is classified as";
        public override string ES => "se clasifica como";
    }


    public class WasClassifiedAs
        : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0xC9BE7C61;

        public WasClassifiedAs()
            : base(UID)
        { }

        public override string CN => "被归类为";
        public override string CNT => "wasClassifiedAs";
        public override string DE => "wurde klassifiziert als";
        public override string EN => "was classified as";
        public override string ES => "fue clasificado como";
    }

    public class CanBeClassified
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0x7B13EE4C;

        public CanBeClassified()
            : base(UID)
        { }

        public override string CN => "可以分类";
        public override string CNT => "canBeClassified";
        public override string DE => "kann klassifiziert werden";
        public override string EN => "can be classified";
        public override string ES => "se puede clasificar";
    }

    public class CantBeClassified
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0xE163154D;

        public CantBeClassified()
            : base(UID)
        { }

        public override string CN => "不能分类";
        public override string CNT => "canNotBeClassified";
        public override string DE => "kann nicht klassifiziert werden";
        public override string EN => "can not be classified";
        public override string ES => "no se puede clasificar";
    }

    /// <sumary>
    /// mko, 17.2.2021
    /// Existenz eines Elementes in einer Menge ist gegeben
    /// </summary>
    public class Exists
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xF3DBE80E;

        public Exists()
            : base(UID)
        { }

        public override string CN => "存在";
        public override string CNT => "exists";
        public override string DE => "vorhanden";
        public override string EN => "exists";
        public override string ES => "existe";

        public override string Glyph => Glyphs.Sets.Operators.ElementOf;
    }

    public class Existing
    : NamingBase, Grammar.Adjectives.IAdjective
    {
        public const long UID = 0x8E36F6E;

        public Existing()
            : base(UID)
        { }

        public override string CN => "现有的";
        public override string CNT => "existing";
        public override string DE => "vorhandene";
        public override string EN => "existing";
        public override string ES => "existente";

        public override string Glyph => Glyphs.Sets.Operators.ElementOf;
    }



    /// <sumary>
    /// mko, 17.2.2021
    /// Existenz eines Elementes in einer Menge ist nicht gegeben
    /// </summary>
    public class NotExists
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x6B3EF2F4;

        public NotExists()
            : base(UID)
        { }

        public override string CN => "不存在";
        public override string CNT => "notExists";
        public override string DE => "nicht vorhanden";
        public override string EN => "not exists";
        public override string ES => "inexistente";

        public override string Glyph => Glyphs.Sets.Operators.NotElementOf;
    }


    public class NotExisting
    : NamingBase, Grammar.Adjectives.IAdjective
    {
        public const long UID = 0x78754865;

        public NotExisting()
            : base(UID)
        { }

        public override string CN => "不存在的";
        public override string CNT => "notExisting";
        public override string DE => "nicht vorhandene";
        public override string EN => "non-existent";
        public override string ES => "inexistente";

        public override string Glyph => Glyphs.Sets.Operators.NotElementOf;
    }

    public class Select
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x343B5CFB;

        public Select()
            : base(UID)
        { }

        public override string CN => "从中选择";
        public override string CNT => "select";
        public override string DE => "wähle aus";
        public override string EN => "selected";
        public override string ES => "seleccionado";

        public override string Glyph => Glyphs.Sets.Selections.lightCheckMark;
    }

    public class CanSelect
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0x1FC2FB81;

        public CanSelect()
            : base(UID)
        { }

        public override string CN => "可以选择";
        public override string CNT => "canSelect";
        public override string DE => "kann auswählen";
        public override string EN => "can choose";
        public override string ES => "puede elegir";

        public override string Glyph => Glyphs.Sets.Selections.lightCheckMark;
    }

    public class CantSelect
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0x139C83B8;

        public CantSelect()
            : base(UID)
        { }

        public override string CN => "不能选择";
        public override string CNT => "cantChooseFrom";
        public override string DE => "kann nicht auswählen";
        public override string EN => "can not select";
        public override string ES => "no puede seleccionar";

        public override string Glyph => Glyphs.Sets.Selections.uncheck;
    }


    public class Selected
        : NamingBase, Grammar.Adjectives.IAdjective
    {
        public const long UID = 0xD38449F0;

        public Selected()
            : base(UID)
        { }

        public override string CN => "选定";
        public override string CNT => "selected";
        public override string DE => "ausgewählte";
        public override string EN => "select";
        public override string ES => "elegir";

        public override string Glyph => Glyphs.Sets.Selections.lightCheckMark;
    }

    public class UnSelected
        : NamingBase, Grammar.Adjectives.IAdjective
    {
        public const long UID = 0xBC7856D7;

        public UnSelected()
            : base(UID)
        { }

        public override string CN => "未被选中的";
        public override string CNT => "notSelected";
        public override string DE => "nicht ausgewählte";
        public override string EN => "unselected";
        public override string ES => "no seleccionado";

        public override string Glyph => Glyphs.Sets.Selections.uncheck;
    }

    public class WasSelected
    : NamingBase, Grammar.Adjectives.IAdjective
    {
        public const long UID = 0xC39E0B5C;

        public WasSelected()
            : base(UID)
        { }

        public override string CN => "被选中";
        public override string CNT => "wasSelected";
        public override string DE => "wurden ausgewählt";
        public override string EN => "was selected";
        public override string ES => "fue seleccionado";

        public override string Glyph => Glyphs.Sets.Selections.lightCheckMark;
    }

    public class WasNotSelected
        : NamingBase, Grammar.Adjectives.IAdjective, Grammar.IFinishedActivity
    {
        public const long UID = 0x4C00716;

        public WasNotSelected()
            : base(UID)
        { }

        public override string CN => "未被选中";
        public override string CNT => "wasNotSelected";
        public override string DE => "wurde nicht ausgewählt";
        public override string EN => "was not selected";
        public override string ES => "no fue seleccionado";

        public override string Glyph => Glyphs.Sets.Selections.uncheck;
    }

    public class IsOutOfRange
    : NamingBase, Grammar.Adjectives.IAdjective, Grammar.IInProgressActivity
    {
        public const long UID = 0xCD75E08A;

        public IsOutOfRange()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => "isOutOfRange";
        public override string DE => "liegt außerhalb des Bereiches";
        public override string EN => "is out of range";
        public override string ES => EN;

        public override string Glyph => Glyphs.Math.Sets.OutOfRange;
    }

    public class IsNotOutOfRange
        : NamingBase, Grammar.Adjectives.IAdjective, Grammar.IInProgressActivity
    {
        public const long UID = 0xCD75E08A;

        public IsNotOutOfRange()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => "isNotOutOfRange";
        public override string DE => "liegt nicht außerhalb des Bereiches";
        public override string EN => "is not out of range";
        public override string ES => EN;

        public override string Glyph => Glyphs.Math.Sets.NotOutOfRange;
    }




}
