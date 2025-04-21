<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AboutShop.aspx.cs" Inherits="DelightShop.User.HomePage" MasterPageFile="~/User/HeaderFooter.master" %>

<asp:Content ContentPlaceHolderID="MainContent" Runat="server">
    <!--===========ABOUT SHOP==============-->
        <section class="about__section section" id="about">
            <div class="about__container container grid">
                <div class="slider__container">
                    <div class="slider-wrapper">
                        <div class="slider">
                                <div id="slider1">
                                    <div class="about__description">
                                        <h2 class="description__shop">Christmas</h2>
                                        <h2 class="description__name">Delight</h2>
                                        <span>Celebrate Christmas in style with our unique holiday treasures! 
                                            Discover the perfect decorations and gifts at Christmas Dreams Shop.
                                            Established in 2025, we bring the magic of Christmas to life!</span>
                                        <button class="go-shop">Let's go shopping</button>
                                    </div>
                                </div>

                                <div id="slider2">
                                    <div class="about__description">
                                        <h2 class="description__shop">Winter Wonderland</h2>
                                        <h2 class="description__name">Magic Awaits</h2>
                                        <span>Embrace the beauty of winter with our exclusive collection of festive decorations. Discover snowflakes, lights, and winter wonders at our store.
                                            Join us in celebrating the season, since 2025!</span>
                                        <button class="go-shop">Shop Winter Magic</button>
                                    </div>
                                </div>

                                <div id="slider3">
                                    <div class="about__description">
                                        <h2 class="description__shop">Festive Cheer</h2>
                                        <h2 class="description__name">Joyful Moments</h2>
                                        <span>Celebrate the spirit of Christmas with our cheerful range of gifts, decorations, and cozy holiday essentials. Perfect for all your celebrations.
                                            Founded in 2025, we bring joy to every holiday!</span>
                                        <button class="go-shop">Shop Festive Cheer</button>
                                    </div>
                                </div>
                            </div>

                
                        <div class="slider-nav">
                            <a href="#slider1"></a>
                            <a href="#slider2"></a>
                            <a href="#slider3"></a>
                        </div>
                    </div>
                </div>
                

                <div class="about__img atropos">
                    <div class="atropos-scale">
                        <div class="atropos-rotate">
                            <div class="atropos-inner">
                                <img src="/assets/img/aboutSun.webp" alt="" data-atropos-offset="10" class="home__img1">
                                <img src="/assets/img/aboutNight.webp" alt="" data-atropos-offset="10" class="home__img2">
                            </div>
                        </div>
                    </div>
                </div>           
        </section>
    <style>
       
    </style>
</asp:Content>
