using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using static ATMO.mko.Logging.PNDocuTerms.DocuEntities.ComposerSubTrees;
using ATMO.mko.QueryBuilder;
using ColTool = DFC3.DB.Tools.TabColAccess;

using DfcTree = ATMO.DFC.Tree;

using ATMO.DFC.Material;
using TT = ATMO.DFC.Naming.TechTerms;
using TTD = ATMO.DFC.Naming.DocuTerms;

using PN = ATMO.mko.Logging.PNDocuTerms;

namespace DFC3.DB.Queries.DfcTreeRepos
{
    /// <summary>
    /// mko, 1.12.2020
    /// Implementierung des Projekt- Repositories
    /// </summary>
    public class ProjectRepo
        : QueriesBaseAsync,
        DfcTree.IProjectRepository_2020_09<DfcTree.ProjectCore, DfcTree.ProjectSimple, DfcTree.ProjectWithBaselineAndExtensions, DfcTree.ProjectManagement, DfcTree.ProjectWithSecurityFeatures, DfcTree.ProjectWithStations>
    {
        public ProjectRepo(IComposer pnL)
            : base(pnL)
        { }


        public DfcTree.IProjectQueryBuilder<DfcTree.ProjectCore> CreateNewQueryBuilder()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Materialnummer eines Projektes bestimmen
        /// </summary>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public async Task<RCV3sV<string>> GetMatNoForProject(DfcTree.IProjectNo prjNo)
        {
            RCV3sV<string> ret = RCV3sV<string>.Failed(null, pnL.eNotCompleted());

            var psp = new ATMO.DFC.Tree.Parser.PSPNo(prjNo);

            return await GetMatNoForPSPNo(psp);
        }

        /// <summary>
        /// Materialnummer einer Position in einer Projktstückliste bestimmen
        /// </summary>
        /// <param name="pspBomPos"></param>
        /// <returns></returns>
        public async Task<RCV3sV<string>> GetMatNoForPSPBomPos(DfcTree.IPSPBomPos bomPos)
        {
            var ret = RCV3sV<string>.Ok(null, pnL.eNotCompleted());

            try
            {
                if (bomPos.LevelType != DfcTree.BomLevelType.BomPos)
                {
                    return await GetMatNoForPSPNo(bomPos);
                }
                else
                {
                    // mko, 11.12.2019
                    // Die PSP- Nummer ist in Wahrheit eine PSPBomPos

                    //var bomPos = (IPSPBomPos)pspNo;

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
                                sql.Eq(tabStpo.PosNr.FQN, sql.Int(bomPos.ProcessModul)),
                                sql.Eq(tabPL.PrjNr.FQN, sql.Int(bomPos.ProjectNo)),
                                sql.Eq(tabPL.StatNr.FQN, sql.Int(bomPos.StationNo))))
                        .done();

                    var res =  await GetRecordAsync(q);

                    if (!res.Succeeded)
                    {
                        ret = RCV3sV<string>.Failed(null, res.ToPlx());
                    }
                    else if (res.Value.IsEmpty)
                    {
                        ret = RCV3sV<string>.Failed(null, pnL.ReturnFetchNotFoundWithDetails(
                                                                UID_of_DataSource: TT.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                                UID_of_DataType: TT.Access.Datasources.WellKnown.ATMO.DFC.Projektliste2Tab.UID,
                                                                Details: pnL.txt($"Can't find Processmodule {bomPos.ProcessModul}"),
                                                                pnL.m(TT.Operators.Relations.Eq.UID,
                                                                    pnL.p(TT.ATMO.DFC.ProjectNo.UID, bomPos.ProjectNo)),
                                                                pnL.m(TT.Operators.Relations.Eq.UID,
                                                                    pnL.p(TT.ATMO.DFC.StationNo.UID, bomPos.StationNo)),
                                                                pnL.m(TT.Operators.Relations.Eq.UID,
                                                                    pnL.p(TT.ATMO.DFC.ProcessModul.UID, bomPos.ProcessModul))));
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
                        var PathToBomNode = new int[] { bomPos.BomType == ATMO.DFC.Tree.BOMTypes.mechanicalBOM ? 10 : 20 }.Concat(bomPos.PathToBomNode);

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

                            var res2 = await GetRecordAsync(q2);

                            if (!res2.Succeeded)
                            {

                                ret = RCV3sV<string>.Failed(null, pnL.ReturnFetchWithDetails(false,
                                                                UID_of_DataSource: TT.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                                UID_of_DataType: TT.Access.Datasources.WellKnown.ATMO.DFC.StpoTab.UID,
                                                                Details: pnL.i(TTD.MetaData.Details.UID,
                                                                            pnL.p(TTD.StateDescription.WhatsUp.UID, $"Can't find BomPos. Found the follwoing BOM Positions: {string.Join(".", foundPositions)}"),
                                                                            pnL.p(TTD.StateDescription.Why.UID, pnL.EncapsulateAsPropertyValue(res.ToPlx()))),
                                                                pnL.m(TT.Operators.Relations.Eq.UID,
                                                                    pnL.p(TT.ATMO.DFC.ProjectNo.UID, bomPos.ProjectNo)),
                                                                pnL.m(TT.Operators.Relations.Eq.UID,
                                                                    pnL.p(TT.ATMO.DFC.StationNo.UID, bomPos.StationNo)),
                                                                pnL.m(TT.Operators.Relations.Eq.UID,
                                                                    pnL.p(TT.ATMO.DFC.ProcessModul.UID, bomPos.ProcessModul)),
                                                                pnL.m(TT.Operators.Relations.Eq.UID,
                                                                    pnL.p(TT.ATMO.DFC.BOMPos.UID, pos))));

                            }
                            else if (res2.Value.IsEmpty)
                            {
                                ret = RCV3sV<string>.Failed(null, pnL.ReturnFetchNotFoundWithDetails(
                                                                UID_of_DataSource: TT.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                                UID_of_DataType: TT.Access.Datasources.WellKnown.ATMO.DFC.StpoTab.UID,
                                                                Details: pnL.txt($"Can't find BomPos. Found the follwoing BOM Positions: {string.Join(".", foundPositions)}"),
                                                                pnL.m(TT.Operators.Relations.Eq.UID,
                                                                    pnL.p(TT.ATMO.DFC.ProjectNo.UID, bomPos.ProjectNo)),
                                                                pnL.m(TT.Operators.Relations.Eq.UID,
                                                                    pnL.p(TT.ATMO.DFC.StationNo.UID, bomPos.StationNo)),
                                                                pnL.m(TT.Operators.Relations.Eq.UID,
                                                                    pnL.p(TT.ATMO.DFC.ProcessModul.UID, bomPos.ProcessModul)),
                                                                pnL.m(TT.Operators.Relations.Eq.UID,
                                                                    pnL.p(TT.ATMO.DFC.BOMPos.UID, pos))));
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
                }
            catch(Exception ex)
            {
                ret = RCV3sV<string>.Failed(null, TraceHlp.FlattenExceptionMessagesPN(ex));
            }

            return ret;
        }


        /// <summary>
        /// Materialnummer zu einer PSP- Nummer bestimmen.
        /// </summary>
        /// <param name="pspNo"></param>
        /// <returns></returns>
        public async Task<RCV3sV<string>> GetMatNoForPSPNo(DfcTree.IPSPBom pspNo)
        {

            var ret = RCV3sV<string>.Failed(null, pnL.ReturnNotCompleted("GetMatNoForPSPNo", pnL.p(TT.ATMO.DFC.PSPNo.UID, pspNo.ToString())));

            switch (pspNo.LevelType)
            {
                case DfcTree.BomLevelType.Project:
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

                        var res = await GetRecordAsync(q);

                        if (!res.Succeeded)
                        {
                            ret = RCV3sV<string>.Failed(null, res.ToPlx());
                        }
                        else if (res.Value.IsEmpty)
                        {
                            ret = RCV3sV<string>.Failed(null, pnL.ReturnFetchNotFound(
                                                                        UID_of_DataSource: TT.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                                        UID_of_DataType: TT.ATMO.DFC.MatNo.UID,
                                                                        CompositeKeyParts: pnL.m(TT.Operators.Relations.Eq.UID,
                                                                                                    pnL.p(TT.ATMO.DFC.ProjectNo.UID, pspNo.ProjectNo))));
                        }
                        else
                        {
                            ret = RCV3sV<string>.Ok(res.Value.Entity.Value);
                        }
                    }
                    break;
                case DfcTree.BomLevelType.ProjectStation:
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

                        var res = await GetRecordAsync(q);

                        if (!res.Succeeded)
                        {
                            ret = RCV3sV<string>.Failed(null, res.ToPlx());
                        }
                        else if (res.Value.IsEmpty)
                        {
                            ret = RCV3sV<string>.Failed(null, pnL.ReturnFetchNotFound(
                                                                        UID_of_DataSource: TT.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                                        UID_of_DataType: TT.ATMO.DFC.MatNo.UID,
                                                                        pnL.m(TT.Operators.Relations.Eq.UID,
                                                                            pnL.p(TT.ATMO.DFC.ProjectNo.UID, pspNo.ProjectNo)),
                                                                        pnL.m(TT.Operators.Relations.Eq.UID,
                                                                            pnL.p(TT.ATMO.DFC.StationNo.UID, pspNo.StationNo))));
                        }
                        else
                        {
                            ret = RCV3sV<string>.Ok(res.Value.Entity.Value);
                        }
                    }
                    break;
                case DfcTree.BomLevelType.FlexConLevel:
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

                        var res = await GetRecordAsync(q);

                        if (!res.Succeeded)
                        {
                            ret = RCV3sV<string>.Failed(null, res.ToPlx());
                        }
                        else if (res.Value.IsEmpty)
                        {
                            ret = RCV3sV<string>.Failed(null, pnL.ReturnFetchNotFound(
                                                                    UID_of_DataSource: TT.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                                                                    UID_of_DataType: TT.ATMO.DFC.MatNo.UID,
                                                                    pnL.m(TT.Operators.Relations.Eq.UID,
                                                                        pnL.p(TT.ATMO.DFC.ProjectNo.UID, pspNo.ProjectNo)),
                                                                    pnL.m(TT.Operators.Relations.Eq.UID,
                                                                        pnL.p(TT.ATMO.DFC.StationNo.UID, pspNo.StationNo)),
                                                                    pnL.m(TT.Operators.Relations.Eq.UID,
                                                                        pnL.p(TT.ATMO.DFC.ProcessModul.UID, pspNo.ProcessModul))));
                        }
                        else
                        {
                            ret = RCV3sV<string>.Ok(res.Value.Entity.Value);
                        }
                    }
                    break;               
                    
                default:
                    ;
                    break;
            }

