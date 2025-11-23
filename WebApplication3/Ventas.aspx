<%@ Page Title="Ventas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="WebApplication3.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
       <h2 id="title">Nueva Venta</h2>

        <div class="mb-3">
            <label>Fecha</label>
            <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="Date" />
        </div>

         <div class="dropdown mb-3">
            <asp:TextBox ID="txtBuscarCliente" runat="server" CssClass="form-control" onfocus="mostrarListaClientes()" onclick="mostrarListaClientes()"
                         placeholder="Buscar cliente..." onkeyup="filtrarCliente()" AutoCompleteType="Disabled"></asp:TextBox>

            <div class="dropdown-menu show" id="listaClientes"
                 style="max-height: 200px; overflow-y: auto; display:none;">
                <asp:Literal ID="litClientes" runat="server"></asp:Literal>
            </div>
        </div>
        
        <div class="mb-3">
            <label>CUIT / CUIL</label>
            <asp:TextBox ID="txtCUIT" runat="server" CssClass="form-control" ReadOnly="true" />
        </div>

        <asp:HiddenField ID="hdnIDCliente" runat="server" />


        <!--  -->
        

        <div class="row mb-3">
                <div class="col">
                    <label>Proveedor</label>
                    <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-control" AutoPostBack="false" ClientIDMode="Static"></asp:DropDownList>
                </div>

                <div class="col">
                    <label>Categoría</label>
                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control" AutoPostBack="false" ClientIDMode="Static"></asp:DropDownList>
                </div>

                <div class="col">
                    <label>Marca</label>
                    <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control" AutoPostBack="false" ClientIDMode="Static"></asp:DropDownList>
                </div>
            </div>


       <div class="mb-3">
            <label>Buscar Producto</label>
            <asp:TextBox ID="txtBuscarProducto" runat="server" CssClass="form-control"
                         onfocus="mostrarListaProductos()" onclick="mostrarListaProductos()"
                         placeholder="Buscar producto..." onkeyup="filtrarProductos()"
                         AutoCompleteType="Disabled"></asp:TextBox>

            <div class="dropdown-menu show" id="listaProductos"
                 style="max-height: 200px; overflow-y: auto; display:none;">
                <asp:Literal ID="litProductos" runat="server"></asp:Literal>
            </div>
        </div>

        <!-- Tabla de productos agregados -->
        <h5>Productos agregados a la venta:</h5>
        <table class="table" id="tablaVenta">
            <thead>
                <tr>
                    <th>Producto</th>
                    <th>Precio</th>
                    <th>Cantidad</th>
                    <th>Subtotal</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody id="cuerpoTablaVenta">
                <!-- Aquí se irán agregando los productos -->
            </tbody>
        </table>

        <h5>Total: $<span id="totalVenta">0.00</span></h5>

        
        <!-- -->

        <script>
            function filtrarCliente() {
                var texto = document.getElementById("<%= txtBuscarCliente.ClientID %>").value.toLowerCase();
                var lista = document.getElementById("listaClientes");
             
                if (texto.trim() === "") {
                    document.getElementById("<%= txtCUIT.ClientID %>").value = "";
                    document.getElementById("<%= hdnIDCliente.ClientID %>").value = "";
                    lista.style.display = "none";
                    return;
                }

                var items = lista.getElementsByTagName("a");
                var hayCoincidencias = false;

                for (var i = 0; i < items.length; i++) {
                    var itemTexto = items[i].textContent.toLowerCase();
                    if (itemTexto.includes(texto)) {
                        items[i].style.display = "block";
                        hayCoincidencias = true;
                    } else {
                        items[i].style.display = "none";
                    }
                }

                lista.style.display = hayCoincidencias ? "block" : "none";
            }

            function seleccionarCliente(nombre, cuit, idCliente) {
                document.getElementById("<%= txtBuscarCliente.ClientID %>").value = nombre;
                document.getElementById("<%= txtCUIT.ClientID %>").value = cuit;
                document.getElementById("<%= hdnIDCliente.ClientID %>").value = idCliente;

                document.getElementById("listaClientes").style.display = "none";
            }

            function mostrarListaClientes() {
                var lista = document.getElementById("listaClientes");

                var items = lista.getElementsByTagName("a");
                for (var i = 0; i < items.length; i++) {
                    items[i].style.display = "block";
                }

                lista.style.display = "block";
            }

            document.addEventListener("click", function (event) {
                var txtBuscar = document.getElementById("<%= txtBuscarCliente.ClientID %>");
                var lista = document.getElementById("listaClientes");
                
                if (!txtBuscar.contains(event.target) && !lista.contains(event.target)) {
                    lista.style.display = "none";
                }
            });

            // ------------------------------------------------------------

            function mostrarListaProductos() {
                document.getElementById("listaProductos").style.display = "block";
            }

            function filtrarProductos() {
                var texto = document.getElementById("txtBuscarProducto").value.toLowerCase().trim();
                var items = document.getElementById("listaProductos").getElementsByTagName("a");

                for (var i = 0; i < items.length; i++) {
                    var nombre = items[i].textContent.toLowerCase();
                    items[i].style.display = nombre.includes(texto) ? "" : "none";
                }
            }

            function seleccionarProductoFromData(a) {
                var nombre = a.getAttribute("data-nombre");
                var precio = parseFloat(a.getAttribute("data-precio")) || 0;
                var id = a.getAttribute("data-id");

                seleccionarProducto(nombre, precio, id);
            }

            function seleccionarProducto(nombre, precio, id) {
                var tbody = document.getElementById("cuerpoTablaVenta");

                var cantidadInicial = 1;
                var subtotalInicial = precio * cantidadInicial;

                var fila = document.createElement("tr");
                fila.innerHTML = `
                    <td>${nombre}</td>
                    <td>${precio.toFixed(2)}</td>
                    <td><input type="number" value="${cantidadInicial}" min="1" class="form-control cantidad"></td>
                    <td class="subtotal">${subtotalInicial.toFixed(2)}</td>
                    <td><button type="button" class="btn btn-danger btn-sm">Eliminar</button></td>`;

                tbody.appendChild(fila);

                var inputCantidad = fila.querySelector("input.cantidad");
                inputCantidad.addEventListener("input", function () {
                    actualizarFila(inputCantidad, precio);
                });

                fila.querySelector("button").addEventListener("click", function () {
                    eliminarFila(fila);
                });

                document.getElementById("listaProductos").style.display = "none";
                document.getElementById("txtBuscarProducto").value = "";

                actualizarTotal();
            }

            function actualizarFila(input, precio) {
                var fila = input.closest("tr");
                var cantidad = parseInt(input.value) || 0;

                fila.cells[3].textContent = (precio * cantidad).toFixed(2);

                actualizarTotal();
            }

            function eliminarFila(fila) {
                fila.remove();
                actualizarTotal();
            }

            function actualizarTotal() {
                var total = 0;
                var filas = document.getElementById("cuerpoTablaVenta").rows;

                for (var i = 0; i < filas.length; i++) {
                    var subtotal = parseFloat(filas[i].cells[3].textContent) || 0;
                    total += subtotal;
                }

                document.getElementById("totalVenta").textContent = total.toFixed(2);
            }

        </script> 

    </main>
</asp:Content>
