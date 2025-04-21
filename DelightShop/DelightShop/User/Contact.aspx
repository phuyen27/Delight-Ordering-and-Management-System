<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="DelightShop.User.Contact" MasterPageFile="~/User/HeaderFooter.master"%>

<asp:Content ContentPlaceHolderID="MainContent" Runat="server">
    <body>
    <section class="section container grid">
       <div class="contact">
        <div class="container_contact">
            <h2>Contact Us</h2>
          
            <div class="contact_descrip">
                <div class="contact-info">
                    <h3>Delight Shop</h3>
                    <p><i class="ri-map-pin-line"></i> Address: 12/23c Christmas Street, Ho Chi Minh City</p>
                    <p><i class="ri-phone-line"></i> Phone number: 0987654321</p>
                    <p><i class="ri-mail-line"></i> Email: delightshop@gmail.com</p>
                </div>

                <div class="contact_img">
                    <img src="/assets/img/contact_img.webp" alt="">
                </div>               
            </div>  
        </div>
       </div>
    </section>
</body>
    <style>
        /*CONTACT*/
.contact {
    background-color: white;
    padding: 50px 20px;
    margin: 40px;
    border-radius: 10px;
}

.contact h2 {
    text-align: center;
    font-size: 2rem;
    margin-bottom: 20px;
    animation: glow 1.5s infinite alternate;
}

.contact p {
    text-align: center;
    margin-bottom: 40px;
}

.contact-info {
    text-align: center;
    margin-top: 40px;
}

.contact-info h3 {
    color: #207c46;
    font-size: 1.5rem;
    padding: 20px 0;
    letter-spacing: 1.5px;
}

.contact-info i {
    font-size: 1.5rem;
    margin-right: 10px;
}

.contact-info p {
    font-size: 1rem;
    margin-bottom: 10px;
    font-weight: 600;
}

.contact-info p i {
    color: red;
}

.contact_descrip {
    display: flex;
    align-items: center;
    justify-content: space-around;
}

.contact_img {
    filter: drop-shadow(var(--shadow-img));
}

    </style>
</asp:Content>

