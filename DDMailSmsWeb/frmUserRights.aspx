<%@ Page Title="Manage User Rights" Language="C#" MasterPageFile="~/MainDDMailSMSMaster.Master" AutoEventWireup="true" CodeBehind="frmUserRights.aspx.cs" Inherits="DDMailSmsWeb.frmUserRights" %>
<asp:Content ID="ConCPhead" ContentPlaceHolderID="CPhead" runat="server">
</asp:Content>
<asp:Content ID="ConCPHDetail" ContentPlaceHolderID="CPHDetail" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RALPfrmUserRights" runat="server" Height="75px"
        Width="75px" Transparency="50">
    </telerik:RadAjaxLoadingPanel>
   

     <telerik:RadAjaxManagerProxy ID="RAMfrmUserRights" runat="server">
      <AjaxSettings>
       <telerik:AjaxSetting AjaxControlID="PfrmUserRights">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PfrmUserRights" LoadingPanelID="RALPfrmUserRights" ></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Panel runat="server" ID="PfrmUserRights">
       <telerik:RadPageLayout runat="server" ID="RDPLayoutfrmUserRights" GridType="Fluid">
             
           <Rows>
                    <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px;margin-bottom:4px" runat="server"  >
                        <Columns>
                              <telerik:LayoutColumn   Span="6"   SpanXs="6"  SpanMd="6"  SpanSm="6">
                                  <div style="border: 1px solid #e9e4e4;padding:10px;height:inherit">
                                       <telerik:RadPageLayout ID="RadPageLayout1" runat="server"  GridType="Fluid">
                                    
                                         <Rows>
                                           <telerik:LayoutRow ID="LayoutRow2"  style="margin-top:4px;margin-left:2px;margin-right:2px;margin-bottom:4px" runat="server"  >
                                                    <Columns>
                                                          <telerik:LayoutColumn   Span="6"   SpanXs="6"  SpanMd="6"  SpanSm="6">
                                                                <telerik:RadDropDownList RenderMode="Lightweight" ID="Rddlcampus"  Width="100%" runat="server"
                                                                     Font-Size="12px" DropDownHeight="200px" OnSelectedIndexChanged="Rddlcampus_SelectedIndexChanged"
                                                                     AutoPostBack="true">
                                                             </telerik:RadDropDownList>
                                                         </telerik:LayoutColumn>
                                                    </Columns>
                                           </telerik:LayoutRow>
                                        </Rows>
                                        <Rows>
                                          <telerik:LayoutRow ID="LayoutRow1"  style="margin-top:4px;margin-left:2px;margin-right:2px;margin-bottom:4px" runat="server"  >
                                                    <Columns>
                                                         <telerik:LayoutColumn   Span="12"   SpanXs="12"  SpanMd="12"  SpanSm="12">
                                                               

                                                                <telerik:RadGrid ID="RGRole" runat="server" AutoGenerateColumns="False" 
                                                                     RenderMode="Auto"  FilterMenu-RenderMode="Lightweight"
                                                                    OnSortCommand="RgvAddRole_SortCommand"  GroupPanelPosition="Top" PageSize="8" Font-Size="10"
                                                                    OnItemCommand="RgvAddRole_ItemCommand" OnNeedDataSource="RGRole_NeedDataSource"
                                                                    AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="true">
                                                                        <ItemStyle Wrap="true"></ItemStyle>
                                                                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                                    <MasterTableView  AllowMultiColumnSorting="true">
                                                                        <Columns>
                                                                            <telerik:GridTemplateColumn HeaderText="Campus Name"  ShowSortIcon="true"  AllowSorting="false" ItemStyle-VerticalAlign="Top" DataField="CampusName"   AllowFiltering="false">
                                                                                <HeaderStyle CssClass="desktopgridItem" />
                                                                                <ItemStyle CssClass="desktopgridItem" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbRole_Id" runat="server" Visible="false" Text='<%# Convert.ToString(Eval("Role_Id")) %>' />
                                                                                     <asp:Label ID="lbCampusName" runat="server"  Text='<%# Convert.ToString(Eval("CampusName")) %>' />
                                                                                    <asp:Label ID="lbCampusID" runat="server" Visible="false" Text='<%# Convert.ToString(Eval("CampusID")) %>' />
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                             <telerik:GridBoundColumn  AllowFiltering="true" ShowSortIcon="true" AllowSorting="true" HeaderText="Role"
                                                                                  HeaderButtonType="TextButton"
                                                                                DataField="Role">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Edit"  AllowFiltering="false">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="ViewRole" runat="server" AlternateText="Delete" ToolTip="Edit"
                                                                                        Height="16px" Width="16px"
                                                                                        ImageUrl="~/images/pencil_edit_button.png" CommandName="ViewSMSTemp" CommandArgument='<%# Eval("Role_Id") %>' />
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>

                                                                        </Columns>
                                                                         
                                                                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                                                                    </MasterTableView>
                                                                    <PagerStyle AlwaysVisible="true" Mode="NumericPages"></PagerStyle>
                                                                    <FilterMenu RenderMode="Lightweight"></FilterMenu>
                                                                    <HeaderContextMenu RenderMode="Lightweight"></HeaderContextMenu>
                                                                    <ClientSettings>
                                                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                                    </ClientSettings>  
                                                            </telerik:RadGrid>
                                                         </telerik:LayoutColumn>
                                                    </Columns>
                                           </telerik:LayoutRow>
                                        </Rows>
                                     </telerik:RadPageLayout>       

                                     
                                            
                                  </div>
                             </telerik:LayoutColumn>
                              <telerik:LayoutColumn   Span="6"   SpanXs="6"  SpanMd="6"  SpanSm="6">
                               <div style="border: 1px solid #e9e4e4;padding:10px;height:auto;margin-left:4px">
                                    <telerik:RadPageLayout runat="server"  GridType="Fluid">
                                        <Rows>
                                           <telerik:LayoutRow  style="margin-top:4px;margin-left:2px;margin-right:2px;margin-bottom:4px" runat="server"  >
                                                    <Columns>
                                                
                                                         <telerik:LayoutColumn   Span="5"   SpanXs="12"  SpanMd="3"  SpanSm="3">
                                                             <div style="float:left">
                                                                <telerik:RadButton ID="RPbtnSave"   Width="100%"  runat="server" Text="Save" OnClick="RPbtnSave_Click">
                                                                     <Icon PrimaryIconCssClass="rbSave"></Icon>
                                                                 </telerik:RadButton>
                                                            </div>
                                                       </telerik:LayoutColumn>
                                                    </Columns>
                                           </telerik:LayoutRow>
                                         </Rows>
                                     </telerik:RadPageLayout>       
                                   
                                        <telerik:RadGrid ID="RgvAddRole" runat="server" AutoGenerateColumns="False" Width="100%" RenderMode="Auto" FilterMenu-RenderMode="Lightweight"
                                         OnNeedDataSource="RgvAddRole_NeedDataSource"
                                         GroupPanelPosition="Top" PageSize="15" Font-Size="10" OnItemDataBound="RgvAddRole_ItemDataBound"
                                         AllowPaging="True" AllowSorting="True">
                                        <ItemStyle Wrap="true"></ItemStyle>
                                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>

                                        <MasterTableView >
                                            <Columns>
                                        
                                                <telerik:GridNumericColumn DataField="menu" HeaderText="Modules" ItemStyle-VerticalAlign="Top" Visible="True" AllowFiltering="true">
                                                </telerik:GridNumericColumn>
                                                <telerik:GridTemplateColumn HeaderText="Show Menu" ItemStyle-VerticalAlign="Top" AllowFiltering="false">
                                                    <HeaderStyle />                                           
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkShowMenu"  runat="server" Checked='<%# Convert.ToBoolean(Eval("show_menu")) %>'/>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn  Visible="false" ItemStyle-VerticalAlign="Top" AllowFiltering="false">
                                                    <HeaderStyle />                                           
                                                    <ItemTemplate>
                                                         <telerik:RadLabel ID="Rshowmenu"   Text="'<%# Eval("show_menu") %>'" ></telerik:RadLabel>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn  Visible="false" ItemStyle-VerticalAlign="Top" AllowFiltering="false">
                                                    <HeaderStyle />                                           
                                                    <ItemTemplate>
                                                       
                                                        <asp:Label ID="Rmenu_id"   runat="server" Text='<%# Convert.ToString(Eval("menu_id")) %>' />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                              
                                            <PagerStyle AlwaysVisible="True"></PagerStyle>
                                        </MasterTableView>
                                        <PagerStyle AlwaysVisible="true" Mode="NumericPages"></PagerStyle>
                                        <FilterMenu RenderMode="Lightweight"></FilterMenu>
                                        <HeaderContextMenu RenderMode="Lightweight"></HeaderContextMenu>
                                    </telerik:RadGrid>
                                      </div>
                             </telerik:LayoutColumn>  
                      </Columns>
                        
                    </telerik:LayoutRow>
               </Rows>
       </telerik:RadPageLayout>

       
    </asp:Panel>
</asp:Content>

