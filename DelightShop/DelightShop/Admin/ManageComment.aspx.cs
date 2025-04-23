using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static DelightShop.FAQ;

namespace DelightShop.Admin
{
    public partial class ManageComment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string keyword = Request.QueryString["search"];
                if (!string.IsNullOrEmpty(keyword))
                {
                    var result = FAQ.SearchFAQByComment(keyword);
                    gvFAQs.DataSource = result;
                    gvFAQs.DataBind();
                }
                else
                {
                    BindFAQList();
                }
                LoadDonHangDropDowns();

            }
        }

        // Lấy danh sách đơn hàng đưa vào dropdownlist
        private void LoadDonHangDropDowns()
        {
           
            string querySP = "Select MaDH from DonDH";
            List<int> spID = admin.GetIDs(querySP, "MaDH");
            ddlMaDH.DataSource = spID;
            ddlMaDH.DataBind();

            ddlMaDH.Items.Insert(0, new ListItem("-- Mã đơn hàng --", "0")); // Tuỳ chọn mặc định
        }

        // Hiển thị danh sách đánh giá
        private void BindFAQList()
        {
            gvFAQs.DataSource = FAQ.GetAllFAQ();
            gvFAQs.DataBind();
        }

        protected void btnAddFAQ_Click(object sender, EventArgs e)
        {
            FAQItem faq = new FAQItem
            {
                MaDH = int.Parse(ddlMaDH.SelectedValue),
                Comment = txtComment.Text
            };

            if (FAQ.InsertFAQ(faq))
            {
                BindFAQList();
                txtComment.Text = "";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Thêm thành công!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Đã xảy ra lỗi khi thêm câu hỏi.');", true);
            }

        }


        protected void gvFAQs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFAQs.PageIndex = e.NewPageIndex;
            BindFAQList();
        }

        protected void gvFAQs_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvFAQs.EditIndex = -1;
            BindFAQList();
        }

        protected void gvFAQs_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int maDG = Convert.ToInt32(gvFAQs.DataKeys[e.RowIndex].Value);
            FAQ.DeleteFAQ(maDG);
            BindFAQList();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Xóa thành công!');", true);
        }

        protected void gvFAQs_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvFAQs.EditIndex = e.NewEditIndex;
            BindFAQList();
        }

        protected void gvFAQs_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int maDG = Convert.ToInt32(gvFAQs.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvFAQs.Rows[e.RowIndex];

            int maDH = int.Parse(((TextBox)row.Cells[1].Controls[0]).Text);
            string comment = ((TextBox)row.Cells[2].Controls[0]).Text;

            FAQItem faq = new FAQItem
            {
                MaDG = maDG,
                MaDH = maDH,
                Comment = comment
            };

            FAQ.UpdateFAQ(faq);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sửa thành công!');", true);

            gvFAQs.EditIndex = -1;
            BindFAQList();
        }

    }
}