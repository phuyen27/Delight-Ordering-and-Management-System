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

            // 👉 Kiểm tra thông tin đăng nhập admin trước
            if (username == "admindelight@gmail.com" && password == "admin123")
            {
                // Lưu thông tin admin vào session (tuỳ bạn dùng hoặc không)
                Session["IsAdmin"] = true;
                Session["Username"] = username;

                // 👉 Chuyển đến trang dashboard trong thư mục Admin
                Response.Redirect("~/Admin/Dashboard.aspx");
                return;
            }

            // 👉 Kiểm tra người dùng thường
            Customer customer = Customer.GetCustomer(username, password);

            if (customer != null)
            {
                Session["CustomerID"] = customer.CustomerID;
                Session["Username"] = customer.Username;
                Session["Password"] = customer.Password;

                Response.Redirect("~/User/HomePage.aspx");
            }
            else
            {
                Response.Write("<script>alert('Sai tên đăng nhập hoặc mật khẩu!');</script>");
            }
        }


    }
}