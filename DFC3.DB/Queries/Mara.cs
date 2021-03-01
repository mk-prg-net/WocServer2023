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
using TechTerms = ATMO.mko.Logging.PNDocuTerms.DocuEntities.Composer.TechTerms;

namespace DFC3.DB.Queries
{
    /// <summary>
    /// mko, 22.10.2018
    /// Abfragen auf den Mara- Tabellen
    /// </summary>
    public class Mara : QueriesBase
    {
        public Mara(IComposer pnL)
            : base(pnL) { }

        /// <summary>
        /// mko, 22.10.2018
        /// Ermittelt zu einer Materialnummer die Projekt und Stationsnummer
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public RCV3sV<Result<DFCTools.PSPNrParser.ProjectStationNo>> GetProjectStationNo(string MatNo)
        {

            var sql = new SQL<DFCTools.PSPNrParser.ProjectStationNo>();

            var q = sql.Select(
                    sql.Map(Tables.MaraPj._.PjNr, (bo, v) => bo.ProjectNo = ColTool.GetSave(v, 0)),
                    sql.Map(Tables.MaraPj._.StatNr, (bo, v) => bo.StationNo = ColTool.GetSave(v, (short)0))
                )
                .From(Tables.MaraPj._)
                .Where(
                    sql.And(
                        sql.StrEq(Tables.MaraPj._.MatNr, sql.Txt(MatNo)),

                        // Es gibt immer einen Eintrag mit Stationsnummer 0!
                        // dieser wird hier ausgeschlossen.
                        sql.NotEq(Tables.MaraPj._.StatNr, sql.Int(0))
                        ))
                .done();

            var res = GetRecord(q);

            return new RCV3sV<Result<DFCTools.PSPNrParser.ProjectStationNo>>(res);
        }


        public RCV3sV<Bo.MaraBo> GetMaraBo(string MatNo)
        {
            var qRes = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var ret = RCV3sV<Bo.MaraBo>.Failed(value: null, qRes.CreateQueryExecutionFailed(pnL.eNotCompleted()));

            try
            {
                var sql = new SQL<Bo.MaraBo>();
                var tab = new Tables.Mara();

                var q = sql.Select(
                            sql.Map(tab.MatNr, (bo, v) => bo.MatNr = ColTool.GetSave(v, "")),
                            sql.Map(tab.NodeType, (bo, v) => bo.NodeType = ColTool.GetSave(v, "")),
                            sql.Map(tab.MKlasse, (bo, v) => bo.MKlasse = ColTool.GetSave(v, "")),
                            sql.Map(tab.MTArt, (bo, v) => bo.MTArt = ColTool.GetSave(v, "")),
                            sql.Map(tab.MSTAE, (bo, v) => bo.MSTAE= ColTool.GetSave(v, "")),
                            sql.Map(tab.StdBg, (bo, v) => bo.StandardBaugruppe = ColTool.GetSave(v, "")),
                            sql.Map(tab.Verbaut, (bo, v) => bo.Verbaut = ColTool.GetSave(v, 0)),
                            sql.Map(tab.ZAT, (bo, v) => bo.ZAT = ColTool.GetSave(v, "")),
                            sql.Map(tab.ZeichungsNr, (bo, v) => bo.ZeichungsNr = ColTool.GetSave(v, ""))
                    )
                    .From(tab)
                    .Where(sql.Eq(tab.MatNr, sql.Txt(MatNo)))
                    .done();

                var getMara = GetRecord(q);

                if (!getMara.Succeeded)
                {
                    ret = RCV3sV<Bo.MaraBo>.Failed(value: null, qRes.CreateQueryExecutionFailed(getMara.ToPlx()));
                }
                else if (getMara.Succeeded && getMara.Value.IsEmpty)
                {
                    ret = RCV3sV<Bo.MaraBo>.Failed(value: null, qRes.CreateQueryResultEmpty());
                }
                else
                {
                    ret = RCV3sV<Bo.MaraBo>.Ok(getMara.Value.Entity);
                }

            }
            catch (Exception ex)
            {
                ret = RCV3sV<Bo.MaraBo>.Failed(value: null, qRes.CreateQueryExecutionFailed(TraceHlp.FlattenExceptionMessagesPN(ex)));
            }

            return ret;
        }

