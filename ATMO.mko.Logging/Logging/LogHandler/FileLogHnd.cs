using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;

using ANC = MKPRG.Naming;


namespace ATMO.mko.Logging.Logging.LogHandler
{

    /// <summary>
    /// mko, 26.4.2011
    /// Appends log messages to a file.
    /// mko, 1.3.2018
    /// Formats Messages in polish notation
    /// 
    /// mko, 5.3.2019
    /// Instead of ILogHnd interface now implements ILoggingHandler.
    /// 
    /// mko, 14.1.2020
    /// Added public FileLogHnd(string FileName, IComposer pnL, IFormater fmt,  bool LogfilePerInstance = false) constructor.
    /// </summary>
    public class FileLogHnd : ILoggingHandler, IDisposable
    {
        StreamWriter writer;
        IComposer pnL;
        IFormater fmt { get; }

        bool firstCall = true;

        object MyLock = new object();

        public FileLogHnd(string FileName, IComposer pnL, bool LogfilePerInstance = false)
            :this(
                 FileName, 
                 pnL, 
                 new PNDocuTerms.DocuEntities.IndentedTextFormatter(PNDocuTerms.Fn._, RCV3.NC), 
                 LogfilePerInstance)
        {            
        }

        public FileLogHnd(string FileName, IComposer pnL, IFormater fmt,  bool LogfilePerInstance = false)
        {
            this.pnL = pnL;
            this.fmt = fmt;            

            if (LogfilePerInstance)
            {
                var dir = Path.GetDirectoryName(FileName);
                var fn = $"{Path.GetFileNameWithoutExtension(FileName)}.{Guid.NewGuid()}{Path.GetExtension(FileName)}";
                var fullName = string.IsNullOrEmpty(dir) ? fn : $"{dir}\\{fn}";
                writer = new StreamWriter(fullName, true);
            }
            else
            {
                writer = new StreamWriter(FileName, true);
            }

            if(writer != null)
                writer.AutoFlush = true;
        }


        /// <summary>
        /// Helper 
        /// </summary>
        /// <param name="obj"></param>
        void Write(ILogInfo18_12 obj)
        {
            writer.WriteLine(fmt.Print(pnL.i($"{obj.AssemblyName}.{obj.TypeName}.{obj.FunctionName}",
                                pnL.p(ANC.DocuTerms.MetaData.Type.UID, obj.LogType.ToString()),
                                pnL.p(ANC.TechTerms.Metrology.Counter.UID, obj.LogCounter),
                                pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(new TimeSpan(obj.TimeStamp.Hour, obj.TimeStamp.Minute, obj.TimeStamp.Second))),
                                pnL.p(ANC.DocuTerms.MetaData.Msg.UID, pnL.EncapsulateAsPropertyValue(obj.Msg)))));
            writer.Flush();
        }

        private void FirstCallInitialisation(ILogInfo18_12 obj)
        {
            if (firstCall)
            {
                writer.WriteLine(fmt.Print(pnL.i(ANC.TechTerms.Runtime.Session.UID, 
                                            pnL.p(ANC.TechTerms.Runtime.SessionId.UID, obj.SessionId), 
                                            pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.date(obj.TimeStamp)))));
                firstCall = false;
            }
        }

        public void Dispose()
        {
            writer.Flush();
            writer.Dispose();
        }

        public void Register(ILoggingServer loggingServer)
        {
            loggingServer.AppendToLogErrorsStream += LoggingServer_AppendToLogErrorsStream;
            loggingServer.AppendToLogInfosStream += LoggingServer_AppendToLogInfosStream;
            loggingServer.AppendToLogMissionCriticalEventsStream += LoggingServer_AppendToLogMissionCriticalEventsStream;
            loggingServer.AppendToLogStateStream += LoggingServer_AppendToLogStateStream;
            loggingServer.AppendToLogTelemetryStream += LoggingServer_AppendToLogTelemetryStream;
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
    }
}
