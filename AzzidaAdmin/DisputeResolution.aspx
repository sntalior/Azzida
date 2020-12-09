<%@ Page Title="" Language="C#" MasterPageFile="~/AzzidaMaster.Master" AutoEventWireup="true" CodeBehind="DisputeResolution.aspx.cs" Inherits="AzzidaAdmin.DisputeResolution" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <div class="box box-primary" id="AddOfferdiv" runat="server">
            <div class="box-header with-border">
                <h4 class="box-title">Dispute Resolution</h4>

            </div>
            <div>
                <asp:GridView ID="GrdDisputeResolution" runat="server" AutoGenerateColumns="false" Width="100%"
                    PageSize="10" AllowPaging="true" OnPageIndexChanging="GrdDisputeResolution_PageIndexChanging" 
                    OnRowCommand="GrdDisputeResolution_RowCommand" OnRowDataBound="GrdDisputeResolution_RowDataBound">
                    <%--OnRowDataBound="GrdDisputeResolution_RowDataBound" --%>
                    <RowStyle HorizontalAlign="Center" />
                    <Columns>
                         <asp:TemplateField HeaderText="Dispute Id">
                            <ItemTemplate>
                                <%#Eval("Id") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dispute Reason">
                            <ItemTemplate>
                                <%#Eval("DisputeReason") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Post Associate">
                            <ItemTemplate>
                                <%#Eval("PostAssociate") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contact Way">
                            <ItemTemplate>
                                <%#Eval("ContactWay") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <%#Eval("Description") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Attachment">
                            <ItemTemplate>
                                
                                <asp:LinkButton ID="Attach" runat="server" CommandName="Attach" CommandArgument='<%#Eval("Id") %>' Text="View Attach"></asp:LinkButton>
                              <asp:HiddenField Id="hdnid" runat="server" Value='<%#Eval("Id") %>'/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Created Date">
                            <ItemTemplate>
                                <%#Eval("CreatedDate") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                     <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("Id") %>' CommandName="UpdateItem" 
                                                    Text="Edit" Visible="true"></asp:LinkButton>/
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Id") %>'
                                                    CommandName="DeleteItem" OnClientClick="return confirm('Are you sure you want to delete this category?');"
                                                    Text="Delete" Visible="true"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </section>
</asp:Content>

