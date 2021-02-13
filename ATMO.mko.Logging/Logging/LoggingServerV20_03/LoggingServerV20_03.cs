using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using bp = mko.BatchProcessing;

namespace ATMO.mko.Logging.Logging
{

    //<unit_header>
    //----------------------------------------------------------------
    //
    // Martin Korneffel: IT Beratung/Softwareentwicklung
    // Stuttgart, den 18.2.2008
    //
    //  Projekt.......: mko
    //  Name..........: LogServer.cs
    //  Aufgabe/Fkt...: Klasse zur Protokollierung von Status-, Info- und Fehlermeldungen.
    //                  Protokollmethoden sind nach Meldungstyp gegliedert, und unabhängig
    //                  vom Protokollmedium. Das Protokollmedium wird über sog. "EventLogHandler"
    //                  bereitgestellt.
    //
    //
    //<unit_environment>
    //------------------------------------------------------------------
    //  Zielmaschine..: PC 
    //  Betriebssystem: Windows XP mit .NET 2.0
    //  Werkzeuge.....: Visual Studio 2005
    //  Autor.........: Martin Korneffel (mko)
    //  Version 1.0...: 2004
    //
    // </unit_environment>
    //
    //<unit_history>
    //------------------------------------------------------------------
    //
    //  Version.......: 1.1
    //  Autor.........: Martin Korneffel (mko)
    //  Datum.........: 13.5.2009
    //  Änderungen....: Protokollmethoden für ILogInfo hinzugefügt
    //
    //  Version.......: 2.0
    //  Autor.........: Martin Korneffel (mko)
    //  Datum.........: 14.7.2009
    //  Änderungen....: Klasse umbenannt von CLog in LogServer
    //
    //  Version.......: 2.1
    //  Autor.........: Martin Korneffel (mko)
    //  Datum.........: 13.2.2018
    //  Änderungen....: Erweitert um die Eigenschaft User. Für diesen erfolgen standardmäßig die Logmeldungen.
    //
    //  Version.......: 2.1
    //  Autor.........: Martin Korneffel (mko)
    //  Datum.........: 8.3.2018
    //  Änderungen....: Log- Zähler wird jetzt verwaltet. Für jede Meldung wird ein Zähler erhöht, und an die Loghandler
    //                  mitgegeben. so wird eine chronologische Reihenfolge aufrechterhalten, da Zeitstempel in der Regel zu ungenau sind.
    //
    //  Version.......: 18.12.x
    //  Autor.........: Martin Korneffel (mko)
    //  Datum.........: 19.12.2018
    //  Änderungen....: Komplette Neuimplementierung: 
    //                  Auftrennen von Spezifikation und Implementierung durch Definition einer Schnittstelle.
    //                  Klassifizierung der Logmeldungen ist jetzt an die Anforderungen von DFC angepasst.
    //                  Log werden asynchron im Hintergrund geschrieben. 
    //
    //  Version.......: 19.12.x
    //  Autor.........: Martin Korneffel (mko)
    //  Datum.........: 20.12.2019
    //  Änderungen....: Logs können jetzt wahlweise synchron oder asynchron im Hintergrund geschrieben werden.
    //
    //  Version.......: 20.3.x
    //  Autor.........: Martin Korneffel (mko)
    //  Datum.........: 09.03.2020
    //  Änderungen....: Log- funktion wurde mittels lock multithreadfest gemacht.
    //
    //  Version.......: 20.3.x
    //  Autor.........: Martin Korneffel (mko)
    //  Datum.........: 17.03.2020
    //  Änderungen....: LogServer auf Basis einer Stapelverarbeitung.
    //                  Log- Meldungen werden als Jobs auf einen Stapel gelegt (Batch), der dann asynchron von einer Satpelverarbeitung bearbeitet wird.
    //                  
    //</unit_history>
    //</unit_header>    



