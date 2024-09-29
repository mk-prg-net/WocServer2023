using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Sequences
{
    public class Sequence
     : NamingBase
    {

        public const long UID = 0x101EF1F4;

        public Sequence()
            : base(UID)
        {
        }

        public override string CNT => "seq";
        public override string CN => "顺序";
        public override string DE => "Sequenz";
        public override string EN => "Sequence";
        public override string ES => "Secuencia";

        public override string Glyph => Glyphs.Algorithm.Sequnce;
    }


    public class Command
        : NamingBase
    {

        public const long UID = 0x4552D8AE;

        public Command()
            : base(UID)
        {
        }

        public override string CNT => "cmd";
        public override string CN => "指挥部";
        public override string DE => "Befehl";
        public override string EN => "Command";
        public override string ES => "Commando";

        public override string Glyph => Glyphs.Algorithm.Function;
    }



    public class Next
         : NamingBase
    {

        public const long UID = 0xF55673A;

        public Next()
            : base(UID)
        {
        }

        public override string CNT => "next";
        public override string CN => "接下来";
        public override string DE => "nächstes";
        public override string EN => "next";
        public override string ES => "siguiente";

        public override string Glyph => Glyphs.Algorithm.NextOp;
    }

    public class Step
        : NamingBase
    {

        public const long UID = 0xD47B4809;

        public Step()
            : base(UID)
        {
        }

        public override string CNT => "step";
        public override string CN => "步骤";
        public override string DE => "Schritt";
        public override string EN => "Step";
        public override string ES => "paso";

        public override string Glyph => Glyphs.Algorithm.Step2;
    }

    public class Line
        : NamingBase
    {

        public const long UID = 0x4F604D74;

        public Line()
            : base(UID)
        {
        }

        public override string CNT => "line";
        public override string CN => "步骤";
        public override string DE => "Zeile";
        public override string EN => "line";
        public override string ES => "línea";

        public override string Glyph => Glyphs.VariousSigns.Brick;
    }



    public class Repeat
     : NamingBase, Grammar.IInProgressActivity
    {

        public const long UID = 0xA9995636;

        public static Repeat I { get; } = new Repeat();

        public Repeat()
            : base(UID)
        {
        }

        public override string CNT => "repeat";
        public override string CN => "重复";
        public override string DE => "wiederhole";
        public override string EN => "repeat";
        public override string ES => "Repita";

        public override string Glyph => Glyphs.Algorithm.Repeat;
    }

    public class ReturnFromSub
     : NamingBase, Grammar.IInProgressActivity
    {

        public const long UID = 0x717BECA;

        public static ReturnFromSub I { get; } = new ReturnFromSub();

        public ReturnFromSub()
            : base(UID)
        {
        }

        public override string CNT => "return";
        public override string CN => "返回";
        public override string DE => "kehre zurück";
        public override string EN => "return";
        public override string ES => "devolver";

        public override string Glyph => Glyphs.Algorithm.Return;
    }

    public class Loop
        : NamingBase
    {

        public const long UID = 0x17AA66D2;

        public Loop()
            : base(UID)
        {
        }

        public override string CNT => "loop";
        public override string CN => "襻";
        public override string DE => "Schleife";
        public override string EN => "return";
        public override string ES => "bucle";

        public override string Glyph => Glyphs.Algorithm.Loop;
    }

    public class JumpForward
        : NamingBase, Grammar.IInProgressActivity
    {

        public const long UID = 0xCE219CDF;

        public static JumpForward I { get; } = new JumpForward();

        public JumpForward()
            : base(UID)
        {
        }

        public override string CNT => "jumpforward";
        public override string CN => "跃进";
        public override string DE => "springe nach vorne";
        public override string EN => "jump forward";
        public override string ES => "saltar hacia adelante";

        public override string Glyph => Glyphs.Algorithm.JumpForward;
    }


    public class JumpBackward
        : NamingBase, Grammar.IInProgressActivity
    {

        public const long UID = 0x41FF90A6;

        public static JumpBackward I { get; } = new JumpBackward();

        public JumpBackward()
            : base(UID)
        {
        }

        public override string CNT => "jumpbackward";
        public override string CN => "倒退";
        public override string DE => "springe zurück";
        public override string EN => "jump backward";
        public override string ES => "saltar hacia atrás";

        public override string Glyph => Glyphs.Algorithm.JumpBackward;
    }

}
