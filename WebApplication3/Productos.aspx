<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="WebApplication3.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="text-center mb-4">Listado de Productos</h1>

    <div class="container text-center">
        <div class="row align-items-start">
            <asp:Repeater ID="repProductos" runat="server">
                <ItemTemplate>
                    <div class="col-md-3 mb-4">
                        <div class="card shadow-sm border-0 h-100">
                            <img src='<%# Eval("ImagenUrl") %>' 
                                 alt='<%# Eval("Nombre") %>' 
                                 class="card-img-top p-2" 
                                 style="object-fit: contain; height: 150px;" 
                                 onerror="this.src='https://via.placeholder.com/150x150?text=Sin+imagen';" />
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                <p class="card-text text-muted"><%# Eval("Descripcion") %></p>
                                <p class="fw-bold">Precio: $<%# string.Format("{0:N2}", Eval("Precio")) %></p>
                                <p>Stock: <%# Eval("Stock") %></p>
                                <p class="text-success"><%# (bool)Eval("Estado") ? "Activo" : "Inactivo" %></p>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

</asp:Content>