<%@ Page Title="Department Compose SMS" Language="C#" MasterPageFile="~/MainDDMailSMSMaster.Master" AutoEventWireup="true" CodeBehind="frmdepartmentcomposesms.aspx.cs" Inherits="DDMailSmsWeb.TDEPTComposeSMS" %>
<asp:Content ID="ConCPhead" ContentPlaceHolderID="CPhead" runat="server">
       <script type="text/javascript">
           function Validate(e, t) {
               var charCode = 0;
               if (t) {
                   charCode = t.which;

                   if (charCode == 8) {

                       var dateInput = document.getElementById('<%= REMessageSMSEditorDept.ClientID %>');
                     console.log(dateInput.value.length);
                     if (document.getElementById('<%= RLREditorSMSLength.ClientID %>').innerHTML != '0') {
                        document.getElementById('<%= RLREditorSMSLength.ClientID %>').innerHTML = (dateInput.value.length - 1).toString();
                    }
                }
                else {
                    var dateInput = document.getElementById('<%= REMessageSMSEditorDept.ClientID %>');
                     console.log(dateInput.value.length);
                     document.getElementById('<%= RLREditorSMSLength.ClientID %>').innerHTML = (dateInput.value.length + 1).toString();
                }
            }
           }
           function OnClientSelectedIndexChangedSLEMAILSeach(sender, eventArgs) {

               var item = eventArgs.get_item();
               sender.set_text('');
               sender.set_value(item.get_value());

           }
      </script>
