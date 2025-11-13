<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="WebApplication3.Error" %>
<%@ Import Namespace="WebApplication3.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Ha Habido un problema</h1>
    <asp:Label Text="text" ID="lblMensaje" runat="server" />
   <%if (Session["Usuario"] != null)
       { %>
    <div class="mt-3">
        <div>
            <asp:Button Text="Ir al Inicio" ID="btnVolverInicio" runat="server" class="btn btn-outline-primary" OnClick="btnVolverInicio_Click" />

        </div>
    </div>
    <%}%>
    <%else
        {  %>
    <div class="mt-3">
        <div>
            <asp:Button Text="Ir a Login" ID="btnIrLogin" runat="server" class="btn btn-outline-primary" OnClick="btnIrLogin_Click" />
        </div>
        <%} %>
</asp:Content>
