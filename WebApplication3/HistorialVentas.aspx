<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialVentas.aspx.cs" Inherits="WebApplication3.HistorialVentas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="LabelBuscar" runat="server" Text="Buscar Venta:" ></asp:Label>
    <asp:TextBox ID="txtBuscar" runat="server" placeholder="Ingrese Fecha..." CssClass="form-control" Width="300px" />
    <br />
    
        <asp:Repeater ID="repVentas" runat="server">
            <HeaderTemplate>
                <table class="table table-hover align-middle">
                    <thead class="table-dark">
                        <tr>
                            <th>ID</th>
                            <th>Fecha</th>
                            <th>Cliente</th>
                            <th>Usuario</th>
                            <th>Total</th>
                            <th>Acción</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>

            <ItemTemplate>
                <tr>
                    <td><%# Eval("IDVenta") %></td>
                    <td><%# ((DateTime)Eval("Fecha")).ToString("dd/MM/yyyy") %></td>
                    <td><%# Eval("Cliente.Nombre") + " " + Eval("Cliente.Apellido") %></td>
                    <td><%# Eval("Usuario.NombreUsuario") %></td>
                    <td>$<%# Eval("Total") %></td>
                    <td>
                        <button class="btn btn-outline-primary btn-sm" type="button" data-bs-toggle="collapse"
                                data-bs-target="#detalle_<%# Eval("IDVenta") %>" aria-expanded="false"
                                aria-controls="detalle_<%# Eval("IDVenta") %>">
                            Ver Detalle
                        </button>                       

                        <a href='ComprobanteVenta.aspx?id=<%# Eval("IDVenta") %>' target="_blank"class="btn btn-dark btn-sm ms-1">
                            📄 Ver Comprobante
                        </a>                 

                    </td>
                </tr>
                <tr class="collapse" id="detalle_<%# Eval("IDVenta") %>">
                    <td colspan="6">
                        <div class="p-3 bg-light border rounded">
                            <h6>Detalles de la venta</h6>
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
                                        <td>$<%# Eval("PrecioUnitario", "{0:0.00}") %></td>
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


</asp:Content>