        /// <summary>
        /// mko, 28.6.2019
        /// Klassifiziert den Inhalt, der hinter einer Materialnummer steht.
        /// 
        /// mko, 29.9.2020
        /// Klassifizierung an neues Feld NodeType angepasst. Das Parsen der Materialklasse aus 
        /// NodeType und MKLASSE erfolgt nun mit dem einheitlichen Algorithmus in ATMO.DFC.Material.StringToMatClassConverter().
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public RCV3sV<MatClass> ClassifyMatNoContent(string MatNo) //, Composer pnL)
        {
            var ret = RCV3sV<MatClass>.Failed(MatClass.none, pnL.eNotCompleted());

            // 1. Prüfen, ob die Materialnummer für ein Projekt oder eine Station steht. Zugriff über Projektliste

            var sqlPrjs = new Queries.Projects(pnL, null);
            var retTest = sqlPrjs.IsMatNoOfProjectOrStation(MatNo);

            if (retTest.Succeeded)
            {
                if (retTest.Value.LevelType == ATMO.DFC.Tree.BomLevelType.Project)
                {
                    ret = RCV3sV<MatClass>.Ok(MatClass.Project);
                }
                else
                {
                    ret = RCV3sV<MatClass>.Ok(MatClass.Station);
                }

            }
            else if (pnL.ReturnSearchFailsEmptyResult().IsSubTreeOf(retTest.MessageEntity))
            {
                // 2. Prüfen, ob die Materialnummer eine DokuMat- Nummer ist

                var sqlMan = new Queries.ATMODocsSQL(pnL);

                var getDocuMat = sqlMan.GetDocuMat(MatNo);

                if (getDocuMat.Succeeded)
                {
                    ret = RCV3sV<MatClass>.Ok(MatClass.ManualDokuMat);
                }
                else if (pnL.ReturnSearchFailsEmptyResult().IsSubTreeOf(getDocuMat.MessageEntity))
                {
                    // 3. Wenn nicht 1. und 2. dann Liegt Baugruppe oder Einzelteil vor: Differenzierung über 
                    //    Mara- Tabelle vornehmen.

                    var sqlMara = new Mara(pnL);
                    var getMara = sqlMara.GetMaraBo(MatNo);

                    if (getMara.Succeeded)
                    {
                        var nType = getMara.Value.NodeType?.ToUpper() ?? "";
                        var mclass = getMara.Value.MKlasse?.ToUpper() ?? "";

                        mclass = nType == "" || nType == "XX" ? mclass : nType;

                        if (mclass == "") {
                            // mko, 5.11.2019
                            // Materialklasse ist nicht definiert -> Inkonsistenz
                            if (getMara.Value.MEINS == "BG")
                            {
                                ret = RCV3sV<MatClass>.Ok(MatClass.MechAssy);
                            }
                            else
                            {
                                // mko, 5.11.2019
                                // lt. Joachim muss jetzt geprüft werden, ob für die Materialnummer ein Stücklistenkopf definiert ist
                                // Wenn ja, dann handelt es sich um eine Baugruppe (Fälle, in denen die Materialnummer ein Projekt
                                // oder eine Station sind, für die auch ein Eintrag in STKO existiert, sind bereits oben abgefangen).

                                var bom = new Bom(pnL);

                                var getStko = bom.GetStKo(MatNo);

                                if (getStko.Succeeded)
                                {
                                    ret = RCV3sV<MatClass>.Ok(MatClass.MechAssy);
                                } else
                                {
                                    // mko, 5.11.2019
                                    // lt. Joachim wird im Fall, das keine Materialklasse explizit definiert wurde, und 
                                    // auch keine Baugruppe vorliegt, immer davon ausgegangen, dass ein mechanisches Einzelteil 
                                    // vorliegt.
                                    ret = RCV3sV<MatClass>.Ok(MatClass.MechSinglePart);
                                }
                            }
                        } else
                        {
                            var x = new StringToMatClassConverter();
                            var getMClass = x.ToMatClass(mclass, pnL);
                            if(getMClass.Succeeded && getMClass.ValueOrException != MatClass.none)
                            {
                                ret = RCV3sV<MatClass>.Ok(getMClass.ValueOrException);
                            }
                            else if (getMara.Value.MEINS == "BG")
                            {
                                //  MEINS = "Material Einheit für Stückzahl"
                                // BG = Baugruppe (Achtung: Konstrukteur kann nachträglich Baugruppen in Einzelteile umdeklarieren. Wenn
                                //                          aber eine Beschaffung lief, kann MEINS nicht mehr angepasst werden- so verbleibt
                                //                          dann MEINS auf BG, obwohl ST jetzt korrekt wäre.
                                //                          -: BG nur in letzter Instanz heranziehen bei der Materialnummernklassifizierung)
                                ret = RCV3sV<MatClass>.Ok(MatClass.MechAssy);
                            }
                            else
                            {
                                // Kann keiner Materialklasse zugeordnet werden
                                ret = RCV3sV<MatClass>.Ok(MatClass.none);
                            }
                        }
                    }
                    else if (pnL.eWarn(TechTerms.Storage.EmptySet).IsSubTreeOf(getMara.MessageEntity))
                    {
                        ret = RCV3sV<MatClass>.Failed(MatClass.none, pnL.ReturnSearchFailsEmptyResult());
                    }
                    else
                    {
                        ret = RCV3sV<MatClass>.Failed(MatClass.none, getMara.ToPlx());
                    }
                }
                else
                {
                    ret = RCV3sV<MatClass>.Failed(MatClass.none, getDocuMat.ToPlx());
                }
            }
            else
            {
                // Die Abfrage in der Projektliste2 ist aus technischen Gründen fehlgeschlagen. Die Bestimmung 
                // des Materialtypes mittels der anderen Tabellen fortzusetzen ist 
                ret = RCV3sV<MatClass>.Failed(MatClass.none, retTest.MessageEntity);
            }

            return ret;
        }
    }
}
