<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaClientes.aspx.cs" Inherits="WebApplication3.AltaClientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <div class="card shadow-sm border-0 p-4 mx-auto" style="max-width: 600px;">
            <h3 class="text-center mb-4">Agregar nuevo cliente</h3>

            <div class="mb-3">
                <label for="txtIdCliente" class="form-label">ID del Cliente:</label>
                <asp:TextBox ID="txtIdCliente" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtCUITCUIL" class="form-label">CUIT o CUIL del cliente:</label>
                <asp:TextBox ID="txtCUITCUIL" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtNombreCliente" class="form-label">Nombre del cliente:</label>
                <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="form-control"></asp:TextBox>
                
            </div>

            <div class="mb-3">
                <label for="txtApellidoCliente" class="form-label">Apellido del cliente:</label>
                <asp:TextBox ID="txtApellidoCliente" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtTelefono" class="form-label">Telefono:</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtMail" class="form-label">Mail:</label>
                <asp:TextBox ID="txtMail" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtDireccion" class="form-label">Direccion:</label>
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="d-flex justify-content-between">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" 
                     OnClientClick="return confirm('¿Seguro desea cancelar esta operación?');"/>
            </div>

            <asp:Label ID="lblError" runat="server" CssClass="text-danger mt-3"></asp:Label>
        </div>
    </div>
</asp:Content>
