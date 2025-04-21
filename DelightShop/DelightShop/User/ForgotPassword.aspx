<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="DelightShop.User.ForgotPassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="shortcut icon" href="assets/img/bear-smile-fill (1).png" type="image/x-icon">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/remixicon/4.1.0/remixicon.min.css">
    <link rel="stylesheet" href="/assets/css/signin_register.css">
    <link rel="shortcut icon" href="/assets/img/icon.webp" type="image/x-icon">
    <title>Forgot Password</title>
</head>
<body>
    <div class="login-container">          
        <div class="img">
            <h3>Chào mừng đến Delight!</h3>
            <img src="/assets/img/Christmas Tree.webp" alt="" class="img_login">
        </div>
        <form class="login-form" id="Form1" runat="server">
            <h1>Quên Mật Khẩu</h1>
            
            <!-- Email -->
            <div class="input-group">
                <input type="email" id="email" placeholder="Nhập email của bạn" runat="server" required />
            </div>

            <!-- Số điện thoại -->
            <div class="input-group">
                <input type="text" id="phonenumber" placeholder="Nhập số điện thoại của bạn" runat="server" required />
            </div>
            
            <!-- Mật khẩu (Ẩn khi chưa kiểm tra) -->
            <div class="input-group" id="passwordGroup" runat="server">
                <input type="password" id="password" placeholder="Nhập lại mật khẩu của bạn" runat="server" required />
            </div>


            <!-- Nút gửi yêu cầu -->
             <button id="submit" type="submit" runat="server" onserverclick="Submit_Click">Đổi mật khẩu</button>
            
            <p class="message">
                <a class="login" href="/Signin.aspx">Quay lại Đăng Nhập</a>
            </p>
        </form>
    </div>
</body>
</html>
