﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DFCObjects.Common;

namespace DFCSecurity
{
    /// <summary>
    /// mko, 20.10.2017
    /// Grundstruktur von Benutzern in DFC.
    /// Im Code von Jorge wurden Benutzer durch ihren Namen dargestellt. Berechtigungen
    /// etc. wurden unabhängig vom Namen verwaltet. Diese Zusammenfassung soll die Übersicht verbessern
    /// 
    /// mko, 20.2.2017
    /// Erweitert um die Eigenschaften IsRasUser und RasUserName.
    /// Ein RAS- User meldet sich aus seiner Domäne an. Sein Benutzername ist an die 
    /// Domäne gebunden. Im DFC- System wird der Name auf einen Bosch user gemappt.
    /// Die Kenntnis, dass es sich um einen RAS-user handelt, und dessen Name in der 
    /// ursprünglichen Domäne sind wichtig für die Protokollierung.
    /// 
    /// mko, 19.3.2018
    /// Erweitert um die Eigenschaft ExportAllowed
    /// Strategisch ist der Zugriff auf vertrauliche Informationen weiter einzuschränken. 
    /// Die Funktion des Exports wird nur den Personen eingeräumt, für die diese Eigenschaft true liefert
    /// 
    /// mko, 13.4.2018
    /// Erweitert im EffectiveExecutionPermissions. Basis für Implementierung neuer Berechtigungen in DFC.
    /// 
    /// mko, 24.4.2018
    /// Entstanden aus dem Refactoring von IUser. Wurde notwendig, um die Unterscheidung zwischen Kunden und Mitarbeitern
    /// expliziter darszustellen
    /// 
    /// mko, 18.5.2018
    /// Erweitert um Eigenschaften, welche die Ausführung privilegierter Funktionen wie DocuCheck oder UseLogin für den
    /// Benutzer erlauben oder nicht.
    /// 
    /// mko, 25.6.2018
    /// Erweitert um die Eigenschaft UseLogin. Diese dokumentiert, ob sich im Rahmen adminstrativer 
    /// Arbeiten ein Administrator mit der Identität eines DFC- Users im Programm bewegt.
    /// 
    /// mko, 24.10.2018
    /// Prüffunktionen für den Zugriff erweitert um den Parameter isEVW (Ersatz- Verschleißteil oder Werkzeug)
    /// </summary>

    public interface IUserV02 : IPerson
    {
        /// <summary>
        /// Name of User, normalized. Prefixes like TEF_, RAS_ are cutted. 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// If true, an admin has impersonated as user 
        /// </summary>
        bool UseLogin { get; }

        /// <summary>
        /// Internal classification of users. Will be used in adapting GUI for users.
        /// </summary>
        UserClasses UserClass { get; }

        /// <summary>
        /// How user access DFC? E.g. via Bosch Network or via RAS
        /// </summary>
        AccessPointTypes AccessPointType { get; }

        /// <summary>
        /// True, if user connected via RAS
        /// </summary>
        bool IsRASUser { get; }

        /// <summary>
        /// Name of user in his native domain, if connected via RAS
        /// </summary>
        string RASUserName { get; }

        /// <summary>
        /// True, if User is Member in one of following UserClasses:
        /// TEF, Customer, 
        /// </summary>
        bool IsCustomer { get; }        
        
        /// <summary>
        /// If user is a customer, here we get the details of them.
        /// The access of a customer is primary restricted to a set of 
        /// projects, associated to them. 
        /// Basically a customer can only read BOM's, Zusammenbauzeichnungen
        /// and details about EVWP and CAT.
        /// </summary>
        ICustomer Customer { get; }

        /// <summary>
        /// True, if user is a employee or an external. 
        /// </summary>
        bool IsCoWorker { get; }

        /// <summary>
        /// If the user is a CoWorker like enmployee or external, here we get the 
        /// details of them.
        /// For customer this property serves per default a null object. 
        /// If the customer is also an CoWorker, the served object defines additional 
        /// access rights like access to drawings
        /// </summary>
        ICoWorker CoWorker { get; }

        /// <summary>
        /// mko, 11.4.2018
        /// This is set, if the user is a Customer, or an internal user choosed during login 
        /// a restricted access for a specific customer (for debugging purpose)
        /// </summary>
        bool AccessToCustomerSpecificProjectsRestricted { get; set; }

