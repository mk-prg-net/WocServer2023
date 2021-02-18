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
    /// Zulässige Zustandsübergänge:
    /// 
    ///   +--: running --+--------------------------------------+--+--: completed
    ///   |              +-----------------+--+--: aborted --+--+  |
    ///   |              |                 |  +--------------+     |
    ///   |              +--: stopped --+--+-----------------------+  
    ///   |                             |
    ///   +-----------------------------+
    /// </summary>
    public enum JobState
    {
        none,
        running,
        completed,
        aborted,
        stopped
    }

    /// <summary>
    /// mko, 8.11.2018
    /// Beschreibt Zustand eines aktuell in Bearbeitung befindlichen DFC- Auftrages
    /// 
    /// mko, 15.11.2019
    /// Das Ergebnis eines Jobs kann jetzt durch einen Docuterm dokumentiert/bewertet werden.
    /// </summary>
    public interface IJob
    {
        long JobId { get; }

        JobState State { get; }

        /// <summary>
        /// Informelle Beschreibung des Jobs
        /// </summary>
        string Title { get; }

        /// <summary>
        /// voraussichtlicher Arbeitsaufwand
        /// </summary>
        long EstimatedEffort { get; }

        /// <summary>
        /// bereits bewältigter Arbeitsaufwand
        /// </summary>
        long CurrentProgress { get; }

        int CurrentProgressInPercent { get; }

        /// <summary>
        /// Zeitpunkt, zu dem der Job erstellt wurde
        /// </summary>
        DateTime Created { get; }

        /// <summary>
        /// Zeitpunkt, zu dem der Job beendet wurde
        /// </summary>
        DateTime Completed { get; }

        /// <summary>
        /// Ausführungszeit des Jobs
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        /// mko, 15.11.2019
        /// Dokumemtation des Ergebnisses eines Jobs in form eines Docuterms
        /// </summary>
        IDocuEntity ResultDocu { get; }
    }
}
