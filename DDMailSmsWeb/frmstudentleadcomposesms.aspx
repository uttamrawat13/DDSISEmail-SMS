<%@ Page   Language="C#" MasterPageFile="~/MainDDMailSMSMaster.Master" AutoEventWireup="true" CodeBehind="frmstudentleadcomposesms.aspx.cs" Inherits="DDMailSmsWeb.TSTDLEADComposeSMS" %>
<asp:Content ID="ConCPhead" ContentPlaceHolderID="CPhead" runat="server">
     <script type="text/javascript">
         function Validate(e, t) {
             var charCode = 0;
             if (t) {
                 charCode = t.which;

                 if (charCode == 8) {

                     var dateInput = document.getElementById('<%= REMessageSMSEditor.ClientID %>');
                    console.log(dateInput.value.length);
                    if (document.getElementById('<%= RLREditorSMSLength.ClientID %>').innerHTML != '0') {
                        document.getElementById('<%= RLREditorSMSLength.ClientID %>').innerHTML = (dateInput.value.length - 1).toString();
                    }
                }
                else {
                    var dateInput = document.getElementById('<%= REMessageSMSEditor.ClientID %>');
                    console.log(dateInput.value.length);
                    document.getElementById('<%= RLREditorSMSLength.ClientID %>').innerHTML = (dateInput.value.length + 1).toString();
                }
            }
        }
      </script>
      
 </asp:Content>
