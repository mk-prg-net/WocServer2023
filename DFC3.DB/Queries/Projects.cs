using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
//using TechTerms = ATMO.mko.Logging.PNDocuTerms.DocuEntities.Composer.TechTerms;
using PN = ATMO.mko.Logging.PNDocuTerms;

using ATMO.mko.QueryBuilder;


using ColTool = DFC3.DB.Tools.TabColAccess;
using DFCObjects.Common.Prj;

using static DFCSecurity.SitesExt;

using static DZAUtilities_Dictionaries.GlobalDictionaries;

using ATMO.DFC.Tree;

using ANC = ATMO.DFC.Naming;


namespace DFC3.DB.Queries
{
    /// <summary>
    /// mko, 20.11.2018
    /// </summary>
    public class Projects : QueriesBase
    {
        Queries.CustGroupsQueries custGroupQueries;

        public Projects(IComposer pnL) : base(pnL)
        {
            custGroupQueries = new Queries.CustGroupsQueries(pnL);
        }

        /// <summary>
        /// Aus Optimierungsgründen (CustGroupQueries liest aktuell einmalig im Konstruktor alle Kundengruppen ein), kann ein bereits
        /// angelegtes custGroupQueries Objekt wiederverwendet werden.
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="custGroupQueries"></param>
        public Projects(IComposer pnL, Queries.CustGroupsQueries custGroupQueries) : base(pnL)
        {
            this.custGroupQueries = custGroupQueries;
        }

        /// <summary>
        /// Gets all projects, associated with a customer group.
        /// </summary>
        /// <param name="custGroupId"></param>
        /// <returns></returns>
        public RCV3sV<IEnumerable<ProjectStationMainData>> GetCustomerProjects(string custGroupId)
        {
            var ret = RCV3sV<IEnumerable<ProjectStationMainData>>.Failed(value: null, ErrorDescription: pnL.eNotCompleted());

            var sql = new SQL<DFCObjects.Common.Prj.ProjectStationMainData>();
            var tab = new DFC3.DB.Tables.Projektliste2();

            var custGroupQueries = new Queries.CustGroupsQueries(pnL);

            var q = sql.Select(
                    sql.Map(tab.PrjNr, (bo, v) => bo.ProjectNo = ColTool.GetSave(v, -1)),
                    sql.Map(tab.StatNr, (bo, v) => bo.StationNo = ColTool.GetSave(v, (short)-1)),
                    sql.Map(tab.Stufe, (bo, v) => bo.Level = ColTool.GetSave(v, (short)0)),
                    sql.Map(tab.ErsDat, (bo, v) => bo.launched = ColTool.GetSave(v, new DateTime(1970, 1, 1))),
                    sql.Map(tab.MatNr, (bo, v) => bo.MatNo = ColTool.GetSave(v, "")),
                    sql.Map(tab.Owner, (bo, v) => bo.OwnedBy = ColTool.GetSave(v, "").ParseOrDefault()),
                    sql.Map(tab.Bennenung, (bo, v) => bo.Title = ColTool.GetSave(v, "")),
                    sql.Map(tab.CustAccess, (bo, v) =>
                    {

                        var custAccess = ColTool.GetSave(v, "");
                        var custGrps = custAccess.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                        bo.CustomerGroups = custGrps.Select(r => new DFCObjects.Common.CustomerGroup(r, custGroupQueries.GetDescriptionIntern(r)));
                    })
                )
                .From(tab)
                .Where(sql.LikeLowerCase(tab.CustAccess, sql.Txt($"%{custGroupId}%")))
                .By(tab.PrjNr).By(tab.StatNr)
                .done();

            var res = GetRecords(q);

            if (!res.Succeeded)
            {
                ret = RCV3sV<IEnumerable<ProjectStationMainData>>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(res.ToPlx()));
            }
            else if (res.Value.IsEmpty)
            {
                ret = RCV3sV<IEnumerable<ProjectStationMainData>>.Ok(value: new ProjectStationMainData[] { }, Message: plxResFactory.CreateQueryResultEmpty());
            }
            else
            {
                ret = RCV3sV<IEnumerable<ProjectStationMainData>>.Ok(value: res.Value.Entities, Message: plxResFactory.CreateQueryResultOk(res.Value.Entities.Count()));
            }

            return ret;
        }

        private RCV3WithValue<RCV3, Result<Bo.Projektliste2Bo>> Query(long ProjectNo, SQL<Bo.Projektliste2Bo> sqlPrjList2, Tables.Projektliste2 tabPj2, FromBuilder<Bo.Projektliste2Bo> select, long _StationNo)
        {
            var q = select
                .From(tabPj2)
                .Where(
                    sqlPrjList2.And(
                        sqlPrjList2.Eq(tabPj2.PrjNr, sqlPrjList2.Long(ProjectNo)),
                        sqlPrjList2.Eq(tabPj2.StatNr, sqlPrjList2.Long(_StationNo))))
                .done();

            var res = this.GetRecord(q);
            return res;
        }


