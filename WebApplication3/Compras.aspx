<%@ Page Title="Compras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="WebApplication3.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
           <h2 id="title">Registrar Nueva Compra</h2>

        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="ddlProveedor">Proveedor:</label>
                    <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-control"
                        DataTextField="Nombre" DataValueField="IDProveedor">           
                    </asp:DropDownList>
                </div>

                

                <div class="form-group">
                    <label for="txtFecha">Fecha de Compra:</label>
                    <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label>Usuario:</label>
                    <asp:Label ID="lblUsuario" runat="server" CssClass="form-control-static"></asp:Label>
                </div>
                <div class="form-group">
                    <label>Total de la Compra:</label>
                    <asp:Label ID="lblTotalCompra" runat="server" CssClass="form-control-static" Text="0.00"></asp:Label>
                </div>
            </div>
        </div>

        <h3>Detalle de Productos</h3>
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label for="ddlProducto">Producto:</label>
                    <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-control"
                        DataTextField="Nombre" DataValueField="IDProducto">
                        
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <label for="txtPrecioUnitario">Precio Unitario:</label>
                    <asp:TextBox ID="txtPrecioUnitario" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <label for="txtCantidad">Cantidad:</label>
                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number" Text="1"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <label for="lblSubtotalDetalle">Subtotal:</label>
                    <asp:Label ID="lblSubtotalDetalle" runat="server" CssClass="form-control-static" Text="0.00"></asp:Label>
                </div>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnAgregarProducto" runat="server" Text="Agregar" CssClass="btn btn-primary mt-4" />
            </div>
        </div>

        <asp:GridView ID="dgvDetalleCompra" runat="server" CssClass="table table-bordered mt-3" AutoGenerateColumns="false" ShowFooter="true">
            <Columns>
                <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" DataFormatString="{0:N0}" />
                <asp:BoundField DataField="PrecioUnitario" HeaderText="P. Unitario" DataFormatString="{0:C}" />
                <asp:TemplateField HeaderText="Subtotal">
                    <ItemTemplate>
                        
                        <%# (Convert.ToDecimal(Eval("Cantidad")) * Convert.ToDecimal(Eval("PrecioUnitario"))).ToString("C") %>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblFooterTotal" runat="server" Text="Total: 0.00"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowDeleteButton="True" ButtonType="Button" DeleteText="Quitar" />
            </Columns>
        </asp:GridView>

        <div class="mt-4">
            <asp:Button ID="btnGuardarCompra" runat="server" Text="Guardar Compra" CssClass="btn btn-success btn-lg" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary btn-lg" CausesValidation="false" PostBackUrl="~/Default.aspx" />
        </div>
    </main>
</asp:Content>
