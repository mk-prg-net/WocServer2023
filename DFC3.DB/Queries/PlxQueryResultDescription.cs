using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;

namespace DFC3.DB.Queries
{

    /// <summary>
    /// mko, 25.10.2018
    /// Einheitliche Beschreibung von Abfrageergebnissen
    /// </summary>
    public class _PlxQueryResultDescriptionFactory
    {
        public const string iQuery = "query";
        public const string mQueryExec = "exec";
        public const string iQueryResult = "result";
        public const string pQueryResultCount = "count";

        IComposer pnL;

        public _PlxQueryResultDescriptionFactory(IComposer pnL)
        {
            this.pnL = pnL;
        }

        /// <summary>
        /// mko, 25.10.2018
        /// Beschreibt Fehler, die eine Abfrage scheitern ließen
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public IDocuEntity CreateQueryExecutionFailed(IDocuEntity description)
        {
            return pnL.i(iQuery, 
                            pnL.m(mQueryExec, 
                                    pnL.ret(pnL.eFails(description))));
        }


        /// <summary>
        /// mko, 25.10.2018
        /// Eine Abfrage verlief erfolgreich, jedoch lieferte sie ein leeres Resultset.
        /// </summary>
        /// <returns></returns>
        public IDocuEntity CreateQueryResultEmpty()
        {
            return pnL.i(iQuery, 
                            pnL.m(mQueryExec, pnL.ret(pnL.eSucceeded())), 
                            pnL.i(iQueryResult, 
                                    pnL.p(pQueryResultCount, 
                                            pnL.txt("0")), 
                                            pnL.eWarn(pnL.txt("empty"))));
        }


        /// <summary>
        /// mko, 25.10.2018
        /// Eine Abfrage verlief erfolgreich. Es werden statistische Informationen geliefert.
        /// </summary>
        /// <param name="countResultsetRows"></param>
        /// <returns></returns>
        public IDocuEntity CreateQueryResultOk(long countResultsetRows, IDocuEntity details = null)
        {
            return pnL.i(iQuery, 
                            pnL.m(mQueryExec, pnL.ret(pnL.eSucceeded())), 
                            pnL.i(iQueryResult, 
                                    pnL.p(pQueryResultCount, 
                                            pnL.txt(countResultsetRows.ToString())),
                                            pnL.KillIf(countResultsetRows == 0, () => pnL.eSucceeded()),
                                            pnL.KillIf(countResultsetRows > 0, () => pnL.eWarn(pnL.txt("empty"))),
                                            pnL.KillIf(details == null, () => details)));
        }


    }
}
