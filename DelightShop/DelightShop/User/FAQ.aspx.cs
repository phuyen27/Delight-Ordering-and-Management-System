using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DelightShop.User
{
    public partial class FAQ : System.Web.UI.Page
    {
        protected List<Customer.FAQ> FAQitem;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CustomerID"] == null)
            {
                Response.Redirect("/Signin.aspx");
                return;
            }
            if (!IsPostBack)
            {
                FAQitem = Customer.GetFAQs();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string userQuestion = txtQuestion.Text;

            int customerID = (int)Session["CustomerID"];
            Customer.InsertFAQ(customerID, userQuestion);
            FAQitem = Customer.GetFAQs();
        }
    }
}