<%@ Page Title="Ventas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="WebApplication3.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <main aria-labelledby="title">
    <h2 id="title">Nueva Venta</h2>

    <!-- Fecha -->
    <div class="mb-3">
        <label>Fecha</label>
        <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="Date" />
    </div>

    <!-- Cliente -->
    <div class="mb-3">
        <label>Cliente</label>
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        <asp:DropDownList ID="ddlClientes" runat="server" CssClass="form-control" AutoPostBack="true" 
            OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged">    
        </asp:DropDownList>
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Green"></asp:Label>
    </div>

    <div class="mb-3">
        <label>CUIT / CUIL</label>
        <asp:TextBox ID="txtCUIT" runat="server" CssClass="form-control" ReadOnly="true" />
    </div>

    <asp:HiddenField ID="hdnIDCliente" runat="server" />

        <!-- -->
        <div class="mb-3" style="position: relative;">
            <label>Producto</label>
            <input type="text" id="txtBuscarProducto" class="form-control" placeholder="Escriba para buscar..." autocomplete="off" />
            <ul id="listaProductos" class="list-group mt-1" style="position: absolute; width: 100%; max-height: 200px; overflow-y: auto; z-index: 1000;"></ul>
        </div>

        <asp:HiddenField ID="hdnIDProducto" runat="server" />
        <asp:Literal ID="litProductos" runat="server" />

        <h4>Productos en la venta</h4>
        <table class="table table-bordered" id="tablaVenta">
            <thead>
                <tr>
                    <th>Nombre</th>
                    <th>Precio</th>
                    <th>Cantidad</th>
                    <th>Stock</th>
                    <th>Acción</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
        <!-- -->
        
       
      <script>
            document.addEventListener("DOMContentLoaded", function () {
              const txtBuscar = document.getElementById("txtBuscarProducto");
              const listaProductos = document.getElementById("listaProductos");
              const hdnIDProducto = document.getElementById("<%= hdnIDProducto.ClientID %>");
              const tablaVenta = document.getElementById("tablaVenta").querySelector("tbody");
                          
            let carrito = [];

            function renderizarProductos(lista) {
                listaProductos.innerHTML = "";
                lista.forEach(p => {
                    const li = document.createElement("li");
                    li.className = "list-group-item list-group-item-action";
                    li.textContent = `${p.Nombre} - $${p.Precio.toFixed(2)} (Stock: ${p.Stock})`;
                    li.dataset.id = p.IDProducto;

                    li.addEventListener("click", () => {
                        agregarAlCarrito(p);
                        txtBuscar.value = "";
                        listaProductos.innerHTML = "";
                    });

                    listaProductos.appendChild(li);
                });
                listaProductos.style.display = lista.length > 0 ? "block" : "none";
            }

            txtBuscar.addEventListener("focus", function () {
                renderizarProductos(productos);
            });

            txtBuscar.addEventListener("input", function () {
                const texto = txtBuscar.value.toLowerCase().trim();
                const filtrados = productos.filter(p => p.Nombre.toLowerCase().includes(texto));
                renderizarProductos(filtrados);
            });

            document.addEventListener("click", function (e) {
                if (!txtBuscar.contains(e.target) && !listaProductos.contains(e.target)) {
                    listaProductos.style.display = "none";
                }
            });

            function agregarAlCarrito(p) {
                  const ddlClientes = document.getElementById("<%= ddlClientes.ClientID %>");

                // Verificar que se haya seleccionado un cliente
                if (!ddlClientes.value) {
                    alert("Debe seleccionar un cliente antes de agregar productos.");
                    return;
                }

                // Si ya está en el carrito, no agregar de nuevo
                if (carrito.find(x => x.IDProducto === p.IDProducto)) return;

                carrito.push({ ...p, cantidad: 1 }); // cantidad inicial 1
                renderizarCarrito();
            }

            function renderizarCarrito() {
                tablaVenta.innerHTML = "";
                carrito.forEach((p, index) => {
                    const tr = document.createElement("tr");

                    // Nombre
                    const tdNombre = document.createElement("td");
                    tdNombre.textContent = p.Nombre;
                    tr.appendChild(tdNombre);

                    // Precio (no editable)
                    const tdPrecio = document.createElement("td");
                    tdPrecio.textContent = p.Precio.toFixed(2);
                    tr.appendChild(tdPrecio);

                    const tdCantidad = document.createElement("td");
                    const inputCantidad = document.createElement("input");
                    inputCantidad.type = "number";
                    inputCantidad.value = p.cantidad;
                    inputCantidad.min = 1;
                    inputCantidad.max = p.Stock;
                    inputCantidad.className = "form-control";
                    inputCantidad.addEventListener("input", () => {
                        let val = parseInt(inputCantidad.value);

                        if (isNaN(val) || inputCantidad.value === "") val = 1;

                        if (val > p.Stock) val = p.Stock;
                        if (val < 1) val = 1;

                        p.cantidad = val;
                        inputCantidad.value = val;
                    });

                    tdCantidad.appendChild(inputCantidad);
                    tr.appendChild(tdCantidad);

                    // Stock
                    const tdStock = document.createElement("td");
                    tdStock.textContent = p.Stock;
                    tr.appendChild(tdStock);

                    // Botón quitar
                    const tdAccion = document.createElement("td");
                    const btnQuitar = document.createElement("button");
                    btnQuitar.className = "btn btn-danger btn-sm";
                    btnQuitar.textContent = "Quitar";
                    btnQuitar.addEventListener("click", () => {
                        carrito.splice(index, 1);
                        renderizarCarrito();
                    });
                    tdAccion.appendChild(btnQuitar);
                    tr.appendChild(tdAccion);

                    tablaVenta.appendChild(tr);
                });
            }
        });
      </script>


      

</main>

</asp:Content>
