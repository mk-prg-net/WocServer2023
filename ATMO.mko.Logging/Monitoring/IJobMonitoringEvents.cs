using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.Monitoring
{
    /// <summary>
    /// mko, 25.5.2020
    /// Ereignisse, die Lebenszyklus von Jobs durch die Jobüberwachung gefeuert werden können.
    /// </summary>
    public interface IJobMonitoringConsoleEvents
    {
        /// <summary>
        /// wird gefeuert, wenn der Abbruch des betroffene Job vom Benutzer eingeleitet wurde 
        /// </summary>
        event Action<IJob> JobAbortRequestedEvent;

        /// <summary>
        /// wird gefeuert, wenn der betroffene Job beendet wurde.
        /// </summary>
        event Action<IJob> JobCompletedEvent;

        /// <summary>
        /// wird gefeuert, wenn die Ausführung des Jobs vom Benutzer vorübergehend angehalten wurde
        /// </summary>
        event Action<IJob> JobStoppedEvent;

        /// <summary>
        /// wird gefeuert, wenn ein zuvor vorübergehend angehaltener Job wieder fortgesetzt wird.
        /// </summary>
        event Action<IJob> JobContinueEvent;
    }
}
