<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="WebApplication3.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2>Clientes</h2>        
           <asp:GridView ID="dgvClientes" CssClass="table" runat="server"></asp:GridView>
    </main>
</asp:Content>
