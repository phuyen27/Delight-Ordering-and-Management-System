using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DelightShop.Admin
{
    public partial class ReceiptManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                LoadEmployeeIDs();
            }
        }

        private void BindData()
        {
            List<order.receipt> receipts = order.getReceiptsAll();
            gvReceipts.DataSource = receipts;
            gvReceipts.DataBind();
        }

        private void LoadEmployeeIDs()
        {
            List<int> employeeIDs = employee.GetAllEmployeeIDs(); // Gọi hàm bạn đã viết

            ddlReceiptStaff.DataSource = employeeIDs;
            ddlReceiptStaff.DataBind();

            ddlReceiptStaff.Items.Insert(0, new ListItem("-- Chọn nhân viên --", "0")); // Tùy chọn mặc định

            List<int> supplierID = DepartmentsAndSuppliers.GetAllSupplierIDs(); // Gọi hàm bạn đã viết

            ddlsupplierID.DataSource = supplierID;
            ddlsupplierID.DataBind();

            ddlsupplierID.Items.Insert(0, new ListItem("-- Chọn nhà cung cấp --", "0")); // Tùy chọn mặc định

        }

        protected void gvReceipts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvReceipts.EditIndex = -1;      
        }


        protected void gvReceipts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReceipts.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void gvReceipts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvReceipts.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void gvReceipts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int receiptID = Convert.ToInt32(gvReceipts.DataKeys[e.RowIndex].Value);

            GridViewRow row = gvReceipts.Rows[e.RowIndex];

            DateTime receiptDate = DateTime.Parse(((TextBox)row.Cells[1].Controls[0]).Text);
            decimal totalPrice = decimal.Parse(((TextBox)row.Cells[2].Controls[0]).Text);

            int staffID = int.Parse(((TextBox)row.Cells[3].Controls[0]).Text); 
            int supplierId = int.Parse(((TextBox)row.Cells[4].Controls[0]).Text);
            try
            {
              
                order.UpdateReceipt(receiptID, receiptDate, totalPrice, staffID, supplierId);

                gvReceipts.EditIndex = -1;
                BindData();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cập nhật thành công');", true);
            }
            catch (Exception ex)
            {
                lblError.Text = "Cập nhật thất bại: " + ex.Message;
            }
        }

        protected void gvReceipts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int receiptID = Convert.ToInt32(gvReceipts.DataKeys[e.RowIndex].Value);

            try
            {
                
                admin.DeleteItems(receiptID, "DELETE FROM PhieuNhapHang WHERE MaPNH = @receiptID", "@receiptID");
                BindData();
                string script = "alert('Xóa thông tin phiếu nhập thành công!');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                gvReceipts.EditIndex = -1;
                BindData();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Xóa thất bại');</script>");
            }

        }

        protected void btnUpdateReceipt_Click(object sender, EventArgs e)
        {


        }

        protected void btnCancelReceipt_Click(object sender, EventArgs e)
        {
            gvReceipts.EditIndex = -1;
            BindData();
        }

        protected void btnAddReceipt_Click(object sender, EventArgs e)
        {
            lblError.Text = ""; // Clear lỗi cũ

            decimal tongTien;
            if (!decimal.TryParse(txtReceiptTotalPrice.Text, out tongTien))
            {
                lblError.Text = "Tổng tiền không hợp lệ!";
                return;
            }

            int maNV = Convert.ToInt32(ddlReceiptStaff.SelectedValue);
            int maNCC = Convert.ToInt32(ddlsupplierID.SelectedValue);
            DateTime ngayNhap;

            if (!DateTime.TryParse(txtReceiptDate.Text, out ngayNhap))
            {
                lblError.Text = "Ngày nhập không hợp lệ!";
                return;
            }

            try
            {
                order.InsertReceipt(ngayNhap, tongTien, maNV, maNCC);

                string script = "alert('Thêm phiếu nhập thành công!');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                gvReceipts.EditIndex = -1;
                BindData();
            }
            catch (Exception ex)
            {
                lblError.Text = "Thêm thất bại: " + ex.Message;
            }
        }

    }
}