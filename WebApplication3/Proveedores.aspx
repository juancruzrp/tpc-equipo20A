<%@ Page Title="Proveedores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="WebApplication3.Proveedores" %>

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
            </Columns>
        </asp:GridView>

        <asp:Button Text="Agregar Proveedor" ID="btnAgregar" runat="server" CssClass="btn btn-outline-primary" OnClick="btnAgregar_Click" />
        <asp:Button Text="Modificar Proveedor" ID="btnModificar" runat="server" CssClass="btn btn-outline-info" OnClick="btnModificar_Click" Enabled="false" />
    </main>
</asp:Content>