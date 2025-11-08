using Acceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

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
                    ISNULL(MIN(I.ImagenUrl), 'https://via.placeholder.com/60x60?text=No+Image') AS ImagenUrl
                FROM 
                    PRODUCTOS P
                LEFT JOIN 
                    Imagenes I ON P.IDProducto = I.IDProducto
                LEFT JOIN 
                        Marcas M ON P.IDMarca = M.IDMarca
                GROUP BY
                    P.IDProducto, P.Nombre, P.Descripcion, P.Precio, P.Stock, P.Estado, P.IDMarca, M.Marca
            ";

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

                    aux.Marca = new Marca();
                    aux.Marca.IDMarca = datos.Lector["IDMarca"] != DBNull.Value
                        ? Convert.ToInt32(datos.Lector["IDMarca"])
                        : 0;
                    aux.Marca.Nombre = datos.Lector["NombreMarca"] != DBNull.Value
                        ? datos.Lector["NombreMarca"].ToString()
                        : "Sin marca";

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
                datos.setearConsulta("DELETE FROM PRODUCTOS WHERE IDProducto = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }




    /*public List<Producto> listar()
    {
        List<Producto> lista = new List<Producto>();
        AccesoDatos datos = new AccesoDatos();
        try
        {
            string consulta = "SELECT IDProducto, Nombre, Descripcion, Precio, Stock, Estado FROM PRODUCTOS";

            datos.setearConsulta(consulta);
            datos.ejecutarLectura();
            while (datos.Lector.Read())
            {
                Producto aux = new Producto();
                aux.IDProducto = (int)datos.Lector["IDProducto"];

                if (!(datos.Lector["Nombre"] is DBNull))
                    aux.Nombre = (string)datos.Lector["Nombre"];

                if (!(datos.Lector["Descripcion"] is DBNull))
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                aux.Precio = datos.Lector["Precio"] != DBNull.Value ? Convert.ToDecimal(datos.Lector["Precio"]) : 0;

                aux.Stock = datos.Lector["Stock"] != DBNull.Value ? Convert.ToInt32(datos.Lector["Stock"]) : 0;

                if (!(datos.Lector["Estado"] is DBNull))
                   aux.Estado = Convert.ToInt32(datos.Lector["Estado"]) == 1;
                else
                    aux.Estado = false;

                lista.Add(aux);
            }
            return lista;
        }
        catch (Exception ex)
        {

            throw ex;
        }
        finally
        {
            datos.cerrarConexion();
        }
    }
   */
}

