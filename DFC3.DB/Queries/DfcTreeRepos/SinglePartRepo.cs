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

using static DFCSecurity.SitesExt;

using SinglePart = ATMO.DFC.Tree.SinglePart;
using SinglePartInBomContex = ATMO.DFC.Tree.SinglePartDecoratedWithBomContext;
using SinglePartWithSecF = ATMO.DFC.Tree.SinglePartDecoratedWithSecurityFeatures;


namespace DFC3.DB.Queries.DfcTreeRepos
{
    /// <summary>
    /// mko, 5.9.2020
    /// </summary>
    public class SinglePartRepo
        : QueriesBaseAsync,
        DfcTree.ISingelPartRepository
        <
            SinglePart,
            SinglePartInBomContex,
            SinglePartWithSecF
        >
    {
        /// <summary>
        /// mko, 5.10.2020
        /// </summary>
        /// <param name="pnL"></param>
        public SinglePartRepo(IComposer pnL)
            : base(pnL) { }

        /// <summary>
        /// mko, 5.10.2020
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public async Task<RCV3sV<SinglePart>> GetSingelPart(string MatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);
            var ret = RCV3sV<SinglePart>.Failed(value: null, pnL.eNotCompleted());

            try
            {
                var sql = new SQL<SinglePart>();
                var tab = new Tables.Mara();

                //var MatClass = ATMO.DFC.Material.MatClass.none;
                var NodeType = "";
                var MatClass = ATMO.DFC.Material.MatClass.none;
                var MKlasseConverter = new StringToMatClassConverter();
                var MSTAEConverter = new StringToMSTAEConverter();
                var OriginOfPartconverter = new StringToOriginOfPartsConverter();

                var q = sql.Select(
                        sql.Map(tab.MatNr, (bo, v) => bo.SinglePartMatNo = MatNo),
                        sql.Map(tab.ZeichungsNr, (bo, v) => bo.SinglePartDrawingMatNo = ColTool.GetSave(v, "")),
                        sql.Map(tab.NodeType, (bo, v) => NodeType = ColTool.GetSave(v, "")),
                        sql.Map(tab.MKlasse, (bo, v) =>
                        {
                            var mcStr = string.IsNullOrWhiteSpace(NodeType) ? ColTool.GetSave(v, "") : NodeType;
                            MatClass = MKlasseConverter.ToMatClass(mcStr, pnL).ValueOrException;

                            TraceHlp.ThrowArgExIf(MatClass != MatClass.ElectSinglePart && MatClass != MatClass.MechSinglePart,
                                pnL.eFails(
                                    pnL.i(TTD.MetaData.Details.UID,
                                        pnL.p(TT.ATMO.DFC.MatNo.UID, MatNo),
                                        pnL.p(TTD.StateDescription.WhatsUp.UID, TT.Access.ATMO.DFC.Errors.MatNoDoesNotReferToExpectedType.UID),
                                        pnL.p(TT.Monitoring.SetPoint.UID, 
                                            pnL.i(TT.Operators.CoDomain.UID, 
                                                pnL.p(TTD.MetaData.Arg.UID, MatClass.ElectSinglePart.ToString()),
                                                pnL.p(TTD.MetaData.Arg.UID, MatClass.MechSinglePart.ToString()))),
                                        pnL.p(TT.Monitoring.ActualValue.UID, MatClass.ToString()))));

                            bo.BomType = MatClass == MatClass.ElectSinglePart ? DfcTree.BOMTypes.electricalBOM : DfcTree.BOMTypes.mechanicalBOM;
                        }),
                        sql.Map(tab.MSTAE, (bo, v) =>
                        {
                            var mstaeStr = ColTool.GetSave(v, "");
                            bo.MSTAE = MSTAEConverter.ToMSTAE(mstaeStr, pnL).ValueOrException;
                        }),
                        sql.Map(tab.MTArt, (bo, v) =>
                        {
                            var mtartStr = ColTool.GetSave(v, "");
                            bo.OriginOfPart = OriginOfPartconverter.ToOriginOfParts(mtartStr, pnL).ValueOrException;
                        }),

                        // Klassifizierungen der Baugruppe laden
                        sql.Map(tab.MAKTX, (bo, v) => bo.SinglePartName = ColTool.GetSave(v, "")),
                        sql.Map(tab.StdBg, (bo, v) => bo.IsStandard = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, ""))),
                        sql.Map(tab.EVWP, (bo, v) => bo.IsSparePart = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, ""))),
                        sql.Map(tab.DOKU, (bo, v) => bo.IsRelevantForDocumentation = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, ""))),
                        sql.Map(tab.ZAT, (bo, v) =>
                            bo.IsStandardAtSite = tab.ParseZAT(ColTool.GetSave(v, ""), pnL)),

                        // Statistische Daten laden
                        sql.Map(tab.Verbaut, (bo, v) => bo.SinglePartTotalNumberOfInstallations = ColTool.GetSave(v, 0))


                    )
                    .From(tab)
                    .Where(sql.Eq(tab.MatNr, sql.Txt(MatNo)))
                    .done();

                var getAssy = await GetRecordAsync(q);

                if (!getAssy.Succeeded)
                {
                    ret = RCV3sV<SinglePart>.Failed(null, qRes.CreateQueryExecutionFailed(getAssy.ToPlx()));
                }
                else if (getAssy.ValueOrException.IsEmpty)
                {
                    ret = RCV3sV<SinglePart>.Failed(null, qRes.CreateQueryResultEmpty(pnL.txt(MatNo)));
                }
                else if (MatClass != MatClass.ElectSinglePart && MatClass != MatClass.MechSinglePart)
                {
                    ret = RCV3sV<SinglePart>.Failed(
                        getAssy.ValueOrException.Entity,
                        pnL.ReturnFetchWithWarnings(pnL.NID(TT.Access.Datasources.WellKnown.ATMO.DFC.MaraTab.UID),
                                                    pnL.i(TTD.MetaData.Details.UID,
                                                        pnL.p(TT.ATMO.DFC.MatNo.UID, MatNo),
                                                        pnL.p(TTD.StateDescription.WhatsUp.UID, TT.Access.ATMO.DFC.Errors.MatNoDoesNotReferToExpectedType.UID),
                                                        pnL.p(TT.Monitoring.SetPoint.UID,
                                                            pnL.i(TT.Operators.CoDomain.UID,
                                                                pnL.p_NID(TTD.MetaData.Arg.UID, TT.ATMO.DFC.MechSinglePart.UID),
                                                                pnL.p_NID(TTD.MetaData.Arg.UID, TT.ATMO.DFC.ElectSinglePart.UID))),
                                                        pnL.p(TT.Monitoring.ActualValue.UID, MatClass.ToString()))));

                }
                else
                {
                    ret = RCV3sV<SinglePart>.Ok(getAssy.ValueOrException.Entity);
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<SinglePart>.Failed(null, pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex))));
            }

            return ret;
        }

        public async Task<RCV3sV<SinglePartInBomContex>> GetSinglePartInBomContext(string bomMatNo, string spMatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var ret = RCV3sV<SinglePartInBomContex>.Failed(value: null, pnL.eNotCompleted());

            try
            {
                // Stücklistenkopf einlesen
                var getAssy = await GetSingelPart(spMatNo);

                if (!getAssy.Succeeded)
                {
                    ret = RCV3sV<SinglePartInBomContex>.Failed(null, pnL.m("GetSinglePartInBomContext", pnL.p(TT.ATMO.DFC.MatNo.UID, spMatNo), pnL.ret(pnL.eFails(pnL.EncapsulateAsEventParameter(getAssy.MessageEntity)))));
                }
                else
                {
                    ret = await GetSinglePartInBomContext(bomMatNo, getAssy.ValueOrException);
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<SinglePartInBomContex>.Failed(null, pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex))));
            }

            return ret;
        }

        public async Task<RCV3sV<SinglePartInBomContex>> GetSinglePartInBomContext(string bomMatNo, DfcTree.ISinglePart sp)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var ret = RCV3sV<SinglePartInBomContex>.Failed(value: null, pnL.eNotCompleted());

            try
            {

                var sql = new SQL<DfcTree.MatBomNodePos>();
                var tab = new Tables.STPOView602();

                var MKlasseConverter = new StringToMatClassConverter();
                var MSTAEConverter = new StringToMSTAEConverter();
                var OriginOfPartconverter = new StringToOriginOfPartsConverter();

                var NodeType = "";

                var q = sql.Select(
                    sql.Map(tab.BGMatNr.FQN, (bo, v) => bo.BomMatNo = ColTool.GetSave(v, "")),
                    sql.Map(tab.NodeType.FQN, (bo, v) =>
                    {
                        NodeType = ColTool.GetSave(v, "");                        
                    }),
                    sql.Map(tab.MatKlasse.FQN, (bo, v) =>
                    {
                        var strMatKlasse = ColTool.GetSave(v, "").Trim().ToUpper();
                        bo.MatClassOfCurrentBomPos = tab.MatClassFromNodeTypeAndMKlasseField(NodeType, strMatKlasse, pnL).ValueOrException;
                    }),
                    sql.Map(tab.Menge.FQN, (bo, v) => bo.CurrentBomPosQuantity = ColTool.GetSave(v, 0)),
                    sql.Map(tab.MatNr.FQN, (bo, v) => bo.MatNoOfCurrentBomPos = ColTool.GetSave(v, "")),
                    sql.Map(tab.PosNr.FQN, (bo, v) => bo.CurrentBomPos = ColTool.GetSave(v, (short)0))
                )
                .From(tab)
                .Where(sql.And(
                            sql.Eq(tab.MatNr.FQN, sql.Txt(sp.SinglePartMatNo)),
                            sql.Eq(tab.BGMatNr.FQN, sql.Txt(bomMatNo))))
                // Auf Sortierung im Oracle DB Server wird aus Performance- Gründen verzichtet. Kann auch 
                // hier im Client erfolgen.
                //.By(tab.PosNr)
                .done();

                var getRes = await GetRecordAsync(q);

                if (!getRes.Succeeded)
                {
                    ret = RCV3sV<SinglePartInBomContex>.Failed(value: null, qRes.CreateQueryExecutionFailed(getRes.ToPlx()));
                }
                else if (getRes.Value.IsEmpty)
                {
                    ret = RCV3sV<SinglePartInBomContex>.Failed(value: null, qRes.CreateQueryResultEmpty());
                }
                else
                {
                    var bomPos = getRes.ValueOrException.Entity;                    

                    ret = RCV3sV<SinglePartInBomContex>.Ok(new SinglePartInBomContex(sp, bomPos));
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<SinglePartInBomContex>.Failed(null, pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex))));
            }

            return ret;
        }

        public async Task<RCV3sV<SinglePartWithSecF>> GetSinglePartWithSecurityFeatures(string MatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var ret = RCV3sV<SinglePartWithSecF>.Failed(value: null, pnL.eNotCompleted());
            try
            {
                // Stücklistenkopf einlesen
                var getAssy = await GetSingelPart(MatNo);

                if (!getAssy.Succeeded)
                {
                    ret = RCV3sV<SinglePartWithSecF>.Failed(
                            null,
                            pnL.m("GetSingelPart",
                                pnL.p(TT.ATMO.DFC.MatNo.UID, MatNo),
                                pnL.ret(pnL.eFails(pnL.EncapsulateAsEventParameter(getAssy.MessageEntity)))));
                }
                else
                {
                    ret = await GetSinglePartWithSecurityFeatures(getAssy.ValueOrException);
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<SinglePartWithSecF>.Failed(null, pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex))));
            }

            return ret;
        }

        /// <summary>
        /// mko, 5.10.2020
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public async Task<RCV3sV<SinglePartWithSecF>> GetSinglePartWithSecurityFeatures(DfcTree.ISinglePart sp)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var ret = RCV3sV<SinglePartWithSecF>.Failed(value: null, pnL.eNotCompleted());
            try
            {


                var mara2Queries = new Mara2(pnL);

                var getReleasedSites = await mara2Queries.GetSiteActivationsFor(sp.SinglePartMatNo);

                if (!getReleasedSites.Succeeded)
                {
                    ret = RCV3sV<SinglePartWithSecF>.Failed(
                        null,
                        qRes.CreateQueryExecutionFailed(
                            pnL.m("GetSiteActivationsFor",
                                pnL.p(TT.ATMO.DFC.MatNo.UID, sp.SinglePartMatNo),
                                pnL.ret(pnL.eFails(pnL.EncapsulateAsEventParameter(getReleasedSites.ToPlx()))))));
                }
                else
                {
                    var spWithSecF = new SinglePartWithSecF(sp, getReleasedSites.ValueOrException.publicForAll, getReleasedSites.ValueOrException.siteAccess);
                    ret = RCV3sV<SinglePartWithSecF>.Ok(spWithSecF);
                }

            }
            catch (Exception ex)
            {
                ret = RCV3sV<SinglePartWithSecF>.Failed(null, pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex))));

            }

            return ret;
        }
    }
}
