<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="DelightShop.Admin.OrderDetail" MasterPageFile="~/Admin/sidebar.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="adminContent" runat="server">
    <style>
    </style>
        <main>
            <div class="head-title">
                <div class="left">
                    <h1>Quản lý chi tiết hóa đơn</h1>
                    
                </div>
            </div>
            
            <div class="orderdetail_info">
                <div class="orderDetail_Add">
                    <div>
                        <div>
                            <asp:Label ID="LabelMaDH" runat="server" Text="Mã HD"></asp:Label>
                            <asp:TextBox ID="EditMaDH" runat="server" CssClass="text_edit"></asp:TextBox>
                        </div>

                        <div>
                            <asp:Label ID="LabelMaSP" runat="server" Text="Sản phẩm"></asp:Label>
                            <asp:TextBox ID="EditMaSP" runat="server" CssClass="text_edit"></asp:TextBox>
                        </div>
                    </div>

                    <div>
                        <div>
                            <asp:Label ID="LabelSoLuong" runat="server" Text="Số lượng"></asp:Label>
                            <asp:TextBox ID="EditSoLuong" runat="server" CssClass="text_edit"></asp:TextBox>
                        </div>

                        <div>
                            <asp:Label ID="LabelDonGia" runat="server" Text="Đơn giá"></asp:Label>
                            <asp:TextBox ID="EditDonGia" runat="server" CssClass="text_edit"></asp:TextBox>
                        </div>
                    </div>
                    
                    <div>
                        <asp:Button ID="btnAddDetail" runat="server" Text="Thêm" CssClass="cancel-btn" OnClick="btnAdd_Detail" />
                    </div>
                </div>

               </div>


            <asp:GridView CssClass="table_pastel_christmas" ID="gvOrderDetails" runat="server" AutoGenerateColumns="False"
                OnRowDataBound="gvOrderDetails_RowDataBound"
                OnRowEditing="gvOrderDetails_RowEditing"
                OnRowDeleting="gvOrderDetails_RowDeleting"
                OnRowCancelingEdit="gvOrderDetails_RowCancelingEdit"
                OnRowUpdating="gvOrderDetails_RowUpdating"
                DataKeyNames="orderDetailID">
                <Columns>
                    <asp:BoundField DataField="orderDetailID" HeaderText="Mã CT" SortExpression="orderDetailID" ReadOnly="True" />

                    <asp:TemplateField HeaderText="Mã SP">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("orderDetailName") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtProductName" runat="server" Text='<%# Eval("orderDetailName") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Số lượng">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("quantity") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("quantity") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Gía">
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("price") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPrice" runat="server" Text='<%# Eval("price") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="orderID" HeaderText="Mã đơn đặt" SortExpression="orderID" ReadOnly="True" />

                    <asp:CommandField ShowEditButton="True" HeaderText="Sửa" ButtonType="Button" ItemStyle-CssClass="editButton" />
                    <asp:CommandField ShowDeleteButton="True" HeaderText="Xóa" ButtonType="Button" ItemStyle-CssClass="deleteButton" />
                </Columns>
            </asp:GridView>


            <asp:Panel ID="pnAddEditDetail" runat="server" CssClass="form-panel" Visible="false">
                <h3>Add/Edit Order Detail</h3>
                <div>
                    <label for="txtOrderDetailID">Order Detail ID:</label>
                    <asp:TextBox ID="txtOrderDetailID" runat="server" CssClass="form-control" ReadOnly="True" />
                </div>
    
                <div>
                    <label for="txtProductName">Product Name:</label>
                    <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" />
                </div>
    
                <div>
                    <label for="txtQuantity">Quantity:</label>
                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" />
                </div>

                <div>
                    <label for="txtPrice">Price:</label>
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" />
                </div>

                <div>
                    <label for="txtOrderID">Order ID:</label>
                    <asp:TextBox ID="txtOrderIDEdit" runat="server" CssClass="form-control"/>
                </div>

                <div>
                    <asp:Button ID="btnSaveDetail" runat="server" Text="Save"  />
                    <asp:Button ID="btnCancelDetail" runat="server" Text="Cancel" OnClick="btnCancelDetail_Click" />
                </div>
            </asp:Panel>

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
