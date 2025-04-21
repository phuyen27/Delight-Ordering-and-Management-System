<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfor.aspx.cs" Inherits="DelightShop.User.UserInfor" MasterPageFile="~/User/HeaderFooter.master" %>

<asp:Content ContentPlaceHolderID="MainContent" Runat="server">
    <form id="form1" runat="server">
            <head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/remixicon/4.1.0/remixicon.min.css">
    <link rel="stylesheet" href="assets/css/style.css">
    <title>User Information</title>
</head>
<body>
    <section class="section container grid">
        <div class="user">
            <div class="user-profile">
                <h2>Personal Information</h2>
                <img src="<%= customer.avt %>" alt="User Image" class="profile-img" />
                <div class="profile-details">
                    <div class="user-info">
                        <p><strong>User name:</strong> <%= customer.Name %></p>
                        <p><strong>Email:</strong> <%= customer.Username %></p>
                        <p><strong>Phone number:</strong> <%= customer.Phone %></p>
                        <p><strong>Address:</strong> <%= customer.Address %></p>
                        <p><strong>Date of Birth:</strong> <%= customer.Date %></p>
                    </div>
                </div>
            </div>

    
            <div class="user-orders">               
               <div class="order-list">
                    <h2>Your order</h2>
                    <asp:Repeater ID="orderRepeater" runat="server">
                        <ItemTemplate>
                            <div class="order-item">
                                <p><strong>Order ID:</strong> <span><%# Eval("orderID") %></span></p>
                                <p><strong>Order date:</strong> <span><%# Eval("orderDate") %></span></p>
                                <p><strong>Total:</strong> <span><%# Eval("total", "{0:N0}") %> VND</span></p>
                              <asp:Button ID="btnSeeDetails" runat="server" Text="See details" 
                                    CommandArgument='<%# Eval("orderID") %>' 
                                    OnClick="ViewOrderDetails_Click" CssClass="view-order" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

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
                            <td><img src='<%# Eval("orderDetailImg") %>' alt="Product Image" /></td>
                            <td><%# Eval("price", "{0:N0}") %> VND</td>
                            <td><%# Eval("quantity") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</div>

        </div>
    </section>
</body>
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
    </form>
</asp:Content>
