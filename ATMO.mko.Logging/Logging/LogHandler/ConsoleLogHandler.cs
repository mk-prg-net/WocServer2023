using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using TechTerms = ATMO.mko.Logging.PNDocuTerms.DocuEntities.Composer.TechTerms;

namespace ATMO.mko.Logging.Logging.LogHandler
{
    /// <summary>
    /// mko, 5.3.2019
    /// 
    /// mko, 20.12.2019
    /// Formaieren der Ausgabe in einem Wunschformat
    /// </summary>
    public class ConsoleLogHandler : ILoggingHandler, IDisposable
    {
        IComposer pnL;
        IFormater fmt;

        bool firstCall = true;

        object MyLock = new object();

        public ConsoleLogHandler(IComposer pnL, IFormater fmt)
        {
            this.pnL = pnL;
            this.fmt = fmt;
        }

        public void Register(ILoggingServer loggingServer)
        {
            loggingServer.AppendToLogErrorsStream += LoggingServer_AppendToLogErrorsStream;
            loggingServer.AppendToLogInfosStream += LoggingServer_AppendToLogInfosStream;
            loggingServer.AppendToLogMissionCriticalEventsStream += LoggingServer_AppendToLogMissionCriticalEventsStream;
            loggingServer.AppendToLogStateStream += LoggingServer_AppendToLogStateStream;
            loggingServer.AppendToLogTelemetryStream += LoggingServer_AppendToLogTelemetryStream;
        }

        private void FirstCallInitialisation(ILogInfo18_12 obj)
        {
            if (firstCall)
            {
                Console.WriteLine(fmt.Print(pnL.i(DFC.Naming.TechTerms.Runtime.Session.UID, 
                                                    pnL.p(DFC.Naming.TechTerms.Runtime.SessionId.UID, obj.SessionId), 
                                                    pnL.p(DFC.Naming.TechTerms.Timeline.DateStamp.UID, pnL.date(obj.TimeStamp)))));
                firstCall = false;
            }
        }


        /// <summary>
        /// Helper 
        /// </summary>
        /// <param name="obj"></param>
        void Write(ILogInfo18_12 obj)
        {
            Console.WriteLine(fmt.Print(
                            pnL.i($"{obj.AssemblyName}.{obj.TypeName}.{obj.FunctionName}",
                                pnL.p(DFC.Naming.DocuTerms.MetaData.Type.UID, obj.LogType.ToString()),
                                pnL.p(DFC.Naming.TechTerms.Metrology.Counter.UID, obj.LogCounter),
                                pnL.p(DFC.Naming.TechTerms.Timeline.TimeStamp.UID, pnL.time(obj.TimeStamp.Hour, obj.TimeStamp.Minute, obj.TimeStamp.Second)),
                                pnL.KillIfNot(obj.Msg is IPropertyValue, 
                                                () => pnL.p(DFC.Naming.DocuTerms.MetaData.Msg.UID, (IPropertyValue)obj.Msg)))));
        }


        private void LoggingServer_AppendToLogTelemetryStream(ILogInfo18_12 obj)
        {
            lock (MyLock)
            {
                FirstCallInitialisation(obj);
                Write(obj);
            }
        }

        private void LoggingServer_AppendToLogStateStream(ILogInfo18_12 obj)
        {
            lock (MyLock)
            {
                FirstCallInitialisation(obj);
                Write(obj);
            }
        }

        private void LoggingServer_AppendToLogMissionCriticalEventsStream(ILogInfo18_12 obj)
        {
            lock (MyLock)
            {
                FirstCallInitialisation(obj);
                Write(obj);
            }
        }

        private void LoggingServer_AppendToLogInfosStream(ILogInfo18_12 obj)
        {
            lock (MyLock)
            {
                FirstCallInitialisation(obj);
                Write(obj);
            }
        }

        private void LoggingServer_AppendToLogErrorsStream(ILogInfo18_12 obj)
        {
            lock (MyLock)
            {
                FirstCallInitialisation(obj);
                Write(obj);
            }
        }

        public void Dispose()
        {
            Console.OpenStandardOutput().Flush();
            Console.OpenStandardError().Flush();
        }
    }
}
