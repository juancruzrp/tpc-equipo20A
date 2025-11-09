using Acceso;
using Dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductoNegocio
    {

        public List<Producto> listar()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos(); 
            try
            {
                string consulta = @"
                SELECT 
                    P.IDProducto, 
                    P.Nombre, 
                    P.Descripcion, 
                    P.Precio, 
                    P.Stock, 
                    P.Estado,
                    P.IDMarca,
                    M.Marca AS NombreMarca,
                    P.IDCategoria,
                    C.Categoria AS NombreCategoria,
                    ISNULL(MIN(I.ImagenUrl), 'https://via.placeholder.com/60x60?text=No+Image') AS ImagenUrl
                FROM 
                    PRODUCTOS P
                LEFT JOIN 
                    Imagenes I ON P.IDProducto = I.IDProducto
                LEFT JOIN 
                    Marcas M ON P.IDMarca = M.IDMarca
                LEFT JOIN 
                    Categorias C ON P.IDCategoria = C.IDCategoria
                WHERE 
                    P.Estado = 1  
                GROUP BY
                    P.IDProducto, P.Nombre, P.Descripcion, P.Precio, P.Stock, P.Estado,
                    P.IDMarca, M.Marca, P.IDCategoria, C.Categoria";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.IDProducto = (int)datos.Lector["IDProducto"];

                    if (!(datos.Lector["Nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["Nombre"];
                    else
                        aux.Nombre = ""; 

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];
                    else
                        aux.Descripcion = ""; 
                    aux.Precio = datos.Lector["Precio"] != DBNull.Value ? Convert.ToDecimal(datos.Lector["Precio"]) : 0;
                    aux.Stock = datos.Lector["Stock"] != DBNull.Value ? Convert.ToInt32(datos.Lector["Stock"]) : 0;

                   
                    if (!(datos.Lector["Estado"] is DBNull))
                        aux.Estado = (bool)datos.Lector["Estado"]; 
                    else
                        aux.Estado = false;

                    aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.IDCategoria = datos.Lector["IDCategoria"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IDCategoria"]) : 0;
                    aux.Categoria.Nombre = datos.Lector["NombreCategoria"] != DBNull.Value ? datos.Lector["NombreCategoria"].ToString() : "Sin categoría";

                    aux.Marca = new Marca();
                    aux.Marca.IDMarca = datos.Lector["IDMarca"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IDMarca"]) : 0;
                    aux.Marca.Nombre = datos.Lector["NombreMarca"] != DBNull.Value ? datos.Lector["NombreMarca"].ToString() : "Sin marca";

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar productos: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM Imagenes WHERE IDProducto = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();

                datos.setearConsulta("DELETE FROM PRODUCTOS WHERE IDProducto = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar producto: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminarLogico(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Productos SET Estado = 0 WHERE IDProducto = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar producto: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
      
        public void agregar(Producto nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    INSERT INTO Productos (Nombre, Descripcion, IDCategoria, IDMarca, Precio, Stock, Estado)
                    VALUES (@Nombre, @Descripcion, @IDCategoria, @IDMarca, @Precio, @Stock, @Estado);
                    SELECT SCOPE_IDENTITY();
                ");

                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IDCategoria", nuevo.Categoria.IDCategoria);
                datos.setearParametro("@IDMarca", nuevo.Marca.IDMarca);
                datos.setearParametro("@Precio", nuevo.Precio);
                datos.setearParametro("@Stock", nuevo.Stock);
                datos.setearParametro("@Estado", nuevo.Estado);

                object id = datos.ejecutarScalar();
                int nuevoID = Convert.ToInt32(id);

                if (!string.IsNullOrEmpty(nuevo.ImagenUrl))
                {
                    datos.limpiarParametros();
                    datos.setearConsulta(@"
                        INSERT INTO Imagenes (IDProducto, ImagenUrl)
                        VALUES (@IDProducto, @ImagenUrl)
                    ");
                    datos.setearParametro("@IDProducto", nuevoID);
                    datos.setearParametro("@ImagenUrl", nuevo.ImagenUrl);
                    datos.ejecutarAccion();
                }
                               
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar producto: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // actualizar los datos del producto
                datos.setearConsulta(@"
                UPDATE Productos 
                SET Nombre = @Nombre,
                    Descripcion = @Descripcion,
                    IDCategoria = @IDCategoria,
                    IDMarca = @IDMarca,
                    Precio = @Precio,
                    Stock = @Stock,
                    Estado = @Estado
                WHERE IDProducto = @IDProducto
                ");

                datos.setearParametro("@Nombre", producto.Nombre);
                datos.setearParametro("@Descripcion", producto.Descripcion);
                datos.setearParametro("@IDCategoria", producto.Categoria.IDCategoria);
                datos.setearParametro("@IDMarca", producto.Marca.IDMarca);
                datos.setearParametro("@Precio", producto.Precio);
                datos.setearParametro("@Stock", producto.Stock);
                datos.setearParametro("@Estado", producto.Estado);
                datos.setearParametro("@IDProducto", producto.IDProducto);

                datos.ejecutarAccion();

                // actualizar la imagen 
                if (!string.IsNullOrEmpty(producto.ImagenUrl))
                {
                    datos.limpiarParametros();
                    datos.setearConsulta(@"
                IF EXISTS (SELECT 1 FROM Imagenes WHERE IDProducto = @IDProducto)
                    UPDATE Imagenes SET ImagenUrl = @ImagenUrl WHERE IDProducto = @IDProducto
                ELSE
                    INSERT INTO Imagenes (IDProducto, ImagenUrl)
                    VALUES (@IDProducto, @ImagenUrl)
            ");
                    datos.setearParametro("@IDProducto", producto.IDProducto);
                    datos.setearParametro("@ImagenUrl", producto.ImagenUrl);
                    datos.ejecutarAccion();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar producto: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }    
   
}

