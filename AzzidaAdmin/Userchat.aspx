<%@ Page Title="" Language="C#" MasterPageFile="~/AzzidaMaster.Master" AutoEventWireup="true" CodeBehind="Userchat.aspx.cs" Inherits="AzzidaAdmin.Userchat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #out {
            height: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- 5506343--%>
    <section class="content">
        <div id="dvUserList" runat="server">

            <div id="tab_2">

                <div>
                    <div>
                        <h2>
                            <i></i>
                            <label id="username" runat="server"></label>
                        </h2>
                    </div>

                    <br>
                    <div>
                        <asp:GridView ID="gvUserChats" runat="server" AutoGenerateColumns="false" Width="100%"
                            PageSize="10" AllowPaging="true" OnRowCommand="gvUserChats_RowCommand">
                            <RowStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="Recent Chats">
                                    <ItemTemplate>
                                        <a id="lblReceiverName" runat="server"></a>
                                        <asp:HiddenField Id="hdnJobId" runat="server" Value='<%#Eval("JobId") %>'/>
                                        <asp:LinkButton ID="lnkreceivername" runat="server" CommandArgument='<%#Eval("ToId") %>' CommandName="viewChat"
                                            Text='<%#Eval("ReceiverName") %>' Visible="true"></asp:LinkButton><br />
                                        <%--  <asp:Label ID="lblMsg" runat="server"><%#Eval("message") %></asp:Label>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>




                            </Columns>
                        </asp:GridView>
                    </div>

                </div>
            </div>
        </div>


    </section>
    <section class="content">
        <div class="box box-primary" id="userconversion" runat="server" visible="false">
            <div class="box-header with-border">
                <h4 class="box-title" id="ReceiverName" runat="server"></h4>

            </div>
            <div id="out" style="height: 300px; overflow-y: auto;">

                <asp:Repeater ID="rptrchatconversion" runat="server" EnableViewState="true" OnItemDataBound="rptrchatconversion_ItemDataBound">
                    <ItemTemplate>
                        <div id="div1">


                            <div class="col-sm-12">

                                <div class="form-box">


                                    <div class="col-sm-12" id="selfUser" runat="server">
                                        <div class="form-group" style="float: left; ">
                                            <%--min-height: 75px;--%>
                                            <div style="float: left">
                                                <asp:Image ID="imgid" runat="server" ImageUrl='<%#Eval("SenderProfilePic") %>' Style="width: 50px; height: 50px; border-radius: 60px;" />
                                            </div>
                                            <div class="col-sm-6" style="float: left;">
                                                <asp:Label ID="lblsendermessage" runat="server" Text='<%#Eval("UserMessage") %>' Style="font-size: 18px;"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblUsrName" runat="server" Text='<%#Eval("SenderName") %>' Style="color: gray; font-size: 12px; float: left;"></asp:Label>
                                                <asp:HiddenField ID="hdnsenderId" runat="server" Value='<%#Eval("SenderId") %>' />
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-sm-12" id="otheruser" runat="server">
                                        <div class="form-group" style="float: left;">
                                            <%--margin-right: 29px; min-height: 75px;" min-height: 75px;--%>
                                            <div style="float: left;">
                                                <%--class="col-sm-2"--%>
                                                <asp:Image ID="imgid1" runat="server" ImageUrl='<%#Eval("SenderProfilePic") %>' Style="width: 50px; height: 50px; border-radius: 60px;" />
                                            </div>
                                            <div class="col-sm-6" style="float: left;">
                                                <asp:Label ID="txtReceivermsg" runat="server" Text='<%#Eval("UserMessage") %>' Style="font-size: 18px;"></asp:Label>
                                                </br>
                                        <asp:Label ID="lblsenderName" runat="server" Text='<%#Eval("SenderName") %>' Style="color: gray; font-size: 12px; float: right;"></asp:Label>
                                                <%--   <asp:HiddenField ID="hdnotherId" runat="server" Value='<%#Eval("SenderId") %>' />--%>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

    </section>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            //    var out = document.getElementById("out")
            //let c = 0

            //setInterval(function () {
            //    // allow 1px inaccuracy by adding 1
            //    const isScrolledToBottom = out.scrollHeight - out.clientHeight <= out.scrollTop + 1

            //    const newElement = document.createElement("div")

            //    newElement.textContent = format(c++, 'Bottom position:', out.scrollHeight - out.clientHeight, 'Scroll position:', out.scrollTop)

            //    out.appendChild(newElement)

            //    // scroll to bottom if isScrolledToBottom is true
            //    if (isScrolledToBottom) {
            //        out.scrollTop = out.scrollHeight - out.clientHeight
            //    }
            //}, 500)
            debugger;
            var element = document.getElementById("out");
            element.scrollTop = element.scrollHeight;

        });

    </script>

</asp:Content>
