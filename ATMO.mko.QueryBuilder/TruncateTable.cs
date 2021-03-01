using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.NaLisp.Core;

namespace ATMO.mko.QueryBuilder
{
    /// <summary>
    /// mko, 11.2.2020
    /// </summary>
    /// <typeparam name="TBo"></typeparam>
    public class TruncateTable<TBo>
    {
        /// <summary>
        /// Erzeugt einen SQL- Truncate Befehl
        /// </summary>
        /// <param name="_Evaluator">NaLisp- Baum Evaluierer</param>
        /// <param name="_Inspector">NaLisp- Baum Konsitenzprüfer</param>
        public TruncateTable()
        {
        }

        /// <summary>
        /// Erzeugt das Truncate Table Kommando
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public QueryBuilderResult<TBo> From(Table tab)
        {
            return new QueryBuilderResult<TBo>($"TRUNCATE TABLE {tab.TableName}");
        }


    }
}
