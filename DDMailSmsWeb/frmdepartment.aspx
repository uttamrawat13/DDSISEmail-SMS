<%@ Page Title="Department" Language="C#" MasterPageFile="~/MainDDMailSMSMaster.Master"  CodeBehind="frmdepartment.aspx.cs" Inherits="DDMailSmsWeb.TDEPTInbox" %>
<asp:Content ID="ConCPhead" ContentPlaceHolderID="CPhead" runat="server">
</asp:Content>
<asp:Content ID="ConCPHDetail" ContentPlaceHolderID="CPHDetail" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RALPDEPTInbox" runat="server" Height="75px"
        Width="75px" Transparency="50">
    </telerik:RadAjaxLoadingPanel>
 
      
    
     <telerik:RadAjaxPanel ID="RAPfrmdepartment" runat="server" LoadingPanelID="RALPDEPTInbox"  ClientEvents-OnRequestStart="requestStart">
   
       <telerik:RadPageLayout runat="server" ID="RDPLayoutDEPTInbox" GridType="Fluid">
            <Rows>
                <telerik:LayoutRow >
                    <Columns>
                        <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                           <!-- INBOX GRIDVIEW ---->
                           <!-- START ---->
                             <telerik:RadGrid ID="rgDepartment" runat="server" AutoGenerateColumns="False" Width="100%" RenderMode="Auto" FilterMenu-RenderMode="Lightweight"
                                GroupPanelPosition="Top" IsExporting="False" PageSize="8" Font-Size="10" OnNeedDataSource="rgDepartment_NeedDataSource"
                                AllowFilteringByColumn="true" AllowPaging="True" AllowSorting="True"  OnItemCommand="rgDepartment_ItemCommand"
                                 OnSortCommand="rgDepartment_SortCommand"
                                OnItemDataBound="rgDepartment_ItemDataBound" OnPageIndexChanged="rgDepartment_PageIndexChanged">
                                <ItemStyle Wrap="true"></ItemStyle>
                                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                <MasterTableView AllowMultiColumnSorting="true">
                                    <Columns>
                                        <telerik:GridTemplateColumn Visible="false">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="ItemLblInboxID" UniqueName="ItemLblInboxID" runat="server" Text='<%# Convert.ToString(Eval("InboxID")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Received From" SortExpression="EmailReceived" DataField="EmailReceived" AllowSorting="true" ShowSortIcon="true" ItemStyle-VerticalAlign="Top"
                                             AllowFiltering="true">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="EmailReceived" runat="server"  Text='<%# Convert.ToString(Eval("EmailReceived")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Subject"  SortExpression="EmailSubject" DataField="EmailSubject" AllowSorting="true" ShowSortIcon="true" ItemStyle-VerticalAlign="Top" AllowFiltering="true">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="EmailSubject" runat="server" Text='<%# Convert.ToString(Eval("EmailSubject")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Received" ItemStyle-VerticalAlign="Top" AllowFiltering="false" >
                                            <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="EmailReceivedDatetime" runat="server" Text='<%# Convert.ToString(Eval("EmailReceivedDatetime")) %>' />
                                                <asp:Label ID="lbdeptEmailBody" runat="server" Visible="false" Text='<%# Convert.ToString(Eval("EmailBody")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Attachment" ItemStyle-VerticalAlign="Top" AllowFiltering="false">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="Attachment" runat="server" Text='<%# Convert.ToString(Eval("EmailAttachmentStatus")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridCheckBoxColumn DataField="EmailIsRead" Visible="false" HeaderText="Unread" DataType="System.Boolean" UniqueName="EmailIsRead">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            <HeaderStyle Width="100px" />
                                        </telerik:GridCheckBoxColumn>
                                        <telerik:GridTemplateColumn HeaderText="Unread" ItemStyle-VerticalAlign="Top" AllowFiltering="false">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ItemChkboxEmailIsRead" runat="server" Checked='<%# Convert.ToBoolean(Eval("EmailIsRead")) %>'
                                                      AutoPostBack="True" OnCheckedChanged="ItemChkboxEmailIsRead_CheckedChanged"  />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Reply" FilterControlWidth="30px" AllowFiltering="false">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnreplystd" CommandName="imgbtnreplystd" runat="server" AlternateText="Reply" ToolTip="Reply This Email" Height="16px" Width="16px"
                                                    ImageUrl="~/images/reply_arrow.png" CommandArgument='<%# Eval("InboxID") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Delete" FilterControlWidth="30px" AllowFiltering="false">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="DeleteInboxDeptemail" runat="server" AlternateText="Delete" ToolTip="Delete This Email" Height="16px" Width="16px"
                                                    OnClientClick="javascript:return confirm('Are You Sure Delete This Email?')"
                                                    ImageUrl="~/images/Delete.png" CommandName="DeleteInboxDeptemail" CommandArgument='<%# Eval("InboxID") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Inbox"  AllowFiltering="false">
                                            <HeaderStyle CssClass="mobileGridItem" />
                                                <ItemStyle CssClass="mobileGridItem" />
                                            <ItemTemplate>
                                                <telerik:RadPageLayout ID="RPLayoutgridDetail" runat="server" GridType="Fluid">
                                                    <Rows>
                                                        <telerik:LayoutRow >
                                                            <Columns>
                                                                <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                                     <strong><%# Eval("EmailReceived") %></strong>
                                                                 </telerik:LayoutColumn>
                                                             </Columns>     
                                                        </telerik:LayoutRow>
                                                    </Rows>
                                                    <Rows>
                                                        <telerik:LayoutRow >
                                                            <Columns>
                                                                <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                                       <%# Eval("EmailSubject") %>
                                                                 </telerik:LayoutColumn>
                                                             </Columns>     
                                                        </telerik:LayoutRow>
                                                    </Rows>
                                                    <Rows>
                                                        <telerik:LayoutRow >
                                                            <Columns>
                                                                <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                                       <%# Eval("EmailReceivedDatetime") %>
                                                                 </telerik:LayoutColumn>
                                                             </Columns>     
                                                        </telerik:LayoutRow>
                                                    </Rows>
                                                </telerik:RadPageLayout>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <NestedViewTemplate>
                                        <asp:HiddenField ID="hdnInboxID" runat="server" Value='<%# Eval("InboxID") %>' />
                                        <div style="border:solid 1px;padding:2px;margin:2px">
                                           <telerik:RadPageLayout ID="RPLayoutgridDetail" runat="server" GridType="Fluid">
                                                    <Rows>
                                                        <telerik:LayoutRow >
                                                            <Columns>
                                                                <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                                      <strong>Received Email:</strong>
                                                                      <%# Eval("EmailReceived") %>
                                                                 </telerik:LayoutColumn>
                                                             </Columns>     
                                                        </telerik:LayoutRow>
                                                    </Rows>
                                                    <Rows>
                                                        <telerik:LayoutRow >
                                                            <Columns>
                                                                <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                                       <strong>Attachment:</strong>
                                                                       <asp:LinkButton ID="Deptdownload_fileDept" runat="server" CommandArgument='<%# Eval("EmailAttachment") %>' CommandName="Deptdownload_file"><%# Eval("EmailAttachment") %></asp:LinkButton>
                                                                 </telerik:LayoutColumn>
                                                             </Columns>     
                                                        </telerik:LayoutRow>
                                                    </Rows>
                                                    <Rows>
                                                        <telerik:LayoutRow >
                                                            <Columns>
                                                                <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                                      <strong>Subject:</strong>
                                                                       <%# Eval("EmailSubject") %>
                                                                 </telerik:LayoutColumn>
                                                             </Columns>     
                                                        </telerik:LayoutRow>
                                                    </Rows>
                                                    <Rows>
                                                        <telerik:LayoutRow >
                                                            <Columns>
                                                                <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                                    <strong>Email Body</strong>
                                                                    <asp:Panel ID="pnlview" ScrollBars="Vertical" runat="server" Height="175px">
                                                                        <telerik:RadLabel runat="server" ID="CntntRadEditor" Width="100%" Style="z-index: 1000;" Text='<%#Eval("EmailBody") %>'>
                                                                        </telerik:RadLabel>
                                                                    </asp:Panel>
                                                                 </telerik:LayoutColumn>
                                                             </Columns>     
                                                        </telerik:LayoutRow>
                                                    </Rows>
                                                    
                                                </telerik:RadPageLayout>
                                        </div>
                                   </NestedViewTemplate>
                                 <PagerStyle AlwaysVisible="True"></PagerStyle>
                                </MasterTableView>
                                <PagerStyle AlwaysVisible="true" Mode="NumericPages"></PagerStyle>
                                <FilterMenu RenderMode="Lightweight"></FilterMenu>
                                <HeaderContextMenu RenderMode="Lightweight"></HeaderContextMenu>
                                 
                            </telerik:RadGrid>
                           <!---END--->
                           <!-- SENT GRIDVIEW ---->
                           <!-- START ---->
                              <telerik:RadGrid ID="rgDepartmentSent" runat="server" AutoGenerateColumns="False" Width="100%" RenderMode="Auto" FilterMenu-RenderMode="Lightweight"
                                    GroupPanelPosition="Top" IsExporting="False" PageSize="8" Font-Size="10" OnNeedDataSource="rgDepartmentSent_NeedDataSource" OnItemCommand="rgDepartmentSent_ItemCommand"
                                    OnSortCommand="rgDepartmentSent_SortCommand" AllowFilteringByColumn="true" AllowPaging="True" AllowSorting="True" OnPageIndexChanged="rgDepartmentSent_PageIndexChanged">
                                    <ItemStyle Wrap="true"></ItemStyle>
                                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                    <MasterTableView AllowMultiColumnSorting="true">

                                        <Columns>
                                            <telerik:GridBoundColumn DataField="EmailsentFrom" HeaderText="Email From" ItemStyle-VerticalAlign="Top" SortExpression="EmailsentFrom"
                                                AllowSorting="true" ShowSortIcon="true"
                                                 AllowFiltering="true"    UniqueName="EmailReceived">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmailSentTo" HeaderText="Sent To" ItemStyle-VerticalAlign="Top"
                                                 AllowSorting="true" ShowSortIcon="true"  SortExpression="EmailSentTo"
                                                  AllowFiltering="true"  UniqueName="EmailSubject">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmailSentSubject" HeaderText="Subject" ItemStyle-VerticalAlign="Top" 
                                                 AllowSorting="true" ShowSortIcon="true" SortExpression="EmailSentSubject"
                                                 AllowFiltering="true"  UniqueName="Attachment">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Attachment" ItemStyle-VerticalAlign="Top" AllowFiltering="false">
                                                <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="Attachment" runat="server" Text='<%# Convert.ToString(Eval("EmailAttachmentStatus")) %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="EmailSentDatetime" HeaderText="Date" AllowSorting="false" ItemStyle-VerticalAlign="Top" AllowFiltering="false"   UniqueName="EmailReceivedDatetime">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>
                                              <telerik:GridTemplateColumn  HeaderText="Sent Email"  AllowFiltering="false">
                                                <HeaderStyle CssClass="mobileGridItem" />
                                                <ItemStyle CssClass="mobileGridItem" />
                                                <ItemTemplate>
                                                    <telerik:RadPageLayout ID="RPLayoutgridMobile" runat="server" GridType="Fluid">
                                                        <Rows>
                                                            <telerik:LayoutRow >
                                                                <Columns>
                                                                    <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                                         <strong>Email From:</strong>
                                                                        <p>
                                                                            <%# Eval("EmailsentFrom") %>
                                                                        </p>
                                                                     </telerik:LayoutColumn>
                                                                 </Columns>     
                                                            </telerik:LayoutRow>
                                                        </Rows>
                                                        <Rows>
                                                            <telerik:LayoutRow >
                                                                <Columns>
                                                                    <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                                        <strong>Sent To:</strong>
                                                                        <p>
                                                                            <%# Eval("EmailSentTo") %>
                                                                        </p>
                                                                     </telerik:LayoutColumn>
                                                                 </Columns>     
                                                            </telerik:LayoutRow>
                                                        </Rows>
                                                        <Rows>
                                                            <telerik:LayoutRow >
                                                                <Columns>
                                                                    <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                                           <strong>Subject:</strong>
                                                                        <p>
                                                                            <%# Eval("EmailSentSubject") %>
                                                                        </p>
                                                                     </telerik:LayoutColumn>
                                                                 </Columns>     
                                                            </telerik:LayoutRow>
                                                        </Rows>
                                                        <Rows>
                                                            <telerik:LayoutRow >
                                                                <Columns>
                                                                    <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                                           <strong>Date:</strong>
                                                                        <p>
                                                                            <%# Eval("EmailSentDatetime") %>
                                                                        </p>
                                                                     </telerik:LayoutColumn>
                                                                 </Columns>     
                                                            </telerik:LayoutRow>
                                                        </Rows>
                                                    </telerik:RadPageLayout>
                                                </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                         </Columns>
                                        <NestedViewTemplate>
                                            <asp:HiddenField ID="hdnInboxID" runat="server" Value='<%# Eval("SentID") %>' />
                                            <div style="border: solid 1px; padding: 2px; margin: 2px">
                                                <telerik:RadPageLayout ID="RPLayoutgridDetail" runat="server" GridType="Fluid">
                                                    <Rows>
                                                        <telerik:LayoutRow>
                                                            <Columns>
                                                                <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
                                                                    <strong>Sent from:</strong>
                                                                    <%# Eval("EmailSentFrom") %>
                                                                </telerik:LayoutColumn>
                                                            </Columns>
                                                        </telerik:LayoutRow>
                                                    </Rows>
                                                    
                                                    <Rows>
                                                        <telerik:LayoutRow>
                                                            <Columns>
                                                                <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
                                                                    <strong>Subject:</strong>
                                                                    <%# Eval("EmailSentSubject") %>
                                                                </telerik:LayoutColumn>
                                                            </Columns>
                                                        </telerik:LayoutRow>
                                                    </Rows>
                                                    <Rows>
                                                        <telerik:LayoutRow>
                                                            <Columns>
                                                                <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
                                                                    <strong>Attachment:</strong>
                                                                    <asp:LinkButton ID="download_filesent" runat="server" CommandArgument='<%# Eval("EmailAttachment") %>' CommandName="download_fileInbox"><%# Eval("EmailAttachment") %></asp:LinkButton>
                                                                </telerik:LayoutColumn>
                                                            </Columns>
                                                        </telerik:LayoutRow>
                                                    </Rows>
                                                    <Rows>
                                                        <telerik:LayoutRow>
                                                            <Columns>
                                                                <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
                                                                    <strong> Body</strong>
                                                                    <asp:Panel ID="pnlview" ScrollBars="Vertical" runat="server" Height="175px">
                                                                        <telerik:RadLabel runat="server" ID="CntntRadEditor" Width="100%" Style="z-index: 1000;" Text='<%#Eval("EmailSentBody") %>'>
                                                                        </telerik:RadLabel>
                                                                    </asp:Panel>
                                                                </telerik:LayoutColumn>
                                                            </Columns>
                                                        </telerik:LayoutRow>
                                                    </Rows>

                                                </telerik:RadPageLayout>
                                            </div>
                                        </NestedViewTemplate>
                                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                                    </MasterTableView>
                                    <PagerStyle AlwaysVisible="true" Mode="NumericPages"></PagerStyle>
                                    <FilterMenu RenderMode="Lightweight"></FilterMenu>
                                    <HeaderContextMenu RenderMode="Lightweight"></HeaderContextMenu>
                                   
                                </telerik:RadGrid>
                            <!---END--->
                            <!-- DELETE GRIDVIEW ---->
                            <!-- START ---->
                               <telerik:RadGrid ID="rgDepartmentDelete" runat="server" AutoGenerateColumns="False" Width="100%" RenderMode="Auto" FilterMenu-RenderMode="Lightweight"
                                    OnSortCommand="rgDepartmentDelete_SortCommand"  GroupPanelPosition="Top" IsExporting="False" PageSize="8" Font-Size="10" OnNeedDataSource="rgDepartmentDelete_NeedDataSource"
                                    AllowFilteringByColumn="true" AllowPaging="True" AllowSorting="True" OnPageIndexChanged="rgDepartmentDelete_PageIndexChanged">
                                    <ItemStyle Wrap="true"></ItemStyle>
                                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                    <MasterTableView AllowMultiColumnSorting="true">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="EmailReceived" HeaderText="Received From" UniqueName="EmailReceived"
                                                 AllowSorting="false" ShowSortIcon="true" SortExpression="EmailReceived"
                                                 AllowFiltering="true"  FilterControlWidth="90%">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                             <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmailSubject" HeaderText="Subject" AllowFiltering="true" 
                                                 AllowSorting="true" ShowSortIcon="true" SortExpression="EmailSubject"
                                                UniqueName="EmailSubject"  FilterControlWidth="90%">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                             <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmailAttachment" HeaderText="Attachment" UniqueName="Attachment"
                                                 AllowSorting="true" ShowSortIcon="true" SortExpression="EmailAttachment"
                                                 AllowFiltering="true"  FilterControlWidth="90%">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                             <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmailReceivedDatetime" HeaderText="ReceivedDatetime"
                                                 UniqueName="EmailReceivedDatetime" AllowFiltering="false" AllowSorting="false"  FilterControlWidth="90%">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>

                                            <telerik:GridTemplateColumn  HeaderText="Delete Email"  AllowFiltering="false">
                                                <HeaderStyle CssClass="mobileGridItem" />
                                                <ItemStyle CssClass="mobileGridItem" />
                                                <ItemTemplate>
                                                    <telerik:RadPageLayout ID="RPLayoutgridMobile" runat="server" GridType="Fluid">
                                                        <Rows>
                                                            <telerik:LayoutRow >
                                                                <Columns>
                                                                    <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                                         <strong>Received From:</strong>
                                                                        <p>
                                                                            <%# Eval("EmailReceived") %>
                                                                        </p>
                                                                     </telerik:LayoutColumn>
                                                                 </Columns>     
                                                            </telerik:LayoutRow>
                                                        </Rows>
                                                        <Rows>
                                                            <telerik:LayoutRow >
                                                                <Columns>
                                                                    <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                                        <strong>Subject:</strong>
                                                                        <p>
                                                                            <%# Eval("EmailSubject") %>
                                                                        </p>
                                                                     </telerik:LayoutColumn>
                                                                 </Columns>     
                                                            </telerik:LayoutRow>
                                                        </Rows>
                                                        <Rows>
                                                            <telerik:LayoutRow >
                                                                <Columns>
                                                                    <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                                           <strong>Datetime:</strong>
                                                                        <p>
                                                                            <%# Eval("EmailReceivedDatetime") %>
                                                                        </p>
                                                                     </telerik:LayoutColumn>
                                                                 </Columns>     
                                                            </telerik:LayoutRow>
                                                        </Rows>
                                                    </telerik:RadPageLayout>
                                                </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        </Columns>
                                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                                    </MasterTableView>
                                    <PagerStyle AlwaysVisible="true" Mode="NumericPages"></PagerStyle>
                                    <FilterMenu RenderMode="Lightweight"></FilterMenu>
                                    <HeaderContextMenu RenderMode="Lightweight"></HeaderContextMenu>
                                </telerik:RadGrid>
                            <!---END--->
                            <!-- SMS Received GRIDVIEW ---->
                            <!-- START ---->
                               <telerik:RadGrid ID="rgvDepartmentReceived"  runat="server" AutoGenerateColumns="False" Width="100%" RenderMode="Auto" FilterMenu-RenderMode="Lightweight"
                                GroupPanelPosition="Top" IsExporting="False" OnSortCommand="rgvDepartmentReceived_SortCommand" PageSize="8" Font-Size="10" OnNeedDataSource="rgvDepartmentReceived_NeedDataSource"
                                AllowFilteringByColumn="true" AllowPaging="True" AllowSorting="True" OnPageIndexChanged="rgvDepartmentReceived_PageIndexChanged"
                                   OnItemDataBound="rgvDepartmentReceived_ItemDataBound">
                                <ItemStyle Wrap="true"></ItemStyle>
                                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="From" HeaderText="From" UniqueName="SMSFrom" AllowFiltering="false"
                                             ItemStyle-VerticalAlign="Top" FilterControlWidth="90%">
                                          <HeaderStyle CssClass="desktopgridItem" />
                                          <ItemStyle CssClass="desktopgridItem" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="To" HeaderText="To" UniqueName="SMSTo" AllowFiltering="true"
                                            AllowSorting="true" ShowSortIcon="true" SortExpression="To"
                                             ItemStyle-VerticalAlign="Top" >
                                          <HeaderStyle CssClass="desktopgridItem" />
                                          <ItemStyle CssClass="desktopgridItem" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="Body" HeaderText="Body" AllowFiltering="true" ItemStyle-VerticalAlign="Top"
                                            AllowSorting="true" ShowSortIcon="true" SortExpression="Body">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblBody" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Body") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lbEmailSMSId" runat="server" Visible="false" Text='<%# Convert.ToString(Eval("EmailSMSId")) %>' />
                                             
                                                <telerik:RadToolTip SkinID="Metro" RenderMode="Lightweight" ID="RadToolTip1" runat="server" TargetControlID="lblBody" Width="500px"
                                                    RelativeTo="Element" Position="MiddleRight">
                                                    <%# DataBinder.Eval(Container, "DataItem.Body") %>
                                                </telerik:RadToolTip>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="Status" AllowSorting="true" UniqueName="lbStatus"  SortExpression="Status"  HeaderText="Status" AllowFiltering="true"
                                             ItemStyle-VerticalAlign="Top">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                        </telerik:GridBoundColumn>
                                          <telerik:GridTemplateColumn DataField="Direction" HeaderText="Direction" AllowFiltering="true" SortExpression="Direction"
                                               AllowSorting="true"
                                                 ItemStyle-VerticalAlign="Top">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                                <ItemTemplate>
                                                    <asp:Image ID="imgdriection" runat="server"  />
                                                    <asp:Label ID="lbldirection"  runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Direction") %>'>
                                                    </asp:Label>
                                                    <telerik:RadToolTip ID="RadToolTip2" SkinID="Metro" RenderMode="Lightweight"  runat="server" TargetControlID="imgdriection"
                                                        
                                                        RelativeTo="Element" Position="MiddleRight">
                                                        <%# DataBinder.Eval(Container, "DataItem.Direction") %>
                                                    </telerik:RadToolTip>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="Direction"  AllowSorting="true" Visible="false"  SortExpression="Direction"   HeaderText="Direction" AllowFiltering="true" 
                                            ItemStyle-VerticalAlign="Top" >
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="date_sent"  AllowSorting="false"  HeaderText="Date Sent" AllowFiltering="false" ItemStyle-VerticalAlign="Top" FilterControlWidth="90%">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                        </telerik:GridBoundColumn>
                                          <telerik:GridTemplateColumn HeaderText="Unread" ItemStyle-VerticalAlign="Top" AllowFiltering="false">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ItemChkboxSMSIsRead" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsRead")) %>'
                                                        AutoPostBack="True" OnCheckedChanged="ItemChkboxSMSIsRead_CheckedChanged" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                          
                                        <telerik:GridBoundColumn DataField="ErrorMessage" Visible="false"  AllowSorting="false"  HeaderText="Error Message" AllowFiltering="false" ItemStyle-VerticalAlign="Top" FilterControlWidth="90%">
                                            <HeaderStyle CssClass="desktopgridItem" />
                                            <ItemStyle CssClass="desktopgridItem" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn  HeaderText="Received SMS" AllowFiltering="false">
                                                <HeaderStyle CssClass="mobileGridItem" />
                                                <ItemStyle CssClass="mobileGridItem" />
                                                <ItemTemplate>
                                                    <telerik:RadPageLayout ID="RPLayoutgridMobile" runat="server" GridType="Fluid">
                                                        <Rows>
                                                            <telerik:LayoutRow >
                                                                <Columns>
                                                                    <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                                         <strong>From:</strong>
                                                                        <p>
                                                                            <%# Eval("From") %>
                                                                        </p>
                                                                     </telerik:LayoutColumn>
                                                                 </Columns>     
                                                            </telerik:LayoutRow>
                                                        </Rows>
                                                        <Rows>
                                                            <telerik:LayoutRow >
                                                                <Columns>
                                                                    <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                                        <strong>To:</strong>
                                                                        <p>
                                                                            <%# Eval("To") %>
                                                                        </p>
                                                                     </telerik:LayoutColumn>
                                                                 </Columns>     
                                                            </telerik:LayoutRow>
                                                        </Rows>
                                                        <Rows>
                                                            <telerik:LayoutRow >
                                                                <Columns>
                                                                    <telerik:LayoutColumn   Span="12"  SpanXs="12" SpanSm="12" >
                                                                           <strong>Body:</strong>
                                                                        <p>
                                                                           <asp:Label ID="lblBodyMobile" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Body") %>'>
                                                                            </asp:Label>
                                                                           <telerik:RadToolTip ID="RadToolTip3" SkinID="Metro" RenderMode="Lightweight"   runat="server" TargetControlID="lblBodyMobile" Width="500px"
                                                                            RelativeTo="Element" Position="MiddleRight">
                                                                            <%# DataBinder.Eval(Container, "DataItem.Body") %>
                                                                            </telerik:RadToolTip>
                                                                        </p>
                                                                     </telerik:LayoutColumn>
                                                                 </Columns>     
                                                            </telerik:LayoutRow>
                                                        </Rows>

                                                    </telerik:RadPageLayout>
                                                </ItemTemplate>
                                        </telerik:GridTemplateColumn> 
                                    
                                    </Columns>
                                    <PagerStyle AlwaysVisible="True"></PagerStyle>
                                </MasterTableView>
                                <PagerStyle AlwaysVisible="true" Mode="NumericPages"></PagerStyle>
                                <FilterMenu RenderMode="Lightweight"></FilterMenu>
                                <HeaderContextMenu RenderMode="Lightweight"></HeaderContextMenu>
                            </telerik:RadGrid>
                            <!-- END ---->
                            <!-- SMS Sent GRIDVIEW --->
                         
                    
                        </telerik:LayoutColumn> 
                </Columns>
            </telerik:LayoutRow>
                 </Rows>
       </telerik:RadPageLayout>
    </telerik:RadAjaxPanel>
     <telerik:RadCodeBlock ID="RCBAddTemplate" runat="server">
        <script type="text/javascript">
            (function ($) {

                requestStart = function (target, arguments) {
                    if (arguments.get_eventTarget().indexOf("imgbtnreplystd") > 0) {
                        arguments.set_enableAjax(false);
                    }
                    if (arguments.get_eventTarget().indexOf("Deptdownload_fileDept") > 0) {
                        arguments.set_enableAjax(false);
                    }
                    if (arguments.get_eventTarget().indexOf("ItemChkboxEmailIsRead") > 0) {
                        arguments.set_enableAjax(false);
                    }
                    if (arguments.get_eventTarget().indexOf("DeleteInboxDeptemail") > 0) {
                        arguments.set_enableAjax(false);
                    }
                    if (arguments.get_eventTarget().indexOf("download_filesent") > 0) {
                        arguments.set_enableAjax(false);
                    }

                    if (arguments.get_eventTarget().indexOf("ItemChkboxSMSIsRead") > 0) {
                        arguments.set_enableAjax(false);
                    }
                }
            })($telerik.$);
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
