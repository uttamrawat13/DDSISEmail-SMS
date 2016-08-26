using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient; 
using System.Configuration;
using DDMailSmsWeb.Classes;
using System.Web;
namespace DDMailSmsWeb.DynamicAccess
{
    
     
     static class DyDataAccessManager
    {
         //static string connStrClient = Convert.ToString(HttpContext.Current.Session["GlobalValueKey"]);
         
        #region "Methods"
         static public DataTable QueryStringValueCheck(string tableName, string whrFieldName,string value)
         {

             DataTable _ds = new DataTable();

             try
             {
                 using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                 {
                     using (SqlCommand CMD = new SqlCommand())
                     {

                         CMD.Connection = connection;
                         CMD.CommandType = System.Data.CommandType.Text;
                         CMD.CommandText = "select * from "+tableName+" where "+whrFieldName+"='"+value+"'";
                         CMD.Prepare();
                         SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                         DataAdapter.Fill(_ds);

                     }
                 }
             }
             catch (Exception ex)
             {

             }
             finally
             {
             }

             return _ds;
         }
     
         static public DataTable GetListDepartment(int DeptID)
         {

             DataTable _ds = new DataTable();

             try
             {
                 using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                 {
                     using (SqlCommand CMD = new SqlCommand())
                     {

                         CMD.Connection = connection;
                         CMD.CommandType = System.Data.CommandType.Text;
                         CMD.CommandText = "select * from Departments where DeptID="+DeptID;
                         CMD.Prepare();
                         SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                         DataAdapter.Fill(_ds);

                     }
                 }
             }
             catch (Exception ex)
             {
                 
             }
             finally
             {
             }

             return _ds;
         }
         static public DataTable GetListDepartment()
         {
             DataTable _ds = new DataTable();
             try
             {
                 using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                 {
                     using (SqlCommand CMD = new SqlCommand())
                     {

                         CMD.Connection = connection;
                         CMD.CommandType = System.Data.CommandType.Text;
                         CMD.CommandText = "select 0 as DeptID,'Select Departments' as DeptDescription from Departments Union select DeptID,DeptDescription from Departments ";
                         CMD.Prepare();
                         SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                         DataAdapter.Fill(_ds);

                     }
                 }
             }
             catch (Exception ex)
             {
                 throw;
             }
             finally
             {
             }

             return _ds;
         }
         static public DataTable GetStudentEmail(string studentno)
         {
             DataTable _ds = new DataTable();
             try
             {
                 using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                 {
                     using (SqlCommand CMD = new SqlCommand())
                     {

                         CMD.Connection = connection;
                         CMD.CommandType = System.Data.CommandType.Text;
                         CMD.CommandText = "select Email,Email2,StudentID,LastName +', '+ Firstname as Name,MobilePhone from Students where studentno='" + studentno + "'";
                   
                         CMD.Prepare();
                         SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                         DataAdapter.Fill(_ds);

                     }
                 }
             }
             catch (Exception ex)
             {
                 throw;
             }
             finally
             {
             }

             return _ds;
         }
         static public DataTable GetLeadEmail(string LeadsID)
         {
             DataTable _ds = new DataTable();
             try
             {
                 using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                 {
                     using (SqlCommand CMD = new SqlCommand())
                     {

                         CMD.Connection = connection;
                         CMD.CommandType = System.Data.CommandType.Text;
                         CMD.CommandText = "select Email,Email2,LeadsID,Lname+', '+FName as Name,PhoneMobile from Lead where LeadsID='" + LeadsID + "'";
                  
                         CMD.Prepare();
                         SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                         DataAdapter.Fill(_ds);

                     }
                 }
             }
             catch (Exception ex)
             {
                 throw;
             }
             finally
             {
             }

             return _ds;
         }
         static public DataTable GetStudentNo(string email, string email2)
         {
             DataTable _ds = new DataTable();
             try
             {
                 using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                 {
                     using (SqlCommand CMD = new SqlCommand())
                     {

                         CMD.Connection = connection;
                         CMD.CommandType = System.Data.CommandType.Text;
                         CMD.CommandText = "select StudentNo from Students where EMail='" + email + "' or EMail2='" + email2 + "'";
                   
                         CMD.Prepare();
                         SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                         DataAdapter.Fill(_ds);

                     }
                 }
             }
             catch (Exception ex)
             {
                 throw;
             }
             finally
             {
             }

             return _ds;
         }
         static public DataTable GetStudentNo(string mobileno)
         {
             DataTable _ds = new DataTable();
             try
             {
                 using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                 {
                     using (SqlCommand CMD = new SqlCommand())
                     {

                         CMD.Connection = connection;
                         CMD.CommandType = System.Data.CommandType.Text;
                         CMD.CommandText = "select StudentNo from Students where MobilePhone='" + mobileno + "'";

                         CMD.Prepare();
                         SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                         DataAdapter.Fill(_ds);

                     }
                 }
             }
             catch (Exception ex)
             {
                 throw;
             }
             finally
             {
             }

             return _ds;
         }
         static public DataTable GetLeadsID(string email, string email2)
         {

             DataTable _ds = new DataTable();
             try
             {
                 var connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"]));

                 using (SqlCommand CMD = new SqlCommand())
                 {
                     connection.Open();
                     CMD.Connection = connection;
                     CMD.CommandType = System.Data.CommandType.Text;
                     CMD.CommandText = "select LeadsID from Lead where EMail='" + email + "' or EMail2='" + email2 + "'";
                     CMD.Prepare();
                     SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                     DataAdapter.Fill(_ds);
                     connection.Close();
                 }
             }
             catch (Exception ex)
             {

                 throw;
             }
             finally
             {

             }

             return _ds;
         }
         static public DataTable GetLeadsID(string mobileno)
         {

             DataTable _ds = new DataTable();
             try
             {
                 var connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"]));

                 using (SqlCommand CMD = new SqlCommand())
                 {
                     connection.Open();
                     CMD.Connection = connection;
                     CMD.CommandType = System.Data.CommandType.Text;
                     CMD.CommandText = "select LeadsID from Lead where PhoneMobile='" + mobileno + "'";
                     CMD.Prepare();
                     SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                     DataAdapter.Fill(_ds);
                     connection.Close();
                 }
             }
             catch (Exception ex)
             {

                 throw;
             }
             finally
             {

             }

             return _ds;
         }
         static public  Boolean     CheckStdMobleSMS(string MobilePhone)
         {
             Boolean result=false;
             DataTable _ds = new DataTable();
             try
             {
                 using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                 {
                     using (SqlCommand CMD = new SqlCommand())
                     {

                         CMD.Connection = connection;
                         CMD.CommandType = System.Data.CommandType.Text;
                         CMD.CommandText = "select Email,Email2,StudentID,Firstname +' '+ LastName as Name,MobilePhone from Students where MobilePhone='" + MobilePhone + "'";

                         CMD.Prepare();
                         SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                         DataAdapter.Fill(_ds);

                         if (_ds.Rows.Count > 0)
                            {
                                result=true;
                            }
                     }
                 }
             }
             catch (Exception ex)
             {
                 throw;
             }
             finally
             {
             }

             return result;
         }
      
