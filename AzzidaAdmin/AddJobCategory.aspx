<%@ Page Title="" Language="C#" MasterPageFile="~/AzzidaMaster.Master" AutoEventWireup="true" CodeBehind="AddJobCategory.aspx.cs" Inherits="AzzidaAdmin.AddJobCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
      
        <div runat="server" id="dvEr" visible="false">
            <div style="width: 100%; text-align: center;">
                <div>
                    <p>
                        <asp:Literal Text="" ID="ltrErr" runat="server" />
                    </p>
                </div>
            </div>
        </div>
    </section>
    <section class="content">
        <div class="box box-primary" id="dvAddEdit" runat="server" visible="true">
            <div class="box-header with-border">
                <h4 class="box-title">Job Category </h4>
            </div>

            <div role="form">
                <div class="box-body">
               


                <div class="form-group">
                    <label class="col-sm-3 control-label">Category Name:</label>
                    <div class="col-sm-10">
                          <asp:HiddenField ID="hdncatId" runat="server" Value="0" />
                        <asp:TextBox ID="txtCategoryName" runat="server" class="form-control" placeholder="Enter Category Name "></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" ErrorMessage="Category name  is required." ControlToValidate="txtCategoryName" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        <br>
                    </div>
                </div>

              </div>
                <br />
                <div class="box-footer">
                     <div style="float: right; margin-bottom: 10px;" class="col-lg-10">
                    <asp:Button ID="btnSave" Text="Save" ValidationGroup="Save" runat="server" OnClick="btnSave_Click"
                        class="btn btn-primary" TabIndex="11" />
                   
                    <asp:ValidationSummary ID="val" runat="server" ShowMessageBox="true" ValidationGroup="Save"
                        ShowSummary="false" />
                        </div>
                </div>
        </div>
        
        <div id="dvUserList" runat="server">
          
            <div id="tab_2">
                <div>
                   
            
                    <div>
                        <div>
                            <h2>
                                <i></i><span>Job Category List</span>&nbsp;
                            </h2>
                        </div>
                        <div>
                            <!--/row-->
                            <br>
                            <div>
                                <asp:GridView ID="grdJobCategory" runat="server" AutoGenerateColumns="false" Width="100%"
                                    PageSize="10" AllowPaging="true" OnPageIndexChanging="grdJobCategory_PageIndexChanging" OnRowCommand="grdJobCategory_RowCommand">
                                    <RowStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Category Name">
                                            <ItemTemplate>
                                                <%#Eval("CategoryName") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        

                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                     <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("Id") %>' CommandName="UpdateItem" 
                                                    Text="Edit" Visible="true"></asp:LinkButton>/
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Id") %>'
                                                    CommandName="DeleteItem" OnClientClick="return confirm('Are you sure you want to delete this category?');"
                                                    Text="Delete" Visible="true"></asp:LinkButton>
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
        </div>
    </section>
</asp:Content>
