<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="DelightShop.User.Payment" MasterPageFile="~/User/HeaderFooter.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" Runat="server">
    <!DOCTYPE html>
    <body>
        <form id="form1" runat="server">
            <div class="section container grid">
                <section id="payment" class="payment">
                    <div class="form-group">
                        <label for="namePayment">Recipient name</label>
                        <asp:TextBox ID="namePayment" runat="server" CssClass="form-control" placeholder="Enter your first and last name"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="phonePayment">Recipient phone number</label>
                        <asp:TextBox ID="phonePayment" runat="server" CssClass="form-control" placeholder="Enter phone number"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="addressPayment">Delivery address</label>
                        <asp:TextBox ID="addressPayment" runat="server" CssClass="form-control" placeholder="Delivery address"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="payment-method">Payment method</label>
                        <asp:DropDownList ID="paymentMethod" runat="server" CssClass="form-control">
                            <asp:ListItem Value="credit-card">Credit card</asp:ListItem>
                            <asp:ListItem Value="paypal">PayPal</asp:ListItem>
                            <asp:ListItem Value="cash-on-delivery">Cash on Delivery</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:Button ID="btnSubmit" runat="server" Text="Payment" CssClass="btn-submit" OnClick="btnSubmit_Click" />
                </section>
            </div>
        </form>
    </body>
</asp:Content>
