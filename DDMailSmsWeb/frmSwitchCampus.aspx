<%@ Page Title="" Language="C#" MasterPageFile="~/MainDDMailSMSMaster.Master" AutoEventWireup="true" CodeBehind="frmSwitchCampus.aspx.cs" Inherits="DDMailSmsWeb.frmSwitchCampus" %>
<asp:Content ID="ConCPhead" ContentPlaceHolderID="CPhead" runat="server">
          <script type="text/javascript">
              function OnClientSelectedIndexChangedCB(sender, eventArgs) {

                  var item = eventArgs.get_item();


                  var ddlfrom = item.get_text().includes("Lcol");

                  if (ddlfrom === true) {

                      var splitdata = item.get_text().split("<ul style='z-index:100'><li class='Lcol1'>");
                      var finalres = splitdata[1].split("<li");
                      var POSCode = finalres[0].replace("</li>", "");

                      sender.set_text(POSCode);
                      console.log(item.get_value());
                      sender.set_value(item.get_value());
                  }
                  if (ddlfrom === false) {
                      var splitdata1 = item.get_text().split("<ul style='z-index:100'><li class='col1'>");
                      var finalres1 = splitdata1[1].split("<li");
                      var POSCode1 = finalres1[0].replace("</li>", "");

                      sender.set_text(POSCode1);
                      console.log(item.get_value());
                      sender.set_value(item.get_value());
                  }

              }
        </script>

