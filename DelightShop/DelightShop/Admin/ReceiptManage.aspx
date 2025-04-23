<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiptManage.aspx.cs" Inherits="DelightShop.Admin.ReceiptManage" MasterPageFile="~/Admin/sidebar.Master" %>

<asp:Content ContentPlaceHolderID="adminContent" Runat="server">
        <main>
            <div class="head-title">
                <div class="left">
                    <h1>Quản lý phiếu nhập</h1>
                    
                </div>
            </div>

            <div class="receipt-form">
                <h3>Thêm phiếu nhập</h3>

                <div>
               
                    <div>
                        <label for="txtReceiptDate">Ngày nhập:</label>
                        <asp:TextBox ID="txtReceiptDate" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                    <div>
                        <label for="txtReceiptTotalPrice">Tổng tiền:</label>
                        <asp:TextBox ID="txtReceiptTotalPrice" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                 </div>
                <div>
                   <div>
                        <label for="ddlReceiptStaff">Nhân viên lập:</label>
                        <asp:DropDownList ID="ddlReceiptStaff" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>


                     <div>
                         <label for="txtsupplierID">Nhà cung cấp:</label>
                          <asp:DropDownList ID="ddlsupplierID" runat="server" CssClass="form-control"></asp:DropDownList>
                     </div>
                 </div>

                <div>
                    <asp:Button ID="btnAddReceipt" runat="server" Text="Thêm phiếu nhập" CssClass="btn btn-primary" OnClick="btnAddReceipt_Click" />
                </div>
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            </div>

            <asp:GridView ID="gvReceipts" runat="server" AutoGenerateColumns="False" CssClass="table_green" BorderWidth="1"
                GridLines="None" AllowPaging="True" PageSize="10" DataKeyNames="receiptID"
                OnRowEditing="gvReceipts_RowEditing" EnableViewState="true" OnRowUpdating="gvReceipts_RowUpdating"
                OnRowCancelingEdit="gvReceipts_RowCancelingEdit" OnRowDeleting="gvReceipts_RowDeleting"
                OnPageIndexChanging="gvReceipts_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="receiptID" HeaderText="Số phiếu nhập" ReadOnly="True" />
                    <asp:BoundField DataField="receiptDate" HeaderText="Ngày nhập" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="receiptTotalPrice" HeaderText="Tổng tiền" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="receiptStaff" HeaderText="Nhân viên lập" />
                    <asp:BoundField DataField="supplierID" HeaderText="Nhà cung cấp" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" ButtonType="Button" />
                </Columns>
            </asp:GridView>

            <asp:Panel ID="pnlEditReceipt" runat="server" CssClass="form-panel" Visible="false">
                <h3>Edit Receipt</h3>
                <div>
                    <label for="txtEditReceiptID">Receipt ID:</label>
                    <asp:TextBox ID="txtEditReceiptID" runat="server" CssClass="form-control" ReadOnly="True" />
                </div>
                <div>
                    <label for="txtEditReceiptDate">Date:</label>
                    <asp:TextBox ID="txtEditReceiptDate" runat="server" CssClass="form-control" TextMode="Date" />
                </div>
                <div>
                    <label for="txtEditReceiptTotalPrice">Total Price:</label>
                    <asp:TextBox ID="txtEditReceiptTotalPrice" runat="server" CssClass="form-control" TextMode="Number" />
                </div>
                <div>
                    <label for="txtEditReceiptStaff">Staff ID:</label>
                    <asp:TextBox ID="txtEditReceiptStaff" runat="server" CssClass="form-control" TextMode="Number" />
                </div>
                <div>
                    <asp:Button ID="btnUpdateReceipt" runat="server" Text="Update" OnClick="btnUpdateReceipt_Click" />
                    <asp:Button ID="btnCancelReceipt" runat="server" Text="Cancel" OnClick="btnCancelReceipt_Click" />
                </div>
            </asp:Panel>
        </main>
   <style>
  .receipt-form {
    background: #fff;
    padding: 20px 25px;
    max-width: 100%;
    margin: 20px auto;
    border-radius: 6px;
    box-shadow: 0 0 6px rgba(0,0,0,0.08);
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}

.receipt-form h3 {
    text-align: center;
    margin-bottom: 20px;
    font-size: 20px;
    color: #333;
    font-weight: 600;
}

.receipt-form > div {
    display: flex;
    flex-wrap: wrap;
    gap: 16px;
    margin-bottom: 16px;
}

.receipt-form div > div {
    flex: 1 1 48%;
}

.receipt-form label {
    display: block;
    font-weight: 500;
    margin-bottom: 4px;
    font-size: 13px;
    color: #444;
}

.receipt-form .form-control {
    width: 100%;
    padding: 8px 10px;
    font-size: 13px;
    border: 1px solid #ccc;
    border-radius: 4px;
    box-sizing: border-box;
    height: 34px;
}

.receipt-form .form-control:focus {
    border-color: #4a90e2;
    outline: none;
    background-color: #fefefe;
}

.receipt-form .btn {
    display: block;
    width: 100%;
    padding: 10px;
    font-size: 14px;
    background-color: #4a90e2;
    color: white;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    transition: background-color 0.25s;
}

.receipt-form .btn:hover {
    background-color: #357abd;
}

   </style>
</asp:Content>
