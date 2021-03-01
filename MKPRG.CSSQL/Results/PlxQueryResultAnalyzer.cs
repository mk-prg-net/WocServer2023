using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKPRG.Tracing;
using MKPRG.Tracing.DocuTerms;
using static MKPRG.Tracing.DocuTerms.DocuEntityHlp;

using ANC = MKPRG.Naming;

namespace MKPRG.CSSQL.Results
{
    /// <summary>
    /// mko, 25.10.2018
    /// 
    /// mko, 6.7.2020
    /// Massives Refactoring auf Basis streng typisierter SubTree Algorithmen.
    /// </summary>
    public class PlxQueryResultAnalyzer
    {
        public PlxQueryResultAnalyzer(IComposer pnL, IDocuEntity plxResult)
        {
            // Die Ergebnisbeschreibung einer Abfrage aus einem DokuTerm isolieren.
            var getSub = pnL.ReturnSearch().AsSubTreeOf(plxResult, pnL);

            // Annahme: die Ergebnisbeschreibung muss existieren, sonst Fehler.
            TraceHlp.ThrowArgExIf(
                !getSub.Succeeded,
                pnL.ReturnFetchWithDetails(
                    false,
                    pnL.EncapsulateAsPropertyValue(plxResult),
                    pnL.i(ANC.DocuTerms.MetaData.Details.UID, pnL.p(ANC.DocuTerms.StateDescription.WhatsUp.UID, pnL.txt("Structure of QueryResult is invalid: root missing."))),
                    pnL.i(ANC.DocuTerms.StateDescription.FinStateDescr.UID,
                            pnL.m(ANC.TechTerms.Search.Search.UID))
                ));

            // Wurzel der Ergebnisbeschreibung
            var root = (IInstance)getSub.Value.subTree;

            ExecFails = pnL.ReturnSearchExecutionFails().IsSubTreeOf(root);                        

            if (ExecFails)
            {
                // Abfrage konnte nicht korrekt ausgeführt werden, kein Ergebnis wurde zurückgeliefert.

                // Menge der Nutzergebnisse wird als leer ausgewiesen.
                EmptyResultset = true;


                var getSearchExecutionFails = pnL.ReturnSearchExecutionFails().AsSubTreeOf(root, pnL);
                TraceHlp.ThrowArgExIfNot(getSearchExecutionFails.Succeeded, pnL.ReturnFetch(false, root, pnL.ReturnSearchExecutionFails()));

                ExecMsg = (IInstance)getSearchExecutionFails.ValueOrException.subTree;
            }
            else 
            {
                ExecMsg = root;

                // Gesamte Ergebnisbeschreibung isolieren 

                var getSearchResult = pnL.SearchResult().AsSubTreeOf(root, pnL);
                TraceHlp.ThrowArgExIfNot(getSearchResult.Succeeded, pnL.ReturnFetch(false, root, pnL.SearchResult()));

                ResultSetDescription = (IInstance)getSearchResult.ValueOrException.subTree;


                // Prüfen, ob eine Leere Menge zurückgegeben wurde. In einigen Kontexten wird bei Rückgabe einer leeren Menge lediglich gewarnt (eFails),
                // in anderen stellt sie einen Fehler dar.
                EmptyResultset = pnL.ReturnSearchFailsEmptyResult().IsSubTreeOf(root);
                EmptyResultset |= pnL.ReturnSearchWarnEmptyResult().IsSubTreeOf(root);

                if (!EmptyResultset)
                {
                    // Fall: Die Ergebnismange ist nicht leer
                    // Anzahl der gefundenen Datensätze aus DokuTerm auslesen

                    var getCount = pnL.p(ANC.TechTerms.Metrology.Counter.UID, pnL._()).AsSubTreeOf(ResultSetDescription, pnL);
                    TraceHlp.ThrowArgExIfNot(getCount.Succeeded, pnL.ReturnFetch(false, ResultSetDescription, pnL.SearchResult()));

                    var prop = (IProperty)getCount.ValueOrException.subTree;
                    Count = prop.PropertyValue.AsLinq().LongVal ?? 0;
                }
                else
                {
                    // Fall: Die Ergebnismenge ist leer
                    Count = 0;
                    FailedBecauseResultIsEmptySet = pnL.ReturnSearchFailsEmptyResult().IsSubTreeOf(root);
                }
            }

        }



        /// <summary>
        /// False, if Execution of Query fails
        /// </summary>
        public bool ExecFails
        {
            get;
        }

        /// <summary>
        /// Message part, describing success of query execution.
        /// </summary>
        public IInstance ExecMsg
        {
            get;
        }

        /// <summary>
        /// True, if result set is empty
        /// </summary>
        public bool EmptyResultset
        {
            get;
        }

        /// <summary>
        /// True, if empty result set is reported inside an eFails. Otherwise false.
        /// </summary>
        public bool FailedBecauseResultIsEmptySet
        {
            get;
        }

        /// <summary>
        /// Count of found Recordsets.
        /// </summary>
        public long Count
        {
            get;
        }

        /// <summary>
        /// Statistics and Details/Metadata about resultset
        /// </summary>
        public IInstance ResultSetDescription
        {
            get;
        }



    }
}
