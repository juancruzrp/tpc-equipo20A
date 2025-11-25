<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaCategorias.aspx.cs" Inherits="WebApplication3.AltaCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="container py-4">
        <div class="card shadow-sm border-0 p-4 mx-auto" style="max-width: 600px;">
            <h3 class="text-center mb-4">
                <%: (ViewState["IdCategoria"] != null) ? "Modificar Categoría" : "Agregar Categoría" %>
            </h3>
                        
            <asp:TextBox ID="txtIdCategoria" runat="server" Visible="false" />
                        
            <div class="mb-3">
                <label class="form-label">Nombre de la Categoría:</label>
                <asp:TextBox ID="txtNombreCategoria" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="d-flex justify-content-between">
                <asp:Button ID="btnGuardarCategoria" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardarCategoria_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" PostBackUrl="~/MarcasYCategorias.aspx" />
            </div>

            <asp:Label ID="lblError" runat="server" CssClass="text-danger mt-3"></asp:Label>
        </div>
    </div>

</asp:Content>
