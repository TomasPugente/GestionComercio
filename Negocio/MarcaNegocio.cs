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
        private List<Marca> listaMarcas;
        private AccesoDatos datos = new AccesoDatos();

        public List<Marca> listar()
        {
            try
            {
                
                datos.setearConsulta("select id, descripcion from Marcas");
                datos.EjecutarLectura();
                cargarLista();
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
            return listaMarcas;
        }

        public void cargarLista()
        {
            try
            {
                listaMarcas = null;
                listaMarcas = new List<Marca>();
                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.Lector["id"];
                    aux.Descripcion = (String)datos.Lector["descripcion"];

                    listaMarcas.Add(aux);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
