<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductsManagement.aspx.cs" Inherits="DelightShop.Admin.ProductsManagement" MasterPageFile="~/Admin/sidebar.Master" %> 

<asp:Content ContentPlaceHolderID="adminContent" Runat="server">
    <!-- MAIN -->
        <main>
            <div class="head-title">
                <div class="left">
                    <h1>Quản lý sản phẩm</h1>
                    
                </div>
            </div>
           <!-- Form thêm sản phẩm -->
            <div class="product-form">
                <h3>Thêm sản phẩm</h3>         
                    <div>
                        <div>
                            <label for="txtNewProductName">Tên sản phẩm:</label>
                            <asp:TextBox ID="txtNewProductName" runat="server" CssClass="form-control" />
                        </div>
                        <div>
                            <label for="ddlNewCategory">Loại:</label>
                            <asp:DropDownList ID="ddlNewCategory" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
             
                        <div>
                            <label for="txtNewPrice">Giá:</label>
                            <asp:TextBox ID="txtNewPrice" runat="server" CssClass="form-control" TextMode="Number" />
                        </div>
                    </div>

                <div>
                    <div>
                        <label for="txtNewOrigin">Xuất xứ:</label>
                        <asp:TextBox ID="txtNewOrigin" runat="server" CssClass="form-control" />
                    </div>
                
               
                    <div>
                        <label for="txtNewDescription">Mô tả:</label>
                        <asp:TextBox ID="txtNewDescription" runat="server" CssClass="form-control" TextMode="MultiLine" />
                    </div>

                    <div>
                        <label for="txtNewQuantity">Số lượng tồn:</label>
                        <asp:TextBox ID="txtNewQuantity" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                </div>

                <div>
                    <div>
                        <label for="fuProductImage">Tải ảnh lên:</label>
                        <asp:FileUpload ID="fuProductImage" runat="server" CssClass="form-control" />
                    </div>
                
                    

                    <div>
                        <asp:Button ID="btnAddProduct" runat="server" Text="Thêm sản phẩm" CssClass="btn btn-primary" OnClick="btnAddProduct_Click" />
                    </div>
                </div>
        </div>
            


<asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False" CssClass="table_green" BorderWidth="1"
    GridLines="None" AllowPaging="True" PageSize="10" DataKeyNames="ProductId"
    OnRowEditing="gvProducts_RowEditing" EnableViewState="true" OnRowUpdating="gvProducts_RowUpdating"
    OnRowCancelingEdit="gvProducts_RowCancelingEdit" OnRowDeleting="gvProducts_RowDeleting"
    OnPageIndexChanging="gvProducts_PageIndexChanging" OnRowDataBound="gvProducts_RowDataBound">
    <Columns>
        <asp:BoundField DataField="ProductId" HeaderText="Mã SP" SortExpression="ProductId" />
        
        <asp:TemplateField HeaderText="Loại SP">
            <ItemTemplate>
                <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CategoryId") %>' />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="ddlCategory" runat="server">
                </asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Tên SP" SortExpression="Name" />
        <asp:BoundField DataField="Price" HeaderText="Giá" SortExpression="Price" DataFormatString="{0:C}" />
        <asp:BoundField DataField="Origin" HeaderText="Xuất xứ" SortExpression="Origin" />
        <asp:BoundField DataField="Description" HeaderText="Mô tả" SortExpression="Description" />
        
        <asp:TemplateField HeaderText="Hình ảnh" SortExpression="Img">
            <ItemTemplate>
                <img src='<%# Eval("Img") %>' alt="Product Image" width="100" height="100" />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="QuantityInStock" HeaderText="Số lượng tồn" SortExpression="QuantityInStock" />
        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" ButtonType="Button" />
    </Columns>
</asp:GridView>
            <asp:Panel ID="pnlAddEditProduct" runat="server" CssClass="form-panel" Visible="false">
                <h3>Thêm/Sửa SP</h3>

                <div>
                    <label for="txtProductId">Mã SP:</label>
                    <asp:TextBox ID="txtProductId" runat="server" CssClass="form-control" ReadOnly="True" />
                </div>

                <div>
                    <label for="txtProductName">Tên SP:</label>
                    <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" />
                </div>

                <div>
                    <label for="ddlCategory">Loại SP:</label>
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>

                <div>
                    <label for="txtPrice">Gía:</label>
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" />
                </div>

                <div>
                    <label for="txtOrigin">Xuất xứ:</label>
                    <asp:TextBox ID="txtOrigin" runat="server" CssClass="form-control" />
                </div>

                <div>
                    <label for="txtDescription">Mô tả:</label>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" />
                </div>

                <div>
                    <label for="txtImg">Hình ảnh (URL):</label>
                    <asp:TextBox ID="txtImg" runat="server" CssClass="form-control" />
                </div>

                <div>
                    <label for="txtQuantityInStock">Quantity in Stock:</label>
                    <asp:TextBox ID="txtQuantityInStock" runat="server" CssClass="form-control" />
                </div>

                <div>
                    <asp:Button ID="btnUpdateProduct" runat="server" Text="Update Product" OnClick="btnUpdateProduct_Click" AutoPostBack="true" />
                    <asp:Button ID="btnCancelProduct" runat="server" Text="Cancel" OnClick="btnCancelProduct_Click" />
                </div>
            </asp:Panel>

        </main>
    <style>
        .product-form {
            display:flex;
            flex-direction:column;
            align-items:center;
            justify-content:center;
        }

        .product-form div{
            display:flex;
            margin: 0 10px;
        }

        div label {
            width:116px;
            margin: 10px;
        }

       

    .product-form h3 {
        font-size: 24px;
        margin-bottom: 20px;
        color: #333;
    }

    td input {
        width:100%;
    }
    </style>
</asp:Content>
