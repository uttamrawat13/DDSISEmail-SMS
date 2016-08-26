<%@ Page Title="Email Configuration" Language="C#" MasterPageFile="~/MainDDMailSMSMaster.Master" AutoEventWireup="true" CodeBehind="frmemailconfiguration.aspx.cs" Inherits="DDMailSmsWeb.frmemailconfiguration" %>
<asp:Content ID="ConCPhead" ContentPlaceHolderID="CPhead" runat="server">
</asp:Content>
<asp:Content ID="ConCPHDetail" ContentPlaceHolderID="CPHDetail" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RALPfrmemailconfiguration" runat="server" Height="75px"
        Width="75px" Transparency="50">
    </telerik:RadAjaxLoadingPanel>
     

     <telerik:RadAjaxManagerProxy ID="RAMfrmemailconfiguration" runat="server">
      <AjaxSettings>
       <telerik:AjaxSetting AjaxControlID="Pfrmemailconfiguration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Pfrmemailconfiguration" LoadingPanelID="RALPfrmemailconfiguration" ></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Panel runat="server" ID="Pfrmemailconfiguration">
       <telerik:RadPageLayout runat="server" ID="RDPLayoutfrmemailconfiguration" GridType="Fluid">
              <Rows>
                    <telerik:LayoutRow ID="LayoutRow1"   runat="server"  >
                        <Columns>
                             <telerik:LayoutColumn   Span="9"   SpanXs="7"  SpanMd="7"  SpanSm="7">
                               <div >
                                    
                                   <telerik:RadGrid ID="RgvEmailConfig" runat="server" AutoGenerateColumns="False" Width="100%" RenderMode="Auto" 
                                       FilterMenu-RenderMode="Lightweight"
                                GroupPanelPosition="Top" IsExporting="False" PageSize="8" Font-Size="10"
                                AllowFilteringByColumn="true" AllowPaging="True" AllowSorting="True"  OnNeedDataSource="RgvEmailConfig_NeedDataSource"
                                   OnItemCommand="RgvEmailConfig_ItemCommand" OnSortCommand="RgvEmailConfig_SortCommand">
                                <ItemStyle Wrap="true"></ItemStyle>
                                <MasterTableView AllowMultiColumnSorting="true">
                                    <Columns>
                                        <telerik:GridTemplateColumn Visible="false">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbID"  runat="server" Text='<%# Convert.ToString(Eval("ID")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn SortExpression="CampusName" ShowSortIcon="true" AllowSorting="true" HeaderText="Campus Name" HeaderButtonType="TextButton"
                                            DataField="CampusName">
                                        </telerik:GridBoundColumn>
                                       <telerik:GridTemplateColumn HeaderText="Campus Name" Visible="false" ShowSortIcon="true" AllowSorting="true" ItemStyle-VerticalAlign="Top" DataField="CampusName" FilterControlWidth="90%"   AllowFiltering="false">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbCampusName" runat="server" Text='<%# Convert.ToString(Eval("CampusName")) %>' />
                                                <asp:Label ID="lbCampusID" runat="server" Visible="false" Text='<%# Convert.ToString(Eval("CampusID")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>



                                     
                                        <telerik:GridTemplateColumn HeaderText="Department Name"   AllowSorting="true" SortExpression="Department" FilterControlWidth="80%" DataField="Department"  AllowFiltering="true">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="DeptMainName" runat="server" Text='<%# Convert.ToString(Eval("Department")) %>' />
                                                <asp:Label ID="lbDeptId" runat="server" Visible="false" Text='<%# Convert.ToString(Eval("DeptName")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                          <telerik:GridTemplateColumn HeaderText="Department ID"   AllowSorting="True" SortExpression="DeptName"  DataField="DeptName"  AllowFiltering="False">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                               <asp:Label ID="DeptName" runat="server" Text='<%# Convert.ToString(Eval("DeptName")) %>' />
                                                <asp:Label ID="lbDeptIdD" runat="server" Visible="false" Text='<%# Convert.ToString(Eval("DeptName")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                         <telerik:GridTemplateColumn HeaderText="Department Email" FilterControlWidth="80%" DataField="DeptEmail"  AllowFiltering="true">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbDeptEmail" runat="server" Text='<%# Convert.ToString(Eval("DeptEmail")) %>' />
                                                <asp:Label ID="lbPwd" Visible="false" runat="server" Text='<%# Convert.ToString(Eval("Pass")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                       
                                       
                                
                                        <telerik:GridTemplateColumn Visible="false" HeaderText="SMTP" FilterControlWidth="90%" DataField="SMTP"  AllowFiltering="true">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbSMTP" runat="server"  Text='<%# Convert.ToString(Eval("SMTP")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn Visible="false" HeaderText="PortOut" FilterControlWidth="90%" DataField="PortOut"  AllowFiltering="true">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbPortOut" runat="server" Text='<%# Convert.ToString(Eval("PortOut")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                       
                                        <telerik:GridTemplateColumn Visible="false" HeaderText="Pop3" FilterControlWidth="90%" DataField="Pop3"  AllowFiltering="true">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbPop3" runat="server" Text='<%# Convert.ToString(Eval("Pop3")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn Visible="false" HeaderText="PortIn" FilterControlWidth="90%" DataField="PortIn"  AllowFiltering="true">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbPortIn" runat="server" Text='<%# Convert.ToString(Eval("PortIn")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="SSL" Visible="false" FilterControlWidth="90%" DataField="SSL"  AllowFiltering="true">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbSSL" runat="server" Text='<%# Convert.ToString(Eval("SSL")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                         <telerik:GridTemplateColumn HeaderText="Active"  ItemStyle-VerticalAlign="Top" AllowFiltering="false">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ItemChkboxActiveuser" runat="server" Checked='<%# Convert.ToBoolean(Eval("Status")) %>'
                                                    AutoPostBack="True" OnCheckedChanged="ItemChkboxActiveuser_CheckedChanged"/>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Edit" AllowFiltering="false">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnedit" runat="server" AlternateText="Edit" ToolTip="Edit" Height="16px" Width="16px"
                                                    ImageUrl="~/images/pencil_edit_button.png"  CommandName="imgbtnedit" CommandArgument='<%# Eval("ID") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Delete" AllowFiltering="false">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtndelete" runat="server" AlternateText="Delete" ToolTip="Delete" Height="16px" Width="16px"
                                                    OnClientClick="javascript:return confirm('Are You Sure Delete This Email Configuration?')"
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
                        <telerik:LayoutColumn   Span="3"  SpanXs="5"  SpanMd="5"  SpanSm="5" >
                            
                             
                                   <telerik:RadPageLayout runat="server" ID="RDPLayoutCreateEmailConfiguation" GridType="Fluid">
                                       <Rows>
                                            <telerik:LayoutRow ID="LayoutRow2"   runat="server"  style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                  <Columns>
                                                        <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                              <telerik:RadPageLayout   runat="server"  GridType="Fluid">
                                                                    <Rows>
                                                                       <telerik:LayoutRow   style="margin-top:4px;margin-left:2px;margin-right:2px;margin-bottom:4px" runat="server"  >
                                                                                <Columns>
                                                                                     <telerik:LayoutColumn   Span="6"   SpanXs="6"  SpanMd="6"  SpanSm="6">
                                                                                            <span style="font:bold;font-size:14px;color:#25a0da;">Email Configuation</span>   
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
                                            <telerik:LayoutRow  ID="LRRlblEmailconfigResulut" runat="server" Visible="false" style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                        <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                           <telerik:RadLabel ID="RlblEmailconfigResulut" runat="server" ForeColor="#cc0000"></telerik:RadLabel>
                                                        </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                       </Rows>    
                                       <Rows>
                                            <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px" >
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                             <telerik:RadDropDownList RenderMode="Lightweight" ID="Rddlcampus"  Width="100%" runat="server" OnSelectedIndexChanged="Rddlcampus_SelectedIndexChanged" Font-Size="12px" DropDownHeight="200px"
                                                                     AutoPostBack="true">
                                                             </telerik:RadDropDownList>
                                                             <asp:RequiredFieldValidator runat="server" ID="RFVRddlcampus" InitialValue="Select Campus"
                                                                    ControlToValidate="Rddlcampus" ValidationGroup="VGEamilConfig" Display="Dynamic" ForeColor="#ff0000"
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
                                                                    AutoPostBack="false" DefaultMessage="Select Departments" >
                                                            </telerik:RadDropDownList> 
                                                            <asp:RequiredFieldValidator runat="server" ID="RFVRddlDept" InitialValue="Select Departments"
                                                                ControlToValidate="RddlDept" ValidationGroup="VGEamilConfig" Display="Dynamic" ForeColor="#ff0000"
                                                                ErrorMessage="Please Select Department" CssClass="validator" />
                                                        </telerik:LayoutColumn> 
                                                   
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows>
                                       <Rows>
                                            <telerik:LayoutRow ID="LayoutRow4"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                        <telerik:RadTextBox RenderMode="Lightweight" runat="server" placeholder="Department Email" ID="RtxtDeptEmail" Width="100%">
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RFVRtxtDeptEmail" runat="server" Display="Dynamic" ValidationGroup="VGEamilConfig"
                                                        ControlToValidate="RtxtDeptEmail" ErrorMessage="Please, Enter Department Email!" ForeColor="Red" />
                                                        <asp:RegularExpressionValidator ID="REVRtxtDeptEmail"
                                                        runat="server" ErrorMessage="Please, Enter Valid Email ID!"
                                                        ValidationGroup="VGEamilConfig" ControlToValidate="RtxtDeptEmail"
                                                        ForeColor="Red" Display="Dynamic" 
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                        </asp:RegularExpressionValidator>
                                                    </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows> 
                                        <Rows>
                                            <telerik:LayoutRow ID="LayoutRow3"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                        <telerik:RadTextBox RenderMode="Lightweight" runat="server" placeholder="Password" ID="RtxtPassword" Width="100%">
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RFVRtxtPassword" runat="server" Display="Dynamic" ValidationGroup="VGEamilConfig"
                                                        ControlToValidate="RtxtPassword" ErrorMessage="Please, Enter Password!" ForeColor="Red" />
                                                    </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows> 
                                        <Rows>
                                            <telerik:LayoutRow ID="LayoutRow7"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                        <telerik:RadTextBox RenderMode="Lightweight" runat="server" placeholder="SMTP (Simple Mail Transfer Protocol)" ID="RtxtSMTP" Width="100%">
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RFVRtxtSMTP" runat="server" Display="Dynamic" ValidationGroup="VGEamilConfig"
                                                        ControlToValidate="RtxtSMTP" ErrorMessage="Please, Enter SMTP!" ForeColor="Red" />
                                                    </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows>
                                        <Rows>
                                            <telerik:LayoutRow ID="LayoutRow8"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                        <telerik:RadTextBox RenderMode="Lightweight" runat="server" placeholder="Port" ID="RtxtPort" Width="100%">
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RFVRtxtPort" runat="server" Display="Dynamic" ValidationGroup="VGEamilConfig"
                                                        ControlToValidate="RtxtPort" ErrorMessage="Please, Enter Port!" ForeColor="Red" />
                                                    </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows>
                                       <Rows>
                                            <telerik:LayoutRow ID="LayoutRow9"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                        <telerik:RadTextBox RenderMode="Lightweight" runat="server" placeholder="POP3" ID="RtxtPOP3" Width="100%">
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RFVRtxtPOP3" runat="server" Display="Dynamic" ValidationGroup="VGEamilConfig"
                                                        ControlToValidate="RtxtPOP3" ErrorMessage="Please, Enter POP3!" ForeColor="Red" />
                                                    </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows>
                                       <Rows>
                                            <telerik:LayoutRow ID="LayoutRow10"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                        <telerik:RadTextBox RenderMode="Lightweight" runat="server" placeholder="Port In" ID="RtxtPortIn" Width="100%">
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RFVRtxtPortIn" runat="server" Display="Dynamic" ValidationGroup="VGEamilConfig"
                                                        ControlToValidate="RtxtPortIn" ErrorMessage="Please, Enter Port In!" ForeColor="Red" />
                                                    </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows>
                                       <Rows>
                                            <telerik:LayoutRow ID="LayoutRow11"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                        <telerik:RadButton RenderMode="Lightweight" ID="RbtnSSL" runat="server" style="text-align:left" Width="137px" ToggleType="CheckBox" ButtonType="StandardButton"
                                                            AutoPostBack="false">
                                                                <ToggleStates>
                                                                    <telerik:RadButtonToggleState Text="SSL True" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                                                    <telerik:RadButtonToggleState Text="SSL False" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                                                </ToggleStates>
                                                         </telerik:RadButton>
                                                         </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows>
                                       <Rows>
                                            <telerik:LayoutRow ID="LayoutRow5"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
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
                                                         <telerik:LayoutColumn   Span="6"  SpanXs="5"  SpanMd="3"  SpanSm="3" >
                                                              <telerik:RadButton ID="RPbtnSave"   Width="100%" OnClick="RPbtnSave_Click" ValidationGroup="VGEamilConfig" runat="server" Text="Save">
                                                                 <Icon PrimaryIconCssClass="rbSave"></Icon>
                                                              </telerik:RadButton>
                                                         </telerik:LayoutColumn> 
                                                         
                                                        <telerik:LayoutColumn   Span="4"  SpanXs="5"  SpanMd="3"  SpanSm="3"  >
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

