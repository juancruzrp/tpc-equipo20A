using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso
{
    public class AccesoDatos
    {

        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public SqlConnection Conexion
        {
            get { return conexion; }
        }

        public AccesoDatos()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=COMERCIOTPC; integrated security=true");
            /// crear otra conexion
            ///conexion = new SqlConnection("");
            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void cerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }

        public object ejecutarScalar()
        {
            try
            {
                comando.Connection = conexion;
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                object result = comando.ExecuteScalar();
                conexion.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }

        public void limpiarParametros()
        {
            comando.Parameters.Clear();
        }

        public DataTable EjecutarConsulta(string query, params SqlParameter[] parametros)
        {
            comando.Connection = conexion;
            comando.CommandText = query;
            comando.Parameters.Clear();
            comando.Parameters.AddRange(parametros);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(comando);

            try
            {
                conexion.Open();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Cierra la conexión y libera recursos en el bloque finally
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                da.Dispose();
            }
        }
        public void setearProcedimiento(string nombreProcedimiento)
        {
            comando = new SqlCommand(nombreProcedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;
        }

    }
}
