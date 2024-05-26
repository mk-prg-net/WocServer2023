using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.Tracing
{
    /// <summary>
    /// mko, 10.3.2024
    /// Beschreibt den Zustand des Systems unmittelbar nach dem Aufruf eines Unterprogrammes.
    /// Die wichtigtste Eigenschaft ist **ReturnedFromSuccessfulCall**, die anzeigt, ob der Aufruf
    /// erfolgreich war oder nicht.
    /// Die weiteren Indikatoren klassifizieren zunächst die Abweichungen vom erwarteten Ergebnis.
    /// Detaillierte Auskunft über Abweichungen bzw. Fehler gibt schließlich der DocuTerm **DescriptorOfMethodCallAndReturnValue**.    
    /// Dieser beschreibt zunächst den Methodenaufruf selbst, und informiert dann über Erfolg (eSucceeded) oder
    /// Mißerfolg (eFails). Die Eventparameter beschreiben dann detailiert die Situation.
    /// 
    /// EN: Descriptor for State after return from a Method Call.
    /// </summary>
    public interface IRet
    {
        /// <summary>
        /// Aufruf der Funktion/des unterprogrammes war erfolgreich
        /// </summary>
        bool ReturnedFromSuccessfulCall
        {
            get;            
        }

        /// <summary>
        /// Der Aufruf des Programmes war erfolgreich, jedoch gibt es Warnhinweise zum Zustand des 
        /// Systems bzw. der Qualität der Ergebnisse
        /// Wenn diese Eigenschaft true ist, dann auch die Eigenschaft **ReturnedFromSuccessfulCall**
        /// </summary>
        bool ReturnedFromSuccessfulCallWithWarnings
        {
            get;            
        }

        /// <summary>
        /// Dieser Indikator 
        /// </summary>
        bool ReturnedFromSuccessfulCallButEmptyResultSet
        {
            get;
        }


        /// <summary>
        /// Die Funktion/das unterprogramm wurde vorzeitig verlassen.
        /// </summary>
        bool ReturnedBeforeExecutionCompleted
        {
            get;
        }

        /// <summary>
        /// Die Implementierung des Unterprogramms ist noch nicht fertiggestellt.
        /// </summary>
        bool MethodIsNotImplemented
        {
            get;
        }

        /// <summary>
        /// Die Authentifizierung des Benutzers ist fehlgeschlagen
        /// </summary>
        bool AuthenticationFailed
        {
            get;
        }

        /// <summary>
        /// Beim Zugriff auf eine Ressource wurde ein Zugriffsrecht angefordert, das dem 
        /// authentifizierten Benutzer jedoch nicht gewährt wurde.
        /// </summary>
        bool AuthorizationFailed
        {
            get;
        }

        /// <summary>
        /// Ein Parameter des aufgerufenen Unterprogrammes hat eine Gültigkeitsprüfung nicht bestanden
        /// </summary>
        bool ValidationOfArgumentsFailed
        {
            get;
        }

        /// <summary>
        /// eine Geschäftsregel wurde beim Ausführen des Unterprogrammes verletzt. Z.B. 
        /// wurde versucht, ein Konto über den gewährten Dispokredit hinaus zu belasten.
        /// </summary>
        bool BusinessRuleViolated
        {
            get;
        }

        /// <summary>
        /// Ein unterprogrammaufruf ist fehlgeschlagen
        /// </summary>
        bool SubProcedureCallFailed
        {
            get;
        }

        /// <summary>
        /// Die Nutzung eines Dienstes eines Subsystems ist fehlgeschlagen. Z.B. ist das Dateisystem
        /// ein Subsystem. Ein Dienst des Dateisystems ist z.B. das öffnen einer Datei für den schreibenden
        /// Zugriff.
        /// </summary>
        bool SubsystemCallFailed
        {
            get;
        }

        /// <summary>
        /// Beim Verarbeiten von Daten wurden Inkonsistenzen in diesen entdeckt.
        /// </summary>
        bool DataInconsistencyOccured
        {
            get;
        }

        /// <summary>
        /// Eine Datenbankabfrage ist 
        /// </summary>
        bool SqlDatabaseQueryFailed
        {
            get;
        }

        bool GeneralError
        {
            get;
        }

        /// <summary>
        /// This DocuTerm describes the Situation after MethodCall
        /// </summary>
        DocuTerms.IMethod StatusDescriptionAfterMethodCall
        {
            get;
        }

    }
}
