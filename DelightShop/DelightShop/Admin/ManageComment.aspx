<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageComment.aspx.cs" Inherits="DelightShop.Admin.ManageComment" MasterPageFile="~/Admin/sidebar.Master" %>

<asp:Content ContentPlaceHolderID="adminContent" runat="server">
    <main>
        <div class="head-title">
            <div class="left">
                <h1>Quản lý đánh giá</h1>
               
            </div>
        </div>

        <div class="faq-form">
            <h3>Thêm đánh giá</h3>

            <div>
                <div>
                    <label for="ddlMaDH">Mã đơn hàng:</label>
                    <asp:DropDownList ID="ddlMaDH" runat="server" CssClass="form-control" />
                </div>

                <div>
                    <label for="txtComment">Nội dung đánh giá:</label>
                    <asp:TextBox ID="txtComment" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                </div>

                <div>
                    <asp:Button ID="btnAddFAQ" runat="server" Text="Thêm đánh giá" CssClass="btn btn-primary" OnClick="btnAddFAQ_Click" />
                </div>
            </div>
        </div>

        <asp:GridView ID="gvFAQs" runat="server" AutoGenerateColumns="False" CssClass="table_green" BorderWidth="1"
            GridLines="None" AllowPaging="True" PageSize="10" DataKeyNames="MaDG"
            OnRowEditing="gvFAQs_RowEditing" OnRowUpdating="gvFAQs_RowUpdating"
            OnRowCancelingEdit="gvFAQs_RowCancelingEdit" OnRowDeleting="gvFAQs_RowDeleting"
            OnPageIndexChanging="gvFAQs_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="MaDG" HeaderText="Mã ĐG" ReadOnly="True" />
                <asp:BoundField DataField="MaDH" HeaderText="Mã Đơn Hàng" />
                <asp:BoundField DataField="Comment" HeaderText="Nội dung" />
                <asp:BoundField DataField="DateFAQ" HeaderText="Ngày đánh giá" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" ButtonType="Button" />
            </Columns>
        </asp:GridView>

        <!-- Panel sửa đánh giá -->
        <asp:Panel ID="pnlEditFAQ" runat="server" CssClass="form-panel" Visible="false">
            <h3>Sửa đánh giá</h3>

            <div>
                <label for="txtEditMaDG">Mã Đánh Giá:</label>
                <asp:TextBox ID="txtEditMaDG" runat="server" CssClass="form-control" ReadOnly="True" />
            </div>

            <div>
                <label for="ddlEditMaDH">Mã Đơn Hàng:</label>
                <asp:DropDownList ID="ddlEditMaDH" runat="server" CssClass="form-control" />
            </div>

            <div>
                <label for="txtEditComment">Nội dung:</label>
                <asp:TextBox ID="txtEditComment" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
            </div>

            <div>
                <asp:Button ID="btnUpdateFAQ" runat="server" Text="Cập nhật" CssClass="btn btn-success" />
                <asp:Button ID="btnCancelFAQ" runat="server" Text="Hủy" CssClass="btn btn-secondary"  />
            </div>
        </asp:Panel>
    </main>
    <style>
        .faq-form div{
            display:flex;
            margin: 15px 0;
            width:100%;
        }

        .form-control {
            width:150px;
            height:40px;
            margin:0 10px;
        }
    </style>
</asp:Content>
