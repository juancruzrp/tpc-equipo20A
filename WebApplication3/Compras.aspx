<%@ Page Title="Compras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="WebApplication3.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
           <h2 id="title">Registrar Nueva Compra</h2>

        <asp:HiddenField ID="hfIDProveedor" runat="server" ClientIDMode="Static" />
        <asp:Button ID="btnCargarCuit" runat="server" OnClick="btnCargarCuit_Click" Style="display:none;" ClientIDMode="Static" />

        <div class="row">
            <div class="col-md-6">

                <div class="form-group">
                    <label for="ddlProveedor">Proveedor:</label>
                    <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-control"
                        DataTextField="Nombre" DataValueField="IDProveedor">           
                    </asp:DropDownList>
                </div>
               
          <div class="dropdown">
            <asp:TextBox ID="txtBuscarProveedor" runat="server" CssClass="form-control" 
                         placeholder="Buscar proveedor..." onkeyup="filtrarProveedor()" AutoCompleteType="Disabled"></asp:TextBox>

            <div class="dropdown-menu show w-100" id="listaProveedores" style="max-height: 200px; overflow-y: auto; display:none;">
                <asp:Literal ID="litProveedores" runat="server"></asp:Literal>
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



        <asp:HiddenField ID="hfIDProducto" runat="server" ClientIDMode="Static" />

        <h3>Detalle de Productos</h3>
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
                     TextMode="Number" step="0.01" 
                     ClientIDMode="Static" 
                     oninput="calcularSubtotal()"></asp:TextBox>
    </div>
</div>


<div class="col-md-2">
    <div class="form-group">
        <label for="txtCantidad">Cantidad:</label>
        <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" 
                     TextMode="Number" Text="1" 
                     ClientIDMode="Static" 
                     oninput="calcularSubtotal()"></asp:TextBox>
    </div>
</div>


<div class="col-md-2">
    <div class="form-group">
        <label for="lblSubtotalDetalle">Subtotal:</label>
        <div class="form-control-static">
            <span id="spanSubtotal">0.00</span>
            <asp:Label ID="lblSubtotalDetalle" runat="server" Visible="false"></asp:Label> 
        </div>
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

        <div class="mt-4">
            <asp:Button ID="btnGuardarCompra" runat="server" Text="Guardar Compra" CssClass="btn btn-success btn-lg" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary btn-lg" CausesValidation="false" PostBackUrl="~/Default.aspx" />
        </div>


    <script>
        var txtBuscar = document.getElementById('<%= txtBuscarProveedor.ClientID %>');
        var lista = document.getElementById('listaProveedores');

        txtBuscar.addEventListener('focus', function () {
            lista.style.display = 'block';
        });
        document.addEventListener('click', function (e) {
            if (e.target !== txtBuscar && e.target.parentNode !== lista) {
                lista.style.display = 'none';
            }
        });

        function filtrarProveedor() {
            let input = txtBuscar.value.toLowerCase();
            let items = document.querySelectorAll("#listaProveedores .dropdown-item");

            lista.style.display = 'block'; 

            items.forEach(item => {
                let texto = item.textContent.toLowerCase();
                item.style.display = texto.includes(input) ? "block" : "none";
            });
        }

        function seleccionarProveedor(id, nombre, cuit) {
            document.getElementById('<%= txtBuscarProveedor.ClientID %>').value = nombre;
        
        document.getElementById('<%= hfIDProveedor.ClientID %>').value = id;

      
        lista.style.display = 'none';
        document.getElementById('<%= btnCargarCuit.ClientID %>').click();
        }

        var txtBuscarProd = document.getElementById('txtBuscarProducto');
        var listaProd = document.getElementById('listaProductos');

        txtBuscarProd.addEventListener('focus', function () {
            
            var proveedorID = document.getElementById('<%= hfIDProveedor.ClientID %>').value;
                if (listaProd.innerHTML.trim() !== "" && proveedorID !== "") {
            listaProd.style.display = 'block';
                } else if (proveedorID === "") {
                     alert("Primero seleccioná un proveedor.");
                         txtBuscarProd.blur(); 
                     }
                        });

        function filtrarProducto() {
            var proveedorID = document.getElementById('<%= hfIDProveedor.ClientID %>').value;
            if (proveedorID === "") return; 

            let input = txtBuscarProd.value.toLowerCase();
            let items = listaProd.querySelectorAll('.dropdown-item');
            listaProd.style.display = 'block';

            items.forEach(item => {
                let texto = item.textContent.toLowerCase();
                item.style.display = texto.includes(input) ? "block" : "none";
            });
        }

        function seleccionarProducto(id, nombre, precio) {
            txtBuscarProd.value = nombre;
            document.getElementById('hfIDProducto').value = id;
            let precioStr = precio.toString().replace(',', '.');
            document.getElementById('txtPrecioUnitario').value = precioStr;

            listaProd.style.display = 'none';
        }



        function seleccionarProducto(id, nombre, precio) {
            document.getElementById('txtBuscarProducto').value = nombre;
            document.getElementById('hfIDProducto').value = id;

            let precioStr = precio.toString().replace(',', '.');

            let inputPrecio = document.getElementById('txtPrecioUnitario');
            inputPrecio.value = precioStr;

            // Ocultar la lista
            document.getElementById('listaProductos').style.display = 'none';
            calcularSubtotal();

            inputPrecio.focus();
            inputPrecio.select();
        }

        function calcularSubtotal() {
            let precio = parseFloat(document.getElementById('txtPrecioUnitario').value) || 0;
            let cantidad = parseFloat(document.getElementById('txtCantidad').value) || 0;
            document.getElementById('spanSubtotal').innerText = (precio * cantidad).toFixed(2);
        }

        document.addEventListener('click', function (e) {
            if (e.target !== txtBuscar && !lista.contains(e.target)) lista.style.display = 'none';
            if (e.target !== txtBuscarProd && !listaProd.contains(e.target)) listaProd.style.display = 'none';
        });

    </script>


    </main>
</asp:Content>
