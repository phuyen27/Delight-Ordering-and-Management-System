using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DelightShop.Admin
{
    public partial class ManageDepartment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string keyword = Request.QueryString["search"];
                if (!string.IsNullOrEmpty(keyword))
                {
                    var result = DepartmentsAndSuppliers.SearchSuppliersByName(keyword);
                    gvDepartments.DataSource = result;
                    gvDepartments.DataBind();
                }
                else
                {
                    BindData();
                }
            }

        }

        private void BindData()
        {
            List<DepartmentsAndSuppliers.Department> departments = DepartmentsAndSuppliers.getAllDepartments();
            gvDepartments.DataSource = departments;
            gvDepartments.DataBind();
        }

        protected void gvDepartments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int departmentsID = Convert.ToInt32(gvDepartments.DataKeys[e.RowIndex].Value);
                admin.DeleteItems(departmentsID, "DELETE FROM PhongBan WHERE MaPhong = @departmentsID", "@departmentsID");
                BindData();
                string script = "alert('Xóa thông tin phòng ban thành công!');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                gvDepartments.EditIndex = -1;
                BindData();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Xóa thất bại');</script>");
            }
        }

        protected void gvDepartments_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDepartments.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void gvDepartments_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvDepartments.Rows[e.RowIndex];

            int departmentID = Convert.ToInt32(gvDepartments.DataKeys[e.RowIndex].Value);
            string name = ((TextBox)row.Cells[1].Controls[0]).Text;
            string dateString = ((TextBox)row.Cells[2].Controls[0]).Text;
            string quantityStr = ((TextBox)row.Cells[3].Controls[0]).Text;

            try
            {
                DateTime establishedDate = DateTime.Parse(dateString);
                int quantity = int.Parse(quantityStr);

                bool result = DepartmentsAndSuppliers.updateDepartment(departmentID, name, establishedDate, quantity);

                if (result)
                {
                    gvDepartments.EditIndex = -1;
                    BindData();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cập nhật thành công!');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cập nhật thất bại!');", true);
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Dữ liệu không hợp lệ!');", true);
            }
        }

        protected void gvDepartments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDepartments.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void gvDepartments_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDepartments.EditIndex = -1;
            BindData();
        }

        protected void btnAddDepartment_Click(object sender, EventArgs e)
        {
            string name = txtDepartmentName.Text;
            DateTime date;
            int quantity;

            if (DateTime.TryParse(txtDepartmentDate.Text, out date) && int.TryParse(txtDepartmentQuantity.Text, out quantity))
            {
                bool result = DepartmentsAndSuppliers.insertDepartment(name, date, quantity);
                if (result)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Thêm thành công!');", true);
                    txtDepartmentName.Text = "";
                    txtDepartmentDate.Text = "";
                    txtDepartmentQuantity.Text = "";
                    BindData();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Thêm thất bại!');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ngày hoặc số lượng không hợp lệ!');", true);
            }
        }
    }
}