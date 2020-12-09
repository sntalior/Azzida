<%@ Page Title="" Language="C#" MasterPageFile="~/AzzidaMaster.Master" AutoEventWireup="true" CodeBehind="PaymentHistory.aspx.cs" Inherits="AzzidaAdmin.PaymentHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content">
        <div class="box box-primary" id="dvAddEdit" runat="server" visible="true">
            <div class="box-header with-border">
                <h4 class="box-title">Payment History</h4>
            </div>
            <div role="form">
                <div class="box-body">

                    <div class="form-group">
                        <label class="col-sm-2 control-label">Search: </label>
                        <div class="col-sm-3">

                            <asp:TextBox ID="txtSearch" runat="server" class="form-control"></asp:TextBox>

                        </div>
                        <div class="col-sm-3">

                            <asp:DropDownList ID="drpStatus" runat="server" class="form-control">
                                <%--OnSelectedIndexChanged="drpStatus_SelectedIndexChanged"--%>
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                <asp:ListItem Text="Success" Value="Success"></asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="col-sm-2">
                            <asp:Button ID="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click" class="btn btn-primary" TabIndex="11" />
                        </div>




                    </div>
                </div>
            </div>

            <div id="dvUserList" runat="server">

                <div id="tab_2">
                    <div>
                        <asp:Label ID="lbldata" runat="server"></asp:Label>

                        <div>
                            <asp:GridView ID="gvPaymentHistoryList" runat="server" AutoGenerateColumns="false" Width="100%"
                                PageSize="10" AllowPaging="true" OnPageIndexChanging="gvPaymentHistoryList_PageIndexChanging">
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Job Title">
                                        <ItemTemplate>
                                            <%#Eval("JobTitle") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lister Name">
                                        <ItemTemplate>
                                            <%#Eval("ListerName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Seeker Name">
                                        <ItemTemplate>
                                            <%#Eval("SeekerName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <%#Eval("JobAmount") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <%#Eval("TotalAmount") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Payment Type">
                                        <ItemTemplate>
                                            <%#Eval("paymentType") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Date">
                                        <ItemTemplate>
                                            <%#Eval("PaymentTime") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <%#Eval("Status") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <%--   <asp:TemplateField HeaderText="Device Type">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkActivate" runat="server" CommandName="Activate" CommandArgument='<%#Eval("Id") %>' Text="Suspend"></asp:LinkButton>
                                                <asp:HiddenField ID="hdnid" runat="server" Value='<%#Eval("Id") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View Chat">
                                            <ItemTemplate>
                                                  <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("Id") %>' CommandName="ViewChat"
                                                    Text="View" Visible="true"></asp:LinkButton>
                                                <%--<a href='Userchat.aspx?Id=<%#Eval("Id") %>'>View</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("Id") %>' CommandName="UpdateItem"
                                                    Text="Edit" Visible="true"></asp:LinkButton>/
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Id") %>'
                                                    CommandName="DeleteItem" OnClientClick="return confirm('Are you sure you want to terminate this user?');"
                                                    Text="Terminate" Visible="true"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
