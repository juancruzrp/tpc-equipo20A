<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication3.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
    <main aria-labelledby="title">a
        <h2 id="title">Pantalla de ingreso</h2>
                
       <asp:Label ID="lblUsuario" runat="server" class="form-label" Text="Usuario"></asp:Label>
       <asp:TextBox ID="txtUsuario" runat="server" class="form-control"></asp:TextBox>
        <br />
        <asp:Label ID="lblcontraseña" runat="server" class="form-label" Text="Contraseña"></asp:Label>
        <asp:TextBox ID="txtContraseña" runat="server" class="form-control" TextMode="Password"></asp:TextBox>  
          <br />

        <asp:Button ID="btnIngresar" runat="server" class="btn btn-primary" OnClick="btnIngresar_Click" Text="Ingresar" />
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    </main>


</asp:Content>
