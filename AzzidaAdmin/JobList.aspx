<%@ Page Title="" Language="C#" MasterPageFile="~/AzzidaMaster.Master" AutoEventWireup="true" CodeBehind="JobList.aspx.cs" Inherits="AzzidaAdmin.JobList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <style type="text/css">
        .cal_Theme1 .ajax__calendar_container {
            background-color: #e2e2e2;
            border: solid 1px #cccccc;
            z-index: 99999;
        }

        .cal_Theme1 .ajax__calendar_header {
            background-color: #ffffff;
            margin-bottom: 4px;
            z-index: 99999;
        }

        .cal_Theme1 .ajax__calendar_title, .cal_Theme1 .ajax__calendar_next, .cal_Theme1 .ajax__calendar_prev {
            color: #004080;
            padding-top: 3px;
            z-index: 99999;
        }

        .cal_Theme1 .ajax__calendar_body {
            background-color: #e9e9e9;
            border: solid 1px #cccccc;
            z-index: 99999;
        }

        .cal_Theme1 .ajax__calendar_dayname {
            text-align: center;
            font-weight: bold;
            margin-bottom: 4px;
            margin-top: 2px;
            z-index: 99999;
        }

        .cal_Theme1 .ajax__calendar_day {
            text-align: center;
        }

        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_day, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_month, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_year, .cal_Theme1 .ajax__calendar_active {
            color: #004080;
            font-weight: bold;
            background-color: #ffffff;
            z-index: 99999;
        }

        .cal_Theme1 .ajax__calendar_today {
            font-weight: bold;
            z-index: 99999;
        }

        .cal_Theme1 .ajax__calendar_other, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_today, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_title {
            color: #bbbbbb;
            z-index: 99999;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
      <%--  <div>
            <div>
                <h1>Job List
                </h1>
            </div>
        </div>--%>
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
        <div class="box box-primary" id="dvJobList" runat="server" visible="true">


            <div id="tab_2">
                <div>


                    <div>
                        <h2>
                            <i></i><span>Job List</span>&nbsp;
                        </h2>
                    </div>
                    <div>
                        <!--/row-->
                        <br>
                        <div>
                <asp:TextBox ID="txtt" runat="server" Height="32px" width="20%"></asp:TextBox> 
                    <asp:Button ID="btnSrch" Text="Search" ValidationGroup="Save" runat="server" OnClick="btnSrch_Click"
                        class="btn btn-primary" TabIndex="11" />
                    </div>
                        <br />
                        <div>
                            <asp:GridView ID="grdJob" runat="server" AutoGenerateColumns="false" Width="100%"
                                PageSize="10" AllowPaging="true" OnPageIndexChanging="grdJob_PageIndexChanging">
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Title">
                                        <ItemTemplate>
                                            <%#Eval("JobTitle") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="How Long">
                                        <ItemTemplate>
                                            <%#Eval("HowLong") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <%#Eval("Amount") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Job Category">
                                        <ItemTemplate>
                                            <%#Eval("JobCategory") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="From Date">
                                        <ItemTemplate>
                                            <%#Eval("FromDate") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <%#Eval("JobDescription") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location">
                                        <ItemTemplate>
                                            <%#Eval("Location") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Latitude">
                                        <ItemTemplate>
                                            <%#Eval("Latitude") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Longitude">
                                        <ItemTemplate>
                                            <%#Eval("Longitude") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <%--<asp:TemplateField HeaderText="Completed Date">
                                        <ItemTemplate>
                                            <%#Eval("CompletedDate") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <%-- <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                     <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("Id") %>' CommandName="UpdateItem" 
                                                    Text="Edit" Visible="true"></asp:LinkButton>/
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Id") %>'
                                                    CommandName="DeleteItem" OnClientClick="return confirm('Are you sure you want to delete this User?');"
                                                    Text="Delete" Visible="true"></asp:LinkButton>
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
