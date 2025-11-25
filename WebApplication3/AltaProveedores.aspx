<%@ Page Title="Alta de Proveedor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaProveedores.aspx.cs" Inherits="WebApplication3.AltaProveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <div class="card shadow-sm border-0 p-4 mx-auto" style="max-width: 600px;">
            <h3 class="text-center mb-4">Agregar o modificar proveedor</h3>

            <div class="mb-3">
                <label for="txtIdProveedor" class="form-label">ID del Proveedor:</label>
                <asp:TextBox ID="txtIdProveedor" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtCUITCUIL" class="form-label">CUIT o CUIL:</label>
                <asp:TextBox ID="txtCUITCUIL" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre:</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtTelefono" class="form-label">Teléfono:</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtMail" class="form-label">Mail:</label>
                <asp:TextBox ID="txtMail" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtDireccion" class="form-label">Dirección:</label>
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtPorcentaje" class="form-label">Porcentaje:</label>
                <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

        <asp:Label class="mb-3" ID="lblError" runat="server" CssClass="text-danger"></asp:Label>

            <div class="mt-3 d-flex justify-content-between">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" PostBackUrl="~/Proveedores.aspx" />
            </div>
        </div>
    </div>


</asp:Content>