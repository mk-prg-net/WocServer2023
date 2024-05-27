using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.Tracing
{

    public interface IRetBld
    {
        /// <summary>
        /// Unterprogramm wurde erfolgreich ausgeführt.
        /// </summary>
        /// <returns></returns>
        IRet ReturnOK();

        /// <summary>
        /// Unterprogramm wurde erfolgreich ausgeführt. Im Rückgabewert des DocuTerm Methodendescriptors
        /// gibt es zusätzliche Erläuterungen zum Ergebnis.
        /// </summary>
        /// <param name="AdditionalInfosAboutSuccessfulReturn"></param>
        /// <returns></returns>
        IRet ReturnOk(DocuTerms.IEventParameter AdditionalInfosAboutSuccessfulReturn);

        /// <summary>
        /// Unterprogramm wurde erfolgreich ausgeführt, aber der erreichte Systemzustand ist 
        /// fragil, und davor wird gewarnt.
        /// </summary>
        /// <param name="Warnings"></param>
        /// <returns></returns>
        IRet ReturnOkButWarnings(DocuTerms.IEventParameter Warnings);

        /// <summary>
        /// Das Programm/die Funktion ist noch nicht programmiert worden
        /// </summary>
        /// <returns></returns>
        IRet MethodIsNotimplemented();

        /// <summary>
        /// Zum Zeitpunkt des Rücksprunges aus der Methode war diese noch nicht abgeschlossen.
        /// </summary>
        /// <returns></returns>
        IRet NotCompleted();

        /// <summary>
        /// Allgemeiner, unklassifizierter Fehler. Diese Funktion sollte nur in der Rapid Prototyping Phase 
        /// bei der Entwicklung eingesetzt werden. Die Fehlermeldungen sind nicht klassifiziert und in 
        /// natürlicher Sprache. Damit sind sie einer detailierten Fehleranlyse im aufrufenden Programm 
        /// nicht zugänglich.
        /// In einem Verfeinerungsschritt sollten diese Fehlermeldungen ersetzt werden durch exaktere. 
        /// Achtung: Diese Fehlermeldungen sind nur im Debug- Zweig einsetzbar. Im Release- Zweig erzeugen sie Ausnahmen!
        /// </summary>
        /// <param name="preleminaryErrorDescription"></param>
        /// <returns></returns>
        IRet _GeneralErrorForDebugOnly(string preleminaryErrorDescription);

        /// <summary>
        /// Allgemeine Fehlermeldung, die den aufgetretenen Fehler meldet (whatsUp). Die Ursache des 
        /// Fehlers wird in why beschrieben
        /// </summary>
        /// <param name="whatsUp">Was für ein Fehler ist aufgetreten</param>
        /// <param name="Why">Beschreibung der Fehlerursache</param>
        /// <returns></returns>
        IRet GeneralError(DocuTerms.IPropertyValue whatsUp, DocuTerms.IPropertyValue Why);

        /// <summary>
        /// Die Authentifizierung eines Benutzers ist fehlgeschlagen
        /// </summary>
        /// <param name="UserIdToAuthenticate"></param>
        /// <returns></returns>
        IRet AuthenticationFailed(string UserIdToAuthenticate);

        /// <summary>
        /// Die Authentifizierung eines Benutzers ist fehlgeschlagen.
        /// </summary>
        /// <param name="UserIdToAuthenticate"></param>
        /// <param name="DescriptionOfFailedAuthorizationProcess"></param>
        /// <returns></returns>
        IRet AuthenticationFailed(string UserIdToAuthenticate, DocuTerms.IMethod DescriptionOfFailedAuthorizationProcess);

        /// <summary>
        /// Für den Zugriff auf eine Ressource wurde ein Zugriffsrecht (z.B. lesen) gefordert. Jedoch konnte dieses 
        /// nicht bereitgestellt werden.
        /// </summary>
        /// <param name="requestedAccessRightNID"></param>
        /// <param name="ResourceClassNID"></param>
        /// <param name="DescriptionOfFailedAuthorizationProcess"></param>
        /// <returns></returns>
        IRet AuthorizationFailed(long requestedAccessRightNID, long ResourceClassNID, DocuTerms.IMethod DescriptionOfFailedAuthorizationProcess);

        /// <summary>
        /// Ein Parameter, mit dem dieses Unterprogramm aufgerufen wurde, konnte nicht validiert werden.
        /// </summary>
        /// <param name="NameOfValidationRule"></param>
        /// <param name="validatedArgument"></param>
        /// <returns></returns>
        IRet ValidationOfArgumentFailed(long NameOfValidationRule, DocuTerms.IProperty validatedArgument);

        /// <summary>
        /// Dokumentiert einen Fehlgeschlagenen Unterprogrammaufruf.
        /// Achtung: Der Name des unterprogrammes ist schon im DocuTerm, der den Fehlgeschlagnenen Unterprogrammaufruf beschreibt,
        /// enthalten.
        /// </summary>
        /// <param name="docuTermThatDescribesFailedSubProcedureCall"></param>
        /// <returns></returns>
        IRet SubprocedureCallFailed(DocuTerms.IMethod docuTermThatDescribesFailedSubProcedureCall);

        /// <summary>
        /// Dokumentiert den fehlgeschlagenen Aufruf der Funktion eines Subsystems.
        /// Ein Subsystem ist z.B. das Dateisystem oder die Netzwerkschnittstelle.
        /// </summary>
        /// <param name="SubSytemNID"></param>
        /// <param name="docuTermThatDescribesFailedSubsystemCall"></param>
        /// <returns></returns>
        IRet SubsytemCallFailed(DocuTerms.IMethod docuTermThatDescribesFailedSubsystemCall);

        /// <summary>
        /// Dokumentiert die Verletzung einer Geschäftsregel (z.B. Konto darf nicht über das vereinbarte Dispo- Limit belastet 
        /// werde).
        /// </summary>
        /// <param name="docuTermForFailedBusinessRuleCheck"></param>
        /// <returns></returns>
        IRet BusinessRuleViolated(DocuTerms.IMethod docuTermForFailedBusinessRuleCheck);

        /// <summary>
        /// Es wurden Inconsistenzen in den zu verarbeitenden Daten entdeckt.
        /// </summary>
        /// <param name="docuTermForFailedDataConsistencyCheck"></param>
        /// <returns></returns>
        IRet DataInconsistencyOccured(DocuTerms.IMethod docuTermForFailedDataConsistencyCheck);

        /// <summary>
        /// Dokumentiert den Fehlgeschlagenen Aufruf einer Datenbankabfrage.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="docuTermThatDescribesFailedQuery"></param>
        /// <returns></returns>
        IRet SqlDatabaseQueryFailed(string sqlQuery, DocuTerms.IMethod docuTermThatDescribesFailedQuery);

    }
}
