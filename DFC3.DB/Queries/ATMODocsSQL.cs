using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using static ATMO.mko.Logging.PNDocuTerms.DocuEntities.DocuEntityHlp;
//using TechTerms = ATMO.mko.Logging.PNDocuTerms.DocuEntities.Composer.TechTerms;
using ANC = ATMO.DFC.Naming;

using ATMO.mko.QueryBuilder;
using DFC3.DB.Bo;
using DFC3.DB.Tables.DZA;
using static DZAUtilities_Dictionaries.GlobalDictionaries;
using DFCObjects.Common.Workflow;
using DFCObjects.Common;
using Doc = DFCObjects.Common.Doc;

using ColTool = DFC3.DB.Tools.TabColAccess;
using DZAUtilities_Dictionaries;
using System.Text.RegularExpressions;

using static DFCSecurity.SecurableExt;
using static DFCSecurity.SitesExt;

using DfcTree = ATMO.DFC.Tree;

using static ATMO.mko.Logging.PNDocuTerms.DocuEntities.ComposerSubTrees;
using TT = ATMO.DFC.Naming.TechTerms;
using TTD = ATMO.DFC.Naming.DocuTerms;

namespace DFC3.DB.Queries
{
    /// <summary>
    /// mko, 29.6.2018
    /// Reimplementation of DBMethods.ShowTechDoc
    /// </summary>
    public class ATMODocsSQL : QueriesBase
    {

        public ATMODocsSQL(IComposer pnL)
            : base(pnL)
        {
        }

        /// <summary>
        /// mko, 27.11.2018
        /// Austausch der static- Elemente gegen Threadsichere Instanzen
        /// </summary>
        //Tables.IPath PathTab;

        /// <summary>
        /// mko, 13.7.2018
        /// 
        /// mko, 26.8.2018
        /// Get's now security information for older versions
        /// 
        /// mko, 4.11.2019
        /// Abruf der Security- Features bezüglich einer Materialnummer in separate Funktion ausgelagert.
        /// 
        /// mko, 5.11.2019
        /// Erweitert um den Parameter "MatNoOfReferencingProject". Z.B. ist im Falle einer ATZ die Zeichnung einer Baugruppe nicht unter der Materialnummer 
        /// der Baugruppe selber , sondern in der Baugruppe abgelegt, auf die in SAP der Zeichnungsverweis gesetzt wurde.
        /// Hier ist neben der docId der Zeichnung selbst im Falle einer ATZ die Materialnummer der Baugruppe anzugeben, !aus! der auf die Zeichnung verwiesen wird.
        /// Dies ist wichtig, da für den Zugriff dokumentspezifiche Informationen wie Status und Typ über die DocId, jedoch Standortzuordnung über die 
        /// Materialnummer der referenzierenden Baugruppe definiert werden.
        /// </summary>
        /// <param name="DocId"></param>
        /// <param name="getEVWForCustGroup">For this customer group the EVW will be queried. If empty, then no EVW will queried</param>
        /// <param name="MatNoOfReferencingContext">Materialnummer des Kontextes (Station, Baugruppe, Einzelteil) aus dem auf das Dokument verwiesen wird</param>
        /// <returns></returns>
        public RCV3sV<DFCSecurity.DocSecurityFeatures> GetSecurityFeatureOf(
            long DocId,
            string getEVWForCustGroup = "",
            string MatNoOfReferencingContext = "")
        {

            var ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Failed(value: null, ErrorDescription: pnL.eNotCompleted());

            // mko, 5.11.2019
            // Signalisiert, das auf ein Dokument zugegriffen werden soll, auf das aus einer Materialnummer verwiesen wird (z.B. ATZ)
            bool isReferencedDoc = !string.IsNullOrWhiteSpace(MatNoOfReferencingContext);

            // Find MatNr document is associated to (lookup in PathView)
            // In PathView only docIds of newest versions are available. But serach for docId in PathView is optimized. 
            // Thats why search starts in PathView.
            var sqlPath = new SQL<PathBo>();

            Func<Table, QueryBuilderResult<PathBo>> CreateQPath = table => sqlPath.Select(
                   sqlPath.Map(Tables.PathView._.DocId, (bo, v) => bo.DocId = ColTool.GetSave<long>(v, -1)),
                   sqlPath.Map(Tables.PathView._.MatNr, (bo, v) => bo.MatNo = ColTool.GetSave<string>(v, "")),
                   // mko, 6.8.2018: DocTypeSAP
                   sqlPath.Map(Tables.PathView._.XType, (bo, v) => bo.DocType = (DocTypeSAP)Enum.Parse(typeof(DocTypeSAP), ColTool.GetSave(v, "unknown") == "TER" ? "TEF" : ColTool.GetSave(v, "unknown"), true)),
                   sqlPath.Map(Tables.PathView._.UserState, (bo, v) => bo.UserState = (DfcDocStates)ColTool.GetSave(v, 0))
               )
               .From(table)
               .Where(sqlPath.Eq(Tables.PathView._.DocId, sqlPath.Long(DocId)))
               .done();

            var getPath = GetRecord(CreateQPath(Tables.PathView._));

            // If search in PathView returns empty, then may be docId points to older version.             
            if (getPath.Succeeded && getPath.Value.IsEmpty)
            {
                // Search in Path for all document versions
                getPath = GetRecord(CreateQPath(Tables.Path._));
            }

            if (getPath.Succeeded && !getPath.Value.IsEmpty)
            {
                if (getPath.Value.Entity.DocType == DocTypeSAP.TER)
                {
                    // mko, 6.8.2018
                    // if document from TEF- Raster, then caluclation of access permissions per side are not required

                    var secF = new DFCSecurity.DocSecurityFeatures();

                    secF.DocType = DocTypeSAP.TER;
                    secF.IsCustProject = true;
                    secF.isEVWP = false;
                    secF.isRelevantForDocumentation = false;
                    secF.isStandardBaugruppe = false;
                    secF.MatNo = getPath.Value.Entity.MatNo;
                    secF.PublicForAll = false;
                    secF.TypeOfSecurableObject = DFCSecurity.SecuredDocs.TEF;
                    secF.UsedInProjects = new long[] { };
                    secF.UserState = getPath.Value.Entity.UserState;
                    secF.ZAT = new DFCSecurity.Site[] { };
                    secF.SitesAccessAllowed = new DFCSecurity.Site[] { };

                    ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Ok(secF);
                }
                else if (getPath.Value.Entity.DocType == DocTypeSAP.MAN)
                {
                    // mko, 28.9.2018
                    // Manuals stehen mit Baugruppen/ Einzelteilen in einer besonderen 1:n Beziehung.
                    // Die MatNr der Manuals ist deshalb kein Schlüssel aus der Mara- Tabelle.
                    // So wird eine Zuordnung eines Manuals zu einem Kundenprojekt nie gefunden werden.
                    // Manuals werden deshalb für Kunden und Mitarbeiter grundsätzlich freigeschaltet                 

                    // Define security feature object
                    var secF = new DFCSecurity.DocSecurityFeatures();

                    secF.MatNo = getPath.Value.Entity.MatNo;
                    secF.DocType = getPath.Value.Entity.DocType;
                    secF.UserState = getPath.Value.Entity.UserState;
                    secF.IsCustProject = true;
                    secF.UsedInProjects = new long[] { };
                    secF.isEVWP = false;
                    secF.isRelevantForDocumentation = true;
                    secF.isStandardBaugruppe = false;
                    secF.PublicForAll = true;

                    var sitesAllowed = new List<DFCSecurity.Site>();
                    foreach (DFCSecurity.Site site in Enum.GetValues(typeof(DFCSecurity.Site)))
                    {
                        sitesAllowed.Add(site);
                    }
                    secF.SitesAccessAllowed = sitesAllowed;

                    secF.TypeOfSecurableObject = DFCSecurity.SecuredDocs.MAN;
                    secF.UserState = getPath.Value.Entity.UserState;
                    secF.ZAT = new DFCSecurity.Site[] { };

                    ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Ok(secF);

                }
                else if (getPath.Value.Entity.DocType == DocTypeSAP.CAT)
                {
                    // mko, 28.9.2018
                    // Katalogteile sind für alle DFC- User offen.

                    // Define security feature object
                    var secF = new DFCSecurity.DocSecurityFeatures();

                    secF.MatNo = getPath.Value.Entity.MatNo;
                    secF.DocType = getPath.Value.Entity.DocType;
                    secF.UserState = getPath.Value.Entity.UserState;
                    secF.IsCustProject = true;
                    secF.UsedInProjects = new long[] { };
                    secF.isEVWP = false;
                    secF.isRelevantForDocumentation = true;
                    secF.isStandardBaugruppe = false;
                    secF.PublicForAll = true;

                    var sitesAllowed = new List<DFCSecurity.Site>();
                    foreach (DFCSecurity.Site site in Enum.GetValues(typeof(DFCSecurity.Site)))
                    {
                        sitesAllowed.Add(site);
                    }
                    secF.SitesAccessAllowed = sitesAllowed;

                    secF.TypeOfSecurableObject = DFCSecurity.SecuredDocs.CAT;
                    secF.UserState = getPath.Value.Entity.UserState;
                    secF.ZAT = new DFCSecurity.Site[] { };

                    ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Ok(secF);
                }
                else
                {
                    // Get Security features, assigned directly to part (lookup in Mara)

                    // mko, 4.11.2019
                    // Abruf der Sicherheitsmerkmale in Funktion ausgelagert.
                    // mko, 5.11.2019
                    // Abruf der Standortzuordnung aus Kontext, aus dem auf das Dokument verwiesen wird
                    var MatNo = isReferencedDoc ? MatNoOfReferencingContext : getPath.Value.Entity.MatNo;

                    var getSecF = GetSecurityFeatureOf(MatNo, getEVWForCustGroup);

                    if (!getSecF.Succeeded)
                    {
                        ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Failed(value: null,
                                                                             inner: RCV3.TranformToRCV3(getSecF),
                                                                             ErrorDescription: pnL.ReturnFetchWithDetails(
                                                                                 Succeeded: false,
                                                                                 UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                                                 UID_of_DataType: ANC.TechTerms.Authorization.ATMO.SecurityFeatures.UID,
                                                                                 Details: pnL.EncapsulateAsEventParameter(getSecF.MessageEntity),
                                                                                 pnL.Predicate(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                                                pnL.p(ANC.TechTerms.ATMO.DFC.DocId.UID, DocId)),
                                                                                 pnL.Predicate(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                                                pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, getPath.Value.Entity.MatNo))));
                    }
                    else if (getSecF.Succeeded)
                    {
                        var secF = getSecF.Value;

                        // mko, 4.11.2019
                        // Hinzufügen dokumentspezifischer Sicherheitsmerkmale
                        secF.DocType = getPath.Value.Entity.DocType;
                        secF.UserState = getPath.Value.Entity.UserState;
                        secF.TypeOfSecurableObject = getPath.Value.Entity.DocType.ToSecuredDocs();

                        ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Ok(secF);
                    }
                }
            }
            else
            {
                // Path query failed
                ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Failed(value: null, inner: RCV3.TranformToRCV3(getPath),
                                                                     ErrorDescription: pnL.ReturnFetchWithDetails(
                                                                                            Succeeded: false,
                                                                                            UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                                                            UID_of_DataType: ANC.TechTerms.Authorization.ATMO.SecurityFeatures.UID,
                                                                                            Details: getPath.Value.IsEmpty ? pnL.EncapsulateAsEventParameter(pnL.NID(ANC.TechTerms.ATMO.DocuCheck.DocuIdIsNotAssignedToMatNo.UID)) : null,
                                                                                            pnL.Predicate(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                                                pnL.p(ANC.TechTerms.ATMO.DFC.DocId.UID, DocId))));

            }

            return ret;
        }

