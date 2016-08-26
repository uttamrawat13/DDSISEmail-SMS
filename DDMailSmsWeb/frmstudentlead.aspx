<%@ Page Title="Student" Language="C#" MasterPageFile="~/MainDDMailSMSMaster.Master" AutoEventWireup="true" CodeBehind="frmstudentlead.aspx.cs" Inherits="DDMailSmsWeb.TSTDLEADInbox" %>

<asp:Content ID="ConCPhead" ContentPlaceHolderID="CPhead" runat="server">
</asp:Content>

<asp:Content ID="ConCPHDetai" ContentPlaceHolderID="CPHDetail" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RALPSTDLEADInbox" runat="server" Height="75px"
        Width="75px" Transparency="50">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RAPfrmstudentlead" runat="server" LoadingPanelID="RALPSTDLEADInbox"  ClientEvents-OnRequestStart="requestStart">
        <asp:Panel runat="server" ID="PSTDLEADInbox">
            <telerik:RadPageLayout runat="server" ID="RDPLayoutLEADInbox" GridType="Fluid">
                <Rows>
                    <telerik:LayoutRow>
                        <Columns>
                            <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
                                <!-- INBOX GRIDVIEW ---->
                                <!-- START ---->
                                <telerik:RadGrid ID="RgvInbox" runat="server" AutoGenerateColumns="False" Width="100%" RenderMode="Auto" FilterMenu-RenderMode="Lightweight"
                                    GroupPanelPosition="Top" IsExporting="False" PageSize="8" Font-Size="10" OnSortCommand="RgvInbox_SortCommand"
                                    AllowFilteringByColumn="true" AllowPaging="True" AllowSorting="True" OnNeedDataSource="RgvInbox_NeedDataSource"
                                    OnItemDataBound="RgvInbox_ItemDataBound" OnPageIndexChanged="RgvInbox_PageIndexChanged" OnItemCommand="RgvInbox_ItemCommand">
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
                                            <telerik:GridTemplateColumn HeaderText="Received From" SortExpression="EmailReceived" ItemStyle-VerticalAlign="Top"
                                                DataField="EmailReceived" FilterControlWidth="85%" AllowFiltering="true">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                                <ItemTemplate>
                                                    <asp:Label ID="EmailReceived" runat="server" Text='<%# Convert.ToString(Eval("EmailReceived")) %>' />
                                                    <asp:Label ID="lblstdEmailBody" runat="server" Visible="false" Text='<%# Convert.ToString(Eval("EmailBody")) %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Subject" SortExpression="EmailSubject" FilterControlWidth="85%"
                                                DataField="EmailSubject" AllowFiltering="true">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                                <ItemTemplate>
                                                    <asp:Label ID="EmailSubject" runat="server" Text='<%# Convert.ToString(Eval("EmailSubject")) %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Received" ItemStyle-VerticalAlign="Top" AllowSorting="true"
                                                ShowSortIcon="true" AllowFiltering="false">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                                <ItemTemplate>
                                                    <asp:Label ID="EmailReceivedDatetime" runat="server" Text='<%# Convert.ToString(Eval("EmailReceivedDatetime")) %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Attachment" ItemStyle-VerticalAlign="Top" AllowFiltering="false">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                                <ItemTemplate>
                                                    <asp:Label ID="Attachment" runat="server" Text='<%# Convert.ToString(Eval("EmailAttachmentStatus")) %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Unread" ItemStyle-VerticalAlign="Top" AllowFiltering="false">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ItemChkboxEmailIsRead" runat="server" Checked='<%# Convert.ToBoolean(Eval("EmailIsRead")) %>'
                                                        AutoPostBack="True" OnCheckedChanged="ItemChkboxEmailIsRead_CheckedChanged" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Reply" AllowFiltering="false">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnreplystd" runat="server" AlternateText="Reply" ToolTip="Reply This Email" Height="16px" Width="16px"
                                                        ImageUrl="~/images/reply_arrow.png" CommandName="imgbtnreplystd" CommandArgument='<%# Eval("InboxID") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Delete" AllowFiltering="false">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="DeleteInboxstdleademail" runat="server" AlternateText="Delete" ToolTip="Delete This Email" Height="16px" Width="16px"
                                                        OnClientClick="javascript:return confirm('Are You Sure Delete This Email?')"
                                                        ImageUrl="~/images/Delete.png" CommandName="DeleteInboxstdleademail" CommandArgument='<%# Eval("InboxID") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Inbox">
                                                <HeaderStyle CssClass="mobileGridItem" />
                                                <ItemStyle CssClass="mobileGridItem" />
                                                <ItemTemplate>
                                                    <telerik:RadPageLayout ID="RPLayoutgridMobile" runat="server" GridType="Fluid">
                                                        <Rows>
                                                            <telerik:LayoutRow>
                                                                <Columns>
                                                                    <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
                                                                        <strong><%# Eval("EmailReceived") %></strong>
                                                                    </telerik:LayoutColumn>
                                                                </Columns>
                                                            </telerik:LayoutRow>
                                                        </Rows>
                                                        <Rows>
                                                            <telerik:LayoutRow>
                                                                <Columns>
                                                                    <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
                                                                        <%# Eval("EmailSubject") %>
                                                                    </telerik:LayoutColumn>
                                                                </Columns>
                                                            </telerik:LayoutRow>
                                                        </Rows>
                                                        <Rows>
                                                            <telerik:LayoutRow>
                                                                <Columns>
                                                                    <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
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
                                            <div style="border: solid 1px; padding: 2px; margin: 2px">
                                                <telerik:RadPageLayout ID="RPLayoutgridDetail" runat="server" GridType="Fluid">
                                                    <Rows>
                                                        <telerik:LayoutRow>
                                                            <Columns>
                                                                <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
                                                                    <strong>Received Email:</strong>
                                                                    <%# Eval("EmailReceived") %>
                                                                </telerik:LayoutColumn>
                                                            </Columns>
                                                        </telerik:LayoutRow>
                                                    </Rows>
                                                    <Rows>
                                                        <telerik:LayoutRow>
                                                            <Columns>
                                                                <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
                                                                    <strong>Attachment:</strong>
                                                                    <asp:LinkButton ID="download_fileInbox" runat="server" CommandArgument='<%# Eval("EmailAttachment") %>' CommandName="download_fileInbox"><%# Eval("EmailAttachment") %></asp:LinkButton>
                                                                </telerik:LayoutColumn>
                                                            </Columns>
                                                        </telerik:LayoutRow>
                                                    </Rows>
                                                    <Rows>
                                                        <telerik:LayoutRow>
                                                            <Columns>
                                                                <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
                                                                    <strong>Subject:</strong>
                                                                    <%# Eval("EmailSubject") %>
                                                                </telerik:LayoutColumn>
                                                            </Columns>
                                                        </telerik:LayoutRow>
                                                    </Rows>
                                                    <Rows>
                                                        <telerik:LayoutRow>
                                                            <Columns>
                                                                <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
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

                                

                                <telerik:RadGrid ID="RgvSent" runat="server" AutoGenerateColumns="False" Width="100%" RenderMode="Auto" FilterMenu-RenderMode="Lightweight"
                                    GroupPanelPosition="Top" IsExporting="False" PageSize="8" Font-Size="10" OnNeedDataSource="RgvSent_NeedDataSource"
                                     OnItemCommand="RgvSent_ItemCommand"
                                    AllowFilteringByColumn="True" OnSortCommand="RgvSent_SortCommand" AllowPaging="True" AllowSorting="True"
                                     OnPageIndexChanged="RgvSent_PageIndexChanged">
                                    <ItemStyle Wrap="true"></ItemStyle>
                                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                    <MasterTableView AllowMultiColumnSorting="true">
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText="Email From" ItemStyle-VerticalAlign="Top" 
                                                DataField="EmailsentFrom" SortExpression="EmailsentFrom" AllowFiltering="true" UniqueName="EmailsentFrom">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmailSentTo" HeaderText="Sent To" ItemStyle-VerticalAlign="Top"
                                                AllowFiltering="true" SortExpression="EmailSentTo" UniqueName="EmailSentTo">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmailSentSubject" HeaderText="Subject" ItemStyle-VerticalAlign="Top"
                                                AllowFiltering="true" SortExpression="EmailSentSubject" UniqueName="Subject">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Attachment" ItemStyle-VerticalAlign="Top" AllowFiltering="false">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                                <ItemTemplate>
                                                    <asp:Label ID="Attachment" runat="server" Text='<%# Convert.ToString(Eval("EmailAttachmentStatus")) %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Date" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" AllowFiltering="false">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                                <ItemTemplate>
                                                    <asp:Label ID="EmailSentDatetime" runat="server" Text='<%# Convert.ToString(Eval("EmailSentDatetime")) %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                      
                                            <telerik:GridTemplateColumn HeaderText="Sent Email" AllowFiltering="false">
                                                <HeaderStyle CssClass="mobileGridItem" />
                                                <ItemStyle CssClass="mobileGridItem" />
                                                <ItemTemplate>
                                                    <telerik:RadPageLayout ID="RPLayoutgridMobile" runat="server" GridType="Fluid">
                                                        <Rows>
                                                            <telerik:LayoutRow>
                                                                <Columns>

                                                                    <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
                                                                        <strong>Email From:</strong>
                                                                        <p>
                                                                            <%# Eval("EmailsentFrom") %>
                                                                        </p>
                                                                    </telerik:LayoutColumn>
                                                                </Columns>
                                                            </telerik:LayoutRow>
                                                        </Rows>
                                                        <Rows>
                                                            <telerik:LayoutRow>
                                                                <Columns>
                                                                    <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
                                                                        <strong>Sent To:</strong>
                                                                        <p>
                                                                            <%# Eval("EmailSentTo") %>
                                                                        </p>
                                                                    </telerik:LayoutColumn>
                                                                </Columns>
                                                            </telerik:LayoutRow>
                                                        </Rows>
                                                        <Rows>
                                                            <telerik:LayoutRow>
                                                                <Columns>
                                                                    <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
                                                                        <strong>Subject:</strong>
                                                                        <p>
                                                                            <%# Eval("EmailSentSubject") %>
                                                                        </p>
                                                                    </telerik:LayoutColumn>
                                                                </Columns>
                                                            </telerik:LayoutRow>
                                                        </Rows>
                                                        <Rows>
                                                            <telerik:LayoutRow>
                                                                <Columns>
                                                                    <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
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
                                <telerik:RadGrid ID="RgvDelete" runat="server" AutoGenerateColumns="False" Width="100%" RenderMode="Auto" FilterMenu-RenderMode="Lightweight"
                                    GroupPanelPosition="Top" IsExporting="False" PageSize="8" Font-Size="10" OnNeedDataSource="RgvDelete_NeedDataSource"
                                    OnSortCommand="RgvDelete_SortCommand"
                                    AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" OnPageIndexChanged="RgvDelete_PageIndexChanged">
                                    <ItemStyle Wrap="true"></ItemStyle>
                                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                    <MasterTableView AllowMultiColumnSorting="true">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="EmailReceived" HeaderText="Received From" UniqueName="EmailReceived" SortExpression="EmailReceived"
                                                AllowFiltering="true"  ItemStyle-Width="250px">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmailSubject" HeaderText="Subject" UniqueName="EmailSubject" SortExpression="EmailSubject"
                                                AllowFiltering="true" >
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmailAttachment" HeaderText="Attachment" UniqueName="Attachment" AllowSorting="false"
                                                AllowFiltering="false" >
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmailReceivedDatetime" HeaderText="ReceivedDatetime" AllowSorting="false"
                                                UniqueName="EmailReceivedDatetime" AllowFiltering="false" >
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Delete Email" AllowFiltering="false">
                                                <HeaderStyle CssClass="mobileGridItem" />
                                                <ItemStyle CssClass="mobileGridItem" />
                                                <ItemTemplate>
                                                    <telerik:RadPageLayout ID="RPLayoutgridMobile" runat="server" GridType="Fluid">
                                                        <Rows>
                                                            <telerik:LayoutRow>
                                                                <Columns>
                                                                    <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
                                                                        <strong>Received From:</strong>
                                                                        <p>
                                                                            <%# Eval("EmailReceived") %>
                                                                        </p>
                                                                    </telerik:LayoutColumn>
                                                                </Columns>
                                                            </telerik:LayoutRow>
                                                        </Rows>
                                                        <Rows>
                                                            <telerik:LayoutRow>
                                                                <Columns>
                                                                    <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
                                                                        <strong>Subject:</strong>
                                                                        <p>
                                                                            <%# Eval("EmailSubject") %>
                                                                        </p>
                                                                    </telerik:LayoutColumn>
                                                                </Columns>
                                                            </telerik:LayoutRow>
                                                        </Rows>
                                                        <Rows>
                                                            <telerik:LayoutRow>
                                                                <Columns>
                                                                    <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
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
                                <telerik:RadGrid ID="RgvSMSReceived" runat="server" AutoGenerateColumns="False" Width="100%" RenderMode="Auto" FilterMenu-RenderMode="Lightweight"
                                    GroupPanelPosition="Top" IsExporting="False" PageSize="8" Font-Size="10" OnNeedDataSource="RgvSMSReceived_NeedDataSource"
                                    AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" OnPageIndexChanged="RgvSMSReceived_PageIndexChanged"
                                    OnSortCommand="RgvSMSReceived_SortCommand" OnItemDataBound="RgvSMSReceived_ItemDataBound">
                                    <ItemStyle Wrap="true"></ItemStyle>
                                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                    <MasterTableView AllowMultiColumnSorting="true">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="From" HeaderText="From" AllowSorting="false" AllowFiltering="false" ItemStyle-VerticalAlign="Top">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="To" HeaderText="To" AllowFiltering="true" SortExpression="To" AllowSorting="true" ItemStyle-VerticalAlign="Top">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>

                                            <telerik:GridTemplateColumn DataField="Body" HeaderText="Body" AllowFiltering="true" SortExpression="Body" AllowSorting="true" ItemStyle-VerticalAlign="Top">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBody" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Body") %>'>
                                                    </asp:Label>
                                                    <telerik:RadToolTip SkinID="Metro" RenderMode="Lightweight" ID="RadToolTip1" runat="server" TargetControlID="lblBody" Width="500px"
                                                        RelativeTo="Element" Position="MiddleRight">
                                                        <%# DataBinder.Eval(Container, "DataItem.Body") %>
                                                    </telerik:RadToolTip>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridBoundColumn DataField="Status"  AllowSorting="true"  SortExpression="Status"   HeaderText="Status" AllowFiltering="true"
                                                 ItemStyle-VerticalAlign="Top" >
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>

                                            <telerik:GridTemplateColumn DataField="Direction" HeaderText="Direction" AllowFiltering="true" SortExpression="Direction" AllowSorting="true"
                                                 ItemStyle-VerticalAlign="Top">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                                <ItemTemplate>
                                                    <asp:Image ID="imgdriection" runat="server"  />
                                                    <asp:Label ID="lbldirection" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Direction") %>'>
                                                    </asp:Label>
                                                    <telerik:RadToolTip SkinID="Metro" RenderMode="Lightweight"  runat="server" TargetControlID="imgdriection"
                                                        
                                                        RelativeTo="Element" Position="MiddleRight">
                                                        <%# DataBinder.Eval(Container, "DataItem.Direction") %>
                                                    </telerik:RadToolTip>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridBoundColumn DataField="Direction"  AllowSorting="true" Visible="false"  SortExpression="Direction"   HeaderText="Direction"
                                                 AllowFiltering="true" ItemStyle-VerticalAlign="Top" >
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="date_sent" AllowSorting="false" HeaderText="Date Time" AllowFiltering="false" ItemStyle-VerticalAlign="Top"
                                                 FilterControlWidth="90%">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ErrorMessage" AllowSorting="false" HeaderText="Error Message" AllowFiltering="false" ItemStyle-VerticalAlign="Top"
                                                 FilterControlWidth="90%">
                                                <HeaderStyle CssClass="desktopgridItem" />
                                                <ItemStyle CssClass="desktopgridItem" />
                                            </telerik:GridBoundColumn>

                                            <telerik:GridTemplateColumn HeaderText="Received SMS" AllowFiltering="false">
                                                <HeaderStyle CssClass="mobileGridItem" />
                                                <ItemStyle CssClass="mobileGridItem" />
                                                <ItemTemplate>
                                                    <telerik:RadPageLayout ID="RPLayoutgridMobile" runat="server" GridType="Fluid">
                                                        <Rows>
                                                            <telerik:LayoutRow>
                                                                <Columns>
                                                                    <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
                                                                        <strong>From:</strong>
                                                                        <p>
                                                                            <%# Eval("From") %>
                                                                        </p>
                                                                    </telerik:LayoutColumn>
                                                                </Columns>
                                                            </telerik:LayoutRow>
                                                        </Rows>
                                                        <Rows>
                                                            <telerik:LayoutRow>
                                                                <Columns>
                                                                    <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
                                                                        <strong>To:</strong>
                                                                        <p>
                                                                            <%# Eval("To") %>
                                                                        </p>
                                                                    </telerik:LayoutColumn>
                                                                </Columns>
                                                            </telerik:LayoutRow>
                                                        </Rows>
                                                        <Rows>
                                                            <telerik:LayoutRow>
                                                                <Columns>
                                                                    <telerik:LayoutColumn Span="12" SpanXs="12" SpanSm="12">
                                                                        <strong>Body:</strong>
                                                                        <p>
                                                                            <asp:Label ID="lblBodyMobile" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Body") %>'>
                                                                            </asp:Label>
                                                                            <telerik:RadToolTip SkinID="Metro" RenderMode="Lightweight" runat="server" TargetControlID="lblBodyMobile" Width="500px"
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
        </asp:Panel>
   </telerik:RadAjaxPanel>
     <telerik:RadCodeBlock ID="RCBAddTemplate" runat="server">
        <script type="text/javascript">
            (function ($) {

                requestStart = function (target, arguments) {
                    if (arguments.get_eventTarget().indexOf("imgbtnreplystd") > 0) {

                        arguments.set_enableAjax(false);
                    }
                    if (arguments.get_eventTarget().indexOf("download_fileInbox") > 0) {
                        arguments.set_enableAjax(false);
                    } 
                    if (arguments.get_eventTarget().indexOf("download_filesent") > 0) {
                        arguments.set_enableAjax(false);
                    }
                    if (arguments.get_eventTarget().indexOf("ItemChkboxEmailIsRead") > 0) {

                        arguments.set_enableAjax(false);
                    }
                    if (arguments.get_eventTarget().indexOf("DeleteInboxstdleademail") > 0) {
                        arguments.set_enableAjax(false);
                    }
                    if (arguments.get_eventTarget().indexOf("LinkbtnCloseExpandCollapse") > 0) {
                        arguments.set_enableAjax(false);
                    }
                }
            })($telerik.$);
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
