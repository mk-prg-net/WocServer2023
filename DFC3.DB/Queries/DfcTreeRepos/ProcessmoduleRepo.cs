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
    public class ProcessmoduleRepo
                : QueriesBaseAsync,
        DfcTree.IProcessmoduleRepository<
            DfcTree.ProcessModuleCore,
            DfcTree.ProcessModuleDecoratedWithSecurityFeatures,
            DfcTree.ProcessModuleDecoratedWithMechanicalArea,
            DfcTree.ProcessModuleDecoratedWithElectricalArea>

    {
        public ProcessmoduleRepo(IComposer pnL)
            : base(pnL) { }

        /// <summary>
        /// 1.12.2020
        /// </summary>
        /// <param name="pmMatNo"></param>
        /// <returns></returns>
        public async Task<RCV3sV<DfcTree.ProcessModuleCore>> GetProcessmodule(string pmMatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);
            var ret = RCV3sV<DfcTree.ProcessModuleCore>.Failed(value: null, pnL.eNotCompleted());

            DfcTree.ProcessModuleCore pm = null;
            //pm.ProcessmoduleMatNo = pmMatNo;

            var tabMaraPj = new Tables.MaraPj();
            var tabStpoView = new Tables.STPOView602();

            var docuFilterTerms = pnL.m(TT.Operators.Relations.Eq.UID, pnL.i(TT.ATMO.DFC.ProcessModul.UID, pnL.p(TT.ATMO.DFC.MatNo.UID, pmMatNo)));

            try
            {
                // Prozessmodul einlesen

                var sql = new SQL<DfcTree.ProcessModuleCore>();
                //int bomPosProcessMod = 0;

                var q = sql.Select(
                        sql.Map(tabMaraPj.PjNr.FQN, (bo, v) =>
                            bo.ProjectNo = ColTool.GetSave(v, 0)),
                        sql.Map(tabMaraPj.StatNr.FQN, (bo, v) =>
                            bo.StationNo = ColTool.GetSave(v, (short)0)),
                        sql.Map(tabStpoView.PosNr.FQN, (bo, v) =>
                        {
                            //bo.ProcessModul = bomPosProcessMod++;
                            bo.CurrentDfcBomPos = ColTool.GetSave(v, (short)0);
                        }),
                        sql.Map(tabMaraPj.MatNr.FQN, (bo, v) =>
                            bo.ProcessmoduleMatNo = ColTool.GetSave(v, "")),
                        sql.Map(tabMaraPj.StlStatus.FQN, (bo, v) =>
                        {
                            var converter = new DfcTree.StringToBomStateConverter();
                            var getBomState = converter.ToBomState(ColTool.GetSave(v, ""), pnL);
                            bo.SAPBomState = getBomState.ValueOrException;

                            // Die beiden aus *SAPBomStatus* ausdifferenzierten Zusände *BomState*
                            // und *ProcurmentState* können im Falle eines Prozessmoduls
                            // vollständig aus dem SAPBomStatus hergeleitet werden, da das Prozessmodul 
                            // nur im Kontext eines einzigen Projektes gültig ist (Nicht mehrfachverbaut)
                            if (bo.SAPBomState == DfcTree.SAPBomStatus._01_Initialized
                            || bo.SAPBomState == DfcTree.SAPBomStatus._02_InProcessByExternal
                            || bo.SAPBomState == DfcTree.SAPBomStatus._03_CreatedByExternal
                            || bo.SAPBomState == DfcTree.SAPBomStatus._04_InProcessByInternal
                            || bo.SAPBomState == DfcTree.SAPBomStatus._05_CreatedByInternal)
                            {
                                bo.BomStatus = (DfcTree.BomStatus)bo.SAPBomState;
                                bo.ProcurementStatus = DfcTree.ProcurementStatus.NoCurrentOrders;
                            }
                            else if (bo.SAPBomState == DfcTree.SAPBomStatus._06_StartOrdering)
                            {
                                bo.BomStatus = DfcTree.BomStatus._05_CreatedByInternal;
                                bo.ProcurementStatus = DfcTree.ProcurementStatus._06_StartOrdering;
                            }
                            else if (bo.SAPBomState == DfcTree.SAPBomStatus._07_InOrdering)
                            {
                                bo.BomStatus = DfcTree.BomStatus._03_05_Created;
                                bo.ProcurementStatus = DfcTree.ProcurementStatus._07_InOrdering;
                            }
                            else if (bo.SAPBomState == DfcTree.SAPBomStatus._08_OrderingCompleted)
                            {
                                bo.BomStatus = DfcTree.BomStatus._03_05_Created;
                                bo.ProcurementStatus = DfcTree.ProcurementStatus._08_OrderingCompleted;
                            }
                            else
                            {
                                bo.BomStatus = DfcTree.BomStatus._03_05_Created;
                                bo.ProcurementStatus = (DfcTree.ProcurementStatus)bo.SAPBomState;
                            }
                        }),
                        sql.Map(tabStpoView.MaterialKurzText.FQN, (bo, v) =>
                            bo.ProcessmoduleName = ColTool.GetSave(v, "")),

                        // mko, 14.12.2020
                        sql.Map(tabStpoView.Menge.FQN, (bo, v) =>
                            bo.CurrentBomPosQuantity = ColTool.GetSave(v, 0))                        
                    )
                    .EqJoinFrom((tabMaraPj.MatNr, tabStpoView.MatNr))
                    .Where(sql.Eq(tabMaraPj.MatNr.FQN, sql.Txt(pmMatNo)))
                    .done();

                var getPm = await GetRecordAsync(q);

                if (!getPm.Succeeded)
                {
                    ret = RCV3sV<DfcTree.ProcessModuleCore>.Failed(
                            null,
                            qRes.CreateQueryExecutionFailed(
                                tabMaraPj, tabStpoView,
                                // Ursache
                                pnL.EncapsulateAsPropertyValue(getPm.ToPlx()),
                                // Filtereinstellungen
                                docuFilterTerms));
                }
                else if (getPm.ValueOrException.IsEmpty)
                {
                    ret = RCV3sV<DfcTree.ProcessModuleCore>.Failed(
                            null,
                            qRes.CreateQueryResultEmpty(
                                tabMaraPj, tabStpoView,
                                docuFilterTerms));
                }
                else
                {
                    pm = getPm.ValueOrException.Entity;

                    // Materialnummer des Projektes und der Station bestimmen

                    var prj = new Projects(pnL);

                    pm.ProjectMatNo = prj.GetMatNoForPSPNo(new DfcTree.Parser.PSPNo((DfcTree.IProjectNo)pm)).ValueOrException;
                    pm.StationMatNo = prj.GetMatNoForPSPNo(new DfcTree.Parser.PSPNo(pm)).ValueOrException;

                    // mko, 14.12.2020
                    // Reihenfolge der Prozessmodule bestimmen, um Richtige Nummerierung zu ermitteln

                    var q2 = sql.Select(
                            sql.Map(tabStpoView.MatNr.FQN, (bo, v) => 
                                bo.ProcessmoduleMatNo = ColTool.GetSave(v, "")),
                            sql.Map(tabStpoView.PosNr.FQN, (bo, v) => 
                                bo.ProcessModul = ColTool.GetSave(v, (short)0))
                        )
                        .From(tabStpoView)
                        .Where(
                            sql.And(
                                    sql.Eq(tabStpoView.BGMatNr.FQN, pm.StationMatNo),
                                    tabStpoView.MatClassEq(sql, MatClass.FlexCon)
                                )
                        )
                        .By(tabStpoView.PosNr.FQN)
                        .done();

                    var getPosList = await GetRecordsAsync(q2);

                    if (!getPosList.Succeeded || getPosList.Value.IsEmpty)
                    {
                        ret = RCV3sV<DfcTree.ProcessModuleCore>.Failed(
                                null,
                                qRes.CreateQueryFailsDueInconsistencies(tabStpoView,
                                    pnL.m("DeterminePmOrderOf",
                                        pnL.i(TT.ATMO.DFC.Station.UID,
                                            pnL.p(TT.ATMO.DFC.StationNo.UID, pm.StationNo),
                                            pnL.p(TT.ATMO.DFC.MatNo.UID, pm.StationMatNo)),
                                        pnL.eFails(pnL.EncapsulateAsEventParameter(getPosList.ToPlx()))),
                                    pnL.i(TT.ATMO.DFC.Station.UID,
                                            pnL.p(TT.ATMO.DFC.StationNo.UID, pm.StationNo),
                                            pnL.p(TT.ATMO.DFC.MatNo.UID, pm.StationMatNo))));
                            
                    }
                    else
                    {
                        // Exakte Position des Prozessmoduls in der Stückliste bestimmen (Bereitstellen einer zu Bom@ATMO konformen 
                        // Nummerierung)

                        var pmList = getPosList.Value.Entities;
                        var pmNo = 0;


                        foreach(var pmPos in pmList)
                        {
                            if(pmPos.ProcessmoduleMatNo == pm.ProcessmoduleMatNo)
                            {
                                break;
                            }
                            else
                            {
                                pmNo++;
                            }
                        }

                        pm.ProcessModul = pmNo;

                        ret = RCV3sV<DfcTree.ProcessModuleCore>.Ok(pm);
                    }

                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<DfcTree.ProcessModuleCore>.Failed(
                            null,
                            qRes.CreateQueryExecutionFailed(
                                tabMaraPj, tabStpoView,
                                // Ursache
                                pnL.EncapsulateAsPropertyValue(TraceHlp.FlattenExceptionMessagesPN(ex)),
                                // Filtereinstellungen
                                docuFilterTerms));

            }

            return ret;
        }

        /// <summary>
        /// mko, 1.12.2020
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public async Task<RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>> GetProcessmoduleSecurityFeatures(string MatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);
            var ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>.Failed(value: null, qRes.CreateQueryExecutionFailed(pnL.eNotCompleted()));
            // Die Sicherheitsmerkmale des Projektes werden übernommen

            try
            {
                var getPm = await GetProcessmodule(MatNo);

                if (!getPm.Succeeded)
                {
                    ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>.Failed(value: null, qRes.CreateQueryExecutionFailed(getPm.ToPlx()));
                }
                else
                {
                    var pm = getPm.Value;

                    var ProjectRepo = new ProjectRepo(pnL);
                    var getProjectSecF = await ProjectRepo.GetProjectWithSecurityFeatures(pm.ProjectMatNo);

                    if (!getProjectSecF.Succeeded)
                    {
                        ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>.Failed(value: null, qRes.CreateQueryExecutionFailed(getProjectSecF.ToPlx()));
                    }
                    else
                    {
                        var prjSecF = getProjectSecF.Value;

                        ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>.Ok(new DfcTree.ProcessModuleDecoratedWithSecurityFeatures(pm, prjSecF));
                    }
                }

            }
            catch (Exception ex)
            {
                ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>.Failed(null, pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex))));
            }

            return ret;
        }

        public async Task<RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>> GetProcessmoduleSecurityFeatures(DfcTree.IProcessmodule processmodule)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);
            var ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>.Failed(value: null, qRes.CreateQueryExecutionFailed(pnL.eNotCompleted()));
            // Die Sicherheitsmerkmale des Projektes werden übernommen

            try
            {
                var ProjectRepo = new ProjectRepo(pnL);
                var getProjectMatNo = await ProjectRepo.GetMatNoForProject(processmodule);
                if (!getProjectMatNo.Succeeded)
                {
                    ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>.Failed(null,
                        qRes.CreateQueryExecutionFailed(
                            pnL.EncapsulateAsPropertyValue(getProjectMatNo.ToPlx()),
                            pnL.m(TT.Operators.Relations.Eq.UID, pnL.p(TT.ATMO.DFC.ProjectNo.UID, processmodule.ProjectNo))));
                }
                else
                {
                    var getProjectSecF = await ProjectRepo.GetProjectWithSecurityFeatures(getProjectMatNo.Value);

                    if (!getProjectSecF.Succeeded)
                    {
                        ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>.Failed(null,
                            qRes.CreateQueryExecutionFailed(getProjectSecF.ToPlx()));
                    }
                    else
                    {
                        var prjSecF = getProjectSecF.Value;
                        ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>.Ok(
                            new DfcTree.ProcessModuleDecoratedWithSecurityFeatures(processmodule, prjSecF));
                    }
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>.Failed(null, pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex))));
            }

            return ret;

        }

        public async Task<RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>> GetProcessmoduleWithAreasOfConstruction(string MatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);
            var ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>.Failed(null, pnL.eNotCompleted());

            try
            {
                var getPmod = await GetProcessmodule(MatNo);

                if (!getPmod.Succeeded)
                {
                    ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>.Failed(null, qRes.CreateQueryExecutionFailed(getPmod.ToPlx()));
                }
                else if (qRes.CreateQueryResultEmpty().IsSubTreeOf(getPmod.MessageEntity))
                {
                    ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>.Failed(null, qRes.CreateQueryResultEmpty(pnL.txt(MatNo)));
                }
                else
                {
                    // Abruf der Sicherheitsmerkmale vom Projekt
                    var prjRepo = new ProjectRepo(pnL);
                    var getProjectSecF = await prjRepo.GetProjectWithSecurityFeatures(getPmod.Value.ProjectMatNo);

                    if (!getProjectSecF.Succeeded)
                    {
                        ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>.Failed(null, qRes.CreateQueryExecutionFailed(getProjectSecF.ToPlx()));
                    }
                    else if (qRes.CreateQueryResultEmpty().IsSubTreeOf(getProjectSecF.MessageEntity))
                    {
                        ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>.Failed(
                            null,
                            qRes.CreateQueryResultEmpty(pnL.i(TT.ATMO.DFC.Project.UID, pnL.p(TT.ATMO.DFC.MatNo.UID, pnL.txt(getPmod.Value.ProjectMatNo)))));
                    }
                    else
                    {
                        ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>.Ok(
                                new DfcTree.ProcessModuleDecoratedWithSecurityFeatures(getPmod.Value,
                                getProjectSecF.Value
                            ));
                    }
                }
            }
            catch (Exception ex)
            {
                RCV3sV<DfcTree.ProcessModuleDecoratedWithSecurityFeatures>.Failed(null, TraceHlp.FlattenExceptionMessagesPN(ex));
            }

            return ret;
        }

        /// <summary>
        /// mko, 3.12.2020
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public async Task<RCV3sV<DfcTree.ProcessModuleDecoratedWithElectricalArea>> GetProcessmoduleWithElectricalArea(string MatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);
            var ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithElectricalArea>.Failed(null, pnL.eNotCompleted());

            var StrToMatClassConverter = new StringToMatClassConverter();

            try
            {
                var getPmod = await GetProcessmodule(MatNo);

                if (!getPmod.Succeeded)
                {
                    ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithElectricalArea>.Failed(null, qRes.CreateQueryExecutionFailed(getPmod.ToPlx()));
                }
                else if (qRes.CreateQueryResultEmpty().IsSubTreeOf(getPmod?.MessageEntity))
                {
                    ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithElectricalArea>.Failed(null, qRes.CreateQueryResultEmpty(pnL.txt(MatNo)));
                }
                else
                {
                    // Materialnummer der mechanische Stückliste bestimmen

                    var sql = new SQL<Bo.StringObj>();
                    var tabStpo = new Tables.STPOView602();

                    var q = sql.Select(
                                    sql.Map(tabStpo.MatNr, (bo, v)
                                        => bo.Value = ColTool.GetSave(v, "")))
                               .From(tabStpo)
                               .Where(
                                    sql.And(
                                        sql.Eq(tabStpo.BGMatNr, MatNo),
                                        sql.LikeUpperCase(tabStpo.MaterialKurzText, $"%{StrToMatClassConverter.ToMatClassString(MatClass.BomTypeEL)}")
                                        )
                                )
                               .done();

                    var getMEMatNo = await GetRecordAsync(q);

                    var FilterCond = pnL.m(TT.Operators.Relations.Eq.UID,
                                        pnL.i(TT.ATMO.DFC.ElectAssy.UID, pnL.p(TT.ATMO.DFC.MatNo.UID, MatNo)));

                    if (!getMEMatNo.Succeeded)
                    {
                        ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithElectricalArea>.Failed(null,
                            qRes.CreateQueryExecutionFailed(
                                tabStpo,
                                pnL.EncapsulateAsPropertyValue(getMEMatNo.ToPlx()),
                                FilterCond));
                    }
                    else if (getMEMatNo.Value.IsEmpty)
                    {
                        ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithElectricalArea>.Failed(null,
                            qRes.CreateQueryFailsDueInconsistencies(
                                tabStpo,
                                pnL.i(TTD.MetaData.Details.UID,
                                        pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.ATMO.SAP.BomErrors.BomAtAtmoStructuralError.UID),
                                        pnL.p_NID(TTD.StateDescription.Why.UID, TT.ATMO.SAP.BomErrors.MainElectAssyMissing.UID)),
                                FilterCond));
                    }
                    else
                    {
                        var MEMatNo = getMEMatNo.Value.Entity.Value;

                        // Mechanische Stückliste laden
                        var AreaRepo = new AreaOfConstructionRepo(pnL);
                        var getEl = await AreaRepo.GetElectricalArea(MEMatNo);

                        if (!getEl.Succeeded)
                        {
                            ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithElectricalArea>.Failed(null,
                                qRes.CreateQueryExecutionFailed(getEl.ToPlx()));
                        }
                        else if (qRes.CreateQueryResultEmpty().IsSubTreeOf(getEl?.MessageEntity))
                        {
                            ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithElectricalArea>.Failed(null,
                                 qRes.CreateQueryFailsDueInconsistencies(
                                                                        pnL.i(TTD.MetaData.Details.UID,
                                                                            pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.ATMO.SAP.BomErrors.BomAtAtmoStructuralError.UID),
                                                                            pnL.p_NID(TTD.StateDescription.Why.UID, TT.ATMO.SAP.BomErrors.MainMechAssyMissing.UID)),
                                                                        pnL.m(TT.Operators.Relations.Eq.UID,
                                                                            pnL.p(TT.ATMO.DFC.MatNo.UID, MEMatNo))));
                        }
                        else
                        {
                            ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithElectricalArea>.Ok(new DfcTree.ProcessModuleDecoratedWithElectricalArea(getPmod.Value, getEl.Value));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithElectricalArea>.Failed(null, TraceHlp.FlattenExceptionMessagesPN(ex));
            }

            return ret;
        }

        /// <summary>
        /// mko, 14.12.2020
        /// </summary>
        /// <param name="processmodule"></param>
        /// <returns></returns>
        public async Task<RCV3sV<DfcTree.ProcessModuleDecoratedWithElectricalArea>> GetProcessmoduleWithElectricalArea(DfcTree.IProcessmodule processmodule)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);
            var ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithElectricalArea>.Failed(null, pnL.eNotCompleted());

            var StrToMatClassConverter = new StringToMatClassConverter();

            try
            {
                // Materialnummer der elektrischen Stückliste bestimmen

                var sql = new SQL<Bo.StringObj>();
                var tabStpo = new Tables.STPOView602();

                var q = sql.Select(
                                sql.Map(tabStpo.MatNr, (bo, v)
                                    => bo.Value = ColTool.GetSave(v, "")))
                           .From(tabStpo)
                           .Where(
                                sql.And(
                                    sql.Eq(tabStpo.BGMatNr, processmodule.ProcessmoduleMatNo),
                                    sql.LikeUpperCase(tabStpo.MaterialKurzText, $"%{StrToMatClassConverter.ToMatClassString(MatClass.BomTypeEL)}")
                                    )
                            )
                           .done();

                var getELMatNo = await GetRecordAsync(q);

                var FilterCond = pnL.m(TT.Operators.Relations.Eq.UID,
                                    pnL.i(TT.ATMO.DFC.ElectAssy.UID, pnL.p(TT.ATMO.DFC.MatNo.UID, processmodule.ProcessmoduleMatNo)));

                if (!getELMatNo.Succeeded)
                {
                    ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithElectricalArea>.Failed(null,
                        qRes.CreateQueryExecutionFailed(
                            tabStpo,
                            pnL.EncapsulateAsPropertyValue(getELMatNo.ToPlx()),
                            FilterCond));
                }
                else if (getELMatNo.Value.IsEmpty)
                {
                    ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithElectricalArea>.Failed(null,
                        qRes.CreateQueryFailsDueInconsistencies(
                            tabStpo,
                            pnL.i(TTD.MetaData.Details.UID,
                                    pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.ATMO.SAP.BomErrors.BomAtAtmoStructuralError.UID),
                                    pnL.p_NID(TTD.StateDescription.Why.UID, TT.ATMO.SAP.BomErrors.MainElectAssyMissing.UID)),
                            FilterCond));
                }
                else
                {
                    var ELMatNo = getELMatNo.Value.Entity.Value;

                    // Elektrische Stückliste laden
                    var AreaRepo = new AreaOfConstructionRepo(pnL);
                    var getEL = await AreaRepo.GetElectricalArea(ELMatNo);

                    if (!getEL.Succeeded)
                    {
                        ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithElectricalArea>.Failed(null,
                            qRes.CreateQueryExecutionFailed(getEL.ToPlx()));
                    }
                    else if (qRes.CreateQueryResultEmpty().IsSubTreeOf(getEL?.MessageEntity))
                    {
                        ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithElectricalArea>.Failed(null,
                             qRes.CreateQueryFailsDueInconsistencies(
                                                                    pnL.i(TTD.MetaData.Details.UID,
                                                                        pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.ATMO.SAP.BomErrors.BomAtAtmoStructuralError.UID),
                                                                        pnL.p_NID(TTD.StateDescription.Why.UID, TT.ATMO.SAP.BomErrors.MainElectAssyMissing.UID)),
                                                                    pnL.m(TT.Operators.Relations.Eq.UID,
                                                                        pnL.p(TT.ATMO.DFC.MatNo.UID, ELMatNo))));
                    }
                    else
                    {
                        ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithElectricalArea>.Ok(new DfcTree.ProcessModuleDecoratedWithElectricalArea(processmodule, getEL.Value));
                    }
                }

            }
            catch (Exception ex)
            {
                ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithElectricalArea>.Failed(null, TraceHlp.FlattenExceptionMessagesPN(ex));
            }

            return ret;
        }

        /// <summary>
        /// mko, 3.12.2020
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public async Task<RCV3sV<DfcTree.ProcessModuleDecoratedWithMechanicalArea>> GetProcessmoduleWithMechArea(string MatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);
            var ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithMechanicalArea>.Failed(null, pnL.eNotCompleted());

            var StrToMatClassConverter = new StringToMatClassConverter();

            try
            {
                var getPmod = await GetProcessmodule(MatNo);

                if (!getPmod.Succeeded)
                {
                    ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithMechanicalArea>.Failed(null, qRes.CreateQueryExecutionFailed(getPmod.ToPlx()));
                }
                else if (qRes.CreateQueryResultEmpty().IsSubTreeOf(getPmod?.MessageEntity))
                {
                    ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithMechanicalArea>.Failed(null, qRes.CreateQueryResultEmpty(pnL.txt(MatNo)));
                }
                else
                {
                    // Materialnummer der mechanische Stückliste bestimmen

                    var sql = new SQL<Bo.StringObj>();
                    var tabStpo = new Tables.STPOView602();

                    var q = sql.Select(
                                    sql.Map(tabStpo.MatNr, (bo, v)
                                        => bo.Value = ColTool.GetSave(v, "")))
                               .From(tabStpo)
                               .Where(
                                    sql.And(
                                        sql.Eq(tabStpo.BGMatNr, MatNo),
                                        sql.LikeUpperCase(tabStpo.MaterialKurzText, $"%{StrToMatClassConverter.ToMatClassString(MatClass.BomTypeME)}")
                                        )
                                )
                               .done();

                    var getMEMatNo = await GetRecordAsync(q);

                    var FilterCond = pnL.m(TT.Operators.Relations.Eq.UID,
                                        pnL.i(TT.ATMO.DFC.MechAssy.UID, pnL.p(TT.ATMO.DFC.MatNo.UID, MatNo)));

                    if (!getMEMatNo.Succeeded)
                    {
                        ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithMechanicalArea>.Failed(null,
                            qRes.CreateQueryExecutionFailed(
                                tabStpo,
                                pnL.EncapsulateAsPropertyValue(getMEMatNo.ToPlx()),
                                FilterCond));
                    }
                    else if (getMEMatNo.Value.IsEmpty)
                    {
                        ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithMechanicalArea>.Failed(null,
                            qRes.CreateQueryFailsDueInconsistencies(
                                tabStpo,
                                pnL.i(TTD.MetaData.Details.UID,
                                        pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.ATMO.SAP.BomErrors.BomAtAtmoStructuralError.UID),
                                        pnL.p_NID(TTD.StateDescription.Why.UID, TT.ATMO.SAP.BomErrors.MainMechAssyMissing.UID)),
                                FilterCond));
                    }
                    else
                    {
                        var MEMatNo = getMEMatNo.Value.Entity.Value;

                        // Mechanische Stückliste laden
                        var AreaRepo = new AreaOfConstructionRepo(pnL);
                        var getME = await AreaRepo.GetMechanicalArea(MEMatNo);

                        if (!getME.Succeeded)
                        {
                            ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithMechanicalArea>.Failed(null,
                                qRes.CreateQueryExecutionFailed(getME.ToPlx()));
                        }
                        else if (qRes.CreateQueryResultEmpty().IsSubTreeOf(getME?.MessageEntity))
                        {
                            ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithMechanicalArea>.Failed(null,
                                 qRes.CreateQueryFailsDueInconsistencies(
                                                                        pnL.i(TTD.MetaData.Details.UID,
                                                                            pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.ATMO.SAP.BomErrors.BomAtAtmoStructuralError.UID),
                                                                            pnL.p_NID(TTD.StateDescription.Why.UID, TT.ATMO.SAP.BomErrors.MainMechAssyMissing.UID)),
                                                                        pnL.m(TT.Operators.Relations.Eq.UID,
                                                                            pnL.p(TT.ATMO.DFC.MatNo.UID, MEMatNo))));
                        }
                        else
                        {
                            ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithMechanicalArea>.Ok(new DfcTree.ProcessModuleDecoratedWithMechanicalArea(getPmod.Value, getME.Value));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithMechanicalArea>.Failed(null, TraceHlp.FlattenExceptionMessagesPN(ex));
            }

            return ret;
        }

        /// <summary>
        /// mko, 7.12.2020
        /// </summary>
        /// <param name="processmodule"></param>
        /// <returns></returns>
        public async Task<RCV3sV<DfcTree.ProcessModuleDecoratedWithMechanicalArea>> GetProcessmoduleWithMechArea(DfcTree.IProcessmodule processmodule)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);
            var ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithMechanicalArea>.Failed(null, pnL.eNotCompleted());

            var StrToMatClassConverter = new StringToMatClassConverter();

            try
            {
                // Materialnummer der mechanische Stückliste bestimmen

                var sql = new SQL<Bo.StringObj>();
                var tabStpo = new Tables.STPOView602();

                var q = sql.Select(
                                sql.Map(tabStpo.MatNr, (bo, v)
                                    => bo.Value = ColTool.GetSave(v, "")))
                           .From(tabStpo)
                           .Where(
                                sql.And(
                                    sql.Eq(tabStpo.BGMatNr, processmodule.ProcessmoduleMatNo),
                                    sql.LikeUpperCase(tabStpo.MaterialKurzText, $"%{StrToMatClassConverter.ToMatClassString(MatClass.BomTypeME)}")
                                    )
                            )
                           .done();

                var getMEMatNo = await GetRecordAsync(q);

                var FilterCond = pnL.m(TT.Operators.Relations.Eq.UID,
                                    pnL.i(TT.ATMO.DFC.MechAssy.UID, pnL.p(TT.ATMO.DFC.MatNo.UID, processmodule.ProcessmoduleMatNo)));

                if (!getMEMatNo.Succeeded)
                {
                    ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithMechanicalArea>.Failed(null,
                        qRes.CreateQueryExecutionFailed(
                            tabStpo,
                            pnL.EncapsulateAsPropertyValue(getMEMatNo.ToPlx()),
                            FilterCond));
                }
                else if (getMEMatNo.Value.IsEmpty)
                {
                    ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithMechanicalArea>.Failed(null,
                        qRes.CreateQueryFailsDueInconsistencies(
                            tabStpo,
                            pnL.i(TTD.MetaData.Details.UID,
                                    pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.ATMO.SAP.BomErrors.BomAtAtmoStructuralError.UID),
                                    pnL.p_NID(TTD.StateDescription.Why.UID, TT.ATMO.SAP.BomErrors.MainMechAssyMissing.UID)),
                            FilterCond));
                }
                else
                {
                    var MEMatNo = getMEMatNo.Value.Entity.Value;

                    // Mechanische Stückliste laden
                    var AreaRepo = new AreaOfConstructionRepo(pnL);
                    var getME = await AreaRepo.GetMechanicalArea(MEMatNo);

                    if (!getME.Succeeded)
                    {
                        ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithMechanicalArea>.Failed(null,
                            qRes.CreateQueryExecutionFailed(getME.ToPlx()));
                    }
                    else if (qRes.CreateQueryResultEmpty().IsSubTreeOf(getME?.MessageEntity))
                    {
                        ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithMechanicalArea>.Failed(null,
                             qRes.CreateQueryFailsDueInconsistencies(
                                                                    pnL.i(TTD.MetaData.Details.UID,
                                                                        pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.ATMO.SAP.BomErrors.BomAtAtmoStructuralError.UID),
                                                                        pnL.p_NID(TTD.StateDescription.Why.UID, TT.ATMO.SAP.BomErrors.MainMechAssyMissing.UID)),
                                                                    pnL.m(TT.Operators.Relations.Eq.UID,
                                                                        pnL.p(TT.ATMO.DFC.MatNo.UID, MEMatNo))));
                    }
                    else
                    {
                        ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithMechanicalArea>.Ok(new DfcTree.ProcessModuleDecoratedWithMechanicalArea(processmodule, getME.Value));
                    }
                }

            }
            catch (Exception ex)
            {
                ret = RCV3sV<DfcTree.ProcessModuleDecoratedWithMechanicalArea>.Failed(null, TraceHlp.FlattenExceptionMessagesPN(ex));
            }

            return ret;
        }
    }
}
