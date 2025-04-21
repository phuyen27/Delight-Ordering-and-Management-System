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
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string firstName = firstname.Text;   
            string lastName = lastname.Text;    
            string NgaySinh = dob.Text;              
            string Email = email.Text;           
            string phoneNumber = phonenumber.Text; 
            string addRess = address.Text;       
            string passWord = password.Text;
            string avt = hfAvatar.Value;
            if (Page.IsValid)
            {
                try
                {
                    Customer.InsertCustomer(firstName, lastName, NgaySinh, Email, phoneNumber, addRess, passWord,avt,"null");

                    SendEmail(Email, firstName + " " + lastName, phoneNumber);
                    string script = "alert('Đăng ký thành công!'); window.location.href = '/Signin.aspx';";
                    ClientScript.RegisterStartupScript(this.GetType(), "Success", script, true);

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Đã có lỗi xảy ra: " + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Vui lòng kiểm tra lại thông tin nhập vào!');</script>");
            }
        }


        protected void SendEmail(string email, string name, string phone) { 
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
                        Subject = "Xác nhận thông tin đăng ký #",
                        Body = "Kính gửi " + name + ",\n\n" +
                               "Cảm ơn bạn đã tin tưởng và lựa chọn dịch vụ của DelightShop. Chúng tôi rất vui mừng được phục vụ bạn.\n\n" +
                               "Thông tin đăng ký của bạn như sau:\n" +
                               "---------------------------------\n" +
                               "Tên: " + name + "\n" +
                               "Số điện thoại: " + phone + "\n" +
                               "---------------------------------\n\n" +
                               "Chúng tôi đã nhận thông tin của bạn và sẽ xử lý trong thời gian sớm nhất. Nếu có bất kỳ câu hỏi nào, bạn đừng ngần ngại liên hệ với chúng tôi qua email hoặc điện thoại.\n\n" +
                               "Xin chân thành cảm ơn bạn đã đồng hành cùng DelightShop. Chúng tôi rất mong được phục vụ bạn trong tương lai.\n\n" +
                               "Trân trọng,\nĐội ngũ DelightShop",

                        IsBodyHtml = false // Chúng ta gửi nội dung dưới dạng plain text
                    };

                    mailMessage.To.Add(new MailAddress(email)); // Gửi email về địa chỉ khách hàng

                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Lỗi khi gửi email: " + ex.Message;
                errorMessage += "<br />" + ex.ToString(); // Hiển thị chi tiết lỗi

                Response.Write("<script>alert('" + errorMessage + "');</script>");
            }
        }

    }
}