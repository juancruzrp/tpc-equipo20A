<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaUsuarios.aspx.cs" Inherits="WebApplication3.AltaUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-4">
        <div class="card shadow-sm border-0 p-4 mx-auto" style="max-width: 600px;">
            <h3 class="text-center mb-4">Agregar nuevo usuario</h3>

             <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                CssClass="alert alert-danger"
                HeaderText="Por favor, corrija los siguientes errores:" />


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
                <asp:RequiredFieldValidator ID="rfvContraseña" runat="server"
                    ControlToValidate="txtContraseña"
                    ErrorMessage="La contraseña es obligatoria."
                    Display="Dynamic" CssClass="text-danger" />
                <asp:RegularExpressionValidator ID="revContraseña" runat="server"
                    ControlToValidate="txtContraseña"
                    ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$"
                    ErrorMessage="La contraseña debe tener al menos 6 caracteres, incluyendo letras y números."
                    Display="Dynamic" CssClass="text-danger" />

            </div>

            <div class="mb-3">
                <label for="ddlTipoUsuario" class="form-label">Tipo de usuario:</label>
                <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-control"
                    DataTextField="Descripcion" DataValueField="IDTipoUsuario">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvTipoUsuario" runat="server"
                    ControlToValidate="ddlTipoUsuario"
                    InitialValue=""
                    ErrorMessage="Debe seleccionar un tipo de usuario."
                    Display="Dynamic" CssClass="text-danger" />

            </div>


            <div class="mb-3">
                <label for="txtFecha" class="form-label">Fecha de Alta:</label>
                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
            <div class="d-flex justify-content-between">
                <asp:Button ID="btnGuardarUsuario" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardarUsuario_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" PostBackUrl="~/Usuarios.aspx" />
            </div>
             <asp:Label ID="lblError" runat="server" CssClass="text-danger mt-3"></asp:Label>
        </div>
    </div>
</asp:Content>
