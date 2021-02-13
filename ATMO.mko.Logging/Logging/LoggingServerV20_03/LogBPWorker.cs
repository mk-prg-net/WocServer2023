using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using bp = mko.BatchProcessing;

namespace ATMO.mko.Logging.Logging
{
    /// <summary>
    /// mko, 17.3.2020
    /// Führt LogJobs aus.
    /// </summary>
    public class LogBPWorker : bp.IWorker
    {
        public LogBPWorker()
        {
        }

        public void doIt(bp.Job currentJob)
        {
            try
            {
                var obj = (LogJob)currentJob;

                obj.logAction?.Invoke(obj);
            }
            catch(System.Threading.ThreadAbortException)
            {
               System.Threading.Thread.ResetAbort();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            };

        }

        public bp.JobProgressInfo GetProgressInfo(bp.Job job)
        {
            return new bp.JobProgressInfo(job.JobId, job.JobState);
        }

        public bool setup(bp.Job currentJob)
        {
            return true;
        }
    }
}
