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

        

        <script>
            function filtrarCliente() {
                var texto = document.getElementById("<%= txtBuscarCliente.ClientID %>").value.toLowerCase();
                var lista = document.getElementById("listaClientes");

                // 👉 Si el usuario borró todo, limpiar CUIT y IDCliente
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

                // Mostrar todos los ítems al hacer clic
                var items = lista.getElementsByTagName("a");
                for (var i = 0; i < items.length; i++) {
                    items[i].style.display = "block";
                }

                lista.style.display = "block";
            }

            document.addEventListener("click", function (event) {
                var txtBuscar = document.getElementById("<%= txtBuscarCliente.ClientID %>");
                var lista = document.getElementById("listaClientes");

                // Si el clic NO fue dentro del textbox ni dentro de la lista → ocultar
                if (!txtBuscar.contains(event.target) && !lista.contains(event.target)) {
                    lista.style.display = "none";
                }
            });

            // ------------------------------------------------------------

            

        </script>

        




    </main>
</asp:Content>
