using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Development
{
    public class UnderConstruction
    : NamingBase, Grammar.IInProgressActivity, Grammar.Adverbs.IAdverb
    {

        public const long UID = 0x3C0A2E2B;

        public UnderConstruction()
            : base(UID)
        {
        }

        public override string CNT => "underConstruction";
        public override string CN => "建设中";
        public override string DE => "in Arbeit";
        public override string EN => "under construction";
        public override string ES => "en construcción";

        public override string Glyph => Glyphs.VariousSigns.UnderConstruction;

    }

    public class ProgramFunction
    : NamingBase
    {

        public const long UID = 0x21015654;

        public ProgramFunction()
            : base(UID)
        {
        }

        public override string CNT => "progFunc";
        public override string CN => "程序功能";
        public override string DE => "Programmfunktion";
        public override string EN => "Program function";
        public override string ES => "Función del programa";

        public override string Glyph => Glyphs.Algorithm.Function;

    }



    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Software
        : NamingBase
    {

        public const long UID = 0xF729ADDE;

        public Software()
            : base(UID)
        {
        }

        public override string CNT => "software";
        public override string CN => "軟件";
        public override string DE => EN;
        public override string EN => "Software";
        public override string ES => EN;

        public override string Glyph => Glyphs.Computer.FloppyDiskBlack;

    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Hardware
        : NamingBase
    {

        public const long UID = 0xF32DD6EB;

        public Hardware()
            : base(UID)
        {
        }


        public override string CNT => "hardware";
        public override string CN => "硬件设施";
        public override string DE => EN;
        public override string EN => "Hardware";
        public override string ES => EN;

        public override string Glyph => Glyphs.Computer.oldPC;
    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Algorithm
        : NamingBase
    {

        public const long UID = 0x31D785BA;

        public Algorithm()
            : base(UID)
        {
        }


        public override string CNT => "algo";
        public override string CN => "算法";
        public override string DE => "Allgorithmus";
        public override string EN => "Algorithm";
        public override string ES => "Algoritmo";

        public override string Glyph => Glyphs.Algorithm.alternate;
    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Computer
        : NamingBase
    {

        public const long UID = 0xA752A36A;

        public Computer()
            : base(UID)
        {
        }


        public override string CNT => "computer";
        public override string CN => "计算机";
        public override string DE => EN;
        public override string EN => "Computer";
        public override string ES => "Computadora";

        public override string Glyph => Glyphs.Computer.PC;
    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Computerprogram
        : NamingBase
    {

        public const long UID = 0xFFEE7B25;

        public Computerprogram()
            : base(UID)
        {
        }

        public override string CNT => "computerProg";
        public override string CN => "计算机程序";
        public override string DE => "Computer Programm";
        public override string EN => "Computer program";
        public override string ES => "programa de ordenador";

        public override string Glyph => Glyphs.Software.SoftwareEngineering;
    }


    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Compiler
        : NamingBase
    {

        public const long UID = 0x15D0D8D6;

        public Compiler()
            : base(UID)
        {
        }

        public override string CNT => "compiler";
        public override string CN => "编译器";
        public override string DE => EN;
        public override string EN => "Compiler";
        public override string ES => EN;

        public override string Glyph => Glyphs.Software.Compiler;
    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Interpreter
        : NamingBase
    {

        public const long UID = 0x9432EEAB;

        public Interpreter()
            : base(UID)
        {
        }

        public override string CNT => "interpreter";
        public override string CN => "口译员";
        public override string DE => EN;
        public override string EN => "Interpreter";
        public override string ES => EN;

        public override string Glyph => Glyphs.Software.Compiler;
    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Debugger
        : NamingBase
    {

        public const long UID = 0xCA7F5283;

        public Debugger()
            : base(UID)
        {
        }

        public override string CNT => "debugger";
        public override string CN => "调试器";
        public override string DE => EN;
        public override string EN => "Debugger";
        public override string ES => EN;

        public override string Glyph => Glyphs.Software.Debugger;
    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Debug
        : NamingBase
    {

        public const long UID = 0xF58A72CD;

        public Debug()
            : base(UID)
        {
        }

        public override string CNT => "debug";
        public override string CN => "调试";
        public override string DE => "Fehlersuche";
        public override string EN => "debug";
        public override string ES => "Resolución de problemas (debug)";

        public override string Glyph => Glyphs.Software.Debugger;
    }


    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Trace
        : NamingBase
    {

        public const long UID = 0x756F2DD4;

        public Trace()
            : base(UID)
        {
        }

        public override string CNT => "trace";
        public override string CN => "追踪";
        public override string DE => "Laufzeitprotokoll (Trace)";
        public override string EN => "Runtime Protocol (Trace)";
        public override string ES => "Protocolo de ejecución (Trace)";


    }


    /// <summary>
    /// mko, 27.1.2021
    /// </summary>
    public class SyntaxError
        : NamingBase
    {

        public const long UID = 0x8CED0ED8;

        public SyntaxError()
            : base(UID)
        {
        }

        public override string CNT => "syntaxError";
        public override string CN => "句法错误";
        public override string DE => "Syntax Fehler";
        public override string EN => "Syntax error";
        public override string ES => "error de sintaxis";

        public override string Glyph => Glyphs.Software.SyntaxError;
    }





    /// <summary>
    /// mko, 21.2.2020
    /// VErsionsnummer einer Anwendung
    /// </summary>
    public class Version
            : NamingBase
    {

        public const long UID = 0xE80FFCF1;

        public Version()
            : base(UID)
        {
        }


        public override string CNT => "ver";
        public override string CN => "版本";
        public override string DE => "Version";
        public override string EN => "Version";
        public override string ES => "Versión";

    }

    public class Versionen
        : PluralForm
    {

        public const long UID = 0x15C65056;

        public Versionen()
            : base(UID)
        {
        }


        public override string CNT => "ver";
        public override string CN => "版本";
        public override string DE => "Version";
        public override string EN => "Version";
        public override string ES => "Versión";

        public override long PluralFormOfNameInSingluarNID => Version.UID;

    }


    /// <summary>
    /// mko, 21.2.2020
    /// Name einer Anwendung
    /// </summary>
    public class ApplicationName
        : NamingBase
    {
        public const long UID = 0x32C9DC39;

        public ApplicationName()
            : base(UID)
        {
        }

        public override string CNT => "appName";
        public override string CN => "申请名称";
        public override string DE => "Anwendungsname";
        public override string EN => "Name ofApplication";
        public override string ES => "Nombre de la aplicación";
    }



    /// <summary>
    /// mko, 21.2.2020
    /// Installation
    /// </summary>
    public class Setup
    : NamingBase
    {
        public const long UID = 0x4E368A29;

        public Setup()
            : base(UID)
        {
        }

        public override string CNT => "setup";
        public override string CN => "设置";
        public override string DE => "Setup";
        public override string EN => "Setup";
        public override string ES => "Setup";

        public override string Glyph => Glyphs.Computer.FloppyDiskWhite;
    }



    /// <summary>
    /// mko, 21.2.2020
    /// Entfernen einer Installation/Anwendung
    /// </summary>
    public class Installation
    : NamingBase
    {
        public const long UID = 0xBCF35077;

        public Installation()
            : base(UID)
        {
        }

        public override string CNT => "instal";
        public override string CN => "安装";
        public override string DE => "Installation";
        public override string EN => "Installation";
        public override string ES => "Instalación";
    }




    /// <summary>
    /// mko, 21.2.2020
    /// Entfernen einer Installation/Anwendung
    /// </summary>
    public class Uninstallation
    : NamingBase
    {
        public const long UID = 0x56CA407A;

        public Uninstallation()
            : base(UID)
        {
        }

        public override string CNT => "uninstal";
        public override string CN => "卸载";
        public override string DE => "Deinstallation";
        public override string EN => "Uninstallation";
        public override string ES => "desinstalación";
    }


    /// <summary>
    /// mko, 21.2.2020
    /// Entfernen einer Installation/Anwendung
    /// </summary>
    public class InstallationPackage
    : NamingBase
    {
        public const long UID = 0x6C12F1CE;

        public InstallationPackage()
            : base(UID)
        {
        }

        public override string CNT => "instalPkg";
        public override string CN => "安装包";
        public override string DE => "Installationspaket";
        public override string EN => "Installation package";
        public override string ES => "Paquete de instalación";
    }
}

