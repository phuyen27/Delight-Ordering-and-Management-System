<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FAQ.aspx.cs" Inherits="DelightShop.User.FAQ" MasterPageFile="~/User/HeaderFooter.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" Runat="server">
    <body>
         <form id="form1" runat="server">
            <div class="section container grid">
                <div class="faq-container">
                    <h1>
                        DELIGHT SHOP
                    </h1>
                    <div class="faq-header">
                        <div class="faq-title">
                            <div class="faq-help">Xin chào, chúng tôi có thể giúp gì cho bạn?</div>
                            <div class="faq-search">
                                <input type="text" placeholder="Nhập từ khóa hoặc nội dung cần tìm" id="searchInput">
                                <button id="searchButton">Tìm kiếm</button>
                            </div>
                        </div>
                        <div class="faq-img">
                            <img src="/assets/img/delight_faq.png" alt="" class="faq-img__item">
                        </div>
                    </div>

                    <h3>Danh mục</h3>
                    <div class="faq-category">
                        <div class="category-item">
                            <i class="category-icon icon1 ri-store-2-fill"></i>
                            <span class="category-title">Mua sắm cùng Delight</span>
                        </div>
                        <div class="category-item">
                            <i class="category-icon icon2 ri-discount-percent-fill"></i>
                            <span class="category-title">Khuyến mãi & giảm giá</span>
                        </div>
                        <div class="category-item">
                            <i class="category-icon icon3 ri-wallet-3-fill"></i>
                            <span class="category-title">Thanh toán</span>
                        </div>
                        <div class="category-item">
                            <i class="category-icon icon1 ri-arrow-up-down-line"></i>
                            <span class="category-title">Lỗi & hoàn tiền</span>
                        </div>                   
                    </div>

                    <div class="faq-section">
                        <h3>Câu hỏi thường gặp</h3>
                        <ul id="faqList">
                            <li class="faq">Tôi có được hoàn trả sản phẩm không?
                                <i class="ri-arrow-down-wide-line"></i>
                                <ul class="faq-list" style="display:none;">
                                    <li class="faq-item">Bạn sẽ được hoàn trả lại khi sản phẩm còn nguyên nhãn</li>
                                </ul>
                            </li>
                            <li class="faq">Hãy hướng dẫn tôi cách thanh toán đơn hàng
                                <i class="ri-arrow-down-wide-line"></i>
                                <ul class="faq-list" style="display:none;">
                                    <li class="faq-item">Nhấn vào link để xem hướng dẫn</li>
                                </ul>
                            </li>
                            <li class="faq">Cách kiểm tra tình trạng sản phẩm
                                <i class="ri-arrow-down-wide-line"></i>
                                <ul class="faq-list" style="display:none;">
                                    <li class="faq-item">Nhấn vào link để xem hướng dẫn</li>
                                </ul>
                            </li>
                            <li class="faq">Chính sách hoàn trả sách
                                <i class="ri-arrow-down-wide-line"></i>
                                <ul class="faq-list" style="display:none;">
                                    <li class="faq-item">Nhấn vào link để xem chính sách</li>
                                </ul>
                            </li>
                        </ul>
                    </div>

                     <div class="user_faq">
                         <div class="faqcomment_list">
                                <% 
                                foreach (var item in FAQitem) { 
                                %>
                                    <div class="faq_item">
                                        <img src="<%: item.avtCustomer %>" alt="Customer Avatar" class="faq-img__item">
                                        <span class="faq_comment"><%: item.comment %></span>
                                        <p class="fad_date"><%: item.dateFAQ %></p>
                                    </div>
                                <% 
                                } 
                                %>
                            </div>
                         <div class="post_faq">
                                <asp:TextBox ID="txtQuestion" runat="server" TextMode="MultiLine" Rows="4" Columns="50" Placeholder="Nhập câu hỏi của bạn" required="true" CssClass="userQuestion"></asp:TextBox>
                                <asp:Button ID="btnSubmit" runat="server" Text="Gửi câu hỏi" OnClick="btnSubmit_Click" CssClass="button_post" />
                         </div>
                     </div>
                </div>   
            </div>
        </form>
       
    </body>
     <script>
         // Toggle FAQ answer visibility
         const faqs = document.querySelectorAll('.faq');
         faqs.forEach(faq => {
             faq.addEventListener('click', () => {
                 const faqList = faq.querySelector('.faq-list');
                 const icon = faq.querySelector('i');
                 faqList.style.display = faqList.style.display === 'none' ? 'block' : 'none';
                 icon.classList.toggle('ri-arrow-up-line');
                 icon.classList.toggle('ri-arrow-down-wide-line');
             });
         });

         // Search functionality
         document.getElementById('searchButton').addEventListener('click', function () {
             const query = document.getElementById('searchInput').value.toLowerCase();
             const faqItems = document.querySelectorAll('.faq');
             faqItems.forEach(faq => {
                 const question = faq.innerText.toLowerCase();
                 faq.style.display = question.includes(query) ? 'block' : 'none';
             });
         });
     </script>
</asp:Content>
