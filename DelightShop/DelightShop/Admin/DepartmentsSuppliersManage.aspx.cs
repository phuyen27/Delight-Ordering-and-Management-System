using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DelightShop.Admin
{
    public partial class DepartmentsSuppliersManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDepartmentData();
                BingSupplierData();
            }
        }

        private void BindDepartmentData()
        {
            List<DepartmentsAndSuppliers.Department> departments = DepartmentsAndSuppliers.getAllDepartments();
            gvDepartments.DataSource = departments;
            gvDepartments.DataBind();
        }

        private void BingSupplierData()
        {
            List<DepartmentsAndSuppliers.Supplier> suppliers = DepartmentsAndSuppliers.getAllSuppliers();
            gvSuppliers.DataSource = suppliers;
            gvSuppliers.DataBind();
        }
        protected void gvDepartments_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvDepartments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvDepartments_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvDepartments_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvSuppliers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvSuppliers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvSuppliers_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvSuppliers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
    }
}