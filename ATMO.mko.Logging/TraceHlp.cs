//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 29.1.2013
//
//  Projekt.......: mko
//  Name..........: TraceHlp.cs
//  Aufgabe/Fkt...: Prüfen von Vor- und Nachbedingungen, Formatieren von Fehler- und 
//                  Statusmeldungen.
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 15.3.2017
//  Änderungen....: Throw...IfNot Methoden implementiert
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 28.3.2017
//  Änderungen....: Throw...If Methoden implementiert
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 05.6.2017
//  Änderungen....: Throw...If Methoden einheitlich um innerException erweitert
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 21.9.2017
//  Änderungen....: Integration in ATMO.mko.Logging
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 22.9.2017
//  Änderungen....: FlattenExceptionMessages aus mko.mkoExceptionMessagesFlat.cs
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 26.9.2017
//  Änderungen....: In FlattenExceptionMessages werden Teile der Meldung jetzt durch ;
//                  getrennt.
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 2.3.2018
//  Änderungen....: Format der Textmeldungen umgestellt auf polnische Notation
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 27.3.2018
//  Änderungen....: Exakte, parsertaugliche polnische Notation
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 18.9.2018
//  Änderungen....: ThrowArgEx... nehmen jetzt als Fehlerbeschreibung auch IDokuEnties an.
//  Version.......: 2.1.61
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 26.6.2020
//  Änderungen....: ThrowArgEx... mit DocuTerm- Parametern werfen jetzt Ausnahmen vom Typ ArgumentExceptionWithDocuTermDescription
//  Version.......: 2.1.61

//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using ANC = MKPRG.Naming;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

//using pnL = ATMO.mko.Logging.PNDocuTerms.Composer;

namespace ATMO.mko.Logging
{
    public class TraceHlp
    {

        static PNDocuTerms.DocuEntities.Composer pnL
        {
            get
            {
                if (_pnL == null)
                {
                    _pnL = new PNDocuTerms.DocuEntities.Composer();
                }
                return _pnL;
            }
        }

        static PNDocuTerms.DocuEntities.Composer _pnL;


        static PNDocuTerms.DocuEntities.PNFormater fmt => RCV3.fmtPN;
        


