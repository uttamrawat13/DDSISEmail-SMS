<%@ Page Title="Add SMS Template" Language="C#" MasterPageFile="~/MainDDMailSMSMaster.Master" AutoEventWireup="true" CodeBehind="frmsmsaddtemplate.aspx.cs" Inherits="DDMailSmsWeb.TSMSADDTemplate" %>
<asp:Content ID="ConCPhead" ContentPlaceHolderID="CPhead" runat="server">

    <script type="text/javascript">
        function Validate(e, t) {
            var charCode = 0;
            if (t) {
                charCode = t.which;

                if (charCode == 8) {

                    var dateInput = document.getElementById('<%= REditorSMS.ClientID %>');
                    console.log(dateInput.value.length);
                    if (document.getElementById('<%= RLREditorSMSLength.ClientID %>').innerHTML != '0') {
                        document.getElementById('<%= RLREditorSMSLength.ClientID %>').innerHTML = (dateInput.value.length - 1).toString();
                    }
                }
                else {
                    var dateInput = document.getElementById('<%= REditorSMS.ClientID %>');
                    console.log(dateInput.value.length);
                    document.getElementById('<%= RLREditorSMSLength.ClientID %>').innerHTML = (dateInput.value.length + 1).toString();
                }
            }
        }
      </script>
