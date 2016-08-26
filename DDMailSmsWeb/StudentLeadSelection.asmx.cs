using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Telerik.Web.UI;

namespace DDMailSmsWeb
{
   [ScriptService]
    public class StudentLeadSelection : System.Web.Services.WebService
    {

        /* get Student master page search*/
         [WebMethod(EnableSession = true)]
         public RadComboBoxData GetStudents(RadComboBoxContext context)
        {
            string searchtext = string.Empty;
            searchtext = Convert.ToString(context.Text);

            RadComboBoxData comboData = new RadComboBoxData();

            
            if (HttpContext.Current.Session["GlobalValueKey"] != null)
            {
                DataTable data = new DataTable();

                using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {
                        string strqry = string.Empty;
                        strqry = strqry + " select  S.StudentID,S.LastName+', '+ S.FirstName  as FullName,convert(varchar(50), S.StudentNo) as StudentNo,  ";
                        strqry = strqry + " (Select StudentStatus from StudentStatus where StudentStatusID=S.StudentStatusID) as StudentStatus  ";
                        strqry = strqry + " ,S.StudentStatusID,S.EMail,S.SSN from Students AS S  ";
                        strqry = strqry + " where  S.LastName+', '+ S.FirstName  like '" + searchtext + "%' or S.StudentNo like '" + searchtext + "%' ";
                        strqry = strqry + " or (Select StudentStatus from StudentStatus where StudentStatusID=S.StudentStatusID) like '" + searchtext + "%' ";
                        strqry = strqry + " or S.EMail like '" + searchtext + "%' or S.SSN like '" + searchtext + "%'  ORDER BY S.LastName+', '+ S.FirstName ";
                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = strqry;
                        CMD.Prepare();
                        SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                        DataAdapter.Fill(data);

                    }
                }
                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > data.Rows.Count)
                    {
                        endOffset = data.Rows.Count;
                    }
                    if (endOffset == data.Rows.Count)
                    {
                        comboData.EndOfItems = true;
                    }
                    else
                    {
                        comboData.EndOfItems = false;
                    }
                    result = new List<RadComboBoxItemData>(endOffset - itemOffset);

                    //=================================================================
                    /*List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                    foreach(row in rows)
                    {
                    RadComboBoxItemData itemData = new RadComboBoxItemData();
                    itemData.Attributes.Add("HostName", row["HostName"].ToString());
                    itemData.Value = row["ID"].ToString();
                    result.Add(itemData);
                    }
                    return result;*/
                    //===================================================================
                    for (int i = itemOffset; i < endOffset; i++)
                    {
                        RadComboBoxItemData itemData = new RadComboBoxItemData();
                        //  itemData.Text = Convert.ToString(data.Rows[i]["FullName"]);
                        string itemtext = string.Empty;
                        itemtext = itemtext + "<ul style='z-index:100'>";
                        itemtext = itemtext + "<li class='col1'>";
                        itemtext = itemtext + Convert.ToString(data.Rows[i]["FullName"]) + "</li>";
                        itemtext = itemtext + "<li class='col2'>";
                        itemtext = itemtext + Convert.ToString(data.Rows[i]["StudentNo"]) + "</li>";
                        itemtext = itemtext + "<li class='col3'>";
                        itemtext = itemtext + Convert.ToString(data.Rows[i]["StudentStatus"]) + "</li>   ";
                        itemtext = itemtext + "<li class='col4'>";
                        itemtext = itemtext + Convert.ToString(data.Rows[i]["EMail"]) + "</li>  ";
                        itemtext = itemtext + "</ul>";
                        itemData.Text = itemtext;
                        itemData.Value = Convert.ToString(data.Rows[i]["StudentNo"]);

                        result.Add(itemData);
                    }

