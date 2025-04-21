using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

namespace DelightShop.Admin
{
    public partial class ManageOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOrderData();
                statusOrder();
                
            }

        }

        private void BindOrderData()
        {
            List<order> orders = order.getOrderAll();
            gvOrders.DataSource = orders;
            gvOrders.DataBind();
        }

       
        private void statusOrder()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT TrangThai, COUNT(*) AS SoLuong
                    FROM DonDH
                    WHERE TrangThai IN ('Processing', 'Pending', 'Completed')
                    GROUP BY TrangThai;
                ";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string trangThai = reader["TrangThai"].ToString();
                    int soLuong = Convert.ToInt32(reader["SoLuong"]);

                    if (trangThai == "Processing")
                    {
                        lblProcess.Text = "Processing: " + soLuong.ToString();
                    }
                    else if (trangThai == "Pending")
                    {
                        lblPending.Text = "Pending: " + soLuong.ToString();
                    }
                    else if (trangThai == "Completed")
                    {
                        lblComplete.Text = "Completed: " + soLuong.ToString();
                    }
                }
                reader.Close();
            }
        }

        protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label lblOrderDate = (Label)e.Row.FindControl("lblOrderDate");
            //    if (lblOrderDate != null)
            //    {
            //        DateTime orderDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "orderDate"));
            //        lblOrderDate.Text = orderDate.ToString("dd/MM/yyyy");
            //    }
            //}
        }


      
        // Sự kiện xử lý khi nhấn nút "Edit"
        protected void gvOrders_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvOrders.EditIndex = e.NewEditIndex;
            BindOrderData();

            GridViewRow row = gvOrders.Rows[e.NewEditIndex];

            int orderID = Convert.ToInt32(gvOrders.DataKeys[e.NewEditIndex].Value);
            txtOrderID.Text = orderID.ToString();
        }

       
        // Sự kiện xử lý khi nhấn nút "Delete"
        protected void gvOrders_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int orderID = Convert.ToInt32(gvOrders.DataKeys[e.RowIndex].Value);
            admin.DeleteItems(orderID, "DELETE FROM DonDH WHERE MaDH = @orderID", "@orderID");
            BindOrderData();
        }

       
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int orderID;
            if (!int.TryParse(txtOrderID.Text, out orderID))
            {
                Response.Write("<script>alert('Order ID không hợp lệ!');</script>");
                return;
            }

            DateTime orderDate = Convert.ToDateTime(txtOrderDate.Text);
            decimal total = Convert.ToDecimal(txtTotal.Text);
            string status = ddlStatus.SelectedValue;
            int customerID = Convert.ToInt32(txtCustomerID.Text);

            bool isUpdated = order.UpdateDonDH(orderID, customerID, total, orderDate, status);
            if (isUpdated)
            {
                pnlAddEdit.Visible = false;
                BindOrderData(); 
            }
        }

      
        protected void gvOrders_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvOrders.EditIndex = -1; // Hủy chế độ chỉnh sửa của dòng hiện tại
            BindOrderData(); 
        }

       


        protected void gvOrders_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int orderID = Convert.ToInt32(gvOrders.DataKeys[e.RowIndex].Value);

            GridViewRow row = gvOrders.Rows[e.RowIndex];

            TextBox txtOrderDate = (TextBox)row.FindControl("txtOrderDate");
            TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
            DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus");

            DateTime orderDate = Convert.ToDateTime(txtOrderDate.Text);
            decimal total = Convert.ToDecimal(txtTotal.Text);
            string status = ddlStatus.SelectedValue;
            TextBox txtCustomerID = (TextBox)row.FindControl("txtCustomerID");
            int customerID = Convert.ToInt32(txtCustomerID.Text);

            bool isUpdated = order.UpdateDonDH(orderID, customerID, total, orderDate, status);
            if (isUpdated)
            {
                string script = "alert('Cập nhật thông tin đơn hàng thành công!');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                gvOrders.EditIndex = -1;
                BindOrderData();
            }
            else
            {
                Response.Write("<script>alert('Cập nhật thất bại');</script>");
            }
        }

       
        // Sự kiện khi nhấn nút "Cancel"
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlAddEdit.Visible = false;
        }

       


        protected void Add_order_Click(object sender, EventArgs e)
        {
            int customerID = Convert.ToInt32(EditCustomerID.Text);
            decimal total = 0;
            string status = ddlStatus.SelectedValue;
            string dateString = EditDate.Value;
            try
            {
                if (DateTime.TryParse(dateString, out DateTime orderDate) && decimal.TryParse(EditTotal.Text, out total))
                {
                    order.InsertDonDH(customerID, total, orderDate, status);
                    BindOrderData();
                }
                string script = "alert('Thêm thông tin đơn hàng thành công!');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

            }
            catch (Exception ex)
            {
                string script = $"alert('Có lỗi xảy ra: {ex.Message}');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }

        }

       
        protected void ViewOrderDetails_Click(object sender, EventArgs e)
        {
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