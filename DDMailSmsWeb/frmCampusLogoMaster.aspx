<%@ Page Title="Manage Campus Logo" Language="C#" MasterPageFile="~/MainDDMailSMSMaster.Master" AutoEventWireup="true" CodeBehind="frmCampusLogoMaster.aspx.cs" Inherits="DDMailSmsWeb.frmCampusLogoMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPhead" runat="server">
</asp:Content>
<asp:Content ID="ConCPHDetai" ContentPlaceHolderID="CPHDetail" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RALPfrmCampusLogoMaster" runat="server" Height="75px"
        Width="75px" Transparency="50">
    </telerik:RadAjaxLoadingPanel>

     <telerik:RadAjaxManagerProxy ID="RAMfrmCampusLogoMaster" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="PfrmCampusLogoMaster">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PfrmCampusLogoMaster" LoadingPanelID="RALPfrmCampusLogoMaster"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Panel runat="server" ID="PfrmCampusLogoMaster">
          <telerik:RadPageLayout runat="server" ID="RDPLayoutfrmCampusLogoMaster" GridType="Fluid">
              <Rows>
                    <telerik:LayoutRow ID="LayoutRow1"   style="margin-top:4px;margin-left:2px;margin-right:2px;margin-bottom:4px" runat="server"  >
                        <Columns>
                             <telerik:LayoutColumn   Span="7"   SpanXs="7"  SpanMd="7"  SpanSm="7">
                               <div style="border: 1px solid #e9e4e4;padding:10px;height:inherit;">
                                    
                               <telerik:RadGrid ID="RgvCampusMaster" runat="server" AutoGenerateColumns="False" Width="100%" RenderMode="Auto" 
                                   FilterMenu-RenderMode="Lightweight"
                                GroupPanelPosition="Top" IsExporting="False" PageSize="8" Font-Size="10"
                                AllowFilteringByColumn="true" AllowPaging="True" AllowSorting="True"  OnNeedDataSource="RgvCampusMaster_NeedDataSource"
                                   OnItemCommand="RgvCampusMaster_ItemCommand" OnSortCommand="RgvCampusMaster_SortCommand">
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
                                        
                                         <telerik:GridTemplateColumn HeaderText="Logo" FilterControlWidth="90%" DataField="Clientlogo"  AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:Image ID="imgCampusLogo" runat="server" Width="100px" Height="50px" ImageUrl='<%# string.Format("Content/clientlogo/{0}", Eval("Clientlogo")) %>' />
                                              
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
                                                                                            <span style="font:bold;font-size:14px;color:#25a0da;">Logo Setup</span>   
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
                                            <telerik:LayoutRow  ID="LRRlblCreateCampusLogoResult" runat="server" Visible="false" style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                        <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                            <telerik:RadLabel ID="RlblCreateCampusLogoResult" runat="server" ForeColor="#cc0000"></telerik:RadLabel>
                                                        </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                       </Rows>    
                                       <Rows>
                                            <telerik:LayoutRow ID="LayoutRow4"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                        <telerik:RadDropDownList RenderMode="Lightweight" ID="RddlCampus" runat="server" Width="100%" DropDownHeight="200px" >
                                                        </telerik:RadDropDownList>
                                                        <asp:RequiredFieldValidator runat="server" ID="RFVRddlCampus" InitialValue="Select a Campus"
                                                           ControlToValidate="RddlCampus" ValidationGroup="campuslogodept" Display="Dynamic"
                                                           ErrorMessage="Please select a Campus" ForeColor="red" />
                                                    </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows> 
                                        <Rows>
                                            <telerik:LayoutRow ID="LayoutRow5"    runat="server"   style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                        <telerik:RadAsyncUpload RenderMode="Lightweight" MultipleFileSelection="Disabled" runat="server" ID="FUCampusLogo"
                                                        AllowedFileExtensions="jpg,jpeg,png,gif" MaxFileInputsCount="1" />
                                                        <p style="font-size: 12px; color: black; font-style: initial; margin: 0px;">
                                                          <span style="color: red;">*</span> Must be upload jpg,jpeg,png,gif file. </p>
                                                    </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows>
                                     
                                         <Rows>
                                                <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px;margin-bottom:4px">
                                                    <Columns>
                                                         <telerik:LayoutColumn   Span="2"  SpanXs="5"  SpanMd="2"  SpanSm="2" >
                                                              <telerik:RadButton ID="RPbtnUpdate"   Width="100%" OnClick="RPbtnUpdate_Click"
                                                                   ValidationGroup="campuslogodept" runat="server" Text="Update">
                                                                 <Icon PrimaryIconCssClass="rbSave"></Icon>
                                                              </telerik:RadButton>
                                                         </telerik:LayoutColumn> 
                                                         <telerik:LayoutColumn   Span="1"  SpanXs="1"  SpanMd="1"  SpanSm="1" >
                                                         </telerik:LayoutColumn> 
                                                        <telerik:LayoutColumn   Span="3"  SpanXs="5"  SpanMd="3"  SpanSm="3"  >
                                                            <telerik:RadButton ID="RPbtnCancel"   Width="100%"   runat="server"
                                                                 OnClick="RPbtnCancel_Click"  Text="Cancel">
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