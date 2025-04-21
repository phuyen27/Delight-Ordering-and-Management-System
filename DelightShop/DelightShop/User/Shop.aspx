<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shop.aspx.cs" Inherits="DelightShop.User.Shop" MasterPageFile="~/User/HeaderFooter.master" %>

<asp:Content ContentPlaceHolderID="MainContent" Runat="server">
    <form id="form1" runat="server">
        <section class="shop__section section" id="shop">
            <!-- Search bar -->
            <div id="search-container">
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="SearchProduct" CssClass="button-search" BackColor="#FF3300" BorderColor="Maroon" BorderStyle="Solid" ForeColor="White" />
              <<input type="search" ID="searchinput" runat="server" placeholder="Search product name here.." CssClass="search" style="padding: 5px; margin: 2px; border-style: solid; border-color: #FF0000;" width="10px" />
            </div>
        
            <!-- Filter buttons -->
            <div id="buttons">
                <asp:Button ID="btnAll" runat="server" Text="Tất cả" OnClick="FilterProducts" CommandArgument="0" CssClass="button-value" />
                <asp:Button ID="btnDecor" runat="server" Text="Đồ trang trí" OnClick="FilterProducts" CommandArgument="1" CssClass="button-value" />
                <asp:Button ID="btnAccessories" runat="server" Text="Phụ kiện cây thông" OnClick="FilterProducts" CommandArgument="2" CssClass="button-value" />
                <asp:Button ID="btnGifts" runat="server" Text="Các món quà" OnClick="FilterProducts" CommandArgument="3" CssClass="button-value" />
                <asp:Button ID="btnCakes" runat="server" Text="Bánh ngọt" OnClick="FilterProducts" CommandArgument="4" CssClass="button-value" />
                <asp:Button ID="btnClothes" runat="server" Text="Quần áo" OnClick="FilterProducts" CommandArgument="5" CssClass="button-value" />
                 <asp:DropDownList ID="ddlSort" runat="server" CssClass="dropdown-value" OnSelectedIndexChanged="SortProducts" AutoPostBack="true">
                    <asp:ListItem Text="Sắp xếp giá giảm dần" Value="priceReduction" />
                    <asp:ListItem Text="Sắp xếp giá tăng dần" Value="priceIncrease" />
                    
                    <asp:ListItem Text="Sắp xếp từ A-Z" Value="az" />
                    <asp:ListItem Text="Sắp xếp từ Z-A" Value="za" />
                    <asp:ListItem Text="Sản phẩm bán chạy" Value="bestSeller" />
                </asp:DropDownList>
             </div>
        
            <!-- Product container -->
            <div class="products">
                <div class="product">
                    <% 
                        if (products != null)
                        {
                            foreach (var product in products)
                            { 
                    %>
                         <article class="shop__card" onclick="showProductDetails('<%= product.ProductId %>', '<%= product.Img %>', '<%= product.Name %>', '<%= product.Price %>', '<%= product.Description %>')">
                             <img src="<%= product.Img %>" alt="image" class="shop__img" />
                             <p id="productId" style="display: none;"> <%= product.ProductId %></p>
   
                            <h3 class="shop__title">
                                <%= product.Name %>
                            </h3>
                            <span class="shop__price"><%= product.Price %> ₫</span>
                            <button class="shop__button">
                                <i class="ri-shopping-bag-line"></i>
                            </button>
                        </article>
                    <% 
                            }
                        }
                    %>
                </div>
            </div>
            <div class="overlay">
                <div class="overlay-container">
                    <button class="close-button">&times;</button>
                    <div class="selected__product">
                        <img src="assets/img/Christmas Tree Ornaments 1.webp" alt="" class="selected__product-img">
                        <div class="selected__product-description">
                            <h2 class="selected__product-name">Christmas tree</h2>
                            <h3 class="selected__product-price">10.000₫</h3>
                            <span class="selected__product-detail">
                                <i class="ri-car-fill"></i>
                                Đơn hàng sẽ được vận chuyển trong thời gian sớm nhất!
                            </span>
                            <span class="selected__product-descrip">This is as concise as it gets while maintaining essential details!</span>
                            <span class="selected__product-origin">Origin: Viet Nam</span>
                            <asp:HiddenField ID="hiddenProductId" runat="server" />

                            <span class="productIdOverLay" style="display: none;"></span>
                            
                            <div class="selected__quantity">
                                <input type="number" id="quantityInput" runat="server" value="1" min="1" />
                            </div>

                            <div class="selected__buttons">
                                
                                <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" OnClick="AddToCart_Click" CssClass="add-cart" />

                                <button class="buy">Buy now</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
        </section>

        <script>
            
            function showProductDetails(id, img, name, price, description) {
                // Cập nhật thông tin sản phẩm
                document.querySelector('.selected__product-img').src = img;
                document.querySelector('.selected__product-name').innerText = name;
                document.querySelector('.selected__product-price').innerText = price + " VNĐ";
                document.querySelector('.selected__product-descrip').innerText = description;
                document.getElementById('<%= hiddenProductId.ClientID %>').value = id;
               
                // Mở overlay
                document.querySelector('.overlay').style.display = 'flex';
            }

            // Đóng overlay mà không reload trang
            document.querySelector('.close-button').addEventListener('click', function (event) {
                event.preventDefault();  // Ngăn ngừa hành động mặc định của sự kiện (postback)
                document.querySelector('.overlay').style.display = 'none';
            });
        </script>

         <style>
        /* For small devices */
        @media screen and (max-width:340px) {
            
        }

        /* For medium devices */
        @media screen and (max-width: 576px){
           .button-value {
               font-size:0.6rem;
               margin:0;
           }

           #buttons{
             
               margin: 20px 0 20px;
               max-width:500px;
               flex-wrap:wrap
           }

           .shop__card {
               width:130px;
              
           }

           .shop__title {
               font-size:0.7rem;
           }
        }
    </style>
      
    </form>
</asp:Content>
