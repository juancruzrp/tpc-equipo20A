<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerFactura.aspx.cs" Inherits="WebApplication3.VerFactura" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Comprobante de Compra</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        body { background-color: #f2f2f2; padding: 20px; }
        .hoja-a4 {
            background-color: white;
            width: 210mm;
            min-height: 297mm;
            margin: 0 auto;
            padding: 20mm;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            position: relative;
        }
        @media print {
            .no-imprimir { display: none !important; }
            body { background-color: white; margin: 0; padding: 0; }
            .hoja-a4 { width: 100%; margin: 0; padding: 0; box-shadow: none; border: none; }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="text-center mb-4 no-imprimir">
            <button type="button" class="btn btn-primary btn-lg" onclick="window.print()">🖨️ Imprimir</button>
            <a href="ComprasListado.aspx" class="btn btn-secondary btn-lg ms-2">Volver</a>
        </div>

        <div class="hoja-a4">
           
            <div class="row mb-4">
                <div class="col-8">
                    <h1>COMPROBANTE</h1>
                    <h4>Ferreteria</h4>
                </div>
                <div class="col-4 text-end">
                    <h5>Nro: <asp:Label ID="lblIDCompra" runat="server" Font-Bold="true"></asp:Label></h5>
                    <p>Fecha: <asp:Label ID="lblFecha" runat="server"></asp:Label></p>
                </div>
            </div>
            


            <hr />

          
            <div class="row mb-4">
                <div class="col-6">
                    <h5 class="fw-bold">Proveedor:</h5>
                    <p class="mb-1"><asp:Label ID="lblProveedor" runat="server"></asp:Label></p>
                    <p class="text-muted">CUIT: <asp:Label ID="lblCuit" runat="server"></asp:Label></p>
                </div>
                <div class="col-6 text-end">
                    <h5 class="fw-bold">Usuario:</h5>
                    <p><asp:Label ID="lblUsuario" runat="server"></asp:Label></p>
                </div>
            </div>

          
            <table class="table table-bordered mt-4">
                <thead class="table-light">
                    <tr>
                        <th>Producto</th>
                        <th class="text-center">Cant.</th>
                        <th class="text-end">Precio</th>
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

            <div class="row mt-4">
                <div class="col-6"></div>
                <div class="col-6 text-end">
                    <h3>Total: <asp:Label ID="lblTotal" runat="server" CssClass="fw-bold"></asp:Label></h3>
                </div>
            </div>
        </div>
    </form>
</body>
</html>