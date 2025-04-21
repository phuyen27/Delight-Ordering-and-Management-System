using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static DelightShop.Customer;

namespace DelightShop.User
{
    public partial class UserInfor : System.Web.UI.Page
    {
        protected Customer customer;
        protected List<order> orderItems;
        protected List<order.orderDetail> orderDetailItems;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["CustomerID"] == null)
            {
                Response.Redirect("/Signin.aspx");
                return;
            }

            int customerID = (int)Session["CustomerID"];
            string customerEmail = (string)Session["Username"];
            string customerPass = (string)Session["Password"];

            if (!IsPostBack)
            {
                customer = Customer.GetCustomer(customerEmail, customerPass);
                orderItems = order.getOrder(customerID);

                orderRepeater.DataSource = orderItems;
                orderRepeater.DataBind();
            }
        }

        protected void ViewOrderDetails_Click(object sender, EventArgs e)
        {
            string customerEmail = (string)Session["Username"];
            string customerPass = (string)Session["Password"];
            customer = Customer.GetCustomer(customerEmail, customerPass);
            Button btn = sender as Button;
            if (btn != null && !string.IsNullOrEmpty(btn.CommandArgument))
            {
                int orderID = Convert.ToInt32(btn.CommandArgument);

                var orderDetails = order.getOrderDetail(orderID);
                orderItemsRepeater.DataSource = orderDetails;
                orderItemsRepeater.DataBind();

                openOverlay();

                if (orderDetails != null && orderDetails.Count > 0)
                {
                    orderItemsRepeater.DataSource = orderDetails;
                    orderItemsRepeater.DataBind();

                    openOverlay();
                }
                else
                {
                    Response.Write("<script>alert('No order details found.');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid order ID.');</script>");
            }
        }


        private void openOverlay()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showOverlay", "document.querySelector('.overlay').style.display = 'flex';", true);
        }
    }
}