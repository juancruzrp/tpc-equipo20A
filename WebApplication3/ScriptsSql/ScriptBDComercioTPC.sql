CREATE DATABASE COMERCIOTPC
Collate Latin1_General_CI_AI
GO
USE COMERCIOTPC;
GO
CREATE TABLE TiposUsuario(
	IDTipoUsuario INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	TipoUsuario VARCHAR(35) NOT NULL, 
)
GO
CREATE TABLE Usuarios (
    IDUsuario INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario NVARCHAR(50) NOT NULL,
    Contraseña NVARCHAR(15) NOT NULL,
    IDTipoUsuario INT NOT NULL,
    FechaAlta DATETIME NOT NULL DEFAULT GETDATE(),
    FechaBaja DATETIME NULL DEFAULT GETDATE(),
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (IDTipoUsuario) REFERENCES TiposUsuario(IDTipoUsuario)
)
GO
CREATE TABLE Clientes(
    IDCliente INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Telefono VARCHAR(15),
    Mail NVARCHAR(50),
    Direccion VARCHAR(75),
    CUIT_CUIL VARCHAR(15),
)
GO
CREATE TABLE Proveedores(
    IDProveedor INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL,
    Telefono VARCHAR(15),
    Mail NVARCHAR(50),
    Direccion VARCHAR(75),
    CUIT_CUIL VARCHAR(15),
)
GO
CREATE TABLE Marcas(
    IDMarca INT PRIMARY KEY IDENTITY(1,1),
    Marca VARCHAR(50) NOT NULL,
)
GO
CREATE TABLE Categorias(
    IDCategoria INT PRIMARY KEY IDENTITY(1,1),
    Categoria VARCHAR(50) NOT NULL,
)
GO 
CREATE TABLE Productos(
    IDProducto INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL,
    Descripcion VARCHAR(200) NULL,
    IDCategoria INT,
    IDMarca INT,
    Precio MONEY NOT NULL,
    Stock INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (IDCategoria) REFERENCES Categorias(IDCategoria),
    FOREIGN KEY (IDMarca) REFERENCES Marcas(IDMarca),
)
GO
CREATE TABLE Ventas(
    IDVenta INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    IDCliente INT,
    IDUsuario INT,
    Total DECIMAL(10,2),
    FOREIGN KEY (IDCliente) REFERENCES Clientes(IDCliente),
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),

)
GO
CREATE TABLE Detalle_Venta(
    IDDetalleVenta INT PRIMARY KEY IDENTITY(1,1),
    IDVenta INT,
    IDProducto INT,
    Cantidad INT NOT NULL,
    PrecioUnitario MONEY NOT NULL,
    FOREIGN KEY (IDVenta) REFERENCES Ventas(IDVenta),
    FOREIGN KEY (IDProducto) REFERENCES Productos(IDProducto),
)
GO
CREATE TABLE Compras(
    IDCompra INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    IDProveedor INT,
    IDUsuario INT,
    Total DECIMAL(10,2),
    FOREIGN KEY (IDProveedor) REFERENCES Proveedores(IDProveedor),
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),

)
GO
CREATE TABLE Detalle_Compra(
    IDDetalleCompra INT PRIMARY KEY IDENTITY(1,1),
    IDCompra INT,
    IDProducto INT,
    Cantidad INT NOT NULL,
    PrecioUnitario MONEY NOT NULL,
    FOREIGN KEY (IDCompra) REFERENCES Compras(IDCompra),
    FOREIGN KEY (IDProducto) REFERENCES Productos(IDProducto),
)
GO
CREATE TABLE Imagenes(
	IDProducto INT NOT NULL,
	ImagenUrl VARCHAR (1000) NOT NULL,
	FOREIGN KEY(IDProducto) REFERENCES Productos(IDProducto)
)