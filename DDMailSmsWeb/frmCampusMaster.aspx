<%@ Page Title="Manage Campus" Language="C#" MasterPageFile="~/MainDDMailSMSMaster.Master" AutoEventWireup="true" CodeBehind="frmCampusMaster.aspx.cs" Inherits="DDMailSmsWeb.frmCampusMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPhead" runat="server">
</asp:Content>
<asp:Content ID="ConCPHDetai" ContentPlaceHolderID="CPHDetail" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RALPfrmCampusMaster" runat="server" Height="75px"
        Width="75px" Transparency="50">
    </telerik:RadAjaxLoadingPanel>
 
     <telerik:RadAjaxManagerProxy ID="RAMfrmCampusMaster" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="PfrmCampusMaster">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PfrmCampusMaster" LoadingPanelID="RALPfrmCampusMaster"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
      </telerik:RadAjaxManagerProxy>
    <asp:Panel runat="server" ID="PfrmCampusMaster">
          <telerik:RadPageLayout runat="server" ID="RDPLayoutfrmCampusMaster" GridType="Fluid">
              <Rows>
                    <telerik:LayoutRow ID="LayoutRow1"   style="margin-top:4px;margin-left:2px;margin-right:2px;margin-bottom:4px" runat="server"  >
                        <Columns>
                             <telerik:LayoutColumn   Span="7"   SpanXs="7"  SpanMd="7"  SpanSm="7">
                               <div style="border: 1px solid #e9e4e4;padding:10px;height:inherit;">
                                    
                               <telerik:RadGrid ID="RgvCampusMaster" runat="server" AutoGenerateColumns="False" Width="100%" RenderMode="Auto" FilterMenu-RenderMode="Lightweight"
                                GroupPanelPosition="Top" IsExporting="False" PageSize="8" Font-Size="10"
                                AllowFilteringByColumn="true" AllowPaging="True" AllowSorting="True"  OnNeedDataSource="RgvCampusMaster_NeedDataSource"
                                   OnItemCommand="RgvCampusMaster_ItemCommand"  OnSortCommand="RgvCampusMaster_SortCommand">
                                <ItemStyle Wrap="true"></ItemStyle>
                                <MasterTableView AllowMultiColumnSorting="true">
                                    <Columns>
                                        <telerik:GridTemplateColumn Visible="false">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbCampusID"  runat="server" Text='<%# Convert.ToString(Eval("CampusID")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Campus Code" Visible="false" ItemStyle-VerticalAlign="Top" DataField="CampusCode" FilterControlWidth="90%"   AllowFiltering="true">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbCampusCode" runat="server" Text='<%# Convert.ToString(Eval("CampusCode")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn SortExpression="CampusName" AllowFiltering="true" ShowSortIcon="true" 
                                            AllowSorting="true" HeaderText="Campus Name" HeaderButtonType="TextButton"
                                            DataField="CampusName">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Campus Name" Visible="false" FilterControlWidth="90%" DataField="CampusName"  ItemStyle-VerticalAlign="Top" AllowFiltering="true">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbCampusName" runat="server" Text='<%# Convert.ToString(Eval("CampusName")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        
                                         <telerik:GridTemplateColumn HeaderText="Database Connection" FilterControlWidth="90%" DataField="CampusConStr" Visible="false"  AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbCampusConStr" runat="server" Text='<%# Convert.ToString(Eval("CampusConStr")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                         <telerik:GridTemplateColumn HeaderText="Status" ItemStyle-VerticalAlign="Top" AllowFiltering="false">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ItemChkboxActiveuser" runat="server" Checked='<%# Convert.ToBoolean(Eval("Active")) %>'
                                                    AutoPostBack="True" OnCheckedChanged="ItemChkboxActiveuser_CheckedChanged"/>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        
                                        <telerik:GridTemplateColumn HeaderText="Edit" AllowFiltering="false"  ItemStyle-VerticalAlign="Top">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnedit" runat="server" AlternateText="Edit" ToolTip="Edit" Height="16px" Width="16px"
                                                    ImageUrl="~/images/pencil_edit_button.png"  CommandName="imgbtnedit" CommandArgument='<%# Eval("CampusID") %>' />
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
                                                                                            <span style="font:bold;font-size:14px;color:#25a0da;">Campus Setup</span>   
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
                                            <telerik:LayoutRow  ID="LRRlblCreateCampusResult" runat="server" Visible="false" style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                        <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                            <telerik:RadLabel ID="RlblCreateCampusResult" runat="server" ForeColor="#cc0000"></telerik:RadLabel>
                                                        </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                       </Rows>    
                                     
                                       <Rows>
                                            <telerik:LayoutRow ID="LayoutRow4"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                        <telerik:RadTextBox RenderMode="Lightweight" runat="server" placeholder="Campus Code" ID="RtxtCampusCode" Width="100%">
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RFVRtxtCampusCode" runat="server" Display="Dynamic" ValidationGroup="VGCreateCampus"
                                                        ControlToValidate="RtxtCampusCode" ErrorMessage="Please enter campus code!" ForeColor="Red" />
                                                    </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows> 
                                        <Rows>
                                            <telerik:LayoutRow ID="LayoutRow5"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                        <telerik:RadTextBox RenderMode="Lightweight" runat="server" placeholder="Campus Name" ID="RtxtCampusName" Width="100%">
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RFVRtxtCampusName" runat="server" Display="Dynamic" ValidationGroup="VGCreateCampus"
                                                        ControlToValidate="RtxtCampusName" ErrorMessage="Please enter campus name!" ForeColor="Red" />
                                                    </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows>
                                      <Rows>
                                            <telerik:LayoutRow ID="LayoutRow7"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" > 
                                                        <telerik:RadTextBox RenderMode="Lightweight" runat="server" placeholder="Campus database connection string" ID="RtxtDatabaseConnection" Width="100%">
                                                        </telerik:RadTextBox>
                                                       <span style="padding: 5px;font-size: 12px;color: darkgreen;"><b>Example:- </b>Data Source=********;Initial Catalog=********;User ID=*****;Password=******* </span>
                                                          <telerik:RadLabel ID="RLChkconstr" runat="server" ForeColor="#cc0000"></telerik:RadLabel>
                                                             <asp:RequiredFieldValidator ID="RFVRtxtDatabaseConnection" runat="server" Display="Dynamic" ValidationGroup="VGCreateCampus"
                                                        ControlToValidate="RtxtDatabaseConnection" ErrorMessage="Please enter campus database connection string!" ForeColor="Red" />
                                                     
                                                           <telerik:RadButton ID="RBtnCheckConstr" ButtonType="ToggleButton"    Width="100%" OnClick="RBtnCheckConstr_Click"   runat="server" Text="Test Data Source...">
                                                        </telerik:RadButton>
                                                    </telerik:LayoutColumn> 
                                                    
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows>
                                        <Rows>
                                            <telerik:LayoutRow ID="LayoutRow6"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                        <telerik:RadButton RenderMode="Lightweight" ID="RbtnCamousStatus" runat="server" Width="120px" style="text-align:left" ToggleType="CheckBox" ButtonType="StandardButton"
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
                                                         <telerik:LayoutColumn   Span="2"  SpanXs="5"  SpanMd="2"  SpanSm="2" >
                                                              <telerik:RadButton ID="RPbtnSave"   Width="100%" OnClick="RPbtnSave_Click" ValidationGroup="VGCreateCampus" runat="server" Text="Save">
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

