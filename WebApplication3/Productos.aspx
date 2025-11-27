<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="WebApplication3.Productos" %>

<%@ Import Namespace="WebApplication3.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="text-center mb-4">Listado de Productos</h2>

    <div class="d-flex justify-content-center mb-4">
        <asp:Label ID="lblBuscar" runat="server" Text="Buscar: " CssClass="me-2 fw-bold"></asp:Label>
        <asp:TextBox ID="txtBuscar" runat="server" AutoPostBack="true" CssClass="form-control w-50" placeholder="Ingrese nombre o marca..." OnTextChanged="txtBuscar_TextChanged" />
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Repeater ID="repProductos" runat="server" OnItemCommand="repProductos_ItemCommand">
                <ItemTemplate>
                    <div class="card mb-3 shadow-sm border-0">
                        <div class="row g-0 align-items-center">

                            <div class="col-md-3 text-center">
                                <img src='<%# Eval("ImagenUrl") %>'
                                    alt='<%# Eval("Nombre") %>'
                                    class="img-fluid p-2 rounded"
                                    style="max-height: 150px; object-fit: contain;"
                                    onerror="this.src='https://us.123rf.com/450wm/koblizeek/koblizeek2208/koblizeek220800128/190320173-no-image-vector-symbol-missing-available-icon-no-gallery-for-this-moment-placeholder.jpg';" />
                            </div>

                            <div class="col-md-9">
                                <div class="card-body text-start">
                                    <h5 class="card-title mb-1"><%# Eval("Nombre") %></h5>
                                    <p class="text-muted mb-1">Marca: <%# Eval("Marca.Nombre") %></p>
                                    <p class="text-muted mb-1">Proveedor: <%# Eval("NombreProveedor") %></p>
                                    <p class="mb-1">Precio de venta: $<%# string.Format("{0:N2}", Eval("PrecioVenta")) %></p>
                                    <p class="mb-1">Stock: <%# Eval("Stock") %></p>
                                    <p class="text-success mb-1"><%# (bool)Eval("Estado") ? "Activo" : "Inactivo" %></p>
                                    <%if (SesionHelper.EsUsuarioAdmin(Session))
                                        {%>
                                    <div class="d-flex gap-2 mt-2">
                                        <asp:Button ID="btnModificar" runat="server" Text="Modificar"
                                            CommandName="Modificar"
                                            CommandArgument='<%# Eval("IDProducto") %>'
                                            CssClass="btn btn-warning btn-sm" />

                                        <asp:Button ID="btnEliminar" runat="server"
                                            Text='<%# (bool)Eval("Estado") ? "Inactivar" : "Activar" %>'
                                            CommandName="Eliminar"
                                            CommandArgument='<%# Eval("IDProducto") %>'
                                            CssClass='<%# (bool)Eval("Estado") ? "btn btn-danger btn-sm" : "btn btn-success btn-sm" %>'
                                            OnClientClick='<%# (bool)Eval("Estado") 
                                            ? "return confirm(\"¿Seguro que desea inactivar este producto?\");" 
                                            : "return confirm(\"¿Seguro que desea activar este producto?\");" %>' />
                                        <%} %>
                                    </div>
                                </div>
                            </div>
                        </div>
                </ItemTemplate>
            </asp:Repeater>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtBuscar" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="repProductos" EventName="ItemCommand" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        $(document).ready(function () {
            let timer;
            $('#<%= txtBuscar.ClientID %>').on('input', function () {
                clearTimeout(timer);
                timer = setTimeout(function () {
                    __doPostBack('<%= txtBuscar.UniqueID %>', '');
                }, 200);
            });
        });
    </script>

</asp:Content>
