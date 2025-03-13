using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class AccesoDatos
    {
        public SqlCommand Comando { get; set; }
        public SqlConnection Conexion { get; set; }
        public SqlDataReader Lector { get; set; }

        public AccesoDatos()
        {
            Comando = new SqlCommand();
            Conexion= new SqlConnection("server= .\\SQLEXPRESS; database=CATALOGO_DB; integrated security=true;");
        }

        public void setearConsulta(String consulta) 
        {

            Comando.CommandType = System.Data.CommandType.Text;
            Comando.CommandText = consulta;
        }
        public void EjecutarLectura()
        {
            Comando.Connection = Conexion;
            Conexion.Open();
            Lector= Comando.ExecuteReader();
        }

        public void CerrarConexion()
        {
            if(Lector != null)
                Lector.Close();
            Comando.Parameters.Clear();
            Conexion.Close();
        }

        public void setearParametro(String variable, object valor)
        {
            Comando.Parameters.AddWithValue(variable, valor);
        }
    }
}
