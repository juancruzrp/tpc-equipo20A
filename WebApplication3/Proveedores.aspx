<%@ Page Title="Proveedores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="WebApplication3.Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2>Proveedores</h2>
        <asp:GridView ID="dgvProveedores" CssClass="table" runat="server"></asp:GridView>
    </main>
</asp:Content>
