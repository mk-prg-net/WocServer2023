using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Runtime.Threads
{

    public class Thread
        : NamingBase
    {

        public const long UID = 0x988349A2;

        public Thread
            ()
            : base(UID)
        {
        }

        public override string CNT => "thread";
        public override string CN => "命令流";
        public override string DE => "Befehlsstrom";
        public override string EN => "thread";
        public override string ES => "Flujo de comandos";

        public override string Glyph => Glyphs.Threads.Thread;
    }


    public class ThreadStarted
        : NamingBase
    {

        public const long UID = 0xE9C8EC1B;

        public ThreadStarted
            ()
            : base(UID)
        {
        }

        public override string CNT => "sessionStart";
        public override string CN => "会议开始";
        public override string DE => "Sitzung wurde gestartete";
        public override string EN => "Session started";
        public override string ES => "Inicio de la sesión";

        public override string Glyph => Glyphs.Threads.ThreadStart;
    }

    /// <summary>
    /// mko, 30.3.2021
    /// </summary>
    public class ThreadEnded
        : NamingBase
    {

        public const long UID = 0xD7B1852C;

        public ThreadEnded
            ()
            : base(UID)
        {
        }

        public override string CNT => "sessionEnded";
        public override string CN => "会议结束";
        public override string DE => "Sitzung wurde beendet";
        public override string EN => "Session ended";
        public override string ES => "Fin de la sesión";

        public override string Glyph => Glyphs.Threads.ThreadStop;
    }
}