    /// <summary>
    /// mko, 17.3.2020
    /// LogServer auf Basis einer Stapelverarbeitung.
    /// Log- Meldungen werden als Jobs auf einen Stapel gelegt (Batch), der dann asynchron von einer Satpelverarbeitung bearbeitet wird.    
    /// </summary>
    public class LoggingServerV20_03
        : bp.BatchProcessor<LogBPWorker>, ILoggingServer
    {

        global::mko.BatchProcessing.IBatchProcessing meAsBP;

        public event Action<ILogInfo18_12> AppendToLogStateStream;
        public event Action<ILogInfo18_12> AppendToLogErrorsStream;
        public event Action<ILogInfo18_12> AppendToLogInfosStream;
        public event Action<ILogInfo18_12> AppendToLogTelemetryStream;
        public event Action<ILogInfo18_12> AppendToLogMissionCriticalEventsStream;


        Object myLock = new object();

        public LoggingServerV20_03(long SessionId)
            : base(new global::mko.Log.LogServer(), new LogBPWorker())
        {
            this.SessionId = SessionId;
            base.log.registerLogHnd(new global::mko.Log.DebugLogHandler());
            meAsBP = this;
        }


        public long SessionId { get; private set; }

        /// <summary>
        /// mko, 19.12.2018
        /// Fortlaufende Nummer der Log- Einträge
        /// </summary>
        public long LogCounter
        {
            get
            {
                // mko, 21.1.2021
                return System.Threading.Interlocked.Read(ref _LogCounter);
            }
        }
        long _LogCounter;

        public class Info : ILogInfo18_12
        {
            public DateTime TimeStamp { get; set; }

            public long SessionId { get; set; }

            public EnumLogTypeDFC LogType { get; set; }

            public long LogCounter { get; set; }

            public string AssemblyName { get; set; }

            public string TypeName { get; set; }

            public string FunctionName { get; set; }

            public IDocuEntity Msg { get; set; }
        }


        public void Log(EnumLogTypeDFC logType, IDocuEntity docuEntity)
        {
            LogImpl(logType, docuEntity, SessionId);
        }

        public void Log(EnumLogTypeDFC logType, IDocuEntity docuEntity, long SessionId)
        {
            LogImpl(logType, docuEntity, SessionId);
        }


        private void LogImpl(EnumLogTypeDFC logType, IDocuEntity docuEntity, long SessionId)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            lock (myLock)
            {
                var logCounter = System.Threading.Interlocked.Increment(ref _LogCounter);

                // Lognachricht erzeugen
                var info = new Info()
                {
                    TimeStamp = DateTime.Now,
                    SessionId = SessionId,
                    LogType = logType,
                    LogCounter = logCounter,
                    AssemblyName = assembly,
                    TypeName = cls,
                    FunctionName = mth.Name,
                    Msg = docuEntity                    
                };

                var jobId = meAsBP.NewJobId();

                LogJob job;

                switch (logType)
                {
                    case EnumLogTypeDFC.Error:
                        job = new LogJob(jobId, info, AppendToLogErrorsStream);
                        meAsBP.pushJob(job);
                        break;
                    case EnumLogTypeDFC.Info:
                        job = new LogJob(jobId, info, AppendToLogInfosStream);
                        meAsBP.pushJob(job);
                        break;
                    case EnumLogTypeDFC.Log:
                        job = new LogJob(jobId, info, AppendToLogMissionCriticalEventsStream);
                        meAsBP.pushJob(job);
                        break;
                    case EnumLogTypeDFC.State:
                        job = new LogJob(jobId, info, AppendToLogStateStream);
                        meAsBP.pushJob(job);
                        break;
                    case EnumLogTypeDFC.Telemetry:
                        job = new LogJob(jobId, info, AppendToLogTelemetryStream);
                        meAsBP.pushJob(job);
                        break;
                    default:
                        { }
                        break;
                }
            }
        }

        public void SetSessionId(long SessionId)
        {
            this.SessionId = SessionId;
        }

        /// <summary>
        /// mko, 14.7.2020
        /// Keine Implementierung, da UserId nicht genutzt wird
        /// </summary>
        /// <param name="userId"></param>
        public void SetUserId(string userId)
        {
            
        }
    }
}
