use COMERCIOTPC
GO
SET Dateformat YMD
GO
INSERT INTO TiposUsuario(TipoUsuario)
VALUES
('Usuario Vendedor'),
('Usuario Administrador');

INSERT INTO Clientes (Nombre, Apellido, Telefono, Mail, Direccion)
VALUES
('Lucía', 'Fernández', '1123456789', 'lucia.fernandez@mail.com', 'Av. Santa Fe 1234'),
('Martín', 'Gómez', '1134567890', 'martin.gomez@mail.com', 'Calle 9 de Julio 567'),
('Carla', 'Pérez', '1145678901', 'carla.perez@mail.com', 'Av. Corrientes 789');

INSERT INTO Proveedores (Nombre, Telefono, Mail, Direccion)
VALUES
('Hierros del Sur', '1122334455', 'ventas@hierrosdelsur.com', 'Av. Belgrano 1200'),
('Tornillos Argentinos S.A.', '1133445566', 'contacto@tornillosar.com', 'Calle Rivadavia 450'),
('Pinturas Delta', '1144556677', 'info@pinturasdelta.com', 'Ruta 8 Km 32'),
('Ferremax Distribuciones', '1155667788', 'ventas@ferremax.com', 'Av. San Martín 980'),
('Electricidad Norte', '1166778899', 'soporte@electricidadnorte.com', 'Calle Mitre 210');

INSERT INTO Marcas (Marca)
VALUES
('Stanley'),
('Bahco'),
('Black+Decker'),
('3M'),
('Philips'),
('Sinteplast'),
('Bremen'),
('Irwin'),
('Voltech');

INSERT INTO Categorias (Categoria)
VALUES
('Herramientas manuales'),
('Herramientas eléctricas'),
('Fijaciones y tornillería'),
('Pinturas y revestimientos'),
('Seguridad y protección'),
('Electricidad'),
('Iluminación'),
('Medición y precisión');


INSERT INTO Productos (Nombre, Descripcion, IDCategoria, IDMarca, Precio, Stock, Estado)
VALUES
('Martillo de carpintero', 'Cabeza de acero y mango de madera', 1, 1, 3500, 25, 1),
('Llave inglesa 10"', 'Ajustable, acero cromado', 1, 2, 4800, 15, 1),
('Caja de tornillos 4mm x 50mm', 'Caja con 100 unidades, acero zincado', 2, 3, 1200, 40, 1),
('Taladro eléctrico 650W', 'Velocidad variable, incluye brocas', 3, 4, 28000, 10, 1),
('Pintura látex blanca 4L', 'Interior/exterior, lavable', 4, 5, 7500, 20, 1),
('Cinta métrica 5m', 'Retráctil, carcasa plástica', 1, 2, 1500, 30, 1),
('Destornillador plano 6"', 'Mango ergonómico, punta magnética', 1, 1, 900, 50, 1),
('Brocas para metal x5', 'Juego de brocas de acero rápido', 3, 4, 3200, 18, 1),
('Sierra manual para madera', 'Hoja de acero templado, mango plástico', 1, 2, 2700, 12, 1),
('Guantes de trabajo reforzados', 'Cuero sintético, talla única', 5, 5, 2200, 35, 1),
('Cable eléctrico 2x1.5mm 10m', 'Aislado, uso domiciliario', 6, 5, 4300, 22, 1),
('Lámpara LED 12W', 'Rosca E27, luz blanca fría', 6, 5, 1100, 60, 1);


INSERT INTO Usuarios(NombreUsuario,Contraseña,IDTipoUsuario,FechaAlta,FechaBaja,Estado)
VALUES
('usuario','usuario',1,GETDATE(),NULL,1),
('admin','admin',2,GETDATE(),NULL,1);

INSERT INTO Imagenes (IDProducto, ImagenUrl)
VALUES
(1, 'https://arcencohogar.vtexassets.com/arquivos/ids/331341/9008367-1.jpg?v=637963583955930000'),
(2, 'https://acdn-us.mitiendanube.com/stores/001/229/031/products/diseno-sin-titulo-891-4993f3f0bc3ef7464d16765609980601-1024-1024.png'),
(3, 'https://argoselectrica.com/wp-content/uploads/2019/10/tornillos-hexagonales-1.png'),
(4, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSYFTZY66QnF-B5vvT9PAS1K3FbykfmpUZECw&s'),
(5, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSiPjVhU1pJHb4VA4o3_sGa7spYbE0g2lJPTA&s');

INSERT INTO Imagenes (IDProducto, ImagenUrl)
VALUES
(6, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSL66bsCSk-nYRH-tgohX2OrortMuCQV5h4Eg&s'),
(7, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRFMeKUc-OBTIb8_CcmuuunoNIhCR3z0rISNw&s'),
(8, 'https://cdn.wurth.com.ar/img/productos/351632-gr-mecha-hss-din-338-d2-5-mm.jpg'),
(9, 'https://http2.mlstatic.com/D_820351-MLA42314967558_062020-C.jpg'),
(10, 'https://http2.mlstatic.com/D_NQ_NP_616758-MLA77700361598_072024-O.webp');

INSERT INTO Imagenes (IDProducto, ImagenUrl)
VALUES
(11, 'https://http2.mlstatic.com/D_NQ_NP_720599-MLA79237976178_092024-O.webp'),
(12, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTVg9RSB7ZCDxd9WJ-rtLsQyei1zd71GDPHQw&s');


INSERT INTO Compras (Fecha, IDProveedor, IDUsuario, Total) VALUES
('2023-11-01', 1, 2, 12500.00),
('2023-11-02', 3, 1, 8750.00),  
('2023-11-03', 2, 2, 15300.00), 
('2023-11-04', 4, 1, 9800.00),  
('2023-11-05', 5, 2, 6500.00);  


INSERT INTO Detalle_Compra (IDCompra, IDProducto, Cantidad, PrecioUnitario) VALUES
-- Detalles para Compra 1 (IDCompra = 1)
(1, 1, 10, 3500.00), 
(1, 7, 20, 900.00),  

-- Detalles para Compra 2 (IDCompra = 2)
(2, 5, 5, 7500.00),  
(2, 9, 8, 2700.00),  

-- Detalles para Compra 3 (IDCompra = 3)
(3, 3, 30, 1200.00), 
(3, 8, 10, 3200.00), 

-- Detalles para Compra 4 (IDCompra = 4)
(4, 2, 15, 4800.00), 
(4, 6, 25, 1500.00), 

-- Detalles para Compra 5 (IDCompra = 5)
(5, 11, 12, 4300.00), 
(5, 12, 40, 1100.00);