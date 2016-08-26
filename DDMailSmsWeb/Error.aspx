<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="DDMailSmsWeb.Error" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<head id="Head1" runat="server">
    <title>Error</title>
    <link href="Content/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Content/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script src="Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="Content/js/bootstrap.js" type="text/javascript"></script>
     <style type="text/css">
        * {
            margin: 0%;
            padding: 0%;
        }

        body {
            width: 100%;
            height: 100%;
            font-family: Arial, Helvetica, sans-serif;
        }

        .thank-page {
            float: left;
            width: 99%;
            height: auto;
            margin: 0px 0.50% 0px 0.50%;
        }

        .wel-bg {
            float: left;
            width: 100%;
            height: auto;
            background-color: #D1E7FC;
            padding: 0px 0px 220px 0px;
        }

        .thank-img {
            float: left;
            width: 250px;
            height: 250px;
            margin: 0px 0px 0px 100px;
            opacity: 0.8;
        }

        .thank-htext {
            float: left;
            width: 100%;
            height: auto;
            margin-top: 50px;
            font-size: 37px;
            font-weight: bold;
            color: #08c;
            text-align: center;
            font-family: Aparajita;
        }

        .thank-text {
            float: left;
            width: 100%;
            height: auto;
            font-size: 15px;
            color: #000;
           
        }
         .style1
         {
             width: 195px;
         }
    </style>
 </head>

<body>

        
    <form id="form2" runat="server"> 
         <telerik:radscriptmanager id="RSMError" runat="server"></telerik:radscriptmanager>
         <div class="container-fluid">
          <div style="float:left; width:100%; height:8px; background-color:#002561;"></div>
          <div class="row">
          <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12 col-lg-offset-3 col-md-offset-3 col-sm-offset-2 col-xs-offset-0 text-center">
        
                      
          

              <asp:Image ID="Imglogo" runat="server" class="log-mt" Width="265px" Height="97px" />
              
              <br /><br /><br />
              <h3 class="hed-text">
                   Following Error :
              </h3>
              <p>
                   <telerik:RadLabel ID="RlblTempResult" runat="server" ForeColor="#cc0000"></telerik:RadLabel>
                             
              </p>
              

              </div>
              </div>

               <div class="row footimg-pos">
          <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding:0px;">
              
          </div>
          </div>


              <div class="row foot-pos">
          <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 foot-bg" >
          <div class="row">
          <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" style="padding: 0px;">
             || Powered by Diamond SIS, Inc. ©2016 ||
          </div>
          </div>
           
          </div>
          </div>
              </div>

          



  

    </form>

 
    
    
</body>
</html>
    