        /// <summary>
        /// Defines the execution permissions, currently valid for the user.
        /// I.e. access to drawings allowed or not ...
        /// For customers read only  access for EVWP and CAT per default allowed.
        /// For CoWorkers additional access rights like StatusChange are possible.
        /// </summary>
        IPermissionVector PermissionVector { get; }


        /// <summary>
        /// true, if user can only access public resources (former as HasDZARoles known)
        /// </summary>
        bool AccessToPublicResourcesOnly { get; }

        /// <summary>
        /// mko, 18.5.2018
        /// If true, then User can perform docuchecks
        /// </summary>
        bool DocuCheckAllowed { get; }

        /// <summary>
        /// mko, 18.5.2018
        /// If true, then User is a sysop and can execute administrative functions like use login.
        /// </summary>
        bool IsSysOp { get; }

        /// <summary>
        /// mko, 24.5.2018
        /// If true, user has access to prices of components
        /// mko, 11.6.2018
        /// Refactored from property to function over sites.
        /// </summary>
        bool IsAllowedToSeePrices(Site site);

        /// <summary>
        /// mko, 4.5.2018
        /// Returns true, if the access operation for given document claims are allowed
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="accessOp">gewünschte Zugriffsoperation</param>
        /// <returns>Wenn True, dann ist der Zugriff erlaubt</returns>
        bool IsDocAccessGranted(IDocSecurityFeatures docSecurityFeatures, AccessOperations accessOp, bool isStandardBaugruppe = false, bool isEVW = false);

        /// <summary>
        /// mko, 23.7.2018
        /// Alias for IsDocAccessGranted
        /// </summary>
        /// <param name="docSecurityFeatures">Sicherheitsmerkmale eines Dokumentes</param>
        /// <param name="accessOp">gewünschte Zugriffsoperation</param>
        /// <param name="isStandardBaugruppe"></param>
        /// <returns>Wenn True, dann ist der Zugriff erlaubt</returns>
        bool PermissionGranted(IDocSecurityFeatures docSecurityFeatures, AccessOperations accessOp, bool isStandardBaugruppe = false, bool isEVW = false);

        /// <summary>
        /// mko, 23.7.2018
        /// Returns true, if the user can execute the defined secured function.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="execMode"></param>
        /// <returns>Wenn True, dann ist der Zugriff erlaubt</returns>
        bool PermissionGranted(SecuredFuncs func, ExecutionModes execMode);

        /// <summary>
        /// mko, 6.5.2019
        /// *geplant*
        /// Bestimmt, ob ein Benutzer auf die Baugruppe/Einzelteil mit der gewünschten Operation zugreifen darf.
        /// Kunden dürfen z.B. nur auf Baugruppen und Einzelteile lesend zugreifen, die in 
        /// Projekten verbaut sind, welche für sie freigeschaltet sind.
        /// </summary>
        /// <param name="assySecF">Sicherheitsmerkmale einer Baugruppe</param>
        /// /// <param name="accessOp">gewünschte Zugriffsoperation</param>
        /// <returns>Wenn True, dann ist der Zugriff erlaubt</returns>
        bool PermissionGranted(ATMO.DFC.Tree.IAssyOrSinglePartSecurityFeatures assySecF, AccessOperations accessOp);


        /// <summary>
        /// mko, 6.5.2019
        /// *geplant*
        /// Bestimmt, ob ein Benutzer auf ein Projekt zugreifen darf.
        /// Projekte sind ATMO- Standorten zugeordnet. Mitarbeiter dürfen generell
        /// nur auf Projekte zugreifen, welche dem Standort zugeordnet sind, dem er angehört.
        /// Z.B. dürfen TDP's von Mitarbeitern nur für Projekte hochgeladen werden, die ihrem Standort zugeordnet sind.
        /// </summary>
        /// <param name="prjSecF">Sichereheitsmerkmale eines Projektes</param>
        /// <param name="accessOp">gewünschte Zugriffsoperation</param>
        /// <returns>Wenn True, dann ist der Zugriff erlaubt</returns>        
        bool PermissionGranted(ATMO.DFC.Tree.IProjectSecurityFeatures prjSecF, AccessOperations accessOp);


        /// <summary>
        /// mko, 6.5.2019
        /// </summary>
        /// <param name="prjSecF"></param>
        /// <param name="accessOp"></param>
        /// <returns></returns>
        bool PermissionGranted(ATMO.DFC.Tree.IStationSecurityFeatures statSecF, AccessOperations accessOp);





    }
}
