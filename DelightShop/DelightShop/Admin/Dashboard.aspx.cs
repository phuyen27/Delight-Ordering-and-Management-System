using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DelightShop.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected List<order> orderItems;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblTotalCustomer.Text = admin.GetDataFunction("SELECT dbo.TinhTongKhachHang()").ToString();
                lblTotal.Text = admin.GetDataFunction("SELECT dbo.TinhTongDoanhThu()").ToString("C2");
                lblTotalOrders.Text = admin.GetDataFunction("SELECT dbo.TinhTongSoLuongDonDatHang()").ToString()+"";

                orderItems = order.getOrderAll();
            }

        }
    }
}