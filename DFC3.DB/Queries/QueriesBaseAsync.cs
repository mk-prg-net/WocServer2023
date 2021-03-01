using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.QueryBuilder;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;

using ATMO.mko.QueryBuilder.Results;

using TT = ATMO.DFC.Naming.TechTerms;
using TTD = ATMO.DFC.Naming.DocuTerms;


namespace DFC3.DB.Queries
{
    /// <summary>
    /// mko, 1.10.2020
    /// Asynchrone Variante von QueriesBase
    /// </summary>
    public class QueriesBaseAsync
    {
        protected IComposer pnL;
        protected PlxQueryResultDescriptionFactory plxResFactory;

        public QueriesBaseAsync(IComposer pnL)
        {
            this.pnL = pnL;
            plxResFactory = new PlxQueryResultDescriptionFactory(pnL);
        }

        /// <summary>
        /// Queries a set of records.
        /// 
        /// mko, 1.10.2020
        /// Asynchron formuliert.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qPath"></param>
        /// <returns></returns>
        protected async Task<RCV3sV<ResultSet<T>>> GetRecordsAsync<T>(QueryBuilderResult<T> qPath)
            where T : new()
        {
            var ret = RCV3sV<ResultSet<T>>.Failed(new ResultSet<T>(), pnL.ReturnNotCompleted("GetRecorsAsync", pnL.p(TT.Search.Filter.UID, qPath.QueryAsSql)));

            var ora = new global::DZA.OracleHelper.OracleSQLAsync();

            try
            {
                using (var reader = await ora.executeSQL(qPath.QueryAsSql))
                {
                    var res = new List<T>();
                    while (await reader.ReadAsync())
                    {
                        // mko, 18.6.2018
                        // StateNrSource as type save Enum

                        var docUserState = new T();
                        qPath.RecordToBoMapper.SetPropertiesOf(docUserState, reader);
                        res.Add(docUserState);
                    }
                    ret = RCV3sV<ResultSet<T>>.Ok(new ResultSet<T>(res));
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<ResultSet<T>>.Failed(null, TraceHlp.FlattenExceptionMessagesPN(ex));
            }
            finally
            {
                ora.CloseOraConnection();
            }
            return ret;
        }

        /// <summary>
        /// mko, 13.7.2018
        /// queries a single record.
        /// 
        /// mko, 1.10.2020
        /// Asynchron formuliert.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qPath"></param>
        /// <returns></returns>
        protected async Task<RCV3sV<Result<T>>> GetRecordAsync<T>(QueryBuilderResult<T> qPath)
            where T : new()
        {

            var ret = RCV3sV<Result<T>>.Failed(new Result<T>(), pnL.ReturnNotCompleted("GetRecordAsync", pnL.p(TT.Search.Filter.UID, qPath.QueryAsSql)));

            var ora = new global::DZA.OracleHelper.OracleSQLAsync();

            try
            {
                using (var reader = await ora.executeSQL(qPath.QueryAsSql))
                {
                    if (await reader.ReadAsync())
                    {
                        // mko, 18.6.2018
                        // StateNrSource as type save Enum

                        var res = new T();
                        qPath.RecordToBoMapper.SetPropertiesOf(res, reader);
                        ret = RCV3sV<Result<T>>.Ok(new Result<T>(res));
                    }
                    else
                    {
                        ret = RCV3sV<Result<T>>.Ok(
                            value: new Result<T>(), 
                            Message: pnL.ReturnFetchWarnEmptySet(TT.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID, TT.Access.Datasources.WellKnown.File.UID, pnL.txt(qPath.QueryAsSql)));
                    }
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<Result<T>>.Failed(new Result<T>(), TraceHlp.FlattenExceptionMessagesPN(ex));
            }
            finally
            {
                ora.CloseOraConnection();
            }
            return ret;
        }


        /// <summary>
        /// mko, 17.9.2018
        /// Executes an insert or update query
        /// 
        /// mko, 1.10.2020
        /// Asynchron formuliert.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qPath"></param>
        /// <returns></returns>
        protected async Task<RCV3> ExecuteDMLAsync<T>(QueryBuilderResult<T> qPath)
            where T : new()
        {
            var ret = RCV3.Failed();

            var ora = new global::DZA.OracleHelper.OracleSQLAsync();

            try
            {
                bool success = await ora.executeSQLInsert(qPath.QueryAsSql);
                ret = RCV3.Ok();
            }
            catch (Exception ex)
            {
                ret = RCV3.Failed(ex);
            }
            finally
            {
                ora.CloseOraConnection();
            }
            return ret;
        }


    }
}
