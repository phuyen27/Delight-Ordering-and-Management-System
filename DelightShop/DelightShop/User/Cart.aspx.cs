using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static DelightShop.Customer;

namespace DelightShop.User
{
    public partial class Cart : System.Web.UI.Page
    {
        protected List<cart.CartItem> cartItems;
        protected decimal totalAmount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CustomerID"] == null)
            {
                Response.Redirect("/Signin.aspx");
                return;
            }

            int customerId = (int)Session["CustomerID"];

            if (!IsPostBack)
            {
                cartItems = cart.GetCartItems(customerId);
                totalAmount = 0;
                foreach (var item in cartItems)
                {
                    totalAmount += item.Quantity * item.Price;
                }
            }
        }

        protected void btnXoa_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int productId = Convert.ToInt32(btn.Attributes["data-product-id"]); // Lấy productID từ button
            int customerId = (int)Session["CustomerID"];

            cartItems = cart.GetCartItems(customerId);

            int cartId = Convert.ToInt32(Request.QueryString["cartId"]);

            cart.DeleteProductFromCart(cartId, productId);

            Response.Write($"<script>alert('Sản phẩm đã được xóa khỏi giỏ hàng.');</script>");

        }


        protected void Checkout_Click(object sender, EventArgs e)
        {
            int customerId = (int)Session["CustomerID"];
            int cartId = Convert.ToInt32(Request.QueryString["cartId"]);

            DateTime ngayDat = DateTime.Now;
            decimal total = Convert.ToDecimal(Request.Form["hiddenTotalAmount"]);

            var selectedProducts = new List<int>();
            var quantities = new List<int>();

            int maDH = order.InsertDonDH(customerId, total, ngayDat, "Processing");
            Session["maDH"] = maDH;

            cartItems = cart.GetCartItems(customerId);

            foreach (var item in cartItems)
            {
                var checkbox = Request.Form["checkbox-" + item.ProductID];
                if (checkbox != null) 
                {
                    selectedProducts.Add(item.ProductID);
                    int quantity = Convert.ToInt32(Request.Form["quantity-" + item.ProductID]);
                    quantities.Add(quantity);
                }
            }

            if (selectedProducts.Count == 0)
            {
                Response.Write("<script>alert('Vui lòng chọn sản phẩm để thanh toán.');</script>");
                return;
            }

           
            for (int i = 0; i < selectedProducts.Count; i++)
            {
                int maSP = selectedProducts[i];
                int soLuong = quantities[i];
                order.InsertChiTietDH(maDH, maSP, soLuong);
                cart.DeleteProductFromCart(cartId, maSP);
            }

            Response.Redirect("Payment.aspx");
        }

    }
}