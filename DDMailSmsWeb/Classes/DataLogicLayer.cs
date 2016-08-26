using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace DDMailSmsWeb.Classes
{
    class DataLogicLayer
    {

        static string dbConnection = HttpContext.Current.Application["GlobalValueKey"].ToString();
        static string dbConnectionLocal = ConfigurationManager.ConnectionStrings["DDSuperConnectionString"].ToString();

        public static DataTable GetResultFromSqlQur(string strQur)
        {

         
            try
            {

                using (SqlConnection conn = new SqlConnection(dbConnection))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();

                    }
                    DataTable dbOleDataTable = new DataTable();
                    SqlCommand dbOleCommand = new SqlCommand();
                    dbOleCommand.Connection = conn;
                    dbOleCommand.Parameters.Clear();
                    dbOleCommand.CommandType = CommandType.Text;
                    dbOleCommand.CommandText = strQur;
                    SqlDataAdapter dbOleDataAdapter = new SqlDataAdapter();
                    dbOleDataAdapter.SelectCommand = dbOleCommand;
                    dbOleDataAdapter.Fill(dbOleDataTable);
                    return dbOleDataTable;
                }
            }
            catch (SqlException sqlEx)
            {
                
                throw sqlEx;
            }
            catch (Exception ex1)
            {
                
                throw ex1;

            }
            finally
            {
                 
            }
        }
        public static int ExecuteNonQuery(string strSQL)
        {
            int retval = 0;
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                } 
                

                 
                SqlCommand dbOleCommand = new SqlCommand();
                dbOleCommand.Connection = conn;
                dbOleCommand.CommandText = strSQL;

                try
                {
                    retval = dbOleCommand.ExecuteNonQuery();
                }
                catch (Exception ex1)
                {
                     
                    throw ex1;

                }
                finally
                { 
                }
                return retval;
            }
            return retval;
        }

         public static int ExecuteNonQueryLocal(string strSQL)
        {
            int retval = 0;
            using (SqlConnection conn = new SqlConnection(dbConnectionLocal))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                } 
                

                 
                SqlCommand dbOleCommand = new SqlCommand();
                dbOleCommand.Connection = conn;
                dbOleCommand.CommandText = strSQL;

                try
                {
                    retval = dbOleCommand.ExecuteNonQuery();
                }
                catch (Exception ex1)
                {
                     
                    throw ex1;

                }
                finally
                { 
                }
                return retval;
            }
            return retval;
        }



        public static int ExecuteScalarReturnIdentity(string strSQL)
        {
              int retval = 0;
              using (SqlConnection conn = new SqlConnection(dbConnection))
              {
                  if (conn.State == ConnectionState.Closed)
                  {
                      conn.Open();

                  }

                  string query2 = "Select @@Identity";


              
                  SqlCommand dbOleCommand = new SqlCommand();
                  dbOleCommand.Connection = conn;
                  dbOleCommand.CommandText = strSQL;
                  try
                  {
                      int successfully = dbOleCommand.ExecuteNonQuery();
                      if (successfully > 0)
                      {
                          dbOleCommand.CommandText = query2;
                          retval = (int)dbOleCommand.ExecuteScalar();
                      }

                  }
                  catch (Exception ex1)
                  {
                      
                      throw ex1;
                  }
                  finally
                  {
                      
                  }
                  return retval;
              }
              return retval;
        }






        public static int toExceuteQueryHavingConnection(SqlCommand dbOleCommand, string strSQL)
        {
              int retval = 0;
              using (SqlConnection conn = new SqlConnection(dbConnection))
              {
                  if (conn.State == ConnectionState.Closed)
                  {
                      conn.Open();

                  }
                  dbOleCommand.Connection = conn;
                  dbOleCommand.CommandText = strSQL;
                  try
                  {
                      retval = dbOleCommand.ExecuteNonQuery();
                  }
                  catch (Exception ex1)
                  {
                      
                      throw ex1;
                  }
                  return retval;
              }
              return retval;
        }

        public static DataTable toGetDatatableHavingConnection(SqlCommand dbOleCommand, string strSQL)
        {
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                }
                DataTable dt = new DataTable();
                try
                {
                    dbOleCommand.CommandType = CommandType.Text;
                    dbOleCommand.CommandText = strSQL;
                    dbOleCommand.Connection = conn;
                    SqlDataAdapter dbOleDataAdapter = new SqlDataAdapter();
                    dbOleDataAdapter.SelectCommand = dbOleCommand;
                    dbOleDataAdapter.Fill(dt);
                }
                catch (Exception ex1)
                {
                    
                    throw ex1;
                }
                return dt;
            }

        }





    }
}
