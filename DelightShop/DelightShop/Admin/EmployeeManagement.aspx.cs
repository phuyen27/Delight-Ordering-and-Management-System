using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static DelightShop.order;

namespace DelightShop.Admin
{
    public partial class EmployeeManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string keyword = Request.QueryString["search"];
                if (!string.IsNullOrEmpty(keyword))
                {
                    var result = employee.SearchEmployeesByName(keyword);
                    gvEmployees.DataSource = result;
                    gvEmployees.DataBind();
                }
                else
                {
                    BindEmployeeData();
                   
                }
                LoadPhongBan(employeeDepartment);
            }
           
        }

        private void BindEmployeeData()
        {
            List<employee> employees = employee.GetAllEmployees();
            gvEmployees.DataSource = employees;
            gvEmployees.DataBind();
        }

        protected void gvEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState.HasFlag(DataControlRowState.Edit))
            {
                DropDownList ddlEmployeeType = (DropDownList)e.Row.FindControl("ddlEmployeeType");
                if (ddlEmployeeType != null)
                {
                    LoadPhongBan(ddlEmployeeType);

                    string currentEmployeeTypeID = DataBinder.Eval(e.Row.DataItem, "employeeTypeID").ToString();
                    ddlEmployeeType.SelectedValue = currentEmployeeTypeID;
                }
            }
        }

        protected void gvEmployees_RowEditing(object sender, GridViewEditEventArgs e) 
        {

            gvEmployees.EditIndex = e.NewEditIndex;
            BindEmployeeData();

            GridViewRow row = gvEmployees.Rows[e.NewEditIndex];
            int employeeID = Convert.ToInt32(gvEmployees.DataKeys[e.NewEditIndex].Value);
            txtEmployeeID.Text = employeeID.ToString();
        }

        protected void gvEmployees_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int employeeID = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Value);
                admin.DeleteItems(employeeID, "DELETE FROM NhanVien WHERE MaNV = @employeeID", "@employeeID");
                BindEmployeeData();
                string script = "alert('Xóa thông tin nhân viên thành công!');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                gvEmployees.EditIndex = -1;
                BindEmployeeData();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Xóa thất bại');</script>");
            }
        }

        protected void gvEmployees_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmployees.EditIndex = -1;
            BindEmployeeData();
        }

        protected void gvEmployees_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            int employeeID = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvEmployees.Rows[e.RowIndex];

            TextBox txtFirstName = (TextBox)row.FindControl("txtFirstName");
            TextBox txtLastName = (TextBox)row.FindControl("txtLastName");
            TextBox txtPhone = (TextBox)row.FindControl("txtPhone");
            DropDownList ddlGender = (DropDownList)row.FindControl("ddlGender");

            TextBox txtDOB = (TextBox)row.FindControl("txtDOB");
            DateTime dob;
            if (!DateTime.TryParse(txtDOB.Text, out dob))
            {
                Response.Write("<script>alert('Invalid Date Format');</script>");
                return;
            }

            DropDownList ddlEmployeeTypeID = (DropDownList)row.FindControl("ddlEmployeeType");

            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string phone = txtPhone.Text;
            string gender = ddlGender.SelectedValue;
            int employeeTypeID = Convert.ToInt32(ddlEmployeeTypeID.SelectedValue);

            try
            {
                employee.UpdateEmployee(employeeID, firstName, lastName, phone, gender, dob, employeeTypeID);
                string script = "alert('Cập nhật thông tin nhân viên thành công!');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                gvEmployees.EditIndex = -1;
                BindEmployeeData();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Cập nhật thất bại');</script>");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
        }
        protected void SaveEmployee(object sender, EventArgs e)
        {
            string fName = firstName.Text;
            string lName = lastName.Text;
            string phoneNumber = employeePhone.Text;
            string gender = employeeGender.SelectedValue;
            DateTime dob = Convert.ToDateTime(employeeBirthday.Text);
            int employeeType = Convert.ToInt32(employeeDepartment.SelectedValue);

            try
            {
                employee.InsertEmployee(fName, lName, phoneNumber, gender, dob, employeeType);

                string script = "alert('Thêm thông tin nhân viên thành công!');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                gvEmployees.EditIndex = -1;
                BindEmployeeData();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Thêm thông tin thất bại');</script>");
            }
        }

     
        protected void btnUpdate_Click(object sender,EventArgs e)
        {
            int employeeID;
            if (!int.TryParse(txtEmployeeID.Text, out employeeID))
            {
                Response.Write("<script>alert('Employee ID không hợp lệ!');</script>");
                return;
            }
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string phone = txtPhone.Text;
            string gender = ddlGender.SelectedValue;
            DateTime dob = Convert.ToDateTime(txtDOB.Text);
            int employeeTypeID = Convert.ToInt32(ddlEmployeeType.SelectedValue);

            try
            {

                employee.UpdateEmployee(employeeID, firstName, lastName, phone, gender, dob, employeeTypeID);
                string script = "alert('Cập nhật thông tin nhân viên thành công!');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                gvEmployees.EditIndex = -1;
                BindEmployeeData();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Cập nhật thất bại');</script>");
            }
        }

        private void LoadPhongBan(DropDownList ddl)
        {
            string query = "SELECT MaPhong, TenPhong FROM PhongBan";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ddl.DataSource = reader;
                ddl.DataTextField = "TenPhong"; 
                ddl.DataValueField = "MaPhong";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem("--Select department--", "0"));
        }


        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

    
        protected void gvEmployees_Sorting(object sender, GridViewSortEventArgs e)
        {
           
        }

       
    }
}