<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmlogin.aspx.cs" Inherits="DDMailSmsWeb.frmlogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" >
    <title>Login</title>
    <meta name="viewport" content="initial-scale=1.0, minimum-scale=1, maximum-scale=1.0, user-scalable=no" />
 
    <link href="styles/login/style.css" rel="stylesheet" />
     <link href="styles/login/Content.css" rel="stylesheet" />
   
<script type="text/javascript" >
    function OnKeyPress(sender, args) {
        if (args.get_keyCode() == 13) {
            $find("<%=RbtnLogin.ClientID %>").click();
        }
    }
</script>

   
</head>
<body>
    <form id="form1" runat="server">
     <telerik:radscriptmanager id="RSMfrmlogin" runat="server"></telerik:radscriptmanager>

    <telerik:RadAjaxLoadingPanel ID="RALPfrmlogin" runat="server" Height="1000px"
        Width="100%"  Transparency="50">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxManager ID="RAMfrmlogin" runat="server" >
      <AjaxSettings>
       <telerik:AjaxSetting AjaxControlID="loginArea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="loginArea" LoadingPanelID="RALPfrmlogin"  ></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
       
        <asp:Panel ID="pfrmlogin" runat="server" >
              
                            <div class="Header"></div><br /><br /><br /><br /><br />
           <div id="loginArea" runat="server" class="login-card divcontainer"  >
            
              <br />         
                
                <telerik:RadPageLayout ID="RadPageLayout1"  runat="server"    GridType="Fluid">
                     <Rows>
                        <telerik:LayoutRow    >
                            <Columns>
                               <telerik:LayoutColumn   Span="12"    >
                                   <div style="text-align: center;"><img src="images/Login-icon.png" /></div>
                                   
                                    </telerik:LayoutColumn>
                            </Columns>
                         </telerik:LayoutRow>
                   </Rows>
                     <Rows>
                        <telerik:LayoutRow>
                            <Columns>
                               <telerik:LayoutColumn   Span="12"    >
                                    <asp:RequiredFieldValidator ID="RFVRTxtUsername" runat="server" Display="Dynamic" ValidationGroup="VGlogin"
                                    ControlToValidate="RTxtUsername" ErrorMessage="Please enter Username!" ForeColor="Red"  />
                                  <telerik:RadTextBox RenderMode="Mobile" ID="RTxtUsername" MaxLength="20" runat="server" Width="100%"   
                                        EmptyMessage="Username">
                                         <ClientEvents OnKeyPress="OnKeyPress" />
                                    </telerik:RadTextBox>
                                  
                               </telerik:LayoutColumn>
                            </Columns>
                         </telerik:LayoutRow>
                   </Rows>
                         <Rows>
                        <telerik:LayoutRow>
                            <Columns>
                               <telerik:LayoutColumn   Span="12">
                                    <asp:RequiredFieldValidator ID="RFVRRTxtPassword" runat="server" Display="Dynamic" ValidationGroup="VGlogin"
                                      ControlToValidate="RTxtPassword" ErrorMessage="Please enter Password!" ForeColor="Red"  />
                           
                               <telerik:RadTextBox EmptyMessage="Password"  RenderMode="Mobile" ID="RTxtPassword" runat="server"  
                                     style="padding:10px;height: 46px;font-size: 16px;width: 100%;" TextMode="Password"  Width="100%">
                                   <ClientEvents OnKeyPress="OnKeyPress" />
                                  
                                    </telerik:RadTextBox>
                                 
                                   <telerik:RadLabel ID="RlblResult" runat="server" ForeColor="#cc0000"></telerik:RadLabel>  
                             </telerik:LayoutColumn>
                            </Columns>
                         </telerik:LayoutRow>
                   </Rows>
                      <Rows>
                        <telerik:LayoutRow ID="LayoutRow1"      >
                            <Columns>
                               <telerik:LayoutColumn   Span="12"    >
                                    <telerik:RadButton RenderMode="Lightweight" ID="RbtnLogin" UseSubmitBehavior="true"  runat="server" Primary="true"   OnClick="RbtnLogin_Click"
                                    style="height: 44px;font-size: 16px;"    Text="Login"   Width="100%" ValidationGroup="VGlogin" >
                                       
                                    </telerik:RadButton>
                               </telerik:LayoutColumn>               
                            </Columns>
                         </telerik:LayoutRow>
                   </Rows>
                </telerik:RadPageLayout>
               
                
           </div> 
           
                            <div class="footer">|| Powered by Diamond SIS, Inc. ©2016 ||</div>
           
        </asp:Panel>
    </form>
</body>
</html>
