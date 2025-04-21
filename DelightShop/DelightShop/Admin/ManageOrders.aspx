<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageOrders.aspx.cs" Inherits="DelightShop.Admin.ManageOrders" MasterPageFile="~/Admin/sidebar.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="adminContent" runat="server">
    <style>
    </style>
        <main>
            <div class="head-title">
                <div class="left">
                    <h1>Quản lý đơn đặt hàng</h1>
                   
                </div>
            </div>

            <div class="order_info">
                <div class="order_edit">
                    <div>
                        <asp:Label ID="Label5" runat="server" Text="Mã khách hàng"></asp:Label>
                        <asp:TextBox ID="EditCustomerID" runat="server" CssClass="text_edit"></asp:TextBox>
                    </div>

                    <div>
                        <label for="ddlStatus">Trạng thái:</label>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                            <asp:ListItem Value="Processing">Processing</asp:ListItem>
                            <asp:ListItem Value="Pending">Pending</asp:ListItem>
                            <asp:ListItem Value="Completed">Completed</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div>
                        <asp:Label ID="Label2" runat="server" Text="Ngày đặt"></asp:Label>
                        <input type="datetime-local" id="EditDate" name="EditDate" required runat="server">
                    </div>


                    <div>
                        <asp:Label ID="Label3" runat="server" Text="Tổng tiền"></asp:Label>
                        <asp:TextBox ID="EditTotal" runat="server" CssClass="text_edit"></asp:TextBox>
                    </div>

                    <div>
                        <asp:Button ID="btnAdd" runat="server" Text="Thêm" CssClass="submit-btn" OnClick="Add_order_Click" />
                    </div>

                </div>

                <div class="chart-container">
                    <canvas id="statusChart"></canvas>
                </div>
            </div>

            <asp:GridView CssClass="table_green" ID="gvOrders" runat="server" AutoGenerateColumns="False"
                OnRowDataBound="gvOrders_RowDataBound"
                OnRowEditing="gvOrders_RowEditing"
                OnRowDeleting="gvOrders_RowDeleting"
                OnRowCancelingEdit="gvOrders_RowCancelingEdit"
                OnRowUpdating="gvOrders_RowUpdating"
                DataKeyNames="orderID">
                <Columns>
                    <asp:BoundField DataField="orderID" HeaderText="Số HD" SortExpression="orderID" ReadOnly="True" />

                    <asp:TemplateField HeaderText="Ngày đặt">
                        <ItemTemplate>
                            <asp:Label ID="lblOrderDate" runat="server" Text='<%# Eval("orderDate", "{0:dd/MM/yyyy}") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtOrderDate" runat="server" Text='<%# Eval("orderDate", "{0:dd/MM/yyyy}") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tổng tiền">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("total") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTotal" runat="server" Text='<%# Eval("total") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Trạng thái">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlStatus" runat="server">
                                <asp:ListItem Value="Processing">Processing</asp:ListItem>
                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                <asp:ListItem Value="Completed">Completed</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Mã khách">
                    <ItemStyle Width="100px" />

                        <ItemTemplate>
                            <asp:Label ID="lblCustomerID" runat="server" Text='<%# Eval("userID") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCustomerID" runat="server" Text='<%# Eval("userID") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowEditButton="True" HeaderText="Sửa" ButtonType="Button" ItemStyle-CssClass="editButton" />
                    <asp:CommandField ShowDeleteButton="True" HeaderText="Xóa" ButtonType="Button" ItemStyle-CssClass="deleteButton" />
                    <asp:TemplateField HeaderText="Chi tiết">
                        <ItemTemplate>
                            <asp:Button ID="btnSeeDetails" runat="server" Text="See details"
                                CommandArgument='<%# Eval("orderID") %>'
                                OnClick="ViewOrderDetails_Click" CssClass="view-order" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>

            <asp:Panel ID="pnlAddEdit" runat="server" CssClass="form-panel" Visible="false">
                <h3>Add/Edit Order</h3>
                <div>
                    <label for="txtOrderID">Order ID:</label>
                    <asp:TextBox ID="txtOrderID" runat="server" CssClass="form-control" />
                </div>
                <div>
                    <label for="txtOrderDate">Order Date:</label>
                    <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control" />
                </div>
                <div>
                    <label for="txtTotal">Total:</label>
                    <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" />
                </div>

                <div>
                    <label for="txtCustomerID">Customer ID:</label>
                    <ItemStyle Width="70px" />
                    <asp:TextBox ID="txtCustomerID" runat="server" CssClass="form-control" />
                </div>

                <div>
                    <label for="ddlStatus">Status:</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                        <asp:ListItem Value="Processing">Processing</asp:ListItem>
                        <asp:ListItem Value="Pending">Pending</asp:ListItem>
                        <asp:ListItem Value="Completed">Completed</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </asp:Panel>

            <asp:Label ID="lblProcess" runat="server" Text="Label" CssClass="hidden"></asp:Label>
            <asp:Label ID="lblPending" runat="server" Text="Label" CssClass="hidden"></asp:Label>
            <asp:Label ID="lblComplete" runat="server" Text="Label" CssClass="hidden"></asp:Label>

      

        </main>
        <div class="overlay">
            <div class="overlay-container">
                <button class="close-button">&times;</button>
                <table class="orderItems">
                    <thead>
                        <tr>
                            <th>Sản phẩm</th>
                            <th>Hình ảnh</th>
                            <th>Giá</th>
                            <th>Số lượng</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="orderItemsRepeater" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("orderDetailName") %></td>
                                    <td>
                                        <img src='<%# Eval("orderDetailImg") %>' alt="Product Image" /></td>
                                    <td><%# Eval("price", "{0:N0}") %> VND</td>
                                    <td><%# Eval("quantity") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>


    <script>
        document.addEventListener('DOMContentLoaded', function () {
            // Lấy số liệu từ các label
            var totalProcess = parseInt(document.getElementById('<%= lblProcess.ClientID %>').innerText.replace('Processing: ', '')) || 0;
             var totalPending = parseInt(document.getElementById('<%= lblPending.ClientID %>').innerText.replace('Pending: ', '')) || 0;
             var totalComplete = parseInt(document.getElementById('<%= lblComplete.ClientID %>').innerText.replace('Completed: ', '')) || 0;

             // Kiểm tra giá trị đã lấy được
             console.log(totalProcess, totalPending, totalComplete);

             var ctx = document.getElementById('statusChart').getContext('2d');
             var statusChart = new Chart(ctx, {
                 type: 'pie',
                 data: {
                     labels: ['Processing', 'Pending', 'Completed'],  // Tên nhóm trong biểu đồ
                     datasets: [{
                         label: 'Process Status Distribution',
                         data: [totalProcess, totalPending, totalComplete],
                         backgroundColor: ['#FFC3A0', '#FFEE93', '#A8E6CF'],  // Màu pastel nhẹ nhàng
                         borderColor: ['#FF9A76', '#FFD700', '#81C784'],  // Viền pastel tương ứng
                         borderWidth: 1

                     }]
                 },
                 options: {
                     responsive: true,
                     plugins: {
                         legend: {
                             position: 'top'
                         },
                         tooltip: {
                             callbacks: {
                                 label: function (tooltipItem) {
                                     return tooltipItem.label + ': ' + tooltipItem.raw + ' items';
                                 }
                             }
                         }
                     }
                 }
             });
         });

        document.querySelector('.close-button').addEventListener('click', function (event) {
            event.preventDefault();  // Ngăn ngừa hành động mặc định của sự kiện (postback)
            document.querySelector('.overlay').style.display = 'none';
        });

        // Khi bạn muốn mở overlay từ C# hoặc JavaScript
        function openOverlay() {
            document.querySelector(".overlay").style.display = "flex";
        }
    </script>
    <style>
        /* Overlay container */
        .overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5); /* Semi-transparent background */
            display: none; /* Hidden by default */
            align-items: center;
            justify-content: center;
            z-index: 9999; /* Ensures overlay is above other content */
        }

        /* Overlay content box */
        .overlay-container {
            background-color: white;
            padding: 30px;
            max-width: 80%;
            max-height: 80%;
            overflow-y: auto;
            border-radius: 10px;
            box-shadow: 0px 4px 15px rgba(0, 0, 0, 0.3);
        }

        /* Close button */
        .close-button {
            background: none;
            border: none;
            font-size: 30px;
            color: #333;
            cursor: pointer;
            position: absolute;
            top: 10px;
            right: 10px;
        }

            .close-button:hover {
                color: red;
            }

        /* Table inside overlay */
        .orderItems {
            width: 100%;
            border-collapse: collapse;
        }

            .orderItems th, .orderItems td {
                padding: 10px;
                border: 1px solid #ddd;
                text-align: left;
            }

            .orderItems th {
                background-color: #f2f2f2;
            }

            .orderItems img {
                width: 50px; /* Adjust the image size */
                height: auto;
            }
    </style>

</asp:Content>
