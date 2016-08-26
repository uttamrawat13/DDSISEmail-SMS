<%@ Page Title="User Role" Language="C#" MasterPageFile="~/MainDDMailSMSMaster.Master" AutoEventWireup="true" CodeBehind="frmaddrole.aspx.cs" Inherits="DDMailSmsWeb.frmaddrole" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ConCPhead" ContentPlaceHolderID="CPhead" runat="server">
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
</script>
</asp:Content>

<asp:Content ID="ConCPHDetail" ContentPlaceHolderID="CPHDetail" runat="server">


    <telerik:RadAjaxLoadingPanel ID="RALPfrmaddrole" runat="server" Height="75px"
        Width="75px" Transparency="50">
    </telerik:RadAjaxLoadingPanel>
      <telerik:RadAjaxManagerProxy ID="RAMfrmaddrole" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="Pfrmaddrole">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Pfrmaddrole" LoadingPanelID="RALPfrmaddrole"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

    <!-- Start Session check  -->
        <cc1:ModalPopupExtender ID="MPEsessionchk" runat="server" TargetControlID="lbtargetcontrol" BackgroundCssClass="ModalPopupBG"  PopupControlID="pnlConfirmation">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlConfirmation" runat="server" Style="display:none;position: fixed;z-index: 100001;top: 0px;width: 100%;height: 100%;left: 0px;background-color: black;background-color: black;color: white;text-align: center;">
                        <b>Session has been expired!</b> 
            <asp:Label ID="lbtargetcontrol" runat="server"></asp:Label>
                        <br/>
                        Please lanunch the application again..<br/><br />
                        <asp:Button ID="btngotologin" runat="server" Text="OK" style="color:black" onclick="btngotologin_Click" />
        </asp:Panel>
     <!-- End Session check  -->

   <asp:Panel runat="server" ID="Pfrmaddrole">

    
        <telerik:RadPageLayout runat="server" ID="RDPLayoutfrmemailconfiguration" GridType="Fluid">
              <Rows>
                    <telerik:LayoutRow ID="LayoutRow1"   style="margin-top:4px;margin-left:2px;margin-right:2px;margin-bottom:4px" runat="server"  >
                        <Columns>
                             <telerik:LayoutColumn   Span="7"   SpanXs="7"  SpanMd="7"  SpanSm="7">
                               <div style="border: 1px solid #e9e4e4;padding:10px;height:inherit;">
                                    
                                 

                               <telerik:RadGrid ID="RgvAddRole" runat="server" AutoGenerateColumns="False" Width="100%"  
                                   FilterMenu-RenderMode="Lightweight"
                                OnSortCommand="RgvAddRole_SortCommand"  GroupPanelPosition="Top" PageSize="30" Font-Size="10"
                                    OnItemCommand="RgvAddRole_ItemCommand" OnNeedDataSource="RgvAddRole_NeedDataSource"
                                AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="true">
                                <ItemStyle Wrap="true"></ItemStyle>
                                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>

                                <MasterTableView  AllowMultiColumnSorting="true">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Campus Name"  ShowSortIcon="true" AllowSorting="true" ItemStyle-VerticalAlign="Top" DataField="CampusName"   AllowFiltering="false">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbCampusName" runat="server" Text='<%# Convert.ToString(Eval("CampusName")) %>' />
                                                <asp:Label ID="lbCampusID" runat="server" Visible="false" Text='<%# Convert.ToString(Eval("CampusID")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn SortExpression="Role_Id" AllowFiltering="false" ShowSortIcon="false" AllowSorting="false" HeaderText="Role Code" HeaderButtonType="TextButton"
                                            DataField="Role_Id">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn SortExpression="Role" AllowFiltering="true" ShowSortIcon="true" AllowSorting="true" HeaderText="Role" HeaderButtonType="TextButton"
                                            DataField="Role">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Edit"  AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ViewRole" runat="server" AlternateText="Delete" ToolTip="Edit This Role"
                                                    Height="16px" Width="16px"
                                                    ImageUrl="~/images/pencil_edit_button.png" CommandName="ViewSMSTemp" CommandArgument='<%# Eval("Role_Id") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="Delete" Visible="false"  AllowFiltering="false">
                                            <ItemTemplate>  

                                                <asp:ImageButton ID="DeleteRole" runat="server" AlternateText="Delete" ToolTip="Delete This Role" Height="16px" Width="16px"
                                                    OnClientClick="javascript:return confirm('Are you SURE you want to remove this role?')"
                                                    ImageUrl="~/images/Delete.png" CommandName="DeleteSMSTemp" CommandArgument='<%# Eval("Role_Id") %>' />
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
                                                                                            <span style="font:bold;font-size:14px;color:#25a0da;">Role Setup</span>   
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
                                            <telerik:LayoutRow  ID="LRRRlblRoleResult" runat="server" Visible="false" style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                        <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                            <telerik:RadLabel ID="RlblRoleResult" runat="server" ForeColor="#cc0000"></telerik:RadLabel>
                                                        </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                       </Rows>    
                                       <Rows>
                                           <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px" >
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                            <telerik:RadDropDownList RenderMode="Lightweight" ID="Rddlcampus"  Width="100%" runat="server"
                                                                     Font-Size="12px" DropDownHeight="200px"
                                                                     AutoPostBack="false">
                                                             </telerik:RadDropDownList>
                                                             <asp:RequiredFieldValidator runat="server" ID="RFVRddlcampus" InitialValue="Select Campus"
                                                                    ControlToValidate="Rddlcampus" ValidationGroup="VGAddRole" Display="Dynamic" ForeColor="#ff0000"
                                                                    ErrorMessage="Please select campus" CssClass="validator" />
                                                     </telerik:LayoutColumn>
                                                </Columns>
                                            </telerik:LayoutRow>
                                       </Rows>
                                       <Rows>
                                            <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px" >
                                                <Columns>
                                                     <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                              
                                                            <asp:TextBox ID="RtxtRoleCode" runat="server" Style="height: 26px;padding: 9px;"  MaxLength="10"  onkeypress="return isNumber(event)" CssClass="riTextBox riEnabled riHover" Width="100%" placeholder="Role Code" >
                                                            </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFVtxtRoleCode" runat="server" Display="Dynamic" ValidationGroup="VGAddRole"
                                                            ControlToValidate="RtxtRoleCode" ErrorMessage="Please enter role code!" ForeColor="Red" />
                                                     </telerik:LayoutColumn>
                                                </Columns>
                                            </telerik:LayoutRow>
                                       </Rows>
                                       <Rows>
                                            <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px">
                                                <Columns>
                                                      <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                          <telerik:RadTextBox RenderMode="Lightweight" runat="server" placeholder="Role Name"  MaxLength="50" ID="txtRoleName"
                                                               Width="100%">
                                                            </telerik:RadTextBox>
                                                            <asp:RequiredFieldValidator ID="RFVtxtRoleName" runat="server" Display="Dynamic" ValidationGroup="VGAddRole"
                                                            ControlToValidate="txtRoleName" ErrorMessage="Please enter role name!" ForeColor="Red" />

                                                        </telerik:LayoutColumn> 
                                                </Columns>
                                            </telerik:LayoutRow>
                                        </Rows>
                                         <Rows>
                                                <telerik:LayoutRow style="margin-top:4px;margin-left:2px;margin-right:2px;margin-bottom:4px">
                                                    <Columns>
                                                         <telerik:LayoutColumn   Span="2"  SpanXs="5"  SpanMd="2"  SpanSm="2" >
                                                              <telerik:RadButton ID="RPbtnSave"   Width="100%" OnClick="RPbtnSave_Click"  ValidationGroup="VGAddRole" runat="server" Text="Save">
                                                                 <Icon PrimaryIconCssClass="rbSave"></Icon>
                                                             </telerik:RadButton>
                                                         </telerik:LayoutColumn> 
                                                         <telerik:LayoutColumn   Span="1"  SpanXs="1"  SpanMd="1"  SpanSm="1" >
                                                         </telerik:LayoutColumn> 
                                                        <telerik:LayoutColumn   Span="3"  SpanXs="5"  SpanMd="3"  SpanSm="3"  >
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

