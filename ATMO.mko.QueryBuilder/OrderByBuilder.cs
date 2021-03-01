using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.QueryBuilder
{
    /// <summary>
    /// mko, 23.1.2018
    /// </summary>
    public class OrderByBuilder<TBo>
    {
        public OrderByBuilder(string query, RecordToBoMapper<TBo> Mapper)
        {
            bld.Append(query);
            this.Mapper = Mapper;
        }

        RecordToBoMapper<TBo> Mapper { get; }
        System.Text.StringBuilder bld = new StringBuilder();
        bool first = true;

        public OrderByBuilder<TBo> By(IColXpr col)
        {
            if (first)
            {
                bld.Append($" ORDER BY {col.Value}");
                first = false;
            }
            else
            {
                bld.Append($", {col.Value}");
            }

            return this;
        }

        public OrderByBuilder<TBo> ByDescending(IColXpr col)
        {
            if (first)
            {
                bld.Append($" ORDER BY {col.Value} desc");
                first = false;
            }
            else
            {
                bld.Append($", {col.Value} desc");
            }

            return this;
        }

        public QueryBuilderResult<TBo> done()
        {
            return new QueryBuilderResult<TBo>(bld.ToString(), Mapper);
        }

    }
}
