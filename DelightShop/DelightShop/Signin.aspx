<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signin.aspx.cs" Inherits="DelightShop.Signin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="shortcut icon" href="assets/img/bear-smile-fill (1).png" type="image/x-icon">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/remixicon/4.1.0/remixicon.min.css">
    <link rel="stylesheet" href="assets/css/signin_register.css">
    <link rel="shortcut icon" href="assets/img/icon.webp" type="image/x-icon">
    <title>Signin</title>
</head>
<body>
        <div class="login-container">          
            <div class="img">
                <h3>Chào mừng đến Delight!</h3>
                <img src="assets/img/sinin_img.webp" alt="" class="img_login">
            </div>
            <form id="form1" runat="server" class="login-form">
                <h1>Đăng Nhập</h1>
                <div class="input-group">
                    <input type="text" id="username" name="username" placeholder="Tên đăng nhập" required>
                </div>
                <div class="input-group">
                    <input type="password" id="password" name="password" placeholder="Mật khẩu" required>
                </div>
                <asp:Button ID="SubmitButton" runat="server" Text="Đăng nhập" CssClass="login-btn" OnClick="SubmitButton_Click" />

                <p class="message">
                    <a class="register" href="/User/Register.aspx">Đăng ký</a>
                    <a class="forgotPass" href="/User/ForgotPassword.aspx">Quên mật khẩu?</a>
                </p>
            </form>
        </div>
</body>
</html>
