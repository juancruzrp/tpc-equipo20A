<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductosAgregar.aspx.cs" Inherits="WebApplication3.AgregarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-4">
        <div class="card shadow-sm border-0 p-4 mx-auto" style="max-width: 600px;">
            <h3 class="text-center mb-4">Agregar nuevo producto</h3>

            <div class="mb-3">
                <label for="lblId" class="form-label">ID Producto</label>
                <asp:TextBox ID="txtIDProducto" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="lblNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingrese el nombre del producto"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="lblDescripcion" class="form-label">Descripción</label>
                <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Descripción del producto"></asp:TextBox>
            </div>
            
            <div class="mb-3">
                <label for="lblPrecio" class="form-label">Precio</label>
                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" placeholder="Ej: 1999.99"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="lblStock" class="form-label">Stock</label>
                <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" placeholder="Cantidad disponible"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="lblCategoria" class="form-label">Categoría</label>
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>

            <div class="mb-3">
                <label for="lblMarca" class="form-label">Marca</label>
                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>

            <div class="mb-3">
                <label for="lblImagenUrl" class="form-label">URL de Imagen</label>
                <asp:TextBox ID="txtImagenUrl" runat="server" CssClass="form-control" placeholder="https://..."></asp:TextBox>
                <asp:Image ID="imgPreview" runat="server" CssClass="img-thumbnail mt-3" Width="150px" />
            </div>

            <div class="form-check form-switch mb-4">                
                <label class="form-check-label" for="chkActivo">Estado</label>
                <br />
                <asp:CheckBox ID="chkEstado" runat="server" Text="Activo" Checked="true" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />

            <br />

            <div class="d-flex justify-content-between">
                
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-success" OnClick="btnAceptar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" PostBackUrl="~/Productos.aspx" />
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        // Vista previa automática de imagen
        $(document).ready(function () {
            $('#<%= txtImagenUrl.ClientID %>').on('input', function () {
                $('#<%= imgPreview.ClientID %>').attr('src', $(this).val());
            });
        });
    </script>

</asp:Content>
