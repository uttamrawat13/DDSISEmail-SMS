<%@ Page Title="Add Email Template" Language="C#" MasterPageFile="~/MainDDMailSMSMaster.Master" AutoEventWireup="true" CodeBehind="frmemailaddtemplate.aspx.cs" Inherits="DDMailSmsWeb.TEMAILADDTemplate" %>
<asp:Content ID="ConCPhead" ContentPlaceHolderID="CPhead" runat="server">
    <script type="text/javascript">
        (function () {
            var $;
            var demo = window.demo = window.demo || {};

            demo.initialize = function () {
                $ = $telerik.$;
            };

            window.validationFailed = function (radAsyncUpload, args) {
                var $row = $(args.get_row());
                $row.addClass("ruError");
                $row.append(span);
            }
        })();
    </script>
</asp:Content>
<asp:Content ID="ConCPHDetail" ContentPlaceHolderID="CPHDetail" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RALPfrmemailaddtemplate" runat="server" Height="75px"
        Width="75px" Transparency="50">
    </telerik:RadAjaxLoadingPanel>
    
     <telerik:RadAjaxManagerProxy ID="RAMfrmemailaddtemplate" runat="server">
      <AjaxSettings>
       <telerik:AjaxSetting AjaxControlID="Pfrmemailaddtemplate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Pfrmemailaddtemplate"  LoadingPanelID="RALPfrmemailaddtemplate" ></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Panel runat="server" ID="Pfrmemailaddtemplate">
         <telerik:RadPageLayout runat="server" ID="RDPLayoutfrmemailconfiguration" GridType="Fluid">
              <Rows>
                    <telerik:LayoutRow ID="LayoutRow1"   style="margin-top:4px;margin-left:2px;margin-right:2px;margin-bottom:4px" runat="server"  >
                        <Columns>
                             <telerik:LayoutColumn   Span="7"   SpanXs="7"  SpanMd="7"  SpanSm="7">
                               <div style="border: 1px solid #e9e4e4;padding:10px;height:inherit;">
                                   <telerik:RadPageLayout runat="server" ID="RadPageLayout1" GridType="Fluid">
            <Rows>
                <telerik:LayoutRow  >
                    <Columns>
                         <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >

                             <telerik:RadGrid ID="RgvEmailTemplate" runat="server" AutoGenerateColumns="False" Width="100%" RenderMode="Lightweight" 
                                FilterMenu-RenderMode="Lightweight"
                                GroupPanelPosition="Top" PageSize="10" Font-Size="10" OnItemCommand="RgvEmailTemplate_ItemCommand"
                                OnNeedDataSource="RgvEmailTemplate_NeedDataSource"  AllowFilteringByColumn="true"
                                AllowPaging="True" AllowSorting="True" OnSortCommand="RgvEmailTemplate_SortCommand">
                                <ItemStyle Wrap="true"></ItemStyle>
                                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>

                                    <MasterTableView AllowMultiColumnSorting="true">
                                        <Columns>
                                            <telerik:GridBoundColumn SortExpression="Title" AllowFiltering="true" ShowSortIcon="true" 
                                            AllowSorting="true" HeaderText="Title" HeaderButtonType="TextButton" ItemStyle-VerticalAlign="Top"
                                            DataField="Title">
                                        </telerik:GridBoundColumn>

                                            <telerik:GridNumericColumn DataField="Active" HeaderText="Status" Visible="false" 
                                                 AllowFiltering="false">
                                            </telerik:GridNumericColumn>
                                            <telerik:GridNumericColumn DataField="ActiveStatus" HeaderText="Status"  AllowSorting="false" Visible="True"
                                                 AllowFiltering="false">
                                            </telerik:GridNumericColumn>

                                            <telerik:GridTemplateColumn HeaderText="Edit" ItemStyle-Width="35px" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ViewEmailTemplate" runat="server" AlternateText="Edit" Height="16px" Width="16px"
                                                        ToolTip="Edit This Record"
                                                        ImageUrl="~/images/pencil_edit_button.png" CommandName="ViewEmailTemplate" CommandArgument='<%# Eval("ID") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn HeaderText="Delete" ItemStyle-Width="40px" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="DeleteEmailTemp" runat="server" AlternateText="Delete" ToolTip="Delete record" Height="16px" Width="16px"
                                                        OnClientClick="javascript:return confirm('Are you SURE delete this record?')"
                                                        ImageUrl="~/images/Delete.png" CommandName="DeleteEmailTemp" CommandArgument='<%# Eval("ID") %>' />

                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns> 
                                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                                    </MasterTableView>
                                    <PagerStyle AlwaysVisible="true" Mode="NumericPages"></PagerStyle>
                                    <FilterMenu RenderMode="Lightweight"></FilterMenu>
                                    <HeaderContextMenu RenderMode="Lightweight"></HeaderContextMenu>
                                </telerik:RadGrid>
                          </telerik:LayoutColumn>
                    </Columns>
                 </telerik:LayoutRow>
            </Rows>
        </telerik:RadPageLayout>
                                </div>
                                 </telerik:LayoutColumn>
                             <telerik:LayoutColumn    Span="5"  SpanXs="5"  SpanMd="5"  SpanSm="5" >
                                        <div style="border: 1px solid #e9e4e4;padding:10px;height:inherit; width:100%;">
                                          <telerik:RadPageLayout runat="server" ID="RDPLayoutRlblTempResult" GridType="Fluid">
                <Rows>
                    <telerik:LayoutRow  ID="LRRlblTempResult" runat="server" Visible="false" style="margin-top:4px;margin-left:2px;margin-right:2px">
                        <Columns>
                         
                                <telerik:LayoutColumn>
                                    <telerik:RadLabel ID="RlblTempResult" runat="server" ForeColor="#cc0000"></telerik:RadLabel>
                                </telerik:LayoutColumn> 
                        
                        </Columns>
                    </telerik:LayoutRow>
                </Rows>
                <Rows>
                <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px">
                    <Columns>
                         
                         <telerik:LayoutColumn >
                                 <telerik:RadTextBox RenderMode="Lightweight" EmptyMessage="Please enter title!" runat="server" 
                                     ID="txtemailtempTitle" Width="100%">
                                 </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RFVtxtemailtempTitle" runat="server" Display="Dynamic" ValidationGroup="TemplateEmailSave"
                                ControlToValidate="txtemailtempTitle" ErrorMessage="Please enter title!" ForeColor="Red" />

                         </telerik:LayoutColumn>
                         
                    </Columns>
                </telerik:LayoutRow>
           </Rows>
                <Rows>
                    <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px">
                        <Columns>
                             
                        
                             <telerik:LayoutColumn  >
                                  <div class="qsf-decoration"></div>
                                    <telerik:RadAsyncUpload RenderMode="Lightweight" MultipleFileSelection="Disabled"    runat="server" ID="fileuploadTemp"
                                    AllowedFileExtensions=".html,.htm" MaxFileInputsCount="1" OnClientValidationFailed="validationFailed" >
                                        </telerik:RadAsyncUpload>
                                    <p style="font-size: 12px; color: black; font-style: initial; margin: 0px;"><span style="color: red;">*</span> Must be upload HTML file.</p>
                             </telerik:LayoutColumn>
                              
  
                        </Columns>
                    </telerik:LayoutRow>
               </Rows>
                <Rows>
                    <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px">
                        <Columns>
                             
                         
                              <telerik:LayoutColumn   >
                                 <telerik:RadButton RenderMode="Lightweight" ID="RbtntempActive" runat="server" Width="135px" ToggleType="CheckBox" ButtonType="StandardButton"
                                    AutoPostBack="false">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="Active: True" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState Text="Active: False" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                 </telerik:RadButton>
                             </telerik:LayoutColumn> 
                           
                       </Columns>
                    </telerik:LayoutRow>
               </Rows>          
                <Rows>
                    <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px">
                        <Columns>                             
                             <telerik:LayoutColumn    >
                                        <telerik:RadEditor RenderMode="Lightweight"  runat="server"
                                            Height="225px" ID="REditorTempEmail" Font-Size="12px"  
                                            SkinID="Office2010Silver"  Width="100%">
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
                             </telerik:LayoutColumn>                             
                           </Columns>
                    </telerik:LayoutRow>
               </Rows> 
                <Rows>
                    
                    <telerik:LayoutRow style="margin-top:10px;margin-bottom:4px;margin-left:2px;margin-right:2px">
                      <Columns>
                              
                             <telerik:LayoutColumn Span="6">
                                  <telerik:RadButton ID="btnSaveEmailTemp"   Width="100%"   runat="server" ValidationGroup="TemplateEmailSave"
                                      OnClick="btnSaveEmailTemp_Click" Text="Save">
                                     <Icon PrimaryIconCssClass="rbSave"></Icon>
                                 </telerik:RadButton>
                             </telerik:LayoutColumn> 
                             <telerik:LayoutColumn Span="6">
                              <telerik:RadButton ID="btnAddNewEmailTemp"   Width="100%"   runat="server" Text="Cancel" OnClick="btnAddNewEmailTemp_Click">
                                     <Icon PrimaryIconCssClass="rbCancel"></Icon>
                                 </telerik:RadButton>
                          </telerik:LayoutColumn>
                          
                             
                      
                      </Columns>
                    </telerik:LayoutRow>
               </Rows>
       </telerik:RadPageLayout>
                                         </div>
                                   </telerik:LayoutColumn>
                         </Columns>
                        
                    </telerik:LayoutRow>
               </Rows>
       </telerik:RadPageLayout>

    </asp:Panel>
</asp:Content>

