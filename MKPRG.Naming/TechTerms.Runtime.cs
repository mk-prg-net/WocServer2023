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

        public override string Glyph => Glyphs.Runtime.Session;
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

        public override string Glyph => Glyphs.Runtime.SessionId;
    }

    /// <summary>
    /// mko, 30.3.2021
    /// </summary>
    public class SessionStarted
        : NamingBase
    {

        public const long UID = 0x85CD7A19;

        public SessionStarted
            ()
            : base(UID)
        {
        }

        public override string CNT => "sessionStart";
        public override string CN => "会议开始";
        public override string DE => "Sitzung wurde gestartete";
        public override string EN => "Session started";
        public override string ES => "Inicio de la sesión";

        public override string Glyph => Glyphs.Runtime.SessionStart;
    }

    /// <summary>
    /// mko, 30.3.2021
    /// </summary>
    public class SessionEnded
        : NamingBase
    {

        public const long UID = 0x71AA7BEE;

        public SessionEnded
            ()
            : base(UID)
        {
        }

        public override string CNT => "sessionEnded";
        public override string CN => "会议结束";
        public override string DE => "Sitzung wurde beendet";
        public override string EN => "Session ended";
        public override string ES => "Fin de la sesión";

        public override string Glyph => Glyphs.Runtime.SessionEnd;
    }

    public class Progress
    : NamingBase
    {

        public const long UID = 0x979B7824;

        public Progress
            ()
            : base(UID)
        {
        }

        public override string CNT => "progress";
        public override string CN => "工作进展";
        public override string DE => "Arbeitsfortschritt";
        public override string EN => "work progress";
        public override string ES => "Progreso del trabajo";

        public override string Glyph => Glyphs.Runtime.Progress;
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

        public override string Glyph => Glyphs.Runtime.Environment;
    }

    public class NewEnvironment
        : NamingBase
    {
        public const long UID = 0xF54B6EAA;

        public NewEnvironment()
            : base(UID)
        {
        }

        public override string CNT => "newEnv";
        public override string CN => "新环境";
        public override string DE => "neue Umgebung";
        public override string EN => "new Environment";
        public override string ES => "nuevo Ambiente";

        public override string Glyph => Glyphs.Runtime.NewEnvironment;
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

        public override string Glyph => Glyphs.Math.Functions.Function;
    }

    public class ReturnValueOfFunction
        : NamingBase
    {
        public const long UID = 0xBC21C133;

        public ReturnValueOfFunction()
            : base(UID)
        {
        }

        public override string CNT => "returnValueOfFunc";
        public override string CN => "调用功能";
        public override string DE => "Rückgabewert der Funktion";
        public override string EN => "Value returned by function";
        public override string ES => "función llamada";

        public override string Glyph => Glyphs.Math.Functions.Function;
    }



    public class Execute
        : NamingBase, Grammar.IInProgressActivity
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


    public class WasExecuted
        : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0xF5096B5A;

        public WasExecuted()
            : base(UID)
        {
        }

        public override string CNT => "wasExecuted";
        public override string CN => "被执行 ";
        public override string DE => "wurde ausgeführt";
        public override string EN => "was executed";
        public override string ES => "fue ejecutado";

        public override string Glyph => Glyphs.Runtime.Execute;
    }

    public class Execution
        : NamingBase
    {
        public const long UID = 0xF35BBBF6;

        public Execution()
            : base(UID)
        {
        }

        public override string CNT => "execution";
        public override string CN => "执行失败 ";
        public override string DE => "Ausführung";
        public override string EN => "execution";
        public override string ES => "la ejecución";

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
        : NamingBase, Grammar.IInProgressActivity
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

    public class WasStarted
        : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0xB7C266AB;


        public WasStarted()
            : base(UID)
        {
        }

        public override string CNT => "wasStarted";
        public override string CN => "已启动";
        public override string DE => "wurde gestartet";
        public override string EN => "was started";
        public override string ES => "se lanzó";


        public override string Glyph => Glyphs.Runtime.Start;
    }

    public class CanBeStarted
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0x7938CF0D;

        public CanBeStarted()
            : base(UID)
        {
        }

        public override string CNT => "CanBeStarted";
        public override string CN => "已启动";
        public override string DE => "kann gestartet werden";
        public override string EN => "can be started";
        public override string ES => "puede iniciarse";


        public override string Glyph => Glyphs.Runtime.Start;
    }

    public class CantBeStarted
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0xE635F69;

        public CantBeStarted()
            : base(UID)
        {
        }

        public override string CNT => "CantBeStarted";
        public override string CN => "开不了口";
        public override string DE => "kann nicht gestartet werden";
        public override string EN => "cant be started";
        public override string ES => "no se puede iniciar";


        public override string Glyph => Glyphs.Runtime.Start;
    }

    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class Stop
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xB5E1B316;

        public Stop()
            : base(UID)
        {
        }

        public override string CNT => "stop";
        public override string CN => "停止";
        public override string DE => "stoppen";
        public override string EN => "stop";
        public override string ES => "parada";

        public override string Glyph => Glyphs.Runtime.Stop;
    }

    public class WasStoped
        : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0x9F564F5A;

        public WasStoped()
            : base(UID)
        {
        }

        public override string CNT => "wasStoped";
        public override string CN => "被制止";
        public override string DE => "wurde gestoppt";
        public override string EN => "was stoped";
        public override string ES => "se detuvo";

        public override string Glyph => Glyphs.Runtime.Stop;
    }

    public class CanBeStoped
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0x4358FEE;

        public CanBeStoped()
            : base(UID)
        {
        }

        public override string CNT => "canBeStoped";
        public override string CN => "挡得住";
        public override string DE => "kann gestoppt werden";
        public override string EN => "can be stoped";
        public override string ES => "puede ser detenido";

        public override string Glyph => Glyphs.Runtime.Stop;
    }

    public class CantBeStoped
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0x1D34DA01;

        public CantBeStoped()
            : base(UID)
        {
        }

        public override string CNT => "cantBeStoped";
        public override string CN => "挡不住";
        public override string DE => "kann nicht gestoppt werden";
        public override string EN => "cant be stoped";
        public override string ES => "no se puede detener";

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
    /// mko, 8.2.2021
    /// </summary>
    public class Cancel
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xAC673A10;

        public Cancel()
            : base(UID)
        {
        }

        public override string CNT => "cancel";
        public override string CN => "取消";
        public override string DE => "Abbrechen";
        public override string EN => "Cancel";
        public override string ES => "Cancelar";

        public override string Glyph => Glyphs.Runtime.Aborted;
    }


    public class WasCanceled
        : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0xCDC6759;

        public WasCanceled()
            : base(UID)
        {
        }

        public override string CNT => "wasCanceled";
        public override string CN => "被取消";
        public override string DE => "wurde abgebrochen";
        public override string EN => "was canceled";
        public override string ES => "fue cancelado";

        public override string Glyph => Glyphs.Runtime.Aborted;
    }

    public class CanBeCanceled
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0x2D1851ED;

        public CanBeCanceled()
            : base(UID)
        {
        }

        public override string CNT => "canBeCanceled";
        public override string CN => "可取消";
        public override string DE => "kann abgebrochen werden";
        public override string EN => "can be canceled";
        public override string ES => "puede ser cancelado";

        public override string Glyph => Glyphs.Runtime.Aborted;
    }

    public class CantBeCanceled
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0x67BEF957;

        public CantBeCanceled()
            : base(UID)
        {
        }

        public override string CNT => "cantBeCanceled";
        public override string CN => "不能取消";
        public override string DE => "kann nicht abgebrochen werden";
        public override string EN => "cant be canceled";
        public override string ES => "no se puede cancelar";

        public override string Glyph => Glyphs.Runtime.Aborted;
    }


    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class Continue
        : NamingBase, Grammar.IInProgressActivity
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

    public class WasContinued
        : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0x4044A8B1;

        public WasContinued()
            : base(UID)
        {
        }

        public override string CNT => "wasContinued";
        public override string CN => "已结束";
        public override string DE => "wurde fortgesetzt";
        public override string EN => "was continued";
        public override string ES => "se continuó";

        public override string Glyph => Glyphs.Runtime.ProcessAndContinue;
    }


    public class CanBeContinued
    : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0x2FFC7A8F;

        public CanBeContinued()
            : base(UID)
        {
        }

        public override string CNT => "canBeContinued";
        public override string CN => "可继续";
        public override string DE => "kann fortgesetzt werden";
        public override string EN => "can be continued";
        public override string ES => "puede continuar";

        public override string Glyph => Glyphs.Runtime.ProcessAndContinue;
    }

    public class CantBeContinued
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0x5699D5D0;

        public CantBeContinued()
            : base(UID)
        {
        }

        public override string CNT => "cantBeContinued";
        public override string CN => "不下去了";
        public override string DE => "kann nicht fortgesetzt werden";
        public override string EN => "cant be continued";
        public override string ES => "puede continuar";

        public override string Glyph => Glyphs.Runtime.ProcessAndContinue;
    }


    /// <summary>
    /// mko, 3.9.2020
    /// </summary>
    public class Finished
        : NamingBase, Grammar.IFinishedActivity
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


    public class WillBeFinished
        : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0xE22F688B;

        public WillBeFinished()
            : base(UID)
        {
        }

        public override string CNT => "willBefinished";
        public override string CN => "将结束";
        public override string DE => "wird beendet werden";
        public override string EN => "will be finished";
        public override string ES => "se concluirá";

        public override string Glyph => Glyphs.Runtime.Finished;
    }


    public class IsBeingFinished
    : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0xDCE360A5;

        public IsBeingFinished()
            : base(UID)
        {
        }

        public override string CNT => "isBeingFinished";
        public override string CN => "工作刚刚完成";
        public override string DE => "wird gerade beendet";
        public override string EN => "is being finished";
        public override string ES => "está en curso";

        public override string Glyph => Glyphs.Runtime.Finished;
    }


    public class ThrowsException
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x495E3155;

        public ThrowsException()
            : base(UID)
        {
        }

        public override string CNT => "throwsException";
        public override string CN => "抛出一个异常";
        public override string DE => "wirft eine Ausnahme";
        public override string EN => "throws an Exception";
        public override string ES => "lanza una excepción";

        public override string Glyph => Glyphs.Runtime.RuntimeError;
    }

    public class HasThrownAnException
        : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0x36BF7522;

        public HasThrownAnException()
            : base(UID)
        {
        }

        public override string CNT => "hasThrownAnException";
        public override string CN => "已经抛出了一个异常";
        public override string DE => "hat eine Ausnahme geworfen";
        public override string EN => "has thrown an exception";
        public override string ES => "ha lanzado una excepción";

        public override string Glyph => Glyphs.Runtime.RuntimeError;
    }

    public class StackTrace
        : NamingBase
    {
        public const long UID = 0xBB46484E;

        public StackTrace()
            : base(UID)
        {
        }

        public override string CNT => "stackTrace";
        public override string CN => "堆栈跟踪";
        public override string DE => "Abbild des Aufrufstapels";
        public override string EN => "Stack Trace";
        public override string ES => EN;

        public override string Glyph => Glyphs.Runtime.Tracing;
    }




}
