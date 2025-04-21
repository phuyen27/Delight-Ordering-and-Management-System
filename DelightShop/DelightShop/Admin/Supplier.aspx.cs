using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DelightShop.Admin
{
    public partial class Supplier : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string keyword = Request.QueryString["search"];
                if (!string.IsNullOrEmpty(keyword))
                {
                    var result = DepartmentsAndSuppliers.SearchSuppliersByName(keyword);
                    gvSuppliers.DataSource = result;
                    gvSuppliers.DataBind();
                }
                else
                {
                    BindData();
                }
            }
        }


        private void BindData()
        {
            List<DepartmentsAndSuppliers.Supplier> suppliers = DepartmentsAndSuppliers.getAllSuppliers();
            gvSuppliers.DataSource = suppliers;
            gvSuppliers.DataBind();
        }
        protected void btnAddSupplier_Click(object sender, EventArgs e)
        {


            String supplierName = txtSupplierName.Text.Trim();
            String supplierAddress = txtSupplierAddress.Text.Trim();
            String supplierEmail = txtSupplierEmail.Text.Trim();
            String supplierPhone = txtSupplierPhone.Text.Trim();
            String supplierWebsite = txtSupplierWebsite.Text.Trim();


            // string name, string address, string phone, string email, string website
            DepartmentsAndSuppliers.insertSupplier(supplierName, supplierAddress, supplierPhone, supplierEmail, supplierWebsite);

            // Làm mới danh sách
            BindData();
        }
        protected void gvSuppliers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSuppliers.EditIndex = e.NewEditIndex;
            BindData();
        }
        protected void gvSuppliers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int supplierID = Convert.ToInt32(gvSuppliers.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvSuppliers.Rows[e.RowIndex];

            string name = ((TextBox)row.Cells[1].Controls[0]).Text.Trim();
            string address = ((TextBox)row.Cells[2].Controls[0]).Text.Trim();
            string email = ((TextBox)row.Cells[3].Controls[0]).Text.Trim();
            string phone = ((TextBox)row.Cells[4].Controls[0]).Text.Trim();
            string website = ((TextBox)row.Cells[5].Controls[0]).Text.Trim();
            DepartmentsAndSuppliers.updateSupplier(supplierID, name, address, phone, email, website);

            gvSuppliers.EditIndex = -1;
            BindData();
        }
        protected void gvSuppliers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int employeeID = Convert.ToInt32(gvSuppliers.DataKeys[e.RowIndex].Value);
                admin.DeleteItems(employeeID, "DELETE FROM NhaCungCap WHERE MaNCC = @employeeID", "@employeeID");
                BindData();
                string script = "alert('Xóa thông tin nhà cung cấp thành công!');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                gvSuppliers.EditIndex = -1;
                BindData();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Xóa thất bại');</script>");
            }
        }
        protected void gvSuppliers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSuppliers.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void gvSuppliers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSuppliers.EditIndex = -1; // Hủy chế độ chỉnh sửa
            BindData();            // Load lại dữ liệu
        }

        protected void btnCancelSupplier_Click(object sender, EventArgs e)
        {
            pnlAddEditSupplier.Visible = false; // Ẩn form chỉnh sửa
        }

        protected void btnUpdateSupplier_Click(object sender, EventArgs e)
        {
            int supplierID = int.Parse(txtEditSupplierID.Text);
            string name = txtEditSupplierName.Text;
            string address = txtEditSupplierAddress.Text;
            string email = txtEditSupplierEmail.Text;
            string phone = txtEditSupplierPhone.Text;
            string website = txtEditSupplierWebsite.Text;


            //// TODO: Gọi logic cập nhật thực tế, ví dụ:
            DepartmentsAndSuppliers.updateSupplier(supplierID, name, address, phone, email, website);

            //// Ẩn panel chỉnh sửa và refresh danh sách
            pnlAddEditSupplier.Visible = false;
            BindData(); // Load lại danh sách
        }

    }

}