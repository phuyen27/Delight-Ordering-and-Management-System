using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DelightShop.Admin
{
    public partial class OrderDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindOrderDetailData();
        }

        private void BindOrderDetailData()
        {
            List<order.orderDetail> orderDetails = order.GetAllChiTietDH();
            gvOrderDetails.DataSource = orderDetails;
            gvOrderDetails.DataBind();
        }


        protected void gvOrderDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvOrderDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvOrderDetails.EditIndex = e.NewEditIndex;
            BindOrderDetailData();
            GridViewRow row = gvOrderDetails.Rows[e.NewEditIndex];
            int orderDetailID = Convert.ToInt32(gvOrderDetails.DataKeys[e.NewEditIndex].Value);
            txtOrderDetailID.Text = orderDetailID.ToString();
        }

        protected void gvOrderDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int orderDetailID = Convert.ToInt32(gvOrderDetails.DataKeys[e.RowIndex].Value);
            admin.DeleteItems(orderDetailID, "DELETE FROM ChiTietDH WHERE MaCTDH = @orderDetailID", "@orderDetailID");
            BindOrderDetailData();
           
        }

        private void btnSaveDetail_Click(object sender, EventArgs e)
        {
            int orderDetailID;
            if (!int.TryParse(txtOrderDetailID.Text, out orderDetailID))
            {
                Response.Write("<script>alert('Order Detail ID không hợp lệ!');</script>");
                return;
            }

            try
            {
                int quantity = Convert.ToInt32(txtQuantity.Text);
                decimal price = Convert.ToDecimal(txtPrice.Text);

                order.UpdateChiTietDH(orderDetailID, quantity, price);
            }
            catch (Exception ex)
            {
                // Thông báo lỗi nếu có ngoại lệ
                string script = "alert('Lỗi: " + ex.Message + "');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                pnAddEditDetail.Visible = false;
                BindOrderDetailData();
                
            }
        }

        protected void gvOrderDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvOrderDetails.EditIndex = -1;
            BindOrderDetailData();
            
        }

        protected void gvOrderDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int orderDetailID = Convert.ToInt32(gvOrderDetails.DataKeys[e.RowIndex].Value);

            GridViewRow row = gvOrderDetails.Rows[e.RowIndex];

            TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
            TextBox txtPrice = (TextBox)row.FindControl("txtPrice");
            TextBox txtOrderIDDetail = (TextBox)row.FindControl("txtOrderIDDetail");

            int quantity = Convert.ToInt32(txtQuantity.Text);
            decimal price = Convert.ToDecimal(txtPrice.Text);


            try
            {
                order.UpdateChiTietDH(orderDetailID, quantity, price);
                string script = "alert('Cập nhật thông tin chi tiết đơn hàng thành công!');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                gvOrderDetails.EditIndex = -1;
                BindOrderDetailData();
               
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Cập nhật thất bại');</script>");
            }
        }

        protected void btnCancelDetail_Click(object sender, EventArgs e)
        {
            pnAddEditDetail.Visible = false;
        }

        protected void btnAdd_Detail(object sender, EventArgs e)
        {
            int orderID = Convert.ToInt32(EditMaDH.Text);
            int productID = Convert.ToInt32(EditMaSP.Text);
            int quatiry = Convert.ToInt32(EditSoLuong.Text);
            try
            {
                order.InsertChiTietDH(orderID, productID, quatiry);
                string script = "alert('Thêm thông tin đơn hàng thành công!');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                BindOrderDetailData();
               
            }
            catch (Exception ex)
            {
                string script = $"alert('Có lỗi xảy ra: {ex.Message}');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
        }


    }
}