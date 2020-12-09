<%@ Page Title="" Language="C#" MasterPageFile="~/AzzidaMaster.Master" AutoEventWireup="true" CodeBehind="SendMessage.aspx.cs" Inherits="AzzidaAdmin.SendMessage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ccl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">

        <div runat="server" id="dvError" visible="false">
            <div style="width: 100%; text-align: center;">
                <div>
                    <p>
                        <asp:Literal Text="" ID="ltrError" runat="server" />
                    </p>
                </div>
            </div>
        </div>
    </section>
    <section class="content">
        <div class="box box-primary" id="dvAddEdit" runat="server" visible="true">
            <div class="box-header with-border">
                <h4 class="box-title">User</h4>
            </div>
            <asp:Label ID="lblsuccessmsg" runat="server" Text=""></asp:Label>
            <asp:Button ID="btntest" runat="server" Style="display: none" />
            <ccl:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btntest"
                PopupControlID="pnlT" BehaviorID="ModalPopupExtender1" CancelControlID="btnclose">
            </ccl:ModalPopupExtender>

            <asp:Panel ID="pnlT" runat="server" Style="background-color: #fff; border: solid 5px silver; display: NONE"
                Width="60%" Height="50%" ScrollBars="Vertical">
                <div style="background-color: #fff;">
                    <div style="float: right">
                        
                        <asp:ImageButton ID="btnclose" runat="server" ImageUrl="ApplicationImages/btn-close.png" ToolTip="Close" Style="width: 30px; height: 30px;" />
                    </div>
                    <div role="form">
                <div class="box-body">
                    <div class="box-header with-border">
                <h4 class="box-title">Send Message</h4>
            </div>
                    <br />
                    <br />
                    
                    <div class="form-group" id="sentmsgdiv" runat="server">
                        <asp:HiddenField ID="hdnuserId" runat="server" Value="0" />
                        <label class="col-sm-2 control-label">Message : </label>
                        <div class="col-sm-3">

                            <asp:TextBox ID="txtmessage" runat="server" class="form-control"></asp:TextBox>

                        </div>
                       
                        <div class="col-sm-2">
                            <asp:Button ID="btnsendmsg" Text="Send" runat="server" class="btn btn-primary" OnClick="btnsendmsg_Click" TabIndex="11" />
                        </div>




                    </div>
                </div>
            </div>
                    <div>
                    </div>

                </div>
            </asp:Panel>
            <div id="tab_2">
                <div>
                  
                    <div>
                        <%--<div>
                            <h2>
                                <i></i><span>User List</span>
                            </h2>
                        </div>--%>

                        <br>
                        <div>
                            <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="false" Width="100%"
                                PageSize="10" AllowPaging="true" OnPageIndexChanging="gvUser_PageIndexChanging" OnRowCommand="gvUser_RowCommand" >
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="First Name">
                                        <ItemTemplate>
                                            <%#Eval("FirstName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Last Name">
                                        <ItemTemplate>
                                            <%#Eval("LastName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Name">
                                        <ItemTemplate>
                                            <%#Eval("UserName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <%#Eval("UserEmail") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Password">
                                        <ItemTemplate>
                                            <%#Eval("UserPassword") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Skills">
                                        <ItemTemplate>
                                            <%#Eval("Skills") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Device Type">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnksendmessage" runat="server" CommandName="sendmsg" CommandArgument='<%#Eval("Id") %>' Text="Send Message"></asp:LinkButton>
                                                <asp:HiddenField ID="hdnid" runat="server" Value='<%#Eval("Id") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    
                                </Columns>
                            </asp:GridView>
                        </div>


                    </div>
                </div>
            </div>
        </div>

    </section>
</asp:Content>