                    if (data.Rows.Count > 0)
                    {

                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), data.Rows.Count.ToString());
                    }
                    else
                    {
                        comboData.Message = "No matches";
                    }
                }

                catch (Exception e)
                {
                    comboData.Message = e.Message;
                }

                comboData.Items = result.ToArray();
            }

          return comboData;
        }
        /* get Lead master page search*/
         [WebMethod(EnableSession = true)]
         public RadComboBoxData GetLeads(RadComboBoxContext context)
        {
            string searchtext = string.Empty;
            searchtext = Convert.ToString(context.Text);
            RadComboBoxData comboData = new RadComboBoxData();
            if (HttpContext.Current.Session["GlobalValueKey"] != null)
            {
                DataTable data = new DataTable();

                using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {
                        string strqry = string.Empty;


                        strqry = strqry + "  SELECT    L.LeadsID as LeadsID,  ";
                        strqry = strqry + " Lead = L.LName + ', ' + L.FName + CASE WHEN LEN(L.MInitial) > 0 Then ' ' + L.MInitial Else '' End ,  ";
                        strqry = strqry + " ISNULL(L.StatusCode,'') AS LeadStatus, AdRep = E.LName + ', ' + E.FName, L.City, L.State, L.Zip, L.SSN  ";
                        strqry = strqry + " FROM Lead AS L LEFT JOIN LeadStatus AS LS ON L.StatusCode = LS.StatusCode  ";
                        strqry = strqry + " LEFT JOIN Employees AS E ON L.EmpID = E.EmpID  ";
                        strqry = strqry + " where  L.LName + ', ' + L.FName like '" + searchtext + "%' or L.StatusCode like '" + searchtext + "%' ";
                        strqry = strqry + " or E.LName + ', ' + E.FName like '" + searchtext + "%' or L.City like '" + searchtext + "%' or ";
                        strqry = strqry + " L.State like '" + searchtext + "%' or L.Zip like '" + searchtext + "%' or L.SSN like '" + searchtext + "%' ";
                        strqry = strqry + " ORDER BY L.LName + ', ' + L.FName + ' ' + ISNULL(L.MInitial,'')  ";
                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = strqry;
                        CMD.Prepare();
                        SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                        DataAdapter.Fill(data);

                    }
                }
                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
         
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > data.Rows.Count)
                    {
                        endOffset = data.Rows.Count;
                    }
                    if (endOffset == data.Rows.Count)
                    {
                        comboData.EndOfItems = true;
                    }
                    else
                    {
                        comboData.EndOfItems = false;
                    }
                    result = new List<RadComboBoxItemData>(endOffset - itemOffset);

                    for (int i = itemOffset; i < endOffset; i++)
                    {
                        RadComboBoxItemData itemData = new RadComboBoxItemData();
                        string itemtext = string.Empty;
                        itemtext = itemtext + "<ul style='z-index:100'>";
                        itemtext = itemtext + "<li class='Lcol1'>";
                        itemtext = itemtext + Convert.ToString(data.Rows[i]["Lead"]);
                        itemtext = itemtext + "</li>";
                        itemtext = itemtext + "<li class='Lcol2'>";
                        itemtext = itemtext + Convert.ToString(data.Rows[i]["LeadsID"]);
                        itemtext = itemtext + "<li class='Lcol3'>";
                        itemtext = itemtext + Convert.ToString(data.Rows[i]["LeadStatus"]);
                        itemtext = itemtext + "<li class='Lcol4'>";
                        itemtext = itemtext + Convert.ToString(data.Rows[i]["AdRep"]);
                        itemtext = itemtext + "<li class='Lcol5'>";
                        itemtext = itemtext + Convert.ToString(data.Rows[i]["City"]);
                        itemtext = itemtext + "<li class='Lcol6'>";
                        itemtext = itemtext + Convert.ToString(data.Rows[i]["State"]);
                        itemtext = itemtext + "<li class='Lcol7'>";
                        itemtext = itemtext + Convert.ToString(data.Rows[i]["Zip"]);
                        itemtext = itemtext + "</ul>";
                        itemData.Text = itemtext;
                        itemData.Value = Convert.ToString(data.Rows[i]["LeadsID"]);

                        result.Add(itemData);
                    }

                    if (data.Rows.Count > 0)
                    {

                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), data.Rows.Count.ToString());
                    }
                    else
                    {
                        comboData.Message = "No matches";
                    }
                }

                catch (Exception e)
                {
                    comboData.Message = e.Message;
                }

                comboData.Items = result.ToArray();
            }
            return comboData;
        }
        /* get Student email department sms write*/
         [WebMethod(EnableSession = true)]
         public RadComboBoxData GetStudentsEmailDept(RadComboBoxContext context)
        {
            string searchtext = string.Empty;
            searchtext = Convert.ToString(context.Text);

            RadComboBoxData comboData = new RadComboBoxData();

            
            if (HttpContext.Current.Session["GlobalValueKey"] != null)
            {
                DataTable data = new DataTable();

                using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {
                        //Last Name,First Name,Status
                        string strqry = string.Empty;
                        strqry = strqry + " select  S.StudentID,S.LastName+', '+ S.FirstName  as FullName,convert(varchar(50), S.StudentNo) as StudentNo,  ";
                        strqry = strqry + " (Select StudentStatus from StudentStatus where StudentStatusID=S.StudentStatusID) as StudentStatus  ";
                        strqry = strqry + "  ,S.EMail from Students AS S  ";
                        strqry = strqry + " where  ( S.EMail is not null and  S.EMail<>'') and (S.LastName+', '+ S.FirstName  like '" + searchtext + "%' ";
                        strqry = strqry + " or (Select StudentStatus from StudentStatus where StudentStatusID=S.StudentStatusID) like '" + searchtext + "%') ";
                        strqry = strqry + "   ORDER BY S.LastName+', '+ S.FirstName ";
                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = strqry;
                        CMD.Prepare();
                        SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                        DataAdapter.Fill(data);

                    }
                }
                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > data.Rows.Count)
                    {
                        endOffset = data.Rows.Count;
                    }
                    if (endOffset == data.Rows.Count)
                    {
                        comboData.EndOfItems = true;
                    }
                    else
                    {
                        comboData.EndOfItems = false;
                    }
                    result = new List<RadComboBoxItemData>(endOffset - itemOffset);

                    //=================================================================
                    /*List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                    foreach(row in rows)
                    {
                    RadComboBoxItemData itemData = new RadComboBoxItemData();
                    itemData.Attributes.Add("HostName", row["HostName"].ToString());
                    itemData.Value = row["ID"].ToString();
                    result.Add(itemData);
                    }
                    return result;*/
                    //===================================================================
                    for (int i = itemOffset; i < endOffset; i++)
                    {
                        RadComboBoxItemData itemData = new RadComboBoxItemData();
                        //  itemData.Text = Convert.ToString(data.Rows[i]["FullName"]);
                        string itemtext = string.Empty;
                        itemtext = itemtext + "<ul>";
                        itemtext = itemtext + "<li class='dpemailcol1'>";
                        itemtext = itemtext + Convert.ToString(data.Rows[i]["FullName"]) + "</li>";
                        itemtext = itemtext + "<li class='dpemailcol2'>";
                        itemtext = itemtext + Convert.ToString(data.Rows[i]["StudentStatus"]) + "</li>";
                        itemtext = itemtext + "<li class='dpemailcol3'>";
                        itemtext = itemtext + Convert.ToString(data.Rows[i]["Email"]) + "</li>   ";
                        itemtext = itemtext + "</ul>";
                        itemData.Text = itemtext;
                        itemData.Value = Convert.ToString(data.Rows[i]["Email"]);

                        result.Add(itemData);
                    }

                    

                    if (data.Rows.Count > 0)
                    {

                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), data.Rows.Count.ToString());
                    }
                    else
                    {
                        comboData.Message = "No matches";
                    }
                }

                catch (Exception e)
                {
                    comboData.Message = e.Message;
                }

                comboData.Items = result.ToArray();
            }

          return comboData;
        }
        /* get student Lead Email department sms write*/
         [WebMethod(EnableSession = true)]
         public RadComboBoxData GetLeadsEmailDept(RadComboBoxContext context)
         {
             string searchtext = string.Empty;
             searchtext = Convert.ToString(context.Text);

             RadComboBoxData comboData = new RadComboBoxData();


             if (HttpContext.Current.Session["GlobalValueKey"] != null)
             {
                 DataTable data = new DataTable();

                 using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                 {
                     using (SqlCommand CMD = new SqlCommand())
                     {
                         //Last Name,First Name,Status
                         string strqry = string.Empty;
                         strqry = strqry + " SELECT    ";
                         strqry = strqry + " (L.LName + ', ' + L.FName + CASE WHEN LEN(L.MInitial) > 0 Then ' ' + L.MInitial Else '' End) As FullName ,  ";
                         strqry = strqry + " ISNULL(L.StatusCode,'') AS Status,L.Email as  Email ";
                         strqry = strqry + " FROM Lead AS L LEFT JOIN LeadStatus AS LS ON L.StatusCode = LS.StatusCode   LEFT JOIN Employees AS E  ";
                         strqry = strqry + " ON L.EmpID = E.EmpID    ";
                         strqry = strqry + " where (L.Email is not null and L.Email<>'') and ( L.LName + ', ' + L.FName like '" + searchtext + "%' or L.StatusCode like  '" + searchtext + "%')      ";
                         strqry = strqry + " ORDER BY L.LName + ', ' + L.FName    ";
                      
                         CMD.Connection = connection;
                         CMD.CommandType = System.Data.CommandType.Text;
                         CMD.CommandText = strqry;
                         CMD.Prepare();
                         SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                         DataAdapter.Fill(data);

                     }
                 }
                 List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                 try
                 {
                     int itemsPerRequest = 10;
                     int itemOffset = context.NumberOfItems;
                     int endOffset = itemOffset + itemsPerRequest;
                     if (endOffset > data.Rows.Count)
                     {
                         endOffset = data.Rows.Count;
                     }
                     if (endOffset == data.Rows.Count)
                     {
                         comboData.EndOfItems = true;
                     }
                     else
                     {
                         comboData.EndOfItems = false;
                     }
                     result = new List<RadComboBoxItemData>(endOffset - itemOffset);

                     //=================================================================
                     /*List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                     foreach(row in rows)
                     {
                     RadComboBoxItemData itemData = new RadComboBoxItemData();
                     itemData.Attributes.Add("HostName", row["HostName"].ToString());
                     itemData.Value = row["ID"].ToString();
                     result.Add(itemData);
                     }
                     return result;*/
                     //===================================================================
                     for (int i = itemOffset; i < endOffset; i++)
                     {
                         RadComboBoxItemData itemData = new RadComboBoxItemData();
                         //  itemData.Text = Convert.ToString(data.Rows[i]["FullName"]);
                         string itemtext = string.Empty;
                         itemtext = itemtext + "<ul>";
                         itemtext = itemtext + "<li class='dpemailcol1'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["FullName"]) + "</li>";
                         itemtext = itemtext + "<li class='dpemailcol2'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["Status"]) + "</li>";
                         itemtext = itemtext + "<li class='dpemailcol3'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["Email"]) + "</li>   ";
                         itemtext = itemtext + "</ul>";
                         itemData.Text = itemtext;
                         itemData.Value = Convert.ToString(data.Rows[i]["Email"]);

                         result.Add(itemData);
                     }
                        



                     if (data.Rows.Count > 0)
                     {

                         comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), data.Rows.Count.ToString());
                     }
                     else
                     {
                         comboData.Message = "No matches";
                     }
                 }

                 catch (Exception e)
                 {
                     comboData.Message = e.Message;
                 }

                 comboData.Items = result.ToArray();
             }

             return comboData;
         }
        /* get student Mobile no department sms write*/
         [WebMethod(EnableSession = true)]
         public RadComboBoxData GetStudentsMobileDept(RadComboBoxContext context)
         {
             string searchtext = string.Empty;
             searchtext = Convert.ToString(context.Text);

             RadComboBoxData comboData = new RadComboBoxData();


             if (HttpContext.Current.Session["GlobalValueKey"] != null)
             {
                 DataTable data = new DataTable();

                 using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                 {
                     using (SqlCommand CMD = new SqlCommand())
                     {
                         //Last Name,First Name,Status
                         string strqry = string.Empty;
                         strqry = strqry + " select  S.StudentID,S.LastName+', '+ S.FirstName  as FullName,convert(varchar(50), S.StudentNo) as StudentNo,  ";
                         strqry = strqry + " (Select StudentStatus from StudentStatus where StudentStatusID=S.StudentStatusID) as StudentStatus  ";
                         strqry = strqry + "  ,S.MobilePhone from Students AS S  ";
                         strqry = strqry + " where  ( S.MobilePhone is not null and  S.MobilePhone<>'') and (S.LastName+', '+ S.FirstName  like '" + searchtext + "%' ";
                         strqry = strqry + " or (Select StudentStatus from StudentStatus where StudentStatusID=S.StudentStatusID) like '" + searchtext + "%' or  S.MobilePhone  like '" + searchtext + "%') ";
                         strqry = strqry + "   ORDER BY S.LastName+', '+ S.FirstName ";
                         CMD.Connection = connection;
                         CMD.CommandType = System.Data.CommandType.Text;
                         CMD.CommandText = strqry;
                         CMD.Prepare();
                         SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                         DataAdapter.Fill(data);

                     }
                 }
                 List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                 try
                 {
                     int itemsPerRequest = 10;
                     int itemOffset = context.NumberOfItems;
                     int endOffset = itemOffset + itemsPerRequest;
                     if (endOffset > data.Rows.Count)
                     {
                         endOffset = data.Rows.Count;
                     }
                     if (endOffset == data.Rows.Count)
                     {
                         comboData.EndOfItems = true;
                     }
                     else
                     {
                         comboData.EndOfItems = false;
                     }
                     result = new List<RadComboBoxItemData>(endOffset - itemOffset);

                     
                     for (int i = itemOffset; i < endOffset; i++)
                     {
                         RadComboBoxItemData itemData = new RadComboBoxItemData();
                         //  itemData.Text = Convert.ToString(data.Rows[i]["FullName"]);
                         string itemtext = string.Empty;
                         itemtext = itemtext + "<ul>";
                         itemtext = itemtext + "<li class='SMScol1'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["FullName"]) + "</li>";
                         itemtext = itemtext + "<li class='SMScol2'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["MobilePhone"]) + "</li>";
                         itemtext = itemtext + "<li class='SMScol3'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["StudentStatus"]) + "</li>   ";
                         itemtext = itemtext + "</ul>";
                         itemData.Text = itemtext;
                         itemData.Value = Convert.ToString(data.Rows[i]["MobilePhone"]);

                         result.Add(itemData);
                     }



                     if (data.Rows.Count > 0)
                     {

                         comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), data.Rows.Count.ToString());
                     }
                     else
                     {
                         comboData.Message = "No matches";
                     }
                 }

                 catch (Exception e)
                 {
                     comboData.Message = e.Message;
                 }

                 comboData.Items = result.ToArray();
             }

             return comboData;
         }
       /* get lead Mobile no department sms write*/
        [WebMethod(EnableSession = true)]
         public RadComboBoxData GetLeadsMobileDept(RadComboBoxContext context)
         {
             string searchtext = string.Empty;
             searchtext = Convert.ToString(context.Text);

             RadComboBoxData comboData = new RadComboBoxData();


             if (HttpContext.Current.Session["GlobalValueKey"] != null)
             {
                 DataTable data = new DataTable();

                 using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["GlobalValueKey"])))
                 {
                     using (SqlCommand CMD = new SqlCommand())
                     {
                         //Last Name,First Name,Status
                         string strqry = string.Empty;
                         strqry = strqry + " SELECT    ";
                         strqry = strqry + " (L.LName + ', ' + L.FName + CASE WHEN LEN(L.MInitial) > 0 Then ' ' + L.MInitial Else '' End) As FullName ,  ";
                         strqry = strqry + " ISNULL(L.StatusCode,'') AS Status,L.PhoneMobile as   MobilePhone ";
                         strqry = strqry + " FROM Lead AS L LEFT JOIN LeadStatus AS LS ON L.StatusCode = LS.StatusCode   LEFT JOIN Employees AS E  ";
                         strqry = strqry + " ON L.EmpID = E.EmpID    ";
                         strqry = strqry + " where (L.PhoneMobile is not null and L.PhoneMobile<>'') and ( L.LName + ', ' + L.FName like '" + searchtext + "%' or L.StatusCode like  '" + searchtext + "%' or L.PhoneMobile like  '" + searchtext + "%')      ";
                         strqry = strqry + " ORDER BY L.LName + ', ' + L.FName    ";

                         CMD.Connection = connection;
                         CMD.CommandType = System.Data.CommandType.Text;
                         CMD.CommandText = strqry;
                         CMD.Prepare();
                         SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                         DataAdapter.Fill(data);

                     }
                 }
                 List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                 try
                 {
                     int itemsPerRequest = 10;
                     int itemOffset = context.NumberOfItems;
                     int endOffset = itemOffset + itemsPerRequest;
                     if (endOffset > data.Rows.Count)
                     {
                         endOffset = data.Rows.Count;
                     }
                     if (endOffset == data.Rows.Count)
                     {
                         comboData.EndOfItems = true;
                     }
                     else
                     {
                         comboData.EndOfItems = false;
                     }
                     result = new List<RadComboBoxItemData>(endOffset - itemOffset);

                     for (int i = itemOffset; i < endOffset; i++)
                     {
                         RadComboBoxItemData itemData = new RadComboBoxItemData();
                         //  itemData.Text = Convert.ToString(data.Rows[i]["FullName"]);
                         string itemtext = string.Empty;
                         itemtext = itemtext + "<ul>";
                         itemtext = itemtext + "<li class='SMScol1'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["FullName"]) + "</li>";
                         itemtext = itemtext + "<li class='SMScol2'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["MobilePhone"]) + "</li>";
                         itemtext = itemtext + "<li class='SMScol3'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["Status"]) + "</li>   ";
                         itemtext = itemtext + "</ul>";
                         itemData.Text = itemtext;
                         itemData.Value = Convert.ToString(data.Rows[i]["MobilePhone"]);

                         result.Add(itemData);
                     }




                     if (data.Rows.Count > 0)
                     {

                         comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), data.Rows.Count.ToString());
                     }
                     else
                     {
                         comboData.Message = "No matches";
                     }
                 }

                 catch (Exception e)
                 {
                     comboData.Message = e.Message;
                 }

                 comboData.Items = result.ToArray();
             }

             return comboData;
         }
       /* Switch Campus*/
         [WebMethod(EnableSession = true)]
         public RadComboBoxData GetSwitchStudents(RadComboBoxContext context)
         {
             string searchtext = string.Empty;
             searchtext = Convert.ToString(context.Text);

             RadComboBoxData comboData = new RadComboBoxData();


             if (HttpContext.Current.Session["Switchcampusconstring"] != null)
             {
                 DataTable data = new DataTable();

                 using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["Switchcampusconstring"])))
                 {
                     using (SqlCommand CMD = new SqlCommand())
                     {
                         string strqry = string.Empty;
                         strqry = strqry + " select  S.StudentID,S.LastName+', '+ S.FirstName  as FullName,convert(varchar(50), S.StudentNo) as StudentNo,  ";
                         strqry = strqry + " (Select StudentStatus from StudentStatus where StudentStatusID=S.StudentStatusID) as StudentStatus  ";
                         strqry = strqry + " ,S.StudentStatusID,S.EMail,S.SSN from Students AS S  ";
                         strqry = strqry + " where  S.LastName+', '+ S.FirstName  like '" + searchtext + "%' or S.StudentNo like '" + searchtext + "%' ";
                         strqry = strqry + " or (Select StudentStatus from StudentStatus where StudentStatusID=S.StudentStatusID) like '" + searchtext + "%' ";
                         strqry = strqry + " or S.EMail like '" + searchtext + "%' or S.SSN like '" + searchtext + "%'  ORDER BY S.LastName+', '+ S.FirstName ";
                         CMD.Connection = connection;
                         CMD.CommandType = System.Data.CommandType.Text;
                         CMD.CommandText = strqry;
                         CMD.Prepare();
                         SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                         DataAdapter.Fill(data);

                     }
                 }
                 List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                 try
                 {
                     int itemsPerRequest = 10;
                     int itemOffset = context.NumberOfItems;
                     int endOffset = itemOffset + itemsPerRequest;
                     if (endOffset > data.Rows.Count)
                     {
                         endOffset = data.Rows.Count;
                     }
                     if (endOffset == data.Rows.Count)
                     {
                         comboData.EndOfItems = true;
                     }
                     else
                     {
                         comboData.EndOfItems = false;
                     }
                     result = new List<RadComboBoxItemData>(endOffset - itemOffset);

                     //=================================================================
                     /*List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                     foreach(row in rows)
                     {
                     RadComboBoxItemData itemData = new RadComboBoxItemData();
                     itemData.Attributes.Add("HostName", row["HostName"].ToString());
                     itemData.Value = row["ID"].ToString();
                     result.Add(itemData);
                     }
                     return result;*/
                     //===================================================================
                     for (int i = itemOffset; i < endOffset; i++)
                     {
                         RadComboBoxItemData itemData = new RadComboBoxItemData();
                         //  itemData.Text = Convert.ToString(data.Rows[i]["FullName"]);
                         string itemtext = string.Empty;
                         itemtext = itemtext + "<ul style='z-index:100'>";
                         itemtext = itemtext + "<li class='col1'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["FullName"]) + "</li>";
                         itemtext = itemtext + "<li class='col2'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["StudentNo"]) + "</li>";
                         itemtext = itemtext + "<li class='col3'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["StudentStatus"]) + "</li>   ";
                         itemtext = itemtext + "<li class='col4'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["EMail"]) + "</li>  ";
                         itemtext = itemtext + "</ul>";
                         itemData.Text = itemtext;
                         itemData.Value = Convert.ToString(data.Rows[i]["StudentNo"]);

                         result.Add(itemData);
                     }

                     if (data.Rows.Count > 0)
                     {

                         comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), data.Rows.Count.ToString());
                     }
                     else
                     {
                         comboData.Message = "No matches";
                     }
                 }

                 catch (Exception e)
                 {
                     comboData.Message = e.Message;
                 }

                 comboData.Items = result.ToArray();
             }

             return comboData;
         }
         [WebMethod(EnableSession = true)]
         public RadComboBoxData GetSwitchLeads(RadComboBoxContext context)
         {
             string searchtext = string.Empty;
             searchtext = Convert.ToString(context.Text);
             RadComboBoxData comboData = new RadComboBoxData();
             if (HttpContext.Current.Session["Switchcampusconstring"] != null)
             {
                 DataTable data = new DataTable();

                 using (SqlConnection connection = new SqlConnection(Convert.ToString(HttpContext.Current.Session["Switchcampusconstring"])))
                 {
                     using (SqlCommand CMD = new SqlCommand())
                     {
                         string strqry = string.Empty;


                         strqry = strqry + "  SELECT    L.LeadsID as LeadsID,  ";
                         strqry = strqry + " Lead = L.LName + ', ' + L.FName + CASE WHEN LEN(L.MInitial) > 0 Then ' ' + L.MInitial Else '' End ,  ";
                         strqry = strqry + " ISNULL(L.StatusCode,'') AS LeadStatus, AdRep = E.LName + ', ' + E.FName, L.City, L.State, L.Zip, L.SSN  ";
                         strqry = strqry + " FROM Lead AS L LEFT JOIN LeadStatus AS LS ON L.StatusCode = LS.StatusCode  ";
                         strqry = strqry + " LEFT JOIN Employees AS E ON L.EmpID = E.EmpID  ";
                         strqry = strqry + " where  L.LName + ', ' + L.FName like '" + searchtext + "%' or L.StatusCode like '" + searchtext + "%' ";
                         strqry = strqry + " or E.LName + ', ' + E.FName like '" + searchtext + "%' or L.City like '" + searchtext + "%' or ";
                         strqry = strqry + " L.State like '" + searchtext + "%' or L.Zip like '" + searchtext + "%' or L.SSN like '" + searchtext + "%' ";
                         strqry = strqry + " ORDER BY L.LName + ', ' + L.FName + ' ' + ISNULL(L.MInitial,'')  ";
                         CMD.Connection = connection;
                         CMD.CommandType = System.Data.CommandType.Text;
                         CMD.CommandText = strqry;
                         CMD.Prepare();
                         SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                         DataAdapter.Fill(data);

                     }
                 }
                 List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);

                 try
                 {
                     int itemsPerRequest = 10;
                     int itemOffset = context.NumberOfItems;
                     int endOffset = itemOffset + itemsPerRequest;
                     if (endOffset > data.Rows.Count)
                     {
                         endOffset = data.Rows.Count;
                     }
                     if (endOffset == data.Rows.Count)
                     {
                         comboData.EndOfItems = true;
                     }
                     else
                     {
                         comboData.EndOfItems = false;
                     }
                     result = new List<RadComboBoxItemData>(endOffset - itemOffset);

                     for (int i = itemOffset; i < endOffset; i++)
                     {
                         RadComboBoxItemData itemData = new RadComboBoxItemData();
                         string itemtext = string.Empty;
                         itemtext = itemtext + "<ul style='z-index:100'>";
                         itemtext = itemtext + "<li class='Lcol1'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["Lead"]);
                         itemtext = itemtext + "</li>";
                         itemtext = itemtext + "<li class='Lcol2'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["LeadsID"]);
                         itemtext = itemtext + "<li class='Lcol3'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["LeadStatus"]);
                         itemtext = itemtext + "<li class='Lcol4'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["AdRep"]);
                         itemtext = itemtext + "<li class='Lcol5'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["City"]);
                         itemtext = itemtext + "<li class='Lcol6'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["State"]);
                         itemtext = itemtext + "<li class='Lcol7'>";
                         itemtext = itemtext + Convert.ToString(data.Rows[i]["Zip"]);
                         itemtext = itemtext + "</ul>";
                         itemData.Text = itemtext;
                         itemData.Value = Convert.ToString(data.Rows[i]["LeadsID"]);

                         result.Add(itemData);
                     }

                     if (data.Rows.Count > 0)
                     {

                         comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), data.Rows.Count.ToString());
                     }
                     else
                     {
                         comboData.Message = "No matches";
                     }
                 }

                 catch (Exception e)
                 {
                     comboData.Message = e.Message;
                 }

                 comboData.Items = result.ToArray();
             }
             return comboData;
         }
       
    }
}

