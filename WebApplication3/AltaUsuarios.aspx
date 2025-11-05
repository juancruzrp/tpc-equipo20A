<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaUsuarios.aspx.cs" Inherits="WebApplication3.AltaUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-6">
            <h2>Alta de Usuarios</h2>
            <br />
            <div class="form-group">
                <label for="txtNombreUsuario">Nombre de Usuario:</label>
                <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtContraseña">Contraseña:</label>
                <asp:TextBox ID="txtContraseña" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="ddlTipoUsuario">Tipo de usuario:</label>
                <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-control"
                    DataTextField="Descripcion" DataValueField="IDTipoUsuario">
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label for="txtFecha">Fecha de Alta:</label>
                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="form-group">
                <div class="form-check form-switch">
                    <input class="form-check-input" type="checkbox" role="switch" id="switchCheckActivo" checked>
                    <label class="form-check-label" for="switchCheckActivo">Usuario Activo</label>
                </div>
            </div>
        </div>
        <div class="col-md-6">
        <asp:Button Text="Guardar" ID="btnGuardarUsuario" CssClass="btn btn-outline-primary" runat="server" OnClick="btnGuardarUsuario_Click" />
        </div>
        </div>
</asp:Content>
