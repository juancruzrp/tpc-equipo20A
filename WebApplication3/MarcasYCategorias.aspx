<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarcasYCategorias.aspx.cs" Inherits="WebApplication3.MarcasYCategorias" %>

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

        .grid-container {
            display: flex;
            gap: 40px;
            justify-content: space-between;
            flex-wrap: wrap;
        }

        .grid-box {
            flex: 1;
            min-width: 400px;
        }

        h2 {
            margin-top: 0;
        }
    </style>

    <main aria-labelledby="title">
        <div class="grid-container">
            <!-- Marcas -->
            <div class="grid-box">
                <h2>Marcas</h2>
                <asp:GridView ID="dgvMarcas"
                    CssClass="table"
                    runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="IDMarca"
                    OnRowDataBound="dgvMarcas_RowDataBound"
                    OnSelectedIndexChanged="dgvMarcas_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="IDMarca" HeaderText="IDMarca" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre de la marca" />
                        <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar"
                            HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none" />
                    </Columns>
                </asp:GridView>
                <% if (SesionHelper.EsUsuarioAdmin(Session))
                    { %>
                <asp:Button Text="Agregar Marca" ID="btnAgregar" runat="server" CssClass="btn btn-outline-primary" OnClick="btnAgregar_Click" />
                <asp:Button Text="Modificar Marca" ID="btnModificar" runat="server" CssClass="btn btn-outline-info" OnClick="btnModificar_Click" Enabled="false" />
                <asp:Button Text="Inactivar Marca" ID="btnInactivar" runat="server" CssClass="btn btn-outline-danger" OnClick="btnInactivar_Click" Enabled="false" OnClientClick="return confirm('¿Estás seguro de que quieres inactivar la marca seleccionada?');" />
                <% } %>
            </div>

            <!-- Categorías -->
            <div class="grid-box">
                <h2>Categorías</h2>
                <asp:GridView ID="dgvCategorias"
                    CssClass="table"
                    runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="IDCategoria"
                    OnRowDataBound="dgvCategorias_RowDataBound"
                    OnSelectedIndexChanged="dgvCategorias_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="IDCategoria" HeaderText="IDCategoria" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre de la categoría" />
                        <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar"
                            HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none" />
                    </Columns>
                </asp:GridView>
                <% if (SesionHelper.EsUsuarioAdmin(Session))
                    { %>
                <asp:Button Text="Agregar Categoría" ID="btnAgregarCat" runat="server" CssClass="btn btn-outline-primary" OnClick="btnAgregarCat_Click" />
                <asp:Button Text="Modificar Categoría" ID="btnModificarCat" runat="server" CssClass="btn btn-outline-info" OnClick="btnModificarCat_Click" Enabled="false" />
                <asp:Button Text="Inactivar Categoría" ID="btnEliminarCat" runat="server" CssClass="btn btn-outline-danger" OnClick="btnEliminarCat_Click" OnClientClick="return confirm('¿Estás seguro de que quieres eliminar la categoría seleccionada?');" />
                <% } %>
            </div>
        </div>
    </main>
</asp:Content>
