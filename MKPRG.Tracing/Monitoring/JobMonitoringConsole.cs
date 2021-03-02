using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Concurrent;

using TechTerms = MKPRG.Naming.TechTerms;
using static MKPRG.Tracing.DocuTerms.ComposerSubTrees;
using MKPRG.Tracing.DocuTerms;

using TTD = MKPRG.Naming.DocuTerms;
using TT = MKPRG.Naming.TechTerms;

namespace MKPRG.Tracing.Monitoring
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
    /// mko, 22.2.2021
    /// Integriert in MKPRG.Tracing
    /// 
    /// </summary>
    public class JobMonitoringConsole 
        : IJobMonitoring, 
        IJobMonitoringConsole,
        IJobMonitoringConsoleEvents
    {
        public JobMonitoringConsole(IComposer pnL)
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

        public RC<IEnumerable<IJob>> Jobs => RC<IEnumerable<IJob>>.Ok(_Jobs.Select(r => r.Value));

        public RC abortJob(long JobId)
        {
            var ret = RC.Failed(pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RC.Failed(JobIdNotFound(JobId));
            }
            else
            {
                _Jobs[JobId].State = JobState.aborted;
                _logQueue[JobId].Enqueue(pnL.m("JobAborted", pnL.p(TT.Timeline.TimeStamp.UID, DateTime.Now.ToString())));


                // Umgebung vom beantragten Jobabbruch benachrichtigen
                JobAbortRequestedEvent?.Invoke(_Jobs[JobId]);
                ret = RC.Ok(pnL);
            }

            return ret;
        }


        public RC<JobState> continueJob(long JobId)
        {

            var ret = RC<JobState>.Failed(JobState.none, ErrorDescription: pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RC<JobState>.Failed(JobState.aborted, ErrorDescription: JobIdNotFound(JobId));

            }
            else if (_Jobs[JobId].State == JobState.aborted)
            {
                ret = RC<JobState>.Failed(JobState.aborted, ErrorDescription: JobAbortedMsg(JobId));
            }
            else if(_Jobs[JobId].State == JobState.stopped)
            {
                _Jobs[JobId].State = JobState.running;
                _logQueue[JobId].Enqueue(pnL.m(TT.Runtime.Continue.UID, pnL.p(TT.Timeline.TimeStamp.UID, pnL.time(DateTime.Now.TimeOfDay))));

                // Umgebung von der Fortsetzung des zuvor gestoppten Jobs benachrichtigen
                JobContinueEvent?.Invoke(_Jobs[JobId]);
                ret = RC<JobState>.Ok(_Jobs[JobId].State);
            }
            else
            {
                // In allen anderen Fällen nichts tun, und mit Ok bestätigen.
                ret = RC<JobState>.Ok(_Jobs[JobId].State);
            }

            return ret;
        }

        private IDocuEntity JobIdNotFound(long JobId)
            => pnL.m(TT.Access.Fetch.UID,
                    pnL.p(TT.Access.Datasources.DataSource.UID, "JobList"),
                    pnL.p(TT.Runtime.Jobs.JobId.UID, pnL.integer(JobId)),
                    pnL.ret(pnL.eFails(TT.Search.NotFound.UID)));
        

        public RC<JobState> deregisterJob(long JobId)
        {
            var ret = RC<JobState>.Failed(value: JobState.none, ErrorDescription: pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RC<JobState>.Failed(JobState.aborted, ErrorDescription: JobIdNotFound(JobId));

            }            
            if(_Jobs[JobId].State == JobState.running)
            {
                ret = RC<JobState>.Failed(value: _Jobs[JobId].State,
                   ErrorDescription:
                   pnL.ReturnValidatePreconditionFailed(
                        pnL.m(TT.Operators.Relations.NotEq.UID,
                            pnL.p_NID(TTD.MetaData.Arg.UID, TTD.StateDescription.CurrentState.UID),
                            pnL.p(TTD.MetaData.Arg.UID, JobState.running.ToString()))));
            }
            else
            {
                _Jobs.TryRemove(JobId, out Job job);                
                job.State = JobState.completed;

                _logQueue.TryRemove(JobId, out ConcurrentQueue<IListMember> logList);                

                ret = RC<JobState>.Ok(job.State);
            }

            return ret;
        }

        public RC<long> registerJob(IListMember jobDescr, long estimatedEffort)
        {
            var job = new Job();
            job.JobId = System.Threading.Interlocked.Increment(ref _nextJobId);
            job.EstimatedEffort = estimatedEffort;
            job.JobDescr = jobDescr;
            job.Created = DateTime.Now;

            _Jobs[job.JobId] = job;
            _logQueue[job.JobId] = new ConcurrentQueue<IListMember>();
            _logQueue[job.JobId].Enqueue(pnL.eStart());

            return RC<long>.Ok(job.JobId);
        }

        public RC<JobState> reportProgess(long JobId, long progress)
        {
            var ret = RC<JobState>.Failed(value: JobState.none, ErrorDescription: pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RC<JobState>.Failed(JobState.aborted, ErrorDescription: JobIdNotFound(JobId));

            }
            else if (_Jobs[JobId].State == JobState.aborted)
            {
                ret = RC<JobState>.Failed(JobState.aborted, ErrorDescription: JobAbortedMsg(JobId));
            }
            else
            {
                var job = _Jobs[JobId];
                job.CurrentProgress += progress;
                ret = RC<JobState>.Ok(job.State);
            }

            return ret;
        }

        public RC<JobState> reportProgess(long JobId, long progress, IListMember logEntry)
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
        public RC<JobState> reportProgessAbsolute(long JobId, long progress)
        {
            var ret = RC<JobState>.Failed(value: JobState.none, ErrorDescription: pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RC<JobState>.Failed(JobState.aborted, ErrorDescription: JobIdNotFound(JobId));

            }
            else if (_Jobs[JobId].State == JobState.aborted)
            {
                ret = RC<JobState>.Failed(JobState.aborted, ErrorDescription: JobAbortedMsg(JobId));
            }
            else
            {
                var job = _Jobs[JobId];
                job.CurrentProgress = progress;
                ret = RC<JobState>.Ok(job.State);
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
        public RC<JobState> reportProgessAbsolute(long JobId, long progress, IListMember logEntry)
        {
            _logQueue[JobId].Enqueue(logEntry);
            return reportProgessAbsolute(JobId, progress);
        }

        public RC<JobState> stopJob(long JobId)
        {
            var ret = RC<JobState>.Failed(JobState.none, ErrorDescription: pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RC<JobState>.Failed(JobState.none, ErrorDescription: JobIdNotFound(JobId));

            }
            else if (_Jobs[JobId].State == JobState.aborted)
            {
                ret = RC<JobState>.Failed(_Jobs[JobId].State, ErrorDescription: JobAbortedMsg(JobId));
            }
            else
            {
                _Jobs[JobId].State = JobState.stopped;
                _logQueue[JobId].Enqueue(pnL.m(TT.Runtime.Stop.UID, pnL.p(TT.Timeline.TimeStamp.UID, pnL.time(DateTime.Now.TimeOfDay, true))));

                // Benachrichtigen der Umgebung, das Job getoppt wurde
                JobStoppedEvent?.Invoke(_Jobs[JobId]);
                ret = RC<JobState>.Ok(_Jobs[JobId].State);
            }

            return ret;

        }

        private IDocuEntity JobAbortedMsg(long JobId)
        {
            return pnL.m(TT.Runtime.Abort.UID,
                    pnL.p(TT.Grammar.Subject.UID, TT.Runtime.Jobs.Job.UID),
                    pnL.p(TT.Runtime.Jobs.JobId.UID, pnL.integer(JobId)));
        }

        public RC<JobState> completeJob(long JobId)
        {            
            var logList = pnL.List(_logQueue[JobId].ToArray());

            var ret = RC<JobState>.Failed(value: JobState.none, ErrorDescription: pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RC<JobState>.Failed(JobState.none, ErrorDescription: JobIdNotFound(JobId));
            }
            else if (_Jobs[JobId].State == JobState.aborted)
            {
                _Jobs[JobId].ResultDocu = logList;
                ret = RC<JobState>.Failed(_Jobs[JobId].State, ErrorDescription: JobAbortedMsg(JobId));
            }
            else
            {
                _Jobs[JobId].State = JobState.completed;
                _Jobs[JobId].ResultDocu = logList;
                _Jobs[JobId].Completed = DateTime.Now;

                // Benachrichtigen der Umgebung, das Job fertiggestellt wurde
                JobCompletedEvent?.Invoke(_Jobs[JobId]);
                ret = RC<JobState>.Ok(_Jobs[JobId].State);
            }

            return ret;
        }

        /// <summary>
        /// mko, 15.11.2019
        /// </summary>
        /// <param name="JobId"></param>
        /// <param name="docuTerm"></param>
        /// <returns></returns>
        public RC<JobState> completeJob(long JobId, IListMember docuTerm)
        {
            _logQueue[JobId].Enqueue(docuTerm);
            var logList = pnL.List(_logQueue[JobId].ToArray());

            var ret = RC<JobState>.Failed(value: JobState.none, ErrorDescription: pnL.eFails());
            if (!_Jobs.ContainsKey(JobId))
            {
                ret = RC<JobState>.Failed(JobState.none, ErrorDescription: JobIdNotFound(JobId));
            }
            else if (_Jobs[JobId].State == JobState.aborted)
            {
                _Jobs[JobId].ResultDocu = logList;
                ret = RC<JobState>.Failed(_Jobs[JobId].State, ErrorDescription: JobAbortedMsg(JobId));
            }
            else
            {
                _Jobs[JobId].State = JobState.completed;
                _Jobs[JobId].ResultDocu = logList;

                // Benachrichtigen der Umgebung, das Job fertiggestellt wurde
                JobCompletedEvent?.Invoke(_Jobs[JobId]);
                ret = RC<JobState>.Ok(_Jobs[JobId].State);
            }

            return ret;
        }
    }
}
