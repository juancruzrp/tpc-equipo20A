<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaMarcas.aspx.cs" Inherits="WebApplication3.AltaMarcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <div class="card shadow-sm border-0 p-4 mx-auto" style="max-width: 600px;">
            <h3 class="text-center mb-4">Marca</h3>

            <!-- ID -->
            <div id="divIdMarca" runat="server" visible="false" class="mb-3">
                <label for="lblIdMarca" class="form-label">ID de la Marca:</label>
                <asp:TextBox ID="txtIdMarca" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
            </div>

            <!-- Nombre -->
            <div class="mb-3">
                <label for="txtNombreMarca" class="form-label">Nombre de la Marca:</label>
                <asp:TextBox ID="txtNombreMarca" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <div class="d-flex justify-content-between">
                    <asp:Button ID="btnGuardarMarca" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardarMarca_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" PostBackUrl="~/MarcasYCategorias.aspx" />
                </div>

                <asp:Label ID="lblError" runat="server" CssClass="text-danger mt-3"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>