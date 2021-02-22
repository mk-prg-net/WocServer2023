using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKPRG.Tracing.DocuTerms;

namespace MKPRG.Tracing.Monitoring
{
    /// <summary>
    /// mko, 8.11.2018
    /// Trheadsafe implementation of a Job
    /// </summary>
    public class Job : IJob
    {
        public Job()
        {
            // mko, 8.3.2019
            // Startet immer im Zustand running
            _JobState = JobState.running;
            _duration = TimeSpan.Zero;

        }

        long _JobId;
        JobState _JobState;
        IListMember _JobDescr;
        long _EstimatedEffort;
        long _CurrentProgress;
        DateTime _Created;
        IDocuEntity _docuTerm;
        TimeSpan _duration;

        public long JobId {
            get
            {
                lock(this)
                {
                    return _JobId;
                }
            }
            set
            {
                lock (this)
                {
                    _JobId = value;
                }
            }
        }

        public JobState State {

            get
            {
                lock (this)
                {
                    return _JobState;
                }
            }

            set
            {
                lock (this)
                {
                    // Prüfen, ob Zustandsübergang gültig ist. Sonst vrebleibt Job im alten Zustand !
                    if (   (_JobState != JobState.running || value == JobState.stopped || value == JobState.aborted || value == JobState.completed)
                        && (_JobState != JobState.stopped || (value == JobState.aborted || value == JobState.completed || value == JobState.running))
                        && (_JobState != JobState.aborted || (value == JobState.aborted || value == JobState.completed))
                        && (_JobState != JobState.completed || value == JobState.completed))
                    {
                        if (_JobState == JobState.running && (value == JobState.stopped || value == JobState.completed || value == JobState.aborted))
                        {
                            Completed = DateTime.Now;
                            _duration = new TimeSpan(Completed.Ticks - Created.Ticks);
                        }

                        // state transist to stopped only possible if current JobState is running
                        // if jobstate is aborted no statuschange is possible
                        _JobState = value;
                    }                     
                }                
            }
        }

        public IListMember JobDescr {
            get
            {
                lock (this)
                {
                    return _JobDescr;
                }
            }
            set
            {
                lock (this)
                {
                    _JobDescr = value;
                }
            }
        }

        public long EstimatedEffort {
            get
            {
                lock (this)
                {
                    return _EstimatedEffort;
                }
            }
            set
            {
                lock (this)
                {
                    _EstimatedEffort = value;
                }
            }
        }

        public long CurrentProgress {
            get
            {
                lock (this)
                {
                    return _CurrentProgress;
                }
            }
            set
            {
                lock (this)
                {
                    _CurrentProgress = value;
                }
            }
        }

        public DateTime Created
        {
            get
            {
                lock (this)
                {
                    return _Created;
                }

            }
            set
            {
                lock (this)
                {
                    _Created = value;
                    _Completed = value;
                }
            }
        }

        DateTime _Completed; 
        public DateTime Completed
        {
            get
            {
                lock (this)
                {
                    return _Completed;
                }

            }
            set
            {
                lock (this)
                {
                    _Completed = value;
                }
            }
        }


        public int CurrentProgressInPercent => (int)(100.0 * CurrentProgress / EstimatedEffort);


        /// <summary>
        /// mko, 15.11.2019
        /// </summary>
        public IDocuEntity ResultDocu
        {
            get
            {
                lock (this)
                {
                    return _docuTerm;
                }
            }
            set
            {
                lock (this)
                {
                    _docuTerm = value;
                }
            }
        }

        public TimeSpan Duration => _duration;
    }
}
