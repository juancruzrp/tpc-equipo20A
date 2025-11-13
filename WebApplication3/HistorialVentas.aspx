<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialVentas.aspx.cs" Inherits="WebApplication3.HistorialVentas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="LabelBuscar" runat="server" Text="Buscar Venta:" ></asp:Label>
    <asp:TextBox ID="txtBuscar" runat="server" placeholder="Ingrese Fecha..." CssClass="form-control" Width="300px" />
    <br />

    <asp:GridView ID="dgvVentas" runat="server" CssClass="table table-striped table-bordered" ></asp:GridView>


</asp:Content>