            return ret;
        }

        public async Task<RCV3sV<DfcTree.ProjectCore>> GetProject(string prjMatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var ret = RCV3sV<DfcTree.ProjectCore>.Failed(value: null, qRes.CreateQueryExecutionFailed(pnL.eNotCompleted()));

            try
            {
                // Informationen zu Projekt und Station abrufen.
                // Beides ist in der Projektliste2 abgelegt. Zuerst werden die allgemeinen Projektinformationen, und dann 
                // die Stationsinformationen abgerufen.

                var sql = new SQL<DfcTree.ProjectCore>();
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
                                sql.Eq(tabPj2.MatNr.FQN, sql.Txt(prjMatNo))))

                        .done();

                var getProj = await GetRecordAsync(q);

                if (!getProj.Succeeded)
                {
                    ret = RCV3sV<DfcTree.ProjectCore>.Failed(value: null, qRes.CreateQueryExecutionFailed(getProj.ToPlx()));
                }
                else if (getProj.Succeeded && getProj.Value.IsEmpty)
                {
                    ret = RCV3sV<DfcTree.ProjectCore>.Failed(value: null, qRes.CreateQueryResultEmpty());
                }
                else
                {
                    ret = RCV3sV<DfcTree.ProjectCore>.Ok(value: getProj.Value.Entity);
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<DfcTree.ProjectCore>.Failed(value: null, qRes.CreateQueryExecutionFailed(TraceHlp.FlattenExceptionMessagesPN(ex)));
            }

            return ret;

        }


        /// <summary>
        /// mko, 5.8.2019
        /// Lädt die InStep- Daten aus der DFC- Datenbank
        /// 
        /// mko, 1.12.2020
        /// Verlagert von Queries.Projects in Queries.DfcTreeRepos.ProjectRepo
        /// </summary>
        /// <param name="prjMatNo"></param>
        /// <returns></returns>
        public async Task<RCV3sV<DfcTree.ProjectManagement>> GetProjectManagement(string prjMatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var ret = RCV3sV<DfcTree.ProjectManagement>.Failed(value: null, qRes.CreateQueryExecutionFailed(pnL.eNotCompleted()));

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
                                sql.Eq(tabPj2.MatNr.FQN, sql.Txt(prjMatNo))))

                        .done();

                var getProj = await GetRecordAsync(q);

                if (!getProj.Succeeded)
                {
                    ret = RCV3sV<DfcTree.ProjectManagement>.Failed(value: null, qRes.CreateQueryExecutionFailed(getProj.ToPlx()));
                }
                else if (getProj.Succeeded && getProj.Value.IsEmpty)
                {
                    ret = RCV3sV<DfcTree.ProjectManagement>.Failed(value: null, qRes.CreateQueryResultEmpty());
                }
                else
                {
                    var pCore = getProj.Value.Entity;

                    // Standortfreischaltungen für das Projekt berechnen

                    var mgmt = new DfcTree.ProjectManagement();

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

                    ret = ErrorsAndWarnings.Any() ? RCV3sV<DfcTree.ProjectManagement>.Ok(mgmt, pnL.eWarn(pnL.List(ErrorsAndWarnings.ToArray()))) : RCV3sV<DfcTree.ProjectManagement>.Ok(mgmt);
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<DfcTree.ProjectManagement>.Failed(value: null, qRes.CreateQueryExecutionFailed(TraceHlp.FlattenExceptionMessagesPN(ex)));
            }

            return ret;
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

        private void SaveErrorsAndWarningsFor(string ResponsiblePerson, RCV3 ret, List<IInstance> ErrorsAndWarnings)
        {
            const string InvalidInstepNameList = "Instep namelist is invalid";

            ErrorsAndWarnings.Add(pnL.i(ResponsiblePerson,
                                    pnL.p(TTD.StateDescription.WhatsUp.UID, InvalidInstepNameList),
                                    pnL.p(TTD.StateDescription.Why.UID, pnL.EncapsulateAsPropertyValue(ret.MessageEntity))));
        }



        public Task<RCV3sV<DfcTree.ProjectWithStations>> GetProjectWithProjectStations(string prjMatNo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// mko, 1.12.2020
        /// 
        /// </summary>
        /// <param name="prjMatNo"></param>
        /// <returns></returns>
        public async Task<RCV3sV<DfcTree.ProjectWithSecurityFeatures>> GetProjectWithSecurityFeatures(string prjMatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var ret = RCV3sV<DfcTree.ProjectWithSecurityFeatures>.Failed(value: null, qRes.CreateQueryExecutionFailed(pnL.eNotCompleted()));

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
                                sql.Eq(tabPj2.MatNr.FQN, sql.Txt(prjMatNo))))

                        .done();

                var getProj = await GetRecordAsync(q);

                if (!getProj.Succeeded)
                {
                    ret = RCV3sV<DfcTree.ProjectWithSecurityFeatures>.Failed(value: null, qRes.CreateQueryExecutionFailed(getProj.ToPlx()));
                }
                else if (getProj.Succeeded && getProj.Value.IsEmpty)
                {
                    ret = RCV3sV<DfcTree.ProjectWithSecurityFeatures>.Failed(value: null, qRes.CreateQueryResultEmpty());
                }
                else
                {
                    var pCore = getProj.Value.Entity;

                    // Standortfreischaltungen für das Projekt berechnen

                    var secF = new DfcTree.ProjectWithSecurityFeatures();

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

                    ret = RCV3sV<DfcTree.ProjectWithSecurityFeatures>.Ok(secF);
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<DfcTree.ProjectWithSecurityFeatures>.Failed(value: null, qRes.CreateQueryExecutionFailed(TraceHlp.FlattenExceptionMessagesPN(ex)));
            }

            return ret;
        }
    }
}
