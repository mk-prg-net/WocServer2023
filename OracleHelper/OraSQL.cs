using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Jorge Sanchez
    /// mko, 6.11.2017
    /// Alle catch- Handler haben bis Dato Fehler absorbiert. Fehler werden jetzt
    /// in aussagekräftige Meldungen umgewandelt und als Ausnahmen erneut geworfen
    /// </summary>
    public class OraSQL
    {
        private OracleConnection myOleDbConnection = null;

        //internal string connectionString = "data source=DZA11;user id=dza_read;password=";

        // mko, 21.3.2019
        // angepasst für Zugriff auf neue Oracle12 DB
        internal static string connectionString = "data source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=rb0orarac06.de.bosch.com)(PORT=38000)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=DZAPRO1_RB0ORARAC06.de.bosch.com)));user id=DZA_ADMIN;password=newpw.346_12c";

        //string PWR = "YXRtb19pY29yZWFk";

        public void Connecto2Ora(bool asAdmin=false)
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
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public OracleDataReader executeSQL(string sqlString)
        {            
            try
            {
                if (myOleDbConnection == null)
                    Connecto2Ora();

                var myOleDbCommand = myOleDbConnection.CreateCommand();

                myOleDbCommand.CommandText = sqlString;

                if (myOleDbConnection.State!= System.Data.ConnectionState.Open)
                    myOleDbConnection.Open();

                var myOleDbDataReader = myOleDbCommand.ExecuteReader();//CommandBehavior.KeyInfo);
                return myOleDbDataReader;
            }
            catch (Exception ex)
            {
                throw new Exception(ATMO.mko.Logging.RC.CreateError(ex).ToString());
            }
        }



        public bool executeSQLInsert(string sqlString, bool asAdmin=false)
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

                myOleDbCommand.ExecuteNonQuery();

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
        public RCV3 executeSQLDMLTransaction(ATMO.mko.Logging.PNDocuTerms.DocuEntities.IComposer pnL, params string[] dmlSqlCmd)
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
                            sqlCmd.ExecuteNonQuery();
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



        private void loadRolesSQL()
        {
            OracleDataReader myOleDbDataReader = executeSQL("SELECT * " + "FROM dza_admin.ORGROLE");
            if (myOleDbDataReader != null)
            {
                loadRoles(myOleDbDataReader);
                myOleDbDataReader.Close();
            }

        }



        private void loadRoles(OracleDataReader reader)
        {
            int RoleID = -1;
            int RoleName = -1;
            int RoleDescription = -1;
            int RoleRemark = -1;

            for (int i = 0; i <= reader.FieldCount; i++)
            {
                string TheColumnName = reader.GetName(0);
                switch (TheColumnName)
                {
                    case "ID":
                        RoleID = i;
                        break;
                    case "NAME":
                        RoleName = i;
                        break;
                    case "DESCRIPTION":
                        RoleDescription = i;
                        break;
                    case "REMARK":
                        RoleRemark = i;
                        break;
                    default:
                        break;
                }
            }
            //lRoles = new List<DFCRole>();
            //while (reader.Read())
            //{
            //    DFCRole drole = new DFCRole();
            //    drole.RoleID = reader.GetValue(RoleID).ToString();
            //    drole.RoleName = reader.GetValue(RoleName).ToString();
            //    drole.RoleDescription = reader.GetValue(RoleDescription).ToString();
            //    drole.RoleRemark = reader.GetValue(RoleRemark).ToString();
            //    lRoles.Add(drole);
            //}
        }


        public bool UploadBulkUsers(List<ADUser> bulkData,bool asAdmin=false)
        {

            OracleCommand myOleDbCommandDelete = new OracleCommand();
            OracleCommand myOleDbCommand = new OracleCommand();
            OracleCommand myOleDbCommandMerge = new OracleCommand();
            
            try
            {
                if (myOleDbConnection == null)
                    Connecto2Ora(asAdmin);


                myOleDbCommandDelete = myOleDbConnection.CreateCommand();
                string DeleteQuery = "TRUNCATE TABLE dza_admin.BOSCH106ADUSRTMP";
                DeleteQuery = "DELETE FROM dza_admin.BOSCH106ADUSRTMP";
                myOleDbCommandDelete.CommandText = DeleteQuery;

                myOleDbCommandMerge = myOleDbConnection.CreateCommand();
                #region merge
                string MergeQuery = "merge into dza_admin.BOSCH106ADUSR dest using (select * from dza_admin.BOSCH106ADUSRTMP) src on (src.USRID = dest.USRID) " +
                                "when matched then update set " +
                                "dest.USRFIRSTNAME = src.USRFIRSTNAME," +
                                "dest.USRLASTNAME = src.USRLASTNAME," +
                                "dest.USRPHONE = src.USRPHONE," +
                                "dest.USRMAIL = src.USRMAIL," +
                                "dest.USRDEPT = src.USRDEPT," +
                                "dest.USRKST = src.USRKST," +
                                "dest.USRDOMAIN = src.USRDOMAIN," +
                                "dest.USRDISPLAYNAME = src.USRDISPLAYNAME," +
                                "dest.USREXTERNALCOMPANY = src.USREXTERNALCOMPANY," +
                                "dest.USRDISABLED = src.USRDISABLED," +
                                "dest.LUP = src.LUP," +
                                "dest.USREXTERNAL = src.USREXTERNAL," +
                                "dest.USRLASTLOGON = src.USRLASTLOGON, " +
                                "dest.USRORG = src.USRORG " +
                                
                    //-- DELETE ROWS IN TARGET TABLE IF SOURCE DEPTNO EQUALS 30
                    //"delete where deptno = 30
                "when not matched then insert (USRID,USRFIRSTNAME,USRLASTNAME,USRPHONE,USRMAIL,USRDEPT,USRKST,USRDOMAIN,USRDISPLAYNAME,USREXTERNALCOMPANY,USRDISABLED,USREXTERNAL,USRLASTLOGON,LUP,USRORG) " +
                  "Values(src.USRID,src.USRFIRSTNAME,src.USRLASTNAME,src.USRPHONE,src.USRMAIL,src.USRDEPT,src.USRKST,src.USRDOMAIN,src.USRDISPLAYNAME,src.USREXTERNALCOMPANY,src.USRDISABLED,src.USREXTERNAL,src.USRLASTLOGON,src.LUP,src.USRORG)";
                //-- ONLY IF DEPTNO IN SOURCE DATA NOT EQUALS "30"
                // "where not src.deptno = 30",
                myOleDbCommandMerge.CommandText = MergeQuery;
                #endregion
                
                myOleDbCommand = myOleDbConnection.CreateCommand();
                string InserQuery = @"Insert into dza_admin.BOSCH106ADUSRTMP (USRID,USRFIRSTNAME,USRLASTNAME,USRPHONE,USRMAIL,USRDEPT,USRKST,USRDOMAIN,USRDISPLAYNAME,USREXTERNALCOMPANY,USRDISABLED,USREXTERNAL,USRLASTLOGON,LUP,USRORG) " +
                      "Values(:USRID,:USRFIRSTNAME,:USRLASTNAME,:USRPHONE,:USRMAIL,:USRDEPT,:USRKST,:USRDOMAIN,:USRDISPLAYNAME,:USREXTERNALCOMPANY,:USRDISABLED,:USREXTERNAL,:USRLASTLOGON,:LUP,:USRORG)";
               
                myOleDbCommand.CommandText = InserQuery;
                myOleDbCommand.CommandType = CommandType.Text;
                myOleDbCommand.BindByName = true;
                // In order to use ArrayBinding, the ArrayBindCount property
                // of OracleCommand object must be set to the number of records to be inserted
                myOleDbCommand.ArrayBindCount = bulkData.Count;
                myOleDbCommand.Parameters.Add(":USRID", OracleDbType.Varchar2, bulkData.Select(c => c.Username).ToArray(), ParameterDirection.Input);
                myOleDbCommand.Parameters.Add(":USRFIRSTNAME", OracleDbType.Varchar2, bulkData.Select(c => c.FirstName).ToArray(), ParameterDirection.Input);
                myOleDbCommand.Parameters.Add(":USRLASTNAME", OracleDbType.Varchar2, bulkData.Select(c => c.LastName).ToArray(), ParameterDirection.Input);
                myOleDbCommand.Parameters.Add(":USRPHONE", OracleDbType.Varchar2, bulkData.Select(c => c.Phone).ToArray(), ParameterDirection.Input);
                myOleDbCommand.Parameters.Add(":USRMAIL", OracleDbType.Varchar2, bulkData.Select(c => c.EMail).ToArray(), ParameterDirection.Input);
                myOleDbCommand.Parameters.Add(":USRDEPT", OracleDbType.Varchar2, bulkData.Select(c => c.Department).ToArray(), ParameterDirection.Input);
                myOleDbCommand.Parameters.Add(":USRKST", OracleDbType.Varchar2, bulkData.Select(c => c.CostCenter).ToArray(), ParameterDirection.Input);
                myOleDbCommand.Parameters.Add(":USRDOMAIN", OracleDbType.Varchar2, bulkData.Select(c => c.Domain).ToArray(), ParameterDirection.Input);
                myOleDbCommand.Parameters.Add(":USRDISPLAYNAME", OracleDbType.Varchar2, bulkData.Select(c => c.DisplayName).ToArray(), ParameterDirection.Input);
                myOleDbCommand.Parameters.Add(":USREXTERNALCOMPANY", OracleDbType.Varchar2, bulkData.Select(c => c.Company).ToArray(), ParameterDirection.Input);
                myOleDbCommand.Parameters.Add(":USRDISABLED", OracleDbType.Varchar2, bulkData.Select(c => c.Disabled).ToArray(), ParameterDirection.Input);
                myOleDbCommand.Parameters.Add(":USREXTERNAL", OracleDbType.Varchar2, bulkData.Select(c => c.External).ToArray(), ParameterDirection.Input);


                myOleDbCommand.Parameters.Add(":USRLASTLOGON", OracleDbType.Date, bulkData.Select(c => c.LastLogon).ToArray(), ParameterDirection.Input);

                myOleDbCommand.Parameters.Add(":LUP", OracleDbType.Date, bulkData.Select(c => c.LUP).ToArray(), ParameterDirection.Input);
                myOleDbCommand.Parameters.Add(":USRORG", OracleDbType.Varchar2, bulkData.Select(c => c.UserOrg).ToArray(), ParameterDirection.Input);
                

                //string tmpTimestamp = dtLastLogon.ToString("MM/dd/yyyy HH:mm:ss").Replace(".", "/");
                //tmpTimestamp = "TO_DATE('" + string.Format(tmpTimestamp, "MM/DD/YYY hh:mm:ss") + "','MM/DD/YY HH24:MI:SS')";

                //myOleDbCommand.CommandText = InserQuery;
                if (myOleDbConnection.State != System.Data.ConnectionState.Open)
                    myOleDbConnection.Open();

                myOleDbCommandDelete.ExecuteNonQuery();
                myOleDbCommand.ExecuteNonQuery();
                myOleDbCommandMerge.ExecuteNonQuery();
                //if (result == bulkData.Count)
                //    returnValue = true;
        
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ATMO.mko.Logging.RC.CreateError(ex).ToString());
                //return false;
            }
            finally
            {
                myOleDbCommand.Connection.Close();
                myOleDbCommand.Dispose(); 
                
            }
        }


        public bool UploadBulkUsersGroup(List<DFCUserGroup> dfcUsrGrp, bool asAdmin = false)
        {
            OracleCommand myOleDbCommandDelete = new OracleCommand();
            OracleCommand myOleDbCommand = new OracleCommand();
            try
            {
                if (myOleDbConnection == null)
                    Connecto2Ora(asAdmin);


                myOleDbCommandDelete = myOleDbConnection.CreateCommand();
                string DeleteQuery = "TRUNCATE TABLE dza_admin.BOSCH106ADUSERGROUP";
                DeleteQuery = "DELETE FROM dza_admin.BOSCH106ADUSERGROUP";
                myOleDbCommandDelete.CommandText = DeleteQuery;
                myOleDbCommand = myOleDbConnection.CreateCommand();

                string InserQuery = @"Insert into dza_admin.BOSCH106ADUSERGROUP (USRGRP,USRID,GRPID) " +
                      "Values(:USRGRP,:USRID,:GRPID)";

                myOleDbCommand.CommandText = InserQuery;
                myOleDbCommand.CommandType = CommandType.Text;
                myOleDbCommand.BindByName = true;
                myOleDbCommand.ArrayBindCount = dfcUsrGrp.Count;
                myOleDbCommand.Parameters.Add(":USRID", OracleDbType.Varchar2, dfcUsrGrp.Select(c => c.Username).ToArray(), ParameterDirection.Input);
                myOleDbCommand.Parameters.Add(":GRPID", OracleDbType.Varchar2, dfcUsrGrp.Select(c => c.Groupname).ToArray(), ParameterDirection.Input);
                myOleDbCommand.Parameters.Add(":USRGRP", OracleDbType.Varchar2, dfcUsrGrp.Select(c => c.UserGroup).ToArray(), ParameterDirection.Input);
                
                if (myOleDbConnection.State != System.Data.ConnectionState.Open)
                    myOleDbConnection.Open();
                myOleDbCommandDelete.ExecuteNonQuery();
                myOleDbCommand.ExecuteNonQuery();



                //InserQuery = @"Insert into dza_admin.BOSCH106ADUSERGROUP (USRGRP,USRID,GRPID) " +
                //     "Values(:USRGRP,:USRID,:GRPID)";
                //myOleDbCommand.CommandText = InserQuery;
                //myOleDbCommand.CommandType = CommandType.Text;
                //myOleDbCommand.BindByName = true;
                //myOleDbCommand.ArrayBindCount = dfcUsrGrp.Count;
                //myOleDbCommand.Parameters.Add(":USRID", OracleDbType.Varchar2, dfcUsrGrp.Select(c => c.Username).ToArray(), ParameterDirection.Input);
                //myOleDbCommand.Parameters.Add(":GRPID", OracleDbType.Varchar2, dfcUsrGrp.Select(c => c.Groupname).ToArray(), ParameterDirection.Input);
                //myOleDbCommand.Parameters.Add(":USRGRP", OracleDbType.Varchar2, dfcUsrGrp.Select(c => c.UserGroup).ToArray(), ParameterDirection.Input);
                ////myOleDbCommand.Parameters.Add(":USRGRP", OracleDbType.Varchar2, dfcUsrGrp.Select(c => c.).ToArray(), ParameterDirection.Input);
                
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ATMO.mko.Logging.RC.CreateError(ex).ToString());
                //return false;
            }
            finally
            {
                myOleDbCommand.Connection.Close();
                myOleDbCommand.Dispose();

            }
        }
    }
}
