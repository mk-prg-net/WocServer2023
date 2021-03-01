using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;
using Oracle.ManagedDataAccess.Client;
//using Oracle.DataAccess.Client;
using DFCObjects.Common.AD;
using System.Data;

using ATMO.mko.Logging;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using TechTerms = ATMO.mko.Logging.PNDocuTerms.DocuEntities.Composer.TechTerms;


namespace DZA.OracleHelper
{
    /// <summary>
    /// mko, 1.10.2020
    /// Asynchrone Variante des Oracle-Helpers
    /// </summary>
    public class OracleSQLAsync
    {
        private OracleConnection myOleDbConnection = null;

        //internal string connectionString = "data source=DZA11;user id=dza_read;password=";

        // mko, 21.3.2019
        // angepasst für Zugriff auf neue Oracle12 DB
        internal static string connectionString = OraSQL.connectionString;

        //string PWR = "YXRtb19pY29yZWFk";

        public void Connecto2Ora(bool asAdmin = false)
        {
            string ConStr = connectionString; // + ServicePassword(PWR);
            if (myOleDbConnection == null)
                myOleDbConnection = new OracleConnection(ConStr);
        }

        public void CloseOraConnection()
        {
            if (myOleDbConnection != null)
            {
                // myOleDbConnection.Close();
                myOleDbConnection.Dispose();
            }
        }

        private string ServicePassword(string PW)
        {
            byte[] data = Convert.FromBase64String(PW);
            string decodedString = Encoding.UTF8.GetString(data);

            return decodedString;
        }

        /// <summary>
        /// originated by Jorge
        /// 
        /// mko, 03.04.2019
        /// Refactoring
        /// 
        /// mko, 1.10.2020
        /// Asynchrone Variante erstellt.
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public async Task<System.Data.Common.DbDataReader> executeSQL(string sqlString)
        {
            try
            {
                if (myOleDbConnection == null)
                    Connecto2Ora();

                var myOleDbCommand = myOleDbConnection.CreateCommand();

                myOleDbCommand.CommandText = sqlString;

                if (myOleDbConnection.State != System.Data.ConnectionState.Open)
                    myOleDbConnection.Open();

                var myOleDbDataReader = await myOleDbCommand.ExecuteReaderAsync();//CommandBehavior.KeyInfo);
                return myOleDbDataReader;
            }
            catch (Exception ex)
            {
                throw new Exception(ATMO.mko.Logging.RC.CreateError(ex).ToString());
            }
        }


        /// <summary>
        /// mko, 1.10.2020
        /// Asynchrone Form erstellt
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="asAdmin"></param>
        /// <returns></returns>
        public async Task<bool> executeSQLInsert(string sqlString, bool asAdmin = false)
        {
            OracleCommand myOleDbCommand = new OracleCommand();
            try
            {
                if (myOleDbConnection == null)
                    Connecto2Ora(asAdmin);

                myOleDbCommand = myOleDbConnection.CreateCommand();

                myOleDbCommand.CommandText = sqlString;

                if (myOleDbConnection.State != System.Data.ConnectionState.Open)
                    myOleDbConnection.Open();

                var rc = await myOleDbCommand.ExecuteNonQueryAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ATMO.mko.Logging.RC.CreateError(ex).ToString());
            }
            finally
            {
                myOleDbCommand.Connection.Close();
                myOleDbCommand.Dispose();
            }
        }

        /// <summary>
        /// mko, 12.11.2019
        /// führt eine Folge von SQL- DML- Kommandos, eingeschlossen in einer Transaktion aus
        /// </summary>
        /// <param name="dmlSqlCmd"></param>
        /// <param name="asAdmin"></param>
        /// <returns></returns>
        public async Task<RCV3> executeSQLDMLTransaction(IComposer pnL, params string[] dmlSqlCmd)
        {
            var ret = RCV3.Failed(pnL.eNotCompleted());

            OracleCommand myOleDbCommand = new OracleCommand();
            try
            {
                using (var connection = new OracleConnection(connectionString))
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
                        ret = RCV3.Failed(TraceHlp.FlattenExceptionMessagesPN(ex));
                    }
                    finally
                    {
                        connection.Close();
                    }

                    ret = RCV3.Ok();
                }
            }
            catch (Exception ex)
            {
                ret = RCV3.Failed(TraceHlp.FlattenExceptionMessagesPN(ex));
            }

            return ret;
        }
    }
}