</asp:Content>
<asp:Content ID="ConCPHDetail" ContentPlaceHolderID="CPHDetail" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RALPfrmdepartmentcomposesms" runat="server" Height="75px"
    Width="75px" Transparency="50">
    </telerik:RadAjaxLoadingPanel>
      <telerik:RadAjaxManagerProxy ID="RAMfrmdepartmentcomposesms" runat="server">
      <AjaxSettings>
       <telerik:AjaxSetting AjaxControlID="pfrmdepartmentcomposesms">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pfrmdepartmentcomposesms" LoadingPanelID="RALPfrmdepartmentcomposesms" ></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
      </telerik:RadAjaxManagerProxy>


    <asp:Panel runat="server" ID="pfrmdepartmentcomposesms">
      <telerik:RadPageLayout runat="server" ID="RPLayoutApplyTempHeader" GridType="Fluid">
             <Rows>
                <telerik:LayoutRow  style="margin-top:4px;margin-left:2px;margin-right:2px">
                    <Columns>
                         <telerik:LayoutColumn   Span="2" HiddenXs="true" SpanSm="2"  >
                         </telerik:LayoutColumn>
                         <telerik:LayoutColumn   Span="5"  SpanXs="9" SpanSm="5"  >
                      
                              <telerik:RadDropDownList RenderMode="Lightweight" ID="RddlApplySMSTemplateDept"  Width="100%" runat="server"  DropDownHeight="200px" AutoPostBack="false">
                            </telerik:RadDropDownList>
                        </telerik:LayoutColumn>
                         <telerik:LayoutColumn   Span="1"  SpanXs="3" SpanSm="3"     >
                               <telerik:RadButton RenderMode="Lightweight" runat="server" Text="Apply" style="margin-left:2px" 
                                   OnClick="RbtnSMSTempApplyDept_Click" ID="RbtnSMSTempApplyDept" >
                                   <Icon PrimaryIconCssClass="rbOk"></Icon>
                               </telerik:RadButton>
                        </telerik:LayoutColumn>
                           <telerik:LayoutColumn   Span="1"  SpanXs="3" HiddenXs="true" HiddenSm="true"     >
                              <telerik:RadButton RenderMode="Lightweight"  OnClick="RbtmSMSTemplateClearDept_Click" runat="server" Text="Reset"   ID="RbtmSMSTemplateClearDept"  >
                               <Icon PrimaryIconCssClass="rbCancel"></Icon>
                              </telerik:RadButton>
                        </telerik:LayoutColumn>
                         <telerik:LayoutColumn   Span="4" HiddenXs="true" SpanSm="5"  >
                         </telerik:LayoutColumn>
                     </Columns>
                </telerik:LayoutRow>
            </Rows>
       </telerik:RadPageLayout>
      <telerik:RadPageLayout runat="server" ID="RPLayoutSendSMSDetail" GridType="Fluid">
              <Rows>
                    <telerik:LayoutRow ID="LRlblSmSResultDept" runat="server" Visible="false" style="margin-top:4px;margin-left:2px;margin-right:2px" >
                        <Columns>
                             <telerik:LayoutColumn   Span="2" HiddenXs="true" SpanSm="2"  >
                            </telerik:LayoutColumn>
                                <telerik:LayoutColumn   Span="5"  SpanXs="12" SpanSm="5" >
                                       <telerik:RadLabel ID="lblSmSResultDept" runat="server" ForeColor="#cc0000"></telerik:RadLabel>
                                </telerik:LayoutColumn>
                            <telerik:LayoutColumn   Span="5" HiddenXs="true" SpanSm="5"  >
                            </telerik:LayoutColumn> 
                        </Columns>
                    </telerik:LayoutRow>
              </Rows>
               <Rows>
                    <telerik:LayoutRow  style="margin-top:4px;margin-left:2px;margin-right:2px">
                        <Columns>
                            <telerik:LayoutColumn   Span="2" HiddenXs="true" SpanSm="2"  >
                            </telerik:LayoutColumn>
                                <telerik:LayoutColumn   Span="5"  SpanXs="12" SpanSm="5" >
                                    <telerik:RadDropDownList RenderMode="Lightweight" ID="RddlSMSLongCodeDept" runat="server" Width="100%" DropDownHeight="200px" >
                                    </telerik:RadDropDownList>
                                    <asp:RequiredFieldValidator runat="server" ID="RFVRddlSMSLongCodeDept" InitialValue="Choose Sender"
                                    ControlToValidate="RddlSMSLongCodeDept" ValidationGroup="SMSdept" Display="Dynamic"
                                    ErrorMessage="Please choose sender" ForeColor="red" />
                                </telerik:LayoutColumn>
                            <telerik:LayoutColumn   Span="5" HiddenXs="true" SpanSm="5"  >
                            </telerik:LayoutColumn> 
                        </Columns>
                    </telerik:LayoutRow>
              </Rows>
          <Rows>
          <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px">
                        <Columns>
                             <telerik:LayoutColumn   Span="2" HiddenXs="true" SpanSm="2"  >
                            </telerik:LayoutColumn>
                            <telerik:LayoutColumn   Span="5"  SpanXs="12" SpanSm="5" >
                                 <span>Mobile No Find by Last Name,First Name,Mobile No,Status</span>
                                <telerik:RadComboBox RenderMode="Mobile" runat="server" ID="RCBStudentsSearch" Visible="false" Height="300px" Width="100%" 
                                    DropDownAutoWidth="Enabled"     OnSelectedIndexChanged="RCBStudentsSearch_SelectedIndexChanged"  AutoPostBack="true"
                                    OpenDropDownOnLoad="false"    OnItemsRequested="RCBStudentsSearch_ItemsRequested"
                                    MarkFirstMatch="true"  EnableLoadOnDemand="true"  
                                    HighlightTemplatedItems="true"  
                                    OnClientItemsRequested="UpdateItemCountField"
                                    DropDownCssClass="exampleRadComboBox">
                                        <HeaderTemplate>
                                            <ul>
                                                <li class="SMScol1">Full Name</li>
                                                <li class="SMScol2">Mobile No</li>
                                                <li class="SMScol3">Status</li>                         
                                            </ul>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <ul>   
                                                <li class="SMScol1">
                                                    <%# Convert.ToString(Eval("FullName")) %></li>
                                                <li class="SMScol2">
                                                    <%# Convert.ToString(Eval("MobilePhone")) %></li>
                                                <li class="SMScol3">
                                                    <%# Convert.ToString(Eval("Status")) %></li>                                                                                                            
                                            </ul>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            A total of
                                            <asp:Literal runat="server" ID="RadComboItemsCount" />
                                            items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                 <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="RCBStudentsLeadSearch" 
                                    Height="300px" Width="100%" DropDownAutoWidth="Enabled"  OpenDropDownOnLoad="false"  
                                    ZIndex="10000000" ItemsPerRequest="10" AutoPostBack="true" OnSelectedIndexChanged="RCBStudentsLeadSearch_SelectedIndexChanged"
                                     EnableLoadOnDemand  ="true" ShowMoreResultsBox="true"  OnClientSelectedIndexChanged="OnClientSelectedIndexChangedSLEMAILSeach" 
                                    EnableVirtualScrolling="true" AllowCustomText="True" AppendDataBoundItems="True" Filter="Contains" ValidateRequest="false" >
                                    <WebServiceSettings Path="~/StudentLeadSelection.asmx"  />
                                </telerik:RadComboBox>
                               </telerik:LayoutColumn>
                            <telerik:LayoutColumn   Span="5" HiddenXs="true" SpanSm="5"  >
                            </telerik:LayoutColumn> 
                        </Columns>
                    </telerik:LayoutRow>
             </Rows>
             <Rows>
                    <telerik:LayoutRow  style="margin-top:4px;margin-left:2px;margin-right:2px">
                        <Columns>
                            <telerik:LayoutColumn   Span="2" HiddenXs="true" SpanSm="2"  >
                            </telerik:LayoutColumn>
                            <telerik:LayoutColumn   Span="5"  SpanXs="12" SpanSm="5" >
                                 <telerik:RadTextBox ID="RtxtIscode" runat="server" Width="18%" EmptyMessage="ISD Code"></telerik:RadTextBox>
                                 <telerik:RadMaskedTextBox RenderMode="Mobile" ID="RtxtMobileDept"  Width="79%" EmptyMessage="Mobile No"    runat="server" Mask="###-###-####" ></telerik:RadMaskedTextBox>
                                <asp:RequiredFieldValidator ID="RFVRtxtIscode" runat="server" ControlToValidate="RtxtIscode" Display="Dynamic" ValidationGroup="SMSdept" 
                                     ErrorMessage="ISD code requried!" ForeColor="Red"></asp:RequiredFieldValidator>
                                 <asp:RequiredFieldValidator ID="RFVRtxtMobileDept" runat="server" ControlToValidate="RtxtMobileDept" Display="Dynamic" ValidationGroup="SMSdept"
                                     ErrorMessage="Mobile required!" ForeColor="Red"></asp:RequiredFieldValidator>

                            </telerik:LayoutColumn>
                            <telerik:LayoutColumn   Span="5" HiddenXs="true" SpanSm="5"  >
                            </telerik:LayoutColumn>
                        </Columns>
                    </telerik:LayoutRow>
             </Rows>
             <Rows>
                    <telerik:LayoutRow  style="margin-top:4px;margin-left:2px;margin-right:2px">
                        <Columns>
                            <telerik:LayoutColumn   Span="2" HiddenXs="true" SpanSm="2"  >
                            </telerik:LayoutColumn>
                             <telerik:LayoutColumn   Span="5"  SpanXs="12" SpanSm="5" >
                                    <telerik:RadTextBox RenderMode="Native" ID="REMessageSMSEditorDept" EmptyMessage="Enter Message"  runat="server"
                                         Height="150px" onkeypress="Validate(this,event);"
                                       MaxLength="140" Width="100%" TextMode="MultiLine" ng-model="REMessageSMSEditorDept">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RFVREMessageSMSEditorDept" runat="server" ControlToValidate="REMessageSMSEditorDept" Display="Dynamic"
                                    ValidationGroup="SMSdept" ErrorMessage="Message Required" ForeColor="Red"></asp:RequiredFieldValidator>

                             </telerik:LayoutColumn> 
                            <telerik:LayoutColumn   Span="5" HiddenXs="true" SpanSm="5"  >
                            </telerik:LayoutColumn>
                        </Columns>
                    </telerik:LayoutRow>
                  <telerik:LayoutRow  style="margin-top:4px;margin-left:2px;margin-right:2px">
                        <Columns>
                            <telerik:LayoutColumn   Span="2" HiddenXs="true" SpanSm="2"  >
                            </telerik:LayoutColumn>
                             <telerik:LayoutColumn   Span="5"  SpanXs="12" SpanSm="5" >
                                 <telerik:RadLabel ID="RLREditorSMSLength" runat="server" Text="0" Style="font-size: 14px; font-style: initial">
                                    </telerik:RadLabel>
                                    
                             </telerik:LayoutColumn> 
                        <telerik:LayoutColumn   Span="5" HiddenXs="true" SpanSm="5"  >
                            </telerik:LayoutColumn>
                        </Columns>
                    </telerik:LayoutRow>
                    <telerik:LayoutRow  style="margin-top:4px;margin-left:2px;margin-right:2px">
                        <Columns>
                            <telerik:LayoutColumn   Span="2" HiddenXs="true" SpanSm="2"  >
                            </telerik:LayoutColumn>
                             <telerik:LayoutColumn   Span="5"  SpanXs="12" SpanSm="5" >
                                       <telerik:RadLabel ID="RadLabel1" runat="server" Text="Max Characters:140" Style="font-size: 14px; font-style: initial"></telerik:RadLabel>
                             </telerik:LayoutColumn> 
                            <telerik:LayoutColumn   Span="5" HiddenXs="true" SpanSm="5"  >
                            </telerik:LayoutColumn>
                        </Columns>
                    </telerik:LayoutRow>
             </Rows>
             <Rows>
                    <telerik:LayoutRow  style="margin-top:4px;margin-bottom:4px;margin-left:2px;margin-right:2px">
                        <Columns>
                            <telerik:LayoutColumn   Span="2" HiddenXs="true" SpanSm="2"  >
                            </telerik:LayoutColumn>
                             <telerik:LayoutColumn   Span="2"  SpanXs="4" SpanSm="4" >
                                    <telerik:RadButton ID="RPbtnSMSSendDept" ValidationGroup="SMSdept"   
                                    Width="100%" runat="server" Text="Send"  OnClick="RPbtnSMSSendDept_Click">
                                        <Icon PrimaryIconCssClass="rbSave"></Icon>
                                    </telerik:RadButton>
                              </telerik:LayoutColumn>
                            <telerik:LayoutColumn   Span="5" HiddenXs="true" SpanSm="5"  >
                            </telerik:LayoutColumn> 
                        </Columns>
                    </telerik:LayoutRow>
             </Rows>
   
       </telerik:RadPageLayout>
   </asp:Panel> 
</asp:Content>
