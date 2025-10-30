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

