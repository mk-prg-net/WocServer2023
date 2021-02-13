using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Trees
{
    /// <summary>
    /// mko, 19.6.2020
    /// Tree
    /// </summary>
    public class Tree : NamingBase
    {
        public const long UID = 0xDF850503;

        public Tree()
            : base(UID)
        { }

        public override string CN => "树";
        public override string CNT => "tree";
        public override string DE => "Baum";
        public override string EN => "Tree";
        public override string ES => "Árbol";

        public override string Glyph => Glyphs.Trees.TreeDown;
    }

    /// <summary>
    /// mko, 19.6.2020
    /// Teilbaum
    /// </summary>
    public class SubTree : NamingBase
    {
        public const long UID = 0xB9892CA0;

        public SubTree()
            : base(UID)
        { }

        public override string CN => "子树";
        public override string CNT => "subTree";
        public override string DE => "Teilbaum";
        public override string EN => "Subtree";
        public override string ES => "Subárbol";

        public override string Glyph => Glyphs.Trees.TreeDown;
    }

    /// <summary>
    /// mko, 19.6.2020
    /// Wurzel
    /// </summary>
    public class Root : NamingBase
    {
        public const long UID = 0x9FC6048E;

        public Root()
            : base(UID)
        { }

        public override string CN => "根";
        public override string CNT => "root";
        public override string DE => "Wurzel";
        public override string EN => "Root";
        public override string ES => "Raíz";

        public override string Glyph => Glyphs.Trees.Root;

    }

    /// <summary>
    /// mko, 19.6.2020
    /// Baumebene
    /// </summary>
    public class Level : NamingBase
    {
        public const long UID = 0x68A1B07F;

        public Level()
            : base(UID)
        { }

        public override string CN => "级别";
        public override string CNT => "level";
        public override string DE => "Ebene";
        public override string EN => "Level";
        public override string ES => EN;
    }

    /// <summary>
    /// mko, 19.6.2020
    /// Elternknoten
    /// </summary>
    public class Parent : NamingBase
    {
        public const long UID = 0x39915886;

        public Parent()
            : base(UID)
        { }

        public override string CN => "父母";
        public override string CNT => "parent";
        public override string DE => "Elternknoten";
        public override string EN => "Parent";
        public override string ES => EN;

        public override string Glyph => Glyphs.ArrowsAndLines.arrN;
    }

    /// <summary>
    /// mko, 19.6.2020
    /// Kindknoten
    /// </summary>
    public class Child : NamingBase
    {
        public const long UID = 0xA9967E85;

        public Child()
            : base(UID)
        { }

        public override string CN => "儿童";
        public override string CNT => "child";
        public override string DE => "Kindknoten";
        public override string EN => "Child";
        public override string ES => EN;

        public override string Glyph => Glyphs.ArrowsAndLines.arrS;
    }

    /// <summary>
    /// mko, 19.6.2020
    /// Hierarchische ID
    /// </summary>
    public class Hid : NamingBase
    {
        public const long UID = 0x7ACB75;

        public Hid()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => "hid";
        public override string DE => EN;
        public override string EN => "HID";
        public override string ES => EN;

        public override string Glyph => Glyphs.Authentication.ID;
    }



}
