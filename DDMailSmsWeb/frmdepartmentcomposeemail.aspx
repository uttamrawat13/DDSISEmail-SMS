<%@ Page Title="Department Compose Email" Language="C#" MasterPageFile="~/MainDDMailSMSMaster.Master" AutoEventWireup="true" CodeBehind="frmdepartmentcomposeemail.aspx.cs" Inherits="DDMailSmsWeb.TDEPTComposeEmail" %>
<asp:Content ID="ConCPhead" ContentPlaceHolderID="CPhead" runat="server">
     <script type="text/javascript">
         function OnClientSelectedIndexChangedSLEMAILSeach(sender, eventArgs) {
            
             var item = eventArgs.get_item();
             sender.set_text('');
             sender.set_value(item.get_value());
              
         }
        </script>
        <style type="text/css">
            .reEditorModes a
            {
                width: 26px !important;
                overflow: hidden;
                border: solid 1px #ececec;
            }
            .reEditorModes a span
            {
                text-indent: -9999px;
            }
            a.reMode_selected
            {
                border: solid 1px #828282;
                background: none #cecece !important;
            }
        </style>

</asp:Content>
<asp:Content ID="ConCPHDetail" ContentPlaceHolderID="CPHDetail" runat="server">
    
    <telerik:RadAjaxLoadingPanel ID="RALPfrmdepartmentcomposeemail" runat="server" Height="75px"
    Width="75px" Transparency="50">
    </telerik:RadAjaxLoadingPanel>
    
      <telerik:RadAjaxManagerProxy ID="RAMfrmdepartmentcomposeemail" runat="server">
       <AjaxSettings>
       <telerik:AjaxSetting AjaxControlID="Pfrmdepartmentcomposeemail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Pfrmdepartmentcomposeemail" LoadingPanelID="RALPfrmdepartmentcomposeemail" ></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
      </telerik:RadAjaxManagerProxy>

    <asp:Panel runat="server" ID="Pfrmdepartmentcomposeemail">
     <telerik:RadPageLayout runat="server" ID="RPLayoutApplyTempHeader" GridType="Fluid">
             <Rows>
                <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px">
                    <Columns>
                          <telerik:LayoutColumn   Span="2" HiddenXs="true" SpanSm="2"  >
                         </telerik:LayoutColumn>
                         <telerik:LayoutColumn   Span="5"  SpanXs="9" SpanSm="5"  >
                            <telerik:RadDropDownList RenderMode="Lightweight" ID="RddlChooseTemplatesdept" Width="100%" runat="server"  DropDownHeight="200px" AutoPostBack="false">
                            </telerik:RadDropDownList>
                        </telerik:LayoutColumn>
                         <telerik:LayoutColumn   Span="1"  SpanXs="3" SpanSm="3"     >
                               <telerik:RadButton RenderMode="Lightweight" runat="server" Text="Apply" style="margin-left:2px"  ID="btnApplyTemplateDept"  OnClick="btnApplyTemplateDept_Click" >
                               <Icon PrimaryIconCssClass="rbOk"></Icon>
                               </telerik:RadButton>
                        </telerik:LayoutColumn>
                           <telerik:LayoutColumn   Span="1"  SpanXs="3" HiddenXs="true" HiddenSm="true"     >
                              <telerik:RadButton RenderMode="Lightweight"  runat="server" Text="Reset"   ID="btnClearTemplateDeptEmail"  
                                  OnClick="btnClearTemplateDeptEmail_Click">
                                 <Icon PrimaryIconCssClass="rbCancel"></Icon>
                              </telerik:RadButton>
                        </telerik:LayoutColumn>
                         <telerik:LayoutColumn   Span="4" HiddenXs="true" SpanSm="5"  >
                         </telerik:LayoutColumn>
                          
                     </Columns>
                </telerik:LayoutRow>
            </Rows>
       </telerik:RadPageLayout>
     <telerik:RadPageLayout runat="server" ID="RPLayoutComposeEmail" GridType="Fluid">
              <Rows>
                    <telerik:LayoutRow  ID="LRRlblComposeMaildeptMsg" runat="server" Visible="false" style="margin-top:4px;margin-left:2px;margin-right:2px">
                        <Columns>
                            <telerik:LayoutColumn   Span="3" HiddenXs="true" SpanSm="3"  >
                            </telerik:LayoutColumn>
                                <telerik:LayoutColumn   Span="6"  SpanXs="12" SpanSm="6" >
                                    <telerik:RadLabel ID="RlblComposeMaildeptMsg" runat="server" ForeColor="#cc0000"></telerik:RadLabel>
                                </telerik:LayoutColumn> 
                            <telerik:LayoutColumn   Span="3" HiddenXs="true" SpanSm="3"  >
                            </telerik:LayoutColumn>
                        </Columns>
                    </telerik:LayoutRow>
              </Rows>
              <Rows>
                    <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px">
                        <Columns>
                             <telerik:LayoutColumn   Span="2" HiddenXs="true" SpanSm="2"  >
                             </telerik:LayoutColumn>
                             <telerik:LayoutColumn    Span="5"  SpanXs="12" SpanSm="5" >
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="RtxtEmailFromdept" EmptyMessage="Email From" Width="100%"  Enabled="false"></telerik:RadTextBox>
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
                             <telerik:LayoutColumn    Span="5"  SpanXs="12" SpanSm="5" >
                                 <span>Email Id Find by Last Name,First Name,Status</span>
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
                    <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px">
                        <Columns>
                                 <telerik:LayoutColumn   Span="2" HiddenXs="true" SpanSm="2"  >
                                 </telerik:LayoutColumn>
                                <telerik:LayoutColumn    Span="5"  SpanXs="12" SpanSm="5" >    
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="Rtxtemailtodept" EmptyMessage="Email To"   Width="100%"  >
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RRFVRtxtemailtodept" runat="server" Display="Dynamic" ValidationGroup="emailcomposedept"
                                    ControlToValidate="Rtxtemailtodept" ErrorMessage="Please, enter an e-mail!" ForeColor="Red" />
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
                             <telerik:LayoutColumn    Span="5"  SpanXs="12" SpanSm="5" >
                               <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="RtxtEmailCCdept" EmptyMessage="Email CC"   Width="100%"  ></telerik:RadTextBox>
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
                             <telerik:LayoutColumn    Span="5"  SpanXs="12" SpanSm="5" >
                               <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="RtxtEmailBCCdept"  EmptyMessage="Email BCC"  Width="100%" ></telerik:RadTextBox>
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
                             <telerik:LayoutColumn    Span="5"  SpanXs="12" SpanSm="5" >
                                <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="RtxtemailSubjectdept" EmptyMessage="Email Subject"   Width="100%" ></telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RFVRtxtemailSubjectdept" runat="server" ControlToValidate="RtxtemailSubjectdept" Display="Dynamic" ValidationGroup="emailcomposedept" ErrorMessage="Subject Required" ForeColor="Red"></asp:RequiredFieldValidator>
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
                             <telerik:LayoutColumn    Span="5"  SpanXs="12" SpanSm="5" >
                                <telerik:RadAsyncUpload RenderMode="Lightweight" MultipleFileSelection="Disabled" MaxFileInputsCount="1" 
                                    EmptyMessage="Email Attachment"  runat="server"  Width="100%"  ID="fileuploadComposeTempdept" />
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
                             <telerik:LayoutColumn    Span="5"  SpanXs="12" SpanSm="5" >
                                   <telerik:RadEditor RenderMode="Lightweight"    runat="server" EmptyMessage="Email Body"    
                                        Height="230px"  Width="100%"  ID="REditComposeBodydept" Font-Size="12px" SkinID="Office2010Silver">
                                        <Tools>
                                            <telerik:EditorToolGroup>
                                                <telerik:EditorTool Name="Cut" />
                                                <telerik:EditorTool Name="Copy" />
                                                <telerik:EditorTool Name="Paste" />
                                                <telerik:EditorTool Name="BackColor" />
                                                <telerik:EditorTool Name="Bold" />
                                                <telerik:EditorTool Name="FontName" />
                                                <telerik:EditorTool Name="FontSize" />
                                                <telerik:EditorTool Name="ForeColor" />
                                                <telerik:EditorTool Name="SelectAll" />                                     
                                                <telerik:EditorTool Name="InsertImage" Enabled="true" />
                                                <telerik:EditorTool Name="InsertLink" />                                             
                                                <telerik:EditorTool Name="InsertOrderedList" />
                                            </telerik:EditorToolGroup>
                                        </Tools>
                                    </telerik:RadEditor>   
                                   <asp:RequiredFieldValidator ID="RFVREditComposeBodydept" runat="server" ControlToValidate="REditComposeBodydept" Display="Dynamic" ValidationGroup="emailcomposedept" ErrorMessage="Body Required" ForeColor="Red"></asp:RequiredFieldValidator>
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
                             <telerik:LayoutColumn    Span="2"  SpanXs="4" SpanSm="4" >
                                 <telerik:RadButton ID="RpushBtnComposeMaildept" ValidationGroup="emailcomposedept"  OnClick="RpushBtnComposeMaildept_Click"  Width="100%"   runat="server" Text="Send">
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

