using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.CSSQL
{
    /// <summary>
    /// Result of succesful executed querybuilder
    /// </summary>
    /// <typeparam name="TBo"></typeparam>
    public interface IQueryBuilderResult<TBo>
    {
        string QueryAsSql { get; }

        RecordToBoMapper<TBo> RecordToBoMapper { get; }
    }

    public class QueryBuilderResult<TBo> : IQueryBuilderResult<TBo>
    {
        internal QueryBuilderResult(string sql, RecordToBoMapper<TBo> mapper)
        {
            QueryAsSql = sql;
            RecordToBoMapper = mapper;
        }

        internal QueryBuilderResult(string sql)
        {
            QueryAsSql = sql;
        }

        public string QueryAsSql { get; }

        public RecordToBoMapper<TBo> RecordToBoMapper { get; }
    }
}
