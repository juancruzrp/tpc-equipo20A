<%@ Page Title="Ventas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="WebApplication3.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2>Nueva Venta</h2>
        <br />

        <!-- 🔍 Campo de búsqueda -->
        <asp:Label ID="LabelBuscar" runat="server" Text="Buscar producto:"></asp:Label>
        <asp:TextBox ID="txtBuscar" runat="server" AutoPostBack="true"
            OnTextChanged="txtBuscar_TextChanged" CssClass="form-control" Width="300px" />
        <br />

        <!-- 🔄 UpdatePanel principal -->
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div style="display: flex; gap: 20px; align-items: flex-start;">

                    <!-- 🧩 Tabla de productos -->
                    <div style="flex: 2;">
                        <h3>Productos</h3>
                        <asp:GridView ID="dgvProductos" runat="server" CssClass="table table-striped"
                            AutoGenerateColumns="false" OnRowCommand="dgvProductos_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Marca.Nombre" HeaderText="Marca" />
                                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
                                <asp:BoundField DataField="Stock" HeaderText="Stock" />

                                <asp:TemplateField HeaderText="Cantidad">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCantidad" runat="server" Text="1" CssClass="form-control" Width="60px" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar"
                                            CommandName="AgregarProducto"
                                            CommandArgument='<%# Eval("IDProducto") %>'
                                            CssClass="btn btn-success btn-sm" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <!-- 🧾 Detalle de venta -->
                    <div style="flex: 1; border-left: 2px solid #ccc; padding-left: 15px;">
                        <h3>Detalle de Venta</h3>
                        <asp:Panel ID="PanelDetalleVenta" runat="server">
                            <asp:GridView ID="dgvDetalleVenta" runat="server" CssClass="table table-bordered"
                                AutoGenerateColumns="false" OnRowCommand="dgvDetalleVenta_RowCommand"
                                ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:BoundField DataField="Nombre" HeaderText="Producto" />
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cant." />
                                    <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
                                    <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnQuitar" runat="server" Text="Quitar"
                                                CommandName="QuitarProducto"
                                                CommandArgument='<%# Eval("IDProducto") %>'
                                                CssClass="btn btn-danger btn-sm" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <div style="text-align: right; margin-top: 10px;">
                                <asp:Label ID="lblTotal" runat="server" Font-Bold="true" Font-Size="Large" Text="Total: $0,00"></asp:Label>
                                <br /><br />
                                <asp:Button ID="btnConfirmarVenta" runat="server" Text="Confirmar Venta"
                                    CssClass="btn btn-primary" OnClick="btnConfirmarVenta_Click" />
                            </div>
                        </asp:Panel>
                    </div>
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="txtBuscar" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnConfirmarVenta" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

        <script type="text/javascript">
            $(document).ready(function () {
                // Detecta cuando el usuario escribe en el textbox
                $('#<%= txtBuscar.ClientID %>').on('input', function () {
            // Dispara el postback automático sin Enter
            __doPostBack('<%= txtBuscar.UniqueID %>', '');
        });
    });
        </script>

    </main>
</asp:Content>
