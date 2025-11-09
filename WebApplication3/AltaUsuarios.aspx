<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaUsuarios.aspx.cs" Inherits="WebApplication3.AltaUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-4">
        <div class="card shadow-sm border-0 p-4 mx-auto" style="max-width: 600px;">
            <h3 class="text-center mb-4">Agregar nuevo usuario</h3>

            <div class="mb-3">
                <label for="txtIdUsuario" class="form-label">ID del Usuario:</label>
                <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtNombreUsuario" class="form-label">Nombre de Usuario:</label>
                <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtContraseña" class="form-label">Contraseña:</label>
                <asp:TextBox ID="txtContraseña" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="ddlTipoUsuario" class="form-label">Tipo de usuario:</label>
                <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-control"
                    DataTextField="Descripcion" DataValueField="IDTipoUsuario">
                </asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="txtFecha" class="form-label">Fecha de Alta:</label>
                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
            <div class="mb-3">
                <div class="form-check form-switch">
                    <label class="form-check-label" for="CheckEstado">Usuario Activo</label>
                    <input runat="server" class="form-check-input" type="checkbox" id="CheckEstado" checked>
                </div>
            </div>
            <div class="d-flex justify-content-between">
                <asp:Button ID="btnGuardarUsuario" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardarUsuario_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" PostBackUrl="~/Usuarios.aspx" />
            </div>
        </div>
    </div>
</asp:Content>
