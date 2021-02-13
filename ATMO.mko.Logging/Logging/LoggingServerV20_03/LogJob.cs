using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using bp = mko.BatchProcessing;

namespace ATMO.mko.Logging.Logging
{
    /// <summary>
    /// mko, 17.3.2020
    /// Logmeldung als Auftrag für einen LogServer
    /// </summary>
    public class LogJob : bp.Job, ILogInfo18_12
    {

        public LogJob(int JobId, ILogInfo18_12 log, Action<ILogInfo18_12> logAction)
        {
            this.JobId = JobId;
            this.JobPriority = JobPriorities.Normal;

            this.logAction = logAction;

            // Die fertigen Jobs müssen nicht abgeholt werden und können 
            this.OneWay = true;

            TimeStamp = log.TimeStamp;
            SessionId = log.SessionId;
            LogType = log.LogType;
            LogCounter = log.LogCounter;
            AssemblyName = log.AssemblyName;
            TypeName = log.TypeName;
            FunctionName = log.FunctionName;
            Msg = log.Msg;
        }


        public Action<ILogInfo18_12> logAction { get; }

        public DateTime TimeStamp { get; }

        public long SessionId { get; }

        public EnumLogTypeDFC LogType { get; }

        public long LogCounter { get; }

        public string AssemblyName { get; }

        public string TypeName { get; }

        public string FunctionName { get; }

        public IDocuEntity Msg { get; }
    }
}
