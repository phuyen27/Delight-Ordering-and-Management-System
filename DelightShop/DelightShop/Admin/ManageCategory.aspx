<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageCategory.aspx.cs" Inherits="DelightShop.Admin.ManageCategory" MasterPageFile="~/Admin/sidebar.Master" %>

<asp:Content ContentPlaceHolderID="adminContent" runat="server">
    <main>
        <div class="head-title">
            <div class="left">
                <h1>Quản lý loại sản phẩm</h1>
               
            </div>
        </div>

        <div class="category-form">
            <h3>Thêm loại sản phẩm</h3>
            <div>
                <div>
                    <label for="txtCategoryName">Tên loại:</label>
                    <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" />
                </div>
                <div style="margin-top:10px;">
                    <asp:Button ID="btnAddCategory" runat="server" Text="Thêm loại" CssClass="btn btn-primary" OnClick="btnAddCategory_Click" />
                </div>
            </div>
        </div>

        <asp:GridView ID="gvCategories" runat="server" AutoGenerateColumns="False" CssClass="table_green"
            BorderWidth="1" GridLines="None" AllowPaging="True" PageSize="10"
            DataKeyNames="categoryID"
            OnRowEditing="gvCategories_RowEditing"
            OnRowUpdating="gvCategories_RowUpdating"
            OnRowCancelingEdit="gvCategories_RowCancelingEdit"
            OnRowDeleting="gvCategories_RowDeleting"
            OnPageIndexChanging="gvCategories_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="categoryID" HeaderText="Mã loại" ReadOnly="True" />
                <asp:BoundField DataField="categoryName" HeaderText="Tên loại" />
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" ButtonType="Button" />
            </Columns>
        </asp:GridView>

        <!-- Form chỉnh sửa loại sản phẩm (nếu dùng panel rời) -->
        <asp:Panel ID="pnlEditCategory" runat="server" CssClass="form-panel" Visible="false">
            <h3>Sửa loại sản phẩm</h3>
            <div>
                <label for="txtEditCategoryID">Mã loại:</label>
                <asp:TextBox ID="txtEditCategoryID" runat="server" CssClass="form-control" ReadOnly="True" />
            </div>
            <div>
                <label for="txtEditCategoryName">Tên loại:</label>
                <asp:TextBox ID="txtEditCategoryName" runat="server" CssClass="form-control" />
            </div>
            <div style="margin-top:10px;">
                <asp:Button ID="btnUpdateCategory" runat="server" Text="Sửa" />
                <asp:Button ID="btnCancelCategory" runat="server" Text="Hủy" />
            </div>
        </asp:Panel>
    </main>
    <style>


        .category-form div{
            display:flex;
            align-items:center;
           gap: 1rem;
        }
    </style>
</asp:Content>
