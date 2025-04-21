using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DelightShop.Admin
{
    public partial class ProductsManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string keyword = Request.QueryString["search"];
                if (!string.IsNullOrEmpty(keyword))
                {
                    var result = classProduct.GetProductsBySearch(keyword);
                    gvProducts.DataSource = result;
                    gvProducts.DataBind();
                }
                else
                {
                    BindProductsData();
                    LoadCategoriesToDropdown();
                }
            }
        }

        private void BindProductsData()
        {
            List<Product> products = classProduct.GetProducts();
            gvProducts.DataSource = products;
            gvProducts.DataBind();
        }

        private void LoadCategoriesToDropdown()
        {
            List<int> categoryid = classProduct.GetAllCategoryIDs(); // Gọi hàm bạn đã viết

            ddlNewCategory.DataSource = categoryid;
            ddlNewCategory.DataBind();

            ddlNewCategory.Items.Insert(0, new ListItem("-- Chọn loại sản phẩm --", "0")); // Tuỳ chọn mặc định

        }

        protected void gvProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Chuyển trang: {e.NewPageIndex}"); // Kiểm tra xem có chạy không
            gvProducts.PageIndex = e.NewPageIndex;
            BindProductsData();
        }


        protected void gvProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProducts.EditIndex = -1;
            BindProductsData();
        }

        protected void gvProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int productID = Convert.ToInt32(gvProducts.DataKeys[e.RowIndex].Value);
            admin.DeleteItems(productID, "DELETE FROM SanPham WHERE MaSP = @productID", "@productID");
            Response.Write("<script>alert('Xóa sản phẩm thành công!');</script>");

            BindProductsData();
        }

        protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProducts.EditIndex = e.NewEditIndex;
            BindProductsData();

            GridViewRow row = gvProducts.Rows[e.NewEditIndex];
            int productID = Convert.ToInt32(gvProducts.DataKeys[e.NewEditIndex].Value);
            txtProductId.Text = productID.ToString();
        }

        protected void gvProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int productID = Convert.ToInt32(gvProducts.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvProducts.Rows[e.RowIndex];

            TextBox txtProductId = (TextBox)row.FindControl("txtProductId");
            TextBox txtProductName = (TextBox)row.FindControl("txtProductName");
            TextBox txtPrice = (TextBox)row.FindControl("txtPrice");
            TextBox txtOrigin = (TextBox)row.FindControl("txtOrigin");
            TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
            TextBox txtQuantityInStock = (TextBox)row.FindControl("txtQuantityInStock");
            DropDownList ddlCategory = (DropDownList)row.FindControl("ddlCategory");

            if (ddlCategory != null && !string.IsNullOrEmpty(ddlCategory.SelectedValue))
            {
                int categoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                string productName = txtProductName.Text;
                decimal price = string.IsNullOrEmpty(txtPrice.Text) ? 0 : Convert.ToDecimal(txtPrice.Text);
                string origin = txtOrigin.Text;
                string description = txtDescription.Text;
                int quantity = string.IsNullOrEmpty(txtQuantityInStock.Text) ? 0 : Convert.ToInt32(txtQuantityInStock.Text);

                try
                {
                    classProduct.UpdateProduct(productID, categoryID, productName, price, origin, description, quantity);
                    string script = "alert('Cập nhật thông tin sản phẩm thành công!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                    gvProducts.EditIndex = -1;
                    BindProductsData();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Cập nhật thất bại');</script>");
                }
            }
            else
            {
                // Nếu ddlCategory không có giá trị hợp lệ
                Response.Write("<script>alert('Vui lòng chọn loại sản phẩm');</script>");
            }
        }



        protected void gvProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState.HasFlag(DataControlRowState.Edit))
            {
                DropDownList ddlEmployeeType = (DropDownList)e.Row.FindControl("ddlCategory");
                if (ddlEmployeeType != null)
                {
                    LoadCategoryID(ddlEmployeeType);

                    string currentCategoryTypeID = DataBinder.Eval(e.Row.DataItem, "CategoryId").ToString();
                    ddlEmployeeType.SelectedValue = currentCategoryTypeID;
                }
            }
        }

        protected void btnUpdateProduct_Click(object sender, EventArgs e)
        {          
           
        }

        protected void btnCancelProduct_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (fuProductImage.HasFile)
            {
                string fileName = Path.GetFileName(fuProductImage.FileName);
                string filePath = "/imgProductUpload/" + fileName;
                fuProductImage.SaveAs(Server.MapPath(filePath));

                string productName = txtNewProductName.Text.Trim();
                int categoryId = Convert.ToInt32(ddlNewCategory.SelectedValue);
                decimal price = Convert.ToDecimal(txtNewPrice.Text.Trim());
                string origin = txtNewOrigin.Text.Trim();
                string description = txtNewDescription.Text.Trim();
                int quantity = Convert.ToInt32(txtNewQuantity.Text.Trim());

                classProduct.AddProduct(categoryId, productName, price, origin, description, filePath, quantity);
                Response.Write("<script>alert('Thêm thông tin sản phẩm thành công!');</script>");

                BindProductsData(); // Cập nhật lại GridView
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please upload an image!');", true);
            }
        }
        private static string connectionString = "Server=UYENBABY2K4\\SQLEXPRESS;Database=DelightManager;Integrated Security=True;";


        private void LoadCategoryID(DropDownList ddl)
        {
            string query = "SELECT MaLoaiSP, TenLoaiSP FROM LoaiSP";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ddl.DataSource = reader;
                ddl.DataTextField = "TenLoaiSP";
                ddl.DataValueField = "MaLoaiSP";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem("-- Select product type --", "0"));
        }

    }
}