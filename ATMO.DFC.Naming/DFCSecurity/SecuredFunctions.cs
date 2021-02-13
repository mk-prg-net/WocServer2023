using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFCSecurity
{
    /// <summary>
    /// mko, 11.6.2018
    /// Defines DFC2 functions with restricted execution rights assigend to Roles
    /// Result of developing a more detailed concept of DFC2 securables.
    /// Securables are now divided in two classes: SecuredDocs and SecuredFuncs
    /// 
    /// mko, 9.11.2018
    /// EditLanguageTables added
    /// 
    /// mko, 26.11.2018
    /// DocuLight Upload added
    /// </summary>
    public enum SecuredFuncs
    {

        /// <summary>
        /// Basic right to execute DFC
        /// </summary>
        DFC,

        /// <summary>
        /// Export function in DFC2
        /// </summary>
        DocExport,

        /// <summary>
        /// Check consistency of documents
        /// </summary>
        DocuCheck,

        /// <summary>
        /// can create EVWPLists
        /// </summary>
        CreateEVWP,

        /// <summary>
        /// Can analyze DFC user permissions
        /// </summary>
        AuditDfcPermissions,

        /// <summary>
        /// Can use Digital Project Board
        /// </summary>
        DigitalProjectBoard,

        /// <summary>
        /// Can view tickets via ticket expert
        /// </summary>
        ShowTickets,

        /// <summary>
        /// Can edit language tablees
        /// </summary>
        EditLanguageTables,


        /// <summary>
        /// Can upload DocuLight
        /// </summary>
        DokuLightUpload,

        /// <summary>
        /// mko, 4.4.2019
        /// Kann Bestellungen bearbeiten.
        /// Erweitert, um ein neue Berechtigungsverwaltung für die Bestellungen zu implementieren.
        /// </summary>
        ProcessOrders,


        /// <summary>
        /// mko, 6.6.2019
        /// Darf Projekte für Kunden freischalten.
        /// </summary>
        ReleaseCustomerProjects,

        // mko, 1.7.2019
        // Recht, eine Baseline zu erstellen. 
        // Bei einer Baseline werden alle dokumente eines Projektes in eine ZIP- Datei kopiert
        // und archiviert. Damit existiert dann ein Schnappschuss der Dokumentationen z.B. zum 
        // Zeitpunkt der Auslieferung
        CreateBaseline,


        /// <summary>
        /// mko, 9.3.2020
        /// DFCI: neue, WebApi basierte DFC- Schnittstelle
        /// Erlaubt das herunterladen anonymisierter SFC's und EDC's für statistische Analysen
        /// </summary>
        DFCI_download_SFC_EDC_for_Stat,
    }
}
