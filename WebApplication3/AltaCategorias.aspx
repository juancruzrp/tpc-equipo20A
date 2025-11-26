<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaCategorias.aspx.cs" Inherits="WebApplication3.AltaCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-4">
        <div class="card shadow-sm border-0 p-4 mx-auto" style="max-width: 600px;">
            <h3 class="text-center mb-4">
                <%: (ViewState["IdCategoria"] != null) ? "Modificar Categoría" : "Agregar Categoría" %>
            </h3>

            <asp:TextBox ID="txtIdCategoria" runat="server" Visible="false" />

            <asp:Panel runat="server" DefaultButton="btnGuardarCategoria">
                <div class="mb-3">
                    <label class="form-label">Nombre de la Categoría:</label>
                    <asp:TextBox ID="txtNombreCategoria" runat="server" CssClass="form-control"></asp:TextBox>
                    
                    <!-- validacion de campo obligatorio -->
                    <asp:RequiredFieldValidator ID="rfvNombreCategoria" runat="server"
                        ControlToValidate="txtNombreCategoria"
                        ErrorMessage="El nombre de la categoria es obligatorio"
                        CssClass="text-danger" Display="None" />
                    
                    <!-- Validacion para no permitir simbolos -->
                    <asp:RegularExpressionValidator ID="revNombreCategoria" runat="server"
                        ControlToValidate="txtNombreCategoria"
                        ValidationExpression="^[a-zA-Z0-9\s]*$"
                        ErrorMessage="No se permiten los símbolos ?, $, *, %,!,etc."
                        CssClass="text-danger" Display="None" />
                    
                    <!-- Validacion de minimo 5 letras y/o numeros -->
                    <asp:RegularExpressionValidator ID="revMinLetras" runat="server"
                        ControlToValidate="txtNombreCategoria"
                        ValidationExpression="^.{5,}$"
                        ErrorMessage="El nombre debe tener al menos 5 letras y/o numeros"
                        CssClass="text-danger" Display="None"/>
                </div>
                    <asp:ValidationSummary ID="ErroresCat" runat="server"
                        CssClass="alert alert-danger text-center"
                        HeaderText="Errores encontrados:" />

                <div class="d-flex justify-content-between">
                    <asp:Button ID="btnGuardarCategoria" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardarCategoria_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" PostBackUrl="~/MarcasYCategorias.aspx"
                        CausesValidation="false" OnClick="btnCancelar_Click"
                        OnClientClick="return confirm('¿Seguro desea cancelar esta operación?');" />
                </div>
            </asp:Panel>

            <asp:Label ID="lblError" runat="server" CssClass="text-danger mt-3"></asp:Label>
        </div>
    </div>

</asp:Content>
