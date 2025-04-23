<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeManagement.aspx.cs" Inherits="DelightShop.Admin.EmployeeManagement" MasterPageFile="~/Admin/sidebar.Master" %>

<asp:Content ContentPlaceHolderID="adminContent" Runat="server">
    <!-- MAIN -->
        <main>
            <div class="head-title">
                <div class="left">
                    <h1>Quản lý nhân viên</h1>
                    
                </div>
            </div>

            <!-- Employee Table Section -->
                            <!-- Add/Edit Modal -->
          
            <div class="supplier-form">
    <h3>Thêm nhân viên</h3>

    <div>
        <div>
            <label for="firstName">Họ:</label>
            <asp:TextBox ID="firstName" runat="server" CssClass="form-control" placeholder="Nhập họ" />
        </div>
        <div>
            <label for="lastName">Tên:</label>
            <asp:TextBox ID="lastName" runat="server" CssClass="form-control" placeholder="Nhập tên" />
        </div>
        <div>
            <label for="employeePhone">Số điện thoại:</label>
            <asp:TextBox ID="employeePhone" runat="server" CssClass="form-control" placeholder="Nhập SĐT" />
        </div>
    </div>

    <div>
        <div>
            <label for="employeeGender">Giới tính:</label>
            <asp:DropDownList ID="employeeGender" runat="server" CssClass="form-control">
                <asp:ListItem Text="Nam" Value="Male" />
                <asp:ListItem Text="Nữ" Value="Female" />
            </asp:DropDownList>
        </div>
        <div>
            <label for="employeeDepartment">Phòng ban:</label>
            <asp:DropDownList ID="employeeDepartment" runat="server" CssClass="form-control" />
        </div>
        <div>
            <label for="employeeBirthday">Ngày sinh:</label>
            <asp:TextBox ID="employeeBirthday" runat="server" CssClass="form-control" TextMode="Date" />
        </div>
    </div>

    <div>
        <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn btn-primary" OnClick="SaveEmployee" />
    </div>
</div>

            <section class="employee-table">                
               <asp:GridView CssClass="table_pastel_christmas" ID="gvEmployees" runat="server" AutoGenerateColumns="False" 
                    OnRowDataBound="gvEmployees_RowDataBound"
                    OnRowEditing="gvEmployees_RowEditing"
                    OnRowDeleting="gvEmployees_RowDeleting"
                    OnRowCancelingEdit="gvEmployees_RowCancelingEdit"
                    OnRowUpdating="gvEmployees_RowUpdating"
                    DataKeyNames="employeeID" OnSorting="gvEmployees_Sorting">
                    <Columns>
                        <asp:BoundField DataField="employeeID" HeaderText="Mã NV" SortExpression="employeeID" ReadOnly="True" />

                        <asp:TemplateField HeaderText="Họ NV">
                            <ItemTemplate>
                                <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("firstName") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFirstName" runat="server" Text='<%# Eval("firstName") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tên NV">
                            <ItemTemplate>
                                <asp:Label ID="lblLastName" runat="server" Text='<%# Eval("lastName") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLastName" runat="server" Text='<%# Eval("lastName") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="SĐT">
                            <ItemTemplate>
                                <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("phone") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPhone" runat="server" Text='<%# Eval("phone") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Giới tính">
                            <ItemTemplate>
                                <asp:Label ID="lblGender" runat="server" Text='<%# Eval("gender") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlGender" runat="server">
                                    <asp:ListItem Value="Male">Nam</asp:ListItem>
                                    <asp:ListItem Value="Female">Nữ</asp:ListItem>
                                    <asp:ListItem Value="Other">Khác</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ngày sinh">
                            <ItemTemplate>
                                <asp:Label ID="lblDOB" runat="server" Text='<%# Eval("dob", "{0:dd/MM/yyyy}") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDOB" runat="server" Text='<%# Eval("dob", "{0:dd/MM/yyyy}") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Phòng ban">
                            <ItemTemplate>
                                <asp:Label ID="lblEmployeeType" runat="server" Text='<%# Eval("employeeTypeID") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlEmployeeType" runat="server">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:CommandField ShowEditButton="True" HeaderText="Sửa" ButtonType="Button" ItemStyle-CssClass="editButton" />
                        <asp:CommandField ShowDeleteButton="True" HeaderText="Xóa" ButtonType="Button" ItemStyle-CssClass="deleteButton" />
                    </Columns>
                </asp:GridView>


                <asp:Panel ID="pnlAddEdit" runat="server" CssClass="form-panel" Visible="false">
                    <h3>Add/Edit Employee</h3>
                    <div>
                        <label for="txtEmployeeID">Employee ID:</label>
                        <asp:TextBox ID="txtEmployeeID" runat="server" CssClass="form-control" ReadOnly="True" />
                    </div>
                    <div>
                        <label for="txtFirstName">First Name:</label>
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" />
                    </div>
                    <div>
                        <label for="txtLastName">Last Name:</label>
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" />
                    </div>
                    <div>
                        <label for="txtPhone">Phone:</label>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" />
                    </div>
                    <div>
                        <label for="ddlGender">Gender:</label>
                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                            <asp:ListItem Value="Male">Male</asp:ListItem>
                            <asp:ListItem Value="Female">Female</asp:ListItem>
                            <asp:ListItem Value="Other">Other</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div>
                        <label for="txtDOB">Date of Birth:</label>
                        <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" />
                    </div>
                    <div>
                        <label for="ddlEmployeeType">Employee Type:</label>
                        <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>

                    <div>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" AutoPostBack="true" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </div>
                </asp:Panel>


            </section>          
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
