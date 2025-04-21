<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DelightShop.User.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/remixicon/4.1.0/remixicon.min.css">
    <link rel="shortcut icon" href="/assets/img/icon.webp" type="image/x-icon">
    <link rel="stylesheet" href="/assets/css/signin_register.css"> 
     <link rel="stylesheet" href="/assets/css/style.css"> 
    <title>Đăng Ký</title>
</head>
    <script>
        function selectAvatar(avatarFileName) {
            // Lưu tên file của avatar đã chọn vào HiddenField
            document.getElementById('<%= hfAvatar.ClientID %>').value = avatarFileName;

            // Thêm hiệu ứng để hiển thị avatar được chọn (tuỳ chỉnh)
            var avatars = document.querySelectorAll('.avatar-thumbnail');
            avatars.forEach(function (avatar) {
                avatar.classList.remove('selected'); // Gỡ bỏ lớp 'selected' nếu có
            });
            event.target.classList.add('selected'); // Thêm lớp 'selected' cho avatar đã chọn
        }
    </script>

<body>
        <div class="login-container">          
    <div class="img">
        <h3>Chào mừng đến Delight!</h3>
        <img src="/assets/img/Christmas Tree.webp" alt="" class="img_login">
    </div>
    <form class="registerform" runat="server" id="signupform">
        <h1>Đăng Ký</h1>
      
        <div class="username">                    
            <asp:TextBox ID="firstname" runat="server" placeholder="Họ khách" CssClass="form-input" Required="true"></asp:TextBox>
            <asp:TextBox ID="lastname" runat="server" placeholder="Tên khách" CssClass="form-input" Required="true"></asp:TextBox>

            <%--<asp:TextBox ID="lastname" runat="server" placeholder="Tên khách" CssClass="form-input" Required="true"></asp:TextBox>--%>
        </div>

        <div class="input-group">
             <asp:TextBox ID="dob" runat="server" TextMode="Date" CssClass="form-input" Required="true"></asp:TextBox>
        </div>
    
        <div class="input-group">
            <asp:TextBox ID="email" runat="server" placeholder="Email" CssClass="form-input" Required="true"></asp:TextBox>
        </div>
        
        <div class="input-group">
            <asp:TextBox ID="phonenumber" runat="server" placeholder="Số điện thoại" CssClass="form-input" Required="true"></asp:TextBox>
        </div>
        
        <div class="input-group">
            <asp:TextBox ID="address" runat="server" placeholder="Địa chỉ" CssClass="form-input" Required="true"></asp:TextBox>
        </div>
        
        <div class="input-group">
            <asp:TextBox ID="password" runat="server" TextMode="Password" placeholder="Mật khẩu" CssClass="form-input" Required="true"></asp:TextBox>
        </div>
        <div class="input-group">
    <label for="avatar">Chọn Avatar</label>
    <div id="avatar-selection">
        <!-- Các hình ảnh sẽ được hiển thị ở đây -->
        <img src="/img_user/user_avt1.png" alt="Avatar 1" class="avatar-thumbnail" onclick="selectAvatar('/img_user/user_avt1.png')" />
        <img src="/img_user/user_avt2.png" alt="Avatar 2" class="avatar-thumbnail" onclick="selectAvatar('/img_user/user_avt2.png')" />
        <img src="/img_user/user_avt3.png" alt="Avatar 3" class="avatar-thumbnail" onclick="selectAvatar('/img_user/user_avt3.png')" />
        <img src="/img_user/user_avt5.png" alt="Avatar 5" class="avatar-thumbnail" onclick="selectAvatar('/img_user/user_avt5.png')" />
        <img src="/img_user/user_avt6.png" alt="Avatar 6" class="avatar-thumbnail" onclick="selectAvatar('/img_user/user_avt6.png')" />
        <img src="/img_user/user_avt7.png" alt="Avatar 7" class="avatar-thumbnail" onclick="selectAvatar('/img_user/user_avt7.png')" />
        <img src="/img_user/user_avt8.png" alt="Avatar 8" class="avatar-thumbnail" onclick="selectAvatar('/img_user/user_avt8.png')" />
        <img src="/img_user/user_avt9.png" alt="Avatar 9" class="avatar-thumbnail" onclick="selectAvatar('/img_user/user_avt9.png')" />
    </div>
    <asp:HiddenField ID="hfAvatar" runat="server" />
</div>

        <!-- Nút đăng ký -->
         <asp:Button ID="btnRegister" runat="server" Text="Đăng Ký" OnClick="btnRegister_Click" CssClass="btn" />
           
        <p class="messageCheck"> Đã có tài khoản?
            <a class="login" href="/Signin.aspx">Đăng nhập</a>
        </p>
    </form>
</div>
</body>
</html>
