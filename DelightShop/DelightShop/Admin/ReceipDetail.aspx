<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceipDetail.aspx.cs" Inherits="DelightShop.Admin.ReceipDetail" MasterPageFile="~/Admin/sidebar.Master"%>

<asp:Content ContentPlaceHolderID="adminContent" Runat="server">
    <main>
        <div class="head-title">
            <div class="left">
                <h1>Quản lý chi tiết phiếu nhập</h1>
               
            </div>
        </div>

        <div class="receipt-form">
            <h3>Thêm chi tiết phiếu nhập</h3>

            <div>
                <div>
                    <label for="ddlReceiptDetailID">Mã chi tiết phiếu:</label>
                    <asp:DropDownList ID="ddlReceiptDetailID" runat="server" CssClass="form-control" />
                </div>

                <div>
                    <label for="ddlReceiptProductID">Mã sản phẩm:</label>
                    <asp:DropDownList ID="ddlReceiptProductID" runat="server" CssClass="form-control" />
                </div>

                <div>
                    <label for="txtProductQuantity">Số lượng:</label>
                    <asp:TextBox ID="txtProductQuantity" runat="server" CssClass="form-control" TextMode="Number" />
                </div>

                <div>
                    <label for="txtPrice">Giá:</label>
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" TextMode="Number" />
                </div>

               

                <div>
                    <asp:Button ID="btnAddReceiptDetail" runat="server" Text="Thêm chi tiết" CssClass="btn btn-primary" OnClick="btnAddReceiptDetail_Click" />
                </div>
            </div>
        </div>

        <asp:GridView ID="gvReceiptDetails" runat="server" AutoGenerateColumns="False" CssClass="table_green" BorderWidth="1"
            GridLines="None" AllowPaging="True" PageSize="10" DataKeyNames="receiptDetailID"
            OnRowEditing="gvReceiptDetails_RowEditing" OnRowUpdating="gvReceiptDetails_RowUpdating"
            OnRowCancelingEdit="gvReceiptDetails_RowCancelingEdit" OnRowDeleting="gvReceiptDetails_RowDeleting"
            OnPageIndexChanging="gvReceiptDetails_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="receiptDetailID" HeaderText="Mã Chi Tiết" ReadOnly="True" />
                <asp:BoundField DataField="receiptProductID" HeaderText="Mã SP" />
                <asp:BoundField DataField="productQuantity" HeaderText="Số lượng" />
                <asp:BoundField DataField="Price" HeaderText="Giá" />
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" ButtonType="Button" />
            </Columns>
        </asp:GridView>

        <asp:Panel ID="pnlAddEditReceiptDetail" runat="server" CssClass="form-panel" Visible="false">
            <h3>Sửa chi tiết phiếu nhập</h3>

            <div>
                <label for="ddlEditReceiptDetailID">Mã PN:</label>
                <asp:DropDownList ID="ddlEditReceiptDetailID" runat="server" CssClass="form-control" Enabled="false" />
            </div>

            <div>
                <label for="ddlEditReceiptProductID">Mã sản phẩm:</label>
                <asp:DropDownList ID="ddlEditReceiptProductID" runat="server" CssClass="form-control" />
            </div>

            <div>
                <label for="txtEditProductQuantity">Số lượng:</label>
                <asp:TextBox ID="txtEditProductQuantity" runat="server" CssClass="form-control" TextMode="Number" />
            </div>

            <div>
                <label for="txtEditPrice">Giá:</label>
                <asp:TextBox ID="txtEditPrice" runat="server" CssClass="form-control" TextMode="Number" />
            </div>

            

            <div>
                <asp:Button ID="btnUpdateReceiptDetail" runat="server" Text="Cập nhật" CssClass="btn btn-success" />
                <asp:Button ID="btnCancelReceiptDetail" runat="server" Text="Hủy"  CssClass="btn btn-secondary" />
            </div>
        </asp:Panel>

    </main>

    <style>
        .receipt-form div {
            display:flex;
            align-items:center;
            justify-content:center;
            gap: 10px;
        }

        .form-control {
            width: 100px;
        }
    </style>
</asp:Content>
