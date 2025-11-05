<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="WebApplication3.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2>Usuarios</h2>
        <asp:GridView ID="dgvUsuarios" CssClass="table" runat="server"></asp:GridView>
    
        <asp:Button Text="Agregar Usuario" ID="btnAgregar" runat="server" class="btn btn-outline-primary" OnClick="btnAgregar_Click" />   
    </main>
</asp:Content>
