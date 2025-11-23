<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerFactura.aspx.cs" Inherits="WebApplication3.VerFactura" %>


<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Factura de Compra</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        body { background-color: #e9ecef; padding: 20px; }

        .factura {
            background: white;
            width: 210mm;
            min-height: 297mm;
            padding: 25mm;
            margin: 0 auto;
            box-shadow: 0px 0px 12px rgba(0,0,0,0.20);
            position: relative;
            border: 1px solid #ccc;
        }

        .cuadro-encabezado {
            border: 2px solid black;
            padding: 10px;
        }

        .marca-agua {
           position: absolute;
         top: 50%;
         left: 50%;
        transform: translate(-50%, -50%) rotate(-25deg);
        opacity: 0.07;
        font-size: 140px;
        pointer-events: none;
        width: 100%;
        text-align: center;
        }

        @media print {
            .no-imprimir { display: none !important; }
            body { background: white; margin: 0; padding: 0; }
            .factura { width: 100%; padding: 10mm; box-shadow: none; border: none; }
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">

        <!-- Botones -->
        <div class="text-center mb-4 no-imprimir">
            <button type="button" class="btn btn-primary btn-lg" onclick="window.print()">🖨️ Imprimir</button>
            <a href="ComprasListado.aspx" class="btn btn-secondary btn-lg ms-2">Volver</a>
        </div>

        <!-- Contenedor Factura -->
        <div class="factura">

            <div class="marca-agua">FERRETERIA</div>

            <!-- Encabezado -->
            <div class="cuadro-encabezado mb-4">
                <div class="row">
                    <div class="col-8">
                        <h2 class="fw-bold mb-0">FACTURA DE COMPRA</h2>
                        <h5>Ferretería Central</h5>
                        <p class="text-muted mb-0">Dirección: Av. Siempreviva 742</p>
                        <p class="text-muted">CUIT: 20-99999999-3</p>
                    </div>

                    <div class="col-4 text-end">
                        <h4 class="fw-bold">N°: <asp:Label ID="lblIDCompra" runat="server"></asp:Label></h4>
                        <p class="mb-1">Fecha: <asp:Label ID="lblFecha" runat="server"></asp:Label></p>
                    </div>
                </div>
            </div>

            <!-- Datos proveedor -->
            <div class="row mb-4">
                <div class="col-6">
                    <h5 class="fw-bold">Proveedor</h5>
                    <p class="mb-1"><asp:Label ID="lblProveedor" runat="server"></asp:Label></p>
                    <p class="text-muted">CUIT: <asp:Label ID="lblCuit" runat="server"></asp:Label></p>
                </div>

                <div class="col-6 text-end">
                    <h5 class="fw-bold">Usuario</h5>
                    <p><asp:Label ID="lblUsuario" runat="server"></asp:Label></p>
                </div>
            </div>

            <hr />

            <!-- Tabla detalle -->
            <table class="table table-bordered mt-4">
                <thead class="table-light">
                    <tr>
                        <th>Producto</th>
                        <th class="text-center">Cantidad</th>
                        <th class="text-end">Precio Unit.</th>
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
                                <td class="text-end">
                                    <%# (Convert.ToDecimal(Eval("Cantidad")) 
                                       * Convert.ToDecimal(Eval("PrecioUnitario")))
                                       .ToString("C") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>

            <!-- Total final -->
            <div class="row mt-4">
                <div class="col-6"></div>
                <div class="col-6 text-end">
                    <h3 class="fw-bold">
                        Total: <asp:Label ID="lblTotal" runat="server"></asp:Label>
                    </h3>
                </div>
            </div>

            <hr />

            <!-- Pie -->
            <div class="text-center mt-4">
                <p class="text-muted">Gracias por su compra</p>
            </div>

        </div>
    </form>

</body>
</html>