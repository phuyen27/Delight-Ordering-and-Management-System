<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="DelightShop.User.Cart" MasterPageFile="~/User/HeaderFooter.master" %>

<asp:Content ContentPlaceHolderID="MainContent" Runat="server">
    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Giỏ Hàng</title>
      
        <script>
            function updateTotal() {
                var total = 0;
                var rows = document.querySelectorAll('.cart-table tbody tr'); 

                rows.forEach(function (row) {
                    var price = parseFloat(row.querySelector('.cart-price').textContent.replace(' ₫', '').replace(/\./g, '').replace(',', '.'));
                    var quantity = parseInt(row.querySelector('.cart-quantity').value);
                    var totalPrice = price * quantity;

                    row.querySelector('.cart-total').textContent = totalPrice.toLocaleString() + ' ₫';

                    if (row.querySelector('.cart-checkbox').checked) {
                        total += totalPrice;
                    }
                });

                document.getElementById('sumCartChoose').textContent = 'Tổng tiền: ' + total.toLocaleString() + ' ₫';
                document.getElementById('hiddenTotalAmount').value = total;
            }

            document.addEventListener('DOMContentLoaded', function () {
                document.querySelectorAll('.cart-quantity, .cart-checkbox').forEach(function (el) {
                    el.addEventListener('change', updateTotal);
                });
                updateTotal();
            });

           
            function ConfirmCheckout() {
                updateTotal();  // Cập nhật tổng tiền trước khi thanh toán
                if (confirm("Bạn có chắc chắn muốn thanh toán không?")) {
                    return true; // Tiến hành sự kiện click
                }
                return false; 
            }

            function ConfirmDelete(button) {
                if (confirm("Bạn có chắc chắn muốn xóa không?")) {
                    const productButton = document.getElementById('pdid');
                    button.setAttribute("data-product-id") = productButton;
                    return productButton;
                }
                return false;
            }

        </script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="section container grid">
                <div class="cart-container">
                    <table class="cart-table">
                        <thead>
                            <tr>
                                <th>Chọn</th>
                                <th>Sản phẩm</th>
                                <th>Hình ảnh</th>
                                <th>Giá</th>
                                <th>Số lượng</th>
                                <th>Thành tiền</th>
                                <th>Thao tác</th>
                            </tr>
                        </thead>
                        <tbody runat="server">
                        <% 
                        foreach (var item in cartItems) { 
                        %>
                        <tr>                            
                           <td><input type="checkbox" id="checkbox-<%: item.ProductID %>" name="checkbox-<%: item.ProductID %>" class="cart-checkbox"></td>
                           <td><%: item.ProductName %></td>
                           <td><img src="<%: item.ProductImg %>" alt="<%: item.ProductName %>"></td>
                           <td class="cart-price"><%: item.Price.ToString("N0") %> ₫</td>
                           <td><input type="number" name="quantity-<%: item.ProductID %>" value="<%: item.Quantity %>" min="1" class="cart-quantity" /></td>
                           <td class="cart-total" id="cart-total-<%: item.ProductID %>"><%: (item.Quantity * item.Price).ToString("N0") %> ₫</td>
        
                            <td>
                                 <button id="pdid" style="none"><%= item.ProductID %></button>
                                    <asp:Button 
                                    ID="btnXoa" 
                                    runat="server" 
                                    CssClass="remove-btn" 
                                    Text="Xóa"  
                                    data-product-id='0'
                                    OnClientClick="return ConfirmDelete(this);" 
                                    OnClick="btnXoa_Click" />
                        </tr>
                        <% 
                        } 
                        %>
                    </tbody>

                    </table>

                    <div class="cart-summary">
                        <p id="sumCartChoose">Tổng tiền: <%= totalAmount.ToString("N0") %> ₫</p>
                        <input type="hidden" id="hiddenTotalAmount" name="hiddenTotalAmount" />
                        <asp:Button 
                            ID="btnCheckout" 
                            runat="server" 
                            Text="Thanh toán" 
                            OnClick="Checkout_Click" 
                            CssClass="checkout-btn" 
                            OnClientClick="updateTotal(); return ConfirmCheckout();" />

                    </div>
                </div>
            </div>
        </form>
    </body>
</asp:Content>
