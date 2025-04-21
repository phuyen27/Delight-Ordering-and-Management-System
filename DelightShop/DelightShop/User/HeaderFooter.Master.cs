using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static DelightShop.Customer;

namespace DelightShop.User
{
    public partial class HeaderFooter : System.Web.UI.MasterPage
    {

        protected List<cart.CartItem> cartItems;
        protected int cartQuantity = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

    }
}