</asp:Content>
<asp:Content ID="ConCPHDetail" ContentPlaceHolderID="CPHDetail" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RALPfrmsmsaddtemplate" runat="server" Height="75px"
        Width="75px" Transparency="50">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxManagerProxy ID="RAMfrmsmsaddtemplate" runat="server">
      <AjaxSettings>
       <telerik:AjaxSetting AjaxControlID="Pfrmsmsaddtemplate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Pfrmsmsaddtemplate" LoadingPanelID="RALPfrmsmsaddtemplate" ></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Panel runat="server" ID="Pfrmsmsaddtemplate">
            <telerik:RadPageLayout runat="server" ID="RDPLayoutfrmemailconfiguration" GridType="Fluid">
                <Rows>
                      <telerik:LayoutRow ID="LayoutRow1"   style="margin-top:4px;margin-left:2px;margin-right:2px;margin-bottom:4px" runat="server"  >
                              <Columns>
                                     <telerik:LayoutColumn   Span="7"   SpanXs="7"  SpanMd="7"  SpanSm="7">
                                         <telerik:RadPageLayout runat="server" ID="RDPLayoutGridDetial" GridType="Fluid">
                                        <Rows>
                                            <telerik:LayoutRow  >
                                                <Columns>
                                                        <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                            <telerik:RadGrid ID="RgvSMSTemplate" runat="server" AutoGenerateColumns="False" Width="100%" RenderMode="Lightweight" 
                                                                FilterMenu-RenderMode="Lightweight"
                                                                GroupPanelPosition="Top" PageSize="10" Font-Size="10" OnItemCommand="RgvSMSTemplate_ItemCommand"
                                                                OnNeedDataSource="RgvSMSTemplate_NeedDataSource"  AllowFilteringByColumn="true"
                                                                AllowPaging="True" AllowSorting="True" OnSortCommand="RgvSMSTemplate_SortCommand">
                                                                <ItemStyle Wrap="true"></ItemStyle>
                                                                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                                <MasterTableView AllowMultiColumnSorting="true" >
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn SortExpression="Title" AllowFiltering="true" ShowSortIcon="true" 
                                                                            AllowSorting="true" HeaderText="Title" HeaderButtonType="TextButton" ItemStyle-VerticalAlign="Top"
                                                                            DataField="Title">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridNumericColumn DataField="Active" HeaderText="Status" Visible="false" ItemStyle-VerticalAlign="Top" AllowFiltering="true">
                                                                        </telerik:GridNumericColumn>
                                                                        <telerik:GridNumericColumn DataField="ActiveStatus" ItemStyle-VerticalAlign="Top" HeaderText="Status" AllowFiltering="false"
                                                                                AllowSorting="false" Visible="True" >
                                                                        </telerik:GridNumericColumn>

                                                                        <telerik:GridTemplateColumn HeaderText="Edit" ItemStyle-Width="35px" AllowFiltering="false">
                                                                            <ItemTemplate>

                                                                                <asp:ImageButton ID="ViewSMSTemp" runat="server" AlternateText="Delete" ToolTip="Edit This Record"
                                                                                    Height="16px" Width="16px"
                                                                                    ImageUrl="~/images/pencil_edit_button.png" CommandName="ViewSMSTemp" CommandArgument='<%# Eval("ID") %>' />

                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>

                                                                        <telerik:GridTemplateColumn HeaderText="Delete" ItemStyle-Width="40px" AllowFiltering="false">
                                                                            <ItemTemplate>

                                                                                <asp:ImageButton ID="DeleteSMSTemp" runat="server" AlternateText="Delete" ToolTip="Delete This Record" Height="16px" Width="16px"
                                                                                    OnClientClick="javascript:return confirm('Are You Sure Delete This Record?')"
                                                                                    ImageUrl="~/images/Delete.png" CommandName="DeleteSMSTemp" CommandArgument='<%# Eval("ID") %>' />
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
                                     </telerik:LayoutColumn>
                                     <telerik:LayoutColumn   Span="5"   SpanXs="5"  SpanMd="5"  SpanSm="5">
                                         <telerik:RadPageLayout runat="server" ID="RDPLayoutADDTEmplate" GridType="Fluid">
                                            <Rows>
                                                <telerik:LayoutRow  ID="LRRlblSMSTempResult" runat="server" Visible="false" style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                    <Columns>
                     
                                                            <telerik:LayoutColumn   >
                                                                <telerik:RadLabel ID="RlblSMSTempResult" runat="server" ForeColor="#cc0000"></telerik:RadLabel>
                                                            </telerik:LayoutColumn> 
                       
                                                    </Columns>
                                                </telerik:LayoutRow>
                                            </Rows>    
                                            <Rows>
                                                <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px" >
                                                    <Columns>
                          
                                                            <telerik:LayoutColumn >
                                                                <telerik:RadTextBox RenderMode="Lightweight" runat="server" placeholder="Enter title" ID="txtSMStitle" Width="100%">
                                                                </telerik:RadTextBox>
                                                                <asp:RequiredFieldValidator ID="RFVtxtSMStitle" runat="server" Display="Dynamic" ValidationGroup="SMSTemplateSave"
                                                                ControlToValidate="txtSMStitle" ErrorMessage="Please enter title!" ForeColor="Red" />
                                                            </telerik:LayoutColumn> 
                    
                                                    </Columns>
                                                </telerik:LayoutRow>
                                            </Rows>
                                            <Rows>
                                                    <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                        <Columns>
                       
                                                                <telerik:LayoutColumn  >
                                                                <telerik:RadTextBox runat="server" ID="REditorSMS" EmptyMessage="Enter content" Height="100px" Width="100%"
                                                                    MaxLength ="139"  TextMode="MultiLine" onkeypress="Validate(this,event);">
                                                                </telerik:RadTextBox>
                                                                <asp:RequiredFieldValidator ID="RFVREditorSMS" runat="server" Display="Dynamic" ValidationGroup="SMSTemplateSave"
                                                                ControlToValidate="REditorSMS" ErrorMessage="Please enter content!" ForeColor="Red" />
                                                                </telerik:LayoutColumn> 
                          
                                                        </Columns>
                                                    </telerik:LayoutRow>
                                                </Rows>
                                            <Rows>
                                                    <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                        <Columns>
                               
                                                                <telerik:LayoutColumn >
                                                                <telerik:RadLabel ID="RLREditorSMSLength" runat="server" Text="0" Style="font-size: 14px; font-style: initial">
                                                                </telerik:RadLabel>
                                                                </telerik:LayoutColumn>
                             
                                                        </Columns>
                                                    </telerik:LayoutRow>
                                                </Rows> 
                                            <Rows>
                                                    <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                        <Columns>
                               
                                                                <telerik:LayoutColumn >
                                                                <telerik:RadLabel ID="RadLabel1"  runat="server" Text="Max Characters:140" ForeColor="Red" Style="font-size: 14px; color: red; font-style: initial"></telerik:RadLabel>
                                                                </telerik:LayoutColumn>
                             
                                                        </Columns>
                                                    </telerik:LayoutRow>
                                                </Rows>
                                            <Rows>
                                                    <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                        <Columns>
                             
                                                                <telerik:LayoutColumn Span="4" >
                                                                    <telerik:RadButton RenderMode="Lightweight" ID="RbtntempSMSActive" runat="server" Width="100%" ToggleType="CheckBox" ButtonType="StandardButton"
                                                                    AutoPostBack="false">
                                                                        <ToggleStates>
                                                                            <telerik:RadButtonToggleState Text="Active:True" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                                            <telerik:RadButtonToggleState Text="Active:False" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                                        </ToggleStates>
                                                                    </telerik:RadButton>
                                                                </telerik:LayoutColumn> 
                              
                                                        </Columns>
                                                    </telerik:LayoutRow>
                                                </Rows>
                                            <Rows>
                                                    <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px;margin-bottom:4px">
                                                        <Columns>
                            
                        
                                                                <telerik:LayoutColumn Span="2" >
                                      
                                                                    <telerik:RadButton ID="RPbtnSave"   Width="100%" OnClick="RPbtnSave_Click"  ValidationGroup="SMSTemplateSave" runat="server" Text="Save">
                                                                        <Icon PrimaryIconCssClass="rbSave"></Icon>
                                                                    </telerik:RadButton>

                                                                </telerik:LayoutColumn> 
                          
                                                            <telerik:LayoutColumn Span="2">
                                    
                                                                <telerik:RadButton ID="RPbtnADD"   Width="100%"   runat="server" OnClick="RPbtnADD_Click"  Text="Cancel">
                                                                        <Icon PrimaryIconCssClass="rbCancel"></Icon>
                                                                    </telerik:RadButton>
                                                            </telerik:LayoutColumn> 
                      
                                                        </Columns>
                        
                                                    </telerik:LayoutRow>
                                                </Rows>
                                        </telerik:RadPageLayout>
                                      </telerik:LayoutColumn>
                              </Columns>
                          </telerik:LayoutRow>
                    </Rows>
              </telerik:RadPageLayout>
    </asp:Panel>
</asp:Content>