        /// <summary>
        /// mko, 4.11.2019
        /// 
        /// Liefert die Security- Features zu einer Materialnummer. Aus der Funktion GetSecurityFeatureOf(long DocId,...) abgeleitet.
        /// 
        /// Motivation: 
        /// Beim Zugriff auf eine ATZ ist die Materialnummer der Baugruppe entscheidend, aus der auf die ATZ verwiesen wird. 
        /// In der ursprünglichen Funktion zur Berechnung von Security- Features wurde zuerst die Materialnummer aus der 
        /// DocId- der ATZ bestimmt. Dieses Verfahren liefert jedoch die Materialnummer der Baugruppe, in welcher die
        /// ATZ definiert ist, und nicht die, aus welcher auf die ATZ verwiesen wird.
        /// 
        /// 
        /// mko, 20.4.2020
        /// Massives Refactoring. Methoden wurden zuvor extrahiert wie GetSiteActivationsFor und GetStdBgAndZATFor, die 
        /// auch in anderen GetSecutiryFeatures* Methoden verwendet werden.
        /// Es wird jetzt strikt nach relevanten Sicherheitsmerkmalen für Kunden oder Mitarbeitern unterschieden.
        /// 
        /// Das Sicherheitsmerkmal EVWP (relevant für Kunden) wird jetzt nur noch für die Einträge in MaraPJ berücksichtigt, 
        /// die der Kundengruppe zugeordnet sind. Vorher wurden alle Einträge zur Materialnummer betrachtet, unabhängig von der 
        /// Kundengruppe. Normalerweise sollte dies zu keiner Einschränkung für den Kunden führen.
        /// 
        /// mko, 21.7.2020
        /// Intitialisierung der SecurityFeatures, um Nullwerte in MatNo und UsedInProjects zu vermeiden.
        /// 
        /// mko, 17.12.2020
        /// Abfrage zur Prüfung des Kundenzugriffes (Freischaltung des Materials unterhalb eines Projekts, Station für die 
        /// Kundengruppe) erweitert:
        /// Jetzt neben Join auf Prjektnummer auch Join auf Stationsnummer. Vorher wurde nur die Projektnummer verbunden mit der Folge
        /// zu vieler Treffer. Wenn z.B. ein Kunde nur Zugriff auf eine Station eines Projekts hatte, bekam er trotzdem 
        /// Zugriff auf die Dokumente der anderen Stationen, da ja mindestens ein Treffer geliefert wurde.
        /// 
        /// mko, 8.1.2021
        /// Abfrage des Kundenzugriffs nochmals komplett überarbeitet. Dazu die Tage zuvor das Tabellengespann **Projektliste2**
        /// und **MaraPJ** analysiert. Es wurden die neue Views **ProjekteSecF** und **StationenSecF** erzeugt und bei der Implementierung
        /// eingesetzt. Diese Views breingen das konzeptionelle Modell der GDM- Datenbank besser zum Vorschein.
        /// Zudem werden jetzt die EVWP- und Dokuhaken Kennungen genauer bestimmt. Als DocuTerm- Listen werden alle 
        /// Stationen zurückgeliefert, in denen zur Materialnummer eine EVWP- Kennung oder ein Dokuhaken gesetzt sind.
        /// </summary>
        /// <param name="MatNo"></param>
        /// <param name="getEVWForCustGroup"></param>
        /// <returns></returns>
        public RCV3sV<DFCSecurity.DocSecurityFeatures> GetSecurityFeatureOf(
            string MatNo,
            string getEVWForCustGroup = "",
            bool getSecFForCoWorker = true)
        {

            var ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Failed(value: null, ErrorDescription: pnL.eNotCompleted());

            // Materialnummer und Liste der Projekte initialisiert, um Nullwerte zu vermeiden
            var secF = new DFCSecurity.DocSecurityFeatures()
            {
                MatNo = MatNo,
                UsedInProjects = new long[] { }
            };

            //----------------------------------------------------------------------------------------------
            // Bestimmen der Sicherheitsmerkmale, die für einen Mitarbeiterzugriff relevant sind:

            if (getSecFForCoWorker)
            {
                // Bestimmen, ob eine Standardbaugruppe vorliegt.
                var getStdBg = GetStdBgAndZATFor(MatNo);

                if (!getStdBg.Succeeded)
                {
                    ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Failed(value: null, getStdBg.ToPlx());
                }
                else
                {
                    secF.isStandardBaugruppe = getStdBg.Value.IsStdBg;
                    secF.ZAT = getStdBg.Value.IsStdBgAtSite;


                    // Bestimmen der Standortfreischaltungen
                    var getSiteAccess = GetSiteActivationsFor(MatNo);

                    if (!getSiteAccess.Succeeded)
                    {
                        ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Failed(value: null, getSiteAccess.ToPlx());
                    }
                    else
                    {
                        secF.PublicForAll = getSiteAccess.Value.publicForAll;
                        secF.SitesAccessAllowed = getSiteAccess.Value.siteAccess;

                        ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Ok(secF);
                    }
                }
            }

            //IsCustProject
            if (!string.IsNullOrWhiteSpace(getEVWForCustGroup))
            {
                // mko, 5.1.2021
                // Beim Zugriff durch einen Kunden muss geprüft werden, ob die Materialnummer in der MaraPJ einem 
                // Projekt bzw. einer Projekt/Station zugeordnet ist, die für den Kunden freigeschaltet wurde.
                // Es sind folgende Fälle zu unterscheiden:
                // 1) Materialnummer ABC ist in einem Projekt XYZ verbaut, das insgesamt für den Kunden freigeschaltet wurde:
                //    (Exists 
                //        (ProjektSecF 
                //            (Row (ProjNr XYZ) (CustAccess %cust-group%))))
                // 
                //     =\ SecF.isCustProject = true                
                //
                // 2) Materialnummer ABC ist in einer Station i eines Projekts XYZ verbaut, die für den Kunden freigeschaltet wurde:
                //    (Exists 
                //        (Projektliste2 
                //            (Row (ProjektNo XYZ) (Station i) (CustAccess cust-group))))
                //
                //      =\ SecF.isCustProj = true
                //      =\ SecF.isEVWP = true, wenn für Projekt XYZ die Station i in der MaraPJ 
                //
                // 3) Wenn die Materialnummer zu einem Kundenprojekt gehört, dann
                //    3.1 Bestimmen, ob eine EVWP Kennung in MaraPJ unter irgend einem Projekt für den Kunden gesetzt wurde
                //    3.2 Bestimmen, ob ein Dokuhaken in MaraPJ unter irgend einem Projekt für den Kunden gesetzt wurde
                //
                //
                //   ......................................................................
                //   : Projektliste2                                                      :
                //   :                                                                    :      
                //   : +---------------+          +-------------+                         :        +------------+
                //   : | Projekte      | <------>>| rel_PrjCust |<<------------------------------>>| CustGroups |
                //   : |               | <---+    |             |                         :        |            |   
                //   : |               |     |    |             |                         :        |            |
                //   : +---------------+ <-+ +    +-------------+                         :  +---->|            |
                //   :   +- prjNr          | |      +- prjNr                              :  |     +------------+
                //   :                     | |      +- CustId                             :  |       +- CustId
                //   :                     | |                                            :  |       +- CustGroup
                //   :                     | |                                            :  |
                //   :                     | |    +-------------+        +--------------+ :  |
                //   :                     | +-->>| Stationen   |<----->>| rel_StatCust |<<--+
                //   :                     |      |             |<-+     |              | :
                //   :                     |      +-------------+  |     +--------------+ :
                //   :                     |         +- PrjNr      |        +- PrjNr      :
                //   :                     |         +- StatNr     |        +- StatNr     :
                //   :                     |                       |        +- CustId     :
                //   ......................|.......................|.......................
                //                         |                       |
                //                         |                       |
                //          ...............|.......................|..............................
                //          : MaraPJ       |                       |                             :
                //          :              |                       |     +----------------+      :
                //          :              |                       +--->>| rel_StatMaraPJ |      :
                //          :              |                             |                |      :
                //          :              |                             +----------------+      :
                //          :              |                                + PrjNr              :
                //          :              |                                + StatNr             :
                //          :              |                                + MatNo              :
                //          :              |                                + EVWP               :
                //          :              |                                + Dokuhaken          :
                //          :              |      +---------------+                              :
                //          :              +---->>| rel_PrjMaraPJ |                              :
                //          :                     | (StatNr == 0) |                              :
                //          :                     +---------------+                              :
                //          :                        + PrjNr                                     :
                //          :                        + MatNo                                     :
                //          :                        + EVWP                                      : 
                //          :                        + Dokuhaken                                 :
                //          :                                                                    :
                //          ......................................................................  
                // 
                // Projekte  := Liste aller Projekte/Anlagen
                // Stationen := Liste aller Stationen eines Projektes
                // rel_PrjCust := Freischaltug eines Projekts/einer Anlage für eine Kundengruppe
                // rel_StatCust := Freischaltung einer Station für eine Kundengruppe
                // rel_PrjMaraPJ := Zuordnung von Material an eine Station (Prozessmodule, Baugruppen, Dokumentation)
                // rel_PrjMaraPj  := Zurodnung von Stationen und 

                // Abfrage Fall 1): Material ist Teil von Projekten, die komplett für den Kunden freigeschaltet sind
                var tabMaraPj = new Tables.MaraPj();
                var tabPrj = new Tables.ProjekteSecF(); //new Tables.Projektliste2("Prj2");
                var sql = new SQL<Bo.MaraPjBo>();
                var qCustPrj = sql.Select(
                    sql.Map(tabPrj.PrjNr.FQN, (bo, v) =>
                        bo.PjNr = ColTool.GetSave(v, 0)),
                    sql.Map(tabMaraPj.StatNr.FQN, (bo, v) =>
                        bo.StatNr = ColTool.GetSave(v, (short)0)),
                    sql.Map(tabMaraPj.EVW.FQN, (bo, v) => 
                        bo.EVW = ColTool.GetSave(v, "")),
                    sql.Map(tabMaraPj.Doku.FQN, (bo, v) =>
                        bo.Doku = ColTool.GetSave(v, ""))
                )
                // Join von Projektliste mit der der MaraPJ, wobei nur über die Projektnummer verbunden wird
                .JoinFrom((tabPrj, tabMaraPj, sql.Eq(tabPrj.PrjNr.FQN, tabMaraPj.PjNr.FQN)))
                .Where(sql.And(
                        // 
                        sql.StrEq(tabMaraPj.MatNr.FQN, sql.Txt(MatNo)),
                        sql.IsNotNull(tabPrj.CustAccess.FQN),
                        sql.LikeUpperCase(tabPrj.CustAccess.FQN, sql.Txt($"%{getEVWForCustGroup}%"))
                    ))
                .done();

                // mko, 22.11.2019
                // Von GetRecord auf GeRecords übergegangen, da in M.4701759.720.10.10.650.8 & .9 die Zeichnungen für Kunden (jph) nicht sichtbar sind, 
                // obwohl das Einzelteil eine EVWP- Kennung hat. In MaraPj gibt es für die Materialnummer 0804DG2232 (Halter) mehrere Einträge (in mehreren 
                // Projekten verbaut), jedoch ist es nicht in jedem als EVWP gekennzeichnet. Ergebnis: Datensätze mit keiner EVWP- Kennung kommen zuerst, und die 
                // Security- Features wurden ohne EVWP Kennung zurückgeliefert.
                // Jetzt wird provisorisch geprüft, ob in irgend einem Projekt das Teil als EVWP gekennzeichnet ist. Wenn ja, dann wird eine EVWP Kennung zurückgeliefert.
                var getPrj = GetRecords(qCustPrj);

                if (!getPrj.Succeeded)
                {
                    ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Failed(
                        value: secF,
                        ErrorDescription: plxResFactory.CreateQueryExecutionFailed(
                                                tabMaraPj,
                                                tabPrj,
                                                pnL.m(TT.Operators.Relations.Eq.UID,
                                                    pnL.p(TT.ATMO.DFC.MatNo.UID, MatNo),
                                                    pnL.p(TT.Authorization.ATMO.CustomerView.UID, getEVWForCustGroup))));
                                            
                }
                else if (!getPrj.Value.IsEmpty)
                {
                    // Achtung: bewusst der Fall !getPrj.Value.IsEmpty
                    // Die Materialnummer ist tatsächlich in Projekten enthalten, die komplett für den Kunden freigeschaltet sind

                    secF.IsCustProject = true;

                    // Bestimmen er EVW- Kennung
                    // Sicherheitsmerkmale, die direkt mit der Materialnummer assoziiert sind, auslesen
                    (long prjNo, long stationNo, bool hasDokuhaken, bool hasEVWP)[] mpjEntities = getPrj.Value.Entities.Select(                        
                        r => (r.PjNr, r.StatNr, !string.IsNullOrWhiteSpace(r.Doku), !string.IsNullOrWhiteSpace(r.EVW))).ToArray();

                    IDTList evwpList = pnL.List();

                    if(mpjEntities.Any(r => r.hasEVWP))
                    {
                        secF.isEVWP = true;

                        // Liste aller Projekte+Stationen, in denen das Teil ein EVWP- Teil ist
                        evwpList = pnL.List(mpjEntities
                                                .Where(r => r.hasEVWP)
                                                .Select(r => pnL.p(TT.ATMO.DFC.PSPNo.UID,  ATMO.DFC.Tree.Parser.PSPNoParser.CreatePSPNo(r.prjNo, r.stationNo)))
                                                .ToArray());
                    }

                    IDTList dokuHakenList = pnL.List();

                    if (mpjEntities.Any(r => r.hasDokuhaken))
                    {
                        secF.isRelevantForDocumentation = true;

                        // Liste aller Projekte+Stationen, in denen das Teil ein EVWP- Teil ist
                        dokuHakenList = pnL.List(mpjEntities
                                                .Where(r => r.hasDokuhaken)
                                                .Select(r => pnL.p(TT.ATMO.DFC.PSPNo.UID, ATMO.DFC.Tree.Parser.PSPNoParser.CreatePSPNo(r.prjNo, r.stationNo)))
                                                .ToArray());
                    }

                    ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Ok(
                            secF,

                            // Zusatzinformationen
                            pnL.ReturnAfterSuccessWithDetails(
                                    "GetSecurityFeatureOf",
                                    pnL.i(TT.Sets.All.UID,
                                        // Liste aller Stationen, in denen Material als EVWP gekennzeichnet ist.
                                        pnL.p(TT.ATMO.DFC.MatClassEVWP.UID, evwpList),

                                        // Liste aller Stationen, in denen Material den Dokuhaken gesetzt hat.
                                        pnL.p(TT.ATMO.DFC.MatClassDocuEnabled.UID, dokuHakenList)
                                )));

                }
                else
                {
                    // Fall 2): Material ist für Kunden möglicherweise innerhalb von bestimmten Stationen freigeschaltet

                    var tabStations = new Tables.StationenSecF();

                    var qCustPrjStat = sql.Select(
                        sql.Map(tabStations.PrjNr.FQN, (bo, v) =>
                            bo.PjNr = ColTool.GetSave(v, 0)),
                        sql.Map(tabMaraPj.StatNr.FQN, (bo, v) =>
                            bo.StatNr = ColTool.GetSave(v, (short)0)),
                        sql.Map(tabMaraPj.EVW.FQN, (bo, v) =>
                            bo.EVW = ColTool.GetSave(v, "")),
                        sql.Map(tabMaraPj.Doku.FQN, (bo, v) =>
                            bo.Doku = ColTool.GetSave(v, ""))
                    )
                    // Join von Projektliste mit der der MaraPJ, wobei nur über die Projektnummer verbunden wird
                    .JoinFrom((tabStations, 
                               tabMaraPj, 
                               sql.And(
                                   sql.Eq(tabStations.PrjNr.FQN, tabMaraPj.PjNr.FQN),
                                   sql.Eq(tabStations.StatNr.FQN, tabMaraPj.StatNr.FQN))))
                    .Where(sql.And(
                            // 
                            sql.StrEq(tabMaraPj.MatNr.FQN, sql.Txt(MatNo)),
                            sql.IsNotNull(tabStations.CustAccess.FQN),
                            sql.LikeUpperCase(tabStations.CustAccess.FQN, sql.Txt($"%{getEVWForCustGroup}%"))
                        ))
                    .done();



                    getPrj = GetRecords(qCustPrjStat);

                    if (!getPrj.Succeeded)
                    {
                        ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Failed(value: secF, ErrorDescription: getPrj.MessageEntity, User: getPrj.User);
                        ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Failed(
                                        value: secF,
                                        ErrorDescription: plxResFactory.CreateQueryExecutionFailed(
                                                                tabMaraPj,
                                                                tabPrj,
                                                                pnL.m(TT.Operators.Relations.Eq.UID,
                                                                    pnL.p(TT.ATMO.DFC.MatNo.UID, MatNo),
                                                                    pnL.p(TT.Authorization.ATMO.CustomerView.UID, getEVWForCustGroup))));
                        

                    }
                    if (!getPrj.Value.IsEmpty)
                    {
                        // Falls Einträge in MaraPJ und Projektliste für das Projekt zu Kundengruppe gefunden wurden, dann ist das Projekt ein Kundenprojekt
                        secF.IsCustProject = true;

                        // Bestimmen er EVW- Kennung
                        // Sicherheitsmerkmale, die direkt mit der Materialnummer assoziiert sind, auslesen
                        (long prjNo, long stationNo, bool hasDokuhaken, bool hasEVWP)[] mpjEntities = getPrj.Value.Entities.Select(
                            r => (r.PjNr, r.StatNr, !string.IsNullOrWhiteSpace(r.Doku), !string.IsNullOrWhiteSpace(r.EVW))).ToArray();

                        IDTList evwpList = pnL.List();

                        if (mpjEntities.Any(r => r.hasEVWP))
                        {
                            secF.isEVWP = true;

                            // Liste aller Projekte+Stationen, in denen das Teil ein EVWP- Teil ist
                            evwpList = pnL.List(mpjEntities
                                                    .Where(r => r.hasEVWP)
                                                    .Select(r => pnL.p(TT.ATMO.DFC.PSPNo.UID, DfcTree.Parser.PSPNoParser.CreatePSPNo(r.prjNo, r.stationNo)))
                                                    .ToArray());
                        }

                        IDTList dokuHakenList = pnL.List();

                        if (mpjEntities.Any(r => r.hasDokuhaken))
                        {
                            secF.isRelevantForDocumentation = true;

                            // Liste aller Projekte+Stationen, in denen das Teil ein EVWP- Teil ist
                            dokuHakenList = pnL.List(mpjEntities
                                                    .Where(r => r.hasDokuhaken)
                                                    .Select(r => pnL.p(TT.ATMO.DFC.PSPNo.UID, DfcTree.Parser.PSPNoParser.CreatePSPNo(r.prjNo, r.stationNo)))
                                                    .ToArray());
                        }

                        ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Ok(
                                secF,

                                // Zusatzinformationen
                                pnL.ReturnAfterSuccessWithDetails(
                                        "GetSecurityFeatureOf",
                                        pnL.i(TT.Sets.All.UID,
                                            // Liste aller Stationen, in denen Material als EVWP gekennzeichnet ist.
                                            pnL.p(TT.ATMO.DFC.MatClassEVWP.UID, evwpList),

                                            // Liste aller Stationen, in denen Material den Dokuhaken gesetzt hat.
                                            pnL.p(TT.ATMO.DFC.MatClassDocuEnabled.UID, dokuHakenList)
                                    )));
                    }
                    else
                    {
                        secF.IsCustProject = false;
                        secF.isEVWP = false;
                        secF.isRelevantForDocumentation = false;
                        ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Ok(secF);
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// mko, 6.4.2020
        /// Bestimmt, ob hinter  einer Materialnummer eine  Standardbaugruppe, oder zumindest eine pro Standort 
        /// standardisierte Baugruppe steht.
        /// </summary>
        /// <param name="MatNo"></param>        
        /// <returns></returns>
        public RCV3sV<(bool IsStdBg, DFCSecurity.Site[] IsStdBgAtSite)> GetStdBgAndZATFor(string MatNo)
        {

            var ret = RCV3sV<(bool IsStdBg, DFCSecurity.Site[] IsStdBgAtSite)>.Failed(value: (false, null), ErrorDescription: pnL.eNotCompleted());

            // Get Security features, assigned directly to part (lookup in Mara)

            var sqlMara = new SQL<MaraBo>();

            var qMara = sqlMara.Select(
                    sqlMara.Map(Tables.Mara._.MatNr, (bo, v) => bo.MatNr = ColTool.GetSave(v, "")),
                    sqlMara.Map(Tables.Mara._.MKlasse, (bo, v) => bo.MKlasse = ColTool.GetSave(v, "")),
                    sqlMara.Map(Tables.Mara._.StdBg, (bo, v) => bo.StandardBaugruppe = ColTool.GetSave(v, "")),
                    sqlMara.Map(Tables.Mara._.Verbaut, (bo, v) => bo.Verbaut = ColTool.GetSave(v, 0)),
                    sqlMara.Map(Tables.Mara._.ZAT, (bo, v) => bo.ZAT = ColTool.GetSave(v, ""))
                )
                .From(Tables.Mara._)
                .Where(sqlMara.Eq(Tables.Mara._.MatNr, sqlMara.Txt(MatNo)))
                .done();

            var getMara = GetRecord(qMara);

            if (!getMara.Succeeded)
            {
                ret = RCV3sV<(bool IsStdBg, DFCSecurity.Site[] IsStdBgAtSite)>.Failed((false, null), getMara.ToPlx());
            }
            else
            {
                // Define security feature object
                bool isStandardBaugruppe = !string.IsNullOrWhiteSpace(getMara.Value.Entity.StandardBaugruppe);

                var ZAT = Tables.Mara._.ParseZAT(getMara.Value.Entity.ZAT, pnL);

                ret = RCV3sV<(bool IsStdBg, DFCSecurity.Site[] IsStdBgAtSite)>.Ok((isStandardBaugruppe, ZAT.ToArray()));
            }

            return ret;
        }

        /// <summary>
        /// 6.4.2020
        /// Bestimmt alle Standortfreischaltungen zu einer Materialnummer
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public RCV3sV<(bool publicForAll, DFCSecurity.Site[] siteAccess)> GetSiteActivationsFor(string MatNo)
        {

            var ret = RCV3sV<(bool publicForAll, DFCSecurity.Site[] siteAccess)>.Failed(value: (false, null), ErrorDescription: pnL.eNotCompleted());

            // lookup in Mara2 for site activations

            var sqlMara2 = new SQL<Mara2Bo>();

            var qMara2 = sqlMara2.Select(
                    sqlMara2.Map(Tables.Mara2._.PA1, (bo, v) => bo.PA1 = ColTool.GetSave(v, "")),
                    sqlMara2.Map(Tables.Mara2._.PA2, (bo, v) => bo.PA2 = ColTool.GetSave(v, "")),
                    sqlMara2.Map(Tables.Mara2._.PA3, (bo, v) => bo.PA3 = ColTool.GetSave(v, "")),
                    sqlMara2.Map(Tables.Mara2._.PA4, (bo, v) => bo.PA4 = ColTool.GetSave(v, "")),
                    sqlMara2.Map(Tables.Mara2._.PA5, (bo, v) => bo.PA5 = ColTool.GetSave(v, "")),
                    sqlMara2.Map(Tables.Mara2._.PA6, (bo, v) => bo.PA6 = ColTool.GetSave(v, "")),
                    sqlMara2.Map(Tables.Mara2._.PA7, (bo, v) => bo.PA7 = ColTool.GetSave(v, "")),
                    sqlMara2.Map(Tables.Mara2._.PA8, (bo, v) => bo.PA8 = ColTool.GetSave(v, "")),
                    sqlMara2.Map(Tables.Mara2._.PA9, (bo, v) => bo.PA9 = ColTool.GetSave(v, "")),
                    sqlMara2.Map(Tables.Mara2._.PMH, (bo, v) => bo.PMH = ColTool.GetSave(v, ""))
                )
                .From(Tables.Mara2._)
                .Where(sqlMara2.Eq(Tables.Mara2._.MatNr, sqlMara2.Txt(MatNo)))
                .done();

            var getMara2 = GetRecord(qMara2);

            if (!getMara2.Succeeded)
            {
                ret = RCV3sV<(bool publicForAll, DFCSecurity.Site[] siteAccess)>.Failed(value: (false, null), getMara2.ToPlx());
            }
            else
            {
                // Parse site activations
                var sitesAllowed = new List<DFCSecurity.Site>();
                if (!string.IsNullOrWhiteSpace(getMara2.Value.Entity.PA1)) sitesAllowed.Add(DFCSecurity.Site.ATMO_1);
                if (!string.IsNullOrWhiteSpace(getMara2.Value.Entity.PA2)) sitesAllowed.Add(DFCSecurity.Site.ATMO_2);
                if (!string.IsNullOrWhiteSpace(getMara2.Value.Entity.PA3)) sitesAllowed.Add(DFCSecurity.Site.ATMO_3);
                if (!string.IsNullOrWhiteSpace(getMara2.Value.Entity.PA4)) sitesAllowed.Add(DFCSecurity.Site.ATMO_4);
                if (!string.IsNullOrWhiteSpace(getMara2.Value.Entity.PA5)) sitesAllowed.Add(DFCSecurity.Site.ATMO_5);
                if (!string.IsNullOrWhiteSpace(getMara2.Value.Entity.PA6)) sitesAllowed.Add(DFCSecurity.Site.ATMO_6);
                if (!string.IsNullOrWhiteSpace(getMara2.Value.Entity.PA7)) sitesAllowed.Add(DFCSecurity.Site.ATMO_7);
                if (!string.IsNullOrWhiteSpace(getMara2.Value.Entity.PA8)) sitesAllowed.Add(DFCSecurity.Site.ATMO_8);
                //if (!string.IsNullOrWhiteSpace(retMara2.Value.PA9)) sitesAllowed.Add(DFCSecurity.Site.ATMO_9);
                if (!string.IsNullOrWhiteSpace(getMara2.Value.Entity.PMH)) sitesAllowed.Add(DFCSecurity.Site.MH);

                if (!sitesAllowed.Any())
                {
                    ret = RCV3sV<(bool publicForAll, DFCSecurity.Site[] siteAccess)>.Ok(value: (true, GetAllAtmoSites()));
                }
                else
                {
                    ret = RCV3sV<(bool publicForAll, DFCSecurity.Site[] siteAccess)>.Ok(value: (false, sitesAllowed.ToArray()));
                }
            }

            return ret;
        }

        /// <summary>
        /// mko, 6.4.2020
        /// Bestimmt, ob hinter einer Materialnummer ein Erstz- und Verschleiss- oder ein Dokumentationsrelevantes Teil steht
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public RCV3sV<(bool isEVWP, bool isRelevantForDocumentation)> GetEVWPAndDokuhakenFor(string MatNo)
        {
            var ret = RCV3sV<(bool isEVWP, bool isRelevantForDocumentation)>.Failed(value: (false, false), pnL.eNotCompleted());

            var sqlEvwDoku = new SQL<Bo.MaraPjBo>();
            var qEvwDoku = sqlEvwDoku.Select(
                    sqlEvwDoku.Map(Tables.MaraPj._.PjNrStatNrMatNr, (bo, v) => bo.PjNrStatNrMatNr = ColTool.GetSave(v, "")),
                    sqlEvwDoku.Map(Tables.MaraPj._.PjNr, (bo, v) => bo.PjNr = ColTool.GetSave(v, 0)),
                    sqlEvwDoku.Map(Tables.MaraPj._.StatNr, (bo, v) => bo.StatNr = ColTool.GetSave(v, (short)0)),
                    sqlEvwDoku.Map(Tables.MaraPj._.MatNr, (bo, v) => bo.MatNr = ColTool.GetSave(v, "")),
                    sqlEvwDoku.Map(Tables.MaraPj._.StlStatus, (bo, v) => bo.StlStatus = ColTool.GetSave(v, "")),
                    sqlEvwDoku.Map(Tables.MaraPj._.EVW, (bo, v) => bo.EVW = ColTool.GetSave(v, "")),
                    sqlEvwDoku.Map(Tables.MaraPj._.Doku, (bo, v) => bo.Doku = ColTool.GetSave(v, "")),
                    sqlEvwDoku.Map(Tables.MaraPj._.Lup, (bo, v) => bo.Lup = ColTool.GetSave(v, DateTime.MinValue)),
                    sqlEvwDoku.Map(Tables.MaraPj._.StlNr, (bo, v) => bo.StlNr = ColTool.GetSave(v, 0L))
                )
                .From(Tables.MaraPj._)
                .Where(sqlEvwDoku.Eq(Tables.MaraPj._.MatNr, sqlEvwDoku.Txt(MatNo)))
                .done();

            // mko, 22.11.2019
            // Von GetRecord auf GeRecords übergegangen, da in M.4701759.720.10.10.650.8 & .9 die Zeichnungen nicht für Kunden (jph) nicht sichtbar sind, 
            // obwohl das Einzelteil eine EVWP- Kennung hat. In MaraPj gibt es für die Materialnummer 0804DG2232 (Halter) für mehrere Einträge (in mehreren 
            // Projekten verbaut), jedoch ist es nicht in jedem als EVWP gekennzeichnet. Ergebnis: DAtensätze mit keiner EVWP- Kennung kommen zuerst, und die 
            // Security- Features wurden ohne EVWP Kennung zurückgeliefert.
            // Jetzt wird provisorisch geprüft, ob in irgend einem Projekt das Teil als EVWP gekennzeichnet ist. Wenn ja, dann wird eine EVWP Kennung zurückgeliefert.
            var getEvwDoku = GetRecords(qEvwDoku);

            if (getEvwDoku.Succeeded && !getEvwDoku.Value.IsEmpty)
            {
                var isEvwp = getEvwDoku.Value.Entities.Any(r => !string.IsNullOrWhiteSpace(r.EVW));
                var dokuHaken = getEvwDoku.Value.Entities.Any(r => !string.IsNullOrWhiteSpace(r.Doku));

                ret = RCV3sV<(bool isEVWP, bool isRelevantForDocumentation)>.Ok((isEvwp, dokuHaken));
            }
            else
            {
                ret = RCV3sV<(bool isEVWP, bool isRelevantForDocumentation)>.Failed(value: (false, false), ErrorDescription: getEvwDoku.MessageEntity, User: getEvwDoku.User);
            }

            return ret;
        }

        /// <summary>
        /// mko, 6.4.2020
        /// Sicherheitsmerkmale für Merkmalsausprägungen einer Baugruppe abrufen.
        /// Kundensicht:
        ///     Merkmale werden immer projetspezikfisch bewertet. Entscheidend ist hier, ob die Baugruppe zu einem 
        ///     Kundenprojekt gehört. Auch Merkmale wie EVWP sind von Interesse.
        /// 
        /// Mitarbeitersicht:
        ///     Ist die Baugruppe für den Standort freigeschaltet (ATMO-weite Standardbaugruppe, ZAT oder explizit für 
        ///     Standort freigeschaltet)
        ///     
        /// </summary>
        /// <param name="pspBom">PSP- Nummer des Projektkontextes</param>        
        /// <param name="AssyBomPos">Stücklistenposition der Baugruppe</param>
        /// <param name="getForCustGroup">Kundengruppe, unter der der aktuelle User angemeldet ist. Wenn der User kein Kunde ist, und kein 
        /// Kundenkontext gefordert ist, dann ist hier eine leere Zeichenkette zu übergeben.</param> 
        /// <param name="getSecFForCoWorker">Wenn true, dann werden die im Kontext eines Zugriffs durch einen Mitarbeiter relevanten Sicherheitsmerkmale bestimmt</param>               
        /// <returns></returns>
        public RCV3sV<DFCSecurity.DocSecurityFeatures> GetSecurityFeaturesOfCharacteristicValuesOfAssy(
                    DfcTree.IPSPBom pspBom,
                    DfcTree.IMatBomNodePosition AssyBomPos,
                    string getForCustGroup = "",
                    bool getSecFForCoWorker = true)
        {

            var ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Failed(value: null, pnL.eNotCompleted());

            var secF = new DFCSecurity.DocSecurityFeatures()
            {
                DocType = DocTypeSAP.CharacteristicValues,
                UserState = DfcDocStates.none,
                MatNo = AssyBomPos.MatNoOfCurrentBomPos,
                TypeOfSecurableObject = DFCSecurity.SecuredDocs.CV,
                UsedInProjects = new long[] { }
            };

            //----------------------------------------------------------------------------------------------
            // Bestimmen der Sicherheitsmerkmale, die für einen Mitarbeiterzugriff relevant sind:

            if (getSecFForCoWorker)
            {
                // Bestimmen, ob eine Standardbaugruppe vorliegt.
                var getStdBg = GetStdBgAndZATFor(AssyBomPos.MatNoOfCurrentBomPos);

                if (!getStdBg.Succeeded)
                {
                    ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Failed(value: null, getStdBg.ToPlx());
                }
                else
                {
                    secF.isStandardBaugruppe = getStdBg.Value.IsStdBg;
                    secF.ZAT = getStdBg.Value.IsStdBgAtSite;


                    // Bestimmen der Standortfreischaltungen
                    var getSiteAccess = GetSiteActivationsFor(AssyBomPos.MatNoOfCurrentBomPos);

                    if (!getSiteAccess.Succeeded)
                    {
                        ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Failed(value: null, getSiteAccess.ToPlx());
                    }
                    else
                    {
                        secF.PublicForAll = getSiteAccess.Value.publicForAll;
                        secF.SitesAccessAllowed = getSiteAccess.Value.siteAccess;

                        ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Ok(secF);
                    }
                }
            }

            //IsCustProject
            if (!string.IsNullOrWhiteSpace(getForCustGroup))
            {
                // When customer access, determine used in projects

                var sqlPrj = new SQL<DFCSecurity.DocSecurityFeatures>();

                var qPrj = sqlPrj.Select(
                        sqlPrj.Map(Tables.MaraPj._.EVW.FQN, (bo, v) =>
                            bo.isEVWP = !string.IsNullOrEmpty(ColTool.GetSave(v, ""))),
                        sqlPrj.Map(Tables.MaraPj._.Doku.FQN, (bo, v) =>
                            bo.isRelevantForDocumentation = !string.IsNullOrEmpty(ColTool.GetSave(v, "")))

                    )
                    .EqJoinFrom((Tables.MaraPj._.PjNr, Tables.Projektliste2._.PrjNr))
                    .Where(sqlPrj.And(
                            sqlPrj.IsNotNull(Tables.Projektliste2._.CustAccess.FQN),
                            sqlPrj.LikeUpperCase(Tables.Projektliste2._.CustAccess.FQN, sqlPrj.Txt($"%{getForCustGroup}%")),
                            sqlPrj.Eq(Tables.MaraPj._.MatNr.FQN, sqlPrj.Txt(AssyBomPos.MatNoOfCurrentBomPos)),
                            sqlPrj.Eq(Tables.MaraPj._.PjNr.FQN, sqlPrj.Int(pspBom.ProjectNo)),
                            sqlPrj.Eq(Tables.MaraPj._.StatNr.FQN, sqlPrj.Int(pspBom.StationNo))
                        ))
                    .done();

                // Ein- und dieselbe Baugruppe, kann in einer Station an verschiedenen Stellen
                // verbaut sein. Jedoch ist der Inhalt wegen dem Materialnummern- Eindeutigkeitsaxiom immer derselbe. Deshalb
                // reicht es aus, ein einziges, stellvertretendes Exemplar der Stücklistenposition mit der Baugruppe X
                // hier abzurufen.
                var getPrj = GetRecord(qPrj);

                if (!getPrj.Succeeded)
                {
                    ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Failed(value: secF, ErrorDescription: getPrj.MessageEntity, User: getPrj.User);
                }
                if (getPrj.Value.IsEmpty)
                {
                    secF.IsCustProject = false;
                    ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Ok(secF);
                }
                else
                {
                    // Falls Einträge in MaraPJ und Projektliste für das Projekt zu Kundengruppe gefunden wurden, dann ist das Projekt ein Kundenprojekt
                    secF.IsCustProject = true;
                    secF.isEVWP = getPrj.Value.Entity.isEVWP;
                    secF.isRelevantForDocumentation = getPrj.Value.Entity.isRelevantForDocumentation;

                    ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Ok(secF);
                }
            }

            return ret;
        }

        /// <summary>
        /// mko, 20.4.2020
        /// Kundensicht:
        ///     Merkmale werden immer projetspezikfisch bewertet. Entscheidend ist hier, ob die Baugruppe zu einem 
        ///     Kundenprojekt gehört. Auch Merkmale wie EVWP sind von Interesse.
        /// 
        /// Mitarbeitersicht:
        ///     Ist die Baugruppe für den Standort freigeschaltet (ATMO-weite Standardbaugruppe, ZAT oder explizit für 
        ///     Standort freigeschaltet)
        ///     
        /// </summary>
        /// <param name="pspBom">PSP- Nummer des Projektkontextes</param>        
        /// <param name="bomMatNo">Materialnummer der Stückliste, in der das Einzelteil als Stücklistenposition gelistet ist</param>
        /// <param name="SPBomPos">Stücklistenposition des Einzelteils</param>
        /// <param name="getForCustGroup">Kundengruppe, unter der der aktuelle User angemeldet ist. Wenn der User kein Kunde ist, und kein 
        /// Kundenkontext gefordert ist, dann ist hier eine leere Zeichenkette zu übergeben.</param> 
        /// <param name="getSecFForCoWorker">Wenn true, dann werden die im Kontext eines Zugriffs durch einen Mitarbeiter relevanten Sicherheitsmerkmale bestimmt</param>               
        /// <returns></returns>
        public RCV3sV<DFCSecurity.DocSecurityFeatures> GetSecurityFeaturesOfCharacteristicValuesOfSP(
                    DfcTree.IPSPBom pspBom,
                    string bomMatNo,
                    DfcTree.IMatBomNodePosition SPBomPos,
                    string getForCustGroup = "",
                    bool getSecFForCoWorker = true)
        {
            var ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Failed(value: null, pnL.eNotCompleted());

            var secF = new DFCSecurity.DocSecurityFeatures()
            {
                DocType = DocTypeSAP.CharacteristicValues,
                UserState = DfcDocStates.none,
                MatNo = SPBomPos.MatNoOfCurrentBomPos,
                TypeOfSecurableObject = DFCSecurity.SecuredDocs.CV,
                UsedInProjects = new long[] { },

                // Einzelteile sind keien Baugruppen, und damit auch keine Standardbaugruppen
                isStandardBaugruppe = false,
                ZAT = new DFCSecurity.Site[] { }
            };

            //----------------------------------------------------------------------------------------------
            // Bestimmen der Sicherheitsmerkmale, die für einen Mitarbeiterzugriff relevant sind:

            if (getSecFForCoWorker)
            {
                // Bestimmen, ob eine Standardbaugruppe vorliegt.
                //var getStdBg = GetStdBgAndZATFor(SPBomPos.MatNoOfCurrentBomPos);


                // Bestimmen der Standortfreischaltungen
                var getSiteAccess = GetSiteActivationsFor(SPBomPos.MatNoOfCurrentBomPos);

                if (!getSiteAccess.Succeeded)
                {
                    ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Failed(value: null, getSiteAccess.ToPlx());
                }
                else
                {
                    secF.PublicForAll = getSiteAccess.Value.publicForAll;
                    secF.SitesAccessAllowed = getSiteAccess.Value.siteAccess;

                    ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Ok(secF);
                }
            }

            //IsCustProject
            if (!string.IsNullOrWhiteSpace(getForCustGroup))
            {
                // When customer access, determine used in projects

                var sqlPrj = new SQL<DFCSecurity.DocSecurityFeatures>();

                var qPrj = sqlPrj.Select(
                        sqlPrj.Map(Tables.MaraPj._.EVW.FQN, (bo, v) =>
                            bo.isEVWP = !string.IsNullOrEmpty(ColTool.GetSave(v, ""))),
                        sqlPrj.Map(Tables.MaraPj._.Doku.FQN, (bo, v) =>
                            bo.isRelevantForDocumentation = !string.IsNullOrEmpty(ColTool.GetSave(v, "")))
                    )
                    .EqJoinFrom((Tables.MaraPj._.PjNr, Tables.Projektliste2._.PrjNr))
                    .Where(sqlPrj.And(
                            sqlPrj.IsNotNull(Tables.Projektliste2._.CustAccess.FQN),
                            sqlPrj.LikeUpperCase(Tables.Projektliste2._.CustAccess.FQN, sqlPrj.Txt($"%{getForCustGroup}%")),
                            sqlPrj.Eq(Tables.MaraPj._.CVBG.FQN, sqlPrj.Txt(bomMatNo)),
                            sqlPrj.Eq(Tables.MaraPj._.CVPOS.FQN, sqlPrj.Int(SPBomPos.CurrentBomPos)),
                            sqlPrj.Eq(Tables.MaraPj._.MatNr.FQN, sqlPrj.Txt(SPBomPos.MatNoOfCurrentBomPos)),
                            sqlPrj.Eq(Tables.MaraPj._.PjNr.FQN, sqlPrj.Int(pspBom.ProjectNo)),
                            sqlPrj.Eq(Tables.MaraPj._.StatNr.FQN, sqlPrj.Int(pspBom.StationNo))
                        ))
                    .done();

                // Ein- und dieselbe Baugruppe, welche das hier betrachtete Einzelteil enthält kann in einer Station an verschiedenen Stellen
                // verbaut sein. Jedoch ist der Inhalt wegen dem Materialnummern- Eindeutigkeitsaxiom immer derselbe. Deshalb
                // reicht es aus, ein einziges, stellvertretendes Exemplar der Stücklistenposition mit dem Einzelteil in einer Baugruppe X
                // hier abzurufen.
                var getPrj = GetRecord(qPrj);

                if (!getPrj.Succeeded)
                {
                    ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Failed(value: secF, ErrorDescription: getPrj.MessageEntity, User: getPrj.User);
                }
                else if (getPrj.Value.IsEmpty)
                {
                    secF.IsCustProject = false;
                    ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Ok(secF);
                }
                else
                {
                    // Falls Einträge in MaraPJ und Projektliste für das Projekt zu Kundengruppe gefunden wurden, 
                    // dann gehört das bewertete Einzelteil zu einem Kundenprojekt
                    secF.IsCustProject = true;
                    secF.isEVWP = getPrj.Value.Entity.isEVWP;
                    secF.isRelevantForDocumentation = getPrj.Value.Entity.isRelevantForDocumentation;

                    ret = RCV3sV<DFCSecurity.DocSecurityFeatures>.Ok(secF);
                }
            }

            return ret;

        }



        /// <summary>
        /// mko, 13.7.2018
        /// 
        /// mko, 26.8.2018
        /// Get's now security information for older versions
        /// 
        /// mko, 13.9.2018
        /// Rückgabetyp auf RCV3WithValue umgestellt. 
        /// Parsen der PSPNr erfolgt nun mit dem allgemeinen PSPNrParser.
        /// Wenn in der PSPnr keine Station definiert wurde, dann wird Stationsnummer 0 eingesetzt.
        /// </summary>
        /// <param name="DocId"></param>
        /// <param name="getEVWForCustGroup">For this customer group the EVW will be queried. If empty, then no EVW will queried</param>
        /// <returns></returns>
        public RCV3sV<ResultSet<ATMODocument>> ShowTecDocs(string PSPnr, bool CustomerAccess)
        {
            var sql = new SQL<ATMODocument>();
            var ret = RCV3sV<ResultSet<ATMODocument>>.Failed(value: null, ErrorDescription: null);

            try
            {
                // parse project and station- number
                (long prjNo, long statNo) = DFCTools.PSPNrParser.ParsePSP(PSPnr);

                // mko, 1.10.2018
                statNo = statNo == -1 ? 0 : statNo;

                var keyMatch = sql.And(
                                    sql.Eq(Tables.Projektliste2._.PrjNr.FQN, sql.Long(prjNo)),
                                    sql.Eq(Tables.Projektliste2._.StatNr.FQN, sql.Int((int)statNo)),
                                    //sql.Eq(Tables.PathView._.UserState, sql.Int(10)),
                                    sql.Eq(Tables.PathView._.XType, sql.Txt("TDP"))
                                );


                string infos = "";
                var ProjectNo = 0L;
                var StationNo = 0L;
                var sel = sql.Select(

                        sql.Map(Tables.PathView._.ProjectNo.FQN, (bo, v) => ProjectNo = ColTool.GetSave(v, -1)),
                        sql.Map(Tables.PathView._.StationNo.FQN, (bo, v) => StationNo = ColTool.GetSave(v, (short)-1)),

                        sql.Map(Tables.PathView._.DocId.FQN, (bo, v) => bo.DocID = ColTool.GetSave(v.ToString(), "")),
                        sql.Map(Tables.PathView._.TDPTyp.FQN, (bo, v) => bo.TDPTYPE = ColTool.GetSave(v, "")),
                        sql.Map(Tables.PathView._.MatNr.FQN, (bo, v) => bo.MatNr = ColTool.GetSave(v, "")),
                        sql.Map(Tables.PathView._.XType.FQN, (bo, v) => bo.DocumentType = (DocTypeSAP)Enum.Parse(typeof(DocTypeSAP), ColTool.GetSave(v, "Unknown"), true)),
                        sql.Map(Tables.PathView._.UserState.FQN, (bo, v) => bo.UserState = ((DfcDocStates)ColTool.GetSave(v, 0)).ToString()),
                        sql.Map(Tables.PathView._.StatusChangeOriginator, (bo, v) => bo.StateEditor = ColTool.GetSave(v, "")),
                        sql.Map(Tables.PathView._.Baselocation, (bo, v) => bo.docBaseLocation = (BaseLocation)(int)ColTool.GetSave(v, (long)BaseLocation.Unknown)),
                        sql.Map(Tables.PathView._.StorageType, (bo, v) => bo.DocStorageType = ColTool.GetSave(v.ToString(), "")),
                        sql.Map(Tables.Projektliste2._.Bennenung, (bo, v) => bo.ProjName = ColTool.GetSave(v, "")),
                        sql.Map(Tables.PathView._.Infos.FQN, (bo, v) =>
                        {
                            //var tag = "|INFOS:=";
                            infos = ColTool.GetSave(v, "");
                            var getInfoPart = DFCObjects.Parser.InfosParser.GetInfosPart(infos, "INFOS", pnL);
                            if (getInfoPart.Succeeded)
                            {
                                bo.INFOS = getInfoPart.Value;
                                if (StationNo != 0)
                                {
                                    bo.Name = $"{DFCTools.PSPNrParser.PSPPrefixFor(ProjectNo)}.{ProjectNo}.{StationNo.ToString("D3")}: {bo.ProjName} Infos: {bo.INFOS}";
                                }
                                else
                                {
                                    bo.Name = $"{DFCTools.PSPNrParser.PSPPrefixFor(ProjectNo)}.{ProjectNo}: {bo.ProjName} Infos: {bo.INFOS}";
                                }
                            }
                            else
                            {
                                bo.INFOS = infos;
                                bo.Name = infos;
                            }
                        })
                    )
                    .From(Tables.PathView._, Tables.Projektliste2._);

                QueryBuilderResult<ATMODocument> q = null;

                if (CustomerAccess)
                {
                    q = sel.Where(
                                sql.And(
                                    sql.Eq(Tables.PathView._.MatNr.FQN, Tables.Projektliste2._.MatNr.FQN),
                                    keyMatch,
                                    sql.NotEq(Tables.PathView._.TDPTyp.FQN, sql.Txt("02")),
                                    sql.NotEq(Tables.PathView._.TDPTyp.FQN, sql.Txt("17"))))
                            .By(Tables.PathView._.TDPTyp).done();
                }
                else
                {
                    q = sel.Where(
                            sql.And(
                                    sql.Eq(Tables.PathView._.MatNr.FQN, Tables.Projektliste2._.MatNr.FQN),
                                    keyMatch))
                           .By(Tables.PathView._.TDPTyp).done();
                }

                var res = GetRecords(q);

                if (res.Succeeded)
                {
                    ret = RCV3sV<ResultSet<ATMODocument>>.Ok(res.Value);
                }
                else
                {
                    ret = RCV3sV<ResultSet<ATMODocument>>.Failed(value: null, ErrorDescription: pnL.i("QuerySQL", pnL.eFails(pnL.EncapsulateAsEventParameter(res.MessageEntity))));
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<ResultSet<ATMODocument>>.Failed(value: null, ErrorDescription: TraceHlp.FlattenExceptionMessagesPN(ex));
            }

            return ret;
        }


        //static (SQL<ATMODocument>, WhereBuilder<ATMODocument>) Select()
        //{
        //    var sql = new SQL<ATMODocument>();
        //    return (sql, sql.Select(
        //            sql.Map(Tables.Path._.DocId, (bo, v) => bo.DocID = ColTool.GetSave(v, "")),
        //            sql.Map(Tables.Path._.MatNr, (bo, v) => bo.MatNr = ColTool.GetSave(v, "")),
        //            sql.Map(Tables.Path._.XType, (bo, v) => bo.DocumentType = (DocTypeSAP)Enum.Parse(typeof(DocTypeSAP), ColTool.GetSave(v, "Unknown"), true)),
        //            sql.Map(Tables.Path._.UserState, (bo, v) => bo.UserState = ColTool.GetSave(v, ((int)DfcDocStates.none).ToString())),
        //            sql.Map(Tables.Path._.StatusChangeOriginator, (bo, v) => bo.StateEditor = ColTool.GetSave(v, "")),
        //            sql.Map(Tables.Path._.Infos, (bo, v) => bo.INFOS = ColTool.GetSave(v, ""))
        //        )
        //        .From(Tables.Path._));
        //}


        /// <summary>
        /// mko, 17.9.2018
        /// Gets all informations of a ducument for a given DocId
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public RCV3sV<Doc.Document> GetPathBo(long docId)
        {
            var sql = new SQL<Doc.Document>();

            Tables.IPath PathTab = Tables.Path._;

            var query = SelectFromPath(sql, PathTab)
                .Where(sql.Eq(PathTab.DocId.FQN, sql.Long(docId)))
                .done();

            var ret = GetRecord(query);

            if (ret.Succeeded && !ret.Value.IsEmpty)
            {
                return RCV3sV<Doc.Document>.Ok(value: ret.Value.Entity, Message: plxResFactory.CreateQueryResultOk(1L));
            }
            else if (ret.Succeeded && ret.Value.IsEmpty)
            {
                return RCV3sV<Doc.Document>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryResultEmpty());
            }
            else
            {
                return RCV3sV<Doc.Document>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(ret.ToPlx()));
            }
        }

        /// <summary>
        /// mko, 28.6.2019
        /// 
        /// Liefert zu einer DokuMat- Nummer die Tabelleneintrag aus. Falls kein Tabelleneintrag vorhanden ist,
        /// wird in Succeeded false zurückgeliefert, und in MessageEntity die Anzeige Empty. 
        /// </summary>
        /// <param name="DouMatNo"></param>
        /// <returns></returns>
        public RCV3sV<IEnumerable<DocuMatBo>> GetDocuMat(string DocuMatNo)
        {
            var ret = RCV3sV<IEnumerable<DocuMatBo>>.Failed(null, pnL.ReturnNotCompleted("GetDocuMat", pnL.p("DocuMatNo", DocuMatNo)));

            var sql = new SQL<Bo.DocuMatBo>();
            var tab = new Tables.DocuMat();

            var q = sql.Select(
                            sql.Map(tab.ID, (bo, v) => bo.ID = ColTool.GetSave(v, "")),
                            sql.Map(tab.MatNo, (bo, v) =>
                                                            bo.MatNo = ColTool.GetSave(v, "")),
                            sql.Map(tab.ManualLanguageISO, (bo, v) =>
                                                            bo.ManualLanguageISO = ColTool.GetSave(v, "")),
                            sql.Map(tab.DocMatNo, (bo, v) =>
                                                            bo.DocMatNo = ColTool.GetSave(v, "")),
                            sql.Map(tab.LastUpdate, (bo, v) =>
                                                            bo.LastUpdate = ColTool.GetSave(v, DateTime.MinValue)),
                            sql.Map(tab.UpdatedBy, (bo, v) =>
                                                            bo.UpdatedBy = ColTool.GetSave(v, "")),
                            sql.Map(tab.Created, (bo, v) =>
                                                            bo.Created = ColTool.GetSave(v, DateTime.MinValue)),
                            sql.Map(tab.Creator, (bo, v) =>
                                                            bo.Creator = ColTool.GetSave(v, "")))
                        .From(tab)
                        .Where(sql.Eq(tab.DocMatNo, sql.Txt(DocuMatNo)))
                        .done();

            var getMan = GetRecords(q);

            if (!getMan.Succeeded)
            {
                ret = RCV3sV<IEnumerable<DocuMatBo>>.Failed(null, getMan.ToPlx());
            }
            else if (getMan.Value.IsEmpty)
            {
                ret = RCV3sV<IEnumerable<DocuMatBo>>.Failed(null, pnL.ReturnSearchFailsEmptyResult());
            }
            else
            {
                ret = RCV3sV<IEnumerable<DocuMatBo>>.Ok(getMan.Value.Entities);
            }

            return ret;
        }

        /// <summary>
        /// mko, 3.5.2019
        /// Attribut- Bezeichner in Infos von Manual- Path- Datensätzen
        /// </summary>
        const string _ManualSupportedLang = "MAN_LANG";

        /// <summary>
        /// mko, 19.9.2018
        /// Liest alle Dokumente ein, die einer Materialnummer zugeordnet sind. Das Ergebnis kann auf einen 
        /// speziellen Dokumenttyp eingeschränkt werden
        /// 
        /// mko, 2.5.2019
        /// Zugeordnete Materialnummer eines Manuals bestimmen via DokuMat Tabelle.
        /// 
        /// Da es zu einer Materialnummer verschiedne Manuals geben kann (im Extremfall pro Sprache ein Manual
        /// </summary>
        /// <param name="MatNr"></param>
        /// <param name="docType"></param>
        /// <returns></returns>
        public RCV3sV<IEnumerable<Doc.Document>> GetPathBo(string MatNr, DocTypeSAP docType = DocTypeSAP.All, bool LatestVersionOnly = false)
        {
            var ret = RCV3sV<IEnumerable<Doc.Document>>.Ok(value: null);

            // mko, 2.5.2019
            // Sonderbehandlung für Manuals            
            if (docType == DocTypeSAP.MAN)
            {
                // Beziehung zwischen Materialnummern, Sprache, in der Manuals veröffentlicht werden und Manual- Materialnummern (DokuMat):
                // MatNo 1--N Language N--1 DocuMat

                // Bestimmen der Manuals, die einer Materialnummer zugeordnet sind.
                // Jedes Manual kann 1 bis n Sprachen abdecken.
                var sql = new SQL<Bo.DocuMatBo>();
                var tab = new Tables.DocuMat();

                // mko, 5.7.2019
                // Um Dokumatnummern von Materialnummern zu unterscheiden, werden diese durch ein Präfix unterschieden
                bool isDokumatNo = false;
                if (MatNr.Replace(".", "").StartsWith(ATMO.DFC.Material.MatNoParser.PrefixDocuMatNo))
                {
                    isDokumatNo = true;
                    MatNr = MatNr.Replace(".", "").Substring(ATMO.DFC.Material.MatNoParser.PrefixDocuMatNo.Length);
                }

                var q = sql.Select(
                                sql.Map(tab.ID, (bo, v) => bo.ID = ColTool.GetSave(v, "")),
                                sql.Map(tab.MatNo, (bo, v) =>
                                                                bo.MatNo = ColTool.GetSave(v, "")),
                                sql.Map(tab.ManualLanguageISO, (bo, v) =>
                                                                bo.ManualLanguageISO = ColTool.GetSave(v, "")),
                                sql.Map(tab.DocMatNo, (bo, v) =>
                                                                bo.DocMatNo = ColTool.GetSave(v, "")),
                                sql.Map(tab.LastUpdate, (bo, v) =>
                                                                bo.LastUpdate = ColTool.GetSave(v, DateTime.MinValue)),
                                sql.Map(tab.UpdatedBy, (bo, v) =>
                                                                bo.UpdatedBy = ColTool.GetSave(v, "")),
                                sql.Map(tab.Created, (bo, v) =>
                                                                bo.Created = ColTool.GetSave(v, DateTime.MinValue)),
                                sql.Map(tab.Creator, (bo, v) =>
                                                                bo.Creator = ColTool.GetSave(v, "")))
                            .From(tab)
                            .SwitchedWhere(
                                (isDokumatNo, sql.Eq(tab.DocMatNo, sql.Txt(MatNr))),
                                (!isDokumatNo, sql.Eq(tab.MatNo, sql.Txt(MatNr)))
                             )
                            //.Where(sql.Eq(tab.MatNo, sql.Txt(MatNr)))
                            .done();

                var getMan = GetRecords(q);

                if (getMan.Succeeded && !getMan.Value.IsEmpty)
                {
                    // Manuals werden durch Gruppen von Dokumat- Datensätzen definiert, die alle dieselbe 
                    // DokuMat Nummer-, aber verschiedene Sprachzuordnungen haben.
                    var Manuals = getMan.Value.Entities.GroupBy(r => r.DocMatNo);

                    // Zu jedem Manual sein Dokument und optional die Dokumente der Vorversionen abrufen
                    var sql2 = new SQL<Doc.Document>();

                    // mko, 28.11.2019
                    // Aktuellste Version der Dokumente wird aus Performance- Gründen aus der PathView ausgelesen.
                    Tables.IPath PathTab = LatestVersionOnly ? (Tables.IPath)new Tables.PathView() : new Tables.Path();

                    var ManualsAndOldVersions = new List<Doc.Document>();

                    // mko, 18.7.2019
                    // Erfassen  aller Dateninkonsitenzen beim Abruf der Dokumente zu Manuals
                    var ReportsAboutDataInconsitencies = new List<IInstance>();

                    foreach (var man in Manuals)
                    {
                        // Zusammenfassen der vom Manual unterstützten Sprachen zu einer kommaseparierten Liste
                        var supportedLang = string.Join(",", man.OrderBy(r => r.ManualLanguageISO).Select(r => r.ManualLanguageISO).Distinct());

                        // Abrufen des Dokumentes und seiner Vorversionen zu einem Manual
                        var q2 = SelectFromPath(sql2, PathTab)
                            .Where(
                                sql.And(
                                    sql2.Eq(PathTab.MatNr.FQN, sql2.Txt(man.Key)),
                                    sql2.Eq(PathTab.XType.FQN, sql2.Txt(DocTypeSAP.MAN.ToString()))))
                            .ByDescending(PathTab.IterationNo)
                            .done();

                        var getDocs = GetRecords(q2);

                        if (getDocs.Succeeded && !getDocs.Value.IsEmpty)
                        {
                            bool first = true;

                            // Anfügen der unterstützen Sprachen an die Dokumente
                            foreach (var doc in getDocs.Value.Entities)
                            {
                                doc.Infos = $"{_ManualSupportedLang}:={supportedLang}";

                                // Aktuellstes Manual, welches den Hauptknoten darstellt, nochmals markieren
                                if (first)
                                {
                                    doc.Infos += $"|Manual:={man.Key}";
                                    first = false;
                                }
                            }
                            ManualsAndOldVersions.AddRange(getDocs.Value.Entities);
                        }
                        else if (getDocs.Succeeded && getDocs.Value.IsEmpty)
                        {
                            // Werden zu einem Manual, das lt. Dokumat- Tabelle einer Materialnummer zugewiesen ist, keine 
                            // Dokumente gefunden, dann sind die Daten in der Datenbank inkonsistent, und es wird ein Fehler 
                            // zurückgemeldet.
                            //
                            // mko, 18.7.2019
                            // Jetzt wird kein Fehler zurückgemeldet Fehler, sondern eine Beschreibung der Inkonsitenzen verfasst.
                            ReportsAboutDataInconsitencies.Add(

                                pnL.ReturnFetchWithDetails(Succeeded: false,
                                        UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID, // TechTerms.Storage.Table, "Path"),
                                        UID_of_DataType: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.PathTab.UID,
                                        Details: pnL.i(ANC.TechTerms.Access.Datasources.DataInconsistency.UID,
                                                      pnL.p_NID(ANC.DocuTerms.MetaData.Details.UID, ANC.TechTerms.ATMO.DocuCheck.ForManualWithDefinedDokumatNumberDoesNotExistsADocumentInPath.UID)),

                                        pnL.Predicate(ANC.TechTerms.Operators.Relations.Eq.UID, pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, MatNr)),
                                        pnL.Predicate(ANC.TechTerms.Operators.Relations.Eq.UID, pnL.p(ANC.TechTerms.ATMO.DFC.SAPDocType.UID, DocTypeSAP.MAN.ToString())),
                                        pnL.Predicate(ANC.TechTerms.Operators.Relations.Eq.UID, pnL.p(ANC.TechTerms.ATMO.DFC.DocuMatNo.UID, man.Key))));

                            //pnL.EmbedMembers(man.OrderBy(r => r.ManualLanguageISO).Select(r => pnL.p("LangISO", r.ManualLanguageISO)).ToArray())}));
                        }
                        else
                        {
                            // Ein allgemeiner Fehler beim Abruf der Dokumente liegt vor.
                            ret = RCV3sV<IEnumerable<Doc.Document>>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(getMan.ToPlx()));
                            break;
                        }
                    }

                    if (ret.Succeeded && !ReportsAboutDataInconsitencies.Any())
                    {
                        // Dokumente zu Manuals wurden gefunden. Es wird die Liste der Dokumente, die Anzahl der gefundenen Dokumente und die Anzahl der Manuals, denen die Dokumente
                        // zugeordnet sind, zurückgegeben.
                        ret = RCV3sV<IEnumerable<Doc.Document>>.Ok(
                            value: ManualsAndOldVersions,
                            plxResFactory.CreateQueryResultOk(
                                ManualsAndOldVersions.Count,
                                pnL.i(ANC.TechTerms.ATMO.DFC.MAN.UID,
                                    pnL.p(ANC.TechTerms.Metrology.Counter.UID, Manuals.Count()))));
                    }
                    else if (ret.Succeeded && ReportsAboutDataInconsitencies.Any())
                    {
                        // Nicht alle Dokumente konnten wegen Konsitenzfehler abgerufen werden
                        ret = RCV3sV<IEnumerable<Doc.Document>>.Ok(
                            value: ManualsAndOldVersions,
                            plxResFactory.CreateQueryResultOk(ManualsAndOldVersions.Count,
                                pnL.m(ANC.TechTerms.Access.Datasources.DataIntegrityCheck.UID,
                                    pnL.ret(pnL.eWarn(
                                        pnL.i(ANC.DocuTerms.MetaData.Details.UID,
                                            pnL.EmbedMembers(ReportsAboutDataInconsitencies.ToArray())))))));
                    }
                }
                else if (getMan.Succeeded && getMan.Value.IsEmpty)
                {

                    if (!isDokumatNo)
                    {
                        // mko, 9.9.2019
                        // Prüfen, ob Manuals bereits in der Path- Tabelle angelegt wurden, jedoch noch nicht in der DokuMat- Tabelle verzeichnet sind.
                        // Dieser Zustand tritt ein, wenn neue Manuals angelegt wurden am Tag, und der nächtliche DokuMat- Lauf noch nicht durchgeführt wurde

                        // Zu jedem Manual sein Dokument und optional die Dokumente der Vorversionen abrufen
                        var sql2 = new SQL<Doc.Document>();
                        var PathTab = new Tables.PathView();

                        var q2 = SelectFromPath(sql2, PathTab)
                            .Where(
                                sql.And(
                                    sql2.Eq(PathTab.MatNr.FQN, sql2.Txt(MatNr)),
                                    sql2.Eq(PathTab.XType.FQN, sql2.Txt(DocTypeSAP.MAN.ToString()))))
                            .done();

                        var getDocs = GetRecord(q2);

                        if (getDocs.Succeeded && !getDocs.Value.IsEmpty)
                        {
                            ret = RCV3sV<IEnumerable<Doc.Document>>.Ok(
                                    value: new Doc.Document[] { getDocs.Value.Entity },
                                    Message: plxResFactory.CreateQueryResultOk(1,
                                                pnL.ReturnFetchWithDetails(Succeeded: false,
                                                    UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                    UID_of_DataType: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.DokuMatTab.UID,
                                                    Details: pnL.i(ANC.TechTerms.Access.Datasources.DataInconsistency.UID,
                                                                  pnL.p_NID(ANC.DocuTerms.MetaData.Details.UID, ANC.TechTerms.ATMO.DocuCheck.NoDokuMatNoIsCurrentlyAssignedToManual.UID)),

                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID, pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, MatNr)),
                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID, pnL.p(ANC.TechTerms.ATMO.DFC.SAPDocType.UID, DocTypeSAP.MAN.ToString())))));

                        }
                        else
                        {
                            ret = RCV3sV<IEnumerable<Doc.Document>>.Ok(value: new Doc.Document[] { }, Message: plxResFactory.CreateQueryResultEmpty());
                        }

                    }
                    else
                    {
                        // Zu einer Materialnummer liegen keine Manuals vor. 
                        ret = RCV3sV<IEnumerable<Doc.Document>>.Ok(value: new Doc.Document[] { }, Message: plxResFactory.CreateQueryResultEmpty());

                    }
                }
                else
                {
                    // Ein allgemeiner Fehler beim Abruf der Manuals zu einer Materialnummer ist aufgetreten.
                    ret = RCV3sV<IEnumerable<Doc.Document>>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(getMan.ToPlx()));
                }
            }
            else
            {
                // Abruf von Dokumenten, die keine Manuals und einer Materialnummer zugeordnet sind
                var sql = new SQL<Doc.Document>();

                // mko, 28.11.2019
                // Aktuellste Version der Dokumente wird aus Performance- Gründen aus der PathView ausgelesen.
                Tables.IPath PathTab = LatestVersionOnly ? (Tables.IPath)new Tables.PathView() : new Tables.Path();

                var q = SelectFromPath(sql, PathTab)
                    .Where(
                        sql.And(
                            sql.Eq(PathTab.MatNr.FQN, sql.Txt(MatNr)),
                            (docType != DocTypeSAP.All ? sql.Eq(PathTab.XType.FQN, sql.Txt(docType.ToString())) : (IColXpr)sql.Nop())
                            ))
                    .ByDescending(PathTab.IterationNo)
                    .done();

                var getRec = GetRecords(q);

                if (getRec.Succeeded && !getRec.Value.IsEmpty)
                {
                    // Es wurden Dokumente gefunden
                    ret = RCV3sV<IEnumerable<Doc.Document>>.Ok(value: getRec.Value.Entities, Message: plxResFactory.CreateQueryResultOk(getRec.Value.Entities.Count()));
                }
                else if (getRec.Succeeded && getRec.Value.IsEmpty)
                {
                    // Es wurden keine Dokumente gefunden
                    ret = RCV3sV<IEnumerable<Doc.Document>>.Ok(value: new Doc.Document[] { }, Message: plxResFactory.CreateQueryResultEmpty());
                }
                else
                {
                    // Es liegt ein allgemeiner Fehler vor
                    ret = RCV3sV<IEnumerable<Doc.Document>>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(getRec.ToPlx()));
                }
            }

            return ret;
        }

        /// <summary>
        /// mko, 19.9.2018
        /// Liest alle Dokumente ein, die einer Projekt bzw Stationsnummer zugeordnet sind. Das Ergebnis kann auf einen 
        /// speziellen Dokumenttyp eingeschränkt werden.
        /// 
        /// mko, 27.11.2018
        /// Zusätzlich einschränken auf LatestVersion == X (aktuellste Version)
        /// </summary>
        /// <param name="PrjNr"></param>
        /// <param name="StatNr">if -1, then content of all stations of a project will be collected</param>
        /// <param name="docType"></param>
        /// <returns></returns>
        public RCV3sV<IEnumerable<Doc.Document>> GetPathBo(long PrjNr, long StatNr = -1, DocTypeSAP docType = DocTypeSAP.All, bool LatestVersionOnly = false)
        {
            var sql = new SQL<Doc.Document>();

            QueryBuilderResult<Doc.Document> query = null;

            // mko, 28.11.2019
            // Aktuellste Version der Dokumente wird aus Performance- Gründen aus der PathView ausgelesen.
            Tables.IPath PathTab = LatestVersionOnly ? (Tables.IPath)Tables.PathView._ : Tables.Path._;

            query = SelectFromPath(sql, PathTab)
                    .Where(
                        sql.And(
                            sql.Eq(PathTab.ProjectNo.FQN, sql.Long(PrjNr)),
                            (StatNr != -1 ? (IColXpr)sql.Eq(PathTab.StationNo.FQN, sql.Long(StatNr)) : sql.Nop()),
                            (docType != DocTypeSAP.All ? (IColXpr)sql.Eq(PathTab.XType.FQN, sql.Txt(docType.ToString())) : sql.Nop())
                            ))
                    .done();

            var ret = GetRecords(query);

            if (ret.Succeeded && !ret.Value.IsEmpty)
            {
                return RCV3sV<IEnumerable<Doc.Document>>.Ok(value: ret.Value.Entities, Message: plxResFactory.CreateQueryResultOk(ret.Value.Entities.Count()));
            }
            else if (ret.Succeeded && ret.Value.IsEmpty)
            {
                return RCV3sV<IEnumerable<Doc.Document>>.Ok(value: new Doc.Document[] { }, Message: plxResFactory.CreateQueryResultEmpty());
            }
            else
            {
                return RCV3sV<IEnumerable<Doc.Document>>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(ret.ToPlx()));
            }
        }

        /// <summary>
        /// mko, 10.12.2018
        /// Liest alle Dokumente ein, die einer Materialnummer zugeordnet sind. Zusätzlich wird auf eine gegebene 
        /// Projekt und Stationsnummer eingeschränkt. 
        /// Diese Abfrage wird beim Zugriff über eine Projekt- und Stationsnummer eingesetzt. 
        /// Einer Materialnummer können z.B. TDP- Zertifikate aus verschiedenen Projekten zugeordnet sein. Durch 
        /// Einschränken auf eine Projekt/Stationsnummer werden nur die bestimmt, welche dem Projekt zugeordnet sind.
        /// Umgekehrt können einer Projekt/Stationsnummer SFC's verscheidener Materialnummern zugeordnet sein (nämlich
        /// die der Station selber und aller untergeordneter Baugruppen). Die zusätzlich Einschränkung auf eine Materialnummer
        /// entfernt alle, einer Baugruppe nicht unmittelbar zugeorndete Dokumente
        /// </summary>
        /// <param name="MatNr"></param>
        /// <param name="PrjNr"></param>
        /// <param name="StatNr"></param>
        /// <param name="docType"></param>
        /// <param name="LatestVersionOnly"></param>
        /// <returns></returns>
        public RCV3sV<IEnumerable<Doc.Document>> GetPathBo(string MatNr, long PrjNr, long StatNr = -1, DocTypeSAP docType = DocTypeSAP.All, bool LatestVersionOnly = false)
        {
            var sql = new SQL<Doc.Document>();

            QueryBuilderResult<Doc.Document> query = null;

            // mko, 28.11.2019
            // Aktuellste Version der Dokumente wird aus Performance- Gründen aus der PathView ausgelesen.
            Tables.IPath PathTab = LatestVersionOnly ? (Tables.IPath)Tables.PathView._ : Tables.Path._;

            query = SelectFromPath(sql, PathTab)
                    .Where(
                        sql.And(
                            sql.Eq(PathTab.ProjectNo.FQN, sql.Long(PrjNr)),
                            (StatNr == -1 ? sql.Nop() : (IColXpr)sql.Eq(PathTab.StationNo.FQN, sql.Long(StatNr))),
                            (string.IsNullOrWhiteSpace(MatNr) ? sql.Nop() : (IColXpr)sql.Eq(PathTab.MatNr.FQN, sql.Txt(MatNr))),
                            (docType == DocTypeSAP.All ? sql.Nop() : (IColXpr)sql.Eq(PathTab.XType.FQN, sql.Txt(docType.ToString())))
                            ))
                    .done();

            var ret = GetRecords(query);

            if (ret.Succeeded && !ret.Value.IsEmpty)
            {
                return RCV3sV<IEnumerable<Doc.Document>>.Ok(value: ret.Value.Entities, Message: plxResFactory.CreateQueryResultOk(ret.Value.Entities.Count()));
            }
            else if (ret.Succeeded && ret.Value.IsEmpty)
            {
                return RCV3sV<IEnumerable<Doc.Document>>.Ok(value: new Doc.Document[] { }, Message: plxResFactory.CreateQueryResultEmpty());
            }
            else
            {
                return RCV3sV<IEnumerable<Doc.Document>>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(ret.ToPlx()));
            }
        }


        /// <summary>
        /// mko, 14.12.2018        
        /// Liefert offnen SFC's, die einen User betreffen
        /// </summary>
        /// <param name="CreatorId"></param>
        /// <param name="docType"></param>
        /// <param name="UserState"></param>
        /// <param name="LatestVersionOnly"></param>
        /// <returns></returns>
        public RCV3sV<IEnumerable<Doc.Document>> GetOpenSFCsFor(string UserId)
        {
            var sql = new SQL<Doc.Document>();

            QueryBuilderResult<Doc.Document> query = null;

            // mko, 28.11.2019
            // Aktuellste Version der Dokumente wird aus Performance- Gründen aus der PathView ausgelesen.
            Tables.IPath PathTab = Tables.Path._;

            var userid = UserId.ToLower();

            query = SelectFromPath(sql, PathTab)
                    .Where(
                        sql.And(
                            sql.Eq(PathTab.XType.FQN, sql.Txt(DocTypeSAP.SFC.ToString())),
                            sql.Eq(PathTab.UserState.FQN, sql.Int((int)DfcDocStates.open)),
                            sql.Or(
                                sql.LikeLowerCase(PathTab.Infos.FQN, sql.Txt($"%konstrukteur:={userid}%")),
                                sql.LikeLowerCase(PathTab.Infos.FQN, sql.Txt($"%werkstattaenderer:={userid}%")),
                                sql.StrEq(PathTab.StatusChangeOriginator.FQN, sql.Txt(userid)),
                                sql.StrEq(PathTab.CreatorId.FQN, sql.Txt(userid))))).done();

            var ret = GetRecords(query);

            if (ret.Succeeded && !ret.Value.IsEmpty)
            {
                return RCV3sV<IEnumerable<Doc.Document>>.Ok(value: ret.Value.Entities, Message: plxResFactory.CreateQueryResultOk(ret.Value.Entities.Count()));
            }
            else if (ret.Succeeded && ret.Value.IsEmpty)
            {
                return RCV3sV<IEnumerable<Doc.Document>>.Ok(value: new Doc.Document[] { }, Message: plxResFactory.CreateQueryResultEmpty());
            }
            else
            {
                return RCV3sV<IEnumerable<Doc.Document>>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(ret.ToPlx()));
            }

        }

        /// <summary>
        /// mko, 14.12.2018        
        /// Liefert offnen EDC's, die einen User betreffen
        /// </summary>
        /// <param name="CreatorId"></param>
        /// <param name="docType"></param>
        /// <param name="UserState"></param>
        /// <param name="LatestVersionOnly"></param>
        /// <returns></returns>
        public RCV3sV<IEnumerable<Doc.Document>> GetOpenEDCsFor(string UserId)
        {
            var sql = new SQL<Doc.Document>();

            QueryBuilderResult<Doc.Document> query = null;

            // mko, 28.11.2019
            // Aktuellste Version der Dokumente wird aus Performance- Gründen aus der PathView ausgelesen.
            Tables.IPath PathTab = Tables.Path._;

            var userid = UserId.ToLower();

            query = SelectFromPath(sql, PathTab)
                    .Where(
                        sql.And(
                            sql.Eq(PathTab.XType.FQN, sql.Txt(DocTypeSAP.EDC.ToString())),
                            sql.Eq(PathTab.UserState.FQN, sql.Int((int)DfcDocStates.open)),
                            sql.Or(
                                sql.LikeLowerCase(PathTab.Infos.FQN, sql.Txt($"%vdp:={userid}%")),
                                sql.LikeLowerCase(PathTab.Infos.FQN, sql.Txt($"%vsm:={userid}%")),
                                sql.LikeLowerCase(PathTab.Infos.FQN, sql.Txt($"%vab:={userid}%")),
                                sql.LikeLowerCase(PathTab.Infos.FQN, sql.Txt($"%dienstleister:={userid}%")),
                                sql.LikeLowerCase(PathTab.Infos.FQN, sql.Txt($"%ersteller:={userid}%")),
                                sql.StrEq(PathTab.StatusChangeOriginator.FQN, sql.Txt(userid)),
                                sql.StrEq(PathTab.CreatorId.FQN, sql.Txt(userid)))))
                    .done();

            var ret = GetRecords(query);

            if (ret.Succeeded && !ret.Value.IsEmpty)
            {
                return RCV3sV<IEnumerable<Doc.Document>>.Ok(value: ret.Value.Entities, Message: plxResFactory.CreateQueryResultOk(ret.Value.Entities.Count()));
            }
            else if (ret.Succeeded && ret.Value.IsEmpty)
            {
                return RCV3sV<IEnumerable<Doc.Document>>.Ok(value: new Doc.Document[] { }, Message: plxResFactory.CreateQueryResultEmpty());
            }
            else
            {
                return RCV3sV<IEnumerable<Doc.Document>>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(ret.ToPlx()));
            }

        }


        /// <summary>
        /// mko, 14.12.2018        
        /// Liefert noch in Bearbeitung befindlichen ATD's (auch ShopFloor)
        /// </summary>
        /// <param name="CreatorId"></param>
        /// <param name="docType"></param>
        /// <param name="UserState"></param>
        /// <param name="LatestVersionOnly"></param>
        /// <returns></returns>
        public RCV3sV<IEnumerable<Doc.Document>> GetInWorkATDsFor(string UserId)
        {
            var sql = new SQL<Doc.Document>();

            QueryBuilderResult<Doc.Document> query = null;

            // mko, 28.11.2019
            // Aktuellste Version der Dokumente wird aus Performance- Gründen aus der PathView ausgelesen.
            Tables.IPath PathTab = Tables.PathView._;

            var userid = UserId.ToLower();

            query = SelectFromPath(sql, PathTab)
                    .Where(
                        sql.And(
                            sql.Eq(PathTab.XType.FQN, sql.Txt(DocTypeSAP.ATD.ToString())),
                            sql.StrEq(PathTab.StatusChangeOriginator.FQN, sql.Txt(userid)),
                            sql.Or(
                                sql.Eq(PathTab.UserState.FQN, sql.Int((int)DfcDocStates.inWork)),
                                sql.Eq(PathTab.UserState.FQN, sql.Int((int)DfcDocStates.shopFloor)))))
                            .done();

            var ret = GetRecords(query);

            if (ret.Succeeded && !ret.Value.IsEmpty)
            {
                return RCV3sV<IEnumerable<Doc.Document>>.Ok(value: ret.Value.Entities, Message: plxResFactory.CreateQueryResultOk(ret.Value.Entities.Count()));
            }
            else if (ret.Succeeded && ret.Value.IsEmpty)
            {
                return RCV3sV<IEnumerable<Doc.Document>>.Ok(value: new Doc.Document[] { }, Message: plxResFactory.CreateQueryResultEmpty());
            }
            else
            {
                return RCV3sV<IEnumerable<Doc.Document>>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(ret.ToPlx()));
            }
        }


        /// <summary>
        /// mko, 21.7.2020
        /// Liste aller CTS bestimmen, die ein User in Work gesetzt hat
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public RCV3sV<IEnumerable<Doc.Document>> GetInWorkCtsFor(string UserId)
        {
            var sql = new SQL<Doc.Document>();

            QueryBuilderResult<Doc.Document> query = null;

            // mko, 28.11.2019
            // Aktuellste Version der Dokumente wird aus Performance- Gründen aus der PathView ausgelesen.
            Tables.IPath PathTab = Tables.PathView._;

            var userid = UserId.ToLower();

            query = SelectFromPath(sql, PathTab)
                    .Where(
                        sql.And(
                            sql.Eq(PathTab.XType.FQN, sql.Txt(DocTypeSAP.CTS.ToString())),
                            sql.StrEq(PathTab.StatusChangeOriginator.FQN, sql.Txt(userid)),
                            sql.Or(
                                sql.Eq(PathTab.UserState.FQN, sql.Int((int)DfcDocStates.inWork))
                                )))
                            .done();

            var ret = GetRecords(query);

            if (ret.Succeeded && !ret.Value.IsEmpty)
            {
                return RCV3sV<IEnumerable<Doc.Document>>.Ok(value: ret.Value.Entities, Message: plxResFactory.CreateQueryResultOk(ret.Value.Entities.Count()));
            }
            else if (ret.Succeeded && ret.Value.IsEmpty)
            {
                return RCV3sV<IEnumerable<Doc.Document>>.Ok(value: new Doc.Document[] { }, Message: plxResFactory.CreateQueryResultEmpty());
            }
            else
            {
                return RCV3sV<IEnumerable<Doc.Document>>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(ret.ToPlx()));
            }
        }


        /// <summary>
        /// mko, 28.11.2018
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        WhereBuilder<Doc.Document> SelectFromPath(SQL<Doc.Document> sql, Tables.IPath PathTab)
        {
            return sql.Select(
                 sql.Map(PathTab.DocId.FQN, (bo, v)
                                                => bo.DocId = ColTool.GetSave(v, -1L)),
                 sql.Map(PathTab.ProjectNo.FQN, (bo, v)
                                                    => bo.ProjectNo = ColTool.GetSave(v, -1)),
                 sql.Map(PathTab.StationNo.FQN, (bo, v)
                                                    => bo.StationNo = ColTool.GetSave(v, (short)-1)),
                 sql.Map(PathTab.MatNr.FQN, (bo, v)
                                                => bo.MatNo = ColTool.GetSave(v, "")),
                 sql.Map(PathTab.StatusChangeOriginator.FQN, (bo, v)
                                                                => bo.StatusChangeOriginator = ColTool.GetSave(v, "")),

                 sql.Map(PathTab.SyncLUP.FQN, (bo, v)
                                                => bo.SyncLUP = ColTool.GetSave(v, DateTime.MinValue)),

                 // mko, 12.4.2019
                 sql.Map(PathTab.FilePDFLup.FQN, (bo, v)
                                                => bo.FilePDFLup = ColTool.GetSave(v, DateTime.MinValue)),


                 sql.Map(PathTab.File2Lup.FQN, (bo, v)
                                                => bo.File2Lup = ColTool.GetSave(v, DateTime.MinValue)),

                 sql.Map(PathTab.File3Lup.FQN, (bo, v)
                                                => bo.File3Lup = ColTool.GetSave(v, DateTime.MinValue)),


                 sql.Map(PathTab.UserState.FQN, (bo, v)
                                                    => bo.UserState = (DfcDocStates)ColTool.GetSave(v, 0)),
                 sql.Map(PathTab.XPath.FQN, (bo, v)
                                                => bo.XPath = ColTool.GetSave(v, "")),
                 sql.Map(PathTab.Infos.FQN, (bo, v)
                                                => bo.Infos = ColTool.GetSave(v, "")),

                 sql.Map(PathTab.CreatorId.FQN, (bo, v)
                                                    => bo.CreatorId = ColTool.GetSave(v, "")),

                 // mko, 28.7.2020
                 sql.Map(PathTab.CreationTime.FQN, (bo, v)
                                                    => bo.CreationTime = ColTool.GetSave(v, DateTime.MinValue)),

                 sql.Map(PathTab.XType.FQN, (bo, v)
                                                => bo.DocType = (DocTypeSAP)Enum.Parse(typeof(DocTypeSAP), ColTool.GetSave(v, "unknown") == "TER" ? "TEF" : ColTool.GetSave(v, "unknown"), true)),
                 sql.Map(PathTab.TDPTyp.FQN, (bo, v)
                                                => bo.TdpType = (TDPType)Enum.Parse(typeof(TDPType), string.IsNullOrWhiteSpace(ColTool.GetSave(v, "none")) ? "none" : ColTool.GetSave(v, "none"), true)),
                 sql.Map(PathTab.StorageType.FQN, (bo, v)
                                                    => bo.StorageType = (StorageType)ColTool.GetSave(v, 0)),


                 sql.Map(PathTab.File2Size.FQN, (bo, v)
                                                    => bo.File2Size = ColTool.GetSave(v, 0L)),
                 sql.Map(PathTab.FileName2.FQN, (bo, v)
                                                    => bo.FileName2 = ColTool.GetSave(v, "")),
                 sql.Map(PathTab.FileName3.FQN, (bo, v)
                                                    => bo.FileName3 = ColTool.GetSave(v, "")),
                 sql.Map(PathTab.FilePdfName.FQN, (bo, v)
                                                    => bo.FilePdfName = ColTool.GetSave(v, "")),
                 sql.Map(PathTab.FilePdfSize.FQN, (bo, v)
                                                    => bo.FilePdfSize = ColTool.GetSave(v, 0L)),

                 sql.Map(PathTab.FS1.FQN, (bo, v)
                                            => bo.FS1 = ColTool.GetSave(v, "")),
                 sql.Map(PathTab.FS2.FQN, (bo, v)
                                            => bo.FS2 = ColTool.GetSave(v, "")),
                 sql.Map(PathTab.FS3.FQN, (bo, v)
                                            => bo.FS3 = ColTool.GetSave(v, "")),
                 sql.Map(PathTab.FS4.FQN, (bo, v)
                                            => bo.FS4 = ColTool.GetSave(v, "")),
                 sql.Map(PathTab.FS5.FQN, (bo, v)
                                            => bo.FS5 = ColTool.GetSave(v, "")),

                 // mko, 2.10.2018
                 sql.Map(Tables.Projektliste2._.Bennenung.FQN, (bo, v)
                                            => bo.StationName = ColTool.GetSave(v, "")),

                 // mko, 11.10.2018
                 sql.Map(PathTab.IterationNo.FQN, (bo, v)
                                            => bo.VersionOrder = ColTool.GetSave(v, 0)),

                 // mko, 28.11.2018
                 // Wenn Abfrage über PathView läuft, dann liegt stets die LatestVersion vor
                 sql.MapIf(!(PathTab is Tables.PathView), PathTab.LatestVersion.FQN, (bo, v)
                                            => bo.IsLatestVersion = ColTool.GetSave(v, "") == "X", bo => bo.IsLatestVersion = true)
             )
             .LeftOuterJoin(PathTab, PathTab.MatNr.FQN, Tables.Projektliste2._, Tables.Projektliste2._.MatNr.FQN);
        }

        /// <summary>
        /// mko, 17.9.2018
        /// Gets attribute- value list, associated with a document via path table.
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public RCV3sV<string> GetDocInfos(long docId)
        {
            var sql = new SQL<Bo.StringObj>();

            Tables.IPath PathTab = Tables.Path._;

            var query = sql.Select(
                    sql.Map(PathTab.Infos, (bo, v) => bo.Value = ColTool.GetSave(v, ""))
                )
                .From(PathTab)
                .Where(sql.Eq(Tables.Path._.DocId, sql.Long(docId)))
                .done();

            var ret = GetRecord(query);

            if (ret.Succeeded && !ret.Value.IsEmpty)
            {
                return RCV3sV<string>.Ok(value: ret.Value.Entity.Value, Message: plxResFactory.CreateQueryResultOk(1L));
            }
            else if (ret.Succeeded && ret.Value.IsEmpty)
            {
                return RCV3sV<string>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryResultEmpty());
            }
            else
            {
                return RCV3sV<string>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(ret.ToPlx()));
            }
        }

        /// <summary>
        /// mko, 17.9.2018
        /// Sets a attribute-value list
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="Infos"></param>
        /// <returns></returns>
        public RCV3 SetDocInfos(long docId, string Infos)
        {
            var sql = new SQL<Bo.StringObj>();

            var query = sql.Update(
                    Tables.Path._,
                    sql.Set(Tables.Path._.Infos, sql.Txt(Infos))
                )
                .Where(sql.Eq(Tables.Path._.DocId, sql.Long(docId)))
                .done();

            var ret = ExecuteDML(query);

            return ret;
        }
    }
}
