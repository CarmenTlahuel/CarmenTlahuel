using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ClassAcceso;
using ClassEntidad;

namespace ClassNegocio
{
    public class LogicaNegocio
    {
        public AccesoSQl nuevo = null;

        public LogicaNegocio(string conec)
        {
            nuevo = new AccesoSQl(conec);
        }

        public List<Usuario> Verusuarios(ref string m)
        {
            List<Usuario> lista = new List<Usuario>();

            SqlDataReader atrapa = null;

            SqlConnection nuev = null;
            nuev = nuevo.AbrirConexion(ref m);

            string consulta = "Select NOMBRE from EMPLEDO";

            atrapa = nuevo.Consulta_DataReader(consulta, nuev, ref m);

            if (atrapa != null)
            {
                while (atrapa.Read())
                {
                    lista.Add(new Usuario()
                    {

                        nombre = (string)atrapa[0]
                    }
                    );
                }
            }
            nuev.Close();
            nuev.Dispose();
            return lista;

        }
        public DataTable VerPublicaciones(ref string m)
        {
            string Ver = "";
            DataSet guarda = null;
            DataTable resultado = null;
            guarda = nuevo.Consulta_DataSet_Simple(Ver, nuevo.AbrirConexion(ref m), ref m);
            if (guarda != null)
            {
                resultado = guarda.Tables[0];

            }
            return resultado;
        }
        public Boolean usuarionuevo(Usuario nueva, ref string m)
        {
            string Insertar = "insert into Usuario(nombre, colonia, numero, cp, nom_centroTrabajo, telefono)" +
                " values (@nom, @col, @num, @cp, @nct, @tel )";
            SqlParameter[] coleccion = new SqlParameter[]
            {

                new SqlParameter("nom", SqlDbType.VarChar,40),
                new SqlParameter("col", SqlDbType.VarChar,20),
                new SqlParameter("num", SqlDbType.Int),
                new SqlParameter("cp", SqlDbType.Int),
                new SqlParameter("nct", SqlDbType.VarChar,30),
                new SqlParameter("tel", SqlDbType.VarChar,12)

            };

            coleccion[0].Value = nueva.nombre;
            coleccion[1].Value = nueva.colonia;
            coleccion[2].Value = nueva.numero;
            coleccion[3].Value = nueva.cp;
            coleccion[4].Value = nueva.nom_centroTrabajo;
            coleccion[5].Value = nueva.telefono;

            Boolean salida = false;

            salida = nuevo.InsertarBD(Insertar, nuevo.AbrirConexion(ref m), ref m, coleccion);

            return salida;
        }
            public DataTable MostarTodasLasMarcas(ref string mensaje)
        {
                string consulta = "Select * from Marca";
                DataSet obtener = null;
                DataTable salida = null;
                obtener = operacion.ConsultaDataSet(consulta, operacion.Abrirconexion(ref mensaje), ref mensaje);
               if (obtener != null)
                     {
                            salida = obtener.Tables[0];
                     }
                return salida;
            }
             public Boolean ModificarMarca(int id_Marca, string Marca, ref string m)
                {
                    string sentencia = "UPDATE Marca SET Marca='" + Marca + "' where id_Marca =" + id_Marca + ";";
                    Boolean salida = false;
                    operacion.ModificarBDmasSeguro(sentencia, operacion.Abrirconexion(ref m), ref m);
                    return salida;
                }
             public Boolean EliminarMarca(int id_Marca, ref string m)
                {
                    string sentencia = "DELETE from Categoria where id_Marca =" + id_Marca + ";";
                    Boolean salida = false;
                    operacion.ModificarBDmasSeguro(sentencia, operacion.Abrirconexion(ref m), ref m);
                    return salida;
                }

    }
}
