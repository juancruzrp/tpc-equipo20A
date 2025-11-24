<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComprobanteVenta.aspx.cs" Inherits="WebApplication3.ComprobanteVenta" %>
 
        <!DOCTYPE html>
        <html lang="es">
        <head runat="server">
            <meta charset="utf-8" />
            <title>Comprobante de Venta</title>
            <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css" rel="stylesheet" />
            <style>                
           body { background-color: #f5f5f5; padding: 20px; }
            .factura {
                background: white;
                width: 210mm;
                min-height: 297mm;
                padding: 20mm;
                margin: 0 auto;
                box-shadow: 0px 0px 15px rgba(0,0,0,0.25);
                border: 1px solid #ccc;
                position: relative;
            }
            .marca-agua {
                position: absolute;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%) rotate(-25deg);
                opacity: 0.05;
                font-size: 140px;
                pointer-events: none;
                text-align: center;
                width: 100%;
            }
            .table th { background-color: #343a40; color: #fff; }
            .table td, .table th { padding: 0.5rem; }
            .total { font-size: 1.5rem; font-weight: bold; text-align: right; }
                
                </style>
        </head>
        <body>
            <form id="form1" runat="server">
                <!-- Botones -->
                <div class="text-center mb-4 no-imprimir">
                    <button type="button" class="btn btn-primary btn-lg" onclick="window.print()">🖨️ Imprimir</button>
                    <a href="HistorialVentas.aspx" class="btn btn-secondary btn-lg ms-2">Volver</a>
                </div>

                <div class="factura">
                    <!-- Encabezado -->
                    <div class="row mb-4">
                        <div class="col-8">
                            <h2 class="fw-bold mb-0">COMPROBANTE DE VENTA</h2>
                            <p>Sistema de gestion de comercios</p>
                        </div>
                        <div class="col-4 text-end">
                            <h4>ID Venta: <asp:Label ID="lblIDVenta" runat="server"></asp:Label></h4>
                            <p>Fecha: <asp:Label ID="lblFecha" runat="server"></asp:Label></p>
                            <p>Usuario: <asp:Label ID="lblUsuario" runat="server"></asp:Label></p>
                        </div>
                    </div>

                    <!-- Datos cliente -->
                    <div class="row mb-4">
                        <div class="col-12">
                            <h5 class="fw-bold">Cliente</h5>
                            <p><asp:Label ID="lblCliente" runat="server"></asp:Label></p>
                            <p class="text-muted">CUIT/CUIL: <asp:Label ID="lblClienteCuit" runat="server"></asp:Label></p>
                        </div>
                    </div>

                    <!-- Tabla detalle -->
                    <table class="table table-bordered mt-4">
                        <thead class="table-light">
                            <tr>
                                <th>Producto</th>
                                <th class="text-center">Cantidad</th>
                                <th class="text-end">Precio Unitario</th>
                                <th class="text-end">Subtotal</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="repDetalles" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("Producto.Nombre") %></td>
                                        <td class="text-center"><%# Eval("Cantidad") %></td>
                                        <td class="text-end"><%# Eval("PrecioUnitario", "{0:C}") %></td>
                                        <td class="text-end"><%# (Convert.ToDecimal(Eval("Cantidad")) * Convert.ToDecimal(Eval("PrecioUnitario"))).ToString("C") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>

                    <!-- Total final -->
                    <div class="row mt-4">
                        <div class="col-6"></div>
                        <div class="col-6 text-end total">
                            Total: <asp:Label ID="lblTotal" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </form>
        </body>
        </html>

