<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierManage.aspx.cs" Inherits="DelightShop.Admin.SupplierManage" MasterPageFile="~/Admin/sidebar.Master" %>

<asp:Content ContentPlaceHolderID="adminContent" Runat="server">
        <main>
            <div class="head-title">
                <div class="left">
                    <h1>Quản lý nhà cung cấp</h1>
                    <ul class="breadcrumb">
                        <li><a href="#">Supplier Management</a></li>
                        <li><i class='bx bx-chevron-right'></i></li>
                        <li><a class="active" href="#">Home</a></li>
                    </ul>
                </div>
            </div>

            <div class="supplier-form">
                <h3>Thêm nhà cung cấp</h3>

                <div>
                    <div>
                        <label for="txtSupplierName">Tên:</label>
                        <asp:TextBox ID="txtSupplierName" runat="server" CssClass="form-control" />
                    </div>
                    <div>
                        <label for="txtSupplierAddress">Địa chỉ:</label>
                        <asp:TextBox ID="txtSupplierAddress" runat="server" CssClass="form-control" />
                    </div>
                    <div>
                        <label for="txtSupplierEmail">Email:</label>
                        <asp:TextBox ID="txtSupplierEmail" runat="server" CssClass="form-control" TextMode="Email" />
                    </div>
                </div>

                <div>
                    <div>
                        <label for="txtSupplierPhone">Số điện thoại:</label>
                        <asp:TextBox ID="txtSupplierPhone" runat="server" CssClass="form-control" />
                    </div>
                    <div>
                        <label for="txtSupplierWebsite">Website:</label>
                        <asp:TextBox ID="txtSupplierWebsite" runat="server" CssClass="form-control" />
                    </div>

                    <div>
                        <asp:Button ID="btnAddSupplier" runat="server" Text="Thêm nhà cung cấp" CssClass="btn btn-primary" OnClick="btnAddSupplier_Click" />
                    </div>
                </div>
            </div>

           
            <asp:GridView ID="gvSuppliers" runat="server" AutoGenerateColumns="False" CssClass="table_green" BorderWidth="1"
                GridLines="None" AllowPaging="True" PageSize="10" DataKeyNames="supplierID"
                OnRowEditing="gvSuppliers_RowEditing" EnableViewState="true" OnRowUpdating="gvSuppliers_RowUpdating"
                OnRowCancelingEdit="gvSuppliers_RowCancelingEdit" OnRowDeleting="gvSuppliers_RowDeleting"
                OnPageIndexChanging="gvSuppliers_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="supplierID" HeaderText="Mã NCC" ReadOnly="True" />
                    <asp:BoundField DataField="supplierName" HeaderText="Tên" />
                    <asp:BoundField DataField="supplierAddress" HeaderText="Địa chỉ" />
                    <asp:BoundField DataField="supplierEmail" HeaderText="Email" />
                    <asp:BoundField DataField="supplierPhone" HeaderText="SĐT" />
                    <asp:BoundField DataField="supplierWebsite" HeaderText="Website" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" ButtonType="Button" />
                </Columns>
            </asp:GridView>

           
            <asp:Panel ID="pnlAddEditSupplier" runat="server" CssClass="form-panel" Visible="false">
                <h3>Sửa NCC</h3>
                <div>
                    <label for="txtEditSupplierID">Mã NCC:</label>
                    <asp:TextBox ID="txtEditSupplierID" runat="server" CssClass="form-control" ReadOnly="True" />
                </div>
                <div>
                    <label for="txtEditSupplierName">Tên:</label>
                    <asp:TextBox ID="txtEditSupplierName" runat="server" CssClass="form-control" />
                </div>
                <div>
                    <label for="txtEditSupplierAddress">Địa chỉ:</label>
                    <asp:TextBox ID="txtEditSupplierAddress" runat="server" CssClass="form-control" />
                </div>
                <div>
                    <label for="txtEditSupplierEmail">Email:</label>
                    <asp:TextBox ID="txtEditSupplierEmail" runat="server" CssClass="form-control" />
                </div>
                <div>
                    <label for="txtEditSupplierPhone">SĐT:</label>
                    <asp:TextBox ID="txtEditSupplierPhone" runat="server" CssClass="form-control" />
                </div>
                <div>
                    <label for="txtEditSupplierWebsite">Website:</label>
                    <asp:TextBox ID="txtEditSupplierWebsite" runat="server" CssClass="form-control" />
                </div>
                <div>
                    <asp:Button ID="btnUpdateSupplier" runat="server" Text="Sửa" OnClick="btnUpdateSupplier_Click" />
                    <asp:Button ID="btnCancelSupplier" runat="server" Text="Hủy" OnClick="btnCancelSupplier_Click" />
                </div>
            </asp:Panel>
        </main>
    
    <style>
        .supplier-form{
            display:flex;
            flex-direction:column;
            justify-content:center;
            align-items:center;
        }

        .supplier-form h3 {
            color:#1d582d;
            font-weight:700;
            font-size:2rem;
        }

        .supplier-form div{
            width:100%;
            display:flex;
            margin: 10px 5px;            
        }

        .supplier-form div div label{
            width:122px;
            color: indianred;
        }

        .form-control {
            padding: 10px 12px;
            border-radius: 6px;
            border: 1px solid #ccc;
            transition: border-color 0.3s;
            font-size: 15px;
            width: 160px;
        }
        .supplier-form .btn {
           
            padding: 10px 18px;
            font-size: 16px;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            background-color: #3498db;
            color: white;
            transition: background-color 0.3s;
        }

        .supplier-form .btn:hover {
            background-color: #2c80b4;
        }
     
        
    </style>
</asp:Content>

