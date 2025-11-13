<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="WebApplication3.Clientes" %>

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
        <h2>Clientes</h2>
        <asp:GridView ID="dgvClientes"
            CssClass="table"
            runat="server"
            AutoGenerateColumns="False"
            DataKeyNames="IDCliente"
            OnRowDataBound="dgvClientes_RowDataBound"
            OnSelectedIndexChanged="dgvClientes_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="IDCliente" HeaderText="ID" />
                <asp:BoundField DataField="CUIT_CUIL" HeaderText="CUIT o CUIL del cliente" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre del cliente" />
                <asp:BoundField DataField="Apellido" HeaderText="Apellido del cliente" />
                <asp:BoundField DataField="Telefono" HeaderText="Telefono del cliente" />
                <asp:BoundField DataField="Mail" HeaderText="Mail del cliente" />
                <asp:BoundField DataField="Direccion" HeaderText="Direccion del cliente" />
                <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar"
                    HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none" />
            </Columns>
        </asp:GridView>
        <asp:Button Text="Agregar Cliente" ID="btnAgregar" runat="server" class="btn btn-outline-primary" OnClick="btnAgregar_Click" />
        <asp:Button Text="Modificar Cliente" ID="btnModificar" runat="server" CssClass="btn btn-outline-info" OnClick="btnModificar_Click" Enabled="false" />
    </main>
</asp:Content>
