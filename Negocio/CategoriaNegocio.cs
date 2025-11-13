using Acceso;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {                
                datos.setearConsulta("SELECT IDCategoria, Categoria, Estado FROM Categorias");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.IDCategoria = datos.Lector["IDCategoria"] != DBNull.Value
                        ? Convert.ToInt32(datos.Lector["IDCategoria"])
                        : 0;
                    aux.Nombre = datos.Lector["Categoria"] != DBNull.Value
                        ? datos.Lector["Categoria"].ToString()
                        : "Sin categoría";
                    aux.Estado = (bool)datos.Lector["Estado"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar categorías: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void CambiarEstado(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Categorias SET Estado = @estado WHERE IDCategoria = @id");
                datos.setearParametro("@estado", categoria.Estado);
                datos.setearParametro("@id", categoria.IDCategoria);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar estado de la categoria: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void agregar(Categoria nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Categorias (Categoria,Estado) VALUES (@nombre, @estado)");
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@estado", nuevo.Estado);
                datos.ejecutarAccion();
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
        public void modificar(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            UPDATE Categorias
            SET Categoria = @nombre,
                Estado = @estado
            WHERE IDCategoria = @idCategoria");

                datos.setearParametro("@nombre", categoria.Nombre);
                datos.setearParametro("@estado", categoria.Estado);
                datos.setearParametro("@idUsuario", categoria.IDCategoria);

                datos.ejecutarAccion();
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
    }
}
