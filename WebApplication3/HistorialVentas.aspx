<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialVentas.aspx.cs" Inherits="WebApplication3.HistorialVentas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="LabelBuscar" runat="server" Text="Buscar Venta:" ></asp:Label>
    <asp:TextBox ID="txtBuscar" runat="server" placeholder="Ingrese Fecha..." CssClass="form-control" Width="300px" />
    <br />

    
    <asp:GridView ID="dgvVentas" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="IDVenta" HeaderText="ID Venta" />
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            <asp:BoundField DataField="Cliente.NombreCompleto" HeaderText="Cliente" />
            <asp:BoundField DataField="Usuario.NombreUsuario" HeaderText="Usuario" />
            <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C2}" />
        </Columns>
    </asp:GridView>


</asp:Content>
