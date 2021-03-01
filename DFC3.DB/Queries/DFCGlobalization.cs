using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using static ATMO.mko.Logging.PNDocuTerms.DocuEntities.DocuEntityHlp;

using ATMO.mko.QueryBuilder;


using ColTool = DFC3.DB.Tools.TabColAccess;
using DFCObjects.Common.Prj;

using static DFCSecurity.SitesExt;

using TechTerms = ATMO.mko.Logging.PNDocuTerms.DocuEntities.Composer.TechTerms;



namespace DFC3.DB.Queries
{

    /// <summary>
    /// mko, 2.5.2019
    /// DFC- Globalisierung: Verwaltung mehrsprachiger Listen mit Fachwörtern (Sprachtabellen/Dictionaries) etc.
    /// </summary>
    public class DFCGlobalization : QueriesBase
    {

        public DFCGlobalization(IComposer pnL): base(pnL)
        {

        }

        /// <summary>
        /// mko, 2.5.2019
        /// ruft den nächsten freien BCODE ab.
        /// </summary>
        /// <returns></returns>
        public RCV3sV<int> NextBCODE()
        {
            var ret = RCV3sV<int>.Failed(value: 0, ErrorDescription: pnL.eNotCompleted());

            var sql = new SQL<Bo.DecimalObj>();
            var tab = new Tables.STB();

            var bcode = new Bo.DecimalObj();
            var q = sql.Select(sql.Map(sql.Max(tab.BCODE), 
                                (bo, v) => 
                                    bo.Value = ColTool.GetSave(v, -1m)))
                       .From(tab)
                       .Where(sql.And(sql.Gt(tab.BCODE, sql.Long(40150)), sql.Lt(tab.BCODE, sql.Int(100000))))
                       .done();

            var res = GetRecord(q);

            if (!res.Succeeded || res.Value.IsEmpty)
            {
                ret = RCV3sV<int>.Failed(value: 0, ErrorDescription: pnL.ReturnFetchWithDetails(false, pnL.txt(tab.TableName), pnL.EncapsulateAsEventParameter(res.ToPlx())));
            }            
            else
            {
                ret = RCV3sV<int>.Ok(value: (int)res.Value.Entity.Value + 1);
            }

            return ret;

        }
    }
}
