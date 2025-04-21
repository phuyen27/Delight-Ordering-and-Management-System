<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="DelightShop.User.HomePage" MasterPageFile="~/User/HeaderFooter.master" %>

<asp:Content ContentPlaceHolderID="MainContent" Runat="server">
    <section class="home__section section" id="home">
        <div class="home__container container grid">
            <div class="home__sataclaus ">
                <img src="/assets/img/santaClaus.webp" alt="" class="home__sataclaus-img">

            </div>
            
            <div class="countdown">
                <div class="home__title">
                    <h2>Let's count down to Christmas</h2>
                </div>

                <div class="home_countdown">
                    <h2 class="countdown__day" id="countdown__day"></h2>
                    <div class="countdown__time">
                        <h3 class="countdown__hour" id="countdown__hour"></h3>
                        <span>:</span>
                        <h3 class="countdown__minute" id="countdown__minute"></h3>
                        <span>:</span>
                        <h3 class="countdown__second" id="countdown__second"></h3>
                    </div>
                </div>
            </div>
        </div>
    </section> 
</asp:Content>
