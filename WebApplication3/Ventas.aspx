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
        <asp:UpdatePanel ID="updFiltros" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

        <div class="row mb-3">
            <div class="col">
                <label>Proveedor</label>
                <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-control"
                    AutoPostBack="true" OnSelectedIndexChanged="Filtros_Changed"
                    DataTextField="Nombre" DataValueField="IDProveedor"></asp:DropDownList>
            </div>

            <div class="col">
                <label>Categoría</label>
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control"
                    AutoPostBack="true" OnSelectedIndexChanged="Filtros_Changed"
                    DataTextField="Nombre" DataValueField="IDCategoria"></asp:DropDownList>
            </div>

            <div class="col">
                <label>Marca</label>
                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control"
                    AutoPostBack="true" OnSelectedIndexChanged="Filtros_Changed"
                    DataTextField="Nombre" DataValueField="IDMarca"></asp:DropDownList>
            </div>
        </div>

        <asp:Literal ID="litProductos" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>

        
        <!-- -->
        <div class="mb-3" style="position: relative;">
            <label>Producto</label>
            <input type="text" id="txtBuscarProducto" class="form-control" placeholder="Escriba para buscar..." autocomplete="off" />
            <ul id="listaProductos" class="list-group mt-1" style="position: absolute; width: 100%; max-height: 200px; overflow-y: auto; z-index: 1000;"></ul>
        </div>

        <asp:HiddenField ID="hdnIDProducto" runat="server" />

        <h4>Productos en la venta</h4>
        <table class="table table-bordered" id="tablaVenta" >
            <thead>
                <tr>
                    <th>Nombre</th>
                    <th>Precio</th>
                    <th>Cantidad</th>
                    <th>Stock</th>
                    <th>Subtotal</th>
                    <th>Acción</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>

        <h4>Total: $ <span id="lblTotal">0.00</span></h4>
        <!-- -->

        <asp:HiddenField ID="hdnProductosVenta" runat="server" />

        <asp:Button ID="btnConfirmar" runat="server" CssClass="btn btn-success"
            Text="Confirmar Venta" OnClick="btnConfirmar_Click" 
            OnClientClick="return confirm('¿Está seguro que desea confirmar la venta?');" />
       
      <script>
          document.addEventListener("DOMContentLoaded", () => {

              const txt = document.getElementById("txtBuscarProducto");
              const lista = document.getElementById("listaProductos");
              const tabla = document.querySelector("#tablaVenta tbody");
              const lblTotal = document.getElementById("lblTotal");

              const hdnProductos = document.getElementById("<%= hdnProductosVenta.ClientID %>");

              lista.style.display = "none";

              txt.addEventListener("focus", () => mostrar(txt.value));
              txt.addEventListener("input", () => mostrar(txt.value));

              function mostrar(filtro) {
                  const f = filtro.toLowerCase().trim();
                  lista.innerHTML = "";

                  const prov = document.getElementById("<%= ddlProveedor.ClientID %>").value;
                  const cat = document.getElementById("<%= ddlCategoria.ClientID %>").value;
                  const marca = document.getElementById("<%= ddlMarca.ClientID %>").value;

                  const filtrados = productos.filter(p => {
                      const cumpleTexto = p.Nombre.toLowerCase().includes(f);
                      const cumpleProv = !prov || p.IDProveedor.toString() === prov;
                      const cumpleCat = !cat || p.Categoria.IDCategoria.toString() === cat;
                      const cumpleMarca = !marca || p.Marca.IDMarca.toString() === marca;
                      return cumpleTexto && cumpleProv && cumpleCat && cumpleMarca;
                  });

                  if (!filtrados.length) { lista.style.display = "none"; return; }

                  filtrados.forEach(p => {
                      const a = document.createElement("a");
                      a.className = "list-group-item list-group-item-action";
                      a.textContent = `${p.Nombre} - $${p.PrecioVenta.toFixed(2)}`;
                      a.onclick = () => seleccionar(p);
                      lista.appendChild(a);
                  });

                  lista.style.display = "block";
              }

              function seleccionar(p) {
                  const ddlClientes = document.getElementById("<%= ddlClientes.ClientID %>");
                  if (!ddlClientes.value || ddlClientes.value === "0") {
                      alert("Debe seleccionar un cliente antes de agregar productos.");
                      return;
                  }

                  if ([...tabla.querySelectorAll("tr")].some(tr => tr.dataset.id == p.IDProducto)) {

                      // ocultar la lista desplegable
                      lista.style.display = "none";

                      // limpiar el campo de búsqueda
                      txt.value = "";

                      // mensaje
                      alert("Este producto ya fue agregado a la venta.");

                      return;
                  }

                  lista.style.display = "none";
                  txt.value = "";

                  const fila = document.createElement("tr");
                  fila.dataset.id = p.IDProducto;

                  fila.innerHTML = `
                    <td>${p.Nombre}</td>
                    <td class="text-center">${p.PrecioVenta.toFixed(2)}</td>
                    <td><input type="number" class="form-control cantidad" value="1" min="1" max="${p.Stock}"></td>
                    <td class="text-center">${p.Stock}</td>
                    <td class="text-center subtotal">${p.PrecioVenta.toFixed(2)}</td>
                    <td><button class="btn btn-danger btn-sm">Quitar</button></td>`;

                  fila.querySelector(".cantidad").oninput = () => actualizarSubtotal(fila, p.PrecioVenta);

                  fila.querySelector("button").onclick = () => { fila.remove(); actualizarTotal(); };

                  tabla.appendChild(fila);
                  actualizarTotal();
              }

              function actualizarSubtotal(fila, precio) {
                  const input = fila.querySelector(".cantidad");
                  let cant = parseInt(input.value) || 1;
                  const stock = parseInt(input.max);  

                  if (cant > stock) cant = stock;  
                  if (cant < 1) cant = 1;

                  input.value = cant;

                  const sub = (cant * precio).toFixed(2);
                  fila.querySelector(".subtotal").textContent = sub;

                  actualizarTotal();
              }

              function actualizarTotal() {
                  let total = 0;
                  tabla.querySelectorAll(".subtotal").forEach(s => {
                      total += parseFloat(s.textContent);
                  });
                  lblTotal.textContent = total.toFixed(2);
              }

              document.addEventListener("click", function (e) {
                  const esClickDentroDeTxt = e.target === txt;
                  const esClickDentroDeLista = lista.contains(e.target);

                  if (!esClickDentroDeTxt && !esClickDentroDeLista) {
                      lista.style.display = "none";
                  }
              });

              const btnConfirmar = document.getElementById("<%= btnConfirmar.ClientID %>");
                btnConfirmar.addEventListener("click", function () {
                    const productosVenta = [...tabla.querySelectorAll("tr")].map(fila => ({
                        IDProducto: fila.dataset.id,
                        Nombre: fila.cells[0].textContent,
                        Precio: parseFloat(fila.cells[1].textContent),
                        Cantidad: parseInt(fila.querySelector(".cantidad").value),
                        Subtotal: parseFloat(fila.cells[4].textContent)
                    }));

                    // guardar en el hidden field como JSON
                    hdnProductos.value = JSON.stringify(productosVenta);

              });

              function actualizarHiddenField() {
                  const productos = [...tabla.querySelectorAll("tr")].map(fila => ({
                      IDProducto: parseInt(fila.dataset.id),
                      Nombre: fila.cells[0].textContent,
                      Precio: parseFloat(fila.cells[1].textContent),
                      Cantidad: parseInt(fila.querySelector(".cantidad").value)
                  }));

                  hdnProductos.value = JSON.stringify(productos);

              }
          });
      </script>










      

</main>

</asp:Content>