        /// <summary>
        /// mko, 17.1.2019
        /// 
        /// mko, 2.8.2019
        /// Reimplementiert in Form der IProjectRepository.GetProject- Form.
        /// </summary>
        /// <param name="MatNo"></param>        
        /// <returns></returns>
        public RCV3sV<ATMO.DFC.Tree.ProjectCore> GetProject(string MatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var ret = RCV3sV<ProjectCore>.Failed(value: null, qRes.CreateQueryExecutionFailed(pnL.eNotCompleted()));

            try
            {
                // Informationen zu Projekt und Station abrufen.
                // Beides ist in der Projektliste2 abgelegt. Zuerst werden die allgemeinen Projektinformationen, und dann 
                // die Stationsinformationen abgerufen.

                var sql = new SQL<ProjectCore>();
                var tabPj2 = new Tables.Projektliste2();
                var tabStpko = new Tables.STKO();


                var q = sql.Select(
                        sql.Map(tabPj2.PrjNr.FQN, (bo, v) => bo.ProjectNo = ColTool.GetSave(v, 0)),
                        sql.Map(tabPj2.Bennenung.FQN, (bo, v) => bo.ProjectName = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.ErsDat.FQN, (bo, v) => bo.ProjectCreated = bo.ProjectLaunched = ColTool.GetSave(v, DateTime.MinValue)),
                        sql.Map(tabStpko.Lup.FQN, (bo, v) => bo.ProjectLup = ColTool.GetSave(v, DateTime.MinValue)),
                        sql.Map(tabPj2.MatNr.FQN, (bo, v) => bo.ProjectMatNo = ColTool.GetSave(v, "")))
                        .From(tabPj2, tabStpko)
                        .Where(sql.And(
                                sql.Eq(tabPj2.MatNr.FQN, tabStpko.MatNo.FQN),
                                sql.Eq(tabPj2.MatNr.FQN, sql.Txt(MatNo))))

                        .done();

                var getProj = GetRecord(q);

                if (!getProj.Succeeded)
                {
                    ret = RCV3sV<ProjectCore>.Failed(value: null, qRes.CreateQueryExecutionFailed(getProj.ToPlx()));
                }
                else if (getProj.Succeeded && getProj.Value.IsEmpty)
                {
                    ret = RCV3sV<ProjectCore>.Failed(value: null, qRes.CreateQueryResultEmpty());
                }
                else
                {
                    ret = RCV3sV<ProjectCore>.Ok(value: getProj.Value.Entity);
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<ProjectCore>.Failed(value: null, qRes.CreateQueryExecutionFailed(TraceHlp.FlattenExceptionMessagesPN(ex)));
            }

            return ret;
        }

        /// <summary>
        /// mko, 2.8.2019
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public RCV3sV<ProjectWithSecurityFeatures> GetProjectWithSecurityFeatures(string MatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var ret = RCV3sV<ProjectWithSecurityFeatures>.Failed(value: null, qRes.CreateQueryExecutionFailed(pnL.eNotCompleted()));

            try
            {
                var sql = new SQL<Bo.Projektliste2Bo>();
                var tabPj2 = new Tables.Projektliste2();
                var tabStpko = new Tables.STKO();

                var q = sql.Select(
                        sql.Map(tabPj2.PrjNr.FQN, (bo, v) => bo.ProjectNo = ColTool.GetSave(v, 0)),
                        sql.Map(tabPj2.Bennenung.FQN, (bo, v) => bo.Bennennung = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.ErsDat.FQN, (bo, v) => bo.ErsDat = ColTool.GetSave(v, DateTime.MinValue)),
                        sql.Map(tabStpko.Lup.FQN, (bo, v) => bo.LUP = ColTool.GetSave(v, DateTime.MinValue)),
                        sql.Map(tabPj2.MatNr.FQN, (bo, v) => bo.MatNr = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.CustAccess.FQN, (bo, v) => bo.CustAccess = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.PA1.FQN, (bo, v) => bo.PA1 = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.PA2.FQN, (bo, v) => bo.PA2 = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.PA3.FQN, (bo, v) => bo.PA3 = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.PA4.FQN, (bo, v) => bo.PA4 = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.PA5.FQN, (bo, v) => bo.PA5 = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.PA6.FQN, (bo, v) => bo.PA6 = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.PA7.FQN, (bo, v) => bo.PA7 = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.PA8.FQN, (bo, v) => bo.PA8 = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.PA9.FQN, (bo, v) => bo.PA9 = ColTool.GetSave(v, "")))
                        .From(tabPj2, tabStpko)
                        .Where(sql.And(
                                sql.Eq(tabPj2.MatNr.FQN, tabStpko.MatNo.FQN),
                                sql.Eq(tabPj2.MatNr.FQN, sql.Txt(MatNo))))

                        .done();

                var getProj = GetRecord(q);

                if (!getProj.Succeeded)
                {
                    ret = RCV3sV<ProjectWithSecurityFeatures>.Failed(value: null, qRes.CreateQueryExecutionFailed(getProj.ToPlx()));
                }
                else if (getProj.Succeeded && getProj.Value.IsEmpty)
                {
                    ret = RCV3sV<ProjectWithSecurityFeatures>.Failed(value: null, qRes.CreateQueryResultEmpty());
                }
                else
                {
                    var pCore = getProj.Value.Entity;

                    // Standortfreischaltungen für das Projekt berechnen



                    var secF = new ProjectWithSecurityFeatures();

                    secF.ProjectCreated = pCore.ErsDat;
                    secF.ProjectLaunched = pCore.ErsDat;
                    secF.ProjectLup = pCore.LUP;
                    secF.ProjectMatNo = pCore.MatNr;
                    secF.ProjectName = pCore.Bennennung;
                    secF.ProjectNo = pCore.ProjectNo;                    

                    secF.AccessAllowedInLocations = tabPj2.GetAccessAllowedInLocationsFrom(
                        pCore.PA1,
                        pCore.PA2,
                        pCore.PA3,
                        pCore.PA4,
                        pCore.PA5,
                        pCore.PA6,
                        pCore.PA7,
                        pCore.PA8,
                        pCore.PA9);

                    // Kundengruppen isolieren

                    if (!string.IsNullOrWhiteSpace(pCore.CustAccess))
                    {
                        var custGrps = pCore.CustAccess.Trim().ToLower().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                        var AllCustGroups = new CustGroupsQueries(pnL);
                        secF.CustGroups = custGrps.Select(r => new DFCObjects.Common.CustomerGroup(r, AllCustGroups.GetDescription(r).ValueOrException)).ToArray();
                    }

                    ret = RCV3sV<ProjectWithSecurityFeatures>.Ok(secF);
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<ProjectWithSecurityFeatures>.Failed(value: null, qRes.CreateQueryExecutionFailed(TraceHlp.FlattenExceptionMessagesPN(ex)));
            }

            return ret;
        }

        /// <summary>
        /// mko, 5.8.2019
        /// 
        /// Lädt die InStep- Daten aus der DFC- Datenbank
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public RCV3sV<ProjectManagement> GetProjectManagement(string MatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var ret = RCV3sV<ProjectManagement>.Failed(value: null, qRes.CreateQueryExecutionFailed(pnL.eNotCompleted()));

            try
            {
                var sql = new SQL<Bo.Projektliste2Bo>();
                var tabPj2 = new Tables.Projektliste2();
                var tabStpko = new Tables.STKO();

                var q = sql.Select(
                        sql.Map(tabPj2.PrjNr.FQN, (bo, v) => bo.ProjectNo = ColTool.GetSave(v, 0)),
                        sql.Map(tabPj2.Bennenung.FQN, (bo, v) => bo.Bennennung = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.ErsDat.FQN, (bo, v) => bo.ErsDat = ColTool.GetSave(v, DateTime.MinValue)),
                        sql.Map(tabStpko.Lup.FQN, (bo, v) => bo.LUP = ColTool.GetSave(v, DateTime.MinValue)),
                        sql.Map(tabPj2.MatNr.FQN, (bo, v) => bo.MatNr = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.PrjFolder.FQN, (bo, v) => bo.PjFolder = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.Owner.FQN, (bo, v) => bo.Owner = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.IS_PM.FQN, (bo, v) => bo.IS_PM = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.IS_VAB.FQN, (bo, v) => bo.IS_VAB = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.IS_VDP.FQN, (bo, v) => bo.IS_VDP = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.IS_VMK.FQN, (bo, v) => bo.IS_VMK = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.IS_VSM.FQN, (bo, v) => bo.IS_VSM = ColTool.GetSave(v, "")),
                        sql.Map(tabPj2.IS_Projektleiter.FQN, (bo, v) => bo.IS_Projektleiter = ColTool.GetSave(v, "")))
                        .From(tabPj2, tabStpko)
                        .Where(sql.And(
                                sql.Eq(tabPj2.MatNr.FQN, tabStpko.MatNo.FQN),
                                sql.Eq(tabPj2.MatNr.FQN, sql.Txt(MatNo))))

                        .done();

                var getProj = GetRecord(q);

                if (!getProj.Succeeded)
                {
                    ret = RCV3sV<ProjectManagement>.Failed(value: null, qRes.CreateQueryExecutionFailed(getProj.ToPlx()));
                }
                else if (getProj.Succeeded && getProj.Value.IsEmpty)
                {
                    ret = RCV3sV<ProjectManagement>.Failed(value: null, qRes.CreateQueryResultEmpty());
                }
                else
                {
                    var pCore = getProj.Value.Entity;

                    // Standortfreischaltungen für das Projekt berechnen

                    var mgmt = new ProjectManagement();

                    mgmt.ProjectCreated = pCore.ErsDat;
                    mgmt.ProjectLaunched = pCore.ErsDat;
                    mgmt.ProjectLup = pCore.LUP;
                    mgmt.ProjectMatNo = pCore.MatNr;
                    mgmt.ProjectName = pCore.Bennennung;
                    mgmt.ProjectNo = pCore.ProjectNo;


                    var userMgmt = new UserMgmtV18_10(pnL);

                    var ErrorsAndWarnings = new List<IInstance>();

                    // Liste der Besitzer abrufen
                    var retRP = SetResponsiblesPersons(pCore.IS_PM, persons => mgmt.ProductOwners = persons, userMgmt, pnL);

                    if (!retRP.Succeeded)
                    {
                        SaveErrorsAndWarningsFor("ProjectOwner", retRP, ErrorsAndWarnings);
                    }

                    // Liste der Projektleiter abrufen
                    retRP = SetResponsiblesPersons(pCore.IS_Projektleiter, persons => mgmt.ProjectManagers = persons, userMgmt, pnL);

                    if (!retRP.Succeeded)
                    {
                        SaveErrorsAndWarningsFor("ProjectManager", retRP, ErrorsAndWarnings);
                    }

                    // List Liste der verantwortlichen für den Analgenbau
                    retRP = SetResponsiblesPersons(pCore.IS_VAB, persons => mgmt.VABs = persons, userMgmt, pnL);

                    if (!retRP.Succeeded)
                    {
                        SaveErrorsAndWarningsFor("VAB", retRP, ErrorsAndWarnings);
                    }

                    // Liste der verantwortlichen Disponenten
                    retRP = SetResponsiblesPersons(pCore.IS_VDP, persons => mgmt.VDPs = persons, userMgmt, pnL);

                    if (!retRP.Succeeded)
                    {
                        SaveErrorsAndWarningsFor("VDP", retRP, ErrorsAndWarnings);
                    }

                    // Liste der verantwortlichen Konstrukteure
                    retRP = SetResponsiblesPersons(pCore.IS_VMK, persons => mgmt.VMKs = persons, userMgmt, pnL);

                    if (!retRP.Succeeded)
                    {
                        SaveErrorsAndWarningsFor("VMK", retRP, ErrorsAndWarnings);
                    }

                    // Liste der verantwortlichen Softwareentwickler
                    retRP = SetResponsiblesPersons(pCore.IS_VSM, persons => mgmt.VSMs = persons, userMgmt, pnL);

                    if (!retRP.Succeeded)
                    {
                        SaveErrorsAndWarningsFor("VSM", retRP, ErrorsAndWarnings);
                    }

                    ret = ErrorsAndWarnings.Any() ? RCV3sV<ProjectManagement>.Ok(mgmt, pnL.eWarn(pnL.List(ErrorsAndWarnings.ToArray()))) : RCV3sV<ProjectManagement>.Ok(mgmt);
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<ProjectManagement>.Failed(value: null, qRes.CreateQueryExecutionFailed(TraceHlp.FlattenExceptionMessagesPN(ex)));
            }

            return ret;
        }

        private void SaveErrorsAndWarningsFor(string ResponsiblePerson, RCV3 ret, List<IInstance> ErrorsAndWarnings)
        {
            const string InvalidInstepNameList = "Instep namelist is invalid";

            ErrorsAndWarnings.Add(pnL.i(ResponsiblePerson,
                                    pnL.p(ANC.DocuTerms.StateDescription.WhatsUp.UID, InvalidInstepNameList),
                                    pnL.p(ANC.DocuTerms.StateDescription.Why.UID, pnL.EncapsulateAsPropertyValue(ret.MessageEntity))));
        }

        /// <summary>
        /// mko, 5.8.2019
        /// Liest Benutzernamen ein, die in einem Feld nach der Konvention {Partikel_LastName}+ FirstName [(Department)];... abgelegt sind
        /// </summary>
        /// <param name="boschFirstLastNameStr"></param>
        /// <param name="SetPersons"></param>
        /// <param name="userMgmt"></param>
        private static RCV3 SetResponsiblesPersons(string boschFirstLastNameStr, Action<DFCObjects.Common.MemberOfDepartment[]> SetPersons, UserMgmtV18_10 userMgmt, IComposer pnL)
        {
            var ret = RCV3.Ok();

            var ErrorsAndWarnings = new List<IMethod>();

            if (!string.IsNullOrWhiteSpace(boschFirstLastNameStr))
            {
                var strPersons = boschFirstLastNameStr.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries);
                //var strPersons = boschFirstLastNameStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                var Persons = new List<DFCObjects.Common.MemberOfDepartment>();
                foreach (var strPerson in strPersons)
                {
                    var getPerson = userMgmt.ParseFromBoschFirstLastnameString(strPerson);

                    if (getPerson.Succeeded)
                    {
                        foreach (var person in getPerson.Value)
                        {
                            Persons.Add(person);
                        }
                    }
                    else
                    {
                        // detaillierte Fehlermeldungen über das Scheitern des Parsens werden aufgehoben
                        ErrorsAndWarnings.Add(
                                pnL.m("ParseFirstLastName",
                                        pnL.p("boschFirstLastNameStr", strPerson),
                                        pnL.ret(
                                            pnL.eFails(pnL.EncapsulateAsEventParameter(getPerson.ToPlx()))))
                            );
                    }
                }

                SetPersons(Persons.ToArray());

                ret = ErrorsAndWarnings.Any() ? RCV3.Failed(pnL.List(ErrorsAndWarnings.ToArray())) : RCV3.Ok();

            }
            else
            {
                SetPersons(new DFCObjects.Common.MemberOfDepartment[] { });
            }

            return ret;

        }


        /// <summary>
        /// mko, 23.1.2019
        /// Lädt die ATD oder ATZ zu einer Materialnummer 
        /// 
        /// mko, 22.5.2019
        /// Überarbeitung: Die sieben möglichen Fälle für Kombinationen aus gültigen/ ungültigen Mat-/ZeichNr und MissingDrawing j/n
        ///                werden jetzt exakter berücksichtigt.
        ///                
        /// mko, 12.11.2019
        /// Wenn ATD und ATZ gleichzeitig existieren, dann die ATD zurückgeben und nicht wie früher die ATZ.
        /// 
        /// </summary>
        /// <param name="MatNo"></param>
        /// <param name="ddc"></param>
        /// <returns></returns>
        public RCV3sV<IEnumerable<DFCObjects.Common.Doc.Document>> GetAtdAtz(string MatNo, bool LatestVersionOnly = false)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var ret = RCV3sV<IEnumerable<DFCObjects.Common.Doc.Document>>.Failed(value: null, qRes.CreateQueryExecutionFailed(pnL.eNotCompleted()));

            try
            {
                var DocumentQueriers = new ATMODocsSQL(pnL);

                // ATD/ATZ laden

                // Zeichnungsnummer zu einer Materialnummer bestimmen. Diese Zuordnung ist in der Mara- Tabelle definiert
                var qMara = new Mara(pnL);
                var getMara = qMara.GetMaraBo(MatNo);

                if (!getMara.Succeeded)
                {
                    ret = RCV3sV<IEnumerable<DFCObjects.Common.Doc.Document>>.Failed(
                            value: null,
                            pnL.ReturnFetchWithDetails(
                                false,
                                UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                UID_of_DataType: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.MaraTab.UID,
                                Details: pnL.EncapsulateAsEventParameter(getMara.ToPlx()),
                                pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                    pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, MatNo))));
                }
                else
                {
                    const string InvalidDrawingNo = "0000000000";

                    var maraDs = getMara.Value;
                    var drawings = new List<DFCObjects.Common.Doc.Document>();

                    if (!string.IsNullOrWhiteSpace(maraDs.ZeichungsNr) && (maraDs.MatNr == maraDs.ZeichungsNr))
                    {
                        // Eine ATD liegt vor
                        var ZeichNr = maraDs.MatNr;

                        // Zeichnung in PathView nachschlagen
                        var getATD = DocumentQueriers.GetPathBo(ZeichNr, DocTypeSAP.ATD, LatestVersionOnly);

                        if (!getATD.Succeeded)
                        {
                            // ... die Abfrage aus internen Gründen gescheitert ist
                            ret = RCV3sV<IEnumerable<DFCObjects.Common.Doc.Document>>.Failed(
                                value: null,
                                ErrorDescription: pnL.ReturnFetchWithDetails(
                                                    false,
                                                    UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                    UID_of_DataType: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.PathTab.UID,
                                                    Details: pnL.EncapsulateAsEventParameter(getATD.ToPlx()),
                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                        pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, MatNo)),
                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                        pnL.p(ANC.TechTerms.ATMO.DFC.SAPDocType.UID, DocTypeSAP.ATD.ToString())),
                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                        pnL.p(ANC.TechTerms.ATMO.DFC.DrawingNo.UID, maraDs.ZeichungsNr))));
                        }
                        else
                        {
                            if (!getATD.Value.Any())
                            {
                                // Mara Zeichnungsnummer verweist auf eine ATD, die es nicht gibt.
                                ret = RCV3sV<IEnumerable<DFCObjects.Common.Doc.Document>>.Ok(
                                    value: new DFCObjects.Common.Doc.Document[] { },
                                    Message: pnL.ReturnFetchWarnEmptySetWithDetails(
                                                    UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                    UID_of_DataType: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.PathTab.UID,
                                                    Details: pnL.NID(ANC.TechTerms.ATMO.DocuCheck.MissingDrawing.UID),
                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                        pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, MatNo)),
                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                        pnL.p(ANC.TechTerms.ATMO.DFC.SAPDocType.UID, DocTypeSAP.ATD.ToString())),
                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                        pnL.p(ANC.TechTerms.ATMO.DFC.DrawingNo.UID, maraDs.ZeichungsNr))));
                            }
                            else
                            {
                                // Zeichnung inkl. Vorversionen wurde gefunden
                                ret = RCV3sV<IEnumerable<DFCObjects.Common.Doc.Document>>.Ok(
                                    value: getATD.Value,
                                    Message: pnL.ReturnFetch(
                                                    true,
                                                    UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                    UID_of_DataType: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.PathTab.UID,
                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                        pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, MatNo)),
                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                        pnL.p(ANC.TechTerms.ATMO.DFC.SAPDocType.UID, DocTypeSAP.ATD.ToString())),
                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                        pnL.p(ANC.TechTerms.ATMO.DFC.DrawingNo.UID, maraDs.ZeichungsNr))));                                    
                            }
                        }
                    }
                    else if (string.IsNullOrEmpty(maraDs.ZeichungsNr))
                    {
                        // Fall: Keine Zeichungsnummer definiert/leer. Entweder gibt es keine Zeichnung, was konsistent wäre, oder eine Zeichnung liegt vor,
                        //       jedoch ist keine Zeichungsnummer definiert.

                        var ZeichNr = maraDs.MatNr;

                        // mögliche Zeichnung in Path nachschlagen
                        var getATD = DocumentQueriers.GetPathBo(ZeichNr, DocTypeSAP.ATD, LatestVersionOnly);

                        if (!getATD.Succeeded)
                        {
                            // Kein Zeichnungsdokument gefunden, weil...
                            // ... die Abfrage aus irgend einem anderen Grund gescheitert ist                                        
                            ret = RCV3sV<IEnumerable<DFCObjects.Common.Doc.Document>>.Failed(value: null,
                                ErrorDescription: pnL.ReturnFetchWithDetails(
                                                    false,
                                                    UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                    UID_of_DataType: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.PathTab.UID,
                                                    Details: pnL.EncapsulateAsEventParameter(getATD.ToPlx()),
                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                        pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, MatNo)),
                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                        pnL.p(ANC.TechTerms.ATMO.DFC.SAPDocType.UID, DocTypeSAP.ATD.ToString())),
                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                        pnL.p_NID(ANC.TechTerms.ATMO.DFC.DrawingNo.UID, ANC.TechTerms.Sets.NullValue.UID))));

                        }
                        else
                        {
                            if (!getATD.Value.Any())
                            {
                                // mko, 20.5.2019
                                // Fall: Keine Zeichnung vorhanden ! Leere Liste mit Zeichnungen mit Warnmeldung "Keine Zeichnung" wird zurückgegeben
                                ret = RCV3sV<IEnumerable<DFCObjects.Common.Doc.Document>>.Ok(
                                    value: new DFCObjects.Common.Doc.Document[] { },
                                    Message: pnL.ReturnFetchWarnEmptySetWithDetails(
                                                UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                UID_of_DataType: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.PathTab.UID,
                                                Details: pnL.NID(ANC.TechTerms.ATMO.DocuCheck.NoDrawing.UID),
                                                pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                    pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, MatNo)),
                                                pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                    pnL.p(ANC.TechTerms.ATMO.DFC.SAPDocType.UID, DocTypeSAP.ATD.ToString())),
                                                pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                    pnL.p_NID(ANC.TechTerms.ATMO.DFC.DrawingNo.UID, ANC.TechTerms.Sets.NullValue.UID))));

                            }
                            else
                            {
                                // Zeichnung und Warmeldung, dass die Zeichnungsnummer leer ist, zurückgeben
                                ret = RCV3sV<IEnumerable<DFCObjects.Common.Doc.Document>>.Ok(
                                    value: getATD.Value,
                                    Message: pnL.ReturnFetchWarnEmptySetWithDetails(
                                                UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                UID_of_DataType: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.PathTab.UID,
                                                Details: pnL.NID(ANC.TechTerms.ATMO.DocuCheck.ATDExistsButNoDrawingNo.UID),
                                                pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                    pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, MatNo)),
                                                pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                    pnL.p(ANC.TechTerms.ATMO.DFC.SAPDocType.UID, DocTypeSAP.ATD.ToString())),
                                                pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                    pnL.p_NID(ANC.TechTerms.ATMO.DFC.DrawingNo.UID, ANC.TechTerms.Sets.NullValue.UID))));
                            }
                        }
                    }
                    else if (maraDs.ZeichungsNr == InvalidDrawingNo)
                    {

                        // Inkonsitenz: Beim Eintragen der Zeichnungsnummer in SAP gab es Zahlendreher etc.. 
                        //              Der DFC- Import hat dies erkannt und den Fehler mit der ungültigen Zeichnungsnummer 0000000000 signalisiert.

                        var ZeichNr = maraDs.MatNr;

                        // Zeichnung in PathView nachschlagen
                        var getATD = DocumentQueriers.GetPathBo(ZeichNr, DocTypeSAP.ATD, LatestVersionOnly);

                        if (!getATD.Succeeded)
                        {
                            // Kein Zeichnungsdokument gefunden, weil...
                            // ... die Abfrage aus irgend einem anderen Grund gescheitert ist                                        
                            ret = RCV3sV<IEnumerable<DFCObjects.Common.Doc.Document>>.Failed(value: null,
                                    ErrorDescription: pnL.ReturnFetchWithDetails(
                                                            false,
                                                            UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                            UID_of_DataType: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.PathTab.UID,
                                                            Details: pnL.List(
                                                                        pnL.p_NID(ANC.DocuTerms.StateDescription.CurrentState.UID, ANC.TechTerms.ATMO.DocuCheck.InvalidDrawingNo.UID),
                                                                        pnL.EncapsulateAsEventParameter(getATD.ToPlx()),
                                                            pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, MatNo)),
                                                            pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                pnL.p(ANC.TechTerms.ATMO.DFC.SAPDocType.UID, DocTypeSAP.ATD.ToString())),
                                                            pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                pnL.p(ANC.TechTerms.ATMO.DFC.DrawingNo.UID, InvalidDrawingNo)))));
                        }
                        else
                        {
                            if (!getATD.Value.Any())
                            {
                                // mko, 20.5.2019
                                // Missing Drawing: ATZ in Mara definiert, jedoch in Path nicht gefunden
                                ret = RCV3sV<IEnumerable<DFCObjects.Common.Doc.Document>>.Ok(
                                    value: new DFCObjects.Common.Doc.Document[] { },
                                    Message: pnL.ReturnFetchWarnEmptySetWithDetails(
                                                UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                UID_of_DataType: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.PathTab.UID,
                                                Details: pnL.NID(ANC.TechTerms.ATMO.DocuCheck.InvalidDrawingNo.UID),
                                                pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                    pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, MatNo)),
                                                pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                    pnL.p(ANC.TechTerms.ATMO.DFC.SAPDocType.UID, DocTypeSAP.ATD.ToString())),
                                                pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                    pnL.p(ANC.TechTerms.ATMO.DFC.DrawingNo.UID, InvalidDrawingNo))));
                            }
                            else
                            {
                                // Zeichnung und Warmeldung, dass die Zeichnungsnummer leer ist, zurückgeben
                                ret = RCV3sV<IEnumerable<DFCObjects.Common.Doc.Document>>.Ok(
                                    value: getATD.Value,
                                    Message: pnL.ReturnFetchWarnEmptySetWithDetails(
                                                UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                UID_of_DataType: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.PathTab.UID,
                                                Details: pnL.NID(ANC.TechTerms.ATMO.DocuCheck.InvalidDrawingNo.UID),
                                                pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                    pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, MatNo)),
                                                pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                    pnL.p(ANC.TechTerms.ATMO.DFC.SAPDocType.UID, DocTypeSAP.ATD.ToString())),
                                                pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                    pnL.p(ANC.TechTerms.ATMO.DFC.DrawingNo.UID, InvalidDrawingNo))));
                            }

                        }
                    }
                    else
                    {
                        // Eine ATZ liegt vor

                        var ZeichNr = maraDs.ZeichungsNr;

                        // Zeichnung in PathView nachschlagen
                        var getATZ = DocumentQueriers.GetPathBo(ZeichNr, DocTypeSAP.ATD, LatestVersionOnly);

                        if (!getATZ.Succeeded)
                        {
                            // mko, 20.5.2019
                            // Achtung: Wenn keine Zeichnung zur Zeichnnungsnummer gefunden wurde, dann liefert GetPathBo
                            //          trotzdem Succeeded = true zurück. Jedoch ist die Liste der Dokumente leer.

                            // Kein Zeichnungsdokument gefunden, weil...
                            // ... die Abfrage aus irgend einem anderen Grund gescheitert ist                                        
                            ret = RCV3sV<IEnumerable<DFCObjects.Common.Doc.Document>>.Failed(value: null,
                                ErrorDescription: pnL.ReturnFetchWithDetails(
                                                    false,
                                                    UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                    UID_of_DataType: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.PathTab.UID,
                                                    Details: pnL.EncapsulateAsEventParameter(getATZ.ToPlx()),
                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                        pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, MatNo)),
                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                        pnL.p(ANC.TechTerms.ATMO.DFC.SAPDocType.UID, DocTypeSAP.ATZ.ToString())),
                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                        pnL.p(ANC.TechTerms.ATMO.DFC.DrawingNo.UID, maraDs.ZeichungsNr))));
                        }
                        else
                        {
                            if (!getATZ.Value.Any())
                            {
                                // mko, 20.5.2019
                                // Missing Drawing: ATZ in Mara definiert, jedoch in Path nicht gefunden
                                ret = RCV3sV<IEnumerable<DFCObjects.Common.Doc.Document>>.Ok(
                                    value: new DFCObjects.Common.Doc.Document[] { },
                                    Message: pnL.ReturnFetchWarnEmptySetWithDetails(
                                        UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                        UID_of_DataType: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.PathTab.UID,
                                        Details: pnL.NID(ANC.TechTerms.ATMO.DocuCheck.MissingDrawing.UID),
                                        pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                            pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, MatNo)),
                                        pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                            pnL.p(ANC.TechTerms.ATMO.DFC.SAPDocType.UID, DocTypeSAP.ATZ.ToString())),
                                        pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                            pnL.p(ANC.TechTerms.ATMO.DFC.DrawingNo.UID, ZeichNr))));
                            }
                            else
                            {
                                // Verweiszeichnung inkl. Vorversionen wurde gefunden.
                                // Dateityp auf ATZ ändern
                                foreach (var drw in getATZ.Value)
                                {
                                    drw.DocType = DocTypeSAP.ATZ;
                                    drw.MatNo = MatNo;
                                    drw.Infos = $"{drw.Infos}|MatNoOfATZDrawing:={ZeichNr}";
                                }

                                // Prüfen, ob nicht fälschlicherweise eine ATD neben der ATZ abgelegt ist
                                var getATD = DocumentQueriers.GetPathBo(maraDs.MatNr, DocTypeSAP.ATD, true);

                                IDocuEntity msg = null;

                                // mko, 12.11.2019
                                // Wenn ATD und ATZ gleichzeitig existieren, dann die ATD zurückgeben und nicht wie früher die ATZ.
                                if (getATD.Succeeded && getATD.Value.Any())
                                {
                                    // Fälschlicherweise gibt es die ATZ zusätzlich zu der ATD direkt unter der Materialnummer.
                                    // Die ATD wird bevorzugt.
                                    msg = pnL.ReturnFetchWarnEmptySetWithDetails(
                                            UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                            UID_of_DataType: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.PathTab.UID,
                                            Details: pnL.NID(ANC.TechTerms.ATMO.DocuCheck.RedundantAtdBesideAtz.UID),
                                            pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, MatNo)),
                                            pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                pnL.p(ANC.TechTerms.ATMO.DFC.SAPDocType.UID, DocTypeSAP.ATZ.ToString())),
                                            pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                pnL.p(ANC.TechTerms.ATMO.DFC.DrawingNo.UID, ZeichNr)));


                                    ret = RCV3sV<IEnumerable<DFCObjects.Common.Doc.Document>>.Ok(getATD.Value, msg);

                                }
                                else
                                {
                                    msg = pnL.ReturnFetch(
                                                true,
                                                UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                UID_of_DataType: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.PathTab.UID,
                                                pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                    pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, MatNo)),
                                                pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                    pnL.p(ANC.TechTerms.ATMO.DFC.SAPDocType.UID, DocTypeSAP.ATZ.ToString())),
                                                pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                    pnL.p(ANC.TechTerms.ATMO.DFC.DrawingNo.UID, ZeichNr)));

                                    ret = RCV3sV<IEnumerable<DFCObjects.Common.Doc.Document>>.Ok(getATZ.Value, msg);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<IEnumerable<DFCObjects.Common.Doc.Document>>.Failed(value: null, qRes.CreateQueryExecutionFailed(TraceHlp.FlattenExceptionMessagesPN(ex)));
            }

            return ret;
        }


        /// <summary>
        /// mko, 3.6.2019
        /// Listet alle Projekte auf, in denen ein Teil mit der gegebenen Materialnummer verbaut ist.
        /// Wird über MaraPJ ermittelt. Materialnummer -> Projektnummer Zuordnung ist dort verlässlich lt. Joachim
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public RCV3sV<IEnumerable<ATMO.DFC.Tree.ProjectCore>> InWichProjectsIsMatNoUsed(string MatNo)
        {

            var ret = RCV3sV<IEnumerable<ATMO.DFC.Tree.ProjectCore>>.Failed(value: null, pnL.eNotCompleted());

            var tab = new Tables.MaraPj();
            var sql = new SQL<ATMO.DFC.Tree.ProjectCore>();

            var q = sql.Select(
                        sql.Map(tab.PjNr, (bo, v) => bo.ProjectNo = ColTool.GetSave(v, -1)),
                        sql.Map(tab.MatNr, (bo, v) => bo.ProjectMatNo = ColTool.GetSave(v, "")))
                        .From(tab)
                        .Where(sql.Eq(tab.MatNr, sql.Txt(MatNo)))
                        .done();


            var getPrjs = GetRecords(q);

            if (!getPrjs.Succeeded)
            {
                ret = RCV3sV<IEnumerable<ATMO.DFC.Tree.ProjectCore>>.Failed(value: null, getPrjs.ToPlx());

            }
            else if (getPrjs.Succeeded & getPrjs.Value.IsEmpty)
            {
                ret = RCV3sV<IEnumerable<ProjectCore>>.Ok(
                    value: new ProjectCore[] { }, 
                    pnL.ReturnSearchWarnEmptyResult(pnL.Predicate(ANC.TechTerms.Operators.Relations.Eq.UID, pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, MatNo))));
            }
            else
            {
                ret = RCV3sV<IEnumerable<ATMO.DFC.Tree.ProjectCore>>.Ok(value: getPrjs.Value.Entities);
            }

            return ret;
        }

        /// <summary>
        /// mko, 22.7.2019
        /// Klassifiziert den Inhalt hinter der Materialnummer. Bestimmt in Abhängigkeit von der Materialklasse, wo das 
        /// Material überall verbaut sein kann.
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns>
        ///     Item1: Materialklasse der MatNo selbst
        ///     Item2: Liste aller Projekte und Stationen, in denen das Material eingesetzt wird
        ///     Item3: Liste aller Baugruppen, in denen das Material eingesetzt wird
        /// </returns>
        public RCV3sV<(ATMO.DFC.Material.MatClass MaterialClassOfMatNo, ATMO.DFC.Tree.Parser.PSPNo[] ProjectStationsWhereMatIsUsed, ATMO.DFC.Material.MatNoContainer[] AssemblyGroupsWhereUsed)> WhereMaterialIsUsed(string MatNo)
        {
            return RCV3sV<(ATMO.DFC.Material.MatClass MaterialClassOfMatNo, ATMO.DFC.Tree.Parser.PSPNo[] ProjectStationsWhereMatIsUsed, ATMO.DFC.Material.MatNoContainer[] AssemblyGroupsWhereUsed)>.Failed((ATMO.DFC.Material.MatClass.none, null, null), null);
        }

        /// <summary>
        /// mko, 22.7.2019
        /// Liefert den Pfad von einem Elternknoten- im DFC- TreeView zum Kindknoten. Falls so ein Pfad nicht existiert, wird in succeeded false zurückgegeben.
        /// Die Methode durchläuft rekursiv die STPO
        /// </summary>
        /// <param name="MatNoOfParent"></param>
        /// <param name="MatNoOfChild"></param>
        /// <returns></returns>
        public RCV3sV<ATMO.DFC.Material.MatNoContainer[]> PathFromParentToChild(string MatNoOfParent, string MatNoOfChild)
        {
            return RCV3sV<ATMO.DFC.Material.MatNoContainer[]>.Failed(null, null);
        }

        /// <summary>
        /// mko, 28.6.2019
        /// 
        /// Liefert in Succeeded true zurück, wenn die Materialnummer für ein Projekt oder eine Station steht.
        /// False wird in Succeeded zurückgeliefert, wenn die Abfrage aus technischen Gründen scheitert, oder
        /// kein Eintrag gefunden wurde. In diesem Fall wird in MessageEntity der Hinweis Empty zurückgegeben
        /// 
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public RCV3sV<ATMO.DFC.Tree.Parser.PSPNo> IsMatNoOfProjectOrStation(string MatNo)
        {
            var ret = RCV3sV<ATMO.DFC.Tree.Parser.PSPNo>.Failed(null, pnL.eNotCompleted());

            var sql = new ATMO.mko.QueryBuilder.SQL<Bo.Projektliste2Bo>();
            var tab = new Tables.Projektliste2();


            var q = sql.Select(
                    sql.Map(tab.PrjNr, (bo, v) => bo.ProjectNo = ColTool.GetSave(v, 0)),
                    sql.Map(tab.StatNr, (bo, v) => bo.StationNo = ColTool.GetSave(v, (short)0))
                )
                .From(tab)
                .Where(sql.Eq(tab.MatNr, sql.Txt(MatNo.ToUpper())))
                .done();

            var res = GetRecord(q);

            if (!res.Succeeded)
            {
                ret = RCV3sV<ATMO.DFC.Tree.Parser.PSPNo>.Failed(null, res.ToPlx());
            }
            else if (res.Value.IsEmpty)
            {
                ret = RCV3sV<ATMO.DFC.Tree.Parser.PSPNo>.Failed(
                    value: null, 
                    pnL.ReturnSearchFailsEmptyResult(pnL.Predicate(ANC.TechTerms.Operators.Relations.Eq.UID, pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, MatNo))));
            }
            else
            {
                ret = RCV3sV<ATMO.DFC.Tree.Parser.PSPNo>.Ok(new ATMO.DFC.Tree.Parser.PSPNo(res.Value.Entity.ProjectNo, res.Value.Entity.StationNo == 0 ? -1 : res.Value.Entity.StationNo));
            }

            return ret;
        }


        /// <summary>
        /// mko, 27.7.2019
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public RCV3sV<ATMO.DFC.Tree.Parser.PSPNo> IsMatNoOfProcessmodule(string MatNo)
        {
            var ret = RCV3sV<ATMO.DFC.Tree.Parser.PSPNo>.Failed(null, pnL.eNotCompleted());

            var sql = new ATMO.mko.QueryBuilder.SQL<Bo.Projektliste2Bo>();
            var tab = new Tables.Projektliste2();


            var q = sql.Select(
                    sql.Map(tab.PrjNr, (bo, v) => bo.ProjectNo = ColTool.GetSave(v, 0)),
                    sql.Map(tab.StatNr, (bo, v) => bo.StationNo = ColTool.GetSave(v, (short)0))
                )
                .From(tab)
                .Where(sql.Eq(tab.MatNr, sql.Txt(MatNo.ToUpper())))
                .done();

            var res = GetRecord(q);

            if (!res.Succeeded)
            {
                ret = RCV3sV<ATMO.DFC.Tree.Parser.PSPNo>.Failed(null, res.ToPlx());
            }
            else if (res.Value.IsEmpty)
            {
                ret = RCV3sV<ATMO.DFC.Tree.Parser.PSPNo>.Failed(
                    value: null, 
                    pnL.ReturnSearchFailsEmptyResult(pnL.Predicate(ANC.TechTerms.Operators.Relations.Eq.UID, pnL.p(ANC.TechTerms.ATMO.DFC.MatNo.UID, MatNo))));
            }
            else
            {
                ret = RCV3sV<ATMO.DFC.Tree.Parser.PSPNo>.Ok(new ATMO.DFC.Tree.Parser.PSPNo(res.Value.Entity.ProjectNo, res.Value.Entity.StationNo == 0 ? -1 : res.Value.Entity.StationNo));
            }

            return ret;
        }

        /// <summary>
        /// mko, 1.8.2019
        /// Ermittel für eine PSP- Nummer die zugeordnete Materialnummer.
        /// Funktioniert für Projekt-, Stations- und Prozessmodul- PSP- nummern
        /// 
        /// mko, 11.10.2019
        /// Auf Suche nach Stücklistenposition erweitert.
        /// </summary>
        /// <param name="pspNo"></param>
        /// <returns></returns>
        public RCV3sV<string> GetMatNoForPSPNo(ATMO.DFC.Tree.Parser.PSPNo pspNo)
        {
            var ret = RCV3sV<string>.Failed(null, pnL.ReturnNotCompleted("GetMatNoForPSPNo", pnL.p("PSPNo", pspNo.ToString())));

            switch (pspNo.LevelType)
            {
                case ATMO.DFC.Tree.BomLevelType.Project:
                    {
                        var tab = new Tables.Projektliste2();
                        var sql = new SQL<Bo.StringObj>();

                        var q = sql.Select(
                                sql.Map(tab.MatNr, (bo, v) => bo.Value = ColTool.GetSave(v, ""))
                            )
                            .From(tab)
                            .Where(
                                sql.And(
                                    sql.Eq(tab.PrjNr, sql.Int(pspNo.ProjectNo)),
                                    sql.Eq(tab.StatNr, sql.Int(0))
                                    ))
                            .done();

                        var res = GetRecord(q);

                        if (!res.Succeeded)
                        {
                            ret = RCV3sV<string>.Failed(null, res.ToPlx());
                        }
                        else if (res.Value.IsEmpty)
                        {
                            ret = RCV3sV<string>.Failed(null, pnL.ReturnFetchNotFound(
                                                                        UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                                        UID_of_DataType: ANC.TechTerms.ATMO.DFC.MatNo.UID,
                                                                        CompositeKeyParts: pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                                                    pnL.p(ANC.TechTerms.ATMO.DFC.ProjectNo.UID, pspNo.ProjectNo))));
                        }
                        else
                        {
                            ret = RCV3sV<string>.Ok(res.Value.Entity.Value);
                        }
                    }
                    break;
                case ATMO.DFC.Tree.BomLevelType.ProjectStation:
                    {
                        var tab = new Tables.Projektliste2();
                        var sql = new SQL<Bo.StringObj>();

                        var q = sql.Select(
                                sql.Map(tab.MatNr, (bo, v) => bo.Value = ColTool.GetSave(v, ""))
                            )
                            .From(tab)
                            .Where(
                                sql.And(
                                    sql.Eq(tab.PrjNr, sql.Int(pspNo.ProjectNo)),
                                    sql.Eq(tab.StatNr, sql.Int(pspNo.StationNo))))
                            .done();

                        var res = GetRecord(q);

                        if (!res.Succeeded)
                        {
                            ret = RCV3sV<string>.Failed(null, res.ToPlx());
                        }
                        else if (res.Value.IsEmpty)
                        {
                            ret = RCV3sV<string>.Failed(null, pnL.ReturnFetchNotFound(
                                                                        UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                                        UID_of_DataType: ANC.TechTerms.ATMO.DFC.MatNo.UID,
                                                                        pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                            pnL.p(ANC.TechTerms.ATMO.DFC.ProjectNo.UID, pspNo.ProjectNo)),
                                                                        pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                            pnL.p(ANC.TechTerms.ATMO.DFC.StationNo.UID, pspNo.StationNo))));


                                
                        }
                        else
                        {
                            ret = RCV3sV<string>.Ok(res.Value.Entity.Value);
                        }
                    }
                    break;
                case ATMO.DFC.Tree.BomLevelType.FlexConLevel:
                    {
                        var tabPL = new Tables.Projektliste2();
                        var tabStpo = new Tables.STPO();
                        var sql = new SQL<Bo.StringObj>();


                        // Das Prozessmodul ist die erste Ebene unterhalb einer Station. 
                        // Projekte und Stationen werden flach in der Projektliste2 abgebildet.
                        // Die Stücklistenpositionen, deren BGMatNr auf einen Stationsdatensatz in 
                        // der Projektliste 2 verweist, sind die Prozessmodule der Station.
                        var q = sql.Select(
                                sql.Map(tabStpo.MatNr.FQN, (bo, v) => bo.Value = ColTool.GetSave(v, ""))
                            )
                            .From(tabPL, tabStpo)
                            .Where(
                                sql.And(
                                    sql.Eq(tabStpo.BGMatNr.FQN, tabPL.MatNr.FQN),
                                    sql.Eq(tabStpo.PosNr.FQN, sql.Int(pspNo.ProcessModul)),
                                    sql.Eq(tabPL.PrjNr.FQN, sql.Int(pspNo.ProjectNo)),
                                    sql.Eq(tabPL.StatNr.FQN, sql.Int(pspNo.StationNo))))
                            .done();

                        var res = GetRecord(q);

                        if (!res.Succeeded)
                        {
                            ret = RCV3sV<string>.Failed(null, res.ToPlx());
                        }
                        else if (res.Value.IsEmpty)
                        {
                            ret = RCV3sV<string>.Failed(null, pnL.ReturnFetchNotFound(
                                                                    UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                                    UID_of_DataType: ANC.TechTerms.ATMO.DFC.MatNo.UID,
                                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                        pnL.p(ANC.TechTerms.ATMO.DFC.ProjectNo.UID, pspNo.ProjectNo)),
                                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                        pnL.p(ANC.TechTerms.ATMO.DFC.StationNo.UID, pspNo.StationNo)),
                                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                        pnL.p(ANC.TechTerms.ATMO.DFC.ProcessModul.UID, pspNo.ProcessModul))));
                        }
                        else
                        {
                            ret = RCV3sV<string>.Ok(res.Value.Entity.Value);
                        }
                    }
                    break;
                case BomLevelType.BomPos:
                    {
                        // mko, 11.12.2019
                        // Die PSP- Nummer ist in Wahrheit eine PSPBomPos

                        var bomPos = (IPSPBomPos)pspNo;

                        var tabPL = new Tables.Projektliste2();
                        var tabStpo = new Tables.STPO();
                        var sql = new SQL<Bo.StringObj>();


                        // Das Prozessmodul ist die erste Ebene unterhalb einer Station. 
                        // Projekte und Stationen werden flach in der Projektliste2 abgebildet.
                        // Die Stücklistenpositionen, deren BGMatNr auf einen Stationsdatensatz in 
                        // der Projektliste 2 verweist, sind die Prozessmodule der Station.
                        var q = sql.Select(
                                sql.Map(tabStpo.MatNr.FQN, (bo, v) => bo.Value = ColTool.GetSave(v, ""))
                            )
                            .From(tabPL, tabStpo)
                            .Where(
                                sql.And(
                                    sql.Eq(tabStpo.BGMatNr.FQN, tabPL.MatNr.FQN),
                                    sql.Eq(tabStpo.PosNr.FQN, sql.Int(pspNo.ProcessModul)),
                                    sql.Eq(tabPL.PrjNr.FQN, sql.Int(pspNo.ProjectNo)),
                                    sql.Eq(tabPL.StatNr.FQN, sql.Int(pspNo.StationNo))))
                            .done();

                        var res = GetRecord(q);

                        if (!res.Succeeded)
                        {
                            ret = RCV3sV<string>.Failed(null, res.ToPlx());
                        }
                        else if (res.Value.IsEmpty)
                        {
                            ret = RCV3sV<string>.Failed(null, pnL.ReturnFetchNotFoundWithDetails(
                                                                    UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                                    UID_of_DataType: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.Projektliste2Tab.UID,
                                                                    Details: pnL.txt($"Can't find Processmodule {pspNo.ProcessModul}"),
                                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                        pnL.p(ANC.TechTerms.ATMO.DFC.ProjectNo.UID, pspNo.ProjectNo)),
                                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                        pnL.p(ANC.TechTerms.ATMO.DFC.StationNo.UID, pspNo.StationNo)),
                                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                        pnL.p(ANC.TechTerms.ATMO.DFC.ProcessModul.UID, pspNo.ProcessModul))));
                        }
                        else
                        {

                            var BgMatNo = res.Value.Entity.Value;
                            // Absteigen bis zur gesuchten Stücklistenposition

                            // Protokoll der gefundenen Stücklistenpositionen
                            var foundPositions = new List<int>();

                            // Dem Pfad zur Stücklistenposition die Position der Stückliste voranstellen
                            // In DFC werden die ID's für mech. oder elektrische Stückliste nicht durch die Codes 1 oder 5, sondern durch Stücklistenpositionen 10 und 20
                            // präsentiert.
                            var PathToBomNode = new int[] { bomPos.BomType == BOMTypes.mechanicalBOM ? 10 : 20 }.Concat(bomPos.PathToBomNode);

                            foreach (var pos in PathToBomNode)
                            {
                                var q2 = sql.Select(
                                        sql.Map(tabStpo.MatNr.FQN, (bo, v) => bo.Value = ColTool.GetSave(v, ""))
                                    )
                                    .From(tabStpo)
                                    .Where(
                                        sql.And(
                                            sql.StrEq(tabStpo.BGMatNr, sql.Txt(BgMatNo)),
                                            sql.Eq(tabStpo.PosNr, sql.Int(pos)))
                                    )
                                    .done();

                                var res2 = GetRecord(q2);

                                if (!res2.Succeeded)
                                {
                                    
                                    ret = RCV3sV<string>.Failed(null, pnL.ReturnFetchWithDetails(false,
                                                                    UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                                    UID_of_DataType: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.StpoTab.UID,
                                                                    Details: pnL.i(ANC.DocuTerms.MetaData.Details.UID,
                                                                                pnL.p(ANC.DocuTerms.StateDescription.WhatsUp.UID, $"Can't find BomPos. Found the follwoing BOM Positions: {string.Join(".", foundPositions)}"),
                                                                                pnL.p(ANC.DocuTerms.StateDescription.Why.UID, pnL.EncapsulateAsPropertyValue(res.ToPlx()))),
                                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                        pnL.p(ANC.TechTerms.ATMO.DFC.ProjectNo.UID, pspNo.ProjectNo)),
                                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                        pnL.p(ANC.TechTerms.ATMO.DFC.StationNo.UID, pspNo.StationNo)),
                                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                        pnL.p(ANC.TechTerms.ATMO.DFC.ProcessModul.UID, pspNo.ProcessModul)),
                                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                        pnL.p(ANC.TechTerms.ATMO.DFC.BOMPos.UID, pos))));

                                }
                                else if (res2.Value.IsEmpty)
                                {
                                    ret = RCV3sV<string>.Failed(null, pnL.ReturnFetchNotFoundWithDetails(
                                                                    UID_of_DataSource: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                                    UID_of_DataType: ANC.TechTerms.Access.Datasources.WellKnown.ATMO.DFC.StpoTab.UID,
                                                                    Details: pnL.txt($"Can't find BomPos. Found the follwoing BOM Positions: {string.Join(".", foundPositions)}"),
                                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                        pnL.p(ANC.TechTerms.ATMO.DFC.ProjectNo.UID, pspNo.ProjectNo)),
                                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                        pnL.p(ANC.TechTerms.ATMO.DFC.StationNo.UID, pspNo.StationNo)),
                                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                        pnL.p(ANC.TechTerms.ATMO.DFC.ProcessModul.UID, pspNo.ProcessModul)),
                                                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID,
                                                                        pnL.p(ANC.TechTerms.ATMO.DFC.BOMPos.UID, pos))));
                                }
                                else
                                {
                                    foundPositions.Add(pos);
                                    BgMatNo = res2.Value.Entity.Value;
                                }

                            }


                            ret = RCV3sV<string>.Ok(BgMatNo);
                        }
                    }
                    break;
                default:
                    ;
                    break;
            }

            return ret;
        }



    }
}
