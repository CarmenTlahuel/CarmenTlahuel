using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ClassAcceso
{
    public class AccesoSQl
    {
        public string cadConexion { get; set; }
        public AccesoSQl(string cadenaBD)
        {
            cadConexion = cadenaBD;
        }

        public SqlConnection AbrirConexion(ref string mensaje) // Metodo con parametros de referencia
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = cadConexion;

            try
            {
                conexion.Open();
                mensaje = "Conexion abierta";

            }
            catch (Exception aviso)
            {
                mensaje = "ERROR: " + aviso.Message;
            }
            return conexion;
        }

        public Boolean InsertarBD(string ConsultaSQL, SqlConnection ConexionBD, ref string mensaje, SqlParameter[] parametro)
        {
            Boolean conexion = false;
            SqlCommand datos = null;
            if (ConexionBD != null)
            {
                mensaje = "Conexion Abierta";
                using (datos = new SqlCommand())
                {
                    datos.CommandText = ConsultaSQL;
                    datos.Connection = ConexionBD;
                    foreach (SqlParameter param in parametro)
                    {
                        datos.Parameters.Add(param);
                    }
                    try
                    {
                        datos.ExecuteNonQuery();
                        mensaje = "Registro Correcto";
                        conexion = true;
                    }
                    catch (Exception MsjError)
                    {
                        mensaje = "Error: " + MsjError.Message;
                        conexion = false;
                    }
                }
                ConexionBD.Close();
                ConexionBD.Dispose();
            }
            else
            {
                mensaje = "No hay conexión con la base de datos";
                conexion = false;
            }
            return conexion;
        }



        public SqlDataReader Consulta_DataReader(string ConsultaSQL, SqlConnection conexionBD, ref string mensaje)
        {
            SqlCommand datos = null;
            SqlDataReader conexion;
            if (conexionBD == null)
            {
                conexion = null;
                mensaje = "No hay conexión con la base de datos";
            }
            else
            {
                using (datos = new SqlCommand(ConsultaSQL, conexionBD))
                {
                    try
                    {
                        conexion = datos.ExecuteReader();
                        mensaje = "Consulta de datos correcta";
                    }
                    catch (Exception MsjError)
                    {
                        mensaje = "Error: " + MsjError.Message;
                        conexion = null;
                    }
                }
            }
            return conexion;
        }

        public DataSet Consulta_DataSet(string ConsultaSQL, SqlConnection conexionBD, ref string mensaje, SqlParameter[] parametro)
        {
            SqlCommand datos = null;
            DataSet conexion = null;
            SqlDataAdapter PaqueteDatos = null;
            if (conexionBD == null)
            {
                conexion = null;
                mensaje = "No hay conexión con la base de datos";
            }
            else
            {
                using (datos = new SqlCommand(ConsultaSQL, conexionBD))
                {
                    using (PaqueteDatos = new SqlDataAdapter())
                    {
                        conexion = new DataSet();
                        PaqueteDatos.SelectCommand = datos;
                        foreach (SqlParameter param in parametro)
                        {
                            datos.Parameters.Add(param);
                        }
                        try
                        {
                            PaqueteDatos.Fill(conexion);
                            mensaje = "Consulta Correcta";
                        }
                        catch (Exception MsjError)
                        {
                            mensaje = "Error: " + MsjError.Message;
                            conexion = null;
                        }
                    }
                }
                conexionBD.Close();
                conexionBD.Dispose();
            }
            return conexion;
        }


        public DataSet Consulta_DataSet_Simple(string ConsultaSQL, SqlConnection conexionBD, ref string mensaje)
        {
            SqlCommand datos = null;
            DataSet conexion = null;
            SqlDataAdapter PaqueteDatos = null;
            if (conexionBD == null)
            {
                conexion = null;
                mensaje = "No hay conexión con la base de datos";
            }
            else
            {
                using (datos = new SqlCommand(ConsultaSQL, conexionBD))
                {
                    using (PaqueteDatos = new SqlDataAdapter())
                    {
                        conexion = new DataSet();
                        PaqueteDatos.SelectCommand = datos;
                        try
                        {
                            PaqueteDatos.Fill(conexion);
                            mensaje = "Consulta Correcta";
                        }
                        catch (Exception MsjError)
                        {
                            mensaje = "Error: " + MsjError.Message;
                            conexion = null;
                        }
                    }
                }
                conexionBD.Close();
                conexionBD.Dispose();
            }
            return conexion;
        }



        public Boolean ModificarBD(string ConsultaSQL, SqlConnection ConexionBD, ref string mensaje)
        {
            Boolean conexion = false;
            SqlCommand datos = null;
            if (ConexionBD != null)
            {
                mensaje = "Conexion Abierta";
                using (datos = new SqlCommand())
                {
                    datos.CommandText = ConsultaSQL;
                    datos.Connection = ConexionBD;
                    try
                    {
                        datos.ExecuteNonQuery();
                        mensaje = "Modificacion Correcta";
                        conexion = true;
                    }
                    catch (Exception MsjError)
                    {
                        mensaje = "Error: " + MsjError.Message;
                        conexion = false;
                    }
                }
                ConexionBD.Close();
                ConexionBD.Dispose();
            }
            else
            {
                mensaje = "No hay conexión con la base de datos";
                conexion = false;
            }
            return conexion;
        }



        public Boolean EliminarBD(string ConsultaSQL, SqlConnection ConexionBD, ref string mensaje)
        {
            Boolean conexion = false;
            SqlCommand datos = null;
            if (ConexionBD != null)
            {
                mensaje = "Conexion Abierta";
                using (datos = new SqlCommand())
                {
                    datos.CommandText = ConsultaSQL;
                    datos.Connection = ConexionBD;
                    try
                    {
                        datos.ExecuteNonQuery();
                        mensaje = "Eliminacion Correcta";
                        conexion = true;
                    }
                    catch (Exception MsjError)
                    {
                        mensaje = "Error: " + MsjError.Message;
                        conexion = false;
                    }
                }
                ConexionBD.Close();
                ConexionBD.Dispose();
            }
            else
            {
                mensaje = "No hay conexión con la base de datos";
                conexion = false;
            }
            return conexion;
        }
    }
}
