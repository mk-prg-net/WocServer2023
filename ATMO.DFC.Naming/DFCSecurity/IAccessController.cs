using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFCSecurity
{

    /// <summary>
    /// mko, 22.10.2019
    /// 
    /// Liefert einen einheitlichen Satz von Methoden, die Entscheiden, ob auf Ressourcen unter gegebenen 
    /// Umständen Zugriff gewährt werden kann oder nicht.
    /// 
    /// Ursprünglich war die Funktionalität des AccessControllers in der IUserV_X_Y Schnittstelle enthalten.
    /// Die explizite Fassung ermöglicht es, Authentifizierung und Authorisierung voneinander zu trennen.  
    /// Vorteile:
    ///  - die Geschäftslogigk prüft Zugriffe mit einem AccessController, wobei die Details der Authentifizierung, 
    ///    welche die Konfiguration des AccessControlers beeinflussten, entfallen.
    ///  - Die Implementierung der Zugriffsprüfungen vereinfacht sich durch spezielle Implementierung von
    ///    AccessControllern für Kunden oder Mitarbeiter.
    /// 
    /// </summary>
    public interface IAccessController
    {
        /// <summary>
        /// This is set, if the user is a Customer, or an internal user that choosed during login 
        /// a restricted access for a specific customer (for debugging purpose)
        /// </summary>
        bool AccessToCustomerSpecificProjectsRestricted { get; }


        /// <summary>
        /// Wenn der Zugriff auf einen  Kundengruppe eingeschränkt ist, dann können hier genaue Informationen zu dieser abgegriffen werden.
        /// </summary>
        DFCObjects.Common.ICustomerGroup ActiveAsMemberOfCustomerGroup { get; }

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
        /// mko, 24.5.2018
        /// If true, user has access to prices of components
        /// 
        /// mko, 11.6.2018
        /// Refactored from Properts in a site - specific function.
        /// </summary>
        bool IsAllowedToSeePrices(Site site);

        /// <summary>
        /// mko, 19.3.2020
        /// Prüft, ob der Zugriff auf ein Dokument eines Typs, zugeordnet einem Standort, mit der gewünschten Zugriffsoperation möglich ist.
        /// Dabei wird ein Morphing des docSecurityTypes der Klassifizierung als Standardbaugruppe oder Ersatz- und Verschleißteil berücksichtigt.
        /// </summary>        
        /// <param name="docSecurityType"></param>
        /// <param name="docAssociatedToSite"></param>
        /// <param name="accessOp"></param>
        /// <param name="isStandardBaugruppe"></param>
        /// <param name="isEVW"></param>
        /// <returns></returns>
        bool PermissionGranted(SecuredDocs docSecurityType, Site docAssociatedToSite, AccessOperations accessOp, bool isStandardBaugruppe = false, bool isEVW = false);


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
        //bool PermissionGranted(ATMO.DFC.Tree.IAssyOrSinglePartSecurityFeatures assySecF, AccessOperations accessOp);

        /// <summary>
        /// mko, 3.9.2020
        /// Bestimmt, ob ein Benutzer auf die Baugruppe mit der gewünschten Operation zugreifen darf.
        /// Kunden dürfen z.B. nur auf Baugruppen und Einzelteile lesend zugreifen, die in 
        /// Projekten verbaut sind, welche für sie freigeschaltet sind.
        /// </summary>
        /// <param name="assySecF"></param>
        /// <param name="accessOp"></param>
        /// <param name="parentIsStadardBaugruppe"></param>
        /// <param name="parentIsEVWP"></param>
        /// <returns></returns>
        bool PermissionGranted(ATMO.DFC.Tree.IAssySecurityFeatures assySecF, AccessOperations accessOp, bool parentIsStadardBaugruppe = false, bool parentIsEVWP= false);

        /// <summary>
        /// mko, 3.9.2020
        /// Bestimmt, ob ein Benutzer auf das Einzelteil mit der gewünschten Operation zugreifen darf.
        /// Kunden dürfen z.B. nur auf Baugruppen und Einzelteile lesend zugreifen, die in 
        /// Projekten verbaut sind, welche für sie freigeschaltet sind.
        /// </summary>
        /// <param name="spSecF"></param>
        /// <param name="accessOp"></param>
        /// <param name="parentIsStadardBaugruppe"></param>
        /// <param name="parentIsEVWP"></param>
        /// <returns></returns>
        bool PermissionGranted(ATMO.DFC.Tree.ISinglePartSecurityFeatures spSecF, AccessOperations accessOp, bool parentIsStadardBaugruppe = false, bool parentIsEVWP = false);  


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

        /// <summary>
        /// mko, 7.9.2020
        /// Prüft die Zugriffsberechtigungen auf ein Prozessmodul. Prozessmodule können mehrere Arbeitsgänge implementieren.
        /// In jedem Arbeitsgang wird dabei zwischen elektrischer und mechanischer Stückliste unterschieden. Prozessmodule stehen mit einer
        /// Station aus einem Projekt in einer 1:1 Beziehung. Daraus folgt, das die Materialnummer eines Prozessmoduls nur genau einmal
        /// für eine Projektstruktur vergeben sein kann.
        /// </summary>
        /// <param name="prjSecF"></param>
        /// <param name="accessOp"></param>
        /// <returns></returns>
        bool PermissionGranted(ATMO.DFC.Tree.IProcessModuleGetSecurityFeatures pmSecF, AccessOperations accessOp);



    }
}
