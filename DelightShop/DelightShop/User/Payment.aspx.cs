using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DelightShop.User
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ form
            string name = namePayment.Text;
            string phone = phonePayment.Text;
            string address = addressPayment.Text;
            string Method = paymentMethod.SelectedValue;
            DateTime date = DateTime.Now;

            int maDH = (int)Session["maDH"]; 
            Customer.InsertThongTinThanhToan(maDH, name, phone, address, date, Method, "Processing");
            string mailCustomer = (string)Session["Username"];
            Session.Remove("maDH");
            SendEmail(mailCustomer,name, phone, address,Method, date);
            Response.Write("<script>alert('Đặt hàng thành công!'); window.location.href='Cart.aspx';</script>");
        
        }
        protected void SendEmail(string email,string name, string phone, string address, string method, DateTime date)
        {
            try
            {
                string smtpHost = "smtp.gmail.com"; // SMTP server của Gmail
                int smtpPort = 587; // Cổng SMTP của Gmail
                string smtpUsername = "delightshophcm@gmail.com"; // Tài khoản Gmail gửi email
                string smtpPassword = "ojgu goou nvky jnoh"; // Mật khẩu của tài khoản Gmail (hoặc mật khẩu ứng dụng nếu có 2FA)

                using (SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.EnableSsl = true; // Bật SSL để bảo mật kết nối

                    MailMessage mailMessage = new MailMessage
                    {
                        From = new MailAddress(smtpUsername), // Địa chỉ gửi email
                        Subject = "Xác nhận đơn hàng #", // Tiêu đề email
                        Body = "Xin chào " + name + ",\n\n" +
                               "Cảm ơn bạn đã thanh toán thành công. Đây là thông tin đơn hàng của bạn:\n\n" +
                               "Tên: " + name + "\n" +
                               "Số điện thoại: " + phone + "\n" +
                               "Địa chỉ: " + address + "\n" +
                               "Phương thức thanh toán: " + method + "\n" +
                               "Ngày thanh toán: " + date.ToString("dd/MM/yyyy HH:mm:ss") + "\n\n" +
                               "Chúng tôi sẽ xử lý đơn hàng của bạn trong thời gian sớm nhất.\n\n" +
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