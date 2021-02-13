using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Concurrent;

using TechTerms = ATMO.mko.Logging.PNDocuTerms.DocuEntities.Composer.TechTerms;
using static ATMO.mko.Logging.PNDocuTerms.DocuEntities.ComposerSubTrees;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;

using TTD = MKPRG.Naming.DocuTerms;
using TT = MKPRG.Naming.TechTerms;

namespace ATMO.mko.Logging.Monitoring
{
    /// <summary>
    /// Implementierung  einer einfachen Jobverwaltung
    /// 
    /// mko, 25.5.2020
    /// IJobMonitoringConsoleEvents implementiert.
    /// 
    /// mko, 6.10.2020
    /// Verhalten der Funktionen erweitert um Aufzeichnung von Logmeldungen während eines Jobs. Die Logmeldungen 
    /// werden am Ende in einer DokuTerm- Liste zusammengefasst und in die  ResultDocu-Eigenschaft des Jobs kopiert.
    /// 
    /// </summary>
    public class JobMonitoringConsole 
        : IJobMonitoring, 
        IJobMonitoringConsole,
        IJobMonitoringConsoleEvents
    {
        public JobMonitoringConsole(PNDocuTerms.DocuEntities.IComposer pnL)
        {
            this.pnL = pnL;
        }

        IComposer pnL;
        long _nextJobId = 1;
        ConcurrentQueue<Job> _newJobQueue = new ConcurrentQueue<Job>();
        ConcurrentDictionary<long, Job> _Jobs = new ConcurrentDictionary<long, Job>();

        /// <summary>
        /// mko, 6.10.2020
        /// </summary>
        ConcurrentDictionary<long, ConcurrentQueue<IListMember>> _logQueue = new ConcurrentDictionary<long, ConcurrentQueue<IListMember>>();

        // Implementierung von IJobMonitoringConsoleEvents
        public event Action<IJob> JobAbortRequestedEvent;
        public event Action<IJob> JobCompletedEvent;
        public event Action<IJob> JobStoppedEvent;
        public event Action<IJob> JobContinueEvent;

        public RCV3sV<IEnumerable<IJob>> Jobs => RCV3sV<IEnumerable<IJob>>.Ok(_Jobs.Select(r => r.Value));

        public RCV3 abortJob(long JobId)
        {
            var ret = RCV3.Failed(pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RCV3.Failed(JobIdNotFound(JobId));
            }
            else
            {
                _Jobs[JobId].State = JobState.aborted;
                _logQueue[JobId].Enqueue(pnL.m("JobAborted", pnL.p(TT.Timeline.TimeStamp.UID, DateTime.Now.ToString())));


                // Umgebung vom beantragten Jobabbruch benachrichtigen
                JobAbortRequestedEvent?.Invoke(_Jobs[JobId]);
                ret = RCV3.Ok();
            }

            return ret;
        }


        public RCV3sV<JobState> continueJob(long JobId)
        {

            var ret = RCV3sV<JobState>.Failed(JobState.none, pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RCV3sV<JobState>.Failed(JobState.aborted,JobIdNotFound(JobId));

            }
            else if (_Jobs[JobId].State == JobState.aborted)
            {
                ret = RCV3sV<JobState>.Failed(JobState.aborted, JobAbortedMsg(JobId));
            }
            else if(_Jobs[JobId].State == JobState.stopped)
            {
                _Jobs[JobId].State = JobState.running;
                _logQueue[JobId].Enqueue(pnL.m("JobContinued", pnL.p(TT.Timeline.TimeStamp.UID, DateTime.Now.ToString())));

                // Umgebung von der Fortsetzung des zuvor gestoppten Jobs benachrichtigen
                JobContinueEvent?.Invoke(_Jobs[JobId]);
                ret = RCV3sV<JobState>.Ok(_Jobs[JobId].State);
            }
            else
            {
                // In allen anderen Fällen nichts tun, und mit Ok bestätigen.
                ret = RCV3sV<JobState>.Ok(_Jobs[JobId].State);
            }

            return ret;
        }

        private PNDocuTerms.DocuEntities.IDocuEntity JobIdNotFound(long JobId)
        {
            return pnL.i("JobList",
                            pnL.m("get",
                                    pnL.p("JobId", pnL.txt(JobId.ToString())),
                                    pnL.ret(pnL.eFails("NotFound"))));
        }

        public RCV3sV<JobState> deregisterJob(long JobId)
        {
            var ret = RCV3sV<JobState>.Failed(value: JobState.none, ErrorDescription: pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RCV3sV<JobState>.Failed(JobState.aborted, JobIdNotFound(JobId));

            }            
            if(_Jobs[JobId].State == JobState.running)
            {
                ret = RCV3sV<JobState>.Failed(value: _Jobs[JobId].State,
                   ErrorDescription:
                   pnL.ReturnValidatePreconditionFailed(
                        pnL.m(TechTerms.RelationalOperators.mNotEq,
                            pnL.p(TechTerms.MetaData.Arg, TechTerms.StateMachine.State),
                            pnL.p(TechTerms.MetaData.Val, JobState.running.ToString()))));
            }
            else
            {
                _Jobs.TryRemove(JobId, out Job job);                
                job.State = JobState.completed;

                _logQueue.TryRemove(JobId, out ConcurrentQueue<IListMember> logList);                

                ret = RCV3sV<JobState>.Ok(job.State);
            }

            return ret;
        }

        public RCV3sV<long> registerJob(string title, long estimatedEffort)
        {
            var job = new Job();
            job.JobId = System.Threading.Interlocked.Increment(ref _nextJobId);
            job.EstimatedEffort = estimatedEffort;
            job.Title = title;
            job.Created = DateTime.Now;

            _Jobs[job.JobId] = job;
            _logQueue[job.JobId] = new ConcurrentQueue<IListMember>();
            _logQueue[job.JobId].Enqueue(pnL.eStart());

            return RCV3sV<long>.Ok(job.JobId);
        }

        public RCV3sV<JobState> reportProgess(long JobId, long progress)
        {
            var ret = RCV3sV<JobState>.Failed(value: JobState.none, ErrorDescription: pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RCV3sV<JobState>.Failed(JobState.aborted, JobIdNotFound(JobId));

            }
            else if (_Jobs[JobId].State == JobState.aborted)
            {
                ret = RCV3sV<JobState>.Failed(JobState.aborted, JobAbortedMsg(JobId));
            }
            else
            {
                var job = _Jobs[JobId];
                job.CurrentProgress += progress;
                ret = RCV3sV<JobState>.Ok(job.State);
            }

            return ret;
        }

        public RCV3sV<JobState> reportProgess(long JobId, long progress, IListMember logEntry)
        {            
            _logQueue[JobId].Enqueue(logEntry);
            return reportProgess(JobId, progress);
        }

        /// <summary>
        /// mko, 18.11.2019
        /// </summary>
        /// <param name="JobId"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public RCV3sV<JobState> reportProgessAbsolute(long JobId, long progress)
        {
            var ret = RCV3sV<JobState>.Failed(value: JobState.none, ErrorDescription: pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RCV3sV<JobState>.Failed(JobState.aborted, JobIdNotFound(JobId));

            }
            else if (_Jobs[JobId].State == JobState.aborted)
            {
                ret = RCV3sV<JobState>.Failed(JobState.aborted, JobAbortedMsg(JobId));
            }
            else
            {
                var job = _Jobs[JobId];
                job.CurrentProgress = progress;
                ret = RCV3sV<JobState>.Ok(job.State);
            }

            return ret;

        }

        /// <summary>
        /// mko, 6.10.2020
        /// </summary>
        /// <param name="JobId"></param>
        /// <param name="progress"></param>
        /// <param name="logEntry"></param>
        /// <returns></returns>
        public RCV3sV<JobState> reportProgessAbsolute(long JobId, long progress, IListMember logEntry)
        {
            _logQueue[JobId].Enqueue(logEntry);
            return reportProgessAbsolute(JobId, progress);
        }

        public RCV3sV<JobState> stopJob(long JobId)
        {
            var ret = RCV3sV<JobState>.Failed(JobState.none, pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RCV3sV<JobState>.Failed(JobState.none, JobIdNotFound(JobId));

            }
            else if (_Jobs[JobId].State == JobState.aborted)
            {
                ret = RCV3sV<JobState>.Failed(_Jobs[JobId].State, JobAbortedMsg(JobId));
            }
            else
            {
                _Jobs[JobId].State = JobState.stopped;
                _logQueue[JobId].Enqueue(pnL.m("JobStopped", pnL.p(TT.Timeline.TimeStamp.UID, DateTime.Now.ToString())));

                // Benachrichtigen der Umgebung, das Job getoppt wurde
                JobStoppedEvent?.Invoke(_Jobs[JobId]);
                ret = RCV3sV<JobState>.Ok(_Jobs[JobId].State);
            }

            return ret;

        }

        private IDocuEntity JobAbortedMsg(long JobId)
        {
            return pnL.i("Job", pnL.p("JobId", pnL.txt(JobId.ToString())), pnL.p("State", pnL.txt("aborted")));
        }

        public RCV3sV<JobState> completeJob(long JobId)
        {            
            var logList = pnL.List(_logQueue[JobId].ToArray());

            var ret = RCV3sV<JobState>.Failed(value: JobState.none, ErrorDescription: pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RCV3sV<JobState>.Failed(JobState.none, JobIdNotFound(JobId));
            }
            else if (_Jobs[JobId].State == JobState.aborted)
            {
                _Jobs[JobId].ResultDocu = logList;
                ret = RCV3sV<JobState>.Failed(_Jobs[JobId].State, JobAbortedMsg(JobId));
            }
            else
            {
                _Jobs[JobId].State = JobState.completed;
                _Jobs[JobId].ResultDocu = logList;
                _Jobs[JobId].Completed = DateTime.Now;

                // Benachrichtigen der Umgebung, das Job fertiggestellt wurde
                JobCompletedEvent?.Invoke(_Jobs[JobId]);
                ret = RCV3sV<JobState>.Ok(_Jobs[JobId].State);
            }

            return ret;
        }

        /// <summary>
        /// mko, 15.11.2019
        /// </summary>
        /// <param name="JobId"></param>
        /// <param name="docuTerm"></param>
        /// <returns></returns>
        public RCV3sV<JobState> completeJob(long JobId, IListMember docuTerm)
        {
            _logQueue[JobId].Enqueue(docuTerm);
            var logList = pnL.List(_logQueue[JobId].ToArray());

            var ret = RCV3sV<JobState>.Failed(value: JobState.none, ErrorDescription: pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RCV3sV<JobState>.Failed(JobState.none, JobIdNotFound(JobId));
            }
            else if (_Jobs[JobId].State == JobState.aborted)
            {
                _Jobs[JobId].ResultDocu = logList;
                ret = RCV3sV<JobState>.Failed(_Jobs[JobId].State, JobAbortedMsg(JobId));
            }
            else
            {
                _Jobs[JobId].State = JobState.completed;
                _Jobs[JobId].ResultDocu = logList;

                // Benachrichtigen der Umgebung, das Job fertiggestellt wurde
                JobCompletedEvent?.Invoke(_Jobs[JobId]);
                ret = RCV3sV<JobState>.Ok(_Jobs[JobId].State);
            }

            return ret;
        }
    }
}
