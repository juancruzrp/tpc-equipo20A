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
            gap: 80px;
            justify-content: center;
            flex-wrap: wrap;
        }

        /* Cajas de las tablas */
        .grid-box {
            flex: 1;
            min-width: 400px;
            display: flex;
            flex-direction: column;
        }

        h2 {
            margin-top: 0;
            text-align: center;
            margin-bottom: 20px;
        }


        .btn-panel {
            margin-top: 20px;
            display: flex;
            gap: 15px;
            flex-wrap: wrap;
        }
    </style>
    <main aria-labelledby="title">
        <div class="grid-container">
            <div class="grid-box">
                <h2>Marcas</h2>
                <asp:UpdatePanel ID="updMarcas" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="dgvMarcas"
                            CssClass="table"
                            runat="server"
                            AutoGenerateColumns="False"
                            DataKeyNames="IDMarca"
                            OnRowDataBound="dgvMarcas_RowDataBound"
                            OnSelectedIndexChanged="dgvMarcas_SelectedIndexChanged"
                            EnableViewState="true">
                            <Columns>
                                <asp:BoundField DataField="IDMarca" HeaderText="IDMarca" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre de la marca" />
                                <asp:CheckBoxField DataField="Estado" HeaderText="Activo" />
                                <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar"
                                    HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none" />
                            </Columns>
                        </asp:GridView>

                        <% if (SesionHelper.EsUsuarioAdmin(Session))
                            { %>

                        <div class="btn-panel">
                            <asp:Button Text="Agregar Marca" ID="btnAgregar" runat="server" CssClass="btn btn-outline-primary" OnClick="btnAgregar_Click" />
                            <asp:Button Text="Modificar Marca" ID="btnModificar" runat="server" CssClass="btn btn-outline-info" OnClick="btnModificar_Click" Enabled="false" />
                            <asp:Button Text="Inactivar Marca" ID="btnInactivar" runat="server" CssClass="btn btn-outline-danger" OnClick="btnInactivar_Click" Enabled="false" />
                        </div>
                        <% } %>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <!-- Categorías -->
            <div class="grid-box">
                <h2>Categorías</h2>
                <asp:UpdatePanel ID="updCategorias" runat="server">
                    <ContentTemplate>
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
                                <asp:CheckBoxField DataField="Estado" HeaderText="Activo" />
                                <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar"
                                    HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none" />
                            </Columns>
                        </asp:GridView>

                        <% if (SesionHelper.EsUsuarioAdmin(Session))
                            { %>

                        <div class="btn-panel">
                            <asp:Button Text="Agregar Categoría" ID="btnAgregarCat" runat="server" CssClass="btn btn-outline-primary" OnClick="btnAgregarCat_Click" />
                            <asp:Button Text="Modificar Categoría" ID="btnModificarCat" runat="server" CssClass="btn btn-outline-info" OnClick="btnModificarCat_Click" Enabled="false" />
                            <asp:Button Text="Inactivar Categoría" ID="btnEliminarCat" runat="server" CssClass="btn btn-outline-danger" OnClick="btnEliminarCat_Click" />
                        </div>
                        <% } %>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </main>
</asp:Content>
