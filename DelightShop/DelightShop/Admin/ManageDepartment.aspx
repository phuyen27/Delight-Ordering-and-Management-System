<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageDepartment.aspx.cs" Inherits="DelightShop.Admin.ManageDepartment" MasterPageFile="~/Admin/sidebar.Master" %>

<asp:Content ContentPlaceHolderID="adminContent" Runat="server">
    <main>
        <div class="head-title">
            <div class="left">
                <h1>Quản lý phòng ban</h1>
               
            </div>
        </div>

        <div class="department-form">
            <h3>Thêm phòng ban</h3>

            <div>
                <div>
                    <label for="txtDepartmentName">Tên phòng ban:</label>
                    <asp:TextBox ID="txtDepartmentName" runat="server" CssClass="form-control" />
                </div>
                <div>
                    <label for="txtDepartmentDate">Ngày thành lập:</label>
                    <asp:TextBox ID="txtDepartmentDate" runat="server" CssClass="form-control" TextMode="Date" />
                </div>
                <div>
                    <label for="txtDepartmentQuantity">Số lượng nhân sự:</label>
                    <asp:TextBox ID="txtDepartmentQuantity" runat="server" CssClass="form-control" TextMode="Number" />
                </div>

                <div>
                    <asp:Button ID="btnAddDepartment" runat="server" Text="Thêm phòng ban" CssClass="btn btn-primary" OnClick="btnAddDepartment_Click" />
                </div>
            </div>
        </div>

        <asp:GridView ID="gvDepartments" runat="server" AutoGenerateColumns="False" CssClass="table_green" BorderWidth="1"
            GridLines="None" AllowPaging="True" PageSize="10" DataKeyNames="departmentID"
            OnRowEditing="gvDepartments_RowEditing"
            OnRowUpdating="gvDepartments_RowUpdating"
            OnRowCancelingEdit="gvDepartments_RowCancelingEdit"
            OnRowDeleting="gvDepartments_RowDeleting"
            OnPageIndexChanging="gvDepartments_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="departmentID" HeaderText="Mã PB" ReadOnly="True" />
                <asp:BoundField DataField="departmentName" HeaderText="Tên phòng ban" />
                <asp:BoundField DataField="departmentDate" HeaderText="Ngày thành lập" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="departmentQuantity" HeaderText="Số lượng nhân sự" />
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" ButtonType="Button" />
            </Columns>
        </asp:GridView>

        <asp:Panel ID="pnlEditDepartment" runat="server" CssClass="form-panel" Visible="false">
            <h3>Sửa thông tin phòng ban</h3>
            <div>
                <label for="txtEditDepartmentID">Mã phòng ban:</label>
                <asp:TextBox ID="txtEditDepartmentID" runat="server" CssClass="form-control" ReadOnly="True" />
            </div>
            <div>
                <label for="txtEditDepartmentName">Tên phòng ban:</label>
                <asp:TextBox ID="txtEditDepartmentName" runat="server" CssClass="form-control" />
            </div>
            <div>
                <label for="txtEditDepartmentDate">Ngày thành lập:</label>
                <asp:TextBox ID="txtEditDepartmentDate" runat="server" CssClass="form-control" TextMode="Date" />
            </div>
            <div>
                <label for="txtEditDepartmentQuantity">Số lượng nhân sự:</label>
                <asp:TextBox ID="txtEditDepartmentQuantity" runat="server" CssClass="form-control" TextMode="Number" />
            </div>
            <div>
                <asp:Button ID="btnUpdateDepartment" runat="server" Text="Sửa" />
                <asp:Button ID="btnCancelDepartment" runat="server" Text="Hủy" />
            </div>
        </asp:Panel>
    </main>
    <style>
        td input {
            width:100%;
            margin:0 5px;
        }

        .department-form div{
            display:flex;
            justify-content:center;
            align-items:center;
        }

        div input {
            width:125px;
            margin:5px;
            padding: 5px;
        }

      
    </style>
</asp:Content>
