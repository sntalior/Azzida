<%@ Page Title="" Language="C#" MasterPageFile="~/AzzidaMaster.Master" AutoEventWireup="true" CodeBehind="JobFee.aspx.cs" Inherits="AzzidaAdmin.JobFee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content">
        <div class="box box-primary" id="dvAddEdit" runat="server" visible="true">
            <div class="box-header with-border">
                <h4 class="box-title">Job Fee</h4>
            </div>

            <div role="form">
                <div class="box-body">



                    <div class="form-group">
                        <label class="col-sm-3 control-label">Fee for job seeker in %: </label>
                        <div class="col-sm-10">
                            <asp:HiddenField ID="hdnjobfee" runat="server" Value="0" />
                            <asp:TextBox ID="txtseekerfee" runat="server" class="form-control" placeholder="Enter fee for job seeker"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvseekerfee" runat="server" ErrorMessage="Fee for job seeker is required." ControlToValidate="txtseekerfee" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            <br>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-3 control-label">Fee for job lister in %: </label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtListerfee" runat="server" class="form-control" placeholder="Enter fee for job lister"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvListerfee" runat="server" ErrorMessage="Fee for job lister is required." ControlToValidate="txtListerfee" ValidationGroup="Save">*</asp:RequiredFieldValidator>

                            <br>
                        </div>

                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">Cancellation Fee in %: </label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtcancelfee" runat="server" class="form-control" placeholder="Enter cancellation fee "></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvcancelfee" runat="server" ErrorMessage="Cancellation fee is required." ControlToValidate="txtcancelfee" ValidationGroup="Save">*</asp:RequiredFieldValidator>

                            <br>
                        </div>

                    </div>
                      <div class="form-group">
                        <label class="col-sm-3 control-label">Background Check: </label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtbckground" runat="server" class="form-control" placeholder="Enter background check"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvbckground" runat="server" ErrorMessage="Background check is required." ControlToValidate="txtbckground" ValidationGroup="Save">*</asp:RequiredFieldValidator>

                            <br>
                        </div>

                    </div>

                </div>

                <div class="box-footer">
                    <div style="float: right; margin-bottom: 10px;" class="col-lg-10">
                        <asp:Button ID="btnSave" Text="Save" ValidationGroup="Save" runat="server" OnClick="btnSave_Click"
                            class="btn btn-primary" TabIndex="11" />

                        <asp:ValidationSummary ID="val" runat="server" ShowMessageBox="true" ValidationGroup="Save"
                            ShowSummary="false" />
                    </div>

                </div>
            </div>
        </div>
    </section>
</asp:Content>
