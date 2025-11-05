<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="WebApplication3.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2>Usuarios</h2>
        <asp:GridView ID="dgvUsuarios" CssClass="table" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="IDUsuario" HeaderText="ID" />
                <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre de Usuario" />
                <asp:BoundField DataField="Contraseña" HeaderText="Contraseña" />
                <asp:BoundField DataField="TipoUsuario.Descripcion" HeaderText="Tipo de Usuario" />
                <asp:BoundField DataField="FechaAlta" HeaderText="Fecha Alta" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="FechaBaja" HeaderText="Fecha Baja" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:CheckBoxField DataField="Estado" HeaderText="Activo" />
            </Columns>
        </asp:GridView>
        <asp:Button Text="Agregar Usuario" ID="btnAgregar" runat="server" class="btn btn-outline-primary" OnClick="btnAgregar_Click" />
    </main>
</asp:Content>
