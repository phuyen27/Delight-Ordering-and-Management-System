using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DelightShop.Admin
{
    public partial class CustomerManagement : System.Web.UI.Page
    {
        protected List<Customer> customers;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string keyword = Request.QueryString["search"];
                if (!string.IsNullOrEmpty(keyword))
                {
                    customers = Customer.GetCustomersByNameSearch(keyword);
                }
                else
                {
                    loadData();
                }
            }
        }


        protected void loadData()
        {
            lblTotalFemale.Text = admin.GetDataFunction("SELECT dbo.TinhTongKhachHangFemale()").ToString();
            lblTotalMale.Text = admin.GetDataFunction("SELECT dbo.TinhTongKhachHangMale()").ToString();

            customers = Customer.GetCustomerAll();

        }

       
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            customers = Customer.GetCustomerAll();

            int customerId = int.Parse(CustomerID.Value);
            string firstName = HoKH.Value;
            string lastName = TenKH.Value;
            string email = EmailKH.Value;
            string dob = NgaySinhKH.Value;
            string phoneNumber = SDTKH.Value;
            string address = DiaChi.Value;
            string gender = GioiTinh.Value;

            try
            {
                Customer.UpdateCustomer(customerId, firstName, lastName, dob, email, phoneNumber, address,gender);

                string script = "alert('Cập nhật thông tin khách hàng thành công!');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

            }
            catch (Exception ex)
            {
                string script = $"alert('Có lỗi xảy ra: {ex.Message}');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
            loadData();

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            customers = Customer.GetCustomerAll();


            string firstName = HoKH.Value;
            string lastName = TenKH.Value;
            string email = EmailKH.Value;
            string dob = NgaySinhKH.Value;
            string phoneNumber = SDTKH.Value;
            string address = DiaChi.Value;
            string gender = GioiTinh.Value;

            string password = "123";
            string avt = "/assets/img/user_Add.png";

            try
            {
                Customer.InsertCustomer (firstName, lastName, dob, email, phoneNumber, address,password,avt,gender);

                string script = "alert('Thêm thông tin khách hàng thành công!');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
            catch (Exception ex)
            {
                string script = $"alert('Có lỗi xảy ra: {ex.Message}');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
            loadData();
        }

        protected void txtTimKiemKH_TextChanged(object sender, EventArgs e)
        {
            customers = Customer.GetCustomerAll();
            string searchQuery = txtTimKiemKH.Text.Trim(); 

            if (!string.IsNullOrEmpty(searchQuery))
            {
                 customers = Customer.GetCustomersByNameSearch(searchQuery);

            }
            else
            {
                customers = Customer.GetCustomerAll();
            }
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try {
                customers = Customer.GetCustomerAll();
                int customerId = int.Parse(CustomerID.Value);
                admin.DeleteItems(customerId, "DELETE FROM KhachHang WHERE MaKH = @customerId", "@customerId");

                string script = "alert('Xóa thông tin khách hàng thành công!');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
            catch (Exception ex)
            {
                string script = $"alert('Có lỗi xảy ra: {ex.Message}');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
            loadData();
        }
    }
}