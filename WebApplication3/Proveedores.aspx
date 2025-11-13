<%@ Page Title="Proveedores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="WebApplication3.Proveedores" %>
<%@ Import Namespace="WebApplication3.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .table tr.selectedRowHighlight {
            background-color: #cce5ff;
            font-weight: bold;
            color: #004085;
        }

        .table tr[style*="cursor:pointer"]:hover {
            background-color: #e0e0e0 !important;
        }
    </style>

    <main aria-labelledby="title">
        <h2>Proveedores</h2>

        <asp:GridView ID="dgvProveedores"
            CssClass="table"
            runat="server"
            AutoGenerateColumns="False"
            DataKeyNames="IDProveedor"
            OnRowDataBound="dgvProveedores_RowDataBound"
            OnSelectedIndexChanged="dgvProveedores_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="IDProveedor" HeaderText="ID" />
                <asp:BoundField DataField="CUIT_CUIL" HeaderText="CUIT o CUIL" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre del proveedor" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                <asp:BoundField DataField="Mail" HeaderText="Mail" />
                <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
            </Columns>
        </asp:GridView>
        <%if (SesionHelper.EsUsuarioAdmin(Session))
            { %>
        <asp:Button Text="Agregar Proveedor" ID="btnAgregar" runat="server" CssClass="btn btn-outline-primary" OnClick="btnAgregar_Click" />
        <asp:Button Text="Modificar Proveedor" ID="btnModificar" runat="server" CssClass="btn btn-outline-info" OnClick="btnModificar_Click" Enabled="false" />
        <asp:Button Text="Eliminar Proveedor" ID="btnInactivar" runat="server" CssClass="btn btn-outline-danger" OnClick="btnInactivar_Click" Enabled="false" OnClientClick="return confirm('¿Estás seguro de que quieres inactivar el proveedor seleccionado?');" />
        <%} %>
    </main>
</asp:Content>