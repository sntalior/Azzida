<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerifyEmail.aspx.cs" Inherits="AzzidaAdmin.VerifyEmail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link rel="shortcut icon" type="image/x-icon" href="Images/splash-logo.png" style="width: 40px;" />
    <title>Email Verification </title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.5 -->
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />
    <%-- media.css --%>
    <link href="bootstrap/css/media.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/AdminLTE.min.css" />
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
        <%--<div class="col-md-6">--%>
        <div class="col-md-12">
            <div style="width: 25%; float: left; height: 5px;"></div>
            <div style="width: 50%; margin-left: 5px; float: left;" id="verifybox">
                <%--  <img src="Images/splash-logo.png" style="width: 54%; margin-top: 91px; margin-left: 75px; margin-bottom: 10px;" />--%>
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h1 class="box-title">Email Verification</h1>
                    </div>
                    <!-- /.box-header -->
                    <!-- form start -->
                    <div id="SuccessMessage" runat="server" visible="false">
                        <span class="box-title" style="text-align: center !important;">
                            <asp:Literal ID="ltrMsg" Text="" runat="server" /></span>
                    </div>

<%--                    <div class="box-body" id="boxbody" runat="server" visible="true">
                        <div class="form-group">
                            <asp:HiddenField Value="0" ID="hdnpwd" runat="server" />
                            <asp:Label ID="lblemailverify" runat="server" Text="Enter Email" class="col-sm-2 control-label" Style="width: 25%;"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtemailverify" runat="server" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvemail" runat="server" ErrorMessage="Please enter email address." ControlToValidate="txtemailverify" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                <br />
                            </div>
                        </div>


                    </div>
                    <!-- /.box-body -->
                    <div class="box-footer" id="boxfooter" runat="server" visible="true">

                        <asp:Button ID="btnsave" runat="server" Text="Save" ValidationGroup="Save" class="btn btn-info pull-right" OnClick="btnsave_Click" />
                        <asp:ValidationSummary ID="val" runat="server" ShowMessageBox="true" ValidationGroup="Save"
                            ShowSummary="false" />
                    </div>
                    <!-- /.box-footer -->--%>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
