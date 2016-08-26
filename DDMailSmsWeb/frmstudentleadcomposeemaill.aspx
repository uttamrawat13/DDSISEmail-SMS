<%@ Page   Language="C#" MasterPageFile="~/MainDDMailSMSMaster.Master" AutoEventWireup="true" CodeBehind="frmstudentleadcomposeemaill.aspx.cs" Inherits="DDMailSmsWeb.TSTDLEADComposeEmail" %>
<asp:Content ID="ConCPhead" ContentPlaceHolderID="CPhead" runat="server">
    
</asp:Content>


<asp:Content ID="ConCPHDetail" ContentPlaceHolderID="CPHDetail" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RALPfrmstudentleadcomposeemaill" runat="server" Height="75px"
    Width="75px" Transparency="50">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManagerProxy ID="RAMfrmstudentleadcomposeemaill" runat="server">
        <AjaxSettings>
       <telerik:AjaxSetting AjaxControlID="Pfrmstudentleadcomposeemaill">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Pfrmstudentleadcomposeemaill" LoadingPanelID="RALPfrmstudentleadcomposeemaill" ></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>    
    </telerik:RadAjaxManagerProxy>
 
    <asp:Panel runat="server" ID="Pfrmstudentleadcomposeemaill">
     <telerik:RadPageLayout runat="server" ID="RPLayoutApplyTempHeader" GridType="Fluid">
             <Rows>
                <telerik:LayoutRow  style="margin-top:4px;margin-left:2px;margin-right:2px">
                    <Columns>
                         <telerik:LayoutColumn   Span="2" HiddenXs="true" SpanSm="2"  >
                         </telerik:LayoutColumn>
                         <telerik:LayoutColumn   Span="5"  SpanXs="9" SpanSm="5"  >
                            <telerik:RadDropDownList RenderMode="Lightweight" ID="RddlChooseTemplates" Width="100%" runat="server"  DropDownHeight="200px" AutoPostBack="false">
                            </telerik:RadDropDownList>
                        </telerik:LayoutColumn>
                         <telerik:LayoutColumn   Span="1"  SpanXs="3" SpanSm="3"     >
                               <telerik:RadButton RenderMode="Lightweight" runat="server" Text="Apply" style="margin-left:2px" OnClick="btmApplyTemplate_Click"  ID="btmApplyTemplate" >
                                 <Icon PrimaryIconCssClass="rbOk"></Icon>
                               </telerik:RadButton>
                        </telerik:LayoutColumn>
                           <telerik:LayoutColumn   Span="1"  SpanXs="3" HiddenXs="true" HiddenSm="true"     >
                              <telerik:RadButton RenderMode="Lightweight"  runat="server" Text="Reset"   ID="btnClearTemplatestdEmail" OnClick="btnClearTemplatestdEmail_Click"  >
                             <Icon PrimaryIconCssClass="rbCancel"></Icon>
                                   </telerik:RadButton>
                        </telerik:LayoutColumn>
                         <telerik:LayoutColumn   Span="4" HiddenXs="true" SpanSm="5"  >
                         </telerik:LayoutColumn>
                     </Columns>
                </telerik:LayoutRow>
            </Rows>
       </telerik:RadPageLayout>
     <telerik:RadPageLayout runat="server" ID="RPLayoutComposeEmailDetail" GridType="Fluid">
                <Rows>
                    <telerik:LayoutRow  ID="LRRlblComposeMailResult" runat="server" Visible="false" style="margin-top:4px;margin-left:2px;margin-right:2px">
                        <Columns>
                            <telerik:LayoutColumn   Span="3" HiddenXs="true" SpanSm="3"  >
                            </telerik:LayoutColumn>
                                <telerik:LayoutColumn   Span="6"  SpanXs="12" SpanSm="6" >
                                    <telerik:RadLabel ID="RlblComposeMailResult" runat="server" ForeColor="#cc0000"></telerik:RadLabel>
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
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="RtxtEmailFrom" EmptyMessage="Email From" Width="100%"  Enabled="false"></telerik:RadTextBox>
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
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="Rtxtemailto" EmptyMessage="Email To"   Width="100%"  >
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RRFVtxtemailto" runat="server" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="Rtxtemailto" ErrorMessage="Please, enter an e-mail!" ValidationGroup="EmailCompose" />
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
                             <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="RtxtEmailCC" EmptyMessage="Email CC"   Width="100%"  ></telerik:RadTextBox>
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
                             <telerik:LayoutColumn    Span="5"  SpanXs="12" SpanSm="5" >
                               <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="RtxtEmailBCC"  EmptyMessage="Email BCC"  Width="100%" ></telerik:RadTextBox>
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
                                <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="RtxtemailSubject" EmptyMessage="Email Subject"   Width="100%" ></telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RFVtxtemailSubject" runat="server" ControlToValidate="RtxtemailSubject" Display="Dynamic" ValidationGroup="EmailCompose" ErrorMessage="Subject Required" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                <telerik:RadAsyncUpload RenderMode="Lightweight" MultipleFileSelection="Disabled" 
                                    MaxFileInputsCount="1"  EmptyMessage="Email Attachment"  runat="server"  Width="100%"  ID="fileuploadComposeTemp" />
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
                               

                                   <telerik:RadEditor RenderMode="Lightweight"  runat="server"
                                        EmptyMessage="Email Body"  Height="230px"  Width="100%"  ID="REditComposeBody" 
                                       Font-Size="12px" SkinID="Office2010Silver">
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
                                    <asp:RequiredFieldValidator ID="RComposeBody" runat="server" ControlToValidate="REditComposeBody" Display="Dynamic" 
                                    ValidationGroup="EmailCompose" ErrorMessage="Body Required" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                 <telerik:RadButton ID="RpushBtnComposeMail" ValidationGroup="EmailCompose"  Width="100%"   runat="server" Text="Send"
                                      OnClick="RpushBtnComposeMail_Click">
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
