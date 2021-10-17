using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Lifecycle
{
    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class Lifecycle
        : NamingBase
    {

        public const long UID = 0xC7DAB22E;

        public Lifecycle()
            : base(UID)
        {
        }

        public override string CNT => "lifecycle";
        public override string CN => "生命周期";
        public override string DE => "Lebenszyklus";
        public override string EN => "Lifecycle";
        public override string ES => "Ciclo de vida";
    }

    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class Refresh
        : NamingBase, Grammar.IInProgressActivity
    {

        public const long UID = 0xEE8C1275;

        public static Refresh I { get; } = new Refresh();

        public Refresh()
            : base(UID)
        {
        }

        public override string CNT => "refresh";
        public override string CN => "刷新";
        public override string DE => "erneuern";
        public override string EN => "refresh";
        public override string ES => "renovar";

        public override string Glyph => Glyphs.VariousSigns.Recycling;
    }

    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class Aging
        : NamingBase, Grammar.IInProgressActivity
    {

        public const long UID = 0xA668EB35;

        public static Aging I { get; } = new Aging();

        public Aging()
            : base(UID)
        {
        }

        public override string CNT => "aging";
        public override string CN => "交替";
        public override string DE => "altern";
        public override string EN => "aging";
        public override string ES => "envejecer";
    }

    /// <summary>
    /// mko, 3.2.2021
    /// </summary>
    public class Creator
    : NamingBase
    {

        public const long UID = 0x17C1D886;

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
    /// mko, 8.2.2021
    /// </summary>
    public class Death
    : NamingBase
    {

        public const long UID = 0xFA806E99;

        public Death()
            : base(UID)
        {
        }

        public override string CNT => "death";
        public override string CN => "死亡";
        public override string DE => "Tod";
        public override string EN => "Death";
        public override string ES => "Muerte";

        public override string Glyph => Glyphs.LiveCycle.Death;
    }


    /// </summary>
    public class IsDefined
        : NamingBase, Grammar.IInProgressActivity
    {

        public const long UID = 0xACC4A74E;

        public IsDefined()
            : base(UID)
        {
        }

        public override string CNT => "isDefined";
        public override string CN => "死亡";
        public override string DE => "ist definiert";
        public override string EN => "is defined";
        public override string ES => "Muerte";

        public override string Glyph => Glyphs.LiveCycle.Create;
    }
}
