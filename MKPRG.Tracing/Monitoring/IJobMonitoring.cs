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
    /// Überwachung von nebenläufigen Prozessen.
    /// Diese Schnittstelle wird vom Job selbst benutzt, um über seinen aktuellen Zustand einen sog. JobMonitor
    /// zu informieren. Dabei registiriert und deregistriert sich im Job- Monitor. Zudem informiert er über seinen 
    /// Prozessfortschritt.
    /// 
    /// mko, 8.3.2019
    /// completeJob hinzugefügt, dafür deregisterJob in die Schnittstelle IJobMonitoringConsole verschoben.
    /// Wann ein fertiggestellter Job aus der Anzeige verschwindet, entscheidet jetzt die Konsole, und nicht der 
    /// Job selbst.
    /// </summary>
    public interface IJobMonitoring
    {
        /// <summary>
        /// Ein neuer nebenläufiger Prozess (Job) wird registiert. Wenn erfolgreich, dann liefert die 
        /// Funktion eine eindeutige ID für die Identifizirung des Jobs durch den Monitor Job zurück
        /// </summary>
        /// <param name="title">Informelle Beschreibung des Jobs (wird benutzt in Anzeigen des Prozessfortschrittes)</param>
        /// <param name="estimatedEffort">prognostizierte Ausführungsdauer in ms</param>        
        /// <returns></returns>
        RC<long> registerJob(IListMember jobDescr, long estimatedEffort);


        /// <summary>
        /// Meldet den aktuellen Prozessfortschritt als Differenz zum erreichten Prozessfortschritt bei der vorausgegangenen Meldung.
        /// Wenn erfolgreich, dann wird im Rückgabewert mitgeteilt, ob der Überwacher einen 
        /// vorzeitigen Abbruch des Jobs wünscht.
        /// Der Rückgabewert zeigt den aktuellen Zustand des Jobs an. Wenn aborted angezeigt wird, dann 
        /// sollte der Job dies durch deregister Job quittieren.
        /// </summary>
        /// <param name="JobId"></param>
        /// <param name="progress">differentieller Prozessfortschritt: um wieviel ist die Arbeit seit der letzten reportProgress- Meldung fortgeschritten</param>
        /// <returns></returns>
        RC<JobState> reportProgess(long JobId, long progress);

        /// <summary>
        /// mko, 6.10.2020
        /// Zeichnet zusaätzlich zu jeder Fortschrittsmeldung eine Logmeldung auf an DokuTerm.
        /// </summary>
        /// <param name="JobId"></param>
        /// <param name="progress"></param>
        /// <param name="logEntry"></param>
        /// <returns></returns>
        RC<JobState> reportProgess(long JobId, long progress, IListMember logEntry);

        /// <summary>
        /// mko, 18.11.2019
        /// Meldet den aktuellen Prozessfortschritt als Anteil vom prognostizierten Gesamtaufwand.
        /// Wenn erfolgreich, dann wird im Rückgabewert mitgeteilt, ob der Überwacher einen 
        /// vorzeitigen Abbruch des Jobs wünscht.
        /// Der Rückgabewert zeigt den aktuellen Zustand des Jobs an. Wenn aborted angezeigt wird, dann 
        /// sollte der Job dies durch deregister Job quittieren.
        /// <param name="JobId"></param>
        /// <param name="progress">Prozessfortschritt als Anteil vom Prognostizierten Gesamtaufwand</param>
        /// <returns></returns>
        RC<JobState> reportProgessAbsolute(long JobId, long progress);

        /// <summary>
        /// mko, 6.10.2020
        /// Zeichnet zusaätzlich zu jeder Fortschrittsmeldung eine Logmeldung auf an DokuTerm.
        /// </summary>
        /// <param name="JobId"></param>
        /// <param name="progress"></param>
        /// <param name="logEntry"></param>
        /// <returns></returns>
        RC<JobState> reportProgessAbsolute(long JobId, long progress, IListMember logEntry);

        /// <summary>
        /// mko, 8.3.2019
        /// Meldet, das ein Job fertiggestellt ist.
        /// </summary>
        /// <param name="JobId"></param>
        /// <returns></returns>
        RC<JobState> completeJob(long JobId);

        /// <summary>
        /// mko, 15.11.2019
        /// Meldet fertigstellung eines Jobs, wobei das Ergebnis durch einen Dokuterm bewertet/dokumentiert werden kann.
        /// </summary>
        /// <param name="JobId"></param>
        /// <param name="docuTerm"></param>
        /// <returns></returns>
        RC<JobState> completeJob(long JobId, IListMember docuTerm);

    }
}
