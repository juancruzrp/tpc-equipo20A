using Acceso;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {                
                datos.setearConsulta("SELECT IDMarca, Marca, Estado FROM Marcas");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.IDMarca = datos.Lector["IDMarca"] != DBNull.Value
                        ? Convert.ToInt32(datos.Lector["IDMarca"])
                        : 0;
                    aux.Nombre = datos.Lector["Marca"] != DBNull.Value
                        ? datos.Lector["Marca"].ToString()
                        : "Sin marca";
                    aux.Estado = (bool)datos.Lector["Estado"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar marcas: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void CambiarEstado(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Marcas SET Estado = @estado WHERE IDMarca = @id");
                datos.setearParametro("@estado", marca.Estado);
                datos.setearParametro("@id", marca.IDMarca);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar estado de la marca: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void agregar(Marca nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Marcas (Marca,Estado) VALUES (@nombre, @estado)");
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
        public void modificar(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            UPDATE Marcas
            SET Marca = @nombre,
                Estado = @estado
            WHERE IDMarca = @idMarca");

                datos.setearParametro("@nombre", marca.Nombre);
                datos.setearParametro("@estado", marca.Estado);
                datos.setearParametro("@idMarca", marca.IDMarca);

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
<<<<<<< HEAD
        }        
=======
        }

        public Marca obtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Marca marca = null;
            try
            {
                datos.setearConsulta("SELECT IDMarca, Marca AS Nombre, Estado FROM Marcas WHERE IDMarca = @idMarca");
                datos.setearParametro("@idMarca", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    marca = new Marca();
                    marca.IDMarca = (int)datos.Lector["IDMarca"];
                    marca.Nombre = (string)datos.Lector["Nombre"];
                    marca.Estado = (bool)datos.Lector["Estado"];
                }

                return marca;
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

       
>>>>>>> 8a39f7095c8df8e998c601e7a202371c5ac313bb

    }
}
