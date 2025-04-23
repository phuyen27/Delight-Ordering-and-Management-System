using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static DelightShop.order;

namespace DelightShop.Admin
{
    public partial class ReceipDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDropdown(); // ✅ Luôn load dropdown duy nhất 1 lần khi lần đầu mở trang

                string searchParam = Request.QueryString["search"];
                int keyword;

                if (int.TryParse(searchParam, out keyword))
                {
                    var result = order.GetReceiptDetailsWithID(keyword);
                    gvReceiptDetails.DataSource = result;
                    gvReceiptDetails.DataBind();
                }
                else
                {
                    BindData();
                }
            }
        }

        private void BindData()
        {
            List<order.receiptDetail> suppliers = order.GetAllReceiptDetails();
            gvReceiptDetails.DataSource = suppliers;
            gvReceiptDetails.DataBind();
        }

        private void loadDropdown()
        {
            string queryPN = "SELECT MaPNH FROM PhieuNhapHang";

            List<int> phieuNhapID = admin.GetIDs(queryPN, "MaPNH"); // Gọi hàm bạn đã viết

            ddlReceiptDetailID.DataSource = phieuNhapID;
            ddlReceiptDetailID.DataBind();

           
            string querySP = "Select MaSP from SanPham";
            List<int> spID = admin.GetIDs(querySP, "MaSP"); // Gọi hàm bạn đã viết

            ddlReceiptProductID.DataSource = spID;
            ddlReceiptProductID.DataBind();

            string queryNV = "Select MaNV from NhanVien";
            List<int> nvID = admin.GetIDs(queryNV, "MaNV"); // Gọi hàm bạn đã viết

        }


        protected void gvReceiptDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int receiptId = Convert.ToInt32(gvReceiptDetails.DataKeys[e.RowIndex].Values["receiptDetailID"]);
            int productId = Convert.ToInt32(((Label)gvReceiptDetails.Rows[e.RowIndex].Cells[1].Controls[0]).Text);

            if (order.DeleteReceiptDetail(receiptId, productId))
            {
                BindData();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Xóa thành công');", true);

            }
        }

        protected void gvReceiptDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvReceiptDetails.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void gvReceiptDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvReceiptDetails.Rows[e.RowIndex];

            int receiptId = Convert.ToInt32(gvReceiptDetails.DataKeys[e.RowIndex].Value);
            int productId = Convert.ToInt32(((TextBox)row.Cells[1].Controls[0]).Text);
            int quantity = Convert.ToInt32(((TextBox)row.Cells[2].Controls[0]).Text);
            decimal price = Convert.ToDecimal(((TextBox)row.Cells[3].Controls[0]).Text);
            int employeeId = Convert.ToInt32(((TextBox)row.Cells[4].Controls[0]).Text);

            order.receiptDetail updatedDetail = new order.receiptDetail
            {
                receiptDetailID = receiptId,
                receiptProductID = productId,
                productQuantity = quantity,
                Price = price,
            };

            if (order.UpdateReceiptDetail(updatedDetail))
            {
                gvReceiptDetails.EditIndex = -1;
                BindData();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cập nhật thành công');", true);

            }
        }

        protected void gvReceiptDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReceiptDetails.EditIndex = -1;
            BindData();
        }

        protected void btnAddReceiptDetail_Click(object sender, EventArgs e)
        {
            order.receiptDetail newDetail = new order.receiptDetail
            {
                receiptDetailID = int.Parse(ddlReceiptDetailID.SelectedValue),
                receiptProductID = int.Parse(ddlReceiptProductID.SelectedValue),
                productQuantity = int.Parse(txtProductQuantity.Text),
                Price = decimal.Parse(txtPrice.Text),

            };

            if (order.InsertReceiptDetail(newDetail))
            {
                BindData();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Thêm chi tiết phiếu thành công');", true);
            }
            else
            {

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Có lỗi sảy ra!');", true);
            }
        }

        protected void gvReceiptDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }
    }
}