<%@ Page Title="Ventas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="WebApplication3.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
       <h2 id="title">Nueva Venta</h2>

         <div class="dropdown">
            <asp:TextBox ID="txtBuscarCliente" runat="server" CssClass="form-control"
                         placeholder="Buscar cliente..." onkeyup="filtrarCliente()" AutoCompleteType="Disabled"></asp:TextBox>

            <div class="dropdown-menu show w-100" id="listaClientes"
                 style="max-height: 200px; overflow-y: auto; display:none;">
                <asp:Literal ID="litClientes" runat="server"></asp:Literal>
            </div>
        </div>

        <!-- Campo CUIT -->
        <div class="mt-2">
            <label>CUIT / CUIL</label>
            <asp:TextBox ID="txtCUIT" runat="server" CssClass="form-control" ReadOnly="true" />
        </div>

        <script>
            function filtrarCliente() {
                var texto = document.getElementById("<%= txtBuscarCliente.ClientID %>").value.toLowerCase();
                var lista = document.getElementById("listaClientes");

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
        </script>

        <asp:HiddenField ID="hdnIDCliente" runat="server" />


    </main>
</asp:Content>
