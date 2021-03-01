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
    /// mko, 26.7.2018
    /// Adapted to new ReturnValue Type RCV3WithValue. 
    /// </summary>
    public class QueriesBase
    {
        protected IComposer pnL;
        protected PlxQueryResultDescriptionFactory plxResFactory;

        public QueriesBase(IComposer pnL)
        {
            this.pnL = pnL;
            plxResFactory = new PlxQueryResultDescriptionFactory(pnL);
        }

        /// <summary>
        /// Queries a set of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qPath"></param>
        /// <returns></returns>
        protected RCV3WithValue<RCV3, ResultSet<T>> GetRecords<T>(QueryBuilderResult<T> qPath)
            where T : new()
        {
            var ret = RCV3WithValue<RCV3, ResultSet<T>>.Failed(new ResultSet<T>());

            var ora = new global::DZA.OracleHelper.OraSQL();

            try
            {
                using (var reader = ora.executeSQL(qPath.QueryAsSql))
                {
                    var res = new List<T>();
                    while (reader.Read())
                    {
                        // mko, 18.6.2018
                        // StateNrSource as type save Enum

                        var docUserState = new T();
                        qPath.RecordToBoMapper.SetPropertiesOf(docUserState, reader);
                        res.Add(docUserState);
                    }
                    ret = RCV3WithValue<RCV3, ResultSet<T>>.Ok(new ResultSet<T>(res));
                }
            }
            catch (Oracle.ManagedDataAccess.Client.OracleException oraEx)
            {
                var errors = new List<IProperty>();
                foreach (Oracle.ManagedDataAccess.Client.OracleError err in oraEx.Errors)
                {
                    errors.Add(pnL.p(TTD.MetaData.Error.UID, err.Message));
                }

                ret = RCV3WithValue<RCV3, ResultSet<T>>.Failed(null,
                    pnL.ReturnFetchWithDetails(
                        false,
                        pnL.txt(oraEx.DataSource),
                        pnL.List(
                            pnL.p(TT.Search.SQL.SqlQuery.UID, qPath.QueryAsSql),
                            pnL.p(TT.Operators.MapTo.UID, typeof(T).Name),
                            pnL.EmbedMembers(errors.ToArray()),
                            pnL.p(TTD.MetaData.Msg.UID, oraEx.Message)
                        )));

            }
            catch (Exception ex)
            {
                ret = RCV3WithValue<RCV3, ResultSet<T>>.Failed(null,
                        pnL.ReturnFetchWithDetails(
                        false,
                        TT.Access.Datasources.WellKnown.ATMO.DFC.GDMDatabase.UID,
                        pnL.List(
                            pnL.p(TT.Search.SQL.SqlQuery.UID, qPath.QueryAsSql),
                            pnL.p(TT.Operators.MapTo.UID, typeof(T).Name),
                            pnL.p(TTD.MetaData.Msg.UID, ex.Message),
                            pnL.p(TTD.MetaData.Error.UID, pnL.EncapsulateAsPropertyValue(TraceHlp.FlattenExceptionMessagesPN(ex)))
                        )));
                    
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
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qPath"></param>
        /// <returns></returns>
        protected RCV3WithValue<RCV3, Result<T>> GetRecord<T>(QueryBuilderResult<T> qPath)
            where T : new()
        {

            var ret = RCV3WithValue<RCV3, Result<T>>.Failed(new Result<T>());

            var ora = new global::DZA.OracleHelper.OraSQL();

            try
            {
                using (var reader = ora.executeSQL(qPath.QueryAsSql))
                {
                    if (reader.Read())
                    {
                        // mko, 18.6.2018
                        // StateNrSource as type save Enum

                        var res = new T();
                        qPath.RecordToBoMapper.SetPropertiesOf(res, reader);
                        ret = RCV3WithValue<RCV3, Result<T>>.Ok(new Result<T>(res));
                    }
                    else
                    {
                        ret = RCV3WithValue<RCV3, Result<T>>.Ok(value: new Result<T>(), Message: $"Resultset of Query is empty: {qPath.QueryAsSql}");
                    }
                }
            }
            catch (Exception ex)
            {
                ret = RCV3WithValue<RCV3, Result<T>>.Failed(new Result<T>(), ex);
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
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qPath"></param>
        /// <returns></returns>
        protected RCV3 ExecuteDML<T>(QueryBuilderResult<T> qPath)
            where T : new()
        {
            var ret = RCV3.Failed();

            var ora = new global::DZA.OracleHelper.OraSQL();

            try
            {
                bool success = ora.executeSQLInsert(qPath.QueryAsSql);
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

