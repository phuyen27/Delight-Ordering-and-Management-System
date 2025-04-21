using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace DelightShop.User
{
    public partial class Shop : System.Web.UI.Page
    {
        protected List<Product> products;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Kiểm tra nếu có tham số categoryId trong URL (nếu cần)
            if (!IsPostBack)
            {
                // Chỉ lấy danh sách sản phẩm khi trang lần đầu tiên được load
                products = classProduct.GetProducts();
            }
        }

        // Phương thức lọc sản phẩm khi nhấn nút Filter
        protected void FilterProducts(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int categoryId = int.Parse(clickedButton.CommandArgument);

            // Lấy sản phẩm theo categoryId, nếu categoryId = 0 thì lấy tất cả sản phẩm
            if (categoryId == 0)
            {
                products = classProduct.GetProducts();
            }
            else
            {
                products = classProduct.GetProductsWithCategory(categoryId);
            }
        }

        // Phương thức tìm kiếm sản phẩm khi nhấn nút Search
        protected void SearchProduct(object sender, EventArgs e)
        {
            string searchQuery = searchinput.Value; // Lấy giá trị tìm kiếm từ ô input

            // Kiểm tra nếu người dùng nhập từ khóa tìm kiếm
            if (!string.IsNullOrEmpty(searchQuery))
            {
                products = classProduct.GetProductsBySearch(searchinput.Value); // Lọc sản phẩm theo tên
            }
            else
            {
                // Nếu không có từ khóa tìm kiếm, hiển thị tất cả sản phẩm
                products = classProduct.GetProducts();
            }
        }

        // Phương thức thêm sản phẩm vào giỏ hàng
        protected void AddToCart_Click(object sender, EventArgs e)
        {
            try
            {
                int productId;
                if (!int.TryParse(hiddenProductId.Value, out productId))
                {
                    Response.Write("<script>alert('Mã sản phẩm không hợp lệ.');</script>");
                    return;
                }

                // Lấy customerId từ session
                int customerId = 0;
                if (Session["CustomerId"] != null)
                {
                    customerId = int.Parse(Session["CustomerId"].ToString());
                }
                else
                {
                    Response.Write("<script>alert('Bạn cần đăng nhập để thêm sản phẩm vào giỏ hàng.');</script>");
                    return;
                }

                int quantity;
                string quantityInputValue = quantityInput.Value; // Lấy giá trị từ input số lượng

                // Kiểm tra nếu giá trị là hợp lệ (là một số nguyên)
                if (!int.TryParse(quantityInputValue, out quantity) || quantity < 1)
                {
                    Response.Write("<script>alert('Số lượng không hợp lệ.');</script>");
                    return;
                }


                // Gọi hàm InsertCartItem để thêm sản phẩm vào giỏ hàng
                classProduct.InsertCartItem(customerId, productId, quantity);

                // Thông báo thành công
                Response.Write("<script>alert('Sản phẩm đã được thêm vào giỏ hàng!');</script>");
                products = classProduct.GetProducts();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Đã xảy ra lỗi: " + ex.Message + "');</script>");
                products = classProduct.GetProducts();
            }
        }

        protected void SortProducts(object sender, EventArgs e)
        {
            string sortOption = ddlSort.SelectedValue; // ddlSort là DropDownList của bạn

            switch (sortOption)
            {
                case "priceReduction":
                    products = classProduct.GetSortedByPrice("desc");
                    break;

                case "priceIncrease":
                    products = classProduct.GetSortedByPrice("asc");
                    break;

                case "az":
                    products = classProduct.GetSortedByName("AtoZ");
                    break;
                case "za":
                    products = classProduct.GetSortedByName("ZtoA");
                    break;
                case "bestSeller":
                    products = classProduct.GetProductsBySales();
                    break;
                default:
                    break;
            }
        }
    }
}