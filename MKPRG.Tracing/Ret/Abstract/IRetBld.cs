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

        IRet AuthorizationFailed(DocuTerms.IMethod DescriptionOfFailedAuthorizationProcess);

        //IRet ArugmentIsOutOfRange(string argName)

        IRet ValidationOfArgumentFailed(string NameOfValidationRule);

    }
}
