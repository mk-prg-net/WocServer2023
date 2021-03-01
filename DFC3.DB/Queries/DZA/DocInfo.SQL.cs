using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.QueryBuilder;
using DFC3.DB.Bo;
using DFC3.DB.Tables.DZA;

namespace DFC3.DB.Queries.DZA
{
    public class DocInfoSQL 
    {
        /// <summary>
        /// mko, 18.6.2018
        /// For given docId proc returns associated business object with doc infos.
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public static RCV2<DocInfoBo> GetDocInfo(long docId)
        {
            var res = RCV2<DocInfoBo>.Failed();
            try
            {
                var sql = new SQL<DocInfoBo>();
                var tab = new DocInfoTab();

                var query = sql.Select(
                        sql.Map(tab.ID, (bo, v) => bo.DocId = (long)v),
                        sql.Map(tab.UserState, (bo, v) => bo.UserState = (DZAUtilities_Dictionaries.GlobalDictionaries.DfcDocStates)(int)v),
                        sql.Map(tab.NrLayers, (bo, v) => bo.NrLayers = (int)v),
                        sql.Map(tab.InfoText, (bo, v) => bo.InfoText = (string)v)
                    )
                    .From(tab)
                    .Where(sql.Eq(tab.ID, sql.Long(docId)))
                    .done();

                var ora = new global::DZA.OracleHelper.OraSQL();

                using (var reader = ora.executeSQL(query.QueryAsSql))
                {
                    if (reader.Read())
                    {
                        var bo = new DocInfoBo();
                        query.RecordToBoMapper.SetPropertiesOf(bo, reader);

                        res = RCV2<DocInfoBo>.Ok(bo);
                    }
                }
            }
            catch (Exception ex)
            {
                res = RCV2<DocInfoBo>.Failed(ex);
            }
            return res;
        }

        /// <summary>
        /// mko, 15.6.2018
        /// queries for a given DocId the NrLayers
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public static RCV2<int> GetNrLayerForDocId(long docId)
        {
            var res = RCV2<int>.Failed();
            try
            {
                var sql = new SQL<DocInfoBo>();
                var tab = new DocInfoTab();

                var query = sql.Select(
                        sql.Map(tab.ID, (bo, v) => bo.DocId = (long)v),
                        sql.Map(tab.NrLayers, (bo, v) => bo.NrLayers = (int)v)
                    )
                    .From(tab)
                    .Where(sql.Eq(tab.ID, sql.Long(docId)))
                    .done();

                var ora = new global::DZA.OracleHelper.OraSQL();

                using (var reader = ora.executeSQL(query.QueryAsSql))
                {
                    if (reader.Read())
                    {
                        var bo = new DocInfoBo();
                        query.RecordToBoMapper.SetPropertiesOf(bo, reader);

                        res = RCV2<int>.Ok(bo.NrLayers);
                    }
                }
            }
            catch (Exception ex)
            {
                res = RCV2<int>.Failed(ex);
            }
            return res;
        }
    }
}
