<%@ Page Title="Ventas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ComprasListado.aspx.cs" Inherits="WebApplication3.ComprasListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
    <h2 class="mb-4">Listado de Compras</h2>

    
    <asp:Repeater ID="repCompras" runat="server">
        <HeaderTemplate>
            <table class="table table-hover align-middle">
                <thead class="table-dark">
                    <tr>
                        <th>ID</th>
                        <th>Fecha</th>
                        <th>Proveedor</th>
                        <th>Total</th>
                        <th>Acción</th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>

        <ItemTemplate>
            <tr>
                <td><%# Eval("IDCompra") %></td>
                <td><%# ((DateTime)Eval("Fecha")).ToString("dd/MM/yyyy") %></td>
                <td><%# Eval("Proveedor.NombreProveedor") %></td>
                <td>$<%# Eval("Total") %></td>
                <td>
                    <button class="btn btn-outline-primary btn-sm" type="button" data-bs-toggle="collapse"
                            data-bs-target="#detalle_<%# Eval("IDCompra") %>" aria-expanded="false"
                            aria-controls="detalle_<%# Eval("IDCompra") %>">
                        Ver Detalle
                    </button>
                </td>
            </tr>
            <tr class="collapse" id="detalle_<%# Eval("IDCompra") %>">
                <td colspan="5">
                    <div class="p-3 bg-light border rounded">
                        <h6>Detalles de la compra</h6>
                        <asp:Repeater ID="repDetalles" runat="server" DataSource='<%# Eval("Detalles") %>'>
                            <HeaderTemplate>
                                <table class="table table-sm table-striped">
                                    <thead>
                                        <tr>
                                            <th>Producto</th>
                                            <th>Cantidad</th>
                                            <th>Precio Unitario</th>
                                            <th>Subtotal</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr>
                                   
                                    <td><%# Eval("Producto.Nombre") %></td>
                                    <td><%# Eval("Cantidad") %></td>
                                    <td>$<%# Eval("PrecioUnitario") %></td>
                                 
                                    <td>$<%# (Convert.ToDecimal(Eval("Cantidad")) * Convert.ToDecimal(Eval("PrecioUnitario"))).ToString("0.00") %></td>
                                </tr>
                            </ItemTemplate>

                            <FooterTemplate>
                                    </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </td>
            </tr>
        </ItemTemplate>

        <FooterTemplate>
                </tbody>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</main>
</asp:Content>
