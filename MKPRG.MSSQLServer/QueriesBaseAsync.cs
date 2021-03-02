using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKPRG.Tracing;
using MKPRG.CSSQL;
using MKPRG.Tracing.DocuTerms;

using MKPRG.CSSQL.Results;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;


namespace MKPRG.MSSQLServer
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

        public string connectionString;

        /// <summary>
        /// Queries a set of records.
        /// 
        /// mko, 1.10.2020
        /// Asynchron formuliert.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qPath"></param>
        /// <returns></returns>
        protected async Task<RC<ResultSet<T>>> GetRecordsAsync<T>(QueryBuilderResult<T> qPath)
            where T : new()
        {
            var ret = RC<ResultSet<T>>.Failed(new ResultSet<T>(), ErrorDescription: pnL.ReturnNotCompleted("GetRecorsAsync", pnL.p(TT.Search.Filter.UID, qPath.QueryAsSql)));

            var sqlSrv = new ProviderAsync(pnL, connectionString);

            try
            {
                var getDataReader = await sqlSrv.executeSQL(qPath.QueryAsSql);
                if (!getDataReader.Succeeded)
                {
                    ret = RC<ResultSet<T>>.Failed(new ResultSet<T>(), ErrorDescription: getDataReader.ToPlx());
                }
                else
                {
                    using (var reader = getDataReader.Value)
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
                        ret = RC<ResultSet<T>>.Ok(new ResultSet<T>(res));
                    }
                }

            }
            catch (Exception ex)
            {
                ret = RC<ResultSet<T>>.Failed(null, ErrorDescription: TraceHlp.FlattenExceptionMessagesPN(ex));
            }
            finally
            {
                sqlSrv.CloseConnection();
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
        protected async Task<RC<Result<T>>> GetRecordAsync<T>(QueryBuilderResult<T> qPath)
            where T : new()
        {

            var ret = RC<Result<T>>.Failed(new Result<T>(), ErrorDescription: pnL.ReturnNotCompleted("GetRecordAsync", pnL.p(TT.Search.Filter.UID, qPath.QueryAsSql)));

            var sqlSrv = new ProviderAsync(pnL, connectionString);

            try
            {
                var getDataReader = await sqlSrv.executeSQL(qPath.QueryAsSql);
                if (!getDataReader.Succeeded)
                {
                    ret = RC<Result<T>>.Failed(new Result<T>(new T()), ErrorDescription: getDataReader.ToPlx());
                }
                else
                {
                    using (var reader = getDataReader.Value)
                    {
                        if (await reader.ReadAsync())
                        {
                            // mko, 18.6.2018
                            // StateNrSource as type save Enum

                            var res = new T();
                            qPath.RecordToBoMapper.SetPropertiesOf(res, reader);
                            ret = RC<Result<T>>.Ok(new Result<T>(res));
                        }
                        else
                        {
                            ret = RC<Result<T>>.Ok(
                                value: new Result<T>(),
                                Message: pnL.ReturnFetchWarnEmptySet(TT.Access.Datasources.WellKnown.DataBaseTable.UID, TT.Access.Datasources.WellKnown.File.UID, pnL.txt(qPath.QueryAsSql)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ret = RC<Result<T>>.Failed(new Result<T>(), ErrorDescription: TraceHlp.FlattenExceptionMessagesPN(ex));
            }
            finally
            {
                sqlSrv.CloseConnection();
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
        protected async Task<RC> ExecuteDMLAsync<T>(QueryBuilderResult<T> qPath)
            where T : new()
        {
            var ret = RC.Failed(pnL);

            var sqlSrv = new ProviderAsync(pnL, connectionString);

            try
            {
                ret = await sqlSrv.executeSQLInsert(qPath.QueryAsSql);                
            }
            catch (Exception ex)
            {
                ret = RC.Failed(ex);
            }
            finally
            {               

            }
            return ret;
        }


    }
}
