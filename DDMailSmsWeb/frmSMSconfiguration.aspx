<%@ Page Title="SMS Configuration" Language="C#" MasterPageFile="~/MainDDMailSMSMaster.Master" AutoEventWireup="true" CodeBehind="frmSMSconfiguration.aspx.cs" Inherits="DDMailSmsWeb.frmSMSconfiguration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPhead" runat="server">
</asp:Content>
<asp:Content ID="ConCPHDetail" ContentPlaceHolderID="CPHDetail" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RALPfrmSMSconfiguration" runat="server" Height="75px"
        Width="75px" Transparency="50">
    </telerik:RadAjaxLoadingPanel>
     

     <telerik:RadAjaxManagerProxy ID="RAMfrmSMSconfiguration" runat="server">
      <AjaxSettings>
       <telerik:AjaxSetting AjaxControlID="PfrmSMSconfiguration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PfrmSMSconfiguration" LoadingPanelID="RALPfrmSMSconfiguration" ></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Panel runat="server" ID="PfrmSMSconfiguration">
       <telerik:RadPageLayout runat="server" ID="RDPLayoutfrmSMSconfiguration" GridType="Fluid">
              <Rows>
                    <telerik:LayoutRow ID="LR"   runat="server"  >
                        <Columns>
                             <telerik:LayoutColumn   Span="8"   SpanXs="7"  SpanMd="7"  SpanSm="7">
                               <div >
                                    
                                   <telerik:RadGrid ID="RgvSMSConfig" runat="server" AutoGenerateColumns="False" Width="100%" RenderMode="Auto" 
                                       FilterMenu-RenderMode="Lightweight"
                                GroupPanelPosition="Top" IsExporting="False" PageSize="8" Font-Size="10"
                                AllowFilteringByColumn="true" AllowPaging="True" AllowSorting="True"  OnNeedDataSource="RgvSMSConfig_NeedDataSource"
                                   OnItemCommand="RgvSMSConfig_ItemCommand" OnSortCommand="RgvSMSConfig_SortCommand">
                                <ItemStyle Wrap="true"></ItemStyle>
                                <MasterTableView AllowMultiColumnSorting="true">
                                    <Columns>
                                        <telerik:GridTemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbID"  runat="server" Text='<%# Convert.ToString(Eval("ID")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                     
                                        
                                       <telerik:GridTemplateColumn HeaderText="Campus Name"  ShowSortIcon="true" SortExpression="CampusName" AllowSorting="true" ItemStyle-VerticalAlign="Top"
                                            DataField="CampusName"  AllowFiltering="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lbCampusName" runat="server" Text='<%# Convert.ToString(Eval("CampusName")) %>' />
                                                <asp:Label ID="lbCampusID" runat="server" Visible="false" Text='<%# Convert.ToString(Eval("CampusID")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        
                                        <telerik:GridTemplateColumn HeaderText="Department Name"   AllowSorting="true" SortExpression="DeptName" 
                                             DataField="DeptName"  AllowFiltering="true">
                                            <ItemTemplate>
                                                <asp:Label ID="DeptMainName" runat="server" Text='<%# Convert.ToString(Eval("DeptName")) %>' />
                                                <asp:Label ID="lbDeptId" runat="server" Visible="false" Text='<%# Convert.ToString(Eval("DeptID")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                          <telerik:GridTemplateColumn HeaderText="Department ID"   AllowSorting="false" SortExpression="DeptID" 
                                             DataField="DeptID"  AllowFiltering="true">
                                            <ItemTemplate>
                                                <asp:Label ID="DeptMainNamee" runat="server" Text='<%# Convert.ToString(Eval("DeptID")) %>' />
                                                <asp:Label ID="lbDeptIdd" runat="server" Visible="false" Text='<%# Convert.ToString(Eval("DeptID")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        
                                        <telerik:GridTemplateColumn HeaderText="LongCode"   AllowSorting="true" SortExpression="LongCode"   AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbLongCode" runat="server" Text='<%# Convert.ToString(Eval("LongCode")) %>' />                                     
                                                <asp:Label ID="lbAccountSID" runat="server" Visible="false" Text='<%# Convert.ToString(Eval("AccountSID")) %>' />
                                                <asp:Label ID="lbAuthToken" runat="server" Visible="false" Text='<%# Convert.ToString(Eval("AuthToken")) %>' />
                                             </ItemTemplate> 
                                        </telerik:GridTemplateColumn>
                                         <telerik:GridTemplateColumn HeaderText="Active"  ItemStyle-VerticalAlign="Top" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ItemChkboxActiveuser" runat="server" Checked='<%# Convert.ToBoolean(Eval("Active")) %>'
                                                    AutoPostBack="True" OnCheckedChanged="ItemChkboxActiveuser_CheckedChanged"/>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Edit" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnedit" runat="server" AlternateText="Edit" ToolTip="Edit" Height="16px" Width="16px"
                                                    ImageUrl="~/images/pencil_edit_button.png"  CommandName="imgbtnedit" CommandArgument='<%# Eval("ID") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Delete" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtndelete" runat="server" AlternateText="Delete" ToolTip="Delete" Height="16px" Width="16px"
                                                    OnClientClick="javascript:return confirm('Are You Sure Delete This SMS Configuration?')"
                                                    ImageUrl="~/images/Delete.png" CommandName="imgbtndelete" CommandArgument='<%# Eval("ID") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                       </Columns>
                                </MasterTableView> 
                                <PagerStyle AlwaysVisible="true" Mode="NumericPages"></PagerStyle>
                                <FilterMenu RenderMode="Lightweight"></FilterMenu>
                                <HeaderContextMenu RenderMode="Lightweight"></HeaderContextMenu>
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                </ClientSettings>
                            </telerik:RadGrid>
                           </div>
                         </telerik:LayoutColumn>
                        <telerik:LayoutColumn   Span="4"  SpanXs="5"  SpanMd="5"  SpanSm="5" >
                            
                             
                                   <telerik:RadPageLayout runat="server" ID="RDPLayoutCreateEmailConfiguation" GridType="Fluid">
                                       <Rows>
                                            <telerik:LayoutRow ID="LayoutRow2"   runat="server"  style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                  <Columns>
                                                        <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                              <telerik:RadPageLayout ID="RadPageLayout1"   runat="server"  GridType="Fluid">
                                                                    <Rows>
                                                                       <telerik:LayoutRow ID="LayoutRow3"   style="margin-top:4px;margin-left:2px;margin-right:2px;margin-bottom:4px" runat="server"  >
                                                                                <Columns>
                                                                                     <telerik:LayoutColumn   Span="6"   SpanXs="6"  SpanMd="6"  SpanSm="6">
                                                                                            <span style="font:bold;font-size:14px;color:#25a0da;">SMS Configuation</span>   
                                                                                     </telerik:LayoutColumn>  

                                                                                     <telerik:LayoutColumn   Span="6"   SpanXs="5"  SpanMd="6"  SpanSm="6">
                                                                                         <div style="float:right;text-align:right;">
                                                                                             <span style="font:bold;font-size:14px;color:#25a0da;">(<span style="color:red">*</span>)Mandatory</span>
                                                                                        </div>
                                                                                   </telerik:LayoutColumn>
                                                                                </Columns>
                                                                       </telerik:LayoutRow>
                                                                     </Rows>
                                                                 </telerik:RadPageLayout>
                                                        </telerik:LayoutColumn> 
                                                   </Columns>
                                            </telerik:LayoutRow>
                                      </Rows>
                                       <Rows>
                                            <telerik:LayoutRow  ID="LRRlblSMSconfigResulut" runat="server" Visible="false" style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                        <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                           <telerik:RadLabel ID="RlblSMSconfigResulut" runat="server" ForeColor="#cc0000"></telerik:RadLabel>
                                                        </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                       </Rows>    
                                       <Rows>
                                            <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px" >
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                             <telerik:RadDropDownList RenderMode="Lightweight" ID="Rddlcampus"  Width="100%" runat="server" Font-Size="12px" DropDownHeight="200px"
                                                                     AutoPostBack="true" OnSelectedIndexChanged="Rddlcampus_SelectedIndexChanged">
                                                             </telerik:RadDropDownList>
                                                             <asp:RequiredFieldValidator runat="server" ID="RFVRddlcampus" InitialValue="Select Campus"
                                                                    ControlToValidate="Rddlcampus" ValidationGroup="VGSMSConfig" Display="Dynamic" ForeColor="#ff0000"
                                                                    ErrorMessage="Please select campus" CssClass="validator" />
                                                     </telerik:LayoutColumn>
                                                </Columns>
                                            </telerik:LayoutRow>
                                       </Rows>
                                       <Rows>
                                            <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                      <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                            <telerik:RadDropDownList RenderMode="Lightweight" ID="RddlDept"  Width="100%" runat="server" Font-Size="12px" DropDownHeight="200px"
                                                                   DefaultMessage="Select Departments"  AutoPostBack="false">
                                                            </telerik:RadDropDownList>
                                                            <asp:RequiredFieldValidator runat="server" ID="RFVRddlDept" InitialValue="Select Departments"
                                                                ControlToValidate="RddlDept" ValidationGroup="VGSMSConfig" Display="Dynamic" ForeColor="#ff0000"
                                                                ErrorMessage="Please Select Department" CssClass="validator" />
                                                        </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows>
                                           <Rows>
                                            <telerik:LayoutRow ID="LayoutRow10"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                        <telerik:RadTextBox RenderMode="Lightweight" runat="server" placeholder="Long Code" ID="RtxtLongCode" Width="100%">
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RFVRtxtLongCode" runat="server" Display="Dynamic" ValidationGroup="VGSMSConfig"
                                                        ControlToValidate="RtxtLongCode" ErrorMessage="Please Enter Long code" ForeColor="Red" />
                                                    </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows>
                                   
                                       <Rows>
                                            <telerik:LayoutRow ID="LayoutRow1"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                        <telerik:RadTextBox RenderMode="Lightweight" runat="server" placeholder="AccountSID" ID="RtxtAccountSID" Width="100%">
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RFVRtxtAccountSID" runat="server" Display="Dynamic" ValidationGroup="VGSMSConfig"
                                                        ControlToValidate="RtxtAccountSID" ErrorMessage="Please Enter AccountSID" ForeColor="Red" />
                                                    </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows>
                                       <Rows>
                                            <telerik:LayoutRow ID="LayoutRow4"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                        <telerik:RadTextBox RenderMode="Lightweight" runat="server" placeholder="Auth Token" ID="RtxtAuthToken" Width="100%">
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RFVRtxtAuthToken" runat="server" Display="Dynamic" ValidationGroup="VGSMSConfig"
                                                        ControlToValidate="RtxtAuthToken" ErrorMessage="Please Enter Auth Token" ForeColor="Red" />
                                                    </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows>
                                       <Rows>
                                            <telerik:LayoutRow ID="LayoutRow6"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                        <telerik:RadButton RenderMode="Lightweight" ID="RbtnStatus" runat="server" style="text-align:left"  Width="137px" ToggleType="CheckBox" ButtonType="StandardButton"
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
                                                <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px;margin-bottom:4px">
                                                    <Columns>
                                                         <telerik:LayoutColumn   Span="3"  SpanXs="5"  SpanMd="3"  SpanSm="3" >
                                                              <telerik:RadButton ID="RPbtnSave"   Width="100%" OnClick="RPbtnSave_Click" ValidationGroup="VGSMSConfig" runat="server" Text="Save">
                                                                 <Icon PrimaryIconCssClass="rbSave"></Icon>
                                                              </telerik:RadButton>
                                                         </telerik:LayoutColumn> 
                                                         <telerik:LayoutColumn   Span="1"  SpanXs="1"  SpanMd="1"  SpanSm="1" >
                                                         </telerik:LayoutColumn> 
                                                        <telerik:LayoutColumn   Span="3"  SpanXs="5"  SpanMd="3"  SpanSm="3"  >
                                                            <telerik:RadButton ID="RPbtnCancel"   Width="100%"   runat="server" OnClick="RPbtnCancel_Click"  Text="Cancel">
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