</asp:Content>
<asp:Content ID="ConCPHDetail" ContentPlaceHolderID="CPHDetail" runat="server">
<telerik:RadAjaxLoadingPanel ID="RALPfrmSwitchCampus" runat="server" Height="75px"
    Width="75px" Transparency="50">
    </telerik:RadAjaxLoadingPanel>
   
    <telerik:RadAjaxManagerProxy ID="RAMfrmSwitchCampus" runat="server">
      <AjaxSettings>
       <telerik:AjaxSetting AjaxControlID="pfrmSwitchCampus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pfrmSwitchCampus" LoadingPanelID="RALPfrmSwitchCampus" ></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
 
    <asp:Panel runat="server" ID="pfrmSwitchCampus">
       
       <telerik:RadPageLayout runat="server" ID="RPLayoutSwitchCampus" GridType="Fluid">

             <Rows>
                    <telerik:LayoutRow ID="LRlblSwitchCampus" runat="server" style="margin-top:4px;margin-left:2px;margin-right:2px" >
                        <Columns>
                             <telerik:LayoutColumn   Span="2" HiddenXs="true" SpanSm="2"  >
                            </telerik:LayoutColumn>
                                <telerik:LayoutColumn   Span="5"  SpanXs="12" SpanSm="5" >
                                       <telerik:RadLabel ID="lblSwitchCampusResult" runat="server" ForeColor="#cc0000"></telerik:RadLabel>
                                </telerik:LayoutColumn>
                            <telerik:LayoutColumn   Span="5" HiddenXs="true" SpanSm="5"  >
                            </telerik:LayoutColumn> 
                        </Columns>
                    </telerik:LayoutRow>
              </Rows>
             <Rows>
                    <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px" >
                        <Columns>
                             <telerik:LayoutColumn   Span="3" HiddenXs="true" SpanSm="3"  >
                            </telerik:LayoutColumn>
                                <telerik:LayoutColumn   Span="6"  SpanXs="12" SpanSm="6" >
                                    <telerik:RadDropDownList RenderMode="Lightweight" ID="Rddlcampus"  Width="100%" runat="server" OnSelectedIndexChanged="Rddlcampus_SelectedIndexChanged" Font-Size="12px" DropDownHeight="200px"
                                      AutoPostBack ="true">
                                    </telerik:RadDropDownList>
                                    <asp:RequiredFieldValidator runat="server" ID="RFVRddlcampus" InitialValue="Select Campus"
                                        ControlToValidate="Rddlcampus" ValidationGroup="VGSwitchcampus" Display="Dynamic" ForeColor="#ff0000"
                                        ErrorMessage="Please select campus" CssClass="validator" />
                                </telerik:LayoutColumn>
                             <telerik:LayoutColumn   Span="3" HiddenXs="true" SpanSm="3"  >
                            </telerik:LayoutColumn> 
                        </Columns>
                    </telerik:LayoutRow>
              </Rows>
             <Rows>
                    <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px">
                        <Columns>
                                   <telerik:LayoutColumn   Span="3" HiddenXs="true" SpanSm="3"  >
                            </telerik:LayoutColumn>
                                <telerik:LayoutColumn   Span="6"  SpanXs="12" SpanSm="6" >
                                        <telerik:RadDropDownList RenderMode="Lightweight" ID="RddlDept"  Width="100%" runat="server" Font-Size="12px" DropDownHeight="200px"
                                        AutoPostBack="false" DefaultMessage="Select Departments" >
                                        </telerik:RadDropDownList> 
                                        <asp:RequiredFieldValidator runat="server" ID="RFVRddlDept" InitialValue="Select Departments"
                                        ControlToValidate="RddlDept" ValidationGroup="VGSwitchcampus" Display="Dynamic" ForeColor="#ff0000"
                                        ErrorMessage="Please select department" CssClass="validator" />
                               </telerik:LayoutColumn>
                              <telerik:LayoutColumn   Span="3" HiddenXs="true" SpanSm="3"  >
                            </telerik:LayoutColumn> 
                        </Columns>
                    </telerik:LayoutRow>
             </Rows>
             <Rows>
                    <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px">
                        <Columns>
                              <telerik:LayoutColumn   Span="3" HiddenXs="true" SpanSm="3"  >
                            </telerik:LayoutColumn>
                                <telerik:LayoutColumn   Span="6"  SpanXs="12" SpanSm="6" >
                                    <telerik:RadButton ID="RBSelectStudent" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                    Text="Student" GroupName="StandardButton" AutoPostBack="true" Checked="true" OnClick="RBSelectStudent_Click">
                                    </telerik:RadButton> 
                                    <telerik:RadButton ID="RBSelectLead" runat="server" ToggleType="Radio"  Checked="false"
                                    Text="Lead" GroupName="StandardButton" ButtonType="ToggleButton" AutoPostBack="true" OnClick="RBSelectLead_Click">
                                    </telerik:RadButton>
                                    <br /> 
                                    <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="RCBStudentsLeadSearch" Height="300px" Width="100%" 
                                    ZIndex="10000000" ItemsPerRequest="10" AutoPostBack="true"  
                                    EnableLoadOnDemand  ="true" ShowMoreResultsBox="true"  OnClientSelectedIndexChanged="OnClientSelectedIndexChangedCB" 
                                    EnableVirtualScrolling="true" AllowCustomText="True" AppendDataBoundItems="True" Filter="Contains" ValidateRequest="false" >
                                    <WebServiceSettings Path="~/StudentLeadSelection.asmx"  />
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RFVRCBStudentsLeadSearch" 
                                        ControlToValidate="RCBStudentsLeadSearch" ValidationGroup="VGSwitchcampus" Display="Dynamic" 
                                        ForeColor="#ff0000"
                                        ErrorMessage="Please select student or lead" CssClass="validator" />
                             </telerik:LayoutColumn>
                            <telerik:LayoutColumn   Span="3" HiddenXs="true" SpanSm="3"  >
                            </telerik:LayoutColumn>  
                        </Columns>
                    </telerik:LayoutRow>
             </Rows>
             <Rows>
                    <telerik:LayoutRow style="margin-top:4px;margin-bottom:4px;margin-left:2px;margin-right:2px">
                        <Columns>
                          <telerik:LayoutColumn   Span="3" HiddenXs="true" SpanSm="3"  >
                            </telerik:LayoutColumn>
                                <telerik:LayoutColumn   Span="6"  SpanXs="12" SpanSm="6" >
                                    <telerik:RadButton ID="btnSwitchCampus" ValidationGroup="VGSwitchcampus"  OnClick="btnSwitchCampus_Click"
                                    Width="100%" runat="server" Text="Switch campus" >
                                        <Icon PrimaryIconCssClass="rbRefresh"></Icon>
                                    </telerik:RadButton>
                              </telerik:LayoutColumn> 
                              <telerik:LayoutColumn   Span="3" HiddenXs="true" SpanSm="3"  >
                             </telerik:LayoutColumn> 
                        </Columns>

                    </telerik:LayoutRow>
             </Rows>
   
       </telerik:RadPageLayout>
     </asp:Panel>

</asp:Content>
