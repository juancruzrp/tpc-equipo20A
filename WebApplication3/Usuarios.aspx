<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="WebApplication3.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

      <style>
        
        .table tr.selectedRowHighlight {
            background-color: #cce5ff; 
            font-weight: bold;
            color: #004085;
        }

        
        .table tr[style*="cursor:pointer"]:hover {
            background-color: #e0e0e0 !important; 
        }
    </style>


    <main aria-labelledby="title">
       <h2>Usuarios</h2>
<asp:GridView ID="dgvUsuarios"
CssClass="table"
runat="server"
AutoGenerateColumns="False"
DataKeyNames="IDUsuario"
OnRowDataBound="dgvUsuarios_RowDataBound"
OnSelectedIndexChanged="dgvUsuarios_SelectedIndexChanged">
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
        
        <asp:Button Text="Modificar Usuario" ID="btnModificar" runat="server" 
                        class="btn btn-outline-info" OnClick="btnModificar_Click" Enabled="false"/>

    </main>
</asp:Content>
