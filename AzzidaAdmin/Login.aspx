<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AzzidaAdmin.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Azzida Admin|  Login Area</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.5 -->
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/AdminLTE.min.css">
    <!-- AdminLTE Skins. We have chosen the skin-blue for this starter
          page. However, you can choose any other skin. Make sure you
          apply the skin class to the body tag so the changes take effect.
    -->
    <link rel="stylesheet" href="dist/css/skins/skin-blue.min.css" />
    <script type="text/javascript" src="https://cdn.ywxi.net/js/1.js" async></script>

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-md-6">
            <div class="box box-info">
                <div class="box-header with-border">
                    <h1 class="box-title">Login to your account</h1>
                </div>
                <!-- /.box-header -->
                <!-- form start -->
                <div>
                    <span>
                        <asp:Literal ID="ltrMsg" Text="" runat="server" /></span>
                </div>

                <div class="box-body">
                    <div class="form-group">
                        <asp:Label ID="lblUserName" runat="server" Text="UserName" class="col-sm-2 control-label"></asp:Label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="Enter UserName"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="Please Enter Your User Name" ControlToValidate="txtUserName" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblPassword" runat="server" Text="Password" class="col-sm-2 control-label"></asp:Label>
                        <div class="col-sm-10">

                            <asp:TextBox ID="txtPassword" runat="server" class="form-control" placeholder="Enter Password" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="password cannot be Left Blank " ControlToValidate="txtPassword" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </div>
                    </div>

                </div>
                <!-- /.box-body -->
                <div class="box-footer">

                    <asp:Button ID="btnLogin" runat="server" Text="Sign in" ValidationGroup="Save" OnClick="btnLogin_Click" class="btn btn-info pull-right" />

                    <asp:ValidationSummary ID="val" runat="server" ShowMessageBox="true" ValidationGroup="Save"
                        ShowSummary="false" />
                </div>
                <!-- /.box-footer -->

            </div>
        </div>
    </form>
</body>
</html>