        #endregion


        #region "Switch to anthor students"

     
          static public DataTable SwitchToStudentBind(string status)
          {
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {
                          string strqry = string.Empty;
                          strqry = strqry + " select S.StudentID,S.LastName+', '+ S.FirstName  as FullName,convert(varchar(50), S.StudentNo) as StudentNo,S.StudentStatusID,  ";
                          strqry = strqry + " (Select StudentStatus from StudentStatus where StudentStatusID=S.StudentStatusID) as StudentStatus  ";
                          strqry = strqry + "  from Students AS S where S.StudentStatusID='"+ status +"'  ";
                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = strqry;
                          CMD.Prepare();
                          SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                          DataAdapter.Fill(_ds);
                      }
                  }
              }
              catch (Exception ex)
              {
                  throw;
              }
              finally
              {
              }

              return _ds;
          }
          static public DataTable SwitchToStudentsBindFilter(string searchtext, string StatusCheckValue)
          {
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {

                        
                          string strqry = string.Empty;
                          if (StatusCheckValue == "0")
                          {
                              strqry = strqry + " select Top 20 S.StudentID,S.LastName+', '+ S.FirstName  as FullName,convert(varchar(50), S.StudentNo) as StudentNo,  ";
                              strqry = strqry + " (Select StudentStatus from StudentStatus where StudentStatusID=S.StudentStatusID) as StudentStatus  ";
                              strqry = strqry + " ,S.StudentStatusID,S.EMail,S.SSN from Students AS S  ";
                              strqry = strqry + " where  S.LastName+', '+ S.FirstName  like '" + searchtext + "%' or S.StudentNo like '" + searchtext + "%' ";
                              strqry = strqry + " or (Select StudentStatus from StudentStatus where StudentStatusID=S.StudentStatusID) like '" + searchtext + "%' ";
                              strqry = strqry + " or S.EMail like '" + searchtext + "%' or S.SSN like '" + searchtext + "%' ";
                          }
                          else   /* Select by status if selected */
                          {
                              strqry = strqry + " select Top 20 S.StudentID,S.LastName+', '+ S.FirstName  as FullName,convert(varchar(50), S.StudentNo) as StudentNo,  ";
                              strqry = strqry + " (Select StudentStatus from StudentStatus where StudentStatusID=S.StudentStatusID) as StudentStatus  ";
                              strqry = strqry + " ,S.StudentStatusID,S.EMail,S.SSN from Students AS S  ";
                              strqry = strqry + " where S.StudentStatusID='" + StatusCheckValue + "' and (  S.LastName+', '+ S.FirstName  like '" + searchtext + "%' or S.StudentNo like '" + searchtext + "%' ";
                              strqry = strqry + " or S.EMail like '" + searchtext + "%' or S.SSN like '" + searchtext + "%') ";
                          }
                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = strqry;
                          CMD.Prepare();
                          SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                          DataAdapter.Fill(_ds);
                      }
                  }
              }
              catch (Exception ex)
              {
                  throw;
              }
              finally
              {
              }

              return _ds;
          }
          static public DataTable GetStudetnsStatus()
          {
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {
                          string strqry = string.Empty;
                          strqry = strqry + " select StudentStatusID,StudentStatus as StudentStatus  from StudentStatus   ";
                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = strqry;
                          CMD.Prepare();
                          SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                          DataAdapter.Fill(_ds);
                      }
                  }
              }
              catch (Exception ex)
              {
                  throw;
              }
              finally
              {
              }

              return _ds;
          }
         
        #endregion

        #region "Switch to anthor Lead"

          //============================================================
          static public DataTable SwitchToLeadBind()
          {
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {
                          string strqry = string.Empty;
                          strqry = strqry + " SELECT top 100 convert(varchar(50), L.LeadsID) as LeadsID,  ";
                          strqry = strqry + " Lead = L.LName + ', ' + L.FName + CASE WHEN LEN(L.MInitial) > 0 Then ' ' + L.MInitial Else '' End ,  ";
                          strqry = strqry + " ISNULL(L.StatusCode,'') AS LeadStatus  ";
                          strqry = strqry + " FROM Lead AS L LEFT JOIN LeadStatus AS LS ON L.StatusCode = LS.StatusCode  ";
                          strqry = strqry + " LEFT JOIN Employees AS E ON L.EmpID = E.EmpID  ";
                          strqry = strqry + " ORDER BY L.LName + ', ' + L.FName + ' ' + ISNULL(L.MInitial,'')  ";

                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = strqry;
                          CMD.Prepare();
                          SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                          DataAdapter.Fill(_ds);
                      }
                  }
              }
              catch (Exception ex)
              {
                  throw;
              }
              finally
              {
              }

              return _ds;
          }
          static public DataTable SwitchToLeadBindFilter(string searchtext, string StatusCheckValue)
          {
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {
                          string strqry = string.Empty;
                          if (StatusCheckValue == "0")
                          {
                              strqry = strqry + " SELECT top 20  convert(varchar(50), L.LeadsID) as LeadsID,  ";
                              strqry = strqry + " Lead = L.LName + ', ' + L.FName + CASE WHEN LEN(L.MInitial) > 0 Then ' ' + L.MInitial Else '' End ,  ";
                              strqry = strqry + " ISNULL(L.StatusCode,'') AS LeadStatus, AdRep = E.LName + ', ' + E.FName, L.City, L.State, L.Zip, L.SSN  ";
                              strqry = strqry + " FROM Lead AS L LEFT JOIN LeadStatus AS LS ON L.StatusCode = LS.StatusCode  ";
                              strqry = strqry + " LEFT JOIN Employees AS E ON L.EmpID = E.EmpID  ";
                              strqry = strqry + " where  L.LName + ', ' + L.FName like '" + searchtext + "%' or L.StatusCode like '" + searchtext + "%' ";
                              strqry = strqry + " or E.LName + ', ' + E.FName like '" + searchtext + "%' or L.City like '" + searchtext + "%' or ";
                              strqry = strqry + " L.State like '" + searchtext + "%' or L.Zip like '" + searchtext + "%' or L.SSN like '" + searchtext + "%' "; 
                              strqry = strqry + " ORDER BY L.LName + ', ' + L.FName + ' ' + ISNULL(L.MInitial,'')  ";
                          }
                          else 
                          {
                              strqry = strqry + " SELECT top 20  convert(varchar(50), L.LeadsID) as LeadsID,  ";
                              strqry = strqry + " Lead = L.LName + ', ' + L.FName + CASE WHEN LEN(L.MInitial) > 0 Then ' ' + L.MInitial Else '' End ,  ";
                              strqry = strqry + " ISNULL(L.StatusCode,'') AS LeadStatus, AdRep = E.LName + ', ' + E.FName, L.City, L.State, L.Zip, L.SSN  ";
                              strqry = strqry + " FROM Lead AS L LEFT JOIN LeadStatus AS LS ON L.StatusCode = LS.StatusCode  ";
                              strqry = strqry + " LEFT JOIN Employees AS E ON L.EmpID = E.EmpID  ";
                              strqry = strqry + " where   L.StatusCode ='" + StatusCheckValue + "' and (  L.LName + ', ' + L.FName like '" + searchtext + "%'  ";
                              strqry = strqry + " or E.LName + ', ' + E.FName like '" + searchtext + "%' or L.City like '" + searchtext + "%' or ";
                              strqry = strqry + " L.State like '" + searchtext + "%' or L.Zip like '" + searchtext + "%' or L.SSN like '" + searchtext + "%' )";
                              strqry = strqry + " ORDER BY L.LName + ', ' + L.FName + ' ' + ISNULL(L.MInitial,'') ";
                          }



                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = strqry;
                          CMD.Prepare();
                          SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                          DataAdapter.Fill(_ds);
                      }
                  }
              }
              catch (Exception ex)
              {
                  throw;
              }
              finally
              {
              }

              return _ds;
          }
          static public DataTable GetLeadStatus()
          {
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {
                          string strqry = string.Empty;
                          strqry = strqry + " select '0' as StatusCode,'Select All' as StatusCodeDesc  from  LeadStatus union   ";
                          strqry = strqry + " select StatusCode,StatusCode as StatusCodeDesc  from LeadStatus   ";
                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = strqry;
                          CMD.Prepare();
                          SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                          DataAdapter.Fill(_ds);
                      }
                  }
              }
              catch (Exception ex)
              {
                  throw;
              }
              finally
              {
              }

              return _ds;
          }
          #endregion

        #region "Department Compose SMS  Get Mobile No"
          static public DataTable GetStudentMobileNo(string searchtext)
          {
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {
                          string strqry = string.Empty;
                          strqry = strqry + " select top 20 S.LastName+','+S.FirstName As FullName,S.StudentNo,S.MobilePhone as MobilePhone , ";
                          strqry=strqry+" (Select StudentStatus from StudentStatus where StudentStatusID=S.StudentStatusID) as Status ";
                          strqry = strqry + " ,S.EMail,S.Email2 from Students As S ";
                          strqry = strqry + " where  (S.MobilePhone is not null and S.MobilePhone<>'')  and (S.LastName+','+S.FirstName like '" + searchtext + "%' or S.StudentNo like '" + searchtext + "%' ";
                          strqry = strqry + " or S.MobilePhone like '" + searchtext + "%'  ";
                          strqry = strqry + " or (Select StudentStatus from StudentStatus where StudentStatusID=S.StudentStatusID)  like '" + searchtext + "%' or ";
                          strqry = strqry + " S.MobilePhone like '" + searchtext + "%' or S.EMail like '" + searchtext + "%' ) "; 
                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = strqry;
                          CMD.Prepare();
                          SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                          DataAdapter.Fill(_ds);
                          

                      }
                  }
              }
              catch (Exception ex)
              {
                  throw;
              }
              finally
              {
              }

              return _ds;
          }
          static public DataTable GetLeadMobileNo(string searchtext)
          {
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {
                          string strqry = string.Empty;
                          strqry = strqry + " SELECT top 20    "; 
                          strqry = strqry + " (L.LName + ', ' + L.FName + CASE WHEN LEN(L.MInitial) > 0 Then ' ' + L.MInitial Else '' End) As FullName ,  ";
                          strqry = strqry + " ISNULL(L.StatusCode,'') AS Status,L.Email as EMail, L.Email2, E.LName + ', ' + E.FName as AdRep,L.PhoneMobile as   MobilePhone  "; 
                          strqry = strqry + " FROM Lead AS L LEFT JOIN LeadStatus AS LS ON L.StatusCode = LS.StatusCode   LEFT JOIN Employees AS E  "; 
                          strqry = strqry + " ON L.EmpID = E.EmpID    ";
                          strqry = strqry + " where (L.PhoneMobile is not null and L.PhoneMobile<>'')  and ( L.LName + ', ' + L.FName like  '" + searchtext + "%' or L.StatusCode like  '" + searchtext + "%'  or E.LName + ', ' + E.FName like  '" + searchtext + "%'    ";
                          strqry = strqry + " or L.City like  '" + searchtext + "%'  or   ";
                          strqry = strqry + " L.State like  '" + searchtext + "%'  or L.Zip like '" + searchtext + "%' or L.SSN like  '" + searchtext + "%')   ";  
                          strqry = strqry + " ORDER BY L.LName + ', ' + L.FName + ' ' + ISNULL(L.MInitial,'')    "; 
                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = strqry;
                          CMD.Prepare();
                          SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                          DataAdapter.Fill(_ds);

                      }
                  }
              }
              catch (Exception ex)
              {
                  throw;
              }
              finally
              {
              }

              return _ds;
          }
        
        #endregion

        #region "Department Compose Email Get  Email Id"
          static public DataTable GetStudentEamilId(string searchtext)
          {
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {
                          string strqry = string.Empty;
                          strqry = strqry + " select top 20 S.LastName+','+S.FirstName As FullName,S.StudentNo,Isnull(S.MobilePhone,'0000000000') as MobilePhone , ";
                          strqry = strqry + " (Select StudentStatus from StudentStatus where StudentStatusID=S.StudentStatusID) as Status ";
                          strqry = strqry + " ,S.EMail from Students As S ";
                          strqry = strqry + " where (S.LastName+','+S.FirstName like '" + searchtext + "%' or S.StudentNo like '" + searchtext + "%' ";
                          strqry = strqry + " or S.MobilePhone like '" + searchtext + "%'  ";
                          strqry = strqry + " or (Select StudentStatus from StudentStatus where StudentStatusID=S.StudentStatusID)  like '" + searchtext + "%' or ";
                          strqry = strqry + " S.MobilePhone like '" + searchtext + "%' or S.EMail like '" + searchtext + "%' ) ";
                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = strqry;
                          CMD.Prepare();
                          SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                          DataAdapter.Fill(_ds);


                      }
                  }
              }
              catch (Exception ex)
              {
                  throw;
              }
              finally
              {
              }

              return _ds;
          }
          static public DataTable GetLeadEamilId(string searchtext)
          {
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {
                          string strqry = string.Empty;
                          strqry = strqry + " SELECT top 20    ";
                          strqry = strqry + " (L.LName + ', ' + L.FName + CASE WHEN LEN(L.MInitial) > 0 Then ' ' + L.MInitial Else '' End) As FullName ,  ";
                          strqry = strqry + " ISNULL(L.StatusCode,'') AS Status,L.Email as EMail,L.PhoneMobile as   MobilePhone  ";
                          strqry = strqry + " FROM Lead AS L LEFT JOIN LeadStatus AS LS ON L.StatusCode = LS.StatusCode   LEFT JOIN Employees AS E  ";
                          strqry = strqry + " ON L.EmpID = E.EmpID    ";
                          strqry = strqry + " where  ( L.LName + ', ' + L.FName like '" + searchtext + "%' or L.StatusCode like  '" + searchtext + "%'  or E.LName + ', ' + E.FName like  '" + searchtext + "%'    ";
                          strqry = strqry + " or L.City like  '" + searchtext + "%'  or   ";
                          strqry = strqry + " L.State like  '" + searchtext + "%'  or L.Zip like '" + searchtext + "%' or L.SSN like  '" + searchtext + "%')   ";
                          strqry = strqry + " ORDER BY L.LName + ', ' + L.FName    ";
                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = strqry;
                          CMD.Prepare();
                          SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                          DataAdapter.Fill(_ds);

                      }
                  } 
              }
              catch (Exception ex)
              {
                  throw;
              }
              finally
              {
              }

              return _ds;
          }

          #endregion

        #region "switch to department"
          static public DataTable GetListDepartmentforswitch()
          {

              DataTable _ds = new DataTable();

              try
              {
                  using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {

                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = "  select DeptId,DeptDescription from Departments order by DeptDescription Asc ";
                          CMD.Prepare();
                          SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                          DataAdapter.Fill(_ds);

                      }
                  }
              }
              catch (Exception ex)
              {

              }
              finally
              {
              }

              return _ds;
          }
        #endregion
    }
  
}