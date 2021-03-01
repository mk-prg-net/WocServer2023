using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using ATMO.mko.QueryBuilder;
using ColTool = DFC3.DB.Tools.TabColAccess;

using ATMO.DFC.Material;
using ATMO.DFC.Tree;
using TechTerms = ATMO.mko.Logging.PNDocuTerms.DocuEntities.Composer.TechTerms;

namespace DFC3.DB.Queries
{
    public class MaraPj
        : QueriesBase
    {
        //
        //

        public MaraPj(IComposer pnL)
            : base(pnL) { }

        /// <summary>
        /// mko, 16.4.2020
        /// Lädt die bewerteten Merkmale einer Baugruppe wie z.B. Länge eines Transportbandes. 
        /// Wegen dem Bom@Atmo Eindeutigkeitsaxiom für Baugruppen sind die bewerteten Merkmale direkt der 
        /// Materialnummer der Baugruppe zugeordnet. Im Prinzip ist die Zuordnung der pspBom nicht notwendig.
        /// Siehe auch https://inside-docupedia.bosch.com/confluence/x/kywNSw    
        /// </summary>
        /// <param name="pspBom"></param>
        /// <param name="MatNoOfAssy"></param>
        /// <returns></returns>
        public RCV3sV<ResultSet<CharacteristicValue>> GetCharacteristicValuesForAssy(IPSPBom pspBom, IMatBomNodePosition bomPosOfAssy)
        {
            var ret = RCV3sV<ResultSet<CharacteristicValue>>.Failed(value: null, ErrorDescription: pnL.eNotCompleted());

            var sql = new SQL<Bo.StringObj>();
            var tabMaraPj = new Tables.MaraPj();

            var q = sql.Select(
                    sql.Map(tabMaraPj.CV, (bo, v) => bo.Value = ColTool.GetSave(v, ""))
                )
                .From(tabMaraPj)
                .Where(                    
                        sql.And(
                        sql.Eq(tabMaraPj.PjNr, sql.Int(pspBom.ProjectNo)),
                        sql.Eq(tabMaraPj.StatNr, sql.Int(pspBom.StationNo)),
                        sql.StrEq(tabMaraPj.MatNr, sql.Txt(bomPosOfAssy.MatNoOfCurrentBomPos)))               

                )
                .done();

            var getCV = GetRecord<Bo.StringObj>(q);            

            ret = CreateCV(pspBom, bomPosOfAssy, getCV);

            return ret;
        }



        /// <summary>
        /// mko, 30.1.2020
        /// Lädt die bewerteten Merkmale eines Einzelteils, wie z.B. die Länge eines Hydraulikschlauches in einer Ventilbaugruppe.
        /// Die Bewertungen eines einzelteils sind abhängig vom Projekt (deshalb die pspBom), der Baugruppe, in welcher sie verbaut sind
        /// (MatNo:Bg) und der Stücklistenposition (BomPos.Pos.
        /// Siehe auch https://inside-docupedia.bosch.com/confluence/x/kywNSw    
        /// 
        /// </summary>
        /// <param name="pspBom">Projekt, Station und Prozessmodul, in welchem das Einzelteil eingesetzt wird</param>
        /// <param name="MatNoOfAssy">Materialnummer der Baugruppe, die das Einzelteil mit bewerteten Merkmalen enthält</param>
        /// <param name="bomPosOfSinglePartInsideAssy">Materialnummer und Stücklistenposition des Einzelteils mit den bewerteten Merkmalen innerhalb der Baugruppe</param>
        /// <returns></returns>           
        public RCV3sV<ResultSet<CharacteristicValue>> GetCharacteristicValuesForSingelPart(IPSPBom pspBom, string MatNoOfAssy, IMatBomNodePosition bomPosOfSinglePartInsideAssy)
        {
            var ret = RCV3sV<ResultSet<CharacteristicValue>>.Failed(value: null, ErrorDescription: pnL.eNotCompleted());

            var sql = new SQL<Bo.StringObj>();
            var tabMaraPj = new Tables.MaraPj();

            var q = sql.Select(
                    sql.Map(tabMaraPj.CV, (bo, v) => bo.Value = ColTool.GetSave(v, ""))
                )
                .From(tabMaraPj)
                .Where(
                        sql.And(
                        sql.Eq(tabMaraPj.PjNr, sql.Int(pspBom.ProjectNo)),
                        sql.Eq(tabMaraPj.StatNr, sql.Int(pspBom.StationNo)),
                        sql.StrEq(tabMaraPj.CVBG, sql.Txt(MatNoOfAssy)),

                        // mko, 4.2.20
                        sql.StrEq(tabMaraPj.MatNr, sql.Txt(bomPosOfSinglePartInsideAssy.MatNoOfCurrentBomPos)),

                        // mko, 4.2.2020
                        // Einschränkung auf CurrentBomPos ausgeschlossen, da das Feld nicht aktuell
                        // zu sein scheint (Tatsächliche Positionsnummern im Baum entsprechen nicht der 
                        // in MaraPj gepflegten. Z.B. M.3000420.10 (@.3842999895) MaraPJ.CVPOS=101, DFCTree.BomPos=105                
                        // 
                        // mko, 6.4.2020
                        // Die CVPOS ist doch entscheidend! Siehe M.8010536.560 (@.3842999895) .118 .119 
                        sql.Eq(tabMaraPj.CVPOS, sql.Int(bomPosOfSinglePartInsideAssy.CurrentBomPos)))
                )
                .done();

            var getCV = GetRecord<Bo.StringObj>(q);

            ret = CreateCV(pspBom, bomPosOfSinglePartInsideAssy, getCV);

            return ret;
        }

        /// <summary>
        /// mko, 16.4.2020
        /// </summary>
        /// <param name="pspBom"></param>
        /// <param name="bomPos"></param>
        /// <param name="getCV"></param>
        /// <returns></returns>
        private RCV3sV<ResultSet<CharacteristicValue>> CreateCV(
            IPSPBom pspBom, IMatBomNodePosition bomPos, 
            RCV3WithValue<RCV3, Result<Bo.StringObj>> getCV)
        {
            RCV3sV<ResultSet<CharacteristicValue>> ret;
            if (!getCV.Succeeded)
            {
                ret = RCV3sV<ResultSet<CharacteristicValue>>.Failed(
                    value: null,
                    ErrorDescription: plxResFactory.CreateQueryExecutionFailed(getCV.ToPlx()));
            }
            else if (getCV.Succeeded && getCV.Value.IsEmpty)
            {
                // Keine Einträge gefunden
                ret = RCV3sV<ResultSet<CharacteristicValue>>.Ok(
                        value: new ResultSet<CharacteristicValue>(),
                        Message: plxResFactory.CreateQueryResultEmpty()
                    );
            }
            else
            {
                // Einträge gefunden
                // Charakteristische Werte Parsen
                var getCVAttributeValues = ATMO.DFC.DB.Tools.InfosParser.GetInAttributeValuesSeparatedList(getCV.Value.Entity.Value, pnL, "=");

                if (!getCVAttributeValues.Succeeded)
                {
                    // Parserfehler
                    ret = RCV3sV<ResultSet<CharacteristicValue>>.Failed(
                        value: null,
                        ErrorDescription: plxResFactory.CreateQueryExecutionFailed(getCVAttributeValues.ToPlx()));
                }
                else
                {
                    // Umformatieren in eine Liste aus CharacteristicValue- Objekten
                    ret = RCV3sV<ResultSet<CharacteristicValue>>.Ok
                        (
                            new ResultSet<CharacteristicValue>(
                                getCVAttributeValues.Value
                                                    .Select(r => new CharacteristicValue(pspBom, bomPos, r.AttributeName, r.AttributeValue)))
                        );
                }
            }

            return ret;
        }
    }
}
