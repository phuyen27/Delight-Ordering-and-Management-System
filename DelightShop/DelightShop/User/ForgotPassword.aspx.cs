using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DelightShop.User
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string Email = email.Value;
            string phone = phonenumber.Value;
            string newPassword = password.Value;
            string userName = (string)Session["Username"];
            // Check if the email and phone number exist in the database
            if (Customer.IsEmailAndPhoneExist(Email, phone))
            {
                if (newPassword.Length >= 6)
                {

                    SendEmail(email.Value, userName);
                    string script = "alert('Thay đổi mật khẩu thành công!'); window.location.href = '/Signin.aspx';";
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", script, true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Mật khẩu phải có ít nhất 6 ký tự!');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Email hoặc số điện thoại không đúng!');", true);
            }
        }

        protected void SendEmail(string email, string name)
        {
            try
            {
                string smtpHost = "smtp.gmail.com"; 
                int smtpPort = 587;
                string smtpUsername = "delightshophcm@gmail.com"; 
                string smtpPassword = "ojgu goou nvky jnoh"; 

                using (SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.EnableSsl = true; 

                    MailMessage mailMessage = new MailMessage
                    {
                        From = new MailAddress(smtpUsername), 
                        Subject = "Xác nhận thông tin #", 
                        Body = "Xin chào " + name + ",\n\n" +
                               "Bạn vừa thay đổi mật khẩu tài khoản tại Delight\n\n" +
                             
                               "Để biết thêm thông tin chi tiết, vui lòng tìm hiểu tại website\n\n" +
                               "Chúng tôi mong nhận được sự quan tâm từ bạn.\n\n" +
                               "Trân trọng,\nĐội ngũ DelightShop",
                        IsBodyHtml = false 
                    };

                    mailMessage.To.Add(new MailAddress(email)); 

                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Lỗi khi gửi email: " + ex.Message;
                errorMessage += "<br />" + ex.ToString(); 

                Response.Write("<script>alert('" + errorMessage + "');</script>");
            }
        }
    }
}