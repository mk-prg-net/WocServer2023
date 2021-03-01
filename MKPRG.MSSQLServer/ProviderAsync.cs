using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

using MKPRG.Tracing;
using MKPRG.Tracing.DocuTerms;
using TechTerms = MKPRG.Naming.TechTerms;


namespace MKPRG.MSSQLServer
{
    /// <summary>
    /// mko, 1.10.2020
    /// Asynchrone Variante des Oracle-Helpers
    /// </summary>
    public class ProviderAsync
        : IDisposable
    {

        IComposer pnL;

        public ProviderAsync(IComposer pnL, string ConnectionString)
        {
            this.pnL = pnL;
            this.connectionString = ConnectionString;
        }

        private SqlConnection currentConnection = null;

        internal string connectionString;

        public void Connect()
        {            
            if (currentConnection == null)
                currentConnection = new SqlConnection(connectionString);
        }

        public void CloseConnection()
        {
            if (currentConnection != null)
            {
                // myOleDbConnection.Close();
                currentConnection.Dispose();
            }
        }

        /// <summary>
        /// mko, 1.10.2020
        /// Asynchrone Variante erstellt.
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public async Task<RC<SqlDataReader>> executeSQL(string sqlString)
        {
            var ret = RC<SqlDataReader>.Failed(null, ErrorDescription: pnL.eNotCompleted());
            
            try
            {
                if (currentConnection == null)
                    Connect();

                var cmd = currentConnection.CreateCommand();

                cmd.CommandText = sqlString;

                if (currentConnection.State != System.Data.ConnectionState.Open)
                    currentConnection.Open();

                var dataReader = await cmd.ExecuteReaderAsync();

                ret = RC<SqlDataReader>.Ok(dataReader);
            }
            catch (Exception ex)
            {
                ret = RC<SqlDataReader>.Failed(
                        null, 
                        ErrorDescription: pnL.m("executeSQL", 
                                                pnL.p("SQLString", sqlString), 
                                                pnL.eFails(TraceHlp.FlattenExceptionAsDocuTermInstance(ex))));                
            }

            // Achtung: Verbindung darf nicht geschlossen werden, da über SqlDataReder Daten eingelesen werden
            return ret;
        }


        /// <summary>
        /// mko, 1.10.2020
        /// Asynchrone Form erstellt
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="asAdmin"></param>
        /// <returns></returns>
        public async Task<RC> executeSQLInsert(string sqlString)
        {
            var ret = RC.Failed(pnL.eNotCompleted());
            var cmd = new SqlCommand();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    cmd = connection.CreateCommand();

                    cmd.CommandText = sqlString;

                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    var rc = await cmd.ExecuteNonQueryAsync();

                    ret = RC.Ok(pnL);
                }
            }
            catch (Exception ex)
            {
                ret = RC.Failed(pnL.m("executeSqlInsert", pnL.p("sqlString", sqlString), pnL.eFails(TraceHlp.FlattenExceptionAsDocuTermInstance(ex))));                
            }
            //finally
            //{
            //    cmd.Connection.Close();
            //    cmd.Dispose();
            //}

            return ret;
        }

        /// <summary>
        /// mko, 12.11.2019
        /// führt eine Folge von SQL- DML- Kommandos, eingeschlossen in einer Transaktion aus
        /// </summary>
        /// <param name="dmlSqlCmd"></param>
        /// <param name="asAdmin"></param>
        /// <returns></returns>
        public async Task<RC> executeSQLDMLTransaction(IComposer pnL, params string[] dmlSqlCmd)
        {
            var ret = RC.Failed(pnL.eNotCompleted());
            
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var sqlCmd = connection.CreateCommand();

                    // Strenge Serialisierung: Kein Einfügen und aktualisieren durch andere während der Änderungen möglich.
                    // Wiederholte Selects führen zum gleichen Ergebnis
                    var transaction = connection.BeginTransaction(IsolationLevel.Serializable);

                    try
                    {
                        foreach (var cmd in dmlSqlCmd)
                        {
                            sqlCmd.CommandText = cmd;
                            await sqlCmd.ExecuteNonQueryAsync();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ret = RC.Failed(TraceHlp.FlattenExceptionMessagesPN(ex));
                    }
                    finally
                    {
                        connection.Close();
                    }

                    ret = RC.Ok(pnL);
                }
            }
            catch (Exception ex)
            {
                ret = RC.Failed(TraceHlp.FlattenExceptionMessagesPN(ex));
            }

            return ret;
        }

        public void Dispose()
        {
            if(currentConnection != null && currentConnection.State != ConnectionState.Closed)
            {
                currentConnection.Close();
                currentConnection.Dispose();
            }
        }
    }
}
