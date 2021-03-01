using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;
//using Oracle.DataAccess.Client;

namespace DZA.OracleHelper
{
    public class OraSQLNew
    {
        private OracleConnection myOleDbConnection = null;

        // private string connectionString = "provider=OraOLEDB.Oracle;data source=DZA11;user id=dza_read;password=";
        //private string connectionString = "data source=DZA11;user id=dza_read;password=";
        //private string connectionString = "data source=DZA11;user id=dza_read;password=";
        //string PWR = "YXRtb19pY29yZWFk";
        //string PWA = "ZGlnaXRhbF9wcm9kMQ==";
        //private string connectionString2 = "data source=DZA11.WORLD;user id=DZA_ADMIN;password=";

        public void Connecto2Ora(bool asAdmin = false)
        {
            #region new Code ConnectionString
            //string connectionstring = OracleConnString("RB-ENGSRV23.DE.BOSCH.COM", "38140", "DZAPRO1", "dza_read", ServicePassword(PWR));
            // if (asAdmin)
            //     connectionstring = OracleConnString("RB-ENGSRV23.DE.BOSCH.COM", "38140", "DZAPRO1", "dza_admin", ServicePassword(PWA));
            //connectionstringTestSystem = OracleConnString("rb-engsrv34.de.bosch.com", "38790", "ASQ43", "dza_admin", ServicePassword(PWR));
            #endregion

            //string ConStr = connectionString + ServicePassword(PWR);
            //if (asAdmin)
            //    ConStr = connectionString2 + ServicePassword(PWA);
            //ConStr = connectionstring;
            if (myOleDbConnection == null)
                myOleDbConnection = new OracleConnection(OraSQL.connectionString);
        }
       
        public void CloseOraConnection()
        {
            if (myOleDbConnection != null)
            {
                // myOleDbConnection.Close();
                myOleDbConnection.Dispose();
            }
        }

        //private string ServicePassword(string PW)
        //{
        //    byte[] data = Convert.FromBase64String(PW);
        //    string decodedString = Encoding.UTF8.GetString(data);

        //    return decodedString;
        //}

        public OracleDataReader executeSQL(string sqlString)
        {
            OracleDataReader myOleDbDataReader = null;
            try
            {
                if (myOleDbConnection == null)
                    Connecto2Ora();
                OracleCommand myOleDbCommand = myOleDbConnection.CreateCommand();
                myOleDbCommand.CommandText = sqlString;
                if (myOleDbConnection.State != System.Data.ConnectionState.Open)
                    myOleDbConnection.Open();
                myOleDbDataReader = myOleDbCommand.ExecuteReader();//CommandBehavior.KeyInfo);
                return myOleDbDataReader;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                //myOleDbConnection.Close();
                //CloseOraConnection();
            }
        }

         public string executeSQLInsertReturnSequence(string sqlString, bool asAdmin = false,bool returnValue=false)
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
                OracleParameter outParam = new OracleParameter("NR", OracleDbType.Decimal); //OracleType.Number);
                outParam.Direction = System.Data.ParameterDirection.Output;
                myOleDbCommand.Parameters.Add(outParam);    
                myOleDbCommand.ExecuteNonQuery();
                return  outParam.Value.ToString();
            }
            catch (Exception ex)
            {
                return "false";
            }
            finally
            {
                myOleDbCommand.Connection.Close();
                myOleDbCommand.Dispose();

            }
        }
        public bool executeSQLInsert(string sqlString, bool asAdmin = false)
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
                return false;
            }
            finally
            {
                myOleDbCommand.Connection.Close();
                myOleDbCommand.Dispose();

            }
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

    }
}
