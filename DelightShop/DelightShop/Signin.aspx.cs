using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DelightShop
{
    public partial class Signin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];

            // Kiểm tra thông tin đăng nhập
            Customer customer = Customer.GetCustomer(username, password);

            if (customer != null)
            {
                // Lưu thông tin người dùng vào session
                Session["CustomerID"] = customer.CustomerID;
                Session["Username"] = customer.Username;
                Session["Password"] = customer.Password;
                // Chuyển hướng đến trang chủ của người dùng
                Response.Redirect("~/User/HomePage.aspx");
            }

            else
            {
                // Nếu đăng nhập thất bại, thông báo lỗi
                Response.Write("<script>alert('Sai tên đăng nhập hoặc mật khẩu!');</script>");
            }

        }

    }
}