<%@ Page Title="Compras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="WebApplication3.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
           <h2 id="title">Registrar Nueva Compra</h2>

        <asp:HiddenField ID="hfIDProveedor" runat="server" ClientIDMode="Static" />
        <asp:Button ID="btnCargarCuit" runat="server" OnClick="btnCargarCuit_Click" Style="display:none;" ClientIDMode="Static" />

        <div class="row">
            <div class="col-md-6">

          <div class="row">
   
    <div class="col-md-6">
        <div class="dropdown">
            <asp:TextBox ID="txtBuscarProveedor" runat="server" CssClass="form-control"
                         placeholder="Buscar proveedor..." onkeyup="filtrarProveedor()" AutoCompleteType="Disabled"></asp:TextBox>

            <div class="dropdown-menu show w-100" id="listaProveedores" style="max-height: 200px; overflow-y: auto; display:none;">
                <asp:Literal ID="litProveedores" runat="server"></asp:Literal>
            </div>
        </div>
    </div>

    
    <div class="col-md-3">
        <div class="form-group">
            <label for="ddlMarca">Filtrar por Marca:</label>
            <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control"
                ClientIDMode="Static" onchange="filtrarProductoPorMarca()">
                <asp:ListItem Text="-- Todas --" Value="0" />
            </asp:DropDownList>
        </div>
    </div>
</div>

        <div class="form-group">
            <label>CUIT/CUIL del Proveedor:</label>
            <asp:TextBox ID="txtCuit" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
         </div>

                <div class="form-group">
                    <label for="txtFecha">Fecha de Compra:</label>
                    <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>
             
            </div>
     


        <asp:HiddenField ID="hfIDProducto" runat="server" ClientIDMode="Static" />

        <h3 class="mt-4 pt-3 border-top">Detalle de Productos</h3>
        <div class="row align-items-end"> 
             <div class="col-md-4">
                 <div class="form-group">
                    <label for="txtBuscarProducto">Producto:</label>
                        <div class="dropdown">
                <asp:TextBox ID="txtBuscarProducto" runat="server" CssClass="form-control" 
                             placeholder="-- Seleccione Producto --" 
                             onkeyup="filtrarProducto()" 
                             ClientIDMode="Static" 
                             AutoCompleteType="Disabled" 
                             Enabled="false"></asp:TextBox>

                <div class="dropdown-menu w-100" id="listaProductos" style="display:none; max-height: 200px; overflow-y: auto;">
                    <asp:Literal ID="litProductos" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>


         <div class="col-md-2">
            <div class="form-group">
        <label for="txtPrecioUnitario">Precio Unitario:</label>
        <asp:TextBox ID="txtPrecioUnitario" runat="server" CssClass="form-control" 
                     TextMode="Number" step="0.01" min="0"
                     ClientIDMode="Static" 
                     oninput="calcularSubtotal()"
                     onchange="validarPrecio()"></asp:TextBox>
    </div>
</div>


<div class="col-md-2">
    <div class="form-group">
        <label for="txtCantidad">Cantidad:</label>
        <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" 
                     TextMode="Number" Text="1" 
                       min="1" step="1" 
                     ClientIDMode="Static" 
                     oninput="calcularSubtotal()"></asp:TextBox>
    </div>
</div>

      
        <div class="col-md-2">
            <label class="form-label">Subtotal:</label>
            <div class="form-control-plaintext fw-bold">
                $ <span id="spanSubtotal">0.00</span>
            </div>
        </div>

        <div class="col-md-2 ">
            <asp:Button ID="btnAgregarProducto" runat="server" Text="Agregar" 
                        CssClass="btn btn-primary w-100" 
                        OnClick="btnAgregarProducto_Click" />
        </div>
    </div>

    
  
    <asp:GridView ID="dgvDetalleCompra" runat="server" CssClass="table table-bordered table-hover" 
                  AutoGenerateColumns="false" OnRowDeleting="dgvDetalleCompra_RowDeleting">
        <Columns>
            <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
            <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
            <asp:TemplateField HeaderText="Subtotal">
                <ItemTemplate>
                    <%# (Convert.ToDecimal(Eval("PrecioUnitario")) * Convert.ToInt32(Eval("Cantidad"))).ToString("C") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" ButtonType="Button" DeleteText="Quitar" ControlStyle-CssClass="btn btn-danger btn-sm" />
         </Columns>
    </asp:GridView>

        <div class="row mt-3">
    <div class="col-12 text-end">
        <h4>Total: $ 
            <asp:Label ID="lblTotalCompra" runat="server" Text="0.00"></asp:Label>
        </h4>
    </div>
</div>

       <div class="mt-4">
    <asp:Button ID="btnGuardarCompra" runat="server" Text="Guardar Compra" 
                CssClass="btn btn-success btn-lg" OnClick="btnGuardarCompra_Click" />
    
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                CssClass="btn btn-secondary btn-lg" CausesValidation="false" 
                OnClick="btnCancelar_Click" />
