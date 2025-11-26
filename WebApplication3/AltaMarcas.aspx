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
            <asp:Panel runat="server" DefaultButton="btnGuardarMarca">

                <div class="mb-3">
                    <label for="txtNombreMarca" class="form-label">Nombre de la Marca:</label>
                    <asp:TextBox ID="txtNombreMarca" runat="server" CssClass="form-control"></asp:TextBox>
                    <!-- validacion de campo obligatorio -->
                    <asp:RequiredFieldValidator ID="rfvNombreMarca" runat="server"
                        ControlToValidate="txtNombreMarca"
                        ErrorMessage="El nombre de la marca es obligatorio"
                        CssClass="text-danger" Display="None" />
                    <!-- Validacion para no permitir simbolos -->
                    <asp:RegularExpressionValidator ID="revNombreMarca" runat="server"
                        ControlToValidate="txtNombreMarca"
                        ValidationExpression="^[a-zA-Z0-9\s]*$"
                        ErrorMessage="No se permiten los símbolos @, #, $, %,?,etc."
                        CssClass="text-danger" Display="None" />
                    <!-- Validacion de minimo 2 letras y/o numeros -->
                    <asp:RegularExpressionValidator ID="revMinLetras" runat="server"
                        ControlToValidate="txtNombreMarca"
                        ValidationExpression="^.{2,}$"
                        ErrorMessage="El nombre debe tener al menos 4 letras y/o numeros"
                        CssClass="text-danger"
                        Display="None" />
                    
                </div>
                    <asp:ValidationSummary ID="ErroresMar" runat="server"
                        CssClass="alert alert-danger text-center"
                        HeaderText="Errores encontrados:" />

                <div class="mb-3">
                    <div class="d-flex justify-content-between">
                        <asp:Button ID="btnGuardarMarca" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardarMarca_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary"
                            CausesValidation="false" OnClick="btnCancelar_Click"
                            OnClientClick="return confirm('¿Seguro desea cancelar esta operación?');" />
                    </div>
                </div>
                <asp:Label ID="lblError" runat="server" CssClass="text-danger mt-3"></asp:Label>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
