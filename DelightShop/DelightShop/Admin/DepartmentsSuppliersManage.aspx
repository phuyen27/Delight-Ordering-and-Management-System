<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentsSuppliersManage.aspx.cs" Inherits="DelightShop.Admin.DepartmentsSuppliersManage" MasterPageFile="~/Admin/sidebar.Master" %>

<asp:Content ContentPlaceHolderID="adminContent" Runat="server">
    <!-- MAIN -->
        <main>
            <div class="head-title">
                <div class="left">
                    <h1>Departments and Suppliers Management</h1>
                    <ul class="breadcrumb">
                        <li>
                            <a href="#">Management</a>
                        </li>
                        <li><i class='bx bx-chevron-right'></i></li>
                        <li>
                            <a class="active" href="#">Departments and Suppliers</a>
                        </li>
                    </ul>
                </div>
            </div>

            <!-- Departments Table Section -->
            <section class="department-supplier-table">
                <div class="table-container">
                    <h2>Manage Departments</h2>
                    <asp:GridView ID="gvDepartments" runat="server" AutoGenerateColumns="False" 
                        OnRowEditing="gvDepartments_RowEditing" 
                        OnRowDeleting="gvDepartments_RowDeleting" 
                        OnRowUpdating="gvDepartments_RowUpdating" 
                        OnRowCancelingEdit="gvDepartments_RowCancelingEdit">
                        <Columns>
                            <asp:BoundField DataField="MaPhong" HeaderText="Department ID" SortExpression="MaPhong" ReadOnly="True" />
                            <asp:BoundField DataField="TenPhong" HeaderText="Department Name" SortExpression="TenPhong" />
                            <asp:BoundField DataField="NamTL" HeaderText="Established Year" SortExpression="NamTL" />
                            <asp:BoundField DataField="SoLuongNV" HeaderText="Number of Employees" SortExpression="SoLuongNV" />
                            <asp:CommandField ShowEditButton="True" HeaderText="Edit" />
                            <asp:CommandField ShowDeleteButton="True" HeaderText="Delete" />
                        </Columns>
                    </asp:GridView>
                    
                    <h2>Manage Suppliers</h2>
                    <asp:GridView ID="gvSuppliers" runat="server" AutoGenerateColumns="False" 
                        OnRowEditing="gvSuppliers_RowEditing" 
                        OnRowDeleting="gvSuppliers_RowDeleting" 
                        OnRowUpdating="gvSuppliers_RowUpdating" 
                        OnRowCancelingEdit="gvSuppliers_RowCancelingEdit">
                        <Columns>
                            <asp:BoundField DataField="MaNCC" HeaderText="Supplier ID" SortExpression="MaNCC" ReadOnly="True" />
                            <asp:BoundField DataField="TenNCC" HeaderText="Supplier Name" SortExpression="TenNCC" />
                            <asp:BoundField DataField="DiaChi" HeaderText="Address" SortExpression="DiaChi" />
                            <asp:BoundField DataField="SDTNCC" HeaderText="Phone" SortExpression="SDTNCC" />
                            <asp:BoundField DataField="EmailNCC" HeaderText="Email" SortExpression="EmailNCC" />
                            <asp:BoundField DataField="WebsiteNCC" HeaderText="Website" SortExpression="WebsiteNCC" />
                            <asp:CommandField ShowEditButton="True" HeaderText="Edit" />
                            <asp:CommandField ShowDeleteButton="True" HeaderText="Delete" />
                        </Columns>
                    </asp:GridView>
                </div>
            </section>
        </main>

    <!-- Modal for Add/Edit Department and Supplier -->
    <div id="departmentModal" class="modal">
        <div class="modal-content">
            <span class="close" onclick="closeModal()">&times;</span>
            <h2 id="modalTitle">Add Department</h2>
            <form id="departmentForm">
                <label for="departmentName">Department Name:</label>
                <input type="text" id="departmentName" name="departmentName" required />

                <label for="establishedYear">Established Year:</label>
                <input type="text" id="establishedYear" name="establishedYear" required />

                <label for="numberOfEmployees">Number of Employees:</label>
                <input type="number" id="numberOfEmployees" name="numberOfEmployees" required />

                <button type="submit">Save</button>
            </form>
        </div>
    </div>

    <script>
        function openModal() {
            document.getElementById('departmentModal').style.display = "block";
        }

        function closeModal() {
            document.getElementById('departmentModal').style.display = "none";
        }
    </script>
    <style>
        /* General Styles */
body {
    font-family: Arial, sans-serif;
}

.main {
    padding: 20px;
}

.head-title {
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.head-title h1 {
    margin: 0;
}

.breadcrumb {
    display: flex;
    list-style: none;
    padding: 0;
}

.breadcrumb li {
    margin-right: 10px;
}

/* Table Styling */
.department-supplier-table {
    margin-top: 20px;
}

.table-container {
    margin-bottom: 50px;
}

table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 20px;
}

th, td {
    padding: 10px;
    text-align: left;
    border: 1px solid #ddd;
}

th {
    background-color: #f4f4f4;
}

/* Modal Styling */
.modal {
    display: none;
    position: fixed;
    z-index: 1;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0,0,0,0.5);
}

.modal-content {
    position: relative;
    background-color: white;
    margin: 10% auto;
    padding: 20px;
    width: 40%;
    box-shadow: 0 4px 8px rgba(0,0,0,0.1);
    border-radius: 5px;
}

.close {
    position: absolute;
    top: 10px;
    right: 10px;
    font-size: 30px;
    cursor: pointer;
}

.modal form {
    display: flex;
    flex-direction: column;
}

.modal input {
    margin-bottom: 10px;
    padding: 8px;
    font-size: 16px;
}

.modal button {
    background-color: #4CAF50;
    color: white;
    padding: 10px;
    border: none;
    cursor: pointer;
}

.modal button:hover {
    background-color: #45a049;
}

    </style>
</asp:Content>