</div>

    <script>
        document.addEventListener('DOMContentLoaded', function () {

            
    const txtBuscarProveedor = document.getElementById('<%= txtBuscarProveedor.ClientID %>');
    const listaProveedores = document.getElementById('listaProveedores');
    const txtBuscarProducto = document.getElementById('txtBuscarProducto');
    const listaProductos = document.getElementById('listaProductos');
    const txtPrecioUnitario = document.getElementById('txtPrecioUnitario');
    const txtCantidad = document.getElementById('txtCantidad');
    const spanSubtotal = document.getElementById('spanSubtotal');
    let precioAnterior = parseFloat(txtPrecioUnitario.value) || 1;

   
    function calcularSubtotal() {
        let cantidad = parseFloat(txtCantidad.value) || 1;
        let precio = parseFloat(txtPrecioUnitario.value) || 0;
        spanSubtotal.innerText = (precio * cantidad).toFixed(2);
    }

    function validarCantidad() {
        if (txtCantidad.value < 1 || txtCantidad.value === "") {
            txtCantidad.value = 1;
        }
        calcularSubtotal();
    }

    function validarPrecio() {
        let valor = parseFloat(txtPrecioUnitario.value);
        if (isNaN(valor) || valor <= 0) {
            txtPrecioUnitario.value = precioAnterior.toFixed(2);
        } else {
            precioAnterior = valor;
        }
        calcularSubtotal();
    }

    txtPrecioUnitario.addEventListener('change', validarPrecio);
    txtPrecioUnitario.addEventListener('input', calcularSubtotal);

    txtCantidad.addEventListener('change', validarCantidad);
    txtCantidad.addEventListener('input', calcularSubtotal);

    
    txtBuscarProveedor.addEventListener('focus', function () {
        listaProveedores.style.display = 'block';
    });

    document.addEventListener('click', function (e) {
        if (e.target !== txtBuscarProveedor && !listaProveedores.contains(e.target)) {
            listaProveedores.style.display = 'none';
        }
    });

           window.filtrarProductoPorMarca = function () {
               
            let idMarcaSeleccionada = document.getElementById("ddlMarca").value;
            let listaProductos = document.getElementById('listaProductos');
            let items = listaProductos.querySelectorAll(".dropdown-item");

             items.forEach(item => {
                    let idMarcaProducto = item.getAttribute("data-idmarca");

                    if (idMarcaSeleccionada == "0" || idMarcaSeleccionada == idMarcaProducto) {
                        item.style.display = "block";
                    } else {
                        item.style.display = "none";
                    }
                });

                document.getElementById('txtBuscarProducto').value = "";
            }

            window.filtrarProducto = function () {
                let proveedorID = document.getElementById('<%= hfIDProveedor.ClientID %>').value;
            if (proveedorID === "") return;

             let input = txtBuscarProducto.value.toLowerCase();
            let idMarcaSeleccionada = document.getElementById("ddlMarca").value; 

            let items = listaProductos.querySelectorAll(".dropdown-item");
            listaProductos.style.display = 'block';

            items.forEach(item => {
            let texto = item.textContent.toLowerCase();
            let idMarcaProducto = item.getAttribute("data-idmarca"); 
            let coincideTexto = texto.includes(input);
            let coincideMarca = (idMarcaSeleccionada == "0" || idMarcaSeleccionada == idMarcaProducto);
            item.style.display = (coincideTexto && coincideMarca) ? "block" : "none";
        });
    }
    window.seleccionarProveedor = function (id, nombre, cuit) {
        txtBuscarProveedor.value = nombre;
        document.getElementById('<%= hfIDProveedor.ClientID %>').value = id;
        listaProveedores.style.display = 'none';
        document.getElementById('<%= btnCargarCuit.ClientID %>').click();
    }

    txtBuscarProducto.addEventListener('focus', function () {
        let proveedorID = document.getElementById('<%= hfIDProveedor.ClientID %>').value;
        if (listaProductos.innerHTML.trim() !== "" && proveedorID !== "") {
            listaProductos.style.display = 'block';
        } else if (proveedorID === "") {
            alert("Primero seleccioná un proveedor.");
            txtBuscarProducto.blur();
        }
    });

    window.filtrarProducto = function () {
        let proveedorID = document.getElementById('<%= hfIDProveedor.ClientID %>').value;
        if (proveedorID === "") return;
        let input = txtBuscarProducto.value.toLowerCase();
        let items = listaProductos.querySelectorAll(".dropdown-item");
        listaProductos.style.display = 'block';
        items.forEach(item => {
            let texto = item.textContent.toLowerCase();
            item.style.display = texto.includes(input) ? "block" : "none";
        });
    }

    window.seleccionarProducto = function (id, nombre, precio) {
        txtBuscarProducto.value = nombre;
        document.getElementById('hfIDProducto').value = id;
        txtPrecioUnitario.value = parseFloat(precio).toFixed(2);
        precioAnterior = parseFloat(precio);
        listaProductos.style.display = 'none';
        calcularSubtotal();
    }

});
    </script>


    </main>
</asp:Content>
