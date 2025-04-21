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
                LoadDonHangDropDowns();
                BindFAQList();
            }
        }

        // Lấy danh sách đơn hàng đưa vào dropdownlist
        private void LoadDonHangDropDowns()
        {
           
            string querySP = "Select MaDH from DonDH";
            List<int> spID = admin.GetIDs(querySP, "MaDH");
            ddlMaDH.DataSource = spID;
            ddlMaDH.DataBind();

            ddlMaDH.Items.Insert(0, new ListItem("-- Chọn mã sản phẩm --", "0")); // Tuỳ chọn mặc định
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

            gvFAQs.EditIndex = -1;
            BindFAQList();
        }

    }
}