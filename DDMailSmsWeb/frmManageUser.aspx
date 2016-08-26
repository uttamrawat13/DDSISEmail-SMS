<%@ Page Title="Manage User" Language="C#" MasterPageFile="~/MainDDMailSMSMaster.Master" AutoEventWireup="true" CodeBehind="frmManageUser.aspx.cs" Inherits="DDMailSmsWeb.frmManageUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPhead" runat="server">
</asp:Content>
<asp:Content ID="ConCPHDetai" ContentPlaceHolderID="CPHDetail" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RALPfrmManageUser" runat="server" Height="75px"
        Width="75px" Transparency="50">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManagerProxy ID="RAMfrmManageUser" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="PfrmManageUser">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PfrmManageUser" LoadingPanelID="RALPfrmManageUser"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Panel runat="server" ID="PfrmManageUser">
          <telerik:RadPageLayout runat="server" ID="RDPLayoutfrmemailconfiguration" GridType="Fluid">
              <Rows>
                    <telerik:LayoutRow ID="LayoutRow1"   style="margin-top:4px;margin-left:2px;margin-right:2px;margin-bottom:4px" runat="server"  >
                        <Columns>
                             <telerik:LayoutColumn   Span="7"   SpanXs="7"  SpanMd="7"  SpanSm="7">
                               <div style="border: 1px solid #e9e4e4;padding:10px;height:inherit;">
                                    
                               <telerik:RadGrid ID="RgvManageUser" runat="server" AutoGenerateColumns="False" Width="100%" 
                                   RenderMode="Auto" FilterMenu-RenderMode="Lightweight"
                                GroupPanelPosition="Top" IsExporting="False" PageSize="8" Font-Size="10"
                                AllowFilteringByColumn="true" AllowPaging="True" AllowSorting="True"  OnNeedDataSource="RgvManageUser_NeedDataSource"
                                   OnItemCommand="RgvManageUser_ItemCommand" OnSortCommand="RgvManageUser_SortCommand">
                                <ItemStyle Wrap="true"></ItemStyle>
                                <MasterTableView AllowMultiColumnSorting="true">
                                    <Columns>
                                       
                                        <telerik:GridBoundColumn SortExpression="CampusName" UniqueName="UNCampusName"  AllowFiltering="true" 
                                            ShowSortIcon="true" AllowSorting="true"
                                             HeaderText="Campus Name" HeaderButtonType="TextButton"
                                            DataField="CampusName">
                                        </telerik:GridBoundColumn>
                     
                                        <telerik:GridTemplateColumn HeaderText="Campus Name" FilterControlWidth="90%" DataField="CampusId" 
                                            Visible="false"  AllowFiltering="true" >
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbCampusId" runat="server" Visible="false" Text='<%# Convert.ToString(Eval("CampusId")) %>' />
                                                <asp:Label ID="lbCampusName" runat="server" Visible="false" Text='<%# Convert.ToString(Eval("CampusName")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                         <telerik:GridTemplateColumn Visible="false">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbUserID"  runat="server" Text='<%# Convert.ToString(Eval("UserID")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn SortExpression="Username" UniqueName="UNUsername" AllowFiltering="true" ShowSortIcon="true"
                                             AllowSorting="true"
                                             HeaderText="User Name" HeaderButtonType="TextButton"
                                            DataField="Username">
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn SortExpression="Role" UniqueName="UNRole"    AllowFiltering="true" 
                                             ShowSortIcon="true" AllowSorting="true"
                                             HeaderText="User Role Name" HeaderButtonType="TextButton"
                                            DataField="Role">
                                        </telerik:GridBoundColumn>

                                         <telerik:GridTemplateColumn HeaderText="User Role Name" Visible="false" FilterControlWidth="90%" DataField="Role"  AllowFiltering="true">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                  <asp:Label ID="lblpassword" Visible="false" runat="server" Text='<%# Convert.ToString(Eval("Password")) %>' />
                                                <asp:Label ID="lbSwitchDept" Visible="false" runat="server" Text='<%# Convert.ToString(Eval("SwitchDept")) %>' />
                                                <asp:Label ID="lbRole" runat="server" Text='<%# Convert.ToString(Eval("Role")) %>' />
                                                <asp:Label ID="lbRoleId" runat="server" Visible="false" Text='<%# Convert.ToString(Eval("User_level")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Status" ItemStyle-VerticalAlign="Top" AllowFiltering="false">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ItemChkboxActiveuser" runat="server" Checked='<%# Convert.ToBoolean(Eval("UserStatus")) %>'
                                                    AutoPostBack="True" OnCheckedChanged="ItemChkboxActiveuser_CheckedChanged"/>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Edit" AllowFiltering="false">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnedit" runat="server" AlternateText="Edit" ToolTip="Edit This User" Height="16px" Width="16px"
                                                    ImageUrl="~/images/pencil_edit_button.png"  CommandName="imgbtnedit" CommandArgument='<%# Eval("UserID") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Delete" AllowFiltering="false">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtndelete" runat="server" AlternateText="Delete" ToolTip="Delete This User" Height="16px" Width="16px"
                                                    OnClientClick="javascript:return confirm('Are You Sure Delete This User?')"
                                                    ImageUrl="~/images/Delete.png" CommandName="imgbtndelete" CommandArgument='<%# Eval("UserID") %>' />
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
                        <telerik:LayoutColumn   Span="5"  SpanXs="5"  SpanMd="5"  SpanSm="5" >
                            
                             
                                   <telerik:RadPageLayout runat="server" ID="RDPLayout" GridType="Fluid">
                                       <Rows>
                                            <telerik:LayoutRow ID="LayoutRow2"   runat="server"  style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                  <Columns>
                                                        <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                              <telerik:RadPageLayout ID="RadPageLayout1"   runat="server"  GridType="Fluid">
                                                                    <Rows>
                                                                       <telerik:LayoutRow ID="LayoutRow3"   style="margin-top:4px;margin-left:2px;margin-right:2px;margin-bottom:4px" runat="server"  >
                                                                                <Columns>
                                                                                     <telerik:LayoutColumn   Span="6"   SpanXs="6"  SpanMd="6"  SpanSm="6">
                                                                                            <span style="font:bold;font-size:14px;color:#25a0da;">Registration</span>   
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
                                            <telerik:LayoutRow  ID="LRRlblCreateNewUserResult" runat="server" Visible="false" style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                        <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                            <telerik:RadLabel ID="RlblCreateNewUserResult" runat="server" ForeColor="#cc0000"></telerik:RadLabel>
                                                        </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                       </Rows>    
                                       <Rows>
                                            <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px" >
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                             <telerik:RadDropDownList RenderMode="Lightweight" ID="Rddlcampus" OnSelectedIndexChanged="Rddlcampus_SelectedIndexChanged"   Width="100%" runat="server" Font-Size="12px" DropDownHeight="200px"
                                                                     AutoPostBack="true">
                                                             </telerik:RadDropDownList>
                                                             <asp:RequiredFieldValidator runat="server" ID="RFVRddlcampus" InitialValue="Select Campus"
                                                                    ControlToValidate="Rddlcampus" ValidationGroup="VGCreateUser" Display="Dynamic" ForeColor="#ff0000"
                                                                    ErrorMessage="Please select campus" CssClass="validator" />
                                                     </telerik:LayoutColumn>
                                                </Columns>
                                            </telerik:LayoutRow>
                                       </Rows>
                                       <Rows>
                                            <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                      <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                          <telerik:RadDropDownList RenderMode="Lightweight" ID="RddlRole"  Width="100%" runat="server" Font-Size="12px" DropDownHeight="200px"
                                                            AutoPostBack="false">
                                                            </telerik:RadDropDownList>
                                                            <asp:RequiredFieldValidator runat="server" ID="RFVRddlRole" InitialValue="Select level"
                                                            ControlToValidate="RddlRole" ValidationGroup="VGCreateUser" Display="Dynamic" ForeColor="#ff0000"
                                                            ErrorMessage="Please Select level" CssClass="validator" />
                                                        </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows>
                                       <Rows>
                                            <telerik:LayoutRow ID="LayoutRow4"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                        <telerik:RadTextBox RenderMode="Lightweight" runat="server" placeholder="User Name" ID="txtUserName" Width="100%">
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RFVtxtUserName" runat="server" Display="Dynamic" ValidationGroup="VGCreateUser"
                                                        ControlToValidate="txtUserName" ErrorMessage="Please enter usernname!" ForeColor="Red" />
                                                    </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows> 
                                        <Rows>
                                            <telerik:LayoutRow ID="LayoutRow7"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                            <telerik:RadTextBox RenderMode="Lightweight" runat="server" TextMode="Password"  placeholder="Password" ID="txtPassword" Width="100%">
                                                            </telerik:RadTextBox>
                                                            <asp:RequiredFieldValidator ID="RFVtxtPassword" runat="server" TextMode="Password" Display="Dynamic" ValidationGroup="VGCreateUser"
                                                            ControlToValidate="txtPassword" ErrorMessage="Please enter password!"  ForeColor="Red" />
                                                    </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows>
                                        <Rows>
                                            <telerik:LayoutRow ID="LayoutRow8"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                            <telerik:RadTextBox RenderMode="Lightweight" runat="server" TextMode="Password" Placeholder="Confirm Password" ID="txtConfirmPassword" Width="100%">
                                                            </telerik:RadTextBox>                                
                                                            <asp:RequiredFieldValidator ID="RFVtxtConfirmPassword" runat="server" Display="Dynamic" ValidationGroup="VGCreateUser"
                                                            ControlToValidate="txtConfirmPassword" ErrorMessage="Please, Enter Confirm Password!" ForeColor="Red" />
                                                            <asp:CompareValidator runat="server" id="cmpNumbers" controltovalidate="txtConfirmPassword" controltocompare="txtPassword" 
                                                            operator="Equal"   errormessage="Passwords must match!" ValidationGroup="VGCreateUser"  ForeColor="Red" />                                                    </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows>
                                         <Rows>
                                            <telerik:LayoutRow ID="LayoutRow5"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                        <telerik:RadButton RenderMode="Lightweight" ID="RbtnAllSwitchdept" runat="server" Width="100%" ToggleType="CheckBox" ButtonType="StandardButton"
                                                            AutoPostBack="false">
                                                                <ToggleStates>
                                                                    <telerik:RadButtonToggleState Text="Allow Switch Department:True" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                                    <telerik:RadButtonToggleState Text="Allow Switch Department:False" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                                </ToggleStates>
                                                         </telerik:RadButton>
                                                         </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows>
                                        <Rows>
                                            <telerik:LayoutRow ID="LayoutRow6"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                        <telerik:RadButton RenderMode="Lightweight" ID="RbtnUserStatus" runat="server" Width="100%" ToggleType="CheckBox" ButtonType="StandardButton"
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
                                                         <telerik:LayoutColumn   Span="4"  SpanXs="5"  SpanMd="2"  SpanSm="2" >
                                                              <telerik:RadButton ID="RPbtnSave"   Width="100%" OnClick="RPbtnSave_Click" ValidationGroup="VGCreateUser" runat="server" Text="Save">
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

