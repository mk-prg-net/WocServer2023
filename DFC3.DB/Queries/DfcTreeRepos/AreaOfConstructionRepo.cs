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
    /// mko, 1.10.2020
    /// Repository der Verantwortungsbereiche der technischen Enwicklung (Mechanische- und Elektrische Ebene)
    /// </summary>
    public class AreaOfConstructionRepo
        : QueriesBaseAsync,
        DfcTree.IAreasOfConstructionRepo<DfcTree.MechanicalArea, DfcTree.ElectricalArea>
    {
        public AreaOfConstructionRepo(IComposer pnL)
            : base(pnL) { }


        /// <summary>
        /// mko, 10.12.2020
        /// </summary>
        /// <param name="MeMatNo"></param>
        /// <returns></returns>
        public async Task<RCV3sV<DfcTree.ElectricalArea>> GetElectricalArea(string MeMatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);
            var ret = RCV3sV<DfcTree.ElectricalArea>.Failed(value: null, pnL.eNotCompleted());

            try
            {
                var sql = new SQL<DfcTree.ElectricalArea>();
                var tabStpoView = new Tables.STPOView602();
                var tabMara = new Tables.Mara();

                var StrToMatClassConverter = new StringToMatClassConverter();

                string BgMatNo = "";

                var q = sql.Select(

                     // Materialnummer des übergeordneten Prozessmoduls bestimmen
                     sql.Map(tabStpoView.BGMatNr, (bo, v) => BgMatNo = ColTool.GetSave(v, "")),

                     sql.Map(tabStpoView.MaterialKurzText, (bo, v) => bo.ElectricalAreaName = ColTool.GetSave(v, "")),
                     sql.Map(tabStpoView.MatNr, (bo, v) => bo.MatNoOfCurrentBomPos = ColTool.GetSave(v, "")),
                     sql.Map(tabStpoView.PosNr, (bo, v) => bo.CurrentDfcBomPos = ColTool.GetSave(v, (short)0)),

                     // Klassifizierungsmerkmale auslesen
                     sql.Map(tabStpoView.DokuHakenInitialwertBeiAnlage, (bo, v) => bo.IsRelevantForDocumentation = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, ""))),
                     sql.Map(tabStpoView.EVWInitialwertBeiAnlage, (bo, v) => bo.IsEVWP = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, ""))),
                     sql.Map(tabStpoView.StdBg, (bo, v) => bo.IsStandard = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, "")))
                 )
                 .From(tabStpoView)
                 .Where(sql.And(
                         sql.Eq(tabStpoView.MatNr, MeMatNo),
                         sql.Eq(tabStpoView.NodeType, StrToMatClassConverter.ToMatClassString(MatClass.BomTypeEL))
                     ))
                 .done();

                var getME = await GetRecordAsync(q);

                if (!getME.Succeeded)
                {
                    ret = RCV3sV<DfcTree.ElectricalArea>.Failed(null, qRes.CreateQueryExecutionFailed(getME.ToPlx()));
                }
                else if (getME.ValueOrException.IsEmpty)
                {
                    ret = RCV3sV<DfcTree.ElectricalArea>.Failed(null, qRes.CreateQueryResultEmpty(pnL.i(TT.Search.Id.UID, pnL.p(TT.ATMO.DFC.MatNo.UID, MeMatNo))));
                }
                else
                {
                    var me = getME.ValueOrException.Entity;

                    // Prozessmodulkopf zur Prozessmodulmaterialnummer bestimmen
                    var pmRepo = new ProcessmoduleRepo(pnL);
                    var getPm = await pmRepo.GetProcessmodule(BgMatNo);

                    var pm = getPm.ValueOrException;

                    me.ProjectNo = pm.ProjectNo;
                    me.ProjectMatNo = pm.ProjectMatNo;

                    me.StationNo = pm.StationNo;
                    me.StationMatNo = pm.StationMatNo;

                    me.ProcessModuleMatNo = pm.ProcessmoduleMatNo;
                    me.ProcessModul = pm.ProcessModul;

                    ret = RCV3sV<DfcTree.ElectricalArea>.Ok(me);
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<DfcTree.ElectricalArea>.Failed(null, pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex)));
            }

            return ret;
        }

        /// <summary>
        /// mko, 10.12.2020
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public async Task<RCV3sV<DfcTree.ElectricalAreaDecoratedWithMainElectricalParts>> GetElectricalAreaMainAssemblies(DfcTree.IElectricalArea me)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);
            var ret = RCV3sV<DfcTree.ElectricalAreaDecoratedWithMainElectricalParts>.Failed(value: null, pnL.eNotCompleted());

            // Holen aller Hauptbaugruppen aus dem Arbeitsgebiet der Elektriker
            var NodeType = "";
            var tabStpoView = new Tables.STPOView602();

            var MKlasseConverter = new StringToMatClassConverter();

            var sql2 = new SQL<DfcTree.MatBomNodePos>();
            var q2 = sql2.Select(
                    sql2.Map(tabStpoView.MatNr, (bo, v) =>
                    {
                        bo.BomMatNo = me.MatNoOfCurrentBomPos;
                        bo.MatNoOfCurrentBomPos = ColTool.GetSave(v, "");
                    }),
                    sql2.Map(tabStpoView.PosNr, (bo, v) => bo.CurrentBomPos = ColTool.GetSave(v, (short)0)),
                    sql2.Map(tabStpoView.Menge, (bo, v) => bo.CurrentBomPosQuantity = ColTool.GetSave(v, 0)),
                    sql2.Map(tabStpoView.NodeType, (bo, v) => NodeType = ColTool.GetSave(v, "")),
                    sql2.Map(tabStpoView.MatKlasse, (bo, v) =>
                    {
                        var mcStr = ColTool.GetSave(v, "");
                        bo.MatClassOfCurrentBomPos = tabStpoView.MatClassFromNodeTypeAndMKlasseField(NodeType, mcStr, pnL).ValueOrException;
                    })
                )
                .From(tabStpoView)
                .Where(
                    sql2.Eq(tabStpoView.BGMatNr, me.MatNoOfCurrentBomPos)
                )
                .By(tabStpoView.PosNr)
                .done();

            var getMechAssies = await GetRecordsAsync(q2);

            if (!getMechAssies.Succeeded)
            {
                ret = RCV3sV<DfcTree.ElectricalAreaDecoratedWithMainElectricalParts>.Failed(null, qRes.CreateQueryExecutionFailed(getMechAssies.ToPlx()));
            }
            else if (getMechAssies.ValueOrException.IsEmpty)
            {
                ret = RCV3sV<DfcTree.ElectricalAreaDecoratedWithMainElectricalParts>.Failed(null, qRes.CreateQueryResultEmpty(pnL.i(TT.Search.Id.UID, pnL.p(TT.ATMO.DFC.MatNo.UID, me.MatNoOfCurrentBomPos))));
            }
            else
            {

                var mechAss = new DfcTree.ElectricalAreaDecoratedWithMainElectricalParts(me, getMechAssies.Value.Entities);
                ret = RCV3sV<DfcTree.ElectricalAreaDecoratedWithMainElectricalParts>.Ok(mechAss);
            }

            return ret;
        }


        /// <summary>
        /// mko, 30.9.2020
        /// Mechanische Ebene eines Prozessmoduls ermitteln. Sie wird über ihre eigene Materialnummre abgerufen.
        /// </summary>
        /// <param name="MeMatNo">Materialnummer des Bereiches der mechanischen Konstruktion</param>
        /// <returns></returns>
        public async Task<RCV3sV<DfcTree.MechanicalArea>> GetMechanicalArea(string MeMatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);
            var ret = RCV3sV<DfcTree.MechanicalArea>.Failed(value: null, pnL.eNotCompleted());

            try
            {
                var sql = new SQL<DfcTree.MechanicalArea>();
                var tabStpoView = new Tables.STPOView602();
                var tabMara = new Tables.Mara();

                var StrToMatClassConverter = new StringToMatClassConverter();

                string BgMatNo = "";

                var q = sql.Select(

                     // Materialnummer des übergeordneten Prozessmoduls bestimmen
                     sql.Map(tabStpoView.BGMatNr, (bo, v) => BgMatNo = ColTool.GetSave(v, "")),

                     sql.Map(tabStpoView.MaterialKurzText, (bo, v) => bo.MechanicalAreaName = ColTool.GetSave(v, "")),
                     sql.Map(tabStpoView.MatNr, (bo, v) => bo.MatNoOfCurrentBomPos = ColTool.GetSave(v, "")),
                     sql.Map(tabStpoView.PosNr, (bo, v) => bo.CurrentDfcBomPos = ColTool.GetSave(v, (short)0)),

                     // Klassifizierungsmerkmale auslesen
                     sql.Map(tabStpoView.DokuHakenInitialwertBeiAnlage, (bo, v) => bo.IsRelevantForDocumentation = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, ""))),
                     sql.Map(tabStpoView.EVWInitialwertBeiAnlage, (bo, v) => bo.IsEVWP = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, ""))),
                     sql.Map(tabStpoView.StdBg, (bo, v) => bo.IsStandard = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, "")))
                 )
                 .From(tabStpoView)
                 .Where(sql.And(
                         sql.Eq(tabStpoView.MatNr, MeMatNo),
                         sql.Eq(tabStpoView.NodeType, StrToMatClassConverter.ToMatClassString(MatClass.BomTypeME))
                     ))
                 .done();

                var getME = await GetRecordAsync(q);

                if (!getME.Succeeded)
                {
                    ret = RCV3sV<DfcTree.MechanicalArea>.Failed(null, qRes.CreateQueryExecutionFailed(getME.ToPlx()));
                }
                else if (getME.ValueOrException.IsEmpty)
                {
                    ret = RCV3sV<DfcTree.MechanicalArea>.Failed(null, qRes.CreateQueryResultEmpty(pnL.i(TT.Search.Id.UID, pnL.p(TT.ATMO.DFC.MatNo.UID, MeMatNo))));
                }
                else
                {
                    var me = getME.ValueOrException.Entity;

                    // Prozessmodulkopf zur Prozessmodulmaterialnummer bestimmen
                    var pmRepo = new ProcessmoduleRepo(pnL);
                    var getPm = await pmRepo.GetProcessmodule(BgMatNo);

                    var pm = getPm.ValueOrException;

                    me.ProjectNo = pm.ProjectNo;
                    me.ProjectMatNo = pm.ProjectMatNo;

                    me.StationNo = pm.StationNo;
                    me.StationMatNo = pm.StationMatNo;

                    me.ProcessModuleMatNo = pm.ProcessmoduleMatNo;
                    me.ProcessModul = pm.ProcessModul;

                    ret = RCV3sV<DfcTree.MechanicalArea>.Ok(me);
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<DfcTree.MechanicalArea>.Failed(null, pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex)));
            }

            return ret;
        }

        /// <summary>
        /// mko, 20.10.2020
        /// Liefert zum Bereich der Mechanik alle Hauptbaugruppen
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public async Task<RCV3sV<DfcTree.MechanicalAreaDecoratedWithMainMechanicalAssemblies>> GetMechanicalAreaMainAssemblies(DfcTree.IMechanicalArea me)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);
            var ret = RCV3sV<DfcTree.MechanicalAreaDecoratedWithMainMechanicalAssemblies>.Failed(value: null, pnL.eNotCompleted());

            // Holen aller Hauptbaugruppen aus dem Arbeitsgebiet der Mechaniker
            var NodeType = "";
            var tabStpoView = new Tables.STPOView602();

            var MKlasseConverter = new StringToMatClassConverter();

            var sql2 = new SQL<DfcTree.MatBomNodePos>();
            var q2 = sql2.Select(
                    sql2.Map(tabStpoView.MatNr, (bo, v) =>
                    {
                        bo.BomMatNo = me.MatNoOfCurrentBomPos;
                        bo.MatNoOfCurrentBomPos = ColTool.GetSave(v, "");
                    }),
                    sql2.Map(tabStpoView.PosNr, (bo, v) => bo.CurrentBomPos = ColTool.GetSave(v, (short)0)),
                    sql2.Map(tabStpoView.Menge, (bo, v) => bo.CurrentBomPosQuantity = ColTool.GetSave(v, 0)),
                    sql2.Map(tabStpoView.NodeType, (bo, v) => NodeType = ColTool.GetSave(v, "")),
                    sql2.Map(tabStpoView.MatKlasse, (bo, v) =>
                    {
                        var mcStr = ColTool.GetSave(v, "");
                        bo.MatClassOfCurrentBomPos =  tabStpoView.MatClassFromNodeTypeAndMKlasseField(NodeType, mcStr, pnL).ValueOrException;
                    })
                )
                .From(tabStpoView)
                .Where(
                    sql2.Eq(tabStpoView.BGMatNr, me.MatNoOfCurrentBomPos)
                )
                .By(tabStpoView.PosNr)
                .done();

            var getMechAssies = await GetRecordsAsync(q2);

            if (!getMechAssies.Succeeded)
            {
                ret = RCV3sV<DfcTree.MechanicalAreaDecoratedWithMainMechanicalAssemblies>.Failed(null, qRes.CreateQueryExecutionFailed(getMechAssies.ToPlx()));
            }
            else if (getMechAssies.ValueOrException.IsEmpty)
            {
                ret = RCV3sV<DfcTree.MechanicalAreaDecoratedWithMainMechanicalAssemblies>.Failed(null, qRes.CreateQueryResultEmpty(pnL.i(TT.Search.Id.UID, pnL.p(TT.ATMO.DFC.MatNo.UID, me.MatNoOfCurrentBomPos))));
            }
            else
            {

                var mechAss = new DfcTree.MechanicalAreaDecoratedWithMainMechanicalAssemblies(me, getMechAssies.Value.Entities);                
                ret = RCV3sV<DfcTree.MechanicalAreaDecoratedWithMainMechanicalAssemblies>.Ok(mechAss);
            }

            return ret;
        }




    }
}
