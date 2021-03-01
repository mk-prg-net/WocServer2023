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

using AssyCore = ATMO.DFC.Tree.AssyCore;
using AssyInBomContex = ATMO.DFC.Tree.AssyCoreDecoratedWithBomContex;
using AssyWithBomItems = ATMO.DFC.Tree.AssyCoreDecoratedWithBomItems;
using AssyWithSecF = ATMO.DFC.Tree.AssyCoreDecoratedWithSecurityFeatures;

namespace DFC3.DB.Queries.DfcTreeRepos
{
    /// <summary>
    /// mko, 1.10.2020
    /// </summary>
    public class AssyRepo
        : QueriesBaseAsync,
        DfcTree.IAssyRepository<
            AssyCore,
            AssyInBomContex,
            AssyWithSecF,
            AssyWithBomItems>

    {

        public AssyRepo(IComposer pnL)
            : base(pnL) { }

        /// <summary>
        /// mko, 1.10.2020
        /// Liest den Stücklistenkopf einer Baugruppe ein.
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public async Task<RCV3sV<AssyCore>> GetAssy(string MatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);
            var ret = RCV3sV<AssyCore>.Failed(value: null, pnL.eNotCompleted());

            try
            {
                var sql = new SQL<AssyCore>();
                var tab = new Tables.Mara();

                //var MatClass = ATMO.DFC.Material.MatClass.none;
                var NodeType = "";
                var MatClass = ATMO.DFC.Material.MatClass.none;
                var MKlasseConverter = new StringToMatClassConverter();
                var MSTAEConverter = new StringToMSTAEConverter();
                var OriginOfPartconverter = new StringToOriginOfPartsConverter();

                var q = sql.Select(
                        sql.Map(tab.MatNr, (bo, v) => bo.AssyMatNo = MatNo),
                        sql.Map(tab.ZeichungsNr, (bo, v) => bo.AssyDrawingMatNo = ColTool.GetSave(v, "")),
                        sql.Map(tab.NodeType, (bo, v) => NodeType = ColTool.GetSave(v, "")),
                        sql.Map(tab.MKlasse, (bo, v) =>
                        {                            
                            var mcStr = string.IsNullOrWhiteSpace(NodeType) ? ColTool.GetSave(v, "") : NodeType;
                            MatClass = MKlasseConverter.ToMatClass(mcStr, pnL).ValueOrException;
                            bo.BomType = MatClass == MatClass.ElectAssy ? DfcTree.BOMTypes.electricalBOM : DfcTree.BOMTypes.mechanicalBOM;
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
                        sql.Map(tab.MAKTX, (bo, v) => bo.AssyName = ColTool.GetSave(v, "")),
                        sql.Map(tab.StdBg, (bo, v) => bo.IsStandard = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, ""))),
                        sql.Map(tab.EVWP, (bo, v) => bo.IsSparePart = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, ""))),
                        sql.Map(tab.DOKU, (bo, v) => bo.IsRelevantForDocumentation = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, ""))),
                        sql.Map(tab.ZAT, (bo, v) => 
                            bo.IsStandardAtSite = tab.ParseZAT(ColTool.GetSave(v, ""), pnL)),

                        // Statistische Daten laden
                        sql.Map(tab.Verbaut, (bo, v) => bo.AssyTotalNumberOfInstallations = ColTool.GetSave(v, 0))


                    )
                    .From(tab)
                    .Where(sql.Eq(tab.MatNr, sql.Txt(MatNo)))
                    .done();

                var getAssy = await GetRecordAsync(q);

                if (!getAssy.Succeeded)
                {
                    ret = RCV3sV<AssyCore>.Failed(null, qRes.CreateQueryExecutionFailed(getAssy.ToPlx()));
                }
                else if (getAssy.ValueOrException.IsEmpty)
                {
                    ret = RCV3sV<AssyCore>.Failed(null, qRes.CreateQueryResultEmpty(pnL.txt(MatNo)));
                }
                else if (MatClass != MatClass.ElectAssy && MatClass != MatClass.MechAssy)
                {
                    ret = RCV3sV<AssyCore>.Failed(
                        getAssy.ValueOrException.Entity,
                        pnL.ReturnFetchWithWarnings(pnL.NID(TT.Access.Datasources.WellKnown.ATMO.DFC.MaraTab.UID),
                                                        pnL.i(TT.Validation.Validate.UID,
                                                            pnL.m(TT.Operators.Relations.IsOfType.UID,
                                                                pnL.p_NID(TTD.MetaData.Type.UID, TT.ATMO.DFC.MechAssy.UID),
                                                                pnL.p_NID(TTD.MetaData.Type.UID, TT.ATMO.DFC.ElectAssy.UID),
                                                                pnL.ret(pnL.eFails()))),
                                                        pnL.txt(MatNo)));
                }
                else
                {
                    ret = RCV3sV<AssyCore>.Ok(getAssy.ValueOrException.Entity);
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<AssyCore>.Failed(null, pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex))));
            }

            return ret;
        }


        /// <summary>
        /// mko, 9.9.2020
        /// 
        /// Liest Stücklistenkopf und Stücklistenpositionen einer Baugruppe ein
        /// 
        /// Achtung: die Serialisierung der heterogenen Ergebnisliste Liste erfordert besondere Einstellungen in
        /// JSON.NET. Details dazu hier: https://www.codeproject.com/Tips/1119121/Serialize-and-Deserialize-Classes-with-Interface-P
        /// 
        /// </summary>
        /// <param name="assy"></param>
        /// <returns></returns>
        public async Task<RCV3sV<AssyWithBomItems>> GetAssyBomItems(string assyMatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var ret = RCV3sV<AssyWithBomItems>.Failed(value: null, pnL.eNotCompleted());

            try
            {
                // Stücklistenkopf einlesen
                var getAssy = await GetAssy(assyMatNo);

                if (!getAssy.Succeeded)
                {
                    ret = RCV3sV<AssyWithBomItems>.Failed(null, pnL.m("GetAssy", pnL.p(TT.ATMO.DFC.MatNo.UID, assyMatNo), pnL.ret(pnL.eFails(pnL.EncapsulateAsEventParameter(getAssy.MessageEntity)))));
                }
                else
                {
                    return await GetAssyBomItems(getAssy.ValueOrException);

                }

            }
            catch (Exception ex)
            {
                ret = RCV3sV<AssyWithBomItems>.Failed(null, pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex))));
            }

            return ret;
        }

        /// <summary>
        /// mko, 1.10.2020
        /// </summary>
        /// <param name="assy"></param>
        /// <returns></returns>
        public async Task<RCV3sV<AssyWithBomItems>> GetAssyBomItems(DfcTree.IAssy assy)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var ret = RCV3sV<AssyWithBomItems>.Failed(value: null, pnL.eNotCompleted());

            try
            {
                var sql = new SQL<Bo.StPoViewBo>();
                var tab = new Tables.STPOView602();

                var MSTAEConverter = new StringToMSTAEConverter();
                var MatClassConverter = new StringToMatClassConverter();
                var OriginOfPartsConverter = new StringToOriginOfPartsConverter();
                var NodeType = "";

                var q = sql.Select(
                        sql.Map(tab.BGMatNr.FQN, (bo, v) => bo.BGMatNr = ColTool.GetSave(v, "")),
                        sql.Map(tab.DokuHakenInitialwertBeiAnlage.FQN, (bo, v) => bo.Dokuhaken = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, ""))),
                        sql.Map(tab.EVWInitialwertBeiAnlage.FQN, (bo, v) => bo.IstEVW = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, ""))),
                        sql.Map(tab.MatArt.FQN, (bo, v) => bo.MatArt = ColTool.GetSave(v, "")),
                        sql.Map(tab.MatSprachCodeBenennung.FQN, (bo, v) => bo.MatSprachCodeBenennung = ColTool.GetSave(v, -1)),
                        sql.Map(tab.MaterialKurzText.FQN, (bo, v) => bo.MaterialKurzText = ColTool.GetSave(v, "")),
                        sql.Map(tab.NodeType.FQN, (bo, v) =>
                        {
                            NodeType = ColTool.GetSave(v, "");
                            bo.NodeType = MatClassConverter.ToMatClass(NodeType, pnL).ValueOrException;
                        }),
                        sql.Map(tab.MatKlasse.FQN, (bo, v) =>
                        {
                            var strMatKlasse = ColTool.GetSave(v, "").Trim().ToUpper();
                            var getMatClass = MatClassConverter.ToMatClass(strMatKlasse, pnL);
                            bo.MatKlasse = tab.MatClassFromNodeTypeAndMKlasseField(NodeType, strMatKlasse, pnL).ValueOrException;
                        }),
                        sql.Map(tab.MatNr.FQN, (bo, v) => bo.MatNr = ColTool.GetSave(v, "")),
                        sql.Map(tab.Menge.FQN, (bo, v) => bo.Menge = ColTool.GetSave(v, 0)),
                        sql.Map(tab.MSTAE.FQN, (bo, v) =>
                        {
                            var strMSTAE = ColTool.GetSave(v, "").Trim();
                            var getMSTAE = MSTAEConverter.ToMSTAE(strMSTAE, pnL);
                            bo.MSTAE = getMSTAE.Succeeded ? getMSTAE.Value : MSTAE.none;
                        }),
                        sql.Map(tab.PosNr.FQN, (bo, v) => bo.PosNr = ColTool.GetSave(v, (short)0)),
                        sql.Map(tab.StdBg.FQN, (bo, v) => bo.StdBg = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, ""))),
                        sql.Map(tab.ZeichnungsNummer.FQN, (bo, v) => bo.ZeichnungsNummer = ColTool.GetSave(v, ""))
                    )
                    .From(tab)
                    .Where(sql.Eq(tab.BGMatNr.FQN, sql.Txt(assy.AssyMatNo)))
                    // Auf Sortierung im Oracle DB Server wird aus Performance- Gründen verzichtet. Kann auch 
                    // hier im Client erfolgen.
                    //.By(tab.PosNr)
                    .done();

                var getRes = await GetRecordsAsync(q);

                if (!getRes.Succeeded)
                {
                    ret = RCV3sV<AssyWithBomItems>.Failed(value: null, qRes.CreateQueryExecutionFailed(getRes.ToPlx()));
                }
                else if (getRes.Value.IsEmpty)
                {
                    ret = RCV3sV<AssyWithBomItems>.Failed(value: null, qRes.CreateQueryResultEmpty());
                }
                else
                {
                    // Einlesen der Stücklistenpositionen als Baugruppen oder Einzelteile

                    //var lstBomItems = new List<DfcTree.IMatBomNodePosition>();
                    var Items = new List<DfcTree.IMatBomNodePosition>();                    

                    foreach (var bomPos in getRes.Value.Entities.OrderBy(bp => bp.PosNr))
                    {

                        Func<IEvent, IMethod> classifyResultDescriptor = e => pnL.m(TT.Operators.Sets.Classify.UID,
                                                                                        pnL.p(TTD.MetaData.Arg.UID, pnL.m(TT.Operators.Relations.Eq.UID,
                                                                                                                        pnL.p_NID(TTD.MetaData.Arg.UID, TT.ATMO.SAP.Materials.MatClass.UID))),
                                                                                        pnL.p(TTD.MetaData.Arg.UID,
                                                                                            pnL.i(TT.ATMO.DFC.BOMPos.UID,
                                                                                                pnL.p(TT.ATMO.DFC.BOMPosNo.UID, bomPos.PosNr),
                                                                                                pnL.p(TT.ATMO.DFC.MatNo.UID, bomPos.MatNr))),
                                                                                        pnL.ret(e));

                        var MatKlasse = bomPos.NodeType == ATMO.DFC.Material.MatClass.none ? bomPos.MatKlasse : bomPos.NodeType;

                        if (MatKlasse == MatClass.none)
                        {
                            var prj = new Mara(pnL);
                            var getMatClass = prj.ClassifyMatNoContent(bomPos.MatNr);
                            if (getMatClass.Succeeded)
                            {
                                MatKlasse = getMatClass.Value;
                            }
                            else
                            {
                                throw new RCV3Exception(classifyResultDescriptor(pnL.eFails(pnL.EncapsulateAsEventParameter(getMatClass.MessageEntity))));
                            }
                        }

                        if (MatKlasse != MatClass.ElectAssy
                            && MatKlasse != MatClass.ElectSinglePart
                            && MatKlasse != MatClass.MechAssy
                            && MatKlasse != MatClass.MechSinglePart)
                        {
                            ret = RCV3sV<AssyWithBomItems>.Failed(null, classifyResultDescriptor(pnL.eFails(TT.ATMO.SAP.BomErrors.IncorrectlyClassifiedMaterial.UID)));
                        }
                        else
                        {
                            //var matBomPos = new ATMO.DFC.Tree.MatBomNodePos()
                            //{
                            //    BomMatNo = assy.AssyMatNo,
                            //    CurrentBomPos = bomPos.PosNr,
                            //    CurrentBomPosQuantity = bomPos.Menge,
                            //    MatClassOfCurrentBomPos = MatKlasse,
                            //    MatNoOfCurrentBomPos = bomPos.MatNr
                            //};

                            switch (MatKlasse)
                            {
                                case MatClass.MechAssy:
                                case MatClass.ElectAssy:
                                    {
                                        var SubAssy = new DfcTree.AssyCoreInBomContext()
                                        {
                                            BomMatNo = assy.AssyMatNo,
                                            AssyCreated = bomPos.Lup,
                                            AssyMatNo = bomPos.MatNr,
                                            AssyTotalNumberOfInstallations = 0,
                                            AssyDrawingMatNo = bomPos.ZeichnungsNummer,
                                            AssyLup = bomPos.Lup,
                                            AssyName = bomPos.MaterialKurzText,
                                            BomType = MatKlasse == MatClass.ElectAssy ? DfcTree.BOMTypes.electricalBOM : DfcTree.BOMTypes.mechanicalBOM,
                                            MSTAE = bomPos.MSTAE,
                                            OriginOfPart = OriginOfPartsConverter.ToOriginOfParts(bomPos.MatArt, pnL).ValueOrException,
                                            IsPattern = false,
                                            IsRelevantForDocumentation = bomPos.Dokuhaken,
                                            IsSparePart = bomPos.IstEVW,
                                            IsStandardAtSite = null,
                                            IsStandard = bomPos.StdBg,
                                            BomStatus = DfcTree.BomStatus.none,
                                            ProcurementStatus = DfcTree.ProcurementStatus.NoCurrentOrders,
                                            SAPBomState = DfcTree.SAPBomStatus.none,                                            
                                            CurrentBomPos = bomPos.PosNr,
                                            CurrentBomPosQuantity = bomPos.Menge
                                        };

                                        //SubAssy.TryAddObjectExtension<DfcTree.IAssyInBomContext>(new AssyInBomContex(SubAssy, SubAssy));

                                        // Anreichern mit weiteren Details
                                        Items.Add(SubAssy);
                                    }
                                    break;
                                case MatClass.MechSinglePart:
                                case MatClass.ElectSinglePart:
                                    {
                                        var sp = new DfcTree.SinglePartInBomContext()
                                        {
                                            BomMatNo = assy.AssyMatNo,
                                            CurrentBomPos = bomPos.PosNr,
                                            CurrentBomPosQuantity = bomPos.Menge,
                                            IsSparePart = bomPos.IstEVW,
                                            BomType = MatKlasse == MatClass.ElectSinglePart ? DfcTree.BOMTypes.electricalBOM : DfcTree.BOMTypes.mechanicalBOM,
                                            MSTAE = bomPos.MSTAE,
                                            SinglePartLup = bomPos.Lup,
                                            SinglePartMatNo = bomPos.MatNr,
                                            SinglePartName = bomPos.MaterialKurzText,
                                            BomStatus = DfcTree.BomStatus.none,
                                            IsPattern = false,
                                            IsRelevantForDocumentation = bomPos.Dokuhaken,
                                            IsStandard = bomPos.StdBg,
                                            ProcurementStatus = DfcTree.ProcurementStatus.NoCurrentOrders,
                                            OriginOfPart = OriginOfPartsConverter.ToOriginOfParts(bomPos.MatArt, pnL).ValueOrException,
                                            SinglePartCreated = bomPos.Lup,
                                            SAPBomState = DfcTree.SAPBomStatus.none,
                                            SinglePartDrawingMatNo = bomPos.ZeichnungsNummer,
                                            SinglePartTotalNumberOfInstallations = 0
                                        };

                                        //var sp = new DfcTree.SinglePartDecoratedWithBomContext(spM, matBomPos);

                                        Items.Add(sp);
                                    }
                                    break;
                                default:
                                    {
                                        ret = RCV3sV<AssyWithBomItems>.Failed(null, pnL.eFails());
                                    }
                                    break;
                            }
                        }
                    }

                    var assyWithBomItems = new AssyWithBomItems(assy, Items);
                    ret = RCV3sV<AssyWithBomItems>.Ok(assyWithBomItems);
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<AssyWithBomItems>.Failed(null, pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex))));
            }

            return ret;
        }


        /// <summary>
        /// mko, 1.10.2020
        /// Abrufen einer Baugruppe als Stücklistenposition innerhalb einer anderen Baugruppe
        /// </summary>
        /// <param name="bomMatNo"></param>
        /// <param name="assyMatNo"></param>
        /// <returns></returns>
        public async Task<RCV3sV<AssyInBomContex>> GetAssyInBomContext(string bomMatNo, string assyMatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var ret = RCV3sV<AssyInBomContex>.Failed(value: null, pnL.eNotCompleted());

            try
            {
                // Stücklistenkopf einlesen
                var getAssy = await GetAssy(assyMatNo);

                if (!getAssy.Succeeded)
                {
                    ret = RCV3sV<AssyInBomContex>.Failed(null, pnL.m("GetAssyInBomContext", pnL.p(TT.ATMO.DFC.MatNo.UID, assyMatNo), pnL.ret(pnL.eFails(pnL.EncapsulateAsEventParameter(getAssy.MessageEntity)))));
                }
                else
                {
                    ret = await GetAssyInBomContext(bomMatNo, getAssy.ValueOrException);
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<AssyInBomContex>.Failed(null, pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex))));
            }

            return ret;
        }

        /// <summary>
        /// mko, 2.10.2020
        /// </summary>
        /// <param name="bomMatNo"></param>
        /// <param name="assy"></param>
        /// <returns></returns>
        public async Task<RCV3sV<AssyInBomContex>> GetAssyInBomContext(string bomMatNo, DfcTree.IAssy assy)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var ret = RCV3sV<AssyInBomContex>.Failed(value: null, pnL.eNotCompleted());

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
                            sql.Eq(tab.MatNr.FQN, sql.Txt(assy.AssyMatNo)),
                            sql.Eq(tab.BGMatNr.FQN, sql.Txt(bomMatNo))))
                // Auf Sortierung im Oracle DB Server wird aus Performance- Gründen verzichtet. Kann auch 
                // hier im Client erfolgen.
                //.By(tab.PosNr)
                .done();

                var getRes = await GetRecordAsync(q);

                if (!getRes.Succeeded)
                {
                    ret = RCV3sV<AssyInBomContex>.Failed(value: null, qRes.CreateQueryExecutionFailed(getRes.ToPlx()));
                }
                else if (getRes.Value.IsEmpty)
                {
                    ret = RCV3sV<AssyInBomContex>.Failed(value: null, qRes.CreateQueryResultEmpty());
                }
                else
                {
                    var bomPos = getRes.ValueOrException.Entity;

                    ret = RCV3sV<AssyInBomContex>.Ok(new AssyInBomContex(assy, bomPos));
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<AssyInBomContex>.Failed(null, pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex))));
            }

            return ret;
        }

        /// <summary>
        /// mko, 2.10.2020
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public async Task<RCV3sV<AssyWithSecF>> GetAssyWithSecurityFeatures(string MatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var ret = RCV3sV<AssyWithSecF>.Failed(value: null, pnL.eNotCompleted());
            try
            {
                // Stücklistenkopf einlesen
                var getAssy = await GetAssy(MatNo);

                if (!getAssy.Succeeded)
                {
                    ret = RCV3sV<AssyWithSecF>.Failed(
                            null, 
                            pnL.m("GetAssy", 
                                pnL.p(TT.ATMO.DFC.MatNo.UID, MatNo), 
                                pnL.ret(pnL.eFails(pnL.EncapsulateAsEventParameter(getAssy.MessageEntity)))));
                }
                else
                {
                    ret = await GetAssyWithSecurityFeatures(getAssy.ValueOrException);
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<AssyWithSecF>.Failed(null, pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex))));
            }

            return ret;
        }


        /// <summary>
        /// mko, 2.10.2020
        /// </summary>
        /// <param name="assy"></param>
        /// <returns></returns>
        public async Task<RCV3sV<AssyWithSecF>> GetAssyWithSecurityFeatures(DfcTree.IAssy assy)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var ret = RCV3sV<AssyWithSecF>.Failed(value: null, pnL.eNotCompleted());
            try
            {


                var mara2Queries = new Mara2(pnL);

                var getReleasedSites = await mara2Queries.GetSiteActivationsFor(assy.AssyMatNo);

                if (!getReleasedSites.Succeeded)
                {
                    ret = RCV3sV<AssyWithSecF>.Failed(
                        null,
                        qRes.CreateQueryExecutionFailed(
                            pnL.m("GetSiteActivationsFor",
                                pnL.p(TT.ATMO.DFC.MatNo.UID, assy.AssyMatNo),
                                pnL.ret(pnL.eFails(pnL.EncapsulateAsEventParameter(getReleasedSites.ToPlx()))))));
                }
                else
                {
                    var assyWithSecF = new AssyWithSecF(assy, getReleasedSites.ValueOrException.publicForAll, getReleasedSites.ValueOrException.siteAccess);
                    ret = RCV3sV<AssyWithSecF>.Ok(assyWithSecF);
                }

            }
            catch (Exception ex)
            {
                ret = RCV3sV<AssyWithSecF>.Failed(null, pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex))));

            }

            return ret;

        }

    }
}
