using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Runtime
{
    public class Session
    : NamingBase
    {

        public const long UID = 0xD3D7B4D7;

        public Session()
            : base(UID)
        {
        }

        public override string CNT => "session";
        public override string CN => "会议";
        public override string DE => "Sitzung";
        public override string EN => "Session";
        public override string ES => "Session";        

    }

    public class SessionId
        : NamingBase
    {

        public const long UID = 0xA7308A78;

        public SessionId
()
            : base(UID)
        {
        }

        public override string CNT => "sessionId";
        public override string CN => "会议编号";
        public override string DE => "Sitzungsnummer";
        public override string EN => "Session Id";
        public override string ES => "Session Id";
    }

    public class Environment
        : NamingBase
    {
        public const long UID = 0x67D4FEB3;

        public Environment()
            : base(UID)
        {
        }

        public override string CNT => "env";
        public override string CN => "环境";
        public override string DE => "Umgebung";
        public override string EN => "Environment";
        public override string ES => "Medio Ambiente";

    }


    public class CalledUpFunction
        : NamingBase
    {
        public const long UID = 0xF15B303F;

        public CalledUpFunction()
            : base(UID)
        {
        }

        public override string CNT => "calledUpFunc";
        public override string CN => "调用功能";
        public override string DE => "aufgerufene Funktion";
        public override string EN => "Called up function";
        public override string ES => "función llamada";

        public override string Glyph => Glyphs.Math.Function;
    }

    public class Execute
    : NamingBase
    {
        public const long UID = 0xFA831C6E;

        public Execute()
            : base(UID)
        {
        }

        public override string CNT => "exec";
        public override string CN => "执行 ";
        public override string DE => "Ausführen";
        public override string EN => "Execute";
        public override string ES => "Ejecute";

        public override string Glyph => Glyphs.Runtime.Execute;
    }

    /// <summary>
    /// Zeitliche Dauer der Ausführung einer Funktion.
    /// </summary>
    public class ExecutionTime
        : NamingBase
    {
        public const long UID = 0xA4197B2D;

        public ExecutionTime()
            : base(UID)
        {
        }

        public override string CNT => "execTime";
        public override string CN => "执行时间";
        public override string DE => "Ausführungszeit";
        public override string EN => "execution time";
        public override string ES => "Tiempo de ejecución";

        public override string Glyph => Glyphs.Metrology.StopWatch;
    }


    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class Workload
        : NamingBase
    {
        public const long UID = 0x53C51052;

        public Workload()
            : base(UID)
        {
        }

        public override string CNT => "workload";
        public override string CN => "工作量";
        public override string DE => "Arbeitslast";
        public override string EN => "workload";
        public override string ES => "carga de trabajo";        
    }

    /// <summary>
    /// mko, 27.1.2021
    /// </summary>
    public class RuntimeError
        : NamingBase
    {

        public const long UID = 0x7DA4A2D;

        public RuntimeError()
            : base(UID)
        {
        }

        public override string CNT => "runtimeError";
        public override string CN => "运行时错误";
        public override string DE => "Laufzeitfehler";
        public override string EN => "Runtime Error";
        public override string ES => "error de ejecución";

        public override string Glyph => Glyphs.Runtime.RuntimeError;
    }


    /// <summary>
    /// Spezielle Art von Laufzeitfehlern: Exceptions
    /// </summary>
    public class RuntimeErrorOfTypeException
    : NamingBase
    {

        public const long UID = 0xBE7068A8;

        public RuntimeErrorOfTypeException()
            : base(UID)
        {
        }

        public override string CNT => "exception";
        public override string CN => "异常";
        public override string DE => "Ausnahme";
        public override string EN => "Exception";
        public override string ES => "Excepción";

        public override string Glyph => Glyphs.Runtime.RuntimeError;
    }

    public class RuntimeErrorOfTypeInnerException
        : NamingBase
    {

        public const long UID = 0x8C3AF2CE;

        public RuntimeErrorOfTypeInnerException()
            : base(UID)
        {
        }

        public override string CNT => "innerException";
        public override string CN => "内部异常";
        public override string DE => "ursächlische Ausnahme";
        public override string EN => "inner Exception";
        public override string ES => "Excepción causal";

        public override string Glyph => Glyphs.Runtime.RuntimeError;
    }




    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Start
        : NamingBase
    {
        public const long UID = 0x58682F;

        public Start()
            : base(UID)
        {
        }

        public override string CNT => "start";
        public override string CN => "开始";
        public override string DE => "starten";
        public override string EN => "start";
        public override string ES => "iniciar";


        public override string Glyph => Glyphs.Runtime.Start;
    }


    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class Stop
        : NamingBase
    {
        public const long UID = 0xB5E1B316;

        public Stop()
            : base(UID)
        {
        }

        public override string CNT => "stop";
        public override string CN => "停止";
        public override string DE => "anhalten";
        public override string EN => "stop";
        public override string ES => "parada";

        public override string Glyph => Glyphs.Runtime.Stop;
    }

    /// <summary>
    /// mko, 7.10.2020
    /// </summary>
    public class Abort
        : NamingBase
    {
        public const long UID = 0x1905C8CB;

        public Abort()
            : base(UID)
        {
        }

        public override string CNT => "abort";
        public override string CN => "终止";
        public override string DE => "abbruch";
        public override string EN => "abort";
        public override string ES => "abortar";

        public override string Glyph => Glyphs.Runtime.Aborted;
    }


    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class Continue
        : NamingBase
    {
        public const long UID = 0xF3421937;

        public Continue()
            : base(UID)
        {
        }

        public override string CNT => "continue";
        public override string CN => "继续";
        public override string DE => "fortsetzen";
        public override string EN => "continue";
        public override string ES => "continuar";

        public override string Glyph => Glyphs.Runtime.ProcessAndContinue;
    }

    /// <summary>
    /// mko, 3.9.2020
    /// </summary>
    public class Finished
        : NamingBase
    {
        public const long UID = 0x4EBA621E;

        public Finished()
            : base(UID)
        {
        }

        public override string CNT => "finished";
        public override string CN => "完了";
        public override string DE => "beendet";
        public override string EN => "finished";
        public override string ES => "terminado";

        public override string Glyph => Glyphs.Runtime.Finished;
    }




}