<asp:Content ID="ConCPHDetail" ContentPlaceHolderID="CPHDetail" runat="server"  > 
   <telerik:RadAjaxLoadingPanel ID="RALPfrmstudentleadcomposesms" runat="server" Height="75px"
    Width="75px" Transparency="50">
    </telerik:RadAjaxLoadingPanel>
   
    <telerik:RadAjaxManagerProxy ID="RAMfrmstudentleadcomposesms" runat="server">
      <AjaxSettings>
       <telerik:AjaxSetting AjaxControlID="pfrmstudentleadcomposesms">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pfrmstudentleadcomposesms" LoadingPanelID="RALPfrmstudentleadcomposesms" ></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
 
    <asp:Panel runat="server" ID="pfrmstudentleadcomposesms">
       <telerik:RadPageLayout runat="server" ID="RPLayoutApplyTempHeader" GridType="Fluid">
             <Rows>
                 <telerik:LayoutRow  style="margin-top:4px;margin-left:2px;margin-right:2px">
                    <Columns>
                         <telerik:LayoutColumn   Span="2" HiddenXs="true" SpanSm="2"  >
                         </telerik:LayoutColumn>
                         <telerik:LayoutColumn   Span="5"  SpanXs="9" SpanSm="5"  >
                            <telerik:RadDropDownList RenderMode="Lightweight" ID="RddlApplySMSTemplate" Width="100%" runat="server"  DropDownHeight="200px" AutoPostBack="false">
                            </telerik:RadDropDownList>
                        </telerik:LayoutColumn>
                         <telerik:LayoutColumn   Span="1"  SpanXs="3" SpanSm="3"     >
                               <telerik:RadButton RenderMode="Lightweight" runat="server" Text="Apply" style="margin-left:2px"  ID="RbtnSMSTempApply"  OnClick="RbtnSMSTempApply_Click" >
                                    <Icon PrimaryIconCssClass="rbOk"></Icon>
                               </telerik:RadButton>
                        </telerik:LayoutColumn>
                           <telerik:LayoutColumn   Span="1"  SpanXs="3" HiddenXs="true" HiddenSm="true"     >
                              <telerik:RadButton RenderMode="Lightweight"  runat="server" Text="Reset"   ID="RbtmSMSTemplateClear" OnClick="RbtmSMSTemplateClear_Click">
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
                    <telerik:LayoutRow ID="LRlblSmSResult" runat="server" style="margin-top:4px;margin-left:2px;margin-right:2px" >
                        <Columns>
                             <telerik:LayoutColumn   Span="2" HiddenXs="true" SpanSm="2"  >
                            </telerik:LayoutColumn>
                                <telerik:LayoutColumn   Span="5"  SpanXs="12" SpanSm="5" >
                                       <telerik:RadLabel ID="lblSmSResult" runat="server" ForeColor="#cc0000"></telerik:RadLabel>
                                </telerik:LayoutColumn>
                            <telerik:LayoutColumn   Span="5" HiddenXs="true" SpanSm="5"  >
                            </telerik:LayoutColumn> 
                        </Columns>
                    </telerik:LayoutRow>
              </Rows>
             <Rows>
                    <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px" >
                        <Columns>
                             <telerik:LayoutColumn   Span="2" HiddenXs="true" SpanSm="2"  >
                            </telerik:LayoutColumn>
                                <telerik:LayoutColumn   Span="5"  SpanXs="12" SpanSm="5" >
                                    <telerik:RadDropDownList RenderMode="Lightweight" ID="RddlSMSLongCode"  Width="100%" runat="server" Font-Size="12px" DropDownHeight="200px"
                                         AutoPostBack="false">
                                   </telerik:RadDropDownList>
                                     <asp:RequiredFieldValidator runat="server" ID="RFVRddlSMSLongCode" InitialValue="Choose Sender"
                                        ControlToValidate="RddlSMSLongCode" ValidationGroup="SMS" Display="Dynamic" ForeColor="#ff0000"
                                        ErrorMessage="Please choose sender" CssClass="validator" />
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
                                  <telerik:RadTextBox ID="RtxtIscode" runat="server" Width="18%" EmptyMessage="ISD Code"></telerik:RadTextBox>
                                       <telerik:RadMaskedTextBox RenderMode="Mobile" ID="RtxtMobile"   EmptyMessage="Mobile No"   Width="79%" runat="server" Mask="###-###-####" ></telerik:RadMaskedTextBox>
                                     
                                      <asp:RequiredFieldValidator ID="RFVRtxtIscode" runat="server" ControlToValidate="RtxtIscode" Display="Dynamic" ValidationGroup="SMS" 
                                           ErrorMessage="ISD code requried!" ForeColor="Red"></asp:RequiredFieldValidator>
                                      <asp:RequiredFieldValidator ID="RFVRtxtMobile" runat="server" ControlToValidate="RtxtMobile" Display="Dynamic" ValidationGroup="SMS" 
                                           ErrorMessage="Mobile required!" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                     
                                   <telerik:RadTextBox RenderMode="Native" ID="REMessageSMSEditor" EmptyMessage="Enter Message"  runat="server" Height="150px"
                                     onkeypress="Validate(this,event);" MaxLength="140"   Width="100%" TextMode="MultiLine">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RFVREMessageSMSEditor" runat="server" ControlToValidate="REMessageSMSEditor" Display="Dynamic" ValidationGroup="SMS"
                                    ErrorMessage="Message Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                </telerik:LayoutColumn>
                            <telerik:LayoutColumn   Span="5" HiddenXs="true" SpanSm="5"  >
                            </telerik:LayoutColumn>  
                        </Columns>
                    </telerik:LayoutRow>
                  <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px">
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
                    <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px">
                        <Columns>
                             <telerik:LayoutColumn   Span="2" HiddenXs="true" SpanSm="2"  >
                            </telerik:LayoutColumn>
                             <telerik:LayoutColumn   Span="5"  SpanXs="12" SpanSm="5" >
                                       <telerik:RadLabel ID="RadLabel3" runat="server" Text="Max Characters:140" Style="font-size: 14px; font-style: initial"></telerik:RadLabel>
                             </telerik:LayoutColumn> 
                            <telerik:LayoutColumn   Span="5" HiddenXs="true" SpanSm="5"  >
                             </telerik:LayoutColumn> 
                         </Columns>
                    </telerik:LayoutRow>
             </Rows>
             <Rows>
                    <telerik:LayoutRow style="margin-top:4px;margin-bottom:4px;margin-left:2px;margin-right:2px">
                        <Columns>
                             <telerik:LayoutColumn   Span="2" HiddenXs="true" SpanSm="2"  >
                            </telerik:LayoutColumn>
                             <telerik:LayoutColumn   Span="2"  SpanXs="4" SpanSm="4" >
                                    <telerik:RadButton ID="btnSMSSend" ValidationGroup="SMS" 
                                    Width="100%" runat="server" Text="Send" OnClick="btnSMSSend_Click">
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