        /// <summary>
        /// Wirft eine Argumentexception, wenn die Bedingung nictht erfüllt ist. Die Fehlermeldung ist automatisch 
        /// umfassend dokumentiert (Zeit, Ort mit Assemblynamen, Klasse und Methode, sowie einer Ursachenbeschreibung).
        /// </summary>
        /// <param name="condition">Bedingung, die erfüllt sein muß</param>
        /// <param name="msg">Fehlermeldung, falls Bedingung nicht erfüllt ist</param>
        /// <param name="callerName">Name der aufrufenden Funktion, siehe https://msdn.microsoft.com/en-us/library/mt653988.aspx </param>
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public static void ThrowArgExIfNot(bool condition, string msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            if (!condition)
            {
                throw new ArgumentException(FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        /// <param name="callerName"></param>
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public static void ThrowArgExIfNot(bool condition, IDocuEntity msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            if (!condition)
            {
                throw new ArgumentExceptionWithDocuTermDescription(FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }


        /// <summary>
        /// Wirft eine Argumentexception, wenn die Bedingung erfüllt ist. Die Fehlermeldung ist automatisch 
        /// umfassend dokumentiert (Zeit, Ort mit Assemblynamen, Klasse und Methode, sowie einer Ursachenbeschreibung).
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="msg"></param>
        /// <param name="callerName"></param>
        public static void ThrowArgExIf(bool condition, string msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            if (condition)
            {
                throw new ArgumentException(FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        /// <param name="callerName"></param>
        public static void ThrowArgExIf(bool condition, IDocuEntity msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            if (condition)
            {
                throw new ArgumentExceptionWithDocuTermDescription(FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }


        /// <summary>
        /// Wirft eine Argumentexception. Die Fehlermeldung ist automatisch 
        /// umfassend dokumentiert (Zeit, Ort mit Assemblynamen, Klasse und Methode, sowie einer Ursachenbeschreibung).
        /// </summary>
        /// <param name="condition">Bedingung, die erfüllt sein muß</param>
        /// <param name="msg">Fehlermeldung, falls Bedingung nicht erfüllt ist</param>
        /// <param name="callerName">Name der aufrufenden Funktion, siehe https://msdn.microsoft.com/en-us/library/mt653988.aspx </param>
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public static void ThrowArgEx(string msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            throw new ArgumentException(FormatErrMsg(assembly, cls, callerName, msg), innerException);
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        /// <param name="callerName"></param>
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public static void ThrowArgEx(IDocuEntity msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            throw new ArgumentExceptionWithDocuTermDescription(FormatErrMsg(assembly, cls, callerName, msg), innerException);
        }


        /// <summary>
        /// Wirft eine Exception, wenn die Bedingung nictht erfüllt ist. Die Fehlermeldung ist automatisch 
        /// umfassend dokumentiert (Zeit, Ort mit Assemblynamen, Klasse und Methode, sowie einer Ursachenbeschreibung).
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="msg"></param>
        /// <param name="callerName"></param>
        public static void ThrowExIfNot(bool condition, string msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.FullName;

            if (!condition)
            {
                throw new Exception(FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        /// <param name="callerName"></param>
        public static void ThrowExIfNot(bool condition, IDocuEntity msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.FullName;

            if (!condition)
            {
                throw new ExceptionWithDocuTermDescription(FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }


        /// <summary>
        /// Wirft eine Exception, wenn die Bedingung erfüllt ist. Die Fehlermeldung ist automatisch 
        /// umfassend dokumentiert (Zeit, Ort mit Assemblynamen, Klasse und Methode, sowie einer Ursachenbeschreibung).
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="msg"></param>
        /// <param name="callerName"></param>
        public static void ThrowExIf(bool condition, string msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.FullName;

            if (condition)
            {
                throw new Exception(FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        /// <param name="callerName"></param>
        public static void ThrowExIf(bool condition, IDocuEntity msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.FullName;

            if (condition)
            {
                throw new ExceptionWithDocuTermDescription(FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }

        /// <summary>
        /// Wirft eine Exception. Die Fehlermeldung ist automatisch 
        /// umfassend dokumentiert (Zeit, Ort mit Assemblynamen, Klasse und Methode, sowie einer Ursachenbeschreibung).
        /// </summary>
        /// <param name="condition">Bedingung, die erfüllt sein muß</param>
        /// <param name="msg">Fehlermeldung, falls Bedingung nicht erfüllt ist</param>
        /// <param name="callerName">Name der aufrufenden Funktion, siehe https://msdn.microsoft.com/en-us/library/mt653988.aspx </param>
        public static void ThrowEx(string msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.FullName;

            if (innerException == null)
            {
                throw new Exception(FormatErrMsg(assembly, cls, callerName, msg));
            }
            else
            {
                throw new Exception(FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }

        public static void ThrowEx(IDocuEntity msg, Exception innerException = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.FullName;

            if (innerException == null)
            {
                throw new ExceptionWithDocuTermDescription(FormatErrMsg(assembly, cls, callerName, msg));
            }
            else
            {
                throw new ExceptionWithDocuTermDescription(FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }

        /// <summary>
        /// mko, 19.2.2019
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        /// <param name="callerName"></param>
        public static void ThrowIndexOutOfRangeException(
            IDocuEntity msg,
            Exception innerException = null,
            [System.Runtime.CompilerServices.CallerMemberName]
            string callerName = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.FullName;

            if (innerException == null)
            {
                throw new IndexOutOfRangeExceptionWithDocuTermDescription(FormatErrMsg(assembly, cls, callerName, msg));

            }
            else
            {
                throw new IndexOutOfRangeExceptionWithDocuTermDescription(FormatErrMsg(assembly, cls, callerName, msg), innerException);
            }
        }


        /// <summary>
        /// mko, 19.2.2019
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        /// <param name="callerName"></param>
        public static void ThrowIndexOutOfRangeExceptionIf(
            bool cond,
            IDocuEntity msg,
            Exception innerException = null,
            [System.Runtime.CompilerServices.CallerMemberName]
                    string callerName = "")
        {
            if (cond)
            {

                var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
                var cls = mth.ReflectedType.Name;
                var assembly = mth.ReflectedType.Assembly.FullName;

                if (innerException == null)
                {
                    throw new IndexOutOfRangeExceptionWithDocuTermDescription(FormatErrMsg(assembly, cls, callerName, msg));

                }
                else
                {
                    throw new IndexOutOfRangeExceptionWithDocuTermDescription(FormatErrMsg(assembly, cls, callerName, msg), innerException);
                }
            }
        }

        /// <summary>
        /// mko, 19.2.2019
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        /// <param name="callerName"></param>
        public static void ThrowIndexOutOfRangeExceptionIfNot(
            bool cond,
            IDocuEntity msg,
            Exception innerException = null,
            [System.Runtime.CompilerServices.CallerMemberName]
                    string callerName = "")
        {
            if (!cond)
            {

                var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
                var cls = mth.ReflectedType.Name;
                var assembly = mth.ReflectedType.Assembly.FullName;

                if (innerException == null)
                {
                    throw new IndexOutOfRangeExceptionWithDocuTermDescription(FormatErrMsg(assembly, cls, callerName, msg));

                }
                else
                {
                    throw new IndexOutOfRangeExceptionWithDocuTermDescription(FormatErrMsg(assembly, cls, callerName, msg), innerException);
                }
            }
        }


        public static string MsgsToString(string[] msgs)
        {
            var res = new StringBuilder();
            foreach (string msg in msgs)
            {
                res.Append($" {msg}");
            }
            return res.ToString();
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static IDocuEntity FormatErrMsg(object Obj, string MethodName, IDocuEntity msg)
        {
            var now = DateTime.Now;
            return pnL.i($"{Obj.GetType().FullName}",
                    pnL.m(MethodName, 
                        pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                        pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                        pnL.ret(pnL.eFails(pnL.EncapsulateAsEventParameter(msg)))));
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static IDocuEntity FormatErrMsg(string ClassName, string MethodName, IDocuEntity msg)
        {
            var now = DateTime.Now;
            return pnL.i(ClassName,
                        pnL.m(MethodName, 
                            pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                            pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                            pnL.ret(pnL.eFails(pnL.EncapsulateAsEventParameter(msg)))));
        }

        /// <summary>
        /// mko, 18.9.2018
        ///         
        /// mko, 26.5.2020
        /// Rückgabetyp von string auf IDocuEntity umgestellt
        /// </summary>
        /// <param name="Assembly"></param>
        /// <param name="ClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static IDocuEntity FormatErrMsg(string Assembly, string ClassName, string MethodName, IDocuEntity msg)
        {
            var now = DateTime.Now;
            return pnL.i($"{Assembly}.{ClassName}",
                        pnL.m(MethodName, 
                            pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                            pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                            pnL.ret(pnL.eFails(pnL.EncapsulateAsEventParameter(msg)))));
        }

        // Warnings

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        //public static string FormatWarningMsg(object Obj, string MethodName, PNDocuTerms.DocuEntities.IDocuEntity msg)
        //{
        //    var now = DateTime.Now;
        //    return fmt.Print(pnL.i($"{Obj.GetType().FullName}",
        //                pnL.m(MethodName,
        //                    pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)), 
        //                    pnL.ret(pnL.eWarn(pnL.EncapsulateAsEventParameter(msg))))));
        //}

        public static IDocuEntity FormatWarningMsg(object Obj, string MethodName, IDocuEntity msg)        
        {
            var now = DateTime.Now;
            return pnL.i($"{Obj.GetType().FullName}",
                        pnL.m(MethodName,
                            pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                            pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                            pnL.ret(pnL.eWarn(pnL.EncapsulateAsEventParameter(msg)))));
        }

        /// <summary>
        /// mko, 18.9.2018
        /// 
        /// mko, 26.5.2020
        /// Rückgabetyp von string auf IDocuEntity umgestellt
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static IDocuEntity FormatWarningMsg(string ClassName, string MethodName, IDocuEntity msg)
        {
            var now = DateTime.Now;
            return pnL.i(ClassName,
                        pnL.m(MethodName, 
                            pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                            pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                            pnL.ret(pnL.eWarn(pnL.EncapsulateAsEventParameter(msg)))));
        }

        /// <summary>
        /// mko, 18.9.2018
        /// 
        /// mko, 26.5.2020
        /// Rückgabetyp von string auf IDocuEntity umgestellt
        /// </summary>
        /// <param name="Assembly"></param>
        /// <param name="ClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static IDocuEntity FormatWarningMsg(string Assembly, string ClassName, string MethodName, IEventParameter msg)
        {
            var now = DateTime.Now;
            return  pnL.i($"{Assembly}.{ClassName}", 
                            pnL.m(MethodName,
                                pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                                pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                                pnL.ret(pnL.eWarn(msg))));
        }

        // Infos
        /// <summary>
        /// mko, 18.9.2018
        /// 
        /// mko, 26.5.2020
        /// Rückgabetyp von string auf IDocuEntity umgestellt
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static IDocuEntity FormatInfoMsg(object Obj, string MethodName, PNDocuTerms.DocuEntities.IEventParameter msg)
        {
            var now = DateTime.Now;
            return pnL.i(Obj.GetType().FullName, 
                    pnL.m(MethodName, 
                        pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                        pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                        pnL.ret(pnL.eInfo(msg))));
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static IDocuEntity FormatInfoMsg(string ClassName, string MethodName, PNDocuTerms.DocuEntities.IEventParameter msg)
        {
            var now = DateTime.Now;
            return pnL.i(ClassName, 
                    pnL.m(MethodName, 
                    pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                    pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                    pnL.ret(pnL.eInfo(msg))));
        }

        /// <summary>
        /// mko, 18.9.2018
        /// </summary>
        /// <param name="Assembly"></param>
        /// <param name="ClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static IDocuEntity FormatInfoMsg(string Assembly, string ClassName, string MethodName, IEventParameter msg)
        {
            var now = DateTime.Now;
            return pnL.i($"{Assembly}.{ClassName}", 
                        pnL.m(MethodName, 
                            pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                            pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                            pnL.ret(pnL.eInfo(msg))));
        }

        /// <summary>
        /// Erzeugt eine Fehlermeldung mit allen notwendigen Informationen wie Ort, Zeit, Fehlerbeschreibung
        /// 
        /// mko, 11.9.2018
        /// msg wir jetzt auf plx Struktur hin geprüft. Falls plx vorliegt, wird diese direkt als Inhalt verwendet.
        /// Sonst wird der Inhalt in ein plx- Text eingeschlossen.
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatErrMsg(object Obj, string MethodName, params string[] msgs)
        {
            var now = DateTime.Now;
            PNDocuTerms.DocuEntities.IDocuEntity pn = null;

            if (msgs.Length == 1)
            {
                var rcParse = PNDocuTerms.Parser.Parser.Parse(msgs[0], PNDocuTerms.Fn._);

                if (rcParse.Succeeded)
                {
                    pn = rcParse.Value;
                }
                else
                {
                    pn = pnL.i($"{Obj.GetType().FullName}",
                            pnL.m(MethodName,
                                pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                                pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                                pnL.ret(pnL.eFails(msgs[0]))));
                }
            }
            else
            {
                pn = pnL.i($"{Obj.GetType().FullName}",
                        pnL.m(MethodName,
                            pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                            pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                            pnL.ret(pnL.eFails(System.String.Join(" ", msgs)))));
            }

            var err = fmt.Print(pn);
            return err;
        }

        /// <summary>
        /// Erzeugt eine Fehlermeldung mit allen notwendigen Informationen wie Ort, Zeit, Fehlerbeschreibung
        /// 
        /// mko, 11.9.2018
        /// msg wir jetzt auf plx Struktur hin geprüft. Falls plx vorliegt, wird diese direkt als Inhalt verwendet.
        /// Sonst wird der Inhalt in ein plx- Text eingeschlossen.
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatErrMsg(string ClassName, string MethodName, params string[] msgs)
        {
            var now = DateTime.Now;

            PNDocuTerms.DocuEntities.IDocuEntity pn = null;

            if (msgs.Length == 1)
            {
                var rcParse = PNDocuTerms.Parser.Parser.Parse(msgs[0], PNDocuTerms.Fn._);

                if (rcParse.Succeeded)
                {
                    pn = rcParse.Value;
                }
                else
                {
                    pn = pnL.i(ClassName,
                            pnL.m(MethodName,
                                pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                                pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                                pnL.ret(pnL.eFails(msgs[0]))));
                }
            }
            else
            {
                pn = pnL.i(ClassName,
                        pnL.m(MethodName,
                            pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                            pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                            pnL.ret(pnL.eFails(System.String.Join(" ", msgs)))));
            }

            var err = fmt.Print(pn);
            return err;
        }

        /// <summary>
        /// Erzeugt eine Fehlermeldung mit allen notwendigen Informationen wie Ort, Zeit, Fehlerbeschreibung        
        ///  
        /// mko, 11.9.2018
        /// msg wir jetzt auf plx Struktur hin geprüft. Falls plx vorliegt, wird diese direkt als Inhalt verwendet.
        /// Sonst wird der Inhalt in ein plx- Text eingeschlossen.
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatErrMsg(string Assembly, string ClassName, string MethodName, params string[] msgs)
        {
            var now = DateTime.Now;

            PNDocuTerms.DocuEntities.IDocuEntity pn = null;

            if (msgs.Length == 1)
            {
                var rcParse = PNDocuTerms.Parser.Parser.Parse(msgs[0], PNDocuTerms.Fn._);

                if (rcParse.Succeeded)
                {
                    pn = rcParse.Value;
                }
                else
                {
                    pn = pnL.i($"{Assembly}.{ClassName}",
                        pnL.m(MethodName, 
                            pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                            pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                            pnL.ret(pnL.eFails(msgs[0]))));
                }
            }
            else
            {
                pn = pnL.i($"{Assembly}.{ClassName}",
                        pnL.m(MethodName, 
                            pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                            pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                            pnL.ret(pnL.eFails(System.String.Join(" ", msgs)))));
            }

            var err = fmt.Print(pn);
            return err;
        }



        /// <summary>
        /// Erzeugt eine Warnmeldung mit allen notwendigen Informationen wie Ort, Zeit, Beschreibung der Warnung      
        /// 
        /// mko, 11.9.2018
        /// msg wir jetzt auf plx Struktur hin geprüft. Falls plx vorliegt, wird diese direkt als Inhalt verwendet.
        /// Sonst wird der Inhalt in ein plx- Text eingeschlossen.
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatWarningMsg(object Obj, string MethodName, params string[] msgs)
        {
            var now = DateTime.Now;

            PNDocuTerms.DocuEntities.IDocuEntity pn = null;

            if (msgs.Length == 1)
            {
                var rcParse = PNDocuTerms.Parser.Parser.Parse(msgs[0], PNDocuTerms.Fn._);

                if (rcParse.Succeeded)
                {
                    pn = rcParse.Value;
                }
                else
                {
                    pn = pnL.i($"{Obj.GetType().FullName}",
                            pnL.m(MethodName,
                                pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                                pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                                pnL.ret(pnL.eWarn(msgs[0]))));
                }
            }
            else
            {
                pn = pnL.i($"{Obj.GetType().FullName}",
                        pnL.m(MethodName,
                            pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                            pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                            pnL.ret(pnL.eWarn(System.String.Join(" ", msgs)))));
            }

            var err = fmt.Print(pn);
            return err;
        }


        /// <summary>
        /// Erzeugt eine Warnmeldung mit allen notwendigen Informationen wie Ort, Zeit, Beschreibung der Warnung   
        /// 
        /// mko, 11.9.2018
        /// msg wir jetzt auf plx Struktur hin geprüft. Falls plx vorliegt, wird diese direkt als Inhalt verwendet.
        /// Sonst wird der Inhalt in ein plx- Text eingeschlossen.
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatWarningMsg(string ClassName, string MethodName, params string[] msgs)
        {
            var now = DateTime.Now;
            PNDocuTerms.DocuEntities.IDocuEntity pn = null;

            if (msgs.Length == 1)
            {
                var rcParse = PNDocuTerms.Parser.Parser.Parse(msgs[0], PNDocuTerms.Fn._);

                if (rcParse.Succeeded)
                {
                    pn = rcParse.Value;
                }
                else
                {
                    pn = pnL.i(ClassName,
                            pnL.m(MethodName, 
                                pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                                pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                                pnL.ret(pnL.eWarn(msgs[0]))));
                }
            }
            else
            {
                pn = pnL.i(ClassName,
                        pnL.m(MethodName,
                            pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                            pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                            pnL.ret(pnL.eWarn(System.String.Join(" ", msgs)))));
            }
            var err = fmt.Print(pn);
            return err;

        }


        /// <summary>
        /// Erzeugt eine Infomeldung mit allen notwendigen Informationen wie Ort, Zeit, Information
        /// 
        /// mko, 11.9.2018
        /// msg wir jetzt auf plx Struktur hin geprüft. Falls plx vorliegt, wird diese direkt als Inhalt verwendet.
        /// Sonst wird der Inhalt in ein plx- Text eingeschlossen.
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatInfoMsg(object Obj, string MethodName, params string[] msgs)
        {
            var now = DateTime.Now;

            PNDocuTerms.DocuEntities.IDocuEntity pn = null;

            if (msgs.Length == 1)
            {
                var rcParse = PNDocuTerms.Parser.Parser.Parse(msgs[0], PNDocuTerms.Fn._);

                if (rcParse.Succeeded)
                {
                    pn = rcParse.Value;
                }
                else
                {
                    pn = pnL.i($"{Obj.GetType().FullName}",
                            pnL.m(MethodName, 
                                pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                                pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                                pnL.ret(pnL.eInfo(msgs[0]))));
                }
            }
            else
            {
                pn = pnL.i($"{Obj.GetType().FullName}",
                            pnL.m(MethodName,
                                pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                                pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                                pnL.ret(pnL.eFails(System.String.Join(" ", msgs)))));
            }

            var err = fmt.Print(pn);
            return err;

        }

        /// <summary>
        /// Erzeugt eine Infomeldung mit allen notwendigen Informationen wie Ort, Zeit, Information
        /// 
        /// mko, 11.9.2018
        /// msg wir jetzt auf plx Struktur hin geprüft. Falls plx vorliegt, wird diese direkt als Inhalt verwendet.
        /// Sonst wird der Inhalt in ein plx- Text eingeschlossen.
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="MethodName"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public static string FormatInfoMsg(string ClassName, string MethodName, params string[] msgs)
        {

            var now = DateTime.Now;

            PNDocuTerms.DocuEntities.IDocuEntity pn = null;

            if (msgs.Length == 1)
            {
                var rcParse = PNDocuTerms.Parser.Parser.Parse(msgs[0], PNDocuTerms.Fn._);

                if (rcParse.Succeeded)
                {
                    pn = rcParse.Value;
                }
                else
                {
                    pn = pnL.i(ClassName, 
                            pnL.m(MethodName, 
                                pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                                pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                                pnL.ret(pnL.eInfo(msgs[0]))));
                }
            }
            else
            {
                pn = pnL.i(ClassName, 
                            pnL.m(MethodName, 
                                pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(now)),
                                pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(now.Hour, now.Minute, now.Second, now.Millisecond)),
                                pnL.ret(pnL.eInfo(System.String.Join(" ", msgs)))));
            }

            var err = fmt.Print(pn);
            return err;            
        }

        /// <summary>
        /// Wandelt eine Ketten von Ausnahmen (Ausnahme -> innere Ausnahme -> innere Ausnahme ...)
        /// in eine Zeichenkette um
        /// 
        /// mko, 26.9.2017
        /// Teile der Meldungen durch ; separiert
        /// 
        /// mko, 2.3.2018
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string FlattenExceptionMessages(Exception ex)
        {
            var rcParse = PNDocuTerms.Parser.Parser.Parse20_06(ex.Message, PNDocuTerms.Fn._, RCV3.pnL);

            if (rcParse.Succeeded)
            {
                if (ex.InnerException != null)
                {
                    return fmt.Print(pnL.i(ex.GetType().Name, 
                                pnL.p(ANC.DocuTerms.MetaData.Msg.UID, pnL.EncapsulateAsPropertyValue(rcParse.Value)),
                                pnL.p("inner", pnL.EncapsulateAsPropertyValue(FlattenExceptionMessagesPN(ex.InnerException)))));
                }
                else
                {
                    return fmt.Print(pnL.i(ex.GetType().Name, 
                                pnL.p(ANC.DocuTerms.MetaData.Msg.UID, pnL.EncapsulateAsPropertyValue(rcParse.Value))));
                }

            }
            else
            {
                if (ex.InnerException != null)
                {
                    return fmt.Print(pnL.i(ex.GetType().Name, 
                                pnL.p(ANC.DocuTerms.MetaData.Msg.UID, ex.Message),
                                pnL.p("inner", pnL.EncapsulateAsPropertyValue(FlattenExceptionMessagesPN(ex.InnerException)))));
                }
                else
                {
                    return fmt.Print(pnL.i(ex.GetType().Name,
                                pnL.p(ANC.DocuTerms.MetaData.Msg.UID, ex.Message)));
                }
            }
        }

        public static IDocuEntity FlattenExceptionMessagesPN(Exception ex)
        {
            try
            {
                if (ex is RCV3Exception rex3)
                {
                    return rex3.MessageAsDocuTerm;
                }
                else
                {

                    var rcParse = string.IsNullOrWhiteSpace(ex.Message) ? RCV3sV<IDocuEntity>.Failed(null) : PNDocuTerms.Parser.Parser.Parse20_06(ex.Message, PNDocuTerms.Fn._, RCV3.pnL);

                    if (rcParse.Succeeded)
                    {
                        if (ex.InnerException != null)
                        {
                            return pnL.i(ex.GetType().Name,
                                pnL.p(ANC.DocuTerms.MetaData.Msg.UID, pnL.EncapsulateAsPropertyValue(rcParse.Value)),
                                pnL.p("inner", pnL.EncapsulateAsPropertyValue(FlattenExceptionMessagesPN(ex.InnerException))));
                        }
                        else
                        {
                            return pnL.i(ex.GetType().Name,
                                pnL.p(ANC.DocuTerms.MetaData.Msg.UID, pnL.EncapsulateAsPropertyValue(rcParse.Value)));
                        }
                    }
                    else
                    {
                        if (ex.InnerException != null)
                        {
                            return pnL.i(ex.GetType().Name,
                                pnL.p(ANC.DocuTerms.MetaData.Msg.UID, string.IsNullOrEmpty(ex.Message) ? "" : ex.Message),
                                pnL.p("inner", pnL.EncapsulateAsPropertyValue(FlattenExceptionMessagesPN(ex.InnerException))));
                        }
                        else
                        {
                            return pnL.i(ex.GetType().Name,
                                pnL.p(ANC.DocuTerms.MetaData.Msg.UID, string.IsNullOrEmpty(ex.Message) ? "" : ex.Message));
                        }
                    }
                }
            }
            catch (Exception exx)
            {
                return pnL.m("FlattenExceptionMessagesPN",
                        pnL.p(ANC.DocuTerms.MetaData.Type.UID, ex.GetType().Name),
                        pnL.KillIf(string.IsNullOrWhiteSpace(ex.Message), () => (IMethodParameter)pnL.p("Message", ex.Message)),
                        pnL.ret(
                            pnL.IfElse(string.IsNullOrWhiteSpace(exx.Message), () => (IReturnValue)pnL.eFails(), () => pnL.eFails(exx.Message))));
                
            }
        }

        public static IInstance FlattenExceptionAsDocuTermInstance(Exception ex)
        {
            try
            {
                if (ex is RCV3Exception _rex3 && _rex3.MessageAsDocuTerm is IInstance i)
                {
                    return i;
                }
                else if(ex is RCV3Exception __rex3)
                {
                    return pnL.i(TT.Runtime.RuntimeErrorOfTypeException.UID,
                            pnL.p(TTD.MetaData.Type.UID, __rex3.GetType().Name),
                            pnL.p(TTD.MetaData.Msg.UID, pnL.EncapsulateAsPropertyValue(__rex3.MessageAsDocuTerm)));                        
                }
                else
                {

                    var rcParse = string.IsNullOrWhiteSpace(ex.Message) ? RCV3sV<IDocuEntity>.Failed(null) : PNDocuTerms.Parser.Parser.Parse20_06(ex.Message, PNDocuTerms.Fn._, RCV3.pnL);

                    if (rcParse.Succeeded)
                    {
                        if (ex.InnerException != null)
                        {
                            return pnL.i(TT.Runtime.RuntimeErrorOfTypeException.UID,
                                pnL.p(TTD.MetaData.Type.UID, ex.GetType().Name),
                                pnL.p(TTD.MetaData.Msg.UID, pnL.EncapsulateAsPropertyValue(rcParse.Value)),
                                pnL.p(TT.Runtime.RuntimeErrorOfTypeInnerException.UID, pnL.EncapsulateAsPropertyValue(FlattenExceptionMessagesPN(ex.InnerException))));
                        }
                        else
                        {
                            return pnL.i(TT.Runtime.RuntimeErrorOfTypeException.UID,
                                pnL.p(TTD.MetaData.Type.UID, ex.GetType().Name),                                
                                pnL.p(TTD.MetaData.Msg.UID, pnL.EncapsulateAsPropertyValue(rcParse.Value)));
                        }
                    }
                    else
                    {
                        if (ex.InnerException != null)
                        {
                            return pnL.i(TT.Runtime.RuntimeErrorOfTypeException.UID,
                                pnL.p(TTD.MetaData.Type.UID, ex.GetType().Name),
                                pnL.p(TTD.MetaData.Msg.UID, string.IsNullOrEmpty(ex.Message) ? "" : ex.Message),
                                pnL.p(TT.Runtime.RuntimeErrorOfTypeInnerException.UID, pnL.EncapsulateAsPropertyValue(FlattenExceptionMessagesPN(ex.InnerException))));
                        }
                        else
                        {
                            return pnL.i(ex.GetType().Name,
                                pnL.p(ANC.DocuTerms.MetaData.Msg.UID, string.IsNullOrEmpty(ex.Message) ? "" : ex.Message));
                        }
                    }
                }
            }
            catch (Exception exx)
            {
                return  
                    pnL.i(TT.Runtime.RuntimeErrorOfTypeException.UID,
                        pnL.p(TTD.MetaData.Type.UID, ex.GetType().Name),
                        pnL.m("FlattenExceptionMessagesPN",
                            pnL.p(TTD.MetaData.Type.UID, ex.GetType().Name),
                            pnL.KillIf(string.IsNullOrWhiteSpace(ex.Message), () => (IMethodParameter)pnL.p("Message", ex.Message)),
                            pnL.ret(
                                pnL.IfElse(string.IsNullOrWhiteSpace(exx.Message), () => (IReturnValue)pnL.eFails(), () => pnL.eFails(exx.Message)))));

            }
        }

    }
}