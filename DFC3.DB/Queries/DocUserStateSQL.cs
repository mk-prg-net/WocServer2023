using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.QueryBuilder;
using DFC3.DB.Bo;
using DFC3.DB.Tables.DZA;
using static DZAUtilities_Dictionaries.GlobalDictionaries;
using DFCObjects.Common.Workflow;

using ColTool = DFC3.DB.Tools.TabColAccess;

namespace DFC3.DB.Queries
{
    /// <summary>
    /// mko, 21.6.2018
    /// Queries userstate of a document
    /// </summary>
    public class DocUserStateSQL
    {
        /// <summary>
        /// mko, 21.6.2018
        /// Gets the current user state of a document
        /// </summary>
        /// <param name="DocId"></param>
        /// <returns></returns>
        public static IRCV2<DocUserstate> GetUserState(long DocId)
        {
            var (sql, select) = Select();
            var ret =  exe(select.Where(sql.Eq(Tables.Path._.DocId, sql.Long(DocId))).done());
            if (ret.Succeeded)
            {
                if (ret.Value.Any())
                {
                    return RCV2<DocUserstate>.Ok(ret.Value.First());
                } else
                {
                    return RCV2<DocUserstate>.Failed(ErrorDescription: $"No document found for DocId {DocId}");
                }
            } else
            {
                return RCV2<DocUserstate>.Failed(inner: ret, ErrorDescription: $"No document found for DocId {DocId}");
            }
        }

        /// <summary>
        /// mko, 21.6.2018
        /// Get userstates of all ducuments associated with a MatNo.
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public static IRCV2<IEnumerable<DocUserstate>> GetUserStatesFor(string MatNo)
        {
            var (sql, select) = Select();
            return exe(select.Where(sql.Eq(Tables.Path._.MatNr, sql.Txt(MatNo))).done());
        }

        /// <summary>
        /// mko, 21.6.2018
        /// Creates select clause
        /// </summary>
        /// <returns></returns>
        static (SQL<DocUserstate>, WhereBuilder<DocUserstate>) Select()
        {            
            var sql = new SQL<DocUserstate>();
            return (sql, sql.Select(
                    sql.Map(Tables.Path._.DocId, (bo, v) => 
                        bo.DocId = ColTool.GetSave(v, 0L)),
                    sql.Map(Tables.Path._.MatNr, (bo, v) => 
                        bo.MatNo = ColTool.GetSave(v, "")),
                    sql.Map(Tables.Path._.XType, (bo, v) => 
                        bo.DocType = (DocTypeSAP)Enum.Parse(typeof(DocTypeSAP), ColTool.GetSave(v, "unknown"), true)),
                    sql.Map(Tables.Path._.UserState, (bo, v) => 
                        bo.UserState = (DfcDocStates)ColTool.GetSave(v, 0)),
                    sql.Map(Tables.Path._.StatusChangeOriginator, (bo, v) => 
                        bo.StatusChangeOriginator = ColTool.GetSave(v, ""))
                )
                .From(Tables.Path._));            
        }

        /// <summary>
        /// mko, 21.6.2018
        /// Executes query
        /// </summary>
        /// <param name="qPath"></param>
        /// <returns></returns>
        static IRCV2<IEnumerable<DocUserstate>> exe(QueryBuilderResult<DocUserstate> qPath) {

            IRCV2<IEnumerable<DocUserstate>> ret = RCV2<IEnumerable<DocUserstate>>.Failed();
            
            var ora = new global::DZA.OracleHelper.OraSQL();

            try
            {
                using (var reader = ora.executeSQL(qPath.QueryAsSql))
                {
                    var res = new List<DocUserstate>();
                    while (reader.Read())
                    {
                        // mko, 18.6.2018
                        // StateNrSource as type save Enum

                        var docUserState = new DocUserstate();
                        qPath.RecordToBoMapper.SetPropertiesOf(docUserState, reader);
                        res.Add(docUserState);                        
                    }
                    ret = RCV2<IEnumerable<DocUserstate>>.Ok(res);
                }
            }
            catch (Exception ex)
            {
                ret = RCV2<IEnumerable<DocUserstate>>.Failed(ex);
            }
            finally
            {
                ora.CloseOraConnection();
            }
            return ret;
        }

    }
}
