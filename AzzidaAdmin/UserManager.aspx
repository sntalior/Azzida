<%@ Page Title="" Language="C#" MasterPageFile="~/Azzidamaster.Master" AutoEventWireup="true" CodeBehind="UserManager.aspx.cs" Inherits="AzzidaAdmin.UserManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

            <div role="form">
                <div class="box-body">

                    <div class="form-group">
                        <label class="col-sm-3 control-label">Role: </label>
                        <div class="col-sm-10">

                            <asp:DropDownList ID="drprole" runat="server" class="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvrole" runat="server" ErrorMessage="Role  is required." ControlToValidate="drprole" InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            <br>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-3 control-label">First Name: </label>
                        <div class="col-sm-10">
                            <asp:HiddenField ID="hdnUserValue" runat="server" Value="0" />
                            <asp:TextBox ID="txtfirstName" runat="server" class="form-control" placeholder="Enter First Name"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="First name  is required." ControlToValidate="txtfirstName" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            <br>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-3 control-label">Last Name: </label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtLastName" runat="server" class="form-control" placeholder="Enter Last Name"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="Last name  is required." ControlToValidate="txtLastName" ValidationGroup="Save">*</asp:RequiredFieldValidator>

                            <br>
                        </div>

                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">User Name: </label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="Enter User Name" Enabled="false"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvtxtUserName" runat="server" ErrorMessage="User name  is required." ControlToValidate="txtUserName" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                             
                            <br>
                        </div>

                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">Email Type: </label>
                        <div class="col-sm-10">

                            <asp:DropDownList ID="drpEmailType" runat="server" class="form-control">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Facebook" Value="facebook"></asp:ListItem>
                                <asp:ListItem Text="Google" Value="google"></asp:ListItem>
                                <asp:ListItem Text="Other" Value="other"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvet" runat="server" ErrorMessage="Email Type  is required." ControlToValidate="drpEmailType" InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            <br>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-3 control-label">Email: </label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Enter Email "></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Email is required." ControlToValidate="txtEmail" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rfvEmailExpression"
                                runat="server" ErrorMessage="Please enter valid email address."
                                ValidationGroup="Save" ControlToValidate="txtEmail"
                                CssClass="requiredFieldValidateStyle"
                                ForeColor="Red"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                            </asp:RegularExpressionValidator>

                            <br>
                        </div>

                    </div>

                    <div class="form-group">
                        <label class="col-sm-3 control-label">Password: </label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" class="form-control" placeholder="Enter Password "></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Password is Required" ControlToValidate="txtPassword" ValidationGroup="Save">*</asp:RequiredFieldValidator>


                            <br>
                        </div>

                    </div>

                    <div class="form-group">
                        <label class="col-sm-3 control-label">Confirm Password:</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" class="form-control" placeholder="Enter Confirm Password "></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ErrorMessage="Confirm Password is Required" ControlToValidate="txtConfirmPassword" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="NewPasswordCompare" runat="server" ErrorMessage="The Confirm Password is not matched with Password."
                                ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" ValidationGroup="Save">*</asp:CompareValidator>


                            <br>
                        </div>

                    </div>

                    <div class="form-group">
                        <label class="col-sm-3 control-label">Skills: </label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtskills" runat="server" TextMode="MultiLine" class="form-control" placeholder="Enter Skills "></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvskills" runat="server" ErrorMessage="Skills is required." ControlToValidate="txtskills" ValidationGroup="Save">*</asp:RequiredFieldValidator>



                        </div>

                    </div>


                    <%-- <div class="form-group">
                    <label class="col-sm-3 control-label">Device Id: </label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtdeviceId" runat="server" class="form-control" placeholder="Enter Device Id "></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvdeviceId" runat="server" ErrorMessage="Device id is required." ControlToValidate="txtdeviceId" ValidationGroup="Save">*</asp:RequiredFieldValidator>


                        <br>
                    </div>

                </div>--%>

                    <%-- <div class="form-group">
                    <label class="col-sm-3 control-label">Device Type: </label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtdevicetype" runat="server" class="form-control" placeholder="Enter Device Type "></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvdevicetype" runat="server" ErrorMessage="Device type is required." ControlToValidate="txtdevicetype" ValidationGroup="Save">*</asp:RequiredFieldValidator>


                        <br>
                    </div>

                </div>--%>
                    <div class="form-group">
                        <label class="col-sm-3 control-label" id="lblImage">Profile Picture: </label>
                        <div class="col-sm-10">
                            <%--    <asp:Image ID="profilePicture" ImageUrl="ProfilePictureUrl" runat="server" class="img-fluid" alt="" Style="height: 130px; width: 130px;" />--%>
                            <asp:FileUpload ID="ProfilePicture1" runat="server" class="form-control"></asp:FileUpload>
                            <%-- <asp:ImageButton ID="btnimagedelete1" OnClick="btnimagedelete1_Click" ImageUrl="~/dist/img/images.png" runat="server" Width="30px" Height="30px" Visible="false" />--%>
                            <br />
                        </div>
                    </div>
                </div>

                <div class="box-footer">
                    <div style="float: right; margin-bottom: 10px;" class="col-lg-10">
                        <asp:Button ID="btnSave" Text="Save" ValidationGroup="Save" runat="server" OnClick="btnSave_Click"
                            class="btn btn-primary" TabIndex="11" />
                        <%--  <asp:Button ID="btnactivate" Text="Activate" ValidationGroup="Save" runat="server" OnClick="btnactivate_Click"
                            class="btn btn-primary" TabIndex="11" CausesValidation="False" />--%>
                        <asp:ValidationSummary ID="val" runat="server" ShowMessageBox="true" ValidationGroup="Save"
                            ShowSummary="false" />
                    </div>

                </div>
            </div>

            <div id="dvUserList" runat="server">

                <div id="tab_2">
                    <div>
                        <asp:LinkButton ID="lnkAddUser" runat="server" Visible="false">Add User</asp:LinkButton>
                        <br />
                        <br />
                        <div>
                            <div>
                                <h2>
                                    <i></i><span>User List</span>
                                </h2>
                            </div>

                            <br>
                            <div>
                                <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="false" Width="100%"
                                    PageSize="10" AllowPaging="true" OnPageIndexChanging="gvUser_PageIndexChanging" OnRowCommand="gvUser_RowCommand" OnRowDataBound="gvUser_RowDataBound">
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
                                                <asp:LinkButton ID="lnkActivate" runat="server" CommandName="Activate" CommandArgument='<%#Eval("Id") %>' Text="Suspend"></asp:LinkButton>
                                                <asp:HiddenField ID="hdnid" runat="server" Value='<%#Eval("Id") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View Chat">
                                            <ItemTemplate>
                                                  <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("Id") %>' CommandName="ViewChat"
                                                    Text="View" Visible="true"></asp:LinkButton>
                                                <%--<a href='Userchat.aspx?Id=<%#Eval("Id") %>'>View</a>--%>
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
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
