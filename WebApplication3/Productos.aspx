<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="WebApplication3.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="text-center mb-4">Listado de Productos</h2>

    <!-- Campo de búsqueda -->
    <div class="d-flex justify-content-center mb-4">
        <asp:Label ID="lblBuscar" runat="server" Text="Buscar: " CssClass="me-2 fw-bold"></asp:Label>
        <asp:TextBox ID="txtBuscar" runat="server" AutoPostBack="true" CssClass="form-control w-50" placeholder="Ingrese nombre o marca..." OnTextChanged="txtBuscar_TextChanged" />
    </div>

    <!-- UpdatePanel para actualizar solo el listado -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Repeater ID="repProductos" runat="server">
                <ItemTemplate>
                    <div class="card mb-3 shadow-sm border-0">
                        <div class="row g-0 align-items-center">
                            <!-- Imagen -->
                            <div class="col-md-3 text-center">
                                <img src='<%# Eval("ImagenUrl") %>' 
                                     alt='<%# Eval("Nombre") %>' 
                                     class="img-fluid p-2 rounded" 
                                     style="max-height: 150px; object-fit: contain;" 
                                     onerror="this.src='https://via.placeholder.com/150x150?text=Sin+imagen';" />
                            </div>

                            <!-- Datos -->
                            <div class="col-md-9">
                                <div class="card-body text-start">
                                    <h5 class="card-title mb-1"><%# Eval("Nombre") %></h5>
                                    <p class="text-muted mb-1">Marca: <%# Eval("Marca.Nombre") %></p>
                                    <p class="mb-1">Precio: $<%# string.Format("{0:N2}", Eval("Precio")) %></p>
                                    <p class="mb-1">Stock: <%# Eval("Stock") %></p>
                                    <p class="text-success mb-1"><%# (bool)Eval("Estado") ? "Activo" : "Inactivo" %></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtBuscar" EventName="TextChanged" />
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