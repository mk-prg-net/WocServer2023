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

namespace DFC3.DB.Queries
{
    /// <summary>
    /// mko, 5.11.2019
    /// 
    /// Abfragen über die Stücklistenstruktur
    /// </summary>
    public class Bom
        : QueriesBase
        //DfcTree.IAssyRepository<DfcTree.AssyCore, DfcTree.AssyCoreInBomContext, DfcTree.AssyWithSecurityFeatures, DfcTree.AssyWithStatistics, DfcTree.AssyWithBomItems>
    {
        public Bom(IComposer pnL)
            : base(pnL) { }

        /// <summary>
        /// Stücklistenkopf zu einer Materialnummer einlesen
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public RCV3sV<Bo.StKoBo> GetStKo(string MatNo)
        {
            var ret = RCV3sV<Bo.StKoBo>.Failed(value: null, pnL.eNotCompleted());

            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            try
            {
                var sql = new SQL<Bo.StKoBo>();
                var tab = new Tables.STKO();

                var q = sql.Select(
                        sql.Map(tab.MatNo, (bo, v) => bo.MatNr = ColTool.GetSave(v, "")),
                        sql.Map(tab.Pos, (bo, v) => bo.Pos = ColTool.GetSave(v, -1)),
                        sql.Map(tab.StlStatus, (bo, v) => bo.StlStatus = ColTool.GetSave(v, "")),
                        sql.Map(tab.Lup, (bo, v) => bo.LUP = ColTool.GetSave(v, new DateTime(1900, 1, 1)))
                    )
                    .From(tab)
                    .Where(sql.StrEq(tab.MatNo, sql.Txt(MatNo)))
                    .done();


                var getRec = GetRecord(q);

                if (!getRec.Succeeded)
                {
                    ret = RCV3sV<Bo.StKoBo>.Failed(value: null, qRes.CreateQueryExecutionFailed(getRec.ToPlx()));
                }
                else if (getRec.Succeeded && getRec.Value.IsEmpty)
                {
                    ret = RCV3sV<Bo.StKoBo>.Failed(value: null, qRes.CreateQueryResultEmpty());
                }
                else
                {
                    ret = RCV3sV<Bo.StKoBo>.Ok(value: getRec.Value.Entity);
                }

            }
            catch (Exception ex)
            {
                ret = RCV3sV<Bo.StKoBo>.Failed(value: null, qRes.CreateQueryExecutionFailed(TraceHlp.FlattenExceptionMessagesPN(ex)));
            }

            return ret;
        }

        /// <summary>
        /// mko, 9.9.2020
        /// 
        /// Achtung: die Serialisierung der heterogenen Ergebnisliste Liste erfordert besondere Einstellungen in
        /// JSON.NET. Details dazu hier: https://www.codeproject.com/Tips/1119121/Serialize-and-Deserialize-Classes-with-Interface-P
        /// 
        /// </summary>
        /// <param name="assy"></param>
        /// <returns></returns>
        public RCV3sV<DfcTree.IMatBomNodePosition[]> GetAssyBomItems(string assyMatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var ret = RCV3sV<DfcTree.IMatBomNodePosition[]>.Failed(value: null, pnL.eNotCompleted());

            try
            {
                var sql = new SQL<Bo.StPoViewBo>();
                var tab = new Tables.STPOView602();

                var MSTAEConverter = new StringToMSTAEConverter();
                var MatClassConverter = new StringToMatClassConverter();

                var q = sql.Select(
                        sql.Map(tab.BGMatNr.FQN, (bo, v) => bo.BGMatNr = ColTool.GetSave(v, "")),
                        sql.Map(tab.DokuHakenInitialwertBeiAnlage.FQN, (bo, v) => bo.Dokuhaken = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, ""))),
                        sql.Map(tab.EVWInitialwertBeiAnlage.FQN, (bo, v) => bo.IstEVW = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, ""))),
                        sql.Map(tab.MatArt.FQN, (bo, v) => bo.MatArt = ColTool.GetSave(v, "")),
                        sql.Map(tab.MatSprachCodeBenennung.FQN, (bo, v) => bo.MatSprachCodeBenennung = ColTool.GetSave(v, -1)),
                        sql.Map(tab.MaterialKurzText.FQN, (bo, v) => bo.MaterialKurzText = ColTool.GetSave(v, "")),
                        sql.Map(tab.MatKlasse.FQN, (bo, v) =>
                        {
                            var strMatKlasse = ColTool.GetSave(v, "").Trim().ToUpper();
                            var getMatClass = MatClassConverter.ToMatClass(strMatKlasse, pnL);
                            bo.MatKlasse = getMatClass.Value;
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
                    .Where(sql.Eq(tab.BGMatNr.FQN, sql.Txt(assyMatNo)))
                    // Auf Sortierung im Oracle DB Server wird aus Performance- Gründen verzichtet. Kann auch 
                    // hier im Client erfolgen.
                    //.By(tab.PosNr)
                    .done();

                var getRes = GetRecords(q);

                if (!getRes.Succeeded)
                {
                    ret = RCV3sV<DfcTree.IMatBomNodePosition[]>.Failed(value: null, qRes.CreateQueryExecutionFailed(getRes.ToPlx()));
                }
                else if (getRes.Value.IsEmpty)
                {
                    ret = RCV3sV<DfcTree.IMatBomNodePosition[]>.Failed(value: null, qRes.CreateQueryResultEmpty());
                }
                else
                {
                    // Einlesen der Stücklistenpositionen als Baugruppen oder Einzelteile

                    var lstBomItems = new List<DfcTree.IMatBomNodePosition>();

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

                        if (bomPos.MatKlasse == MatClass.none)
                        {
                            var prj = new Queries.Mara(pnL);
                            var getMatClass = prj.ClassifyMatNoContent(bomPos.MatNr);
                            if (getMatClass.Succeeded)
                            {
                                bomPos.MatKlasse = getMatClass.Value;
                            }
                            else
                            {
                                ret = RCV3sV<DfcTree.IMatBomNodePosition[]>.Failed(new DfcTree.IMatBomNodePosition[] { },
                                                                                    classifyResultDescriptor(pnL.eFails(pnL.EncapsulateAsEventParameter(getMatClass.MessageEntity))));
                            }
                        }

                        if (bomPos.MatKlasse != MatClass.ElectAssy
                            && bomPos.MatKlasse != MatClass.ElectSinglePart
                            && bomPos.MatKlasse != MatClass.MechAssy
                            && bomPos.MatKlasse != MatClass.MechSinglePart)
                        {
                            ret = RCV3sV<DfcTree.IMatBomNodePosition[]>.Failed(new DfcTree.IMatBomNodePosition[] { },
                                                                                    classifyResultDescriptor(pnL.eFails(TT.ATMO.SAP.BomErrors.IncorrectlyClassifiedMaterial.UID)));
                        }
                        else
                        {
                            switch (bomPos.MatKlasse)
                            {
                                case MatClass.MechAssy:
                                case MatClass.ElectAssy:
                                    {
                                        var SubAssy = new DfcTree.AssyCoreInBomContext()
                                        {
                                            BomMatNo = assyMatNo,
                                            AssyName = bomPos.MaterialKurzText,
                                            CurrentBomPos = bomPos.PosNr,
                                            IsSparePart = bomPos.IstEVW,
                                            IsStandard = bomPos.StdBg,
                                            BomType = bomPos.MatKlasse == MatClass.ElectAssy ? DfcTree.BOMTypes.electricalBOM : DfcTree.BOMTypes.mechanicalBOM,
                                            AssyMatNo = bomPos.MatNr,
                                            MSTAE = bomPos.MSTAE
                                        };

                                        // Anreichern mit weiteren Details
                                        lstBomItems.Add(SubAssy);
                                    }
                                    break;
                                case MatClass.MechSinglePart:
                                case MatClass.ElectSinglePart:
                                    {
                                        var sp = new DfcTree.SinglePartInBomContext()
                                        {
                                            BomMatNo = assyMatNo,
                                            CurrentBomPos = bomPos.PosNr,
                                            CurrentBomPosQuantity = bomPos.Menge,
                                            IsSparePart = bomPos.IstEVW,
                                            BomType = bomPos.MatKlasse == MatClass.ElectSinglePart ? DfcTree.BOMTypes.electricalBOM : DfcTree.BOMTypes.mechanicalBOM,
                                            MSTAE = bomPos.MSTAE,
                                            SinglePartLup = bomPos.Lup,
                                            SinglePartMatNo = bomPos.MatNr,
                                            SinglePartName = bomPos.MaterialKurzText
                                        };

                                        lstBomItems.Add(sp);
                                    }
                                    break;
                                default:
                                    {
                                        ret = RCV3sV<DfcTree.IMatBomNodePosition[]>.Failed(null, pnL.eFails());
                                    }
                                    break;
                            }
                        }
                    }

                    ret = RCV3sV<DfcTree.IMatBomNodePosition[]>.Ok(lstBomItems.ToArray());
                }

            }
            catch (Exception ex)
            {
                ret = RCV3sV<DfcTree.IMatBomNodePosition[]>.Failed(new DfcTree.IMatBomNodePosition[] { }, pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex))));
            }

            return ret;
        }


        /// <summary>
        /// mko, 15.9.2020
        /// Für eine Materialnummer eines Priozessmoduls wird ein Prozessmodul- Objekt inklusive dem Kopf der mechanischen 
        /// und elektrischen SWtückliste erzeugt.
        /// </summary>
        /// <param name="pmMatNo"></param>
        /// <returns></returns>
        public RCV3sV<DfcTree.ProcessModuleSimple> GetProcessModule(string pmMatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);
            var ret = RCV3sV<DfcTree.ProcessModuleSimple>.Failed(value: null, pnL.eNotCompleted());

            DfcTree.ProcessModuleSimple pm = null;
            pm.ProcessmoduleMatNo = pmMatNo;

            try
            {
                // Prozessmodul einlesen

                var sql = new SQL<DfcTree.ProcessModuleSimple>();
                var tabMaraPj = new Tables.MaraPj();
                var tabStpoView = new Tables.STPOView602();

                int bomPosProcessMod = 0;

                var q = sql.Select(
                        sql.Map(tabMaraPj.PjNr.FQN, (bo, v) =>
                            bo.ProjectNo = ColTool.GetSave(v, 0)),
                        sql.Map(tabMaraPj.StatNr.FQN, (bo, v) =>
                            bo.StationNo = ColTool.GetSave(v, (short)0)),
                        sql.Map(tabStpoView.PosNr.FQN, (bo, v) =>
                            {
                                bo.ProcessModul = bomPosProcessMod++;                                
                                bo.CurrentDfcBomPos = ColTool.GetSave(v, (short)0);
                            }),
                        sql.Map(tabMaraPj.MatNr.FQN, (bo, v) =>
                            bo.ProcessmoduleMatNo = ColTool.GetSave(v, "")),
                        sql.Map(tabMaraPj.StlStatus.FQN, (bo, v) =>
                        {
                            var converter = new DfcTree.StringToBomStateConverter();
                            var getMatClass = converter.ToBomState(ColTool.GetSave(v, ""), pnL);
                            bo.SAPBomState = getMatClass.ValueOrException;

                            // Die beiden aus *SAPBomStatus* ausdifferenzierten Zusände *BomState*
                            // und *ProcurmentState* können im Falle eines Prozessmoduls
                            // vollständig aus dem SAPBomStatus hergeleitet werden, da das Prozessmodul 
                            // nur im Kontext eines einzigen Projektes gültig ist (Nicht mehrfachverbaut)
                            if(bo.SAPBomState == DfcTree.SAPBomStatus._01_Initialized
                            || bo.SAPBomState == DfcTree.SAPBomStatus._02_InProcessByExternal
                            || bo.SAPBomState == DfcTree.SAPBomStatus._03_CreatedByExternal
                            || bo.SAPBomState == DfcTree.SAPBomStatus._04_InProcessByInternal
                            || bo.SAPBomState == DfcTree.SAPBomStatus._05_CreatedByInternal)
                            {
                                bo.BomStatus = (DfcTree.BomStatus)bo.SAPBomState;
                                bo.ProcurementStatus = DfcTree.ProcurementStatus.NoCurrentOrders;
                            }
                            else
                            {
                                bo.BomStatus = DfcTree.BomStatus._05_CreatedByInternal;
                                bo.ProcurementStatus = (DfcTree.ProcurementStatus)bo.SAPBomState;
                            }
                        }),
                        sql.Map(tabStpoView.MaterialKurzText.FQN, (bo, v) =>
                            bo.ProcessmoduleName = ColTool.GetSave(v, ""))
                    )
                    .EqJoinFrom((tabMaraPj.MatNr, tabStpoView.MatNr))
                    .Where(sql.Eq(tabMaraPj.MatNr.FQN, sql.Txt(pmMatNo)))
                    .done();

                var getPm = GetRecord(q);

                if (!getPm.Succeeded)
                {
                    ret = RCV3sV<DfcTree.ProcessModuleSimple>.Failed(null, qRes.CreateQueryExecutionFailed(pnL.m(TT.Access.Fetch.UID,
                                                                                                            pnL.p_NID(TT.Grammar.Subject.UID, TT.ATMO.DFC.ProcessModul.UID),
                                                                                                            pnL.eFails(pnL.EncapsulateAsEventParameter(getPm.ToPlx())))));
                }
                else if (getPm.ValueOrException.IsEmpty)
                {
                    ret = RCV3sV<DfcTree.ProcessModuleSimple>.Failed(null, qRes.CreateQueryResultEmpty(pnL.i(TT.ATMO.DFC.ProcessModul.UID, pnL.p(TT.ATMO.DFC.MatNo.UID, pmMatNo))));
                }
                else
                {
                    pm = getPm.ValueOrException.Entity;

                    // Materialnummer des Projektes und der Station bestimmen

                    var prj = new Projects(pnL);

                    pm.ProjectMatNo = prj.GetMatNoForPSPNo(new DfcTree.Parser.PSPNo((DfcTree.IProjectNo)pm)).ValueOrException;
                    pm.StationMatNo = prj.GetMatNoForPSPNo(new DfcTree.Parser.PSPNo(pm)).ValueOrException;                                       

                    // elektrische  und Mechanische Stückliste einlesen

                    var getPmBoms = GetAssyBomItems(pmMatNo);

                    if (!getPmBoms.Succeeded)
                    {
                        // Abfrage ist aus technischen Gründen fehlgeschlagen
                        ret = RCV3sV<DfcTree.ProcessModuleSimple>.Failed(null, qRes.CreateQueryExecutionFailed(pnL.m(TT.Access.Fetch.UID,
                                                                                                                pnL.p_NID(TT.Grammar.Subject.UID, TT.ATMO.DFC.BOMPosList.UID),
                                                                                                                pnL.eFails(pnL.EncapsulateAsEventParameter(getPmBoms.ToPlx())))));
                    }
                    else if (!getPmBoms.ValueOrException.Any())
                    {
                        // Fehler, es muss eine mechanische oder eine elektrische Hauptbaugruppe vorhanden sein
                        ret = RCV3sV<DfcTree.ProcessModuleSimple>.Failed(null, qRes.CreateQueryExecutionFailed(pnL.m(TT.Access.Fetch.UID,
                                                                                        pnL.p_NID(TT.Grammar.Subject.UID, TT.ATMO.DFC.BOMPosList.UID),
                                                                                        pnL.eFails(pnL.i(TTD.MetaData.Details.UID,
                                                                                                    pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.ATMO.SAP.BomErrors.BomAtAtmoStructuralError.UID),
                                                                                                    pnL.p(TTD.StateDescription.Why.UID,
                                                                                                            pnL.m(TT.Operators.Boolean.AND.UID,
                                                                                                                   pnL.p(TT.Operators.Boolean.ItIsValid.UID, TT.ATMO.SAP.BomErrors.MainMechAssyMissing.UID),
                                                                                                                   pnL.p(TT.Operators.Boolean.ItIsValid.UID, TT.ATMO.SAP.BomErrors.MainElectAssyMissing.UID)
                                                                                            )))))));


                    }                    
                    else
                    {
                        var pmBoms = getPmBoms.ValueOrException;                            
                        

                        var MechBoms = pmBoms.Where(a => a.MatClassOfCurrentBomPos == MatClass.MechAssy || a.MatClassOfCurrentBomPos == MatClass.MechSinglePart);
                        var ElBoms = pmBoms.Where(a => a.MatClassOfCurrentBomPos == MatClass.ElectAssy || a.MatClassOfCurrentBomPos == MatClass.ElectSinglePart);

                        var MechBomsExists = MechBoms?.Any() ?? false;
                        if (MechBomsExists)
                        {   
                            pm.MainMechanicalAssemblies = MechBoms.Select(r => new DfcTree.MatBomNodePos(r)).ToArray();

                            // Details zur Mechanischen Ebene abrufen
                            //var me = GetMechanicalArea(pm.ProcessmoduleMatNo).ValueOrException;

                            //pm.MechanicalArea.MechanicalAreaName = me.MechanicalAreaName;
                            //pm.MechnicalAreaMatNo = me.

                        }

                        var ElBomsExists = ElBoms?.Any() ?? false;
                        if (ElBomsExists)
                        {
                            pm.MainElectricalAssemblies = ElBoms.Select(r => new DfcTree.MatBomNodePos(r)).ToArray();
                        }

                        // Warnungsmeldung, wenn nur eine oder beide Stücklisten für die Hauptbaugruppen fehlen
                        var msg =  pnL.IfElse(MechBomsExists && ElBomsExists,
                            () => pnL.eSucceeded(),
                            () => (IInstanceMember)pnL.eWarn(pnL.i(TTD.MetaData.Details.UID,
                                            pnL.KillIf(MechBomsExists && ElBomsExists, () => pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.ATMO.SAP.BomErrors.BomAtAtmoStructuralError.UID)),
                                            pnL.KillIf(MechBomsExists, () => pnL.p(TTD.StateDescription.Why.UID, TT.ATMO.SAP.BomErrors.MainMechAssyMissing.UID)),
                                            pnL.KillIf(ElBomsExists, () => pnL.p(TTD.StateDescription.Why.UID, TT.ATMO.SAP.BomErrors.MainElectAssyMissing.UID)))));


                        ret = RCV3sV<DfcTree.ProcessModuleSimple>.Ok(pm, msg);
                    }

                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<DfcTree.ProcessModuleSimple>.Failed(null, pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex))));
            }



            return ret;
        }

        /// <summary>
        /// mko, 20.9.2020
        /// List eine Assembly mit Grunddaten
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public Task<RCV3sV<DfcTree.AssyCore>> GetAssy(string MatNo)
        {
            return new Task<RCV3sV<DfcTree.AssyCore>>(() => {

                var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);
                var ret = RCV3sV<DfcTree.AssyCore>.Failed(value: null, pnL.eNotCompleted());

                try
                {
                    var sql = new SQL<DfcTree.AssyCore>(SQL.Dialect.Oracle);
                    var tab = new Tables.STPOView602();

                    var MSTAEConverter = new StringToMSTAEConverter();
                    var MatClassConverter = new StringToMatClassConverter();

                    var q = sql.Select(
                            sql.Map(tab.BGMatNr.FQN, (bo, v) => bo.AssyMatNo = ColTool.GetSave(v, "")),
                            sql.Map(tab.DokuHakenInitialwertBeiAnlage.FQN, (bo, v) => bo.IsRelevantForDocumentation = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, ""))),
                            sql.Map(tab.EVWInitialwertBeiAnlage.FQN, (bo, v) => bo.IsSparePart = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, ""))),
                            sql.Map(tab.MatArt.FQN, (bo, v) => bo.AssyMatNo = ColTool.GetSave(v, "")),
                            //sql.Map(tab.MatSprachCodeBenennung.FQN, (bo, v) => bo.MatSprachCodeBenennung = ColTool.GetSave(v, -1)),
                            sql.Map(tab.MaterialKurzText.FQN, (bo, v) => bo.AssyName = ColTool.GetSave(v, "")),
                            sql.Map(tab.MatKlasse.FQN, (bo, v) =>
                            {
                                var strMatKlasse = ColTool.GetSave(v, "").Trim().ToUpper();
                                var getMatClass = MatClassConverter.ToMatClass(strMatKlasse, pnL);
                                bo.BomType = getMatClass.Value == MatClass.ElectAssy ? DfcTree.BOMTypes.electricalBOM : DfcTree.BOMTypes.mechanicalBOM;
                            }),
                            sql.Map(tab.MatNr.FQN, (bo, v) => bo.AssyMatNo = ColTool.GetSave(v, "")),
                            sql.Map(tab.MSTAE.FQN, (bo, v) =>
                            {
                                var strMSTAE = ColTool.GetSave(v, "").Trim();
                                var getMSTAE = MSTAEConverter.ToMSTAE(strMSTAE, pnL);
                                bo.MSTAE = getMSTAE.Succeeded ? getMSTAE.Value : MSTAE.none;
                            }),
                            sql.Map(tab.StdBg.FQN, (bo, v) => bo.IsStandard = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, "")))
                            //sql.Map(tab.ZeichnungsNummer.FQN, (bo, v) => bo.ZeichnungsNummer = ColTool.GetSave(v, ""))
                        )
                        .From(tab)
                        .Where(sql.Eq(tab.BGMatNr.FQN, sql.Txt(MatNo)))
                        // Auf Sortierung im Oracle DB Server wird aus Performance- Gründen verzichtet. Kann auch 
                        // hier im Client erfolgen.
                        //.By(tab.PosNr)
                        .done();

                    var getRes = GetRecords(q);

                    if (!getRes.Succeeded)
                    {
                        ret = RCV3sV<DfcTree.AssyCore>.Failed(value: null, qRes.CreateQueryExecutionFailed(getRes.ToPlx()));
                    }
                    else if (getRes.Value.IsEmpty)
                    {
                        ret = RCV3sV<DfcTree.AssyCore>.Failed(value: null, qRes.CreateQueryResultEmpty());
                    }
                    else
                    {
                        // Einlesen der Stücklistenpositionen als Baugruppen oder Einzelteile
                    }
                }
                catch (Exception ex)
                {

                }

                return ret;
            });
        }

        /// <summary>
        /// mko, 20.9.2020
        /// List eine Assembly mit Sicherheitsmerkmalen
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public Task<RCV3sV<DfcTree.AssyWithSecurityFeatures>> GetAssyWithSecurityFeatures(string MatNo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// List eine Assembly und ihre Stücklistenpositionen
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public Task<RCV3sV<DfcTree.AssyCoreInBomContext>> GetAssyInBomContext(string bomMatNo, string assyMatNo)
        {
            throw new NotImplementedException();
        }

        //Task<RCV3sV<DfcTree.AssyWithBomItems>> DfcTree.IAssyRepository<DfcTree.AssyCore, DfcTree.AssyCoreInBomContext, DfcTree.AssyWithSecurityFeatures, DfcTree.AssyWithStatistics, DfcTree.AssyWithBomItems>.GetAssyBomItems(string MatNo)
        //{
        //    throw new NotImplementedException();
        //}


        public Task<RCV3sV<DfcTree.ElectricalArea>> GetElectricalArea(string MatNo)
        {
            throw new NotImplementedException();
        }
    }
}
