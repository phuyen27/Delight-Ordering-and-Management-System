using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DelightShop.Admin
{
    public partial class ManageCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string keyword = Request.QueryString["search"];
                if (!string.IsNullOrEmpty(keyword))
                {
                    //var result = classProduct.SearchCategoriesByName(keyword);
                    //gvCategories.DataSource = result;
                    //gvCategories.DataBind();
                }
                else
                {
                    BindData();
                }
            }
        }

        private void BindData()
        {
            List<category> suppliers = classProduct.GetCategories();
            gvCategories.DataSource = suppliers;
            gvCategories.DataBind();
        }

        protected void gvCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int categoryID = Convert.ToInt32(gvCategories.DataKeys[e.RowIndex].Value);
                admin.DeleteItems(categoryID, "DELETE FROM LoaiSP WHERE MaLoaiSP = @categoryID", "@categoryID");
                BindData();
                string script = "alert('Xóa thông tin loại sản phẩm thành công!');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                gvCategories.EditIndex = -1;
                BindData();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Xóa thất bại');</script>");
            }
        }

        protected void gvCategories_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCategories.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void gvCategories_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvCategories.Rows[e.RowIndex];

            int categoryID = Convert.ToInt32(gvCategories.DataKeys[e.RowIndex].Value);
            string categoryName = ((TextBox)row.Cells[1].Controls[0]).Text;

            category cat = new category
            {
                categoryID = categoryID,
                categoryName = categoryName
            };

            classProduct.UpdateCategory(cat);

            gvCategories.EditIndex = -1;
            BindData();
        }

        protected void gvCategories_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategories.EditIndex = -1;
            BindData();
        }

        protected void gvCategories_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCategories.EditIndex = -1;
            BindData();
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txtCategoryName.Text.Trim();

            if (!string.IsNullOrEmpty(name))
            {
                category cat = new category
                {
                    categoryName = name
                };

                classProduct.AddCategory(cat);
                txtCategoryName.Text = "";
                BindData();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Thêm loại sản phẩm thành công!');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Vui lòng nhập tên loại sản phẩm!');", true);
            }
        }

    }